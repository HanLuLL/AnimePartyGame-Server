using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(PKElementAnimationClip))]
[DisplayName("BattleShow/BattleAnimationClip")]
public class PKElementAnimationTrack : PlayableTrack
{
}
