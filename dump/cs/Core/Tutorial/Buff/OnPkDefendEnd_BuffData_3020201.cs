using Cysharp.Threading.Tasks;
using Tools;
using party.model;
using party.protocol;

namespace Core.Tutorial.Buff;

public class OnPkDefendEnd_BuffData_3020201 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		buff.KeepRound--;
		int id = buff.Source.Id;
		if (StaticConfigure.Event.InfoDict.TryGetValue(id, out var value))
		{
			int changeAtk = -value.Params.GetSafeByIndex(0);
			HeroAttrEffect atkUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetAtkUpdate(buff.PlayerId, changeAtk);
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				PlayerId = buff.PlayerId,
				Cause = new CauseOrigin(),
				EffectDatas = { atkUpdate }
			});
		}
	}
}
