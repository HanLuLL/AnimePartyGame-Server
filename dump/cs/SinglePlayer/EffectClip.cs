using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SinglePlayer;

[Serializable]
public class EffectClip : PlayableAsset, ITimelineClipAsset
{
	public EffectBehaviour template = new EffectBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<EffectBehaviour>.Create(graph, template);
	}
}
