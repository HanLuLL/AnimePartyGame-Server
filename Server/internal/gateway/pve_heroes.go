package gateway

import (
	"context"
	"errors"
	"sort"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

func (s *Server) handlePveHeroUpLevel(ctx context.Context, sess *Session, q *protocolpb.PveHeroUpLvC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.PveHeroUpLvS2C{}, Err: ErrAuth}, nil
	}
	if !s.resources.IsDefaultHero(q.DefId) || len(q.UseItems) == 0 || len(q.UseItems) > 32 {
		return DispatchResult{Message: &protocolpb.PveHeroUpLvS2C{DefId: q.DefId}, Err: ErrInvalidParam}, nil
	}
	itemExps := make(map[int32]int32, len(q.UseItems))
	for itemID, count := range q.UseItems {
		if count <= 0 {
			return DispatchResult{Message: &protocolpb.PveHeroUpLvS2C{DefId: q.DefId}, Err: ErrInvalidParam}, nil
		}
		itemExp, found := s.resources.PveItemExp(itemID)
		if !found {
			return DispatchResult{Message: &protocolpb.PveHeroUpLvS2C{DefId: q.DefId}, Err: ErrInvalidParam}, nil
		}
		itemExps[itemID] = itemExp
	}
	profile, counts, err := s.store.UpgradePveHero(ctx, playerID, q.DefId, q.UseItems, itemExps, s.resources.PveLevelExps())
	if errors.Is(err, store.ErrInventoryInsufficient) {
		return DispatchResult{Message: &protocolpb.PveHeroUpLvS2C{DefId: q.DefId}, Err: ErrItemEnough}, nil
	}
	if errors.Is(err, store.ErrPveProgressInvalid) {
		return DispatchResult{Message: &protocolpb.PveHeroUpLvS2C{DefId: q.DefId}, Err: ErrInvalidParam}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	s.log.Info("PVE hero leveled", "player_id", playerID, "hero_id", q.DefId, "level", profile.Level, "exp", profile.Exp)
	push := s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{Item: pveInventoryMessages(counts), IsNotShow: true}, []int64{playerID}, 0)
	return DispatchResult{Message: &protocolpb.PveHeroUpLvS2C{
		DefId: q.DefId, Lv: profile.Level, Exp: profile.Exp, ReturnItems: map[int32]int32{},
	}, Pushes: []Push{push}}, nil
}

func (s *Server) handlePveHeroTalentUp(ctx context.Context, sess *Session, q *protocolpb.PveHeroTalentUpC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.PveHeroTalentUpS2C{}, Err: ErrAuth}, nil
	}
	if !s.resources.IsDefaultHero(q.RoleId) {
		return DispatchResult{Message: &protocolpb.PveHeroTalentUpS2C{RoleId: q.RoleId}, Err: ErrInvalidParam}, nil
	}
	orderedTalents := s.resources.PveTalentIDs(q.RoleId)
	if q.TalentId <= 0 || len(orderedTalents) == 0 || !containsInt32(orderedTalents, q.TalentId) {
		return DispatchResult{Message: &protocolpb.PveHeroTalentUpS2C{RoleId: q.RoleId}, Err: ErrInvalidParam}, nil
	}
	talent, found := s.resources.PveTalent(q.TalentId)
	if !found || len(talent.NeedMaterials) == 0 {
		return DispatchResult{Message: &protocolpb.PveHeroTalentUpS2C{RoleId: q.RoleId}, Err: ErrInvalidParam}, nil
	}
	profile, counts, err := s.store.UnlockPveHeroTalent(ctx, playerID, q.RoleId, q.TalentId,
		talent.UnlockNeedPVELevel, orderedTalents, talent.NeedMaterials)
	if errors.Is(err, store.ErrInventoryInsufficient) {
		return DispatchResult{Message: &protocolpb.PveHeroTalentUpS2C{RoleId: q.RoleId}, Err: ErrItemEnough}, nil
	}
	if errors.Is(err, store.ErrPveTalentLocked) || errors.Is(err, store.ErrPveProgressInvalid) {
		return DispatchResult{Message: &protocolpb.PveHeroTalentUpS2C{RoleId: q.RoleId}, Err: ErrInvalidParam}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	s.log.Info("PVE hero talent unlocked", "player_id", playerID, "hero_id", q.RoleId, "talent_id", q.TalentId)
	push := s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{Item: pveInventoryMessages(counts), IsNotShow: true}, []int64{playerID}, 0)
	return DispatchResult{Message: &protocolpb.PveHeroTalentUpS2C{RoleId: q.RoleId, Talents: profile.Talents},
		Pushes: []Push{push}}, nil
}

func pveInventoryMessages(counts map[int32]int32) []*modelpb.ItemEtc {
	ids := make([]int, 0, len(counts))
	for itemID := range counts {
		ids = append(ids, int(itemID))
	}
	sort.Ints(ids)
	items := make([]*modelpb.ItemEtc, 0, len(ids))
	for _, rawID := range ids {
		itemID := int32(rawID)
		items = append(items, &modelpb.ItemEtc{ItemId: itemID, Count: counts[itemID]})
	}
	return items
}
