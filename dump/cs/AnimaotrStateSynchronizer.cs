using System.Collections.Generic;
using UnityEngine;

public class AnimaotrStateSynchronizer : MonoBehaviour
{
	[SerializeField]
	private int tagId;

	[SerializeField]
	private Animator animator;

	private static Dictionary<int, List<AnimaotrStateSynchronizer>> synchronizers = new Dictionary<int, List<AnimaotrStateSynchronizer>>();

	private static Dictionary<int, AnimaotrStateSynchronizer> mainSynchronizers = new Dictionary<int, AnimaotrStateSynchronizer>();

	public void SynchronizeAnimatorState()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		AnimaotrStateSynchronizer animaotrStateSynchronizer = mainSynchronizers[tagId];
		if ((Object)(object)animaotrStateSynchronizer.animator != null && (Object)(object)animator != null)
		{
			AnimatorStateInfo currentAnimatorStateInfo = animaotrStateSynchronizer.animator.GetCurrentAnimatorStateInfo(0);
			animator.Play(((AnimatorStateInfo)(ref currentAnimatorStateInfo)).fullPathHash, 0, ((AnimatorStateInfo)(ref currentAnimatorStateInfo)).normalizedTime);
		}
	}

	private void OnEnable()
	{
		if (!synchronizers.TryGetValue(tagId, out var value))
		{
			value = new List<AnimaotrStateSynchronizer>();
			synchronizers.Add(tagId, value);
		}
		if (value.Count == 0)
		{
			mainSynchronizers.Add(tagId, this);
		}
		else
		{
			SynchronizeAnimatorState();
		}
		value.Add(this);
	}

	private void OnDisable()
	{
		List<AnimaotrStateSynchronizer> list = synchronizers[tagId];
		AnimaotrStateSynchronizer animaotrStateSynchronizer = mainSynchronizers[tagId];
		list.Remove(this);
		if (animaotrStateSynchronizer == this)
		{
			mainSynchronizers.Remove(tagId);
			if (list.Count > 0)
			{
				mainSynchronizers[tagId] = list[0];
			}
		}
	}
}
