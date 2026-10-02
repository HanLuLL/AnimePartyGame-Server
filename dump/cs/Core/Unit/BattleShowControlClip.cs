using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class BattleShowControlClip : PlayableAsset, IPlayableAsset
{
	public BattleShowControlBehaviour template = new BattleShowControlBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<BattleShowControlBehaviour>.Create(graph, template);
	}
}
