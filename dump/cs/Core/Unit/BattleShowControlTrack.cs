using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(BattleShowControlClip))]
[DisplayName("BattleShow/BattleShow Control Track")]
public class BattleShowControlTrack : PlayableTrack
{
}
