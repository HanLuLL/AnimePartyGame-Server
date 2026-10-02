using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class DiceHUDClip : PlayableAsset, IPlayableAsset
{
	public DiceHUDBehaviour template = new DiceHUDBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<DiceHUDBehaviour>.Create(graph, template);
	}
}
