using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine.Scripting;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class TutorialLandDrawCard : BaseTutorialLand
{
	public override UniTask Pass(int landId, long playerId)
	{
		return UniTask.CompletedTask;
	}

	public override async UniTask Stay(int landId, long playerId)
	{
		int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
		List<HandCardData> collection = TutorialGame.GetSystem<TutorialBoardManager>().cardManager.TryGetCards(2);
		if (round == 2 && SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001)
		{
			collection = new List<HandCardData>
			{
				HandCardData.GetTutorialHandCardData(21004),
				HandCardData.GetTutorialHandCardData(20014)
			};
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		playerDataById.cardContainer._HandCards.AddRange(collection);
		HeroAttrEffect cardUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCardUpdate(playerId, playerDataById.cardContainer._CardInfos);
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Land,
				Id = landId
			},
			PlayerId = playerId,
			EffectDatas = { cardUpdate }
		});
	}
}
