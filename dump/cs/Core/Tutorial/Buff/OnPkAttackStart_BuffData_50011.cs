using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;

namespace Core.Tutorial.Buff;

public class OnPkAttackStart_BuffData_50011 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(buff.PlayerId);
		if (playerDataById != null && !(playerDataById.CharacterInst == null) && playerDataById.Property.HealthState == HealthState.Full)
		{
			int value = playerDataById.Property.CureCount.property.Value;
			(SimpleSingletonProvider<GameLogicManager>.inst.fight?.battleFightData)?.TutorialUpdatePKAtk(buff.PlayerId, value);
			await UniTask.CompletedTask;
		}
	}
}
