using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class ActorSortingOrderClip : PlayableAsset, IPlayableAsset
{
	public ActorSortingOrderBehaviour template = new ActorSortingOrderBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<ActorSortingOrderBehaviour>.Create(graph, template);
	}
}
