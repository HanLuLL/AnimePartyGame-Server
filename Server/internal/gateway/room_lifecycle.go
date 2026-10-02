package gateway

import (
	"context"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

func (s *Server) handleSearchRoom(ctx context.Context, q *protocolpb.SearchRoomC2S) (DispatchResult, error) {
	r, err := s.store.RoomSnapshot(ctx, q.RoomId)
	if err != nil {
		return DispatchResult{Message: &protocolpb.SearchRoomS2C{RoomId: q.RoomId}, Err: ErrRoomNotExist}, nil
	}
	return DispatchResult{Message: &protocolpb.SearchRoomS2C{
		RoomId: r.ID, IsPwd: r.Password != "", RoomServerId: 1,
	}}, nil
}

func (s *Server) handleRoomKickPlayer(ctx context.Context, sess *Session, q *protocolpb.RoomKickPlayerC2S) (DispatchResult, error) {
	_, actorID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, actorID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	room, dissolved, err := s.store.KickRoomPlayer(ctx, roomID, actorID, q.PlayerId)
	if err != nil {
		return DispatchResult{Err: mapRoomError(err)}, nil
	}
	msg := &protocolpb.RoomKickPlayerS2C{PlayerId: q.PlayerId}
	ids := []int64{q.PlayerId}
	if !dissolved {
		ids = append(ids, memberIDs(room)...)
	}
	push := s.pushFor("RoomKickPlayerS2C", msg, ids, actorID)
	s.log.Info("room player kicked", "room_id", roomID, "actor_player_id", actorID, "target_player_id", q.PlayerId)
	return DispatchResult{Message: msg, Pushes: []Push{push}}, nil
}

func (s *Server) handleRoomAbdication(ctx context.Context, sess *Session, q *protocolpb.RoomAbdicationC2S) (DispatchResult, error) {
	_, actorID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, actorID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	if err = s.store.TransferRoomMaster(ctx, roomID, actorID, q.PlayerId); err != nil {
		return DispatchResult{Err: mapRoomError(err)}, nil
	}
	msg := &protocolpb.RoomAbdicationS2C{MasterId: q.PlayerId}
	push := s.pushFor("RoomAbdicationS2C", msg, mustMemberIDs(ctx, s.store, roomID), actorID)
	return DispatchResult{Message: msg, Pushes: []Push{push}}, nil
}

func (s *Server) handleChoiceHero(ctx context.Context, sess *Session, q *protocolpb.ChoiceHeroC2S2) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ChoiceHeroS2C2{}, Err: ErrAuth}, nil
	}
	if !s.resources.IsDefaultHero(q.HeroId) {
		return DispatchResult{Message: &protocolpb.ChoiceHeroS2C2{}, Err: ErrRoomHeroNotUse}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Message: &protocolpb.ChoiceHeroS2C2{}, Err: ErrRoomNotExist}, nil
	}
	profile, err := s.store.PveHeroProfile(ctx, playerID, q.HeroId)
	if err != nil {
		return DispatchResult{}, err
	}
	var talentID int32
	if len(profile.Talents) > 0 {
		talentID = profile.Talents[len(profile.Talents)-1]
	}
	if err = s.store.SetRoomHero(ctx, roomID, playerID, q.HeroId, profile.Level, talentID, 0, 0, nil, s.resources.HeroMaxHP(q.HeroId)); err != nil {
		return DispatchResult{Message: &protocolpb.ChoiceHeroS2C2{}, Err: mapRoomError(err)}, nil
	}
	return DispatchResult{Message: &protocolpb.ChoiceHeroS2C2{}}, nil
}

func (s *Server) handleAffirmHero(ctx context.Context, sess *Session, q *protocolpb.AffirmHeroC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.AffirmHeroS2C{}, Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Message: &protocolpb.AffirmHeroS2C{}, Err: ErrRoomNotExist}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{Message: &protocolpb.AffirmHeroS2C{}, Err: ErrRoomNotExist}, nil
	}
	var self *store.Player
	for i := range room.Players {
		if room.Players[i].ID == playerID {
			self = &room.Players[i]
			break
		}
	}
	if self == nil {
		return DispatchResult{Message: &protocolpb.AffirmHeroS2C{}, Err: ErrRoomPlayerNotExist}, nil
	}
	if q.Auto && self.HeroID == 0 {
		if err = s.chooseDefaultHero(ctx, room, playerID, q); err != nil {
			return DispatchResult{Message: &protocolpb.AffirmHeroS2C{}, Err: mapRoomError(err)}, nil
		}
	}
	room, conflict, allConfirmed, err := s.store.ConfirmRoomHero(ctx, roomID, playerID)
	if err != nil {
		return DispatchResult{Message: &protocolpb.AffirmHeroS2C{}, Err: mapRoomError(err)}, nil
	}
	var heroID, adorn int32
	for _, p := range room.Players {
		if p.ID == playerID {
			heroID, adorn = p.HeroID, p.UseAdorn
			break
		}
	}
	msg := &protocolpb.AffirmHeroS2C{HeroId: heroID, HasChoice: conflict, PlayerId: playerID, UseAdorn: adorn}
	if conflict {
		return DispatchResult{Message: msg, Err: ErrSucc}, nil
	}
	ids := memberIDs(room)
	result := DispatchResult{Message: msg, Pushes: []Push{
		s.pushFor("AffirmHeroS2C", msg, ids, playerID),
		s.pushFor("HeroBarBoxChangeS2C", &protocolpb.HeroBarBoxChangeS2C{Box: s.roomMessage(room).Box}, ids, 0),
	}}
	if allConfirmed {
		s.log.Info("room hero selection complete", "room_id", roomID, "players", len(room.Players))
	}
	return result, nil
}

