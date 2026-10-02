using Cysharp.Threading.Tasks;
using Tools;
using party.model;
using party.protocol;

namespace Core.Tutorial.Buff;

public class OnCreate_BuffData_3020201 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		int id = buff.Source.Id;
		if (StaticConfigure.Event.InfoDict.TryGetValue(id, out var value))
		{
			int safeByIndex = value.Params.GetSafeByIndex(0);
			HeroAttrEffect atkUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetAtkUpdate(buff.PlayerId, safeByIndex);
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				PlayerId = buff.PlayerId,
				Cause = new CauseOrigin
				{
					S = CauseOrigin.Types.source.Card,
					Id = buff.Source.Id
				},
				EffectDatas = { atkUpdate }
			});
		}
	}
}
