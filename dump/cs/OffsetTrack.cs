using System;
using System.ComponentModel;
using Core.Unit;
using UnityEngine.Timeline;

[Serializable]
[TrackClipType(typeof(OffsetClip))]
[TrackBindingType(typeof(BattleActor))]
[DisplayName("BattleShow/Offset Track")]
public class OffsetTrack : PlayableTrack
{
}
