package gateway

import (
	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

func findGoldDropBuff(player store.Player) (store.BuffState, bool) {
	for _, buff := range player.Buffs {
		if buff.BuffID == 3001801 {
			return buff, true
		}
	}
	return store.BuffState{}, false
}

func (s *Server) goldDropBuffPush(playerID int64, oldGold, newGold int32, buff store.BuffState, recipients []int64) Push {
	effects := make([]*protocolpb.HeroAttrEffect, 0, 2)
	if oldGold != newGold {
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId: playerID, ChangeGold: newGold - oldGold, OriGold: oldGold, CurrGold: newGold,
			}},
		})
	}
	effects = append(effects, &protocolpb.HeroAttrEffect{
		PlayerId: playerID,
		Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
			PlayerId: playerID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Delete,
		}},
	})
	return s.pushFor("UpdateHeroAttrS2C", &protocolpb.UpdateHeroAttrS2C{
		PlayerId:    playerID,
		Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_event, Id: 30018},
		EffectDatas: effects,
	}, recipients, 0)
}
