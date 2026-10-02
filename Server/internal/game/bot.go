package game

import (
	"crypto/rand"
	"fmt"
	"math/big"
	"sort"

	store "astralparty-server/internal/db"
	"astralparty-server/internal/gdconf"
)

// ChooseBotStep selects one legal adjacent land. Lower room difficulty values
// produce more deliberate choices, following the requested inverse mapping.
// The server still validates and commits every step through the same rules as
// a human move.
func (e *Engine) ChooseBotStep(room store.Room, player store.Player) (int32, error) {
	graph, ok := e.Config.BoardGraphForMap(int64(room.MapID), room.MapIndex)
	if !ok {
		return 0, fmt.Errorf("board graph missing for map %d index %d", room.MapID, room.MapIndex)
	}
	nodes := make(map[int32]gdconf.LandNode, len(graph.Nodes))
	for _, node := range graph.Nodes {
		nodes[node.ID] = node
	}
	from, ok := nodes[player.NodeID]
	if !ok || from.Disabled {
		return 0, fmt.Errorf("bot current land %d is not active", player.NodeID)
	}
	candidates := make([]gdconf.LandNode, 0, len(from.NeighborLandIDs))
	for _, id := range from.NeighborLandIDs {
		node, exists := nodes[id]
		if exists && !node.Disabled && id != player.BackNodeID {
			candidates = append(candidates, node)
		}
	}
	if len(candidates) == 0 && player.BackNodeID != 0 {
		if node, exists := nodes[player.BackNodeID]; exists && !node.Disabled {
			candidates = append(candidates, node)
		}
	}
	if len(candidates) == 0 {
		return 0, fmt.Errorf("bot has no active move from land %d", player.NodeID)
	}
	// Easier rooms make the bot deliberately choose a useful tile. Higher
	// difficulty values reduce the policy to a more human-like random choice.
	tier := 5 - room.Difficulty
	if tier < 1 {
		tier = 1
	}
	if tier > 5 {
		tier = 5
	}
	if tier == 1 {
		idx, err := rand.Int(rand.Reader, big.NewInt(int64(len(candidates))))
		if err != nil {
			return 0, err
		}
		return candidates[idx.Int64()].ID, nil
	}
	sort.Slice(candidates, func(i, j int) bool {
		si, sj := botLandScore(candidates[i].LandType), botLandScore(candidates[j].LandType)
		if si != sj {
			return si > sj
		}
		return candidates[i].ID < candidates[j].ID
	})
	// The stronger tiers choose from the top few options with small randomness
	// so identical bot seats do not all make the same route choice.
	choiceCount := 1
	if tier <= 2 && len(candidates) > 1 {
		choiceCount = 2
	} else if tier == 3 && len(candidates) > 2 {
		choiceCount = 3
	}
	idx, err := rand.Int(rand.Reader, big.NewInt(int64(choiceCount)))
	if err != nil {
		return 0, err
	}
	return candidates[idx.Int64()].ID, nil
}

func botLandScore(landType int32) int {
	switch landType {
	case 20: // Heal
		return 8
	case 24: // Relic
		return 7
	case 23: // Gift
		return 6
	case 14: // DrawCard
		return 5
	case 2: // FillingStation
		return 4
	case 15: // RollGold
		return 3
	case 7, 22, 26: // Shop, PVE shop, vendor
		return 2
	case 17: // BloodLoss
		return -8
	case 19, 21: // Monster, monster pursuit
		return -3
	case 10: // Gamble
		return -2
	default:
		return 0
	}
}
