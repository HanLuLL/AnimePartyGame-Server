using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class PlayBGVideoClip : PlayableAsset, IPlayableAsset
{
	public PlayBGVideoBehaviour template = new PlayBGVideoBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<PlayBGVideoBehaviour>.Create(graph, template);
	}
}
