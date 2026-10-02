using Core;
using Core.Unit;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

public class CharacterAnimationAudio : StateMachineBehaviour
{
	[SerializeField]
	public int audioEventID;

	[SerializeField]
	public bool Debut;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((StateMachineBehaviour)this).OnStateEnter(animator, stateInfo, layerIndex);
		if (audioEventID != 0 && !Debut)
		{
			Stage.inst.PlayOneShotSound(audioEventID);
			return;
		}
		CharacterAnimator component = ((Component)(object)animator).gameObject.GetComponent<CharacterAnimator>();
		if (component != null)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.SHOW, component.owner.player.Id);
		}
	}
}
