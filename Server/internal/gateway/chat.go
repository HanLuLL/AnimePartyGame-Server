package gateway

import (
	"context"
	"encoding/json"
	"strconv"
	"strings"
	"time"
	"unicode"
	"unicode/utf8"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

const chatCooldown = 600 * time.Millisecond
const maxRoomChatRunes = 200

func (s *Server) allowChat(playerID int64) bool {
	now := time.Now()
	s.chatMu.Lock()
	defer s.chatMu.Unlock()
	if s.chatLastAt == nil {
		s.chatLastAt = make(map[int64]time.Time)
	}
	if previous, ok := s.chatLastAt[playerID]; ok && now.Sub(previous) < chatCooldown {
		return false
	}
	s.chatLastAt[playerID] = now
	if len(s.chatLastAt) > 4096 {
		for id, previous := range s.chatLastAt {
			if now.Sub(previous) > time.Minute {
				delete(s.chatLastAt, id)
			}
		}
	}
	return true
}

func normalizeRoomChat(message string) (string, bool) {
	message = strings.TrimSpace(message)
	if message == "" || utf8.RuneCountInString(message) > maxRoomChatRunes {
		return "", false
	}
	for _, r := range message {
		if unicode.IsControl(r) {
			return "", false
		}
	}
	return message, true
}

func roomChatRecipients(room store.Room, sender int64, requested []int64) ([]int64, bool) {
	members := make(map[int64]struct{}, len(room.Players))
	for _, player := range room.Players {
		members[player.ID] = struct{}{}
	}
	if len(requested) == 0 {
		requested = memberIDs(room)
	}
	seen := make(map[int64]struct{}, len(requested))
	recipients := make([]int64, 0, len(requested))
	for _, playerID := range requested {
		if _, ok := members[playerID]; !ok {
			return nil, false
		}
		if playerID == sender {
			continue
		}
		if _, exists := seen[playerID]; exists {
			continue
		}
		seen[playerID] = struct{}{}
		recipients = append(recipients, playerID)
	}
	return recipients, true
}

func hasRoomPlayer(room store.Room, playerID int64) bool {
	for _, player := range room.Players {
		if player.ID == playerID {
			return true
		}
	}
	return false
}

func (s *Server) validQuickChatIndex(index int32) bool {
	if index <= 0 {
		return false
	}
	if !s.resources.Available() {
		return true
	}
	for _, id := range s.resources.IDs("Chat_infos") {
		raw, ok := s.resources.Get("Chat_infos", id)
		if !ok {
			continue
		}
		var info struct {
			ChatID   int32 `json:"ChatID"`
			ChatType []struct {
				Value int32 `json:"value"`
			} `json:"chatType"`
		}
		if json.Unmarshal(raw, &info) != nil || info.ChatID != index {
			continue
		}
		for _, chatType := range info.ChatType {
			if chatType.Value == 1 { // ChatType_Room, also used by match-team quick chat.
				return true
			}
		}
	}
	return false
}

func parseChatInt(value string) (int64, bool) {
	parsed, err := strconv.ParseInt(strings.TrimSpace(value), 10, 64)
	return parsed, err == nil
}

func (s *Server) resourceIDExists(table string, id int64) bool {
	if id <= 0 {
		return false
	}
	if !s.resources.Available() {
		return true
	}
	_, found := s.resources.Get(table, id)
	return found
}

func (s *Server) validBattleChatPayload(message string, room store.Room) bool {
	parts := strings.Split(message, ",")
	if len(parts) < 2 {
		return false
	}
	typeID, ok := parseChatInt(parts[0])
	if !ok {
		return false
	}
	value := func(index int) (int64, bool) {
		if index >= len(parts) {
			return 0, false
		}
		return parseChatInt(parts[index])
	}
	resourceExists := func(table string, id int64) bool {
		return s.resourceIDExists(table, id)
	}
	switch typeID {
	case 3: // LANDMARK: type, chat mark, land ID.
		if len(parts) != 3 {
			return false
		}
		markID, markOK := value(1)
		landID, landOK := value(2)
		if !markOK || !landOK || !resourceExists("Chat_marks", markID) || landID <= 0 {
			return false
		}
		if graph, found := s.resources.BoardGraphForMap(int64(room.MapID), room.MapIndex); found && s.resources.Available() {
			for _, node := range graph.Nodes {
				if int64(node.ID) == landID {
					return true
				}
			}
			return false
		}
		return true
	case 4: // RELICINFO: type, relic ID.
		if len(parts) != 2 {
			return false
		}
		relicID, valid := value(1)
		return valid && resourceExists("Relic_infos", relicID)
	case 5: // PRAISE: type, target player ID.
		if len(parts) != 2 {
			return false
		}
		targetID, valid := value(1)
		return valid && targetID > 0 && hasRoomPlayer(room, targetID)
	case 6: // PVEEVENT: type, chat mark, progress, event card IDs joined by '|'.
		if len(parts) != 4 {
			return false
		}
		markID, markOK := value(1)
		_, progressOK := value(2)
		if !markOK || !progressOK || !resourceExists("Chat_marks", markID) {
			return false
		}
		if strings.TrimSpace(parts[3]) == "" {
			return true
		}
		for _, rawID := range strings.Split(strings.TrimSpace(parts[3]), "|") {
			cardID, cardOK := parseChatInt(rawID)
			if !cardOK || !resourceExists("MapEvent_mapEventCards", cardID) {
				return false
			}
		}
		return true
	case 7: // PVETASK: type, task ID.
		if len(parts) != 2 {
			return false
		}
		taskID, valid := value(1)
		return valid && taskID > 0
	case 8: // CARD: type, card ID.
		if len(parts) != 2 {
			return false
		}
		cardID, valid := value(1)
		return valid && resourceExists("Card_infos", cardID)
	case 9: // SKILL: type, cooldown.
		if len(parts) != 2 {
			return false
		}
		cooldown, valid := value(1)
		return valid && cooldown >= 0
	case 10: // PLAYERMARK: type, target player ID, chat mark.
		if len(parts) != 3 {
			return false
		}
		targetID, targetOK := value(1)
		markID, markOK := value(2)
		return targetOK && targetID > 0 && hasRoomPlayer(room, targetID) && markOK && resourceExists("Chat_marks", markID)
	default:
		return false
	}
}

func (s *Server) handlePlayerChat(ctx context.Context, sess *Session, q *protocolpb.PlayerChatC2S) (DispatchResult, error) {
	_, _, playerID, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	message, valid := normalizeRoomChat(q.Msg)
	if !valid {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	if room.State != 25 || !s.validBattleChatPayload(message, room) {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	recipients, valid := roomChatRecipients(room, playerID, q.PlayerIds)
	if !valid {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if !s.allowChat(playerID) {
		return DispatchResult{Err: ErrRoomChatCd}, nil
	}
	response := &protocolpb.PlayerChatS2C{PlayerId: playerID, Msg: message}
	pushes := []Push{}
	if len(recipients) > 0 {
		pushes = append(pushes, s.pushFor("PlayerChatS2C", response, recipients, 0))
	}
	s.log.Info("room chat delivered", "room_id", roomID, "player_id", playerID, "recipients", len(recipients), "message_runes", utf8.RuneCountInString(message))
	return DispatchResult{Message: response, Pushes: pushes}, nil
}

func (s *Server) handleChatMapMarkers(ctx context.Context, sess *Session, q *protocolpb.ChatMapMarkersC2S) (DispatchResult, error) {
	_, _, playerID, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	message, valid := normalizeRoomChat(q.Msg)
	if !valid {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	if room.State != 25 || !s.validBattleChatPayload(message, room) || !s.resourceIDExists("Chat_marks", int64(q.MarkId)) {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if !s.allowChat(playerID) {
		return DispatchResult{Err: ErrRoomChatCd}, nil
	}
	response := &protocolpb.ChatMapMarkersS2C{PlayerId: playerID, Msg: message}
	recipients := make([]int64, 0, len(room.Players)-1)
	for _, player := range room.Players {
		if player.ID != playerID {
			recipients = append(recipients, player.ID)
		}
	}
	pushes := []Push{}
	if len(recipients) > 0 {
		pushes = append(pushes, s.pushFor("ChatMapMarkersS2C", response, recipients, 0))
	}
	s.log.Info("battle map chat delivered", "room_id", roomID, "player_id", playerID, "mark_id", q.MarkId, "recipients", len(recipients))
	return DispatchResult{Message: response, Pushes: pushes}, nil
}

func (s *Server) handleMatchTeamChat(ctx context.Context, sess *Session, q *protocolpb.MatchTeamChatC2S) (DispatchResult, error) {
	_, _, playerID, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.TeamId <= 0 || !s.validQuickChatIndex(q.Index) {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	team, err := s.store.MatchTeamSnapshot(ctx, q.TeamId)
	if err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if team.State == 3 { // Playing teams no longer expose the Home quick-chat UI.
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	member := false
	recipients := make([]int64, 0, len(team.Players))
	for _, player := range team.Players {
		if player.ID == playerID {
			member = true
			continue
		}
		recipients = append(recipients, player.ID)
	}
	if !member {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if !s.allowChat(playerID) {
		return DispatchResult{Err: ErrRoomChatCd}, nil
	}
	response := &protocolpb.MatchTeamChatS2C{PlayerId: playerID, Index: q.Index}
	pushes := []Push{}
	if len(recipients) > 0 {
		pushes = append(pushes, s.pushFor("MatchTeamChatS2C", response, recipients, 0))
	}
	s.log.Info("match team quick chat delivered", "team_id", q.TeamId, "player_id", playerID, "recipients", len(recipients), "index", q.Index)
	return DispatchResult{Message: response, Pushes: pushes}, nil
}
