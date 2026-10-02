using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class KillVfxClip : PlayableAsset, IPlayableAsset
{
	public KillVfxBehaviour template = new KillVfxBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<KillVfxBehaviour>.Create(graph, template);
	}
}
