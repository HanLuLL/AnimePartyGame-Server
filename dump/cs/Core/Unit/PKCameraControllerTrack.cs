using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(PKCameraControllerClip))]
[DisplayName("BattleShow/PKCameraController Track")]
public class PKCameraControllerTrack : PlayableTrack
{
}
