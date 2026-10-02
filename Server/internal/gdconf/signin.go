package gdconf

import (
	"bytes"
	"encoding/json"
	"strconv"
	"time"
)

// SignInActivity is the client-visible seven-day sign-in schedule sourced
// from SignIn_infos, FixSignIn_infos, and SignIn_datas.
type SignInActivity struct {
	ID       int32
	RewardID int32
	BeginAt  int64
	EndAt    int64
	Days     map[int32]map[int32]int32
}

func (s *Store) SignInActivityIDs() []int32 {
	ids := s.IDs("SignIn_infos")
	result := make([]int32, 0, len(ids))
	for _, id := range ids {
		if id > 0 && id <= int64(^uint32(0)>>1) {
			result = append(result, int32(id))
		}
	}
	return result
}

func (s *Store) ActiveSignInActivities(now time.Time) []SignInActivity {
	ids := s.IDs("SignIn_infos")
	activities := make([]SignInActivity, 0, len(ids))
	for _, rawID := range ids {
		if rawID <= 0 || rawID > int64(^uint32(0)>>1) {
			continue
		}
		activity, ok := s.SignInActivity(int32(rawID), now)
		if ok {
			activities = append(activities, activity)
		}
	}
	return activities
}

func (s *Store) SignInActivity(id int32, now time.Time) (SignInActivity, bool) {
	if id <= 0 {
		return SignInActivity{}, false
	}
	info, err := s.ReadRaw("SignIn_infos", int64(id))
	if err != nil {
		return SignInActivity{}, false
	}
	beginAt := signInTimestamp(info["beginTime"])
	endAt := signInTimestamp(info["endTime"])
	if fix, fixErr := s.ReadRaw("FixSignIn_infos", int64(id)); fixErr == nil {
		if value := signInTimestamp(fix["beginTime"]); value > 0 {
			beginAt = value
		}
		if value := signInTimestamp(fix["endTime"]); value > 0 {
			endAt = value
		}
	}
	if beginAt <= 0 || endAt <= beginAt || now.Unix() <= beginAt || now.Unix() >= endAt {
		return SignInActivity{}, false
	}

	rewardID := signInInt32(info["signinRewardID"])
	if rewardID <= 0 {
		rewardID = id
	}
	data, err := s.ReadRaw("SignIn_datas", int64(rewardID))
	if err != nil {
		return SignInActivity{}, false
	}
	var schedule struct {
		Items []struct {
			Day    int32                      `json:"day"`
			Reward map[string]json.RawMessage `json:"reward"`
		} `json:"signInDataConfigureItems"`
	}
	if err := json.Unmarshal(data["signInDataConfigureItems"], &schedule.Items); err != nil || len(schedule.Items) < 7 {
		return SignInActivity{}, false
	}
	days := make(map[int32]map[int32]int32, len(schedule.Items))
	for _, item := range schedule.Items {
		if item.Day < 1 || item.Day > 7 || len(item.Reward) == 0 {
			return SignInActivity{}, false
		}
		rewards := make(map[int32]int32, len(item.Reward))
		for rawItemID, rawCount := range item.Reward {
			itemID, parseErr := strconv.ParseInt(rawItemID, 10, 32)
			count := signInInt32(rawCount)
			if parseErr != nil || itemID <= 0 || count <= 0 {
				return SignInActivity{}, false
			}
			rewards[int32(itemID)] = count
		}
		if len(rewards) == 0 || days[item.Day] != nil {
			return SignInActivity{}, false
		}
		days[item.Day] = rewards
	}
	if len(days) != 7 {
		return SignInActivity{}, false
	}
	return SignInActivity{ID: id, RewardID: rewardID, BeginAt: beginAt, EndAt: endAt, Days: days}, true
}

func signInInt32(raw json.RawMessage) int32 {
	var value int32
	if json.Unmarshal(raw, &value) == nil {
		return value
	}
	var wrapped struct {
		Value int32 `json:"value"`
	}
	if json.Unmarshal(raw, &wrapped) == nil {
		return wrapped.Value
	}
	return 0
}

func signInTimestamp(raw json.RawMessage) int64 {
	if len(raw) == 0 || bytes.Equal(raw, []byte("null")) {
		return 0
	}
	var value any
	decoder := json.NewDecoder(bytes.NewReader(raw))
	decoder.UseNumber()
	if decoder.Decode(&value) != nil {
		return 0
	}
	return signInTimestampValue(value)
}

func signInTimestampValue(value any) int64 {
	switch typed := value.(type) {
	case json.Number:
		seconds, _ := typed.Int64()
		return seconds
	case string:
		if seconds, err := strconv.ParseInt(typed, 10, 64); err == nil {
			return seconds
		}
		if parsed, err := time.Parse(time.RFC3339Nano, typed); err == nil {
			return parsed.Unix()
		}
	case []any:
		if len(typed) > 0 {
			return signInTimestampValue(typed[0])
		}
	case map[string]any:
		for _, key := range []string{"seconds", "Seconds", "field_1", "value", "_unknown"} {
			if child, ok := typed[key]; ok {
				if seconds := signInTimestampValue(child); seconds != 0 {
					return seconds
				}
			}
		}
	}
	return 0
}
