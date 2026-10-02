using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(DiceHUDClip))]
[DisplayName("BattleShow/Control HUD")]
public class DiceHUDTrack : PlayableTrack
{
}
