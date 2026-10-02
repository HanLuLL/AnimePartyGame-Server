using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class PlayVideoClip : PlayableAsset, IPlayableAsset
{
	public PlayVideoBehaviour template = new PlayVideoBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<PlayVideoBehaviour>.Create(graph, template);
	}
}
