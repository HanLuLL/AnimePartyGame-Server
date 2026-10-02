using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core;

[Serializable]
[TrackClipType(typeof(SceneScreenImpulseClip))]
[DisplayName("Scene/ScreenShake Track")]
public class SceneScreenImpulseTrack : PlayableTrack
{
}
