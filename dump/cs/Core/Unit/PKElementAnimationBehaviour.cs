using System;
using Core.Scene;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class PKElementAnimationBehaviour : PlayableBehaviour
{
	[SerializeField]
	private string _targetKey;

	[SerializeField]
	private string _animationState;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if (string.IsNullOrEmpty(_targetKey))
		{
			Debug.LogError("请输入一个有效的物体名称");
			return;
		}
		if (string.IsNullOrEmpty(_animationState))
		{
			Debug.LogError("请输入一个有效的状态名称");
			return;
		}
		BattleShowDirector battleShowDirector = BattleSceneController.inst?.directorManager;
		if (battleShowDirector == null)
		{
			return;
		}
		PKElementAnimationTarget[] componentsInChildren = battleShowDirector.GetComponentsInChildren<PKElementAnimationTarget>();
		foreach (PKElementAnimationTarget pKElementAnimationTarget in componentsInChildren)
		{
			if (pKElementAnimationTarget.Key.Equals(_targetKey))
			{
				pKElementAnimationTarget.CrossFade(_animationState, 0.1f);
			}
		}
		base.OnBehaviourPlay(playable, info);
	}
}
