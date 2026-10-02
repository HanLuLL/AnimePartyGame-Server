using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;
using party.protocol;

namespace Core.Tutorial.Buff;

public class OnCreate_BuffData_5000701 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		int id = buff.Source.Id;
		if (StaticConfigure.Relic.InfoDict.TryGetValue(id, out var value))
		{
			int safeByIndex = value.Params.GetSafeByIndex(0);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(buff.PlayerId).player.Property.maxHP += safeByIndex;
			HeroAttrEffect hpUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetHpUpdate(buff.PlayerId, safeByIndex);
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				PlayerId = buff.PlayerId,
				Cause = new CauseOrigin
				{
					S = CauseOrigin.Types.source.HeroBuff,
					Id = buff.UniqueId
				},
				EffectDatas = { hpUpdate }
			});
		}
	}
}
