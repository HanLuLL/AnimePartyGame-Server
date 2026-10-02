using System;
using System.ComponentModel;
using UnityEngine.Timeline;

namespace Core.Unit;

[Serializable]
[TrackClipType(typeof(SignalClip))]
[DisplayName("Signal/CustomSignal Track")]
public class SignalTrack : PlayableTrack
{
}
