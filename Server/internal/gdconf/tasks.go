package gdconf

import (
	"encoding/json"
	"strconv"
)

// TaskRewardConfig is the server-side subset of the client's task resources
// required to validate progress and grant a configured reward.
type TaskRewardConfig struct {
	ID            int32
	Type          int32
	ConditionType int32
	Param         int32
	Ref           int32
	Day           int32
	Progress      int32
	Reward        map[int32]int32
}

// TaskReward returns one resource-backed task definition. Task types match
// TaskLogic.RequestTaskRewardC2S in the 3.2.0 client.
func (s *Store) TaskReward(taskType, id int32) (TaskRewardConfig, bool) {
	tables := map[int32]string{
		1: "Task_beginners",
		2: "Task_weeklys",
		3: "Achieve_globals",
		4: "Task_weeklyProgresss",
		5: "Task_day7S",
	}
	table, ok := tables[taskType]
	if !ok || id <= 0 {
		return TaskRewardConfig{}, false
	}
	row, err := s.ReadRaw(table, int64(id))
	if err != nil {
		return TaskRewardConfig{}, false
	}
	definition := TaskRewardConfig{
		ID: id, Type: taskType,
		ConditionType: taskInt32(row["conditionType"]),
		Param:         taskInt32(row["param"]),
		Ref:           taskInt32(row["ref"]),
		Day:           taskInt32(row["day"]),
		Progress:      taskInt32(row["progress"]),
		Reward:        make(map[int32]int32),
	}
	var rawRewards map[string]json.RawMessage
	if json.Unmarshal(row["reward"], &rawRewards) != nil {
		return TaskRewardConfig{}, false
	}
	for rawID, rawCount := range rawRewards {
		itemID, err := strconv.ParseInt(rawID, 10, 32)
		if err != nil || itemID <= 0 {
			return TaskRewardConfig{}, false
		}
		count := taskInt32(rawCount)
		if count <= 0 {
			return TaskRewardConfig{}, false
		}
		definition.Reward[int32(itemID)] = count
	}
	if definition.Type == 4 {
		if definition.Progress <= 0 {
			return TaskRewardConfig{}, false
		}
	} else if definition.ConditionType <= 0 || definition.Param <= 0 {
		return TaskRewardConfig{}, false
	}
	if definition.Type == 5 && definition.Day <= 0 {
		return TaskRewardConfig{}, false
	}
	return definition, true
}

func taskInt32(raw json.RawMessage) int32 {
	var value int32
	if json.Unmarshal(raw, &value) == nil {
		return value
	}
	var enum struct {
		Value int32 `json:"value"`
	}
	if json.Unmarshal(raw, &enum) == nil {
		return enum.Value
	}
	return 0
}
