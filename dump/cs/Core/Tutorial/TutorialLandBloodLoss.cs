using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine.Scripting;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class TutorialLandBloodLoss : BaseTutorialLand
{
	private readonly int bloodLoss;

	public TutorialLandBloodLoss()
	{
		if (StaticConfigure.Land.InfoDict.TryGetValue(17, out var value))
		{
			bloodLoss = value.Params.GetSafeByIndex(0);
		}
	}

	public override UniTask Pass(int landId, long playerId)
	{
		return UniTask.CompletedTask;
	}

	public override async UniTask Stay(int landId, long playerId)
	{
		HeroAttrEffect hpUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetHpUpdate(playerId, bloodLoss);
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Land,
				Id = landId
			},
			PlayerId = playerId,
			EffectDatas = { hpUpdate }
		});
	}
}
