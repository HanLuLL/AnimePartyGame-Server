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

func (s *Server) handleControlMoveCard(ctx context.Context, in wire.Frame, q *protocolpb.UseEffectCardC2S, roomID, playerID int64, round int32, info cfgstore.ControlMoveCardInfo) (DispatchResult, error) {
	if q.GetUseSelectCardIndex() != 0 {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	var skillID int32
	if info.SkillCooldownDelta < 0 {
		room, err := s.store.RoomSnapshot(ctx, roomID)
		if err != nil {
			return DispatchResult{}, err
		}
		if player, found := findRoomPlayer(room, playerID); found {
			skillID = s.activeSkillID(room, player)
		}
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	actionSN := s.nextActionSN()
	result, created, err := s.store.BeginControlMoveCardUse(ctx, roomID, playerID, round, in.CmdID, in.UPSN, raw, q.GetCardId(), info.MaxPoint, actionSN, skillID, info.SkillCooldownDelta, info.UseCardLimitDelta)
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
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{
		s.pushFor("UseEffectCardS2C", message, ids, playerID),
		s.pushFor("UpdateHeroAttrS2C", controlMoveCardAttrUpdate(result), ids, 0),
		s.actionPushWithSN(round, 5067, playerID, &protocolpb.ThrowDiceResultC2S{MaxPoint: result.MaxPoint}, result.ActionSN),
	}
	s.log.Info("controlled movement card opened point selection", "room_id", roomID, "player_id", playerID, "card_id", result.CardID, "max_point", result.MaxPoint, "action_sn", result.ActionSN, "round", round)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func controlMoveCardAttrUpdate(result store.ControlMoveCardResult) *protocolpb.UpdateHeroAttrS2C {
	cards := make([]*modelpb.CardInfo, 0, len(result.Cards))
	for _, card := range result.Cards {
		cards = append(cards, &modelpb.CardInfo{UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum, IsTemp: card.IsTemp, BattleCost: card.BattleCost})
	}
	effects := []*protocolpb.HeroAttrEffect{{
		PlayerId: result.PlayerID,
		Data:     &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{PlayerId: result.PlayerID, Cards: cards}},
	}}
	if result.SkillID > 0 {
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: result.PlayerID,
			Data:     &protocolpb.HeroAttrEffect_Cd{Cd: &protocolpb.HeroCdChangeS2C{PlayerId: result.PlayerID, Cd: result.SkillCooldown}},
		})
	}
	if result.UseCardNum > 0 {
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: result.PlayerID,
			Data: &protocolpb.HeroAttrEffect_UseCardNum{UseCardNum: &protocolpb.HeroUseCardNumChangeS2C{
				PlayerId: result.PlayerID, UseCardNum: result.UseCardNum, UseCardMaxNum: result.UseCardMaxNum,
			}},
		})
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId:    result.PlayerID,
		Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_card, Id: int64(result.CardID)},
		EffectDatas: effects,
	}
}

func (s *Server) handleThrowDiceResult(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.ThrowDiceResultC2S) (DispatchResult, error) {
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
	if pending != 0 || q.GetInfo() == nil {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil || phase != store.TurnPhaseControlMove {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	prompt, exists, err := s.store.ControlMovePrompt(ctx, roomID, playerID, round)
	if err != nil {
		return DispatchResult{}, err
	}
	if !exists || q.GetInfo().GetSn() != prompt.ActionSN || q.GetPoint() < 1 || q.GetPoint() > prompt.MaxPoint {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	completed, created, err := s.store.CompleteControlMoveSelection(ctx, roomID, playerID, round, prompt.ActionSN, q.GetPoint(), in.CmdID, in.UPSN, raw)
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
	message := &protocolpb.ThrowDiceResultS2C{PlayerId: playerID, MaxPoint: completed.MaxPoint, Point: q.GetPoint()}
	pushes := []Push{s.movementActionPush(ctx, roomID, round, playerID)}
	s.log.Info("controlled movement point selected", "room_id", roomID, "player_id", playerID, "card_id", completed.CardID, "point", q.GetPoint(), "max_point", completed.MaxPoint, "round", round)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}
