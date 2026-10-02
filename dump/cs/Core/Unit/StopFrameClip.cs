using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class StopFrameClip : PlayableAsset, IPlayableAsset
{
	public StopFrameBehaviour template = new StopFrameBehaviour();

	[HideInInspector]
	public TimelineClip clip;

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		template.StartTime = ((clip != null) ? clip.start : 0.0);
		return ScriptPlayable<StopFrameBehaviour>.Create(graph, template);
	}
}
