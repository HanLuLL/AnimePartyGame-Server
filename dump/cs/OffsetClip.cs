using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class OffsetClip : PlayableAsset, IPlayableAsset
{
	public OffsetBehaviour template = new OffsetBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<OffsetBehaviour>.Create(graph, template);
	}
}
