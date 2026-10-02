using System;
using Core;
using Tools;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class WwiseAudioBehaviour : PlayableBehaviour
{
	public int playId;

	public int stopId;

	private PlayableDirector _director;

	public override void OnPlayableCreate(Playable playable)
	{
		base.OnPlayableCreate(playable);
		if ((UnityEngine.Object)(object)_director == null)
		{
			_director = playable.GetPlayableDirector();
		}
	}

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if ((UnityEngine.Object)(object)_director == null)
		{
			Debug.LogError("轨道中， 尝试播放音效时刻， 无法获取导播对象");
		}
		else if (playId > 0)
		{
			SimpleSingletonProvider<AudioManager>.inst.SendEvent(playId, ((Component)(object)_director).gameObject);
		}
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		if ((UnityEngine.Object)(object)_director == null)
		{
			Debug.LogError("轨道中， 尝试播放音效时刻， 无法获取导播对象");
		}
		else if (stopId > 0)
		{
			SimpleSingletonProvider<AudioManager>.inst.SendEvent(stopId, ((Component)(object)_director).gameObject);
		}
	}
}
