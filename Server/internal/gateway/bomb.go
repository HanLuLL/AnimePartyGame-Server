package gateway

import (
	"context"
	"errors"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

func bombModels(bombs []store.BombState) []*modelpb.Bomb {
	result := make([]*modelpb.Bomb, 0, len(bombs))
	for _, bomb := range bombs {
		result = append(result, &modelpb.Bomb{
			OnwerPlayerId: bomb.OwnerPlayerID,
			PlayerId:      bomb.PlayerID,
			IsOpen:        bomb.IsOpen,
			CardId:        bomb.CardID,
		})
	}
	return result
}

func bombAttrEffect(playerID int64, bombs []store.BombState) *protocolpb.HeroAttrEffect {
	return &protocolpb.HeroAttrEffect{
		PlayerId: playerID,
		Data: &protocolpb.HeroAttrEffect_Bomb{Bomb: &protocolpb.HeroBombChangeS2C{
			PlayerId: playerID,
			Bombs:    bombModels(bombs),
		}},
	}
}

func bombCardAttrUpdate(result store.BombCardUseResult) *protocolpb.UpdateHeroAttrS2C {
	cards := make([]*modelpb.CardInfo, 0, len(result.Cards))
	for _, card := range result.Cards {
		cards = append(cards, &modelpb.CardInfo{
			UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum,
			IsTemp: card.IsTemp, BattleCost: card.BattleCost,
		})
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: result.PlayerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_card, Id: int64(store.EffectCardTransferBomb)},
		EffectDatas: []*protocolpb.HeroAttrEffect{
			{PlayerId: result.PlayerID, Data: &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{PlayerId: result.PlayerID, Cards: cards}}},
			bombAttrEffect(result.TargetID, result.Bombs),
			{PlayerId: result.PlayerID, Data: &protocolpb.HeroAttrEffect_UseCardNum{UseCardNum: &protocolpb.HeroUseCardNumChangeS2C{
				PlayerId: result.PlayerID, UseCardNum: result.UseCardNum, UseCardMaxNum: result.UseCardMaxNum,
			}}},
		},
	}
}

func bombThrowAttrUpdate(result store.BombThrowResult, point int32) *protocolpb.UpdateHeroAttrS2C {
	effects := []*protocolpb.HeroAttrEffect{bombAttrEffect(result.PlayerID, result.PlayerBombs)}
	if result.TargetPlayerID != 0 {
		effects = append(effects, bombAttrEffect(result.TargetPlayerID, result.TargetBombs))
	}
	if result.NewHP != result.OldHP {
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: result.PlayerID,
			Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
				PlayerId: result.PlayerID, ChangeHp: result.NewHP - result.OldHP, OriHp: result.OldHP,
				CurrHp: result.NewHP, RealChangeHp: result.NewHP - result.OldHP, MaxHp: result.MaxHP,
			}},
		})
	}
	for _, buff := range result.RemovedBuffs {
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: result.PlayerID,
			Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
				PlayerId: result.PlayerID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Delete,
			}},
		})
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId:    result.PlayerID,
		Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_bomb_die, Id: int64(point)},
		EffectDatas: effects,
	}
}

