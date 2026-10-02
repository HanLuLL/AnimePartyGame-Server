package gateway

import (
	"context"
	"crypto/rand"
	"errors"
	"math/big"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

func (s *Server) newBattleState(attacker, defender store.Player, pursuit bool) (*store.BattleState, error) {
	battleID, err := randomSessionID()
	if err != nil {
		return nil, err
	}
	attack, defense := s.resources.HeroCombatAttributes(attacker.HeroID)
	defenderAttack, defenderDefense := s.resources.HeroCombatAttributes(defender.HeroID)
	attackerRole := newBattleRole(attacker, attack, defense)
	defenderRole := newBattleRole(defender, defenderAttack, defenderDefense)
	attackerBuffAttack := hero115BattleAttackBonus(attacker.Buffs)
	defenderBuffAttack := hero115BattleAttackBonus(defender.Buffs)
	attackerRole.Atk += attackerBuffAttack
	attackerRole.MinAtk += attackerBuffAttack
	attackerRole.MaxAtk += attackerBuffAttack
	defenderRole.Atk += defenderBuffAttack
	defenderRole.MinAtk += defenderBuffAttack
	defenderRole.MaxAtk += defenderBuffAttack
	return &store.BattleState{
		BattleID:     battleID,
		Attacker:     attackerRole,
		Defender:     defenderRole,
		CardUseState: map[int64]bool{attacker.ID: false, defender.ID: false},
		Stage:        store.TurnPhaseBattleChallenge,
		IsPursuit:    pursuit,
	}, nil
}

func newBattleRole(player store.Player, attack, defense int32) store.BattleRoleState {
	return store.BattleRoleState{
		PlayerID: player.ID, HeroID: player.HeroID, Atk: attack, Def: defense,
		Cost: 3, MaxCost: 3, MinAtk: attack, MaxAtk: attack,
		MinDef: defense, MaxDef: defense, InitAtk: attack, InitDef: defense,
	}
}

func battleRoleFor(state *store.BattleState, playerID int64) *store.BattleRoleState {
	if state == nil {
		return nil
	}
	if state.Attacker.PlayerID == playerID {
		return &state.Attacker
	}
	if state.Defender.PlayerID == playerID {
		return &state.Defender
	}
	return nil
}

func removeBattleBuff(buffs []store.BuffState, buffID int32) ([]store.BuffState, *store.BuffState) {
	remaining := make([]store.BuffState, 0, len(buffs))
	var removed *store.BuffState
	for _, buff := range buffs {
		if removed == nil && buff.BuffID == buffID {
			copy := buff
			removed = &copy
			continue
		}
		remaining = append(remaining, buff)
	}
	return remaining, removed
}

func (s *Server) battleBuffDeletePush(playerID, battleID int64, buff store.BuffState, recipients []int64) Push {
	return s.battleBuffChangePush(playerID, battleID, buff, protocolpb.HeroBuffChangeS2C_Delete, recipients)
}

func (s *Server) loadBattle(ctx context.Context, playerID int64) (store.Room, int32, string, error) {
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return store.Room{}, 0, "", store.ErrActionNotReady
	}
	_, round, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return store.Room{}, 0, "", err
	}
	if pending != 0 {
		return store.Room{}, 0, "", store.ErrActionNotReady
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil {
		return store.Room{}, 0, "", err
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return store.Room{}, 0, "", err
	}
	if room.Battle == nil || room.Battle.Stage != phase {
		return store.Room{}, 0, "", store.ErrActionNotReady
	}
	return room, round, phase, nil
}

