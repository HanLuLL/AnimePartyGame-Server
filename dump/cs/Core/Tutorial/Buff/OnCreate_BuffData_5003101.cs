using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;
using party.protocol;

namespace Core.Tutorial.Buff;

public class OnCreate_BuffData_5003101 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		int id = buff.Source.Id;
		if (StaticConfigure.Relic.InfoDict.TryGetValue(id, out var value))
		{
			int safeByIndex = value.Params.GetSafeByIndex(0);
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(buff.PlayerId);
			if (playerDataById.cardContainer._HandCards.Count < safeByIndex)
			{
				int safeByIndex2 = value.Params.GetSafeByIndex(1);
				List<HandCardData> collection = TutorialGame.GetSystem<TutorialBoardManager>().cardManager.TryGetCards(safeByIndex2);
				playerDataById.cardContainer._HandCards.AddRange(collection);
				HeroAttrEffect cardUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCardUpdate(buff.PlayerId, playerDataById.cardContainer._CardInfos);
				await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
				{
					PlayerId = buff.PlayerId,
					Cause = new CauseOrigin
					{
						S = CauseOrigin.Types.source.HeroBuff,
						Id = buff.UniqueId
					},
					EffectDatas = { cardUpdate }
				});
			}
		}
	}
}
