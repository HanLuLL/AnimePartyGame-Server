using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.Scripting;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class TutorialLandRollGold : BaseTutorialLand
{
	public int RandomIndex;

	public override async UniTask Stay(int landId, long playerId)
	{
		if (!StaticConfigure.Land.InfoDict.TryGetValue(15, out var info))
		{
			Debug.LogError("无法获取RollGold格子的配置");
			return;
		}
		if (RandomIndex == 0)
		{
			int count = info.Params.Count;
			RandomIndex = UnityEngine.Random.Range(0, count);
		}
		await MonoSingletonProvider<NetManager>.inst.RPC.RollGoldS2C.OnRollGoldS2CServerCallBackAsync(new RollGoldS2C
		{
			Point = RandomIndex
		}, 0, isDispatch: true);
		int safeByIndex = info.Params.GetSafeByIndex(RandomIndex);
		RandomIndex = 0;
		HeroAttrEffect goldUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetGoldUpdate(playerId, safeByIndex);
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Land,
				Id = landId
			},
			PlayerId = playerId,
			EffectDatas = { goldUpdate }
		});
	}

	public override UniTask Pass(int landId, long playerId)
	{
		return UniTask.CompletedTask;
	}
}
