using System.ComponentModel;
using UnityEngine.Timeline;

namespace SinglePlayer;

[TrackClipType(typeof(BulletClip))]
[TrackColor(0.1f, 0.6f, 0.4f)]
[DisplayName("单人玩法/子弹轨道")]
public class BulletTrack : TrackAsset
{
}
