using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(PlayVideoClip))]
[DisplayName("BattleShow/PlayVideo Track")]
public class PlayVideoTrack : PlayableTrack
{
}
