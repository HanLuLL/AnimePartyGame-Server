using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine.Scripting;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class TutorialLandHeal : BaseTutorialLand
{
	private readonly int healHp;

	public TutorialLandHeal()
	{
		if (StaticConfigure.Land.InfoDict.TryGetValue(20, out var value))
		{
			healHp = value.Params.GetSafeByIndex(0);
		}
	}

	public override UniTask Pass(int landId, long playerId)
	{
		return UniTask.CompletedTask;
	}

	public override async UniTask Stay(int landId, long playerId)
	{
		HeroAttrEffect hpUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetHpUpdate(playerId, healHp);
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
