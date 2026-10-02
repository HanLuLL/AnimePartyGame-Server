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

func (s *Server) handleDirectDamageEffectCard(ctx context.Context, in wire.Frame, q *protocolpb.UseEffectCardC2S, roomID, playerID int64, round int32, info cfgstore.DirectDamageCardInfo) (DispatchResult, error) {
	if q.GetUseSelectCardIndex() != 0 || q.GetSkillId() != 0 || q.GetCounterPlayer() != 0 || len(q.GetTargetIds()) != 1 || len(q.GetTargetNodeIds()) != 0 || len(q.GetCardUniqueIds()) != 0 || len(q.GetLandBuffUniqueIds()) != 0 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	actor, found := findRoomPlayer(room, playerID)
	if !found {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	var target store.Player
	targetFound := false
	targetIDs, err := s.effectCardDamageTargetIDs(room, actor, info)
	if err != nil {
		return DispatchResult{}, err
	}
	for _, candidateID := range targetIDs {
		if candidateID != q.GetTargetIds()[0] {
			continue
		}
		target, targetFound = findRoomPlayer(room, candidateID)
		break
	}
	if !targetFound {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, damage, err := s.completeDirectDamageEffectCard(ctx, room, actor, target, round, in.CmdID, in.UPSN, raw, q.GetCardId(), info)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	ids := mustMemberIDs(ctx, s.store, roomID)
	message := &protocolpb.UseEffectCardS2C{PlayerId: playerID, CardId: q.GetCardId(), TargetIds: []int64{target.ID}}
	pushes := s.directDamageEffectCardPushes(result, q.GetCardId(), ids)
	pushes = append(pushes, s.movementActionPush(ctx, roomID, round, playerID))
	if damage > 0 {
		pushes = append(pushes, s.taskConditionPushes(ctx, []int64{actor.ID, target.ID})...)
	}
	s.log.Info("direct damage effect card resolved", "room_id", roomID, "round", round, "player_id", actor.ID, "target_id", target.ID, "card_id", q.GetCardId(), "damage", damage, "target_hp", result.TargetNewHP, "buffs_removed", len(result.RemovedBuffs))
	return DispatchResult{Message: message, Pushes: append([]Push{s.pushFor("UseEffectCardS2C", message, ids, playerID)}, pushes...)}, nil
}

func (s *Server) completeDirectDamageEffectCard(ctx context.Context, room store.Room, actor, target store.Player, round int32, cmd uint16, upsn int64, payload []byte, cardID int32, info cfgstore.DirectDamageCardInfo) (store.EffectCardDamageResult, int32, error) {
	damage := info.Damage
	targetBuffs := append(make([]store.BuffState, 0, len(target.Buffs)), target.Buffs...)
	expectedTargetBuffs := append(make([]store.BuffState, 0, len(target.Buffs)), target.Buffs...)
	damage, targetBuffs, removedBuffs := store.ApplyDestinyDamageBuffs(damage, targetBuffs)
	if len(removedBuffs) > 0 {
		s.log.Info("effect card damage buff applied", "room_id", room.ID, "player_id", target.ID, "card_id", cardID, "buff_count", len(removedBuffs), "remaining_damage", damage)
	}
	if damage > 0 {
		if params, ok := s.domain.EventParams(30020); ok && len(params) >= 2 && params[1] < 0 {
			if remaining, buff := removeBattleBuff(targetBuffs, 3002001); buff != nil {
				damage += params[1]
				if damage < 0 {
					damage = 0
				}
				targetBuffs = remaining
				removedBuffs = append(removedBuffs, *buff)
				s.log.Info("effect card event buff applied", "room_id", room.ID, "player_id", target.ID, "card_id", cardID, "remaining_damage", damage)
			}
		}
	}
	if damage > 0 && !usesPVEPassiveSkills(room.Mode) {
		damage = -s.applyHeroHPChangePassive(room, target.ID, -damage)
	}
	if damage < 0 {
		damage = 0
	}
	if damage > target.HP {
		damage = target.HP
	}
	maxHP := s.domain.HeroMaxHP(target.HeroID)
	result, created, err := s.store.CompleteDirectDamageEffectCardUse(ctx, room.ID, actor.ID, target.ID, round, cmd, upsn, payload, cardID, target.HP, damage, maxHP, expectedTargetBuffs, targetBuffs, removedBuffs)
	if err != nil {
		return store.EffectCardDamageResult{}, 0, err
	}
	if !created {
		return store.EffectCardDamageResult{}, 0, store.ErrActionNotReady
	}
	return result, damage, nil
}

func (s *Server) directDamageEffectCardPushes(result store.EffectCardDamageResult, cardID int32, recipients []int64) []Push {
	pushes := []Push{s.pushFor("UpdateHeroAttrS2C", directDamageEffectCardUserAttr(result, cardID), recipients, 0)}
	if result.TargetNewHP == result.TargetOldHP && len(result.RemovedBuffs) == 0 {
		return pushes
	}
	effects := make([]*protocolpb.HeroAttrEffect, 0, 1+len(result.RemovedBuffs))
	if result.TargetNewHP != result.TargetOldHP {
		damage := result.TargetOldHP - result.TargetNewHP
		effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: result.TargetID, Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
			PlayerId: result.TargetID, ChangeHp: -damage, OriHp: result.TargetOldHP, CurrHp: result.TargetNewHP,
			RealChangeHp: -damage, RealHp: result.TargetNewHP, MaxHp: result.TargetMaxHP,
		}}})
	}
	for _, buff := range result.RemovedBuffs {
		effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: result.TargetID, Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
			PlayerId: result.TargetID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Delete,
		}}})
	}
	pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", &protocolpb.UpdateHeroAttrS2C{
		PlayerId:    result.TargetID,
		Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_card, Id: int64(cardID)},
		EffectDatas: effects,
	}, recipients, 0))
	return pushes
}

func directDamageEffectCardUserAttr(result store.EffectCardDamageResult, cardID int32) *protocolpb.UpdateHeroAttrS2C {
	cards := make([]*modelpb.CardInfo, 0, len(result.Cards))
	for _, card := range result.Cards {
		cards = append(cards, &modelpb.CardInfo{UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum, IsTemp: card.IsTemp, BattleCost: card.BattleCost})
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: result.PlayerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_card, Id: int64(cardID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{
			{PlayerId: result.PlayerID, Data: &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{PlayerId: result.PlayerID, Cards: cards}}},
			{PlayerId: result.PlayerID, Data: &protocolpb.HeroAttrEffect_UseCardNum{UseCardNum: &protocolpb.HeroUseCardNumChangeS2C{PlayerId: result.PlayerID, UseCardNum: result.UseCardNum, UseCardMaxNum: result.UseCardMaxNum}}},
		},
	}
}
