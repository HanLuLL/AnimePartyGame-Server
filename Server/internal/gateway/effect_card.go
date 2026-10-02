package gateway

import (
	"context"
	"errors"

	store "astralparty-server/internal/db"
	cfgstore "astralparty-server/internal/gdconf"
	wire "astralparty-server/internal/protocol"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

func (s *Server) usableEffectCardIDs(room store.Room, player store.Player) ([]int32, error) {
	maxHP := s.domain.HeroMaxHP(player.HeroID)
	canHeal := player.HP < maxHP
	seen := make(map[int32]struct{})
	usable := make([]int32, 0, len(player.Cards))
	for _, card := range player.Cards {
		if _, ok := seen[card.CardID]; ok {
			continue
		}
		_, isHealingCard := s.resources.EffectCardHealAmount(card.CardID)
		_, isControlMoveCard := s.resources.EffectCardControlMoveInfo(card.CardID)
		damageInfo, isDamageCard := s.resources.EffectCardDirectDamageInfo(card.CardID)
		canDamage := false
		if isDamageCard {
			targets, err := s.effectCardDamageTargetIDs(room, player, damageInfo)
			if err != nil {
				return nil, err
			}
			canDamage = len(targets) > 0
		}
		if card.CardID == store.EffectCardTransferBomb || isControlMoveCard || (canHeal && isHealingCard) || canDamage {
			seen[card.CardID] = struct{}{}
			usable = append(usable, card.CardID)
		}
	}
	return usable, nil
}

func (s *Server) effectCardDamageTargetIDs(room store.Room, actor store.Player, info cfgstore.DirectDamageCardInfo) ([]int64, error) {
	targets := make([]int64, 0, len(room.Players))
	for _, candidate := range room.Players {
		if candidate.ID == actor.ID || candidate.HP <= 0 || candidate.HospitalRounds > 0 || !s.resources.IsHero(candidate.HeroID) || roomTeamID(room.Mode, candidate.Slot) == roomTeamID(room.Mode, actor.Slot) {
			continue
		}
		landType, hasLand := s.domain.LandTypeAt(room, candidate.NodeID)
		if hasLand && landType == 13 {
			continue
		}
		inRange, err := s.domain.WithinLandDistance(room, actor.NodeID, candidate.NodeID, info.Range)
		if err != nil {
			return nil, err
		}
		if inRange {
			targets = append(targets, candidate.ID)
		}
	}
	return targets, nil
}

func (s *Server) effectCardHealAmount(room store.Room, playerID int64, amount int32) int32 {
	// The normal-mode passive is confirmed as a general healing modifier.
	// The PVE passive 11012 is documented as card-related, but its exact effect
	// trigger is not confirmed, so do not apply it to effect-card healing yet.
	if usesPVEPassiveSkills(room.Mode) {
		return amount
	}
	return s.applyHeroHPChangePassive(room, playerID, amount)
}

func (s *Server) movementActionPush(ctx context.Context, roomID int64, round int32, playerID int64) Push {
	bombActive, bombErr := s.store.BeginBombThrow(ctx, roomID, playerID, round)
	if bombErr != nil {
		s.log.Error("bomb turn state could not be loaded", "room_id", roomID, "player_id", playerID, "round", round, "err", bombErr)
		return Push{}
	}
	if bombActive {
		return s.actionPush(round, 5059, playerID, &protocolpb.BombThrowDiceC2S{})
	}
	done, err := s.store.EffectCardTurnDone(ctx, roomID, playerID, round)
	if err != nil {
		s.log.Error("effect card turn state could not be loaded", "room_id", roomID, "player_id", playerID, "round", round, "err", err)
		return Push{}
	}
	if done {
		return s.actionPush(round, 5021, playerID, &protocolpb.ThrowDiceC2S{})
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		s.log.Error("effect card action room could not be loaded", "room_id", roomID, "player_id", playerID, "err", err)
		return Push{}
	}
	for _, player := range room.Players {
		if player.ID == playerID {
			canUseSkill := usesPVEPassiveSkills(room.Mode) && s.activeSkillID(room, player) == skill10202ID && player.SkillCooldowns[skill10202ID] == 0
			usable, usableErr := s.usableEffectCardIDs(room, player)
			if usableErr != nil {
				s.log.Error("effect card candidates could not be evaluated", "room_id", room.ID, "player_id", playerID, "err", usableErr)
				return Push{}
			}
			return s.actionPush(round, 5055, playerID, &protocolpb.UseEffectCardC2S{
				CanUseCardIds: usable,
				NotUseSkill:   !canUseSkill,
			})
		}
	}
	s.log.Error("effect card action player is missing from room", "room_id", roomID, "player_id", playerID)
	return Push{}
}

func (s *Server) handleUseEffectCard(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.UseEffectCardC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	current, round, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	if current != playerID {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	if pending != 0 || phase != store.TurnPhaseThrowDice {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	done, err := s.store.EffectCardTurnDone(ctx, roomID, playerID, round)
	if err != nil {
		return DispatchResult{}, err
	}
	if done {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	if q.GetUseSkill() {
		return s.handleUseSkillEvent(ctx, sess, in, q, roomID, playerID, round)
	}
	if q.GetCardId() == store.EffectCardTransferBomb {
		if q.GetUseSelectCardIndex() != 0 || q.GetSkillId() != 0 || q.GetCounterPlayer() != 0 || len(q.GetTargetIds()) != 0 || len(q.GetTargetNodeIds()) != 0 || len(q.GetCardUniqueIds()) != 0 || len(q.GetLandBuffUniqueIds()) != 0 {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		return s.handleUseBombCard(ctx, sess, in, q, roomID, playerID, round)
	}
	if info, supported := s.resources.EffectCardControlMoveInfo(q.GetCardId()); supported {
		if q.GetSkillId() != 0 || q.GetCounterPlayer() != 0 || len(q.GetTargetIds()) != 0 || len(q.GetTargetNodeIds()) != 0 || len(q.GetCardUniqueIds()) != 0 || len(q.GetLandBuffUniqueIds()) != 0 {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		return s.handleControlMoveCard(ctx, in, q, roomID, playerID, round, info)
	}
	if info, supported := s.resources.EffectCardDirectDamageInfo(q.GetCardId()); supported {
		return s.handleDirectDamageEffectCard(ctx, in, q, roomID, playerID, round, info)
	}
	if q.GetSkillId() != 0 || q.GetCounterPlayer() != 0 || len(q.GetTargetIds()) != 0 || len(q.GetTargetNodeIds()) != 0 || len(q.GetCardUniqueIds()) != 0 || len(q.GetLandBuffUniqueIds()) != 0 {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if q.GetCardId() < 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	var heal int32
	var maxHP int32
	if q.GetCardId() != 0 {
		if q.GetUseSelectCardIndex() != 0 {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		room, roomErr := s.store.RoomSnapshot(ctx, roomID)
		if roomErr != nil {
			return DispatchResult{}, roomErr
		}
		player, found := findRoomPlayer(room, playerID)
		if !found {
			return DispatchResult{Err: ErrRoomNotAction}, nil
		}
		usable := false
		usableCardIDs, usableErr := s.usableEffectCardIDs(room, player)
		if usableErr != nil {
			return DispatchResult{}, usableErr
		}
		for _, cardID := range usableCardIDs {
			if cardID == q.GetCardId() {
				usable = true
				break
			}
		}
		if !usable {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
		var configured bool
		heal, configured = s.resources.EffectCardHealAmount(q.GetCardId())
		if !configured {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		maxHP = s.domain.HeroMaxHP(player.HeroID)
		heal = s.effectCardHealAmount(room, playerID, heal)
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.CompleteEffectCardUse(ctx, roomID, playerID, round, in.CmdID, in.UPSN, raw, q.GetCardId(), heal, maxHP)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	message := &protocolpb.UseEffectCardS2C{PlayerId: playerID, CardId: q.GetCardId()}
	memberIDs := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{s.pushFor("UseEffectCardS2C", message, memberIDs, playerID)}
	if q.GetCardId() != 0 {
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", effectCardAttrUpdate(playerID, q.GetCardId(), result, heal), memberIDs, 0))
		s.log.Info("effect card resolved", "room_id", roomID, "player_id", playerID, "card_id", q.GetCardId(), "hp_change", result.NewHP-result.OldHP, "round", round)
	} else {
		s.log.Info("effect card phase skipped", "room_id", roomID, "player_id", playerID, "round", round)
	}
	pushes = append(pushes, s.movementActionPush(ctx, roomID, round, playerID))
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func effectCardAttrUpdate(playerID int64, cardID int32, result store.EffectCardUseResult, amount int32) *protocolpb.UpdateHeroAttrS2C {
	cards := make([]*modelpb.CardInfo, 0, len(result.Cards))
	for _, card := range result.Cards {
		cards = append(cards, &modelpb.CardInfo{UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum, IsTemp: card.IsTemp, BattleCost: card.BattleCost})
	}
	effects := []*protocolpb.HeroAttrEffect{{
		PlayerId: playerID,
		Data:     &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{PlayerId: playerID, Cards: cards}},
	}}
	if result.NewHP != result.OldHP {
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
				PlayerId: playerID, ChangeHp: amount, OriHp: result.OldHP, CurrHp: result.NewHP,
				RealChangeHp: result.NewHP - result.OldHP, MaxHp: result.MaxHP,
			}},
		})
	}
	effects = append(effects, &protocolpb.HeroAttrEffect{
		PlayerId: playerID,
		Data: &protocolpb.HeroAttrEffect_UseCardNum{UseCardNum: &protocolpb.HeroUseCardNumChangeS2C{
			PlayerId: playerID, UseCardNum: result.UseCardNum, UseCardMaxNum: result.UseCardMaxNum,
		}},
	})
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId:    playerID,
		Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_card, Id: int64(cardID)},
		EffectDatas: effects,
	}
}

func (s *Server) resolveBotEffectCardTurn(ctx context.Context, roomID int64, round int32, room store.Room, bot store.Player, ids []int64) ([]Push, error) {
	done, err := s.store.EffectCardTurnDone(ctx, roomID, bot.ID, round)
	if err != nil || done {
		return nil, err
	}
	cardID := int32(0)
	heal := int32(0)
	var maxHP int32
	usable, err := s.usableEffectCardIDs(room, bot)
	if err != nil {
		return nil, err
	}
	maxHP = s.domain.HeroMaxHP(bot.HeroID)
	missingHP := maxHP - bot.HP
	var bestEffectiveHeal int32
	var bestConfiguredHeal int32
	for _, candidate := range usable {
		amount, ok := s.resources.EffectCardHealAmount(candidate)
		if !ok {
			continue
		}
		effectiveHeal := amount
		if effectiveHeal > missingHP {
			effectiveHeal = missingHP
		}
		if cardID == 0 || effectiveHeal > bestEffectiveHeal ||
			(effectiveHeal == bestEffectiveHeal && amount < bestConfiguredHeal) {
			cardID = candidate
			heal = amount
			bestEffectiveHeal = effectiveHeal
			bestConfiguredHeal = amount
		}
	}
	var damageTarget store.Player
	var damageInfo cfgstore.DirectDamageCardInfo
	if cardID == 0 {
		for _, candidate := range usable {
			info, ok := s.resources.EffectCardDirectDamageInfo(candidate)
			if !ok || info.Damage < damageInfo.Damage {
				continue
			}
			targetIDs, targetErr := s.effectCardDamageTargetIDs(room, bot, info)
			if targetErr != nil {
				return nil, targetErr
			}
			var candidateTarget store.Player
			for _, targetID := range targetIDs {
				target, found := findRoomPlayer(room, targetID)
				if !found {
					continue
				}
				if candidateTarget.ID == 0 || target.HP < candidateTarget.HP {
					candidateTarget = target
				}
			}
			if candidateTarget.ID == 0 {
				continue
			}
			if damageTarget.ID == 0 || info.Damage > damageInfo.Damage || (info.Damage == damageInfo.Damage && candidateTarget.HP < damageTarget.HP) {
				damageTarget = candidateTarget
				damageInfo = info
				cardID = candidate
			}
		}
	}
	if cardID > 0 && damageTarget.ID > 0 {
		request := &protocolpb.UseEffectCardC2S{CardId: cardID, TargetIds: []int64{damageTarget.ID}}
		raw, marshalErr := proto.Marshal(request)
		if marshalErr != nil {
			return nil, marshalErr
		}
		result, damage, damageErr := s.completeDirectDamageEffectCard(ctx, room, bot, damageTarget, round, 5055, s.nextActionSN(), raw, cardID, damageInfo)
		if damageErr != nil {
			return nil, damageErr
		}
		message := &protocolpb.UseEffectCardS2C{PlayerId: bot.ID, CardId: cardID, TargetIds: []int64{damageTarget.ID}}
		pushes := []Push{s.pushFor("UseEffectCardS2C", message, ids, 0)}
		pushes = append(pushes, s.directDamageEffectCardPushes(result, cardID, ids)...)
		if damage > 0 {
			pushes = append(pushes, s.taskConditionPushes(ctx, []int64{bot.ID, damageTarget.ID})...)
		}
		s.log.Info("bot direct damage effect card resolved", "room_id", roomID, "round", round, "player_id", bot.ID, "target_id", damageTarget.ID, "card_id", cardID, "damage", damage, "target_hp", result.TargetNewHP)
		return pushes, nil
	}
	if cardID == 0 {
		for _, candidate := range usable {
			if candidate == store.EffectCardTransferBomb {
				cardID = candidate
				break
			}
		}
	}
	if cardID == store.EffectCardTransferBomb {
		request := &protocolpb.UseEffectCardC2S{CardId: cardID}
		raw, marshalErr := proto.Marshal(request)
		if marshalErr != nil {
			return nil, marshalErr
		}
		result, created, useErr := s.store.CompleteBombCardUse(ctx, roomID, bot.ID, round, 5055, s.nextActionSN(), raw)
		if useErr != nil {
			return nil, useErr
		}
		if !created {
			return nil, store.ErrActionNotReady
		}
		message := &protocolpb.UseEffectCardS2C{PlayerId: bot.ID, CardId: cardID, TargetIds: []int64{result.TargetID}}
		return []Push{
			s.pushFor("UseEffectCardS2C", message, ids, 0),
			s.pushFor("UpdateHeroAttrS2C", bombCardAttrUpdate(result), ids, 0),
		}, nil
	}
	if cardID != 0 {
		heal = s.effectCardHealAmount(room, bot.ID, heal)
	}
	request := &protocolpb.UseEffectCardC2S{CardId: cardID}
	raw, err := proto.Marshal(request)
	if err != nil {
		return nil, err
	}
	result, created, err := s.store.CompleteEffectCardUse(ctx, roomID, bot.ID, round, 5055, s.nextActionSN(), raw, cardID, heal, maxHP)
	if err != nil {
		return nil, err
	}
	if !created {
		return nil, store.ErrActionNotReady
	}
	message := &protocolpb.UseEffectCardS2C{PlayerId: bot.ID, CardId: cardID}
	pushes := []Push{s.pushFor("UseEffectCardS2C", message, ids, 0)}
	if cardID != 0 {
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", effectCardAttrUpdate(bot.ID, cardID, result, heal), ids, 0))
		s.log.Info("bot effect card resolved", "room_id", roomID, "player_id", bot.ID, "card_id", cardID, "hp_change", result.NewHP-result.OldHP, "round", round)
	}
	return pushes, nil
}
