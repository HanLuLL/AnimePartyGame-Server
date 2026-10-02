package gateway

import (
	"context"
	"crypto/rand"
	"math/big"
	"sort"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

// resolveBotBattleActions advances only actions owned by bots. It stops as
// soon as the next decision belongs to a human, leaving the normal PredictAction
// prompt and persisted battle state for that player.
func (s *Server) resolveBotBattleActions(ctx context.Context, roomID int64) []Push {
	var pushes []Push
	for step := 0; step < 8; step++ {
		room, err := s.store.RoomSnapshot(ctx, roomID)
		if err != nil {
			s.log.Error("bot battle could not load room", "room_id", roomID, "err", err)
			return pushes
		}
		state := room.Battle
		if state == nil {
			return pushes
		}
		phase, err := s.store.CurrentTurnPhase(ctx, roomID)
		if err != nil {
			s.log.Error("bot battle phase could not be loaded", "room_id", roomID, "battle_id", state.BattleID, "err", err)
			return pushes
		}
		if phase != state.Stage {
			s.log.Error("bot battle phase does not match persisted state", "room_id", roomID, "battle_id", state.BattleID, "phase", phase, "stage", state.Stage)
			return pushes
		}

		var actor store.Player
		var request any
		var cmdID uint16
		switch phase {
		case store.TurnPhaseBattleChallenge:
			actor, _ = findRoomPlayer(room, state.Attacker.PlayerID)
			if !actor.IsBot {
				return pushes
			}
			request = &protocolpb.AskBattleC2S{AskPlayerId: state.Defender.PlayerID, IsBattle: true}
			cmdID = 5047
		case store.TurnPhaseBattleCards:
			for _, playerID := range []int64{state.Attacker.PlayerID, state.Defender.PlayerID} {
				if state.CardUseState[playerID] {
					continue
				}
				candidate, found := findRoomPlayer(room, playerID)
				if found && candidate.IsBot {
					actor = candidate
					break
				}
			}
			if actor.ID == 0 {
				return pushes
			}
			choice, choiceErr := s.botBattleCardChoice(room, actor, state)
			if choiceErr != nil {
				s.log.Error("bot battle card choice failed", "room_id", roomID, "battle_id", state.BattleID, "player_id", actor.ID, "err", choiceErr)
				return pushes
			}
			request = choice
			cmdID = 5035
		case store.TurnPhaseBattleAttack:
			actor, _ = findRoomPlayer(room, state.Attacker.PlayerID)
			if !actor.IsBot {
				return pushes
			}
			request = &protocolpb.BattleThrowDiceC2S{}
			cmdID = 5037
		case store.TurnPhaseBattleDefense:
			actor, _ = findRoomPlayer(room, state.Defender.PlayerID)
			if !actor.IsBot {
				return pushes
			}
			request = &protocolpb.BattleChoiceC2S{Dodge: botShouldDodge(state, actor.HP)}
			cmdID = 5039
		default:
			return pushes
		}

		session := &Session{}
		session.setIdentity(0, actor.AccountID, actor.ID, actor.Nick, 0)
		frame := wire.Frame{CmdID: cmdID, UPSN: s.nextActionSN()}
		var result DispatchResult
		switch q := request.(type) {
		case *protocolpb.AskBattleC2S:
			result, err = s.handleAskBattle(ctx, session, frame, q)
		case *protocolpb.BattleUseCardC2S:
			result, err = s.handleBattleUseCard(ctx, session, frame, q)
		case *protocolpb.BattleThrowDiceC2S:
			result, err = s.handleBattleThrowDice(ctx, session, frame, q)
		case *protocolpb.BattleChoiceC2S:
			result, err = s.handleBattleChoice(ctx, session, frame, q)
		}
		if err != nil || result.Err != 0 {
			s.log.Error("bot battle action failed", "room_id", roomID, "battle_id", state.BattleID, "player_id", actor.ID, "phase", phase, "err", err, "code", result.Err)
			return pushes
		}
		pushes = append(pushes, result.Pushes...)
		s.log.Info("bot battle action resolved", "room_id", roomID, "battle_id", state.BattleID, "player_id", actor.ID, "phase", phase)
	}
	s.log.Error("bot battle action chain exceeded safety limit", "room_id", roomID)
	return pushes
}

type botBattleCardCandidate struct {
	uniqueID int32
	cardID   int32
	cost     int32
	score    int64
}

func (s *Server) botBattleCardChoice(room store.Room, player store.Player, state *store.BattleState) (*protocolpb.BattleUseCardC2S, error) {
	role := battleRoleFor(state, player.ID)
	if role == nil || len(role.UseCards) > 0 {
		return &protocolpb.BattleUseCardC2S{}, nil
	}
	effectType := int32(1)
	if player.ID == state.Defender.PlayerID {
		effectType = 2
	}
	candidates := make([]botBattleCardCandidate, 0, len(player.Cards))
	for _, card := range player.Cards {
		config, ok := s.resources.BattleCardInfo(card.CardID)
		if !ok || config.EffectType != effectType {
			continue
		}
		cost := config.Cost
		if card.BattleCost >= 0 {
			cost = card.BattleCost
		}
		if cost < 0 || cost > role.Cost || config.MaxBonus <= 0 {
			continue
		}
		candidates = append(candidates, botBattleCardCandidate{
			uniqueID: card.UniqueID, cardID: card.CardID, cost: cost,
			score: int64(config.MinBonus) + int64(config.MaxBonus),
		})
	}
	if len(candidates) == 0 {
		return &protocolpb.BattleUseCardC2S{}, nil
	}
	sort.Slice(candidates, func(i, j int) bool {
		leftCost, rightCost := candidates[i].cost, candidates[j].cost
		if leftCost < 1 {
			leftCost = 1
		}
		if rightCost < 1 {
			rightCost = 1
		}
		left := candidates[i].score * int64(rightCost)
		right := candidates[j].score * int64(leftCost)
		if left != right {
			return left > right
		}
		if candidates[i].score != candidates[j].score {
			return candidates[i].score > candidates[j].score
		}
		if candidates[i].cost != candidates[j].cost {
			return candidates[i].cost < candidates[j].cost
		}
		if candidates[i].cardID != candidates[j].cardID {
			return candidates[i].cardID < candidates[j].cardID
		}
		return candidates[i].uniqueID < candidates[j].uniqueID
	})
	tier := 5 - room.Difficulty
	if tier < 1 {
		tier = 1
	}
	if tier > 5 {
		tier = 5
	}
	choiceCount := 1
	if tier == 1 {
		choiceCount = len(candidates)
	} else if tier == 2 && len(candidates) > 1 {
		choiceCount = 2
	} else if tier == 3 && len(candidates) > 2 {
		choiceCount = 3
	}
	index := 0
	if choiceCount > 1 {
		n, err := rand.Int(rand.Reader, big.NewInt(int64(choiceCount)))
		if err != nil {
			return nil, err
		}
		index = int(n.Int64())
	}
	return &protocolpb.BattleUseCardC2S{CardUid: candidates[index].uniqueID}, nil
}

func botShouldDodge(state *store.BattleState, defenderHP int32) bool {
	if state == nil || defenderHP <= 0 || state.Attacker.Point < 1 || state.Attacker.Point > 6 {
		return false
	}
	attackTotal := state.Attacker.Atk + state.Attacker.Point
	var dodgeDamage, standDamage int32
	for point := int32(1); point <= 6; point++ {
		if state.Attacker.Point >= point && point != 6 {
			damage := attackTotal
			if damage > defenderHP {
				damage = defenderHP
			}
			dodgeDamage += damage
		}
		damage := attackTotal - (state.Defender.Def + point)
		if damage < 1 {
			damage = 1
		}
		if damage > defenderHP {
			damage = defenderHP
		}
		standDamage += damage
	}
	return dodgeDamage < standDamage
}
