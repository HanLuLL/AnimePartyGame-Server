using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(ActorSortingOrderClip))]
[DisplayName("BattleShow/Actor Sorting Order")]
public class ActorSortingOrderTrack : PlayableTrack
{
}
