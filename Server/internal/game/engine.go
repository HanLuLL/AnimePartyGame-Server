// Package game is the domain layer. New handlers should be added here rather
// than in gateway code: gateway decodes a command, game mutates state, and the
// gateway serializes the returned response/pushes.
package game

import (
	"context"
	"crypto/rand"
	"errors"
	"fmt"
	"math/big"
	"sort"

	store "astralparty-server/internal/db"
	"astralparty-server/internal/gdconf"
)

type Engine struct {
	DB     *store.Store
	Config *gdconf.Store
}

var eventEffectIDs = map[int32]bool{
	30001: true,
	30002: true,
	30003: true,
	30006: true,
	30005: true,
	30008: true,
	30009: true,
	30010: true,
	30012: true,
	30013: true,
	30014: true,
	30015: true,
	30018: true,
	30019: true,
	30020: true,
	30021: true,
	30201: true,
	30202: true,
	30206: true,
	30205: true,
}

func New(d *store.Store, c *gdconf.Store) *Engine { return &Engine{DB: d, Config: c} }
func (e *Engine) Room(ctx context.Context, id int64) (store.Room, error) {
	return e.DB.RoomSnapshot(ctx, id)
}
func (e *Engine) MapEvents(mapID int64) []int32 { return e.Config.MapEventIDs(mapID) }
func (e *Engine) EventPoolForMapMode(mapID int64, mode int32) (int32, bool) {
	return e.Config.EventPoolForMapMode(mapID, mode)
}
func (e *Engine) EventIDsForPool(poolID int32) ([]int32, bool) {
	return e.Config.EventIDsForPool(poolID)
}
func (e *Engine) EventInfoExists(eventID int32) bool {
	return e.Config.EventInfoExists(eventID)
}
func (e *Engine) EventParams(eventID int32) ([]int32, bool) {
	return e.Config.EventParams(eventID)
}
func (e *Engine) EventBuffIDs(eventID int32) ([]int32, bool) {
	return e.Config.EventBuffIDs(eventID)
}
func (e *Engine) BuffTiming(buffID int32) (int32, int32, bool) {
	return e.Config.BuffTiming(buffID)
}
func (e *Engine) BuffRoundCountType(buffID int32) (int32, bool) {
	return e.Config.BuffRoundCountType(buffID)
}
func (e *Engine) BuffClearsOnDeath(buffID int32) bool {
	return e.Config.BuffClearsOnDeath(buffID)
}
func (e *Engine) BattleCardIDs(poolID int32) ([]int32, bool) {
	return e.Config.BattleCardIDs(poolID)
}
func (e *Engine) EffectCardIDs(poolID int32) ([]int32, bool) {
	return e.Config.EffectCardIDs(poolID)
}
func (e *Engine) CardPoolsForMapMode(mapID int64, mode int32) (int32, int32, bool) {
	return e.Config.CardPoolsForMapMode(mapID, mode)
}
func (e *Engine) CardInHandLimit(mode int32) (int32, bool) {
	return e.Config.CardInHandLimit(mode)
}
func (e *Engine) MapProgressLimit(mapID int64, difficulty int32) (int32, bool) {
	return e.Config.MapProgressLimit(mapID, difficulty)
}
func (e *Engine) EventEffectSupported(eventID int32) bool {
	return eventEffectIDs[eventID]
}
func (e *Engine) HasPlayableEvent(room store.Room) bool {
	poolID, ok := e.EventPoolForMapMode(int64(room.MapID), room.Mode)
	if !ok {
		return false
	}
	eventIDs, ok := e.EventIDsForPool(poolID)
	if !ok {
		return false
	}
	for _, eventID := range eventIDs {
		if e.EventEffectSupported(eventID) && e.EventInfoExists(eventID) {
			return true
		}
	}
	return false
}
func (e *Engine) LandParams(landType int32) ([]int32, bool) {
	return e.Config.LandParams(landType)
}
func (e *Engine) HeroMaxHP(heroID int32) int32 { return e.Config.HeroMaxHP(heroID) }
func (e *Engine) UpgradeItems(planID int32) ([]gdconf.UpgradeItem, bool) {
	return e.Config.UpgradeItems(planID)
}
func (e *Engine) AsymmetricalBattleRules() (gdconf.AsymmetricalRules, bool) {
	return e.Config.AsymmetricalBattleRules()
}

func (e *Engine) LandTypeAt(room store.Room, nodeID int32) (int32, bool) {
	graph, ok := e.Config.BoardGraphForMap(int64(room.MapID), room.MapIndex)
	if !ok {
		return 0, false
	}
	for _, node := range graph.Nodes {
		if node.ID != nodeID {
			continue
		}
		if node.RuntimeLandType != 0 {
			return node.RuntimeLandType, true
		}
		return node.LandType, true
	}
	return 0, false
}