func (s *Server) handleUseBombCard(ctx context.Context, _ *Session, in wire.Frame, q *protocolpb.UseEffectCardC2S, roomID, playerID int64, round int32) (DispatchResult, error) {
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.CompleteBombCardUse(ctx, roomID, playerID, round, in.CmdID, in.UPSN, raw)
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
	message := &protocolpb.UseEffectCardS2C{
		PlayerId:  playerID,
		CardId:    store.EffectCardTransferBomb,
		TargetIds: []int64{result.TargetID},
	}
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{
		s.pushFor("UseEffectCardS2C", message, ids, playerID),
		s.pushFor("UpdateHeroAttrS2C", bombCardAttrUpdate(result), ids, 0),
		s.movementActionPush(ctx, roomID, round, playerID),
	}
	s.log.Info("bomb card placed", "room_id", roomID, "player_id", playerID, "target_player_id", result.TargetID, "round", round)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func (s *Server) handleBombThrowDice(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.BombThrowDiceC2S) (DispatchResult, error) {
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
	if pending != 0 || phase != store.TurnPhaseBombThrow {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	player, found := findRoomPlayer(room, playerID)
	if !found {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	_, point, err := randomMovementDice(1)
	if err != nil {
		return DispatchResult{}, err
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.CompleteBombThrow(ctx, roomID, playerID, round, point, s.domain.HeroMaxHP(player.HeroID), in.CmdID, in.UPSN, raw)
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
	message := &protocolpb.BombThrowDiceS2C{PlayerId: playerID, Point: point, IsNext: result.Passed}
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{
		s.pushFor("BombThrowDiceS2C", message, ids, playerID),
		s.pushFor("UpdateHeroAttrS2C", bombThrowAttrUpdate(result, point), ids, 0),
	}
	s.log.Info("bomb throw resolved", "room_id", roomID, "player_id", playerID, "point", point, "passed", result.Passed, "target_player_id", result.TargetPlayerID, "hp", result.OldHP, "->", result.NewHP, "remaining", len(result.PlayerBombs), "round", round)
	if result.More {
		pushes = append(pushes, s.actionPush(round, 5059, playerID, &protocolpb.BombThrowDiceC2S{}))
	} else if result.NewHP <= 0 {
		updatedRoom, snapshotErr := s.store.RoomSnapshot(ctx, roomID)
		if snapshotErr != nil {
			return DispatchResult{}, snapshotErr
		}
		updatedPlayer, exists := findRoomPlayer(updatedRoom, playerID)
		if !exists {
			return DispatchResult{Err: ErrRoomNotAction}, nil
		}
		skipped, nextPlayer, nextRound, deadPushes, skipErr := s.skipDeadPlayerAction(ctx, roomID, updatedPlayer, round, 0, store.TurnPhaseThrowDice, ids)
		if skipErr != nil {
			return DispatchResult{}, skipErr
		}
		if skipped {
			pushes = append(pushes, deadPushes...)
			pushes = append(pushes, s.turnPushes(ctx, roomID, nextRound, nextPlayer)...)
		}
	} else {
		pushes = append(pushes, s.movementActionPush(ctx, roomID, round, playerID))
	}
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func (s *Server) resolveBotBombTurns(ctx context.Context, roomID int64, round int32, bot store.Player, ids []int64) ([]Push, error) {
	var pushes []Push
	for count := 0; count < 32; count++ {
		active, err := s.store.BeginBombThrow(ctx, roomID, bot.ID, round)
		if err != nil {
			return pushes, err
		}
		if !active {
			return pushes, nil
		}
		_, point, err := randomMovementDice(1)
		if err != nil {
			return pushes, err
		}
		seq := s.nextActionSN()
		request := &protocolpb.BombThrowDiceC2S{Info: &modelpb.ActionInfo{Sn: seq}}
		raw, err := proto.Marshal(request)
		if err != nil {
			return pushes, err
		}
		result, created, err := s.store.CompleteBombThrow(ctx, roomID, bot.ID, round, point, s.domain.HeroMaxHP(bot.HeroID), 5059, seq, raw)
		if err != nil {
			return pushes, err
		}
		if !created {
			return pushes, store.ErrActionNotReady
		}
		message := &protocolpb.BombThrowDiceS2C{PlayerId: bot.ID, Point: point, IsNext: result.Passed}
		pushes = append(pushes,
			s.pushFor("BombThrowDiceS2C", message, ids, 0),
			s.pushFor("UpdateHeroAttrS2C", bombThrowAttrUpdate(result, point), ids, 0),
		)
		s.log.Info("bot bomb throw resolved", "room_id", roomID, "player_id", bot.ID, "point", point, "passed", result.Passed, "target_player_id", result.TargetPlayerID, "hp", result.OldHP, "->", result.NewHP, "remaining", len(result.PlayerBombs), "round", round)
		if !result.More {
			return pushes, nil
		}
	}
	return pushes, errors.New("bot bomb resolution exceeded the per-turn limit")
}
