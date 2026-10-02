using System;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class PlayVideoBehaviour : PlayableBehaviour
{
	[SerializeField]
	public int VideoConfigId;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if (SimpleSingletonProvider<UIManager>.inst.Fight.isShowing)
		{
			string videoKey = VideoConfigId.GetVideoKey();
			SimpleSingletonProvider<UIManager>.inst.Fight.ShowFightVideo(videoKey);
		}
		base.OnBehaviourPlay(playable, info);
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		if (SimpleSingletonProvider<UIManager>.inst.Fight.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.Fight.StopFightVideo();
		}
		base.OnBehaviourPause(playable, info);
	}
}
