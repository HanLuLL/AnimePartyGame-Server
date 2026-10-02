using Core.Net;
using Core.Tutorial.Tools;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Scripting;
using party.model;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class TutorialLandFillingStation : BaseTutorialLand
{
	private readonly UpgradeDataConfigure _upgradeInfo;

	private CtsInfo _cts;

	public TutorialLandFillingStation()
	{
		if (!StaticConfigure.Upgrade.DataDict.TryGetValue(5, out var value))
		{
			Debug.LogError("无法在Upgrade中获取5对应的PVE升级配置");
		}
		_upgradeInfo = value;
	}

	public override async UniTask Pass(int landId, long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			_cts = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
			SimpleSingletonProvider<UIManager>.inst.landFillingStation.DealLand_StopOrContinue(new Action
			{
				Sn = UIDGenerator.NextUID(),
				PlayerId = playerId
			});
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_cts);
		}
	}

	public override async UniTask Stay(int landId, long playerId)
	{
		if (_upgradeInfo == null)
		{
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		HeroAttrEffect hpUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetHpUpdate(playerId, 2);
		int value = playerDataById.Property.level.Value;
		int value2 = playerDataById.Property.gold.Value;
		UpdateHeroAttrS2C updateHeroAttrS2C = new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Land,
				Id = landId
			},
			PlayerId = playerId,
			EffectDatas = { hpUpdate }
		};
		if (value < _upgradeInfo.UpgradeDataConfigureItems.Count)
		{
			UpgradeDataConfigureItem safeByIndex = _upgradeInfo.UpgradeDataConfigureItems.GetSafeByIndex(value);
			if (safeByIndex != null && value2 >= safeByIndex.Gold)
			{
				HeroAttrEffect goldUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetGoldUpdate(playerId, -safeByIndex.Gold);
				HeroAttrEffect lVUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetLVUpdate(playerId, safeByIndex.Star);
				updateHeroAttrS2C.EffectDatas.Add(goldUpdate);
				updateHeroAttrS2C.EffectDatas.Add(lVUpdate);
			}
		}
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(updateHeroAttrS2C);
	}

	public async UniTask RequestStopOrContinueC2S(long playerId, bool stop)
	{
		if (stop)
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.StopMove();
		}
		await MonoSingletonProvider<NetManager>.inst.RPC.StopOrContinueS2C.OnStopOrContinueS2CServerCallBackAsync(new StopOrContinueS2C
		{
			PlayerId = playerId,
			Stop = stop
		}, 0, isDispatch: true);
		_cts?.Cancel();
	}
}
