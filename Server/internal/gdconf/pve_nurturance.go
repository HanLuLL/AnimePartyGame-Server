package gdconf

import "encoding/json"

type PveTalent struct {
	ID                 int32           `json:"id"`
	UnlockNeedPVELevel int32           `json:"unlockNeedPVELevel"`
	NeedMaterials      map[int32]int32 `json:"needMaterials"`
}

// PveLevelExps maps each current level to the EXP required to reach the next
// level. The final row is the cap and has no EXP requirement.
func (s *Store) PveLevelExps() map[int32]int32 {
	levels := make(map[int32]int32)
	for _, rawID := range s.IDs("PVENurturance_levelups") {
		var row struct {
			ID  int32 `json:"id"`
			Exp int32 `json:"exp"`
		}
		raw, ok := s.Get("PVENurturance_levelups", rawID)
		if ok && json.Unmarshal(raw, &row) == nil && row.ID > 0 {
			levels[row.ID] = row.Exp
		}
	}
	return levels
}

func (s *Store) PveItemExp(itemID int32) (int32, bool) {
	raw, ok := s.Get("PVENurturance_itemExps", int64(itemID))
	if !ok {
		return 0, false
	}
	var row struct {
		ID  int32 `json:"id"`
		Exp int32 `json:"exp"`
	}
	if json.Unmarshal(raw, &row) != nil || row.ID != itemID || row.Exp <= 0 {
		return 0, false
	}
	return row.Exp, true
}

func (s *Store) PveTalent(id int32) (PveTalent, bool) {
	raw, ok := s.Get("PVENurturance_breaks", int64(id))
	if !ok {
		return PveTalent{}, false
	}
	var row PveTalent
	if json.Unmarshal(raw, &row) != nil || row.ID != id || row.UnlockNeedPVELevel <= 0 {
		return PveTalent{}, false
	}
	if row.NeedMaterials == nil {
		row.NeedMaterials = map[int32]int32{}
	}
	return row, true
}

func (s *Store) PveTalentIDs(heroID int32) []int32 {
	raw, ok := s.Get("Character_infos", int64(heroID))
	if !ok {
		return nil
	}
	var row struct {
		ID       int32   `json:"id"`
		PveBreak []int32 `json:"pveBreak"`
	}
	if json.Unmarshal(raw, &row) != nil || row.ID != heroID {
		return nil
	}
	return append([]int32(nil), row.PveBreak...)
}
