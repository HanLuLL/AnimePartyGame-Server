using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class KillShakeClip : PlayableAsset, IPlayableAsset
{
	public KillShakeBehaviour template = new KillShakeBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<KillShakeBehaviour>.Create(graph, template);
	}
}
