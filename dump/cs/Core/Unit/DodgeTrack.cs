using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(DodgeClip))]
[DisplayName("BattleShow/Dodge Track")]
public class DodgeTrack : PlayableTrack
{
}
