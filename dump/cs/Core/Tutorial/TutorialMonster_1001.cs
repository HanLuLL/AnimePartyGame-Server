using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Scripting;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class TutorialMonster_1001 : TutorialBaseMonster
{
	private const int _targetGoldCount = 4;

	private const int _SkillId = 100111;

	public override async UniTask StartEncounterEvent(long playerId, long target)
	{
		int value = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(target).Property.gold.Value;
		int goldCount = Mathf.Min(4, value);
		HeroAttrEffect goldUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetGoldUpdate(target, -goldCount);
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Unknown,
				Id = 100111L
			},
			PlayerId = target,
			EffectDatas = { goldUpdate }
		});
		HeroAttrEffect goldUpdate2 = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetGoldUpdate(playerId, goldCount);
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Unknown,
				Id = 100111L
			},
			PlayerId = playerId,
			EffectDatas = { goldUpdate2 }
		});
	}
}
