package gateway

import (
	"encoding/json"
	"fmt"
	"os"
	"sort"
	"strconv"
)

// GenerateHandbook writes an operator handbook listing the GM
// commands and every resource-backed ID with localized names. Names resolve
// through each table's nameID into its dedicated STR*_locals table, matching
// how the game client looks them up.
func (s *Server) GenerateHandbook(path string) error {
	f, err := os.Create(path)
	if err != nil {
		return err
	}
	defer f.Close()

	fmt.Fprintln(f, "# Astral Party Server Handbook")
	fmt.Fprintln(f, "# Generated at server startup; IDs come from the loaded config tables.")
	fmt.Fprintln(f, "")
	fmt.Fprintln(f, "# GM Commands (send to any friend as a chat message)")
	for _, usage := range s.GMCommandUsages() {
		fmt.Fprintln(f, "  "+usage)
	}
	fmt.Fprintln(f, "")

	s.dumpHandbookTable(f, "Items (Item_infos)", "Item_infos", "STRItem_locals")
	s.dumpHandbookTable(f, "Heroes (Character_infos)", "Character_infos", "STRCharacter_locals")
	s.dumpHandbookTable(f, "Cards (Card_infos)", "Card_infos", "STRCard_locals")
	s.dumpHandbookTable(f, "Lands (Land_infos)", "Land_infos", "STRLand_locals")
	return nil
}

// dumpHandbookTable writes "id : name" rows sorted by numeric ID. Names come
// from the row's nameID field looked up in the given STR locals table.
func (s *Server) dumpHandbookTable(f *os.File, title, table, localsTable string) {
	ids := s.resources.IDs(table)
	if len(ids) == 0 {
		return
	}
	sorted := make([]int64, len(ids))
	copy(sorted, ids)
	sort.Slice(sorted, func(i, j int) bool { return sorted[i] < sorted[j] })
	fmt.Fprintln(f, "# "+title)
	for _, id := range sorted {
		row, ok := s.resources.Get(table, id)
		if !ok {
			continue
		}
		var fields map[string]any
		if json.Unmarshal(row, &fields) != nil {
			continue
		}
		name := strconv.FormatInt(id, 10)
		if nameID, ok := fields["nameID"].(float64); ok {
			if locals, ok := s.resources.Get(localsTable, int64(nameID)); ok {
				var entry map[string]any
				if json.Unmarshal(locals, &entry) == nil {
					if text, ok := entry["simplified"].(string); ok && text != "" {
						name = text
					}
				}
			}
		}
		fmt.Fprintf(f, "%d : %s\n", id, name)
	}
	fmt.Fprintln(f, "")
}

// HandbookPath returns the handbook file next to the database.
func HandbookPath(dbPath string) string {
	for i := len(dbPath) - 1; i >= 0; i-- {
		if dbPath[i] == '/' || dbPath[i] == '\\' {
			return dbPath[:i] + "/Handbook.txt"
		}
	}
	return "Handbook.txt"
}
