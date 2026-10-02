using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(BattleShowScreenImpulseClip))]
[DisplayName("BattleShow/ScreenShake Track")]
public class BattleShowScreenImpulseTrack : PlayableTrack
{
}
