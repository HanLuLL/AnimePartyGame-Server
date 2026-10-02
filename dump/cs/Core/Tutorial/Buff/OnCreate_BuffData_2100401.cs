using Cysharp.Threading.Tasks;
using Tools;
using party.model;
using party.protocol;

namespace Core.Tutorial.Buff;

public class OnCreate_BuffData_2100401 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		int id = buff.Source.Id;
		if (StaticConfigure.Card.InfoDict.TryGetValue(id, out var value))
		{
			int safeByIndex = value.Params.GetSafeByIndex(0);
			int safeByIndex2 = value.Params.GetSafeByIndex(1);
			HeroAttrEffect hpUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetHpUpdate(buff.PlayerId, safeByIndex);
			HeroAttrEffect atkUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetAtkUpdate(buff.PlayerId, safeByIndex2);
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				PlayerId = buff.PlayerId,
				Cause = new CauseOrigin
				{
					S = CauseOrigin.Types.source.Card,
					Id = buff.Source.Id
				},
				EffectDatas = { hpUpdate, atkUpdate }
			});
		}
	}
}
