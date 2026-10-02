using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(FlyVfxClip))]
[DisplayName("BattleShow/FlyVfx Track")]
public class FlyVfxTrack : PlayableTrack
{
}