// ActiveNeighborLandIDs returns the map's active exits from a land in its
// configured order, which is also the order consumed by the client board.
func (e *Engine) ActiveNeighborLandIDs(room store.Room, landID int32) ([]int32, error) {
	graph, ok := e.Config.BoardGraphForMap(int64(room.MapID), room.MapIndex)
	if !ok {
		return nil, fmt.Errorf("board graph missing for map %d index %d", room.MapID, room.MapIndex)
	}
	for _, node := range graph.Nodes {
		if node.ID != landID || node.Disabled {
			continue
		}
		neighbors := make(map[int32]bool, len(graph.Nodes))
		for _, candidate := range graph.Nodes {
			neighbors[candidate.ID] = !candidate.Disabled
		}
		front := make([]int32, 0, len(node.NeighborLandIDs))
		for _, neighborID := range node.NeighborLandIDs {
			if neighbors[neighborID] {
				front = append(front, neighborID)
			}
		}
		if len(front) == 0 {
			return nil, fmt.Errorf("land %d has no active exits", landID)
		}
		return front, nil
	}
	return nil, fmt.Errorf("land %d is not active", landID)
}

// WithinLandDistance follows the active neighbor graph used by ordinary
// movement and card target selection. It returns false when either endpoint
// is disabled or no path of at most maxDistance edges exists.
func (e *Engine) WithinLandDistance(room store.Room, fromID, targetID, maxDistance int32) (bool, error) {
	if maxDistance < 0 {
		return false, nil
	}
	graph, ok := e.Config.BoardGraphForMap(int64(room.MapID), room.MapIndex)
	if !ok {
		return false, fmt.Errorf("board graph missing for map %d index %d", room.MapID, room.MapIndex)
	}
	nodes := make(map[int32]gdconf.LandNode, len(graph.Nodes))
	for _, node := range graph.Nodes {
		if !node.Disabled {
			nodes[node.ID] = node
		}
	}
	if _, found := nodes[fromID]; !found {
		return false, fmt.Errorf("source land %d is not active", fromID)
	}
	if _, found := nodes[targetID]; !found {
		return false, fmt.Errorf("target land %d is not active", targetID)
	}
	if fromID == targetID {
		return true, nil
	}
	type step struct {
		id       int32
		distance int32
	}
	queue := []step{{id: fromID}}
	seen := map[int32]bool{fromID: true}
	for len(queue) > 0 {
		current := queue[0]
		queue = queue[1:]
		if current.distance >= maxDistance {
			continue
		}
		for _, neighborID := range nodes[current.id].NeighborLandIDs {
			if seen[neighborID] {
				continue
			}
			if _, active := nodes[neighborID]; !active {
				continue
			}
			distance := current.distance + 1
			if neighborID == targetID {
				return true, nil
			}
			seen[neighborID] = true
			queue = append(queue, step{id: neighborID, distance: distance})
		}
	}
	return false, nil
}

// ForwardLandIDs returns every active exit except the land the character just
// left, preserving the movement direction in reconnect snapshots.
func (e *Engine) ForwardLandIDs(room store.Room, landID, backID int32) ([]int32, error) {
	neighbors, err := e.ActiveNeighborLandIDs(room, landID)
	if err != nil {
		return nil, err
	}
	front := make([]int32, 0, len(neighbors))
	for _, neighborID := range neighbors {
		if neighborID != backID {
			front = append(front, neighborID)
		}
	}
	if len(front) == 0 {
		return neighbors, nil
	}
	return front, nil
}

// JumpTargetAt resolves a Jump land through the board graph shipped in
// Resources. FrontNodeIDs are the active exits from the destination land.
func (e *Engine) JumpTargetAt(room store.Room, landID int32) (int32, []int32, bool, error) {
	graph, ok := e.Config.BoardGraphForMap(int64(room.MapID), room.MapIndex)
	if !ok {
		return 0, nil, false, fmt.Errorf("board graph missing for map %d index %d", room.MapID, room.MapIndex)
	}
	nodes := make(map[int32]gdconf.LandNode, len(graph.Nodes))
	for _, node := range graph.Nodes {
		nodes[node.ID] = node
	}
	land, ok := nodes[landID]
	if !ok || land.Disabled {
		return 0, nil, false, fmt.Errorf("jump land %d is not active", landID)
	}
	landType := land.LandType
	if land.RuntimeLandType != 0 {
		landType = land.RuntimeLandType
	}
	if landType != 18 {
		return 0, nil, false, nil
	}
	if land.JumpNodeID < 0 || land.JumpNodeID == landID {
		return 0, nil, false, fmt.Errorf("jump land %d has invalid destination %d", landID, land.JumpNodeID)
	}
	destination, ok := nodes[land.JumpNodeID]
	if !ok || destination.Disabled {
		return 0, nil, false, fmt.Errorf("jump land %d destination %d is not active", landID, land.JumpNodeID)
	}
	front := make([]int32, 0, len(destination.NeighborLandIDs))
	for _, neighborID := range destination.NeighborLandIDs {
		neighbor, exists := nodes[neighborID]
		if exists && !neighbor.Disabled {
			front = append(front, neighborID)
		}
	}
	if len(front) == 0 {
		return 0, nil, false, fmt.Errorf("jump destination %d has no active exits", destination.ID)
	}
	return destination.ID, front, true, nil
}

