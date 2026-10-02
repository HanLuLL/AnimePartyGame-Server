using System;
using SinglePlayer.GamePlay;
using UnityEngine;
using UnityEngine.Playables;

namespace SinglePlayer;

[Serializable]
public class EffectBehaviour : PlayableBehaviour
{
	[SerializeField]
	private int _effectId;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		PlayableDirector playableDirector = playable.GetPlayableDirector();
		if (_effectId <= 0)
		{
			Debug.LogError($"特效不存在：{_effectId}");
		}
		else
		{
			((Component)(object)playableDirector).GetComponent<IPlayEffect>()?.PlayEffect(_effectId);
		}
	}
}
