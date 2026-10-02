using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
public class PKCameraControllerClip : PlayableAsset
{
	public PKCameraControllerBehaviour template = new PKCameraControllerBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<PKCameraControllerBehaviour>.Create(graph, template);
	}
}
