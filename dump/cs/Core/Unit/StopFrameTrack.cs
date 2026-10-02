using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(StopFrameClip))]
[DisplayName("BattleShow/StopFrame Track")]
public class StopFrameTrack : PlayableTrack
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		foreach (TimelineClip clip in ((TrackAsset)this).GetClips())
		{
			StopFrameClip stopFrameClip = clip.asset as StopFrameClip;
			if (stopFrameClip != null)
			{
				stopFrameClip.clip = clip;
			}
		}
		return ((TrackAsset)this).CreateTrackMixer(graph, go, inputCount);
	}
}
