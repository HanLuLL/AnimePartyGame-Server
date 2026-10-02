using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class HideDefenderClip : PlayableAsset, IPlayableAsset
{
	public HideDefenderBehaviour template = new HideDefenderBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<HideDefenderBehaviour>.Create(graph, template);
	}
}
