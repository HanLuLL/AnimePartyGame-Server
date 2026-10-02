using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;
using party.protocol;

namespace Core.Tutorial.Buff;

public class OnCreate_BuffData_5002801 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		int id = buff.Source.Id;
		if (StaticConfigure.Relic.InfoDict.TryGetValue(id, out var value))
		{
			int safeByIndex = value.Params.GetSafeByIndex(0);
			int value2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(buff.PlayerId).Property.CardMaxVailUseCount.Value;
			HeroAttrEffect item = new HeroAttrEffect
			{
				PlayerId = buff.PlayerId,
				UseCardNum = new HeroUseCardNumChangeS2C
				{
					PlayerId = buff.PlayerId,
					UseCardMaxNum = value2 + safeByIndex
				}
			};
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				PlayerId = buff.PlayerId,
				Cause = new CauseOrigin
				{
					S = CauseOrigin.Types.source.HeroBuff,
					Id = buff.UniqueId
				},
				EffectDatas = { item }
			});
		}
	}
}
