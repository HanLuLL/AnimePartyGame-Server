using System.Collections.Generic;
using Core.Net;
using Core.Tutorial.Tools;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine.Scripting;
using party.model;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class TutorialLandPveshop : BaseTutorialLand
{
	private CtsInfo _cts;

	public override async UniTask Pass(int landId, long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			CreatePVEShopData(playerId).Forget();
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001)
			{
				await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowSystemInfo(120, 1f);
			}
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_cts);
		}
	}

	private async UniTask CreatePVEShopData(long playerId)
	{
		_cts = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
		LandLogic land = SimpleSingletonProvider<GameLogicManager>.inst.land;
		List<int> list = TutorialGame.GetSystem<TutorialBoardManager>().cardManager.TryGetCards(3).ConvertAll((HandCardData card) => card.CardId);
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round == 2)
		{
			list = new List<int> { 21001, 10006, 21006 };
		}
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001 && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round == 2)
		{
			list = new List<int> { 21013, 20008, 10005 };
		}
		land.CardShopAction = new Action
		{
			PlayerId = playerId,
			Sn = UIDGenerator.NextUID()
		};
		land.PVEShopData = new PVEShopBuyC2S
		{
			Cards = { (IEnumerable<int>)list },
			Gold = 3
		};
		list.ForEach(delegate
		{
			land.PVEShopData.Alreadys.Add(item: false);
		});
		await SimpleSingletonProvider<UIManager>.inst.landShop.OpenPVECardShop();
	}

	public override async UniTask Stay(int landId, long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		List<HandCardData> collection = TutorialGame.GetSystem<TutorialBoardManager>().cardManager.TryGetCards(1);
		playerDataById.cardContainer._HandCards.AddRange(collection);
		HeroAttrEffect cardUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCardUpdate(playerId, playerDataById.cardContainer._CardInfos);
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.ShopOpen,
				Id = landId
			},
			PlayerId = playerId,
			EffectDatas = { cardUpdate }
		});
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			CreatePVEShopData(playerId).Forget();
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_cts);
		}
	}

	public async void RequestPVEShopBuyC2S(long sn, List<int> indexList, long assistPlayer, bool isClose)
	{
		LandLogic land = SimpleSingletonProvider<GameLogicManager>.inst.land;
		long playerId = land.CardShopAction.PlayerId;
		List<HandCardData> buyCards = new List<HandCardData>();
		indexList.ForEach(delegate(int index)
		{
			if (land.PVEShopData.Alreadys.Count > index)
			{
				land.PVEShopData.Alreadys[index] = true;
				int safeByIndex = land.PVEShopData.Cards.GetSafeByIndex(index);
				land.PVEShopData.BuyCards.Add(safeByIndex);
				buyCards.Add(HandCardData.GetTutorialHandCardData(safeByIndex));
			}
		});
		await MonoSingletonProvider<NetManager>.inst.RPC.PVEShopBuyS2C.OnPVEShopBuyS2CServerCallBackAsync(new PVEShopBuyS2C
		{
			PlayerId = playerId,
			IsClose = isClose,
			BuyCards = { (IEnumerable<int>)land.PVEShopData.BuyCards }
		}, 0, isDispatch: true);
		if (isClose)
		{
			_cts?.Cancel();
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		int id = playerDataById.CharacterInst.standLand.Id;
		int changeGold = -indexList.Count * 3;
		HeroAttrEffect goldUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetGoldUpdate(playerId, changeGold);
		playerDataById.cardContainer._HandCards.AddRange(buyCards);
		HeroAttrEffect cardUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCardUpdate(playerId, playerDataById.cardContainer._CardInfos);
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.ShopBuy,
				Id = id
			},
			PlayerId = playerId,
			EffectDatas = { goldUpdate, cardUpdate }
		});
		await SimpleSingletonProvider<UIManager>.inst.landShop.OpenPVECardShop();
	}
}
