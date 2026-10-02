using System;
using Cinemachine;
using Cinemachine.Utility;
using UnityEngine;

namespace Core.Sprite;

[Serializable]
[CreateAssetMenu(menuName = "角色/受击抖动配置", fileName = "New SpriteShake.asset", order = 1)]
public class SpriteShakeAsset : ScriptableObject
{
	[SerializeField]
	private NoiseSettings _noiseSettings;

	[SerializeField]
	private float _amplitudeGain = 1f;

	[SerializeField]
	private float _frequencyGain = 1f;

	[SerializeField]
	private float _noiseBlendInPercent;

	[SerializeField]
	private float _noiseBlendOutPercent;

	public NoiseSettings noiseSettings => _noiseSettings;

	public float noiseBlendInPercent => _noiseBlendInPercent;

	public float noiseBlendOutPercent => _noiseBlendOutPercent;

	public Vector3 GetPosition(float time, float totalTime)
	{
		if (time >= 0f)
		{
			float num = 0f;
			float num2 = 0f;
			NoiseSettings.TransformNoiseParams[] positionNoise = _noiseSettings.PositionNoise;
			for (int i = 0; i < positionNoise.Length; i++)
			{
				NoiseSettings.TransformNoiseParams transformNoiseParams = positionNoise[i];
				float num3 = num;
				NoiseSettings.NoiseParams x = transformNoiseParams.X;
				num = num3 + x.GetValueAt(time * _frequencyGain, 0f) * _amplitudeGain;
				num *= GetDamp(time, totalTime);
				float num4 = num2;
				x = transformNoiseParams.Y;
				num2 = num4 + x.GetValueAt(time * _frequencyGain, 0f) * _amplitudeGain;
				num2 *= GetDamp(time, totalTime);
			}
			return new Vector3(num, num2, 0f);
		}
		return Vector3.zero;
	}

	private float GetDamp(float time, float totalTime)
	{
		float num = totalTime * _noiseBlendInPercent;
		float num2 = totalTime * _noiseBlendOutPercent;
		float num3 = Mathf.Max(0f, totalTime - num - num2);
		if (time < num && num > 0.0001f)
		{
			return Damper.Damp(1f, num, time);
		}
		time -= num;
		if (time < num3)
		{
			return 1f;
		}
		time -= num3;
		if (time < num2 && num2 > 0.0001f)
		{
			return 1f - Damper.Damp(1f, num2, time);
		}
		return 0f;
	}
}