func (s *Server) battleActionPrompts(room store.Room, round int32, state *store.BattleState) []Push {
	if state == nil {
		return nil
	}
	switch state.Stage {
	case store.TurnPhaseBattleChallenge:
		if player, found := findRoomPlayer(room, state.Attacker.PlayerID); found && !player.IsBot {
			return []Push{s.actionPush(round, 5047, state.Attacker.PlayerID, &protocolpb.AskBattleC2S{AskPlayerId: state.Defender.PlayerID, IsPursuit: state.IsPursuit})}
		}
	case store.TurnPhaseBattleCards:
		var pushes []Push
		for _, playerID := range []int64{state.Attacker.PlayerID, state.Defender.PlayerID} {
			player, found := findRoomPlayer(room, playerID)
			if found && !player.IsBot && !state.CardUseState[playerID] {
				pushes = append(pushes, s.actionPush(round, 5035, playerID, &protocolpb.BattleUseCardC2S{}))
			}
		}
		return pushes
	case store.TurnPhaseBattleAttack:
		return []Push{s.actionPush(round, 5037, state.Attacker.PlayerID, &protocolpb.BattleThrowDiceC2S{})}
	case store.TurnPhaseBattleDefense:
		return []Push{s.actionPush(round, 5039, state.Defender.PlayerID, &protocolpb.BattleChoiceC2S{})}
	default:
		return nil
	}
	return nil
}

func (s *Server) resumeBattleForPlayer(room store.Room, round int32, playerID int64) []Push {
	state := room.Battle
	if state == nil || battleRoleFor(state, playerID) == nil {
		return nil
	}
	switch state.Stage {
	case store.TurnPhaseBattleChallenge:
		if playerID == state.Attacker.PlayerID {
			return []Push{s.actionPush(round, 5047, playerID, &protocolpb.AskBattleC2S{AskPlayerId: state.Defender.PlayerID, IsPursuit: state.IsPursuit})}
		}
	case store.TurnPhaseBattleCards:
		if !state.CardUseState[playerID] {
			return []Push{s.actionPush(round, 5035, playerID, &protocolpb.BattleUseCardC2S{})}
		}
	case store.TurnPhaseBattleAttack:
		if playerID == state.Attacker.PlayerID {
			return []Push{s.actionPush(round, 5037, playerID, &protocolpb.BattleThrowDiceC2S{})}
		}
	case store.TurnPhaseBattleDefense:
		if playerID == state.Defender.PlayerID {
			return []Push{s.actionPush(round, 5039, playerID, &protocolpb.BattleChoiceC2S{})}
		}
	}
	return nil
}

func requestMayAdvanceBattle(message proto.Message) bool {
	switch message.(type) {
	case *protocolpb.MoveC2S, *protocolpb.AskBattleC2S, *protocolpb.BattleUseCardC2S, *protocolpb.BattleThrowDiceC2S, *protocolpb.BattleChoiceC2S:
		return true
	default:
		return false
	}
}

