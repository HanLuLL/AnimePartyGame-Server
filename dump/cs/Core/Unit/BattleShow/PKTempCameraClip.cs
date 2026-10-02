using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit.BattleShow;

[Serializable]
public class PKTempCameraClip : PlayableAsset
{
	public PKTempCameraBehaviour template = new PKTempCameraBehaviour();

	public ClipCaps clipCaps => (ClipCaps)0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<PKTempCameraBehaviour>.Create(graph, template);
	}
}
