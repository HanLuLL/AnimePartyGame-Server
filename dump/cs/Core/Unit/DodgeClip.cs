using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class DodgeClip : PlayableAsset, IPlayableAsset
{
	public DodgeBehaviour template = new DodgeBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<DodgeBehaviour>.Create(graph, template);
	}
}
