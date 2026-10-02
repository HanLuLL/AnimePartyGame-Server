using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class BattleShowScreenImpulseClip : PlayableAsset, IPlayableAsset
{
	public BattleShowScreenImpulseBehaviour template = new BattleShowScreenImpulseBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<BattleShowScreenImpulseBehaviour>.Create(graph, template);
	}
}
