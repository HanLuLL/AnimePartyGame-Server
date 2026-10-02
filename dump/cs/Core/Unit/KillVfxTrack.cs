using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(KillVfxClip))]
[DisplayName("BattleShow/KillVfx Track")]
public class KillVfxTrack : PlayableTrack
{
}
