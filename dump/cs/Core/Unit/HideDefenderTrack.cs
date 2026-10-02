using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(HideDefenderClip))]
[DisplayName("BattleShow/HideDefender Track")]
public class HideDefenderTrack : PlayableTrack
{
}
