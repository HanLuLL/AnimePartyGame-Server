using UnityEngine;

namespace Core;

public class CharacterAnimationSwitch : StateMachineBehaviour
{
	[SerializeField]
	public int LoopTime;

	[SerializeField]
	public string TriggerName;

	private int loopCount;

	private float lastNormalizedTime;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		loopCount = 0;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		float normalizedTime = ((AnimatorStateInfo)(ref stateInfo)).normalizedTime;
		if ((int)normalizedTime > (int)lastNormalizedTime)
		{
			loopCount++;
			if (loopCount >= LoopTime && !string.IsNullOrEmpty(TriggerName))
			{
				animator.SetTrigger(TriggerName);
				loopCount = 0;
			}
		}
		lastNormalizedTime = normalizedTime;
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		loopCount = 0;
	}
}
