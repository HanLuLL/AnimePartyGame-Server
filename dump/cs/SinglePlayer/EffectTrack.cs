using System.ComponentModel;
using UnityEngine.Timeline;

namespace SinglePlayer;

[TrackClipType(typeof(EffectClip))]
[TrackColor(0.1f, 0.6f, 0.4f)]
[DisplayName("单人玩法/特效轨道")]
public class EffectTrack : TrackAsset
{
}
