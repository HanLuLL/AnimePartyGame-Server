package gateway

import (
	"context"
	"errors"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

const fillingStationHeal int32 = 2

func (s *Server) handleStopOrContinue(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.StopOrContinueC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	current, _, _, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	if current != playerID {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil || phase != store.TurnPhaseFillingStation {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	resolved, landID, created, err := s.resolveFillingStation(ctx, roomID, playerID, in.CmdID, in.UPSN, raw, q.Stop)
	if err != nil {
		if errors.Is(err, store.ErrActionNotReady) || errors.Is(err, store.ErrTurnPlayerMismatch) {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	msg := &protocolpb.StopOrContinueS2C{PlayerId: playerID, Stop: q.Stop}
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{s.pushFor("StopOrContinueS2C", msg, ids, playerID)}
	if resolved.Completed {
		if q.Stop {
			if !resolved.Asymmetrical || resolved.NewSpecialScore != resolved.OldSpecialScore {
				pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", fillingStationAttrUpdate(playerID, landID, resolved), ids, 0))
			}
			if resolved.NewGameScore != resolved.OldGameScore {
				pushes = append(pushes, s.pushFor("GameScoreChangeS2C", &protocolpb.GameScoreChangeS2C{SpecialScore: resolved.NewGameScore}, ids, 0))
			}
		}
		pushes = append(pushes, s.turnPushes(ctx, roomID, resolved.Round, resolved.NextPlayer)...)
	} else {
		pushes = append(pushes, s.actionPush(resolved.Round, 5027, playerID, &protocolpb.MoveC2S{}))
	}
	if q.Stop {
		s.log.Info("FillingStation choice resolved", "room_id", roomID, "player_id", playerID, "land_id", landID, "completed", resolved.Completed, "hp_before", resolved.OldHP, "hp_after", resolved.NewHP, "gold_before", resolved.OldGold, "gold_after", resolved.NewGold, "hero_level_before", resolved.OldLevel, "hero_level_after", resolved.NewLevel, "personal_score_before", resolved.OldSpecialScore, "personal_score_after", resolved.NewSpecialScore, "game_score_before", resolved.OldGameScore, "game_score_after", resolved.NewGameScore)
	} else {
		s.log.Info("FillingStation continue choice resolved", "room_id", roomID, "player_id", playerID, "land_id", landID, "completed", resolved.Completed, "remaining_move", resolved.Pending)
	}
	return DispatchResult{Message: msg, Pushes: pushes}, nil
}

func (s *Server) resolveFillingStation(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, stop bool) (store.FillingStationResult, int32, bool, error) {
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return store.FillingStationResult{}, 0, false, err
	}
	var player *store.Player
	for i := range room.Players {
		if room.Players[i].ID == playerID {
			player = &room.Players[i]
			break
		}
	}
	if player == nil {
		return store.FillingStationResult{}, 0, false, store.ErrPlayerNotFound
	}
	landType, exists := s.domain.LandTypeAt(room, player.NodeID)
	if !exists || (landType != 1 && landType != 2 && !(room.Mode == 7 && landType == 23)) {
		return store.FillingStationResult{}, 0, false, errors.New("stop/continue phase is not on a supported land")
	}
	_, round, _, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return store.FillingStationResult{}, 0, false, err
	}
	giftCount := int32(0)
	if room.Mode == 7 && landType == 23 {
		rules, ok := s.domain.AsymmetricalBattleRules()
		if !ok {
			s.log.Warn("asymmetrical battle resource missing; using client stage defaults", "room_id", roomID)
			rules.SpeedRound = 7
		}
		giftCount = 2
		if round < rules.SpeedRound {
			giftCount = 1
		}
	}
	var upgrades []store.FillingStationUpgrade
	if configured, ok := s.domain.UpgradeItems(room.UpgradePlan); ok {
		upgrades = make([]store.FillingStationUpgrade, 0, len(configured))
		for _, item := range configured {
			upgrades = append(upgrades, store.FillingStationUpgrade{Star: item.Star, Gold: item.Gold})
		}
	} else {
		s.log.Warn("upgrade plan resource missing; station will still heal", "room_id", roomID, "upgrade_plan", room.UpgradePlan)
	}
	maxHP := s.domain.HeroMaxHP(player.HeroID)
	healAmount := int32(0)
	if room.Mode != 7 {
		healAmount = s.applyHeroHPChangePassive(room, playerID, fillingStationHeal)
	}
	teamID := roomTeamID(room.Mode, player.Slot)
	result, created, err := s.store.CompleteFillingStation(ctx, roomID, playerID, cmd, upsn, payload, stop, healAmount, maxHP, upgrades, room.Mode, teamID, landType, giftCount)
	if err != nil {
		return store.FillingStationResult{}, 0, false, err
	}
	return result, player.NodeID, created, nil
}

func fillingStationAttrUpdate(playerID int64, landID int32, result store.FillingStationResult) *protocolpb.UpdateHeroAttrS2C {
	effects := make([]*protocolpb.HeroAttrEffect, 0, 4)
	if !result.Asymmetrical {
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
				PlayerId: playerID, ChangeHp: result.HealAmount, OriHp: result.OldHP,
				CurrHp: result.NewHP, RealChangeHp: result.NewHP - result.OldHP, MaxHp: result.MaxHP,
			}},
		})
	}
	if result.Upgraded {
		effects = append(effects,
			&protocolpb.HeroAttrEffect{PlayerId: playerID, Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId: playerID, ChangeGold: result.NewGold - result.OldGold, OriGold: result.OldGold, CurrGold: result.NewGold,
			}}},
			&protocolpb.HeroAttrEffect{PlayerId: playerID, Data: &protocolpb.HeroAttrEffect_Lv{Lv: &protocolpb.HeroUpLvS2C{
				PlayerId: playerID, CurrLv: result.NewLevel,
			}}},
		)
	}
	if result.NewSpecialScore != result.OldSpecialScore {
		effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: playerID, Data: &protocolpb.HeroAttrEffect_SpecialScore{SpecialScore: &protocolpb.HeroSpecialScoreChangeS2C{
			PlayerId: playerID, ChangeScore: result.NewSpecialScore - result.OldSpecialScore,
			OriScore: result.OldSpecialScore, CurrScore: result.NewSpecialScore,
		}}})
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId:    playerID,
		Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_land, Id: int64(landID)},
		EffectDatas: effects,
	}
}

