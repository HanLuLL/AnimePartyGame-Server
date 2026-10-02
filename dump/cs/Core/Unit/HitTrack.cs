using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(HitClip))]
[DisplayName("BattleShow/Hit Track")]
public class HitTrack : PlayableTrack
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		foreach (TimelineClip clip in ((TrackAsset)this).GetClips())
		{
			HitClip hitClip = clip.asset as HitClip;
			if (hitClip != null)
			{
				hitClip.clip = clip;
			}
		}
		return ((TrackAsset)this).CreateTrackMixer(graph, go, inputCount);
	}
}
