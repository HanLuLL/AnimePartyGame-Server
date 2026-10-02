using Cysharp.Threading.Tasks;
using Tools;
using party.model;
using party.protocol;

namespace Core.Tutorial.Buff;

public class OnPkDefendEnd_BuffData_5004001 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		int id = buff.Source.Id;
		if (StaticConfigure.Relic.InfoDict.TryGetValue(id, out var value))
		{
			int safeByIndex = value.Params.GetSafeByIndex(0);
			int safeByIndex2 = value.Params.GetSafeByIndex(1);
			HeroAttrEffect cureUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCureUpdate(buff.PlayerId, safeByIndex);
			HeroAttrEffect goldUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetGoldUpdate(buff.PlayerId, safeByIndex2);
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				PlayerId = buff.PlayerId,
				Cause = new CauseOrigin
				{
					S = CauseOrigin.Types.source.HeroBuff,
					Id = buff.UniqueId
				},
				EffectDatas = { cureUpdate, goldUpdate }
			});
		}
	}
}