func (s *Server) botFillingStationStop(room store.Room, bot store.Player, landID int32) bool {
	if room.Mode == 7 {
		landType, exists := s.domain.LandTypeAt(room, landID)
		if !exists {
			return false
		}
		teamID := roomTeamID(room.Mode, bot.Slot)
		switch landType {
		case 23:
			return teamID == 10 || (teamID == 20 && room.SpecialScore > 0)
		case 1, 2:
			return teamID == 10 && bot.SpecialScore > 0
		default:
			return false
		}
	}
	if bot.HP < s.domain.HeroMaxHP(bot.HeroID) {
		return true
	}
	items, ok := s.domain.UpgradeItems(room.UpgradePlan)
	return ok && bot.HeroLevel >= 0 && int(bot.HeroLevel) < len(items) && bot.Gold >= items[bot.HeroLevel].Gold
}

func (s *Server) resolveBotFillingStation(ctx context.Context, roomID int64, bot store.Player, stop bool, ids []int64) (store.FillingStationResult, []Push, error) {
	request := &protocolpb.StopOrContinueC2S{Stop: stop}
	raw, err := proto.Marshal(request)
	if err != nil {
		return store.FillingStationResult{}, nil, err
	}
	result, landID, created, err := s.resolveFillingStation(ctx, roomID, bot.ID, 5077, s.nextActionSN(), raw, stop)
	if err != nil {
		return store.FillingStationResult{}, nil, err
	}
	if !created {
		return store.FillingStationResult{}, nil, store.ErrActionNotReady
	}
	msg := &protocolpb.StopOrContinueS2C{PlayerId: bot.ID, Stop: stop}
	pushes := []Push{s.pushFor("StopOrContinueS2C", msg, ids, 0)}
	if stop {
		if !result.Asymmetrical || result.NewSpecialScore != result.OldSpecialScore {
			pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", fillingStationAttrUpdate(bot.ID, landID, result), ids, 0))
		}
		if result.NewGameScore != result.OldGameScore {
			pushes = append(pushes, s.pushFor("GameScoreChangeS2C", &protocolpb.GameScoreChangeS2C{SpecialScore: result.NewGameScore}, ids, 0))
		}
	}
	return result, pushes, nil
}
