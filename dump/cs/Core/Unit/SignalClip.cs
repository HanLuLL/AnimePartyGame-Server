using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class SignalClip : PlayableAsset, IPlayableAsset
{
	public SignalBehaviour template = new SignalBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<SignalBehaviour>.Create(graph, template);
	}
}
