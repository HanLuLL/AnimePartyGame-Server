using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;
using party.protocol;

namespace Core.Tutorial.Buff;

public class OnRemove_BuffData_2000801 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		int id = buff.Source.Id;
		if (StaticConfigure.Card.InfoDict.TryGetValue(id, out var value))
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(buff.PlayerId);
			int safeByIndex = value.Params.GetSafeByIndex(2);
			int safeByIndex2 = value.Params.GetSafeByIndex(3);
			playerDataById.Property.OnHitExtraDamageChange(safeByIndex2);
			HeroAttrEffect atkUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetAtkUpdate(buff.PlayerId, -safeByIndex);
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				PlayerId = buff.PlayerId,
				Cause = new CauseOrigin(),
				EffectDatas = { atkUpdate }
			});
		}
		await UniTask.CompletedTask;
	}
}
