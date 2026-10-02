using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit.BattleShow;

[Serializable]
[TrackClipType(typeof(PKTempCameraClip))]
[DisplayName("BattleShow/PKTempCamera Track")]
public class PKTempCameraTrack : PlayableTrack
{
}