// HospitalNodeID finds the map's configured hospital tile. A map without an
// active hospital cannot resolve the hospital event and should not draw it.
func (e *Engine) HospitalNodeID(room store.Room) (int32, bool) {
	graph, ok := e.Config.BoardGraphForMap(int64(room.MapID), room.MapIndex)
	if !ok {
		return 0, false
	}
	var hospitalID int32
	for _, node := range graph.Nodes {
		landType := node.LandType
		if node.RuntimeLandType != 0 {
			landType = node.RuntimeLandType
		}
		if node.Disabled || landType != 13 {
			continue
		}
		if hospitalID == 0 || node.ID < hospitalID {
			hospitalID = node.ID
		}
	}
	return hospitalID, hospitalID > 0
}

func (e *Engine) EventAvailableInRoom(eventID int32, room store.Room) bool {
	if eventID == 30015 {
		_, ok := e.HospitalNodeID(room)
		return ok
	}
	if eventID == 30013 {
		return e.HasConnectedLandSet(room, 4)
	}
	return true
}

func (e *Engine) HasConnectedLandSet(room store.Room, count int) bool {
	graph, ok := e.Config.BoardGraphForMap(int64(room.MapID), room.MapIndex)
	if !ok || count <= 0 {
		return false
	}
	for _, component := range landComponents(graph.Nodes) {
		if len(component) >= count {
			return true
		}
	}
	return false
}

// RandomConnectedLandSet returns a uniformly seeded connected subset grown
// from an active node. The game data determines connectivity; the server owns
// the random seed and the resulting player positions.
func (e *Engine) RandomConnectedLandSet(room store.Room, count int) ([]int32, bool, error) {
	graph, ok := e.Config.BoardGraphForMap(int64(room.MapID), room.MapIndex)
	if !ok || count <= 0 {
		return nil, false, nil
	}
	components := landComponents(graph.Nodes)
	eligible := make([][]int32, 0, len(components))
	for _, component := range components {
		if len(component) >= count {
			eligible = append(eligible, component)
		}
	}
	if len(eligible) == 0 {
		return nil, false, nil
	}
	componentIndex, err := secureIndex(len(eligible))
	if err != nil {
		return nil, false, err
	}
	component := eligible[componentIndex]
	startIndex, err := secureIndex(len(component))
	if err != nil {
		return nil, false, err
	}
	nodes := make(map[int32]gdconf.LandNode, len(graph.Nodes))
	for _, node := range graph.Nodes {
		if !node.Disabled {
			nodes[node.ID] = node
		}
	}
	selected := []int32{component[startIndex]}
	selectedSet := map[int32]bool{selected[0]: true}
	for len(selected) < count {
		frontierSet := make(map[int32]bool)
		for _, currentID := range selected {
			for _, neighborID := range nodes[currentID].NeighborLandIDs {
				if _, exists := nodes[neighborID]; !exists || selectedSet[neighborID] {
					continue
				}
				frontierSet[neighborID] = true
			}
		}
		frontier := make([]int32, 0, len(frontierSet))
		for id := range frontierSet {
			frontier = append(frontier, id)
		}
		sort.Slice(frontier, func(i, j int) bool { return frontier[i] < frontier[j] })
		if len(frontier) == 0 {
			return nil, false, nil
		}
		index, err := secureIndex(len(frontier))
		if err != nil {
			return nil, false, err
		}
		id := frontier[index]
		selected = append(selected, id)
		selectedSet[id] = true
	}
	return selected, true, nil
}

