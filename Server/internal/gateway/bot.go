package gateway

import (
	"context"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

// turnPushes starts a human turn or resolves consecutive server-controlled
// turns until a human becomes active. Bot output uses the same authoritative
// movement validation and database transaction as human MoveC2S requests.
func (s *Server) turnPushes(ctx context.Context, roomID int64, round int32, playerID int64) []Push {
	return s.resolveBotTurns(ctx, roomID)
}

func (s *Server) beginHumanTurn(ctx context.Context, roomID int64, round int32, playerID int64) []Push {
	ids := mustMemberIDs(ctx, s.store, roomID)
	roundMsg := &protocolpb.GameRoundChangeS2C{Round: round}
	action := &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}
	start := s.roundStartMessage(ctx, roomID, round, playerID)
	pushes := []Push{s.pushFor("GameRoundChangeS2C", roundMsg, ids, 0), s.pushFor("ActionStartNotifyS2C", action, ids, 0), s.pushFor("RoundStartS2C", start, []int64{playerID}, 0)}
	if playerID > 0 {
		pushes = append(pushes, s.movementActionPush(ctx, roomID, round, playerID))
	}
	return pushes
}

func (s *Server) resolveBotTurns(ctx context.Context, roomID int64) []Push {
	var pushes []Push
	ids := mustMemberIDs(ctx, s.store, roomID)
	for turn := 0; turn < 16; turn++ {
		playerID, round, pending, err := s.store.CurrentTurn(ctx, roomID)
		if err != nil {
			s.log.Error("bot turn could not load current turn", "room_id", roomID, "err", err)
			return pushes
		}
		room, err := s.store.RoomSnapshot(ctx, roomID)
		if err != nil {
			s.log.Error("bot turn could not load room", "room_id", roomID, "err", err)
			return pushes
		}
		if room.Mode == 7 {
			if rules, ok := s.domain.AsymmetricalBattleRules(); ok {
				winnerTeamID := int64(0)
				switch {
				case room.SpecialScore >= rules.AttackerWinScore:
					winnerTeamID = 10
				case round >= rules.DefenderWinRound:
					winnerTeamID = 20
				}
				if winnerTeamID != 0 {
					finishPush, finishErr := s.finishRoomPush(ctx, room, winnerTeamID)
					if finishErr != nil {
						s.log.Error("asymmetrical match finish failed", "room_id", roomID, "winner_team_id", winnerTeamID, "err", finishErr)
						return pushes
					}
					s.log.Info("asymmetrical match finished", "room_id", roomID, "winner_team_id", winnerTeamID, "round", round, "game_score", room.SpecialScore)
					return append(pushes, finishPush)
				}
			} else {
				s.log.Warn("asymmetrical match win thresholds are unavailable", "room_id", roomID)
			}
		}
		var bot store.Player
		for _, player := range room.Players {
			if player.ID == playerID {
				bot = player
				break
			}
		}
		if bot.ID == 0 {
			s.log.Error("active turn player is not in room snapshot", "room_id", roomID, "player_id", playerID)
			return pushes
		}
		phase, err := s.store.CurrentTurnPhase(ctx, roomID)
		if err != nil {
			s.log.Error("bot turn phase could not be loaded", "room_id", roomID, "player_id", playerID, "err", err)
			return pushes
		}
		if bot.HospitalRounds > 0 && pending == 0 && phase == store.TurnPhaseThrowDice {
			skipped, next, newRound, hospitalPushes, e := s.skipHospitalizedTurn(ctx, roomID, bot, round, pending, phase, ids)
			if e != nil {
				return pushes
			}
			if !skipped {
				continue
			}
			pushes = append(pushes, hospitalPushes...)
			playerID, round = next, newRound
			continue
		}
		if pending == 0 && phase == store.TurnPhaseThrowDice {
			startPushes, startErr := s.resolveTurnStartEffects(ctx, roomID, round, room, bot, ids)
			if startErr != nil {
				s.log.Error("turn-start effects could not be resolved", "room_id", roomID, "player_id", playerID, "err", startErr)
				return pushes
			}
			pushes = append(pushes, startPushes...)
			if len(startPushes) > 0 {
				room, err = s.store.RoomSnapshot(ctx, roomID)
				if err != nil {
					s.log.Error("room snapshot could not be refreshed after turn-start effects", "room_id", roomID, "player_id", playerID, "err", err)
					return pushes
				}
				for _, member := range room.Players {
					if member.ID == playerID {
						bot = member
						break
					}
				}
			}
		}
		if pending == 0 && phase == store.TurnPhaseThrowDice && bot.HP > 0 {
			bombActive, bombErr := s.store.BeginBombThrow(ctx, roomID, playerID, round)
			if bombErr != nil {
				s.log.Error("bomb turn could not be started", "room_id", roomID, "player_id", playerID, "err", bombErr)
				return pushes
			}
			if bombActive {
				phase = store.TurnPhaseBombThrow
			}
		}
		if bot.HP <= 0 && pending <= 0 && phase == store.TurnPhaseThrowDice {
			skipped, next, newRound, deadPushes, e := s.skipDeadPlayerAction(ctx, roomID, bot, round, pending, phase, ids)
			if e != nil {
				return pushes
			}
			if !skipped {
				continue
			}
			pushes = append(pushes, deadPushes...)
			playerID, round = next, newRound
			continue
		}
		if phase == store.TurnPhaseBattleChallenge || phase == store.TurnPhaseBattleCards || phase == store.TurnPhaseBattleAttack || phase == store.TurnPhaseBattleDefense {
			pushes = append(pushes, s.resolveBotBattleActions(ctx, roomID)...)
			return pushes
		}
		if !bot.IsBot {
			if phase == store.TurnPhaseAbandonCards {
				handLimit, found := s.domain.CardInHandLimit(room.Mode)
				if !found {
					handLimit, found = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
				}
				if !found || handLimit <= 0 || len(bot.Cards) <= int(handLimit) {
					s.log.Error("human card discard phase has no valid excess hand", "room_id", roomID, "player_id", playerID, "hand_count", len(bot.Cards), "hand_limit", handLimit)
					return pushes
				}
				pushes = append(pushes,
					s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
					s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
					s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, roomID, round, playerID), []int64{playerID}, 0),
					s.actionPush(round, 5075, playerID, &protocolpb.AbandonCardC2S{}),
				)
				return pushes
			}
			if phase == store.TurnPhaseSelectEvent {
				choices, actionSN, exists, choiceErr := s.store.SkillEventOffer(ctx, roomID, playerID)
				if choiceErr != nil || !exists {
					s.log.Error("human skill event offer could not be restored", "room_id", roomID, "player_id", playerID, "err", choiceErr)
					return pushes
				}
				pushes = append(pushes, s.actionPushWithSN(round, 5317, playerID, &protocolpb.SelectEventC2S{Events: choices, Idx: 0}, actionSN))
				return pushes
			}
			if phase == store.TurnPhaseHospital {
				if landType, exists := s.domain.LandTypeAt(room, bot.NodeID); !exists || landType != 13 {
					s.log.Error("human Hospital phase is not on a Hospital land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				pushes = append(pushes,
					s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
					s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
					s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, roomID, round, playerID), []int64{playerID}, 0),
					s.actionPush(round, 5093, playerID, &protocolpb.TriggerHospitalC2S{}),
				)
				return pushes
			}
			if phase == store.TurnPhaseDivination {
				choices, exists, choiceErr := s.store.DivinationChoices(ctx, roomID, playerID)
				if choiceErr != nil || !exists {
					s.log.Error("human Divination offer could not be restored", "room_id", roomID, "player_id", playerID, "err", choiceErr)
					return pushes
				}
				pushes = append(pushes,
					s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
					s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
					s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, roomID, round, playerID), []int64{playerID}, 0),
					s.actionPush(round, 5069, playerID, &protocolpb.TriggerDivinationC2S{CanChoiceIds: choices}),
				)
				return pushes
			}
			if phase == store.TurnPhaseEvent {
				pushes = append(pushes,
					s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
					s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
					s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, roomID, round, playerID), []int64{playerID}, 0),
					s.actionPush(round, 5053, playerID, &protocolpb.TriggerEventC2S{}),
				)
				return pushes
			}
			if phase == store.TurnPhaseDestiny {
				if landType, exists := s.domain.LandTypeAt(room, bot.NodeID); !exists || landType != 16 {
					s.log.Error("human Destiny phase is not on a Destiny land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				pushes = append(pushes,
					s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
					s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
					s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, roomID, round, playerID), []int64{playerID}, 0),
					s.actionPush(round, 5071, playerID, &protocolpb.TriggerDestinyC2S{}),
				)
				return pushes
			}
			if phase == store.TurnPhaseFillingStation {
				pushes = append(pushes,
					s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
					s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
					s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, roomID, round, playerID), []int64{playerID}, 0),
					s.actionPush(round, 5077, playerID, &protocolpb.StopOrContinueC2S{}),
				)
				return pushes
			}
			if phase == store.TurnPhaseLottery {
				landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
				if !exists || landType != 9 {
					s.log.Error("pending lottery phase is not on a Lottery land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				chooseCount, numberLimit, e := s.lotterySettings()
				if e != nil || !lotteryHasCapacity(bot, chooseCount, numberLimit) {
					s.log.Error("human lottery choice is unavailable", "room_id", roomID, "player_id", playerID, "err", e)
					return pushes
				}
				pushes = append(pushes,
					s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
					s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
					s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, roomID, round, playerID), []int64{playerID}, 0),
					s.actionPush(round, 5041, playerID, &protocolpb.LotteryChoiceC2S{Num: chooseCount}),
				)
				return pushes
			}
			if phase == store.TurnPhasePursuit {
				landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
				if !exists || landType != 4 {
					s.log.Error("pending pursuit phase is not on a Pursuit land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				pushes = append(pushes, s.actionPush(round, 5033, playerID, &protocolpb.PursuitC2S{}))
				return pushes
			}
			if phase == store.TurnPhaseBattery {
				if landType, exists := s.domain.LandTypeAt(room, bot.NodeID); !exists || landType != batteryLandType {
					s.log.Error("human Battery phase is not on a Battery land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				data, available := s.batteryActionData(room, bot)
				if !available {
					s.log.Error("human Battery phase has no eligible targets", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				pushes = append(pushes,
					s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
					s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
					s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, roomID, round, playerID), []int64{playerID}, 0),
					s.actionPush(round, 5063, playerID, data),
				)
				return pushes
			}
			if phase == store.TurnPhaseShop {
				landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
				if !exists || landType != 7 {
					s.log.Error("pending shop phase is not on a Shop land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				shop, shopErr := s.store.ActiveShop(ctx, roomID, playerID)
				if shopErr != nil {
					s.log.Error("human shop offer could not be restored", "room_id", roomID, "player_id", playerID, "err", shopErr)
					return pushes
				}
				pushes = append(pushes,
					s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
					s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
					s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, roomID, round, playerID), []int64{playerID}, 0),
					s.actionPush(round, 5029, playerID, shopActionData(shop)),
				)
				return pushes
			}
			if phase == store.TurnPhasePVEShop {
				landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
				if !exists || landType != 22 {
					s.log.Error("pending PVE shop phase is not on a PVE shop land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				shop, shopErr := s.store.ActiveShop(ctx, roomID, playerID)
				if shopErr != nil || !shop.PVE {
					s.log.Error("human PVE shop offer could not be restored", "room_id", roomID, "player_id", playerID, "err", shopErr)
					return pushes
				}
				pushes = append(pushes,
					s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
					s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
					s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, roomID, round, playerID), []int64{playerID}, 0),
					s.actionPush(round, 5215, playerID, pveShopActionData(shop)),
				)
				return pushes
			}
			pushes = append(pushes, s.beginHumanTurn(ctx, roomID, round, playerID)...)
			return pushes
		}
		pushes = append(pushes,
			s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
			s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: playerID}, ids, 0),
		)
		if phase == store.TurnPhaseBombThrow {
			bombPushes, bombErr := s.resolveBotBombTurns(ctx, roomID, round, bot, ids)
			if bombErr != nil {
				s.log.Error("bot bomb turn failed", "room_id", roomID, "player_id", playerID, "err", bombErr)
				return pushes
			}
			pushes = append(pushes, bombPushes...)
			room, err = s.store.RoomSnapshot(ctx, roomID)
			if err != nil {
				s.log.Error("room snapshot could not be refreshed after bot bomb", "room_id", roomID, "player_id", playerID, "err", err)
				return pushes
			}
			for _, member := range room.Players {
				if member.ID == playerID {
					bot = member
					break
				}
			}
			phase, err = s.store.CurrentTurnPhase(ctx, roomID)
			if err != nil {
				s.log.Error("turn phase could not be refreshed after bot bomb", "room_id", roomID, "player_id", playerID, "err", err)
				return pushes
			}
			if bot.HP <= 0 && phase == store.TurnPhaseThrowDice {
				skipped, next, newRound, deadPushes, skipErr := s.skipDeadPlayerAction(ctx, roomID, bot, round, 0, phase, ids)
				if skipErr != nil {
					return pushes
				}
				if skipped {
					pushes = append(pushes, deadPushes...)
					pushes = append(pushes, s.turnPushes(ctx, roomID, newRound, next)...)
					return pushes
				}
			}
		}
		if pending == 0 && phase == store.TurnPhaseThrowDice {
			effectPushes, effectErr := s.resolveBotEffectCardTurn(ctx, roomID, round, room, bot, ids)
			if effectErr != nil {
				s.log.Error("bot effect card phase failed", "room_id", roomID, "player_id", playerID, "err", effectErr)
				return pushes
			}
			pushes = append(pushes, effectPushes...)
		}
		if phase == store.TurnPhaseHospital {
			resolved, created, e := s.resolveTriggerHospital(ctx, roomID, playerID, 5093, s.nextActionSN(), &protocolpb.TriggerHospitalC2S{})
			if e != nil || !created {
				s.log.Error("bot Hospital resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			_, hospitalPushes := hospitalMessages(ctx, s, roomID, playerID, resolved)
			pushes = append(pushes, hospitalPushes...)
			playerID, round = resolved.NextPlayer, resolved.Round
			continue
		}
		if phase == store.TurnPhaseDivination {
			resolvedPlayer, resolvedRound, divinationPushes, e := s.botDivination(ctx, room, bot)
			if e != nil {
				s.log.Error("bot Divination resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			pushes = append(pushes, divinationPushes...)
			playerID, round = resolvedPlayer, resolvedRound
			continue
		}
		if phase == store.TurnPhaseShop {
			nextPlayer, nextRound, resolved, e := s.resolveBotShop(ctx, room, bot, ids)
			if e != nil {
				s.log.Error("bot shop resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			pushes = append(pushes, resolved...)
			playerID, round = nextPlayer, nextRound
			continue
		}
		if phase == store.TurnPhasePVEShop {
			nextPlayer, nextRound, resolved, e := s.resolveBotPVEShop(ctx, room, bot, ids)
			if e != nil {
				s.log.Error("bot PVE shop resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			pushes = append(pushes, resolved...)
			playerID, round = nextPlayer, nextRound
			continue
		}
		if phase == store.TurnPhaseLottery {
			landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
			if !exists || landType != 9 {
				s.log.Error("bot lottery phase is not on a Lottery land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
				return pushes
			}
			resolvedPlayer, resolvedRound, resolved, e := s.resolveBotLotteryChoice(ctx, roomID, bot, bot.NodeID, ids)
			if e != nil {
				s.log.Error("bot lottery choice failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			pushes = append(pushes, resolved...)
			playerID, round = resolvedPlayer, resolvedRound
			continue
		}
		if phase == store.TurnPhasePursuit {
			landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
			if !exists || landType != 4 {
				s.log.Error("bot pursuit phase is not on a Pursuit land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
				return pushes
			}
			nextPlayer, nextRound, resolved, e := s.resolveBotPursuit(ctx, room, bot, ids)
			if e != nil {
				s.log.Error("bot Pursuit resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			pushes = append(pushes, resolved...)
			playerID, round = nextPlayer, nextRound
			continue
		}
		if phase == store.TurnPhaseEvent {
			request := &protocolpb.TriggerEventC2S{}
			resolved, created, e := s.resolveTriggerEvent(ctx, roomID, playerID, 5053, s.nextActionSN(), request)
			if e != nil || !created {
				s.log.Error("bot event resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			_, eventPushes := s.eventMessages(ctx, roomID, playerID, resolved)
			pushes = append(pushes, eventPushes...)
			playerID, round = resolved.NextPlayer, resolved.Round
			continue
		}
		if phase == store.TurnPhaseDestiny {
			if landType, exists := s.domain.LandTypeAt(room, bot.NodeID); !exists || landType != 16 {
				s.log.Error("bot Destiny phase is not on a Destiny land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
				return pushes
			}
			resolved, created, e := s.resolveTriggerDestiny(ctx, roomID, playerID, 5071, s.nextActionSN(), &protocolpb.TriggerDestinyC2S{})
			if e != nil || !created {
				s.log.Error("bot Destiny resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			_, destinyPushes := s.destinyMessages(ctx, roomID, playerID, resolved)
			pushes = append(pushes, destinyPushes...)
			playerID, round = resolved.NextPlayer, resolved.Round
			continue
		}
		if phase == store.TurnPhaseFillingStation {
			if landType, exists := s.domain.LandTypeAt(room, bot.NodeID); !exists || (landType != 1 && landType != 2 && !(room.Mode == 7 && landType == 23)) {
				s.log.Error("bot stop/continue phase is not on a supported land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
				return pushes
			}
			resolved, stationPushes, e := s.resolveBotFillingStation(ctx, roomID, bot, s.botFillingStationStop(room, bot, bot.NodeID), ids)
			if e != nil {
				s.log.Error("bot FillingStation recovery failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			pushes = append(pushes, stationPushes...)
			if resolved.Completed {
				playerID, round = resolved.NextPlayer, resolved.Round
				continue
			}
			pending = resolved.Pending
		}
		if phase == store.TurnPhaseAbandonCards {
			handLimit, found := s.domain.CardInHandLimit(room.Mode)
			if !found {
				handLimit, found = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
			}
			if !found || handLimit <= 0 || len(bot.Cards) <= int(handLimit) || len(bot.Cards)-int(handLimit) > 128 {
				s.log.Error("bot card discard phase has no valid excess hand", "room_id", roomID, "player_id", playerID, "hand_count", len(bot.Cards), "hand_limit", handLimit)
				return pushes
			}
			request := &protocolpb.AbandonCardC2S{CardUniqueIds: make([]int32, 0, len(bot.Cards)-int(handLimit))}
			for _, card := range bot.Cards[:len(bot.Cards)-int(handLimit)] {
				request.CardUniqueIds = append(request.CardUniqueIds, card.UniqueID)
			}
			raw, marshalErr := proto.Marshal(request)
			if marshalErr != nil {
				return pushes
			}
			resolved, created, discardErr := s.store.CompleteAbandonCards(ctx, roomID, playerID, 5075, s.nextActionSN(), raw, request.CardUniqueIds, handLimit)
			if discardErr != nil || !created {
				s.log.Error("bot card discard phase failed", "room_id", roomID, "player_id", playerID, "err", discardErr)
				return pushes
			}
			message := &protocolpb.AbandonCardS2C{PlayerId: playerID, CardUniqueIds: resolved.RemovedIDs}
			pushes = append(pushes,
				s.pushFor("AbandonCardS2C", message, ids, 0),
				s.pushFor("UpdateHeroAttrS2C", handCardSnapshotUpdate(playerID, resolved.Cards), ids, 0),
			)
			s.log.Info("bot discarded excess battle cards", "room_id", roomID, "player_id", playerID, "discard_count", len(resolved.RemovedIDs), "hand_count", len(resolved.Cards))
			playerID, round = resolved.NextPlayer, resolved.Round
			continue
		}
		if phase == store.TurnPhaseBattery {
			if landType, exists := s.domain.LandTypeAt(room, bot.NodeID); !exists || landType != batteryLandType {
				s.log.Error("bot Battery phase is not on a Battery land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
				return pushes
			}
			nextPlayer, newRound, resolved, e := s.resolveBotBattery(ctx, room, bot, ids)
			if e != nil {
				s.log.Error("bot Battery phase failed", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID, "err", e)
				return pushes
			}
			pushes = append(pushes, resolved...)
			playerID, round = nextPlayer, newRound
			continue
		}
		if pending <= 0 {
			if phase == store.TurnPhaseLandHeal {
				landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
				if !exists || landType != 20 {
					s.log.Error("bot Heal phase is not on a Heal land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				nextPlayer, newRound, resolved, e := s.resolveBotLandHeal(ctx, roomID, playerID, bot.NodeID, ids)
				if e != nil {
					s.log.Error("bot Heal resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
					return pushes
				}
				pushes = append(pushes, resolved...)
				playerID, round = nextPlayer, newRound
				continue
			}
			if phase == store.TurnPhaseLandBloodLoss {
				landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
				if !exists || landType != 17 {
					s.log.Error("bot BloodLoss phase is not on a BloodLoss land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				nextPlayer, newRound, resolved, e := s.resolveBotLandBloodLoss(ctx, roomID, playerID, bot.NodeID, ids)
				if e != nil {
					s.log.Error("bot BloodLoss resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
					return pushes
				}
				pushes = append(pushes, resolved...)
				playerID, round = nextPlayer, newRound
				continue
			}
			if phase == store.TurnPhaseRollGold {
				landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
				if !exists || landType != 15 {
					s.log.Error("bot RollGold phase is not on a RollGold land", "room_id", roomID, "player_id", playerID, "land_id", bot.NodeID)
					return pushes
				}
				nextPlayer, newRound, resolved, e := s.resolveBotRollGold(ctx, roomID, playerID, bot.NodeID, ids)
				if e != nil {
					s.log.Error("bot RollGold resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
					return pushes
				}
				pushes = append(pushes, resolved...)
				playerID, round = nextPlayer, newRound
				continue
			}
			if phase != store.TurnPhaseThrowDice && phase != store.TurnPhaseMoveAgain {
				s.log.Error("bot cannot roll in unexpected turn phase", "room_id", roomID, "player_id", playerID, "phase", phase)
				return pushes
			}
			diceCount := 1
			if phase == store.TurnPhaseThrowDice {
				for _, buff := range bot.Buffs {
					if buff.BuffID == 3000201 {
						diceCount = 2
						break
					}
				}
			}
			diceVals, points, e := randomMovementDice(diceCount)
			if e != nil {
				s.log.Error("bot dice roll failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			pending = points
			goldBonus := int32(0)
			if points == 6 && s.resources.HeroHasPassiveSkill(bot.HeroID, 11411, usesPVEPassiveSkills(room.Mode)) {
				goldBonus = 6
			}
			rewardSkillID, rewardCards, e := s.movementPassiveCardReward(bot.HeroID, room.Mode, points)
			if e != nil {
				s.log.Error("bot movement passive card reward could not be prepared", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			var action proto.Message
			if phase == store.TurnPhaseMoveAgain {
				action = &protocolpb.MoveAgainC2S{}
			} else {
				action = &protocolpb.ThrowDiceC2S{}
			}
			raw, e := proto.Marshal(action)
			if e != nil {
				return pushes
			}
			cmdID := uint16(5021)
			if phase == store.TurnPhaseMoveAgain {
				cmdID = 5043
			}
			var goldResult store.MoveGoldResult
			var cardResult store.MoveCardsResult
			created, consumedBuff, e := s.store.BeginMoveRoll(ctx, roomID, playerID, cmdID, s.nextActionSN(), raw, diceVals, phase, goldBonus, rewardCards, &goldResult, &cardResult)
			if e != nil || !created {
				s.log.Error("bot movement roll transaction failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			if consumedBuff != nil {
				pushes = append(pushes, s.consumedDiceBuffPush(playerID, *consumedBuff, ids))
			}
			if phase == store.TurnPhaseMoveAgain {
				pushes = append(pushes, s.pushFor("MoveAgainS2C", &protocolpb.MoveAgainS2C{PlayerId: playerID, MovePoint: pending}, ids, 0))
			} else {
				pushes = append(pushes, s.pushFor("ThrowDiceS2C", &protocolpb.ThrowDiceS2C{Vals: diceVals, MovePoint: pending, PlayerId: playerID}, ids, 0))
			}
			if goldResult.Changed {
				pushes = append(pushes, s.passiveGoldPush(playerID, 11411, goldResult.OldGold, goldResult.NewGold, ids))
				s.log.Info("bot movement passive reward", "room_id", roomID, "player_id", playerID, "skill_id", 11411, "gold", goldResult.NewGold-goldResult.OldGold)
			}
			if cardResult.Changed {
				pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", movementSkillCardRewardAttrUpdate(playerID, rewardSkillID, cardResult.Cards), ids, 0))
				s.log.Info("bot movement passive card reward", "room_id", roomID, "player_id", playerID, "skill_id", rewardSkillID, "card_id", cardResult.Cards[0].CardID)
			}
		}
		ended := false
		for pending > 0 {
			room, err = s.store.RoomSnapshot(ctx, roomID)
			if err != nil {
				s.log.Error("bot move could not load room", "room_id", roomID, "err", err)
				return pushes
			}
			for _, player := range room.Players {
				if player.ID == playerID {
					bot = player
					break
				}
			}
			target, e := s.domain.ChooseBotStep(room, bot)
			if e != nil {
				s.log.Error("bot could not choose a legal step", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			force := target == bot.BackNodeID
			end, e := s.domain.ValidateMoveStep(room, bot, target, pending, force)
			if e != nil {
				s.log.Error("bot step failed validation", "room_id", roomID, "player_id", playerID, "node_id", target, "err", e)
				return pushes
			}
			jumpTarget, jumpFront, isJump, e := s.domain.JumpTargetAt(room, target)
			if e != nil {
				s.log.Error("bot jump land could not be resolved", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
				return pushes
			}
			var teleport *store.MoveTeleport
			if isJump {
				teleport = &store.MoveTeleport{NodeID: jumpTarget, FrontNodeIDs: jumpFront}
			}
			landType, landExists := s.domain.LandTypeAt(room, target)
			moveAgain := end && landExists && landType == 12
			rollGold := end && landExists && landType == 15
			landHeal := end && landExists && landType == 20
			landBloodLoss := end && landExists && landType == 17
			landHospital := end && landExists && landType == 13
			landDivination := end && landExists && landType == 6
			landGamble := end && landExists && landType == 10
			noGamblePlayers := false
			drawCard := end && landExists && landType == 14
			fillingStation := end && landExists && (landType == 1 || landType == 2 || (room.Mode == 7 && landType == 23))
			landEvent := end && landExists && landType == 8 && s.domain.HasPlayableEvent(room)
			landDestiny := end && landExists && landType == 16
			if landDestiny && len(s.destinyCandidates(room)) == 0 {
				s.log.Error("Destiny resource pool has no supported outcome for bot", "room_id", roomID, "player_id", playerID, "land_id", target)
				return pushes
			}
			landPursuit := end && landExists && landType == 4
			landBattery := end && landExists && landType == batteryLandType
			if landBattery {
				if _, configErr := s.batteryDamage(); configErr != nil {
					s.log.Error("Battery land configuration is unavailable for bot", "room_id", roomID, "player_id", playerID, "land_id", target, "err", configErr)
					return pushes
				}
				_, landBattery = s.batteryActionData(room, bot)
			}
			landShop := end && landExists && landType == 7
			landPveShop := end && landExists && landType == 22
			landLottery := false
			if end && landExists && landType == 9 {
				chooseCount, numberLimit, settingsErr := s.lotterySettings()
				if settingsErr != nil {
					s.log.Error("lottery land configuration is unavailable for bot", "room_id", roomID, "player_id", playerID, "land_id", target, "err", settingsErr)
					return pushes
				}
				landLottery = lotteryHasCapacity(bot, chooseCount, numberLimit)
			}
			var cardsToDraw []store.CardState
			var handLimit int32
			if drawCard {
				cardsToDraw, handLimit, e = s.drawCardLandReward(room, bot, round, target)
				if e != nil {
					s.log.Error("draw-card land reward could not be prepared for bot", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
					return pushes
				}
			} else if landPveShop {
				cardsToDraw, handLimit, e = s.drawPVEShopVisitReward(room, bot)
				if e != nil {
					s.log.Error("PVE shop visit card could not be prepared for bot", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
					return pushes
				}
			}
			if landHeal {
				params, ok := s.domain.LandParams(20)
				if !ok || len(params) == 0 || params[0] < 0 {
					s.log.Error("Heal resource is unavailable for bot land", "room_id", roomID, "player_id", playerID, "land_id", target)
					return pushes
				}
			}
			if landBloodLoss {
				params, ok := s.domain.LandParams(17)
				if !ok || len(params) == 0 || params[0] > 0 {
					s.log.Error("BloodLoss resource is unavailable for bot land", "room_id", roomID, "player_id", playerID, "land_id", target)
					return pushes
				}
			}
			if landHospital {
				params, ok := s.domain.LandParams(13)
				if !ok || len(params) < 2 || params[0] < 0 || params[1] < 0 {
					s.log.Error("Hospital resource is unavailable for bot land", "room_id", roomID, "player_id", playerID, "land_id", target)
					return pushes
				}
			}
			var divinationChoices []int32
			if landDivination {
				divinationChoices, e = s.newDivinationChoices(room)
				if e != nil {
					s.log.Error("Divination resources are unavailable for bot land", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
					return pushes
				}
			}
			var gambleState *store.GambleState
			if landGamble {
				gambleState, landGamble, e = s.newGambleState(room, target)
				if e != nil {
					s.log.Error("Gamble resources are unavailable for bot land", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
					return pushes
				}
				noGamblePlayers = !landGamble
			}
			var shopState *store.ShopState
			if landShop || landPveShop {
				var prepared store.ShopState
				var prepareErr error
				if landPveShop {
					prepared, prepareErr = s.preparePVEShop(room, bot)
				} else {
					prepared, prepareErr = s.prepareLandShop(room, bot)
				}
				if prepareErr != nil {
					s.log.Error("bot land shop offer could not be prepared", "room_id", roomID, "player_id", playerID, "land_id", target, "err", prepareErr)
					return pushes
				}
				shopState = &prepared
			}
			var battleState *store.BattleState
			landAction := moveAgain || rollGold || landHeal || landBloodLoss || landHospital || landDivination || landGamble || fillingStation || landEvent || landDestiny || landLottery || landPursuit || landBattery || landShop || landPveShop || drawCard
			if end && !landAction && bot.HP > 0 {
				for _, other := range room.Players {
					if other.ID == playerID || other.HP <= 0 || other.NodeID != target || roomTeamID(room.Mode, other.Slot) == roomTeamID(room.Mode, bot.Slot) {
						continue
					}
					candidate, stateErr := s.newBattleState(bot, other, false)
					if stateErr != nil {
						s.log.Error("bot encounter battle could not be prepared", "room_id", roomID, "attacker_id", playerID, "defender_id", other.ID, "err", stateErr)
						return pushes
					}
					battleState = candidate
					break
				}
			}
			q := &protocolpb.MoveC2S{Direction: target, ForceDir: force}
			frontNodeIDs, e := s.domain.ForwardLandIDs(room, target, bot.NodeID)
			if e != nil {
				s.log.Error("bot move exits could not be prepared", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
				return pushes
			}
			goldDropBuff, hasGoldDropBuff := findGoldDropBuff(bot)
			oldGold := bot.Gold
			raw, e := proto.Marshal(q)
			if e != nil {
				return pushes
			}
			sn := s.nextActionSN()
			var cardResult store.MoveCardsResult
			var placeResult store.MovePlaceResult
			var goldResult store.MoveGoldResult
			var landBuffPickup store.LandBuffPickupResult
			next, newRound, created, e := s.store.CommitMoveStep(ctx, roomID, playerID, 5027, sn, raw, bot.NodeID, target, end, moveAgain, rollGold, landHeal, landBloodLoss, landHospital, landDivination, landGamble, fillingStation, landEvent, landDestiny, landLottery, landPursuit, landBattery, landShop, landPveShop, shopState, divinationChoices, gambleState, battleState, frontNodeIDs, teleport, cardsToDraw, handLimit, &cardResult, &placeResult, &goldResult, &landBuffPickup)
			if e != nil || !created {
				s.log.Error("bot step transaction failed", "room_id", roomID, "player_id", playerID, "err", e)
				return pushes
			}
			pushes = append(pushes, s.pushFor("MoveS2C", &protocolpb.MoveS2C{PlayerId: playerID, NodeIds: []int32{target}, End: end}, ids, 0))
			if placeResult.Changed {
				pushes = append(pushes, s.jumpPlacePush(playerID, target, placeResult, ids))
			}
			if cardResult.Changed {
				pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", landCardRewardAttrUpdate(playerID, target, cardResult.Cards, landPveShop), ids, 0))
			}
			if goldResult.Changed && !hasGoldDropBuff {
				pushes = append(pushes, s.shopEntryGoldPush(playerID, target, goldResult.OldGold, goldResult.NewGold, ids))
				s.log.Info("bot legendary merchant entry reward", "room_id", roomID, "player_id", playerID, "gold", goldResult.NewGold-goldResult.OldGold)
			}
			if landBuffPickup.LandBuff.Buff.UniqueID > 0 {
				pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", landBuffPickupAttrUpdate(playerID, landBuffPickup, !(end && hasGoldDropBuff)), ids, 0))
				pushes = append(pushes, s.landBuffsPush(store.LandBuffUpdate{NodeID: landBuffPickup.LandBuff.NodeID, Buffs: landBuffPickup.Remaining}, ids))
				s.log.Info("bot land summon collected", "room_id", roomID, "player_id", playerID, "node_id", landBuffPickup.LandBuff.NodeID, "buff_id", landBuffPickup.LandBuff.Buff.BuffID, "summon_id", landBuffPickup.LandBuff.Buff.Source.ID, "gold", landBuffPickup.LandBuff.Value)
			}
			if end && hasGoldDropBuff {
				room, e = s.store.RoomSnapshot(ctx, roomID)
				if e != nil {
					s.log.Error("bot room snapshot could not refresh after movement buff", "room_id", roomID, "player_id", playerID, "err", e)
					return pushes
				}
				found := false
				for _, member := range room.Players {
					if member.ID == playerID {
						bot = member
						found = true
						break
					}
				}
				if !found {
					s.log.Error("bot player disappeared after movement buff", "room_id", roomID, "player_id", playerID)
					return pushes
				}
				pushes = append(pushes, s.goldDropBuffPush(playerID, oldGold, bot.Gold, goldDropBuff, ids))
			}
			pending--
			if end {
				if battleState != nil {
					pushes = append(pushes, s.resolveBotBattleActions(ctx, roomID)...)
					return pushes
				}
				if fillingStation {
					resolved, stationPushes, e := s.resolveBotFillingStation(ctx, roomID, bot, s.botFillingStationStop(room, bot, target), ids)
					if e != nil {
						s.log.Error("bot FillingStation resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
						return pushes
					}
					pushes = append(pushes, stationPushes...)
					if resolved.Completed {
						ended = true
						playerID, round = resolved.NextPlayer, resolved.Round
						break
					}
					pending = resolved.Pending
					continue
				}
				ended = true
				if cardResult.DiscardRequired {
					break
				}
				if landLottery {
					resolvedPlayer, resolvedRound, resolved, e := s.resolveBotLotteryChoice(ctx, roomID, bot, target, ids)
					if e != nil {
						s.log.Error("bot lottery land resolution failed", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
						return pushes
					}
					pushes = append(pushes, resolved...)
					playerID, round = resolvedPlayer, resolvedRound
				} else if landPursuit {
					nextPlayer, nextRound, resolved, e := s.resolveBotPursuit(ctx, room, bot, ids)
					if e != nil {
						s.log.Error("bot Pursuit land resolution failed", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
						return pushes
					}
					pushes = append(pushes, resolved...)
					playerID, round = nextPlayer, nextRound
				} else if landBattery {
					nextPlayer, nextRound, resolved, e := s.resolveBotBattery(ctx, room, bot, ids)
					if e != nil {
						s.log.Error("bot Battery resolution failed", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
						return pushes
					}
					pushes = append(pushes, resolved...)
					playerID, round = nextPlayer, nextRound
				} else if rollGold {
					next, newRound, resolved, e := s.resolveBotRollGold(ctx, roomID, playerID, target, ids)
					if e != nil {
						s.log.Error("bot RollGold resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
						return pushes
					}
					pushes = append(pushes, resolved...)
					playerID, round = next, newRound
				} else if landHeal {
					next, newRound, resolved, e := s.resolveBotLandHeal(ctx, roomID, playerID, target, ids)
					if e != nil {
						s.log.Error("bot Heal resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
						return pushes
					}
					pushes = append(pushes, resolved...)
					playerID, round = next, newRound
				} else if landBloodLoss {
					next, newRound, resolved, e := s.resolveBotLandBloodLoss(ctx, roomID, playerID, target, ids)
					if e != nil {
						s.log.Error("bot BloodLoss resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
						return pushes
					}
					pushes = append(pushes, resolved...)
					playerID, round = next, newRound
				} else if landEvent {
					request := &protocolpb.TriggerEventC2S{}
					resolved, created, e := s.resolveTriggerEvent(ctx, roomID, playerID, 5053, s.nextActionSN(), request)
					if e != nil || !created {
						s.log.Error("bot event resolution failed", "room_id", roomID, "player_id", playerID, "err", e)
						return pushes
					}
					_, eventPushes := s.eventMessages(ctx, roomID, playerID, resolved)
					pushes = append(pushes, eventPushes...)
					playerID, round = resolved.NextPlayer, resolved.Round
				} else if landDestiny {
					resolved, created, e := s.resolveTriggerDestiny(ctx, roomID, playerID, 5071, s.nextActionSN(), &protocolpb.TriggerDestinyC2S{})
					if e != nil || !created {
						s.log.Error("bot Destiny resolution failed", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
						return pushes
					}
					_, destinyPushes := s.destinyMessages(ctx, roomID, playerID, resolved)
					pushes = append(pushes, destinyPushes...)
					playerID, round = resolved.NextPlayer, resolved.Round
				} else if landHospital {
					resolved, created, e := s.resolveTriggerHospital(ctx, roomID, playerID, 5093, s.nextActionSN(), &protocolpb.TriggerHospitalC2S{})
					if e != nil || !created {
						s.log.Error("bot Hospital land resolution failed", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
						return pushes
					}
					_, hospitalPushes := hospitalMessages(ctx, s, roomID, playerID, resolved)
					pushes = append(pushes, hospitalPushes...)
					playerID, round = resolved.NextPlayer, resolved.Round
				} else if landDivination {
					resolvedPlayer, resolvedRound, divinationPushes, e := s.botDivination(ctx, room, bot)
					if e != nil {
						s.log.Error("bot Divination land resolution failed", "room_id", roomID, "player_id", playerID, "land_id", target, "err", e)
						return pushes
					}
					pushes = append(pushes, divinationPushes...)
					playerID, round = resolvedPlayer, resolvedRound
				} else if landGamble {
					gambleRoom, roomErr := s.store.RoomSnapshot(ctx, roomID)
					if roomErr != nil {
						s.log.Error("Gamble room could not be refreshed", "room_id", roomID, "player_id", playerID, "err", roomErr)
						return pushes
					}
					pushes = append(pushes, s.gambleStartPushes(gambleRoom, round, *gambleState)...)
					gamblePushes, completed, gambleNext, gambleRound, gambleErr := s.resolveGambleBots(ctx, roomID)
					if gambleErr != nil {
						s.log.Error("bot Gamble resolution failed", "room_id", roomID, "player_id", playerID, "err", gambleErr)
						return pushes
					}
					pushes = append(pushes, gamblePushes...)
					if !completed {
						return pushes
					}
					playerID, round = gambleNext, gambleRound
				} else {
					if noGamblePlayers {
						pushes = append(pushes, s.handleGambleLandNoPlayers(roomID, playerID)...)
					}
					playerID, round = next, newRound
				}
				break
			}
		}
		if !ended {
			s.log.Error("bot reached an invalid unfinished movement state", "room_id", roomID, "bot_player_id", bot.ID)
			return pushes
		}
	}
	s.log.Error("automatic bot turn chain exceeded safety limit", "room_id", roomID)
	return pushes
}
