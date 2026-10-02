using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;
using party.model;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class TutorialLandPortal : BaseTutorialLand
{
	public override UniTask Pass(int landId, long playerId)
	{
		return UniTask.CompletedTask;
	}

	public override async UniTask Stay(int landId, long playerId)
	{
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Land,
				Id = landId
			},
			PlayerId = playerId,
			EffectDatas = 
			{
				new HeroAttrEffect
				{
					Place = new HeroPlaceChangeS2C
					{
						PlayerId = playerId,
						Place = new HeroPlace
						{
							BackNodeId = 9,
							FrontNodeIds = { 49 },
							NodeId = 48
						}
					}
				}
			}
		});
	}
}
