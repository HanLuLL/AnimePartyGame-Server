using System.Collections.Generic;
using Render.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameLogic;

[ExecuteInEditMode]
public class WindController : MonoBehaviour
{
	private static WindController _instance;

	private const int MAX_WIND_COUNT = 4;

	private const int WIND_BUFFER_STEP = 6;

	[SerializeField]
	private Vector3 windDir = new Vector3(1f, 0f, 1f);

	[SerializeField]
	private float windSpeed = 0.1f;

	[SerializeField]
	private float windNoiseTile = 1f;

	[SerializeField]
	[FormerlySerializedAs("_WindNoiseStrength")]
	private float windNoiseStrength = 1f;

	private List<CircleWind> circleWinds = new List<CircleWind>();

	private float[] circleWindBuffer = new float[24];

	public static WindController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType<WindController>();
			}
			return _instance;
		}
	}

	private void OnEnable()
	{
		if (_instance == null)
		{
			_instance = this;
		}
	}

	private void OnDisable()
	{
		if (_instance == this)
		{
			_instance = null;
		}
	}

	public void AddCircleWind(CircleWind wind)
	{
		circleWinds.Add(wind);
	}

	public void RemoveCircleWind(CircleWind wind)
	{
		circleWinds.Remove(wind);
	}

	private void UpdateParameter()
	{
		Shader.SetGlobalVector(ShaderConstant._WindDir, windDir);
		Shader.SetGlobalFloat(ShaderConstant._WindSpeed, windSpeed);
		Shader.SetGlobalFloat(ShaderConstant._WindNoiseTile, windNoiseTile);
		Shader.SetGlobalFloat(ShaderConstant._WindNoiseStrength, windNoiseStrength);
		int num = Mathf.Min(circleWinds.Count, 4);
		for (int i = 0; i < num; i++)
		{
			circleWinds[i].FillCircleWindwBuffer(circleWindBuffer, i * 6);
		}
		Shader.SetGlobalFloatArray(ShaderConstant._CircleWindBuffer, circleWindBuffer);
		Shader.SetGlobalInt(ShaderConstant._CircleWindCount, num);
	}

	private void Update()
	{
		UpdateParameter();
	}
}
