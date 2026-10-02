using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;

namespace Core.Unit;

public class CharacterShowComponent_1037 : CharacterShowComponent
{
	protected override async UniTask Dead()
	{
		Owner.ResetStep(0);
		Owner.characterAnimator.grayed = true;
		await Owner.characterAnimator.Die(die: true);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.deadDeal.Dispatch(Owner.player.Id);
	}
}
