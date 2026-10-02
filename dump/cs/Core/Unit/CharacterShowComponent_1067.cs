using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;

namespace Core.Unit;

public class CharacterShowComponent_1067 : CharacterShowComponent
{
	protected override async UniTask Dead()
	{
		Owner.ResetStep(0);
		Owner.characterAnimator.grayed = true;
		ReleaseDeadEffect();
		await Owner.characterAnimator.Die(die: true);
		DeadEffect = await Owner.PlayCharacterEffect(32);
	}

	protected override async UniTask Resurrection()
	{
		await base.Resurrection();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(Owner, willMove: false, forceDeploy: true);
	}
}
