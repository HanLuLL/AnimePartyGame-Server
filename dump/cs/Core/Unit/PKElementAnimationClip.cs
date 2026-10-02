using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class PKElementAnimationClip : PlayableAsset, IPlayableAsset
{
	public PKElementAnimationBehaviour template = new PKElementAnimationBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<PKElementAnimationBehaviour>.Create(graph, template);
	}
}