func landComponents(nodes []gdconf.LandNode) [][]int32 {
	active := make(map[int32]gdconf.LandNode, len(nodes))
	ids := make([]int32, 0, len(nodes))
	for _, node := range nodes {
		if node.Disabled {
			continue
		}
		active[node.ID] = node
		ids = append(ids, node.ID)
	}
	sort.Slice(ids, func(i, j int) bool { return ids[i] < ids[j] })
	visited := make(map[int32]bool, len(active))
	components := make([][]int32, 0)
	for _, start := range ids {
		if visited[start] {
			continue
		}
		visited[start] = true
		component := []int32{start}
		for cursor := 0; cursor < len(component); cursor++ {
			for _, neighbor := range active[component[cursor]].NeighborLandIDs {
				if _, exists := active[neighbor]; !exists || visited[neighbor] {
					continue
				}
				visited[neighbor] = true
				component = append(component, neighbor)
			}
		}
		components = append(components, component)
	}
	return components
}

func secureIndex(length int) (int, error) {
	value, err := rand.Int(rand.Reader, big.NewInt(int64(length)))
	if err != nil {
		return 0, err
	}
	return int(value.Int64()), nil
}

// InitialPositions assigns each player to the serialized Born land whose
// PlayerSerialNumber matches the client's zero-based room slot.
func (e *Engine) InitialPositions(room store.Room) (map[int64]store.InitialPosition, error) {
	graph, ok := e.Config.BoardGraphForMap(int64(room.MapID), room.MapIndex)
	if !ok {
		return nil, fmt.Errorf("board graph missing for map %d index %d", room.MapID, room.MapIndex)
	}
	positions := make(map[int64]store.InitialPosition, len(room.Players))
	for _, player := range room.Players {
		var born *gdconf.LandNode
		for i := range graph.Nodes {
			node := &graph.Nodes[i]
			if node.LandType == 1 && node.PlayerSerialNumber == player.Slot {
				born = node
				break
			}
		}
		if born == nil {
			return nil, fmt.Errorf("map %s has no born land for player slot %d", graph.AssetName, player.Slot)
		}
		back := born.InitDirLandID
		if back == born.ID || !hasActiveNeighbor(graph.Nodes, born.ID, back) {
			back = 0
			for _, id := range born.NeighborLandIDs {
				if hasActiveNeighbor(graph.Nodes, born.ID, id) {
					back = id
					break
				}
			}
		}
		if back == born.ID || !hasActiveNeighbor(graph.Nodes, born.ID, back) {
			return nil, fmt.Errorf("born land %d has no active neighbor", born.ID)
		}
		front, err := e.ForwardLandIDs(room, born.ID, back)
		if err != nil {
			return nil, err
		}
		positions[player.ID] = store.InitialPosition{NodeID: born.ID, BackNodeID: back, FrontNodeIDs: front}
	}
	return positions, nil
}

// ValidateMoveStep validates one chosen destination. MoveC2S.Direction is one
// adjacent land ID; the client sends another request for each remaining step.
func (e *Engine) ValidateMoveStep(room store.Room, player store.Player, targetID, pending int32, forceDirection bool) (bool, error) {
	if pending <= 0 {
		return false, errors.New("no movement points remain")
	}
	graph, ok := e.Config.BoardGraphForMap(int64(room.MapID), room.MapIndex)
	if !ok {
		return false, fmt.Errorf("board graph missing for map %d index %d", room.MapID, room.MapIndex)
	}
	nodes := make(map[int32]gdconf.LandNode, len(graph.Nodes))
	for _, node := range graph.Nodes {
		nodes[node.ID] = node
	}
	from, ok := nodes[player.NodeID]
	if !ok || from.Disabled {
		return false, fmt.Errorf("current land %d is not active", player.NodeID)
	}
	to, ok := nodes[targetID]
	if !ok || to.Disabled {
		return false, fmt.Errorf("target land %d is not active", targetID)
	}
	adjacent := false
	for _, id := range from.NeighborLandIDs {
		if id == targetID {
			adjacent = true
			break
		}
	}
	if !adjacent {
		return false, fmt.Errorf("land %d is not adjacent to %d", targetID, from.ID)
	}
	if !forceDirection && targetID == player.BackNodeID {
		return false, errors.New("reverse movement is not allowed")
	}
	landType := to.LandType
	if to.RuntimeLandType != 0 {
		landType = to.RuntimeLandType
	}
	stop := landType == 1 || landType == 2 || landType == 7 || landType == 8 || landType == 12 || landType == 22 || landType == 23 || landType == 24
	return pending == 1 || stop, nil
}

func hasActiveNeighbor(nodes []gdconf.LandNode, fromID, neighborID int32) bool {
	var from *gdconf.LandNode
	for i := range nodes {
		if nodes[i].ID == fromID {
			from = &nodes[i]
			break
		}
	}
	if from == nil {
		return false
	}
	for _, id := range from.NeighborLandIDs {
		if id != neighborID {
			continue
		}
		for _, node := range nodes {
			if node.ID == id {
				return !node.Disabled
			}
		}
	}
	return false
}
