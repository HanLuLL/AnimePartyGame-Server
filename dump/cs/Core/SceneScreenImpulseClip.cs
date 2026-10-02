using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core;

[Serializable]
public class SceneScreenImpulseClip : PlayableAsset, IPlayableAsset
{
	public SceneScreenImpulseBehaviour template = new SceneScreenImpulseBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<SceneScreenImpulseBehaviour>.Create(graph, template);
	}
}
