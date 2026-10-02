using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class WwiseAudioClip : PlayableAsset, ITimelineClipAsset
{
	public WwiseAudioBehaviour template = new WwiseAudioBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<WwiseAudioBehaviour>.Create(graph, template);
	}
}
