using System;
using Cinemachine;
using UnityEngine;

namespace Core.Camera;

[Serializable]
[CreateAssetMenu(menuName = "相机/抖动配置", fileName = "New CinemachineImpulse.asset", order = 10)]
public class CinemachineImpulseAsset : ScriptableObject
{
	[SerializeField]
	private NoiseSettings _noiseSettings;

	[SerializeField]
	private float _amplitudeGain = 1f;

	[SerializeField]
	private float _frequencyGain = 1f;

	[SerializeField]
	private float _noiseBlendInTime;

	[SerializeField]
	private float _noiseDurationTime;

	[SerializeField]
	private float _noiseBlendOutTime;

	public NoiseSettings noiseSettings => _noiseSettings;

	public float amplitudeGain => _amplitudeGain;

	public float frequencyGain => _frequencyGain;

	public float noiseBlendInTime => _noiseBlendInTime;

	public float noiseDurationTime => _noiseDurationTime;

	public float noiseBlendOutTime => _noiseBlendOutTime;
}
