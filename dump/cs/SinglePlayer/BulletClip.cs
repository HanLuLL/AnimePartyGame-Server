using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SinglePlayer;

[Serializable]
public class BulletClip : PlayableAsset, ITimelineClipAsset
{
	public BulletBehaviour template = new BulletBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<BulletBehaviour>.Create(graph, template);
	}
}
