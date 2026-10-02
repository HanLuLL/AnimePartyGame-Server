using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(KillShakeClip))]
[DisplayName("BattleShow/KillShake Track")]
public class KillShakeTrack : PlayableTrack
{
}
