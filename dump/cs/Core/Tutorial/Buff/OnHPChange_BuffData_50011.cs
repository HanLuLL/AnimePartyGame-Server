using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;
using party.protocol;

namespace Core.Tutorial.Buff;

public class OnHPChange_BuffData_50011 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		int id = buff.Source.Id;
		if (!StaticConfigure.Relic.InfoDict.TryGetValue(id, out var value))
		{
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(buff.PlayerId);
		if (playerDataById == null || playerDataById.CharacterInst == null)
		{
			return;
		}
		HealthState preHealthState = playerDataById.Property.PreHealthState;
		HealthState healthState = playerDataById.Property.HealthState;
		if (preHealthState != healthState)
		{
			int safeByIndex = value.Params.GetSafeByIndex(0);
			HeroAttrEffect heroAttrEffect = null;
			if (healthState == HealthState.Full)
			{
				heroAttrEffect = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetAtkUpdate(buff.PlayerId, safeByIndex);
			}
			else if (preHealthState == HealthState.Full)
			{
				heroAttrEffect = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetAtkUpdate(buff.PlayerId, -safeByIndex);
			}
			if (heroAttrEffect != null)
			{
				await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
				{
					PlayerId = buff.PlayerId,
					Cause = new CauseOrigin
					{
						S = CauseOrigin.Types.source.HeroBuff,
						Id = buff.UniqueId
					},
					EffectDatas = { heroAttrEffect }
				});
			}
		}
	}
}