func (s *Server) chooseDefaultHero(ctx context.Context, room store.Room, playerID int64, q *protocolpb.AffirmHeroC2S) error {
	defaults := s.resources.DefaultHeroIDs()
	used := make(map[int32]bool)
	for _, p := range room.Players {
		if p.ID != playerID && p.HeroConfirmed {
			used[p.HeroID] = true
		}
	}
	var chosen int32
	var selected *modelpb.DefaultHeroInfo
	for _, heroID := range defaults {
		if info, ok := q.DefaultHeroInfos[heroID]; ok && info != nil && !used[heroID] {
			chosen, selected = heroID, info
			break
		}
	}
	if chosen == 0 {
		for _, heroID := range defaults {
			if !used[heroID] {
				chosen = heroID
				break
			}
		}
	}
	if chosen == 0 {
		return errNoDefaultHero
	}
	if selected != nil && selected.HeroId > 0 && selected.HeroId != chosen {
		selected = nil
	}
	_ = selected // account progression is authoritative; the request map only provides a roster hint.
	profile, err := s.store.PveHeroProfile(ctx, playerID, chosen)
	if err != nil {
		return err
	}
	var talentID int32
	if len(profile.Talents) > 0 {
		talentID = profile.Talents[len(profile.Talents)-1]
	}
	return s.store.SetRoomHero(ctx, room.ID, playerID, chosen, profile.Level, talentID, 0, 0, nil, s.resources.HeroMaxHP(chosen))
}

func (s *Server) handleChooseSkin(ctx context.Context, sess *Session, q *protocolpb.ChooseSkinC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ChooseSkinS2C{}, Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Message: &protocolpb.ChooseSkinS2C{}, Err: ErrRoomNotExist}, nil
	}
	if q.UseAdorn > 0 {
		if !s.resources.IsHeroSkin(q.HeroId, q.UseAdorn) {
			return DispatchResult{Message: &protocolpb.ChooseSkinS2C{}, Err: ErrRoomChooseSkinNotExist}, nil
		}
		if !s.resources.IsDefaultHeroSkin(q.HeroId, q.UseAdorn) {
			owned, e := s.store.HasInventoryItem(ctx, playerID, q.UseAdorn)
			if e != nil {
				return DispatchResult{}, e
			}
			if !owned {
				return DispatchResult{Message: &protocolpb.ChooseSkinS2C{}, Err: ErrRoomChooseSkinNotOwn}, nil
			}
		}
	}
	if q.SkinPendant != 0 {
		owned, e := s.store.HasInventoryItem(ctx, playerID, q.SkinPendant)
		if e != nil {
			return DispatchResult{}, e
		}
		if !owned {
			return DispatchResult{Message: &protocolpb.ChooseSkinS2C{}, Err: ErrRoomChooseSkinNotOwn}, nil
		}
	}
	room, allConfirmed, err := s.store.ChooseRoomSkin(ctx, roomID, playerID, q.HeroId, q.UseAdorn, q.SkinPendant, q.Affirmed)
	if err != nil {
		return DispatchResult{Message: &protocolpb.ChooseSkinS2C{}, Err: mapRoomError(err)}, nil
	}
	msg := &protocolpb.ChooseSkinS2C{PlayerId: playerID, UseAdorn: q.UseAdorn, Affirmed: q.Affirmed, HeroId: q.HeroId}
	ids := memberIDs(room)
	result := DispatchResult{Message: msg, Pushes: []Push{s.pushFor("ChooseSkinS2C", msg, ids, playerID)}}
	if allConfirmed {
		result.Pushes = append(result.Pushes, s.pushFor("RoomNotifyS2C", &protocolpb.RoomNotifyS2C{Room: s.roomMessage(room)}, ids, 0))
		s.log.Info("room skin selection complete; battle assets may load", "room_id", roomID, "players", len(room.Players))
	}
	return result, nil
}

func memberIDs(room store.Room) []int64 {
	ids := make([]int64, 0, len(room.Players))
	for _, p := range room.Players {
		ids = append(ids, p.ID)
	}
	return ids
}

var errNoDefaultHero = roomFlowError("no starter hero configured")

type roomFlowError string

func (e roomFlowError) Error() string { return string(e) }
