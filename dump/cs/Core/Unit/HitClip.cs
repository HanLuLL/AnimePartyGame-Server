using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class HitClip : PlayableAsset, IPlayableAsset
{
	public HitBehaviour template = new HitBehaviour();

	[HideInInspector]
	public TimelineClip clip;

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		template.StartTime = clip.start;
		return ScriptPlayable<HitBehaviour>.Create(graph, template);
	}
}