func (s *Server) handleAskBattle(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.AskBattleC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	s.battleMu.Lock()
	locked := true
	defer func() {
		if locked {
			s.battleMu.Unlock()
		}
	}()
	room, round, phase, err := s.loadBattle(ctx, playerID)
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	state := room.Battle
	if phase != store.TurnPhaseBattleChallenge || playerID != state.Attacker.PlayerID || (q.AskPlayerId != 0 && q.AskPlayerId != state.Defender.PlayerID) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if q.IsBattle {
		state.Stage = store.TurnPhaseBattleCards
		if state.CardUseState == nil {
			state.CardUseState = map[int64]bool{}
		}
		state.CardUseState[state.Attacker.PlayerID] = false
		state.CardUseState[state.Defender.PlayerID] = false
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.ApplyBattleAction(ctx, room.ID, playerID, in.CmdID, in.UPSN, raw, phase, state, nil, nil, !q.IsBattle, nil)
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	response := &protocolpb.AskBattleS2C{PlayerId: state.Attacker.PlayerID, AskPlayerId: state.Defender.PlayerID, IsBattle: q.IsBattle}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	pushes := []Push{s.pushFor("AskBattleS2C", response, ids, playerID)}
	if q.IsBattle {
		pushes = append(pushes, s.pushFor("BattleS2C", &protocolpb.BattleS2C{Battle: battleMessage(state)}, ids, 0))
		pushes = append(pushes, s.battleActionPrompts(room, round, state)...)
		s.log.Info("battle accepted", "room_id", room.ID, "battle_id", state.BattleID, "attacker_id", state.Attacker.PlayerID, "defender_id", state.Defender.PlayerID)
	} else {
		s.battleMu.Unlock()
		locked = false
		pushes = append(pushes, s.turnPushes(ctx, room.ID, result.Round, result.NextPlayer)...)
		s.log.Info("player battle declined", "room_id", room.ID, "attacker_id", state.Attacker.PlayerID, "defender_id", state.Defender.PlayerID)
	}
	return DispatchResult{Message: response, Pushes: pushes}, nil
}

func (s *Server) handleBattleUseCard(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.BattleUseCardC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	s.battleMu.Lock()
	defer s.battleMu.Unlock()
	room, round, phase, err := s.loadBattle(ctx, playerID)
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	state := room.Battle
	role := battleRoleFor(state, playerID)
	if phase != store.TurnPhaseBattleCards || role == nil || state.CardUseState[playerID] {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	player, found := findRoomPlayer(room, playerID)
	if !found {
		return DispatchResult{Err: ErrRoomPlayerNotExist}, nil
	}
	var cardUpdate *store.BattleCardsUpdate
	cardID := int32(0)
	noCard := q.CardUid == 0
	if !noCard {
		cardIndex := -1
		for index, card := range player.Cards {
			if card.UniqueID == q.CardUid {
				cardIndex = index
				break
			}
		}
		if cardIndex < 0 {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
		card := player.Cards[cardIndex]
		config, configured := s.resources.BattleCardInfo(card.CardID)
		if !configured || (playerID == state.Attacker.PlayerID && config.EffectType != 1) || (playerID == state.Defender.PlayerID && config.EffectType != 2) {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
		cost := config.Cost
		if card.BattleCost >= 0 {
			cost = card.BattleCost
		}
		if cost < 0 || role.Cost < cost {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
		bonus, err := randomBattleBonus(config.MinBonus, config.MaxBonus)
		if err != nil {
			return DispatchResult{}, err
		}
		if config.EffectType == 1 {
			role.MinAtk += config.MinBonus
			role.MaxAtk += config.MaxBonus
			role.Atk += bonus
		} else {
			role.MinDef += config.MinBonus
			role.MaxDef += config.MaxBonus
			role.Def += bonus
		}
		role.Cost -= cost
		role.UseCards = append(role.UseCards, card.UniqueID)
		role.CardCombatBonus = append(role.CardCombatBonus, store.BattleCardBonus{CardID: card.CardID, Bonus: bonus})
		cardID = card.CardID
		player.Cards = append(player.Cards[:cardIndex], player.Cards[cardIndex+1:]...)
		cardUpdate = &store.BattleCardsUpdate{PlayerID: playerID, Cards: player.Cards}
		effectType := int32(1)
		if playerID == state.Defender.PlayerID {
			effectType = 2
		}
		if role.Cost <= 0 || !s.hasUsableBattleCard(*role, player.Cards, effectType) {
			state.CardUseState[playerID] = true
		}
	} else {
		state.CardUseState[playerID] = true
	}
	if state.CardUseState[state.Attacker.PlayerID] && state.CardUseState[state.Defender.PlayerID] {
		state.Stage = store.TurnPhaseBattleAttack
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	_, created, err := s.store.ApplyBattleAction(ctx, room.ID, playerID, in.CmdID, in.UPSN, raw, phase, state, cardUpdate, nil, false, nil)
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	response := &protocolpb.BattleUseCardS2C{PlayerId: playerID, CardId: cardID, NoCard: noCard}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	pushes := []Push{s.pushFor("BattleUseCardS2C", response, ids, playerID)}
	if cardUpdate != nil {
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", battleCardsAttrUpdate(playerID, cardUpdate.Cards, cardID), ids, 0))
	}
	pushes = append(pushes, s.pushFor("BattleS2C", &protocolpb.BattleS2C{Battle: battleMessage(state)}, ids, 0))
	if state.Stage == store.TurnPhaseBattleCards && !state.CardUseState[playerID] {
		pushes = append(pushes, s.actionPush(round, 5035, playerID, &protocolpb.BattleUseCardC2S{}))
	} else if state.Stage == store.TurnPhaseBattleAttack {
		pushes = append(pushes, s.actionPush(round, 5037, state.Attacker.PlayerID, &protocolpb.BattleThrowDiceC2S{}))
	}
	if cardUpdate != nil {
		s.log.Info("battle card applied", "room_id", room.ID, "battle_id", state.BattleID, "player_id", playerID, "card_id", cardID, "remaining_cost", role.Cost)
	}
	return DispatchResult{Message: response, Pushes: pushes}, nil
}

func (s *Server) hasUsableBattleCard(role store.BattleRoleState, cards []store.CardState, effectType int32) bool {
	for _, card := range cards {
		config, ok := s.resources.BattleCardInfo(card.CardID)
		if !ok || (effectType != 0 && config.EffectType != effectType) {
			continue
		}
		cost := config.Cost
		if card.BattleCost >= 0 {
			cost = card.BattleCost
		}
		if role.Cost >= cost && cost >= 0 {
			return true
		}
	}
	return false
}

func battleCardsAttrUpdate(playerID int64, cards []store.CardState, sourceCardID int32) *protocolpb.UpdateHeroAttrS2C {
	cardMessages := make([]*modelpb.CardInfo, 0, len(cards))
	for _, card := range cards {
		cardMessages = append(cardMessages, &modelpb.CardInfo{UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum, IsTemp: card.IsTemp, BattleCost: card.BattleCost})
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId:    playerID,
		Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_battle, Id: int64(sourceCardID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{{PlayerId: playerID, Data: &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{PlayerId: playerID, Cards: cardMessages}}}},
	}
}

func randomBattleBonus(minimum, maximum int32) (int32, error) {
	if minimum < 0 || maximum < minimum {
		return 0, errors.New("invalid battle card bonus range")
	}
	span := int64(maximum) - int64(minimum) + 1
	n, err := rand.Int(rand.Reader, big.NewInt(span))
	if err != nil {
		return 0, err
	}
	return minimum + int32(n.Int64()), nil
}

func (s *Server) handleBattleThrowDice(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.BattleThrowDiceC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	s.battleMu.Lock()
	defer s.battleMu.Unlock()
	room, round, phase, err := s.loadBattle(ctx, playerID)
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	state := room.Battle
	if phase != store.TurnPhaseBattleAttack || playerID != state.Attacker.PlayerID {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	attacker, attackerFound := findRoomPlayer(room, state.Attacker.PlayerID)
	defender, defenderFound := findRoomPlayer(room, state.Defender.PlayerID)
	if !attackerFound || !defenderFound {
		return DispatchResult{Err: ErrRoomPlayerNotExist}, nil
	}
	var attackBonus int32
	var consumedBuff *store.BuffState
	var buffUpdate *store.BattleBuffUpdate
	if params, ok := s.domain.EventParams(30202); ok && len(params) > 0 && params[0] > 0 {
		if remaining, buff := removeBattleBuff(attacker.Buffs, 3020201); buff != nil {
			attackBonus += params[0]
			consumedBuff = buff
			buffUpdate = &store.BattleBuffUpdate{PlayerID: attacker.ID, Buffs: remaining}
			s.log.Info("battle event buff applied", "room_id", room.ID, "battle_id", state.BattleID, "player_id", attacker.ID, "buff_id", buff.BuffID, "attack_bonus", params[0])
		}
	}
	if params := s.resources.HeroPassiveSkillParams(attacker.HeroID, 11611, usesPVEPassiveSkills(room.Mode)); len(params) >= 2 && params[0] > 0 && params[1] > 0 {
		handDifference := int32(len(attacker.Cards) - len(defender.Cards))
		if handDifference > 0 {
			if handDifference > params[1] {
				handDifference = params[1]
			}
			bonus := handDifference * params[0]
			attackBonus += bonus
			s.log.Info("battle passive applied", "room_id", room.ID, "battle_id", state.BattleID, "player_id", attacker.ID, "skill_id", 11611, "hand_difference", handDifference, "attack_bonus", bonus)
		}
	}
	if params := s.resources.HeroPassiveSkillParams(attacker.HeroID, 10611, usesPVEPassiveSkills(room.Mode)); len(params) >= 2 && params[0] > 0 && params[1] > 0 {
		maxHP := s.resources.HeroMaxHP(attacker.HeroID)
		if maxHP > 0 && int64(attacker.HP)*100 <= int64(maxHP)*int64(params[0]) {
			attackBonus += params[1]
			s.log.Info("battle passive applied", "room_id", room.ID, "battle_id", state.BattleID, "player_id", attacker.ID, "skill_id", 10611, "hp", attacker.HP, "max_hp", maxHP, "attack_bonus", params[1])
		}
	}
	state.Attacker.Atk += attackBonus
	state.Attacker.MinAtk += attackBonus
	state.Attacker.MaxAtk += attackBonus
	point, err := randomGamblePoint()
	if err != nil {
		return DispatchResult{}, err
	}
	state.Attacker.Point = point
	state.Stage = store.TurnPhaseBattleDefense
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	var buffUpdates []store.BattleBuffUpdate
	if buffUpdate != nil {
		buffUpdates = append(buffUpdates, *buffUpdate)
	}
	_, created, err := s.store.ApplyBattleAction(ctx, room.ID, playerID, in.CmdID, in.UPSN, raw, phase, state, nil, nil, false, nil, buffUpdates...)
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	response := &protocolpb.BattleThrowDiceS2C{PlayerId: playerID, Val: point}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	pushes := []Push{s.pushFor("BattleThrowDiceS2C", response, ids, playerID), s.pushFor("BattleS2C", &protocolpb.BattleS2C{Battle: battleMessage(state)}, ids, 0)}
	if consumedBuff != nil {
		pushes = append(pushes, s.battleBuffDeletePush(playerID, state.BattleID, *consumedBuff, ids))
	}
	pushes = append(pushes, s.actionPush(round, 5039, state.Defender.PlayerID, &protocolpb.BattleChoiceC2S{}))
	s.log.Info("battle attack die rolled", "room_id", room.ID, "battle_id", state.BattleID, "player_id", playerID, "point", point)
	return DispatchResult{Message: response, Pushes: pushes}, nil
}

func (s *Server) handleBattleChoice(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.BattleChoiceC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	s.battleMu.Lock()
	locked := true
	defer func() {
		if locked {
			s.battleMu.Unlock()
		}
	}()
	room, _, phase, err := s.loadBattle(ctx, playerID)
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	state := room.Battle
	if phase != store.TurnPhaseBattleDefense || playerID != state.Defender.PlayerID || state.Attacker.Point < 1 || state.Attacker.Point > 6 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	defender, found := findRoomPlayer(room, playerID)
	if !found {
		return DispatchResult{Err: ErrRoomPlayerNotExist}, nil
	}
	attacker, found := findRoomPlayer(room, state.Attacker.PlayerID)
	if !found {
		return DispatchResult{Err: ErrRoomPlayerNotExist}, nil
	}
	defenderPoint, err := randomGamblePoint()
	if err != nil {
		return DispatchResult{}, err
	}
	dodge := q.Dodge && !q.NoDodge
	attackTotal := state.Attacker.Atk + state.Attacker.Point
	damage := int32(0)
	if dodge {
		if state.Attacker.Point >= defenderPoint && defenderPoint != 6 {
			damage = attackTotal
		}
		state.Defender.Def = 0
	} else {
		defenseTotal := state.Defender.Def + defenderPoint
		damage = attackTotal - defenseTotal
		if damage < 1 {
			damage = 1
		}
		state.Defender.Def = defenseTotal
	}
	var consumedBuff *store.BuffState
	var consumedBuffs []store.BuffState
	attackerBuffs := append([]store.BuffState(nil), attacker.Buffs...)
	defenderBuffs := append([]store.BuffState(nil), defender.Buffs...)
	attackerBuffChanged, defenderBuffChanged := false, false
	var passiveBuffChanges []battleBuffChange
	if damage > 0 {
		baseDamage := damage
		var removed []store.BuffState
		damage, defenderBuffs, removed = store.ApplyDestinyDamageBuffs(damage, defenderBuffs)
		if len(removed) > 0 {
			consumedBuffs = append(consumedBuffs, removed...)
			defenderBuffChanged = true
			s.log.Info("battle Destiny damage buff applied", "room_id", room.ID, "battle_id", state.BattleID, "player_id", defender.ID, "incoming_damage", baseDamage, "remaining_damage", damage, "buff_count", len(removed))
		}
	}
	if !dodge && damage > 0 {
		baseDamage := damage
		if params, ok := s.domain.EventParams(30020); ok && len(params) >= 2 && params[1] < 0 {
			if remaining, buff := removeBattleBuff(defenderBuffs, 3002001); buff != nil {
				damage += params[1]
				if damage < 0 {
					damage = 0
				}
				consumedBuff = buff
				defenderBuffs = remaining
				defenderBuffChanged = true
				s.log.Info("battle event buff applied", "room_id", room.ID, "battle_id", state.BattleID, "player_id", defender.ID, "buff_id", buff.BuffID, "incoming_damage", baseDamage, "remaining_damage", damage)
			}
		}
	}
	if damage > 0 {
		damage = -s.applyHeroHPChangePassive(room, defender.ID, -damage)
	}
	if damage < 0 {
		damage = 0
	}
	if damage > defender.HP {
		damage = defender.HP
	}
	newHP := defender.HP - damage
	attackHit := !dodge || (state.Attacker.Point >= defenderPoint && defenderPoint != 6)
	dodgeSucceeded := dodge && !attackHit
	if attackHit {
		updatedBuffs, change, gainErr := s.gainHero115BattleStack(attacker, room.Mode, attackerBuffs)
		if gainErr != nil {
			return DispatchResult{}, gainErr
		}
		attackerBuffs = updatedBuffs
		if change != nil {
			attackerBuffChanged = true
			passiveBuffChanges = append(passiveBuffChanges, *change)
			s.log.Info("battle passive stack gained", "room_id", room.ID, "battle_id", state.BattleID, "player_id", attacker.ID, "skill_id", hero11511SkillID, "buff_id", change.buff.BuffID, "progress", change.buff.Progress)
		}
	}
	if dodgeSucceeded {
		updatedBuffs, change, gainErr := s.gainHero115BattleStack(defender, room.Mode, defenderBuffs)
		if gainErr != nil {
			return DispatchResult{}, gainErr
		}
		defenderBuffs = updatedBuffs
		if change != nil {
			defenderBuffChanged = true
			passiveBuffChanges = append(passiveBuffChanges, *change)
			s.log.Info("battle passive stack gained", "room_id", room.ID, "battle_id", state.BattleID, "player_id", defender.ID, "skill_id", hero11511SkillID, "buff_id", change.buff.BuffID, "progress", change.buff.Progress)
		}
	}
	var deathClearedBuffs []store.BuffState
	if newHP == 0 {
		defenderBuffs, deathClearedBuffs = removeAllBattleBuffs(defenderBuffs, hero11511BuffID)
		if len(deathClearedBuffs) > 0 {
			defenderBuffChanged = true
		}
	}
	state.Attacker.Atk = attackTotal
	state.Defender.Point = defenderPoint
	state.Defender.Dodge = dodge
	state.Defender.IncHP = -damage
	state.IsEnd = true
	state.FightBack = false
	hpUpdates := []store.BattleHPUpdate(nil)
	if damage > 0 {
		hpUpdates = append(hpUpdates, store.BattleHPUpdate{PlayerID: playerID, NewHP: newHP})
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	var buffUpdates []store.BattleBuffUpdate
	if attackerBuffChanged {
		buffUpdates = append(buffUpdates, store.BattleBuffUpdate{PlayerID: attacker.ID, Buffs: attackerBuffs})
	}
	if defenderBuffChanged {
		buffUpdates = append(buffUpdates, store.BattleBuffUpdate{PlayerID: defender.ID, Buffs: defenderBuffs})
	}
	var stats []store.BattleStatsDelta
	if damage > 0 {
		attackerStats := store.BattleStatsDelta{PlayerID: attacker.ID, TotalDamage: damage}
		defenderStats := store.BattleStatsDelta{PlayerID: defender.ID, TotalInjured: damage}
		if newHP == 0 {
			attackerStats.KillCount = 1
			defenderStats.TotalDie = 1
		}
		stats = append(stats, attackerStats, defenderStats)
	}
	result, created, err := s.store.ApplyBattleAction(ctx, room.ID, playerID, in.CmdID, in.UPSN, raw, phase, state, nil, hpUpdates, true, stats, buffUpdates...)
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	response := &protocolpb.BattleChoiceS2C{PlayerId: playerID, Val: defenderPoint, Dodge: dodge, ExistFightBack: false}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	pushes := []Push{s.pushFor("BattleChoiceS2C", response, ids, playerID), s.pushFor("BattleS2C", &protocolpb.BattleS2C{Battle: battleMessage(state)}, ids, 0)}
	if damage > 0 {
		maxHP := s.resources.HeroMaxHP(defender.HeroID)
		hpUpdate := &protocolpb.UpdateHeroAttrS2C{
			PlayerId: playerID,
			Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_battle, Id: state.BattleID},
			EffectDatas: []*protocolpb.HeroAttrEffect{{PlayerId: playerID, Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
				PlayerId: playerID, ChangeHp: -damage, OriHp: defender.HP, CurrHp: newHP,
				RealChangeHp: -damage, RealHp: newHP, MaxHp: maxHP,
			}}}},
		}
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", hpUpdate, ids, 0))
	}
	if consumedBuff != nil {
		consumedBuffs = append(consumedBuffs, *consumedBuff)
	}
	for _, buff := range consumedBuffs {
		pushes = append(pushes, s.battleBuffDeletePush(playerID, state.BattleID, buff, ids))
	}
	for _, change := range passiveBuffChanges {
		pushes = append(pushes, s.battleBuffChangePush(change.playerID, state.BattleID, change.buff, change.op, ids))
	}
	for _, buff := range deathClearedBuffs {
		pushes = append(pushes, s.battleBuffChangePush(defender.ID, state.BattleID, buff, protocolpb.HeroBuffChangeS2C_Delete, ids))
	}
	taskPlayers := make([]int64, 0, len(stats))
	for _, delta := range stats {
		if delta.KillCount > 0 || delta.TotalDamage > 0 || delta.TotalDie > 0 || delta.TotalInjured > 0 {
			taskPlayers = append(taskPlayers, delta.PlayerID)
		}
	}
	s.battleMu.Unlock()
	locked = false
	pushes = append(pushes, s.taskConditionPushes(ctx, taskPlayers)...)
	pushes = append(pushes, s.turnPushes(ctx, room.ID, result.Round, result.NextPlayer)...)
	s.log.Info("battle resolved", "room_id", room.ID, "battle_id", state.BattleID, "attacker_id", state.Attacker.PlayerID, "defender_id", playerID, "attack_point", state.Attacker.Point, "defense_point", defenderPoint, "dodged", dodge, "dodge_succeeded", dodgeSucceeded, "attack_hit", attackHit, "damage", damage, "defender_hp", newHP)
	return DispatchResult{Message: response, Pushes: pushes}, nil
}
