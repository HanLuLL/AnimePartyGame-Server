using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(PlayBGVideoClip))]
[DisplayName("BattleShow/PlayBGVideo Track")]
public class PlayBGVideoTrack : PlayableTrack
{
}
