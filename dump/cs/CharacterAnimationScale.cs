using Core.Unit;
using UnityEngine;

public class CharacterAnimationScale : StateMachineBehaviour
{
	[SerializeField]
	public float spriteScale;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((StateMachineBehaviour)this).OnStateEnter(animator, stateInfo, layerIndex);
		CharacterAnimator component = ((Component)(object)animator).GetComponent<CharacterAnimator>();
		component.UpdateAnimationObjectScale(component.defaultAnimationScale * spriteScale);
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((StateMachineBehaviour)this).OnStateExit(animator, stateInfo, layerIndex);
		CharacterAnimator component = ((Component)(object)animator).GetComponent<CharacterAnimator>();
		component.UpdateAnimationObjectScale(component.defaultAnimationScale);
	}
}
