using UnityEngine;
using UnityEngine.Rendering;

public class EnvLightingController : MonoBehaviour
{
	[SerializeField]
	private EnvLightingMode lightingMode;

	[SerializeField]
	private Material skyMaterial;

	[SerializeField]
	[ColorUsage(true, true)]
	private Color ambientSkyColor;

	[SerializeField]
	[ColorUsage(true, true)]
	private Color ambientEquatorColor;

	[SerializeField]
	[ColorUsage(true, true)]
	private Color ambientGroundColor;

	[SerializeField]
	[Range(0f, 8f)]
	private float intensityMultiplier = 1f;

	private bool isSkyBoxMode => lightingMode == EnvLightingMode.Skybox;

	private bool isGradientMode => lightingMode == EnvLightingMode.Gradient;

	private bool isColorMode => lightingMode == EnvLightingMode.Color;

	private bool isShowAmbientSkyColor
	{
		get
		{
			if (!isSkyBoxMode || !(skyMaterial == null))
			{
				return !isSkyBoxMode;
			}
			return true;
		}
	}

	private bool isShowintensity
	{
		get
		{
			if (isSkyBoxMode)
			{
				return skyMaterial != null;
			}
			return false;
		}
	}

	private void OnEnable()
	{
		if (Application.isPlaying)
		{
			UpdateEnvLighting();
		}
	}

	public void UpdateEnvLighting()
	{
		RenderSettings.ambientMode = (AmbientMode)lightingMode;
		RenderSettings.ambientSkyColor = ambientSkyColor;
		if (isSkyBoxMode)
		{
			RenderSettings.skybox = skyMaterial;
			if (isShowintensity)
			{
				RenderSettings.ambientIntensity = intensityMultiplier;
			}
		}
		if (isGradientMode)
		{
			RenderSettings.ambientEquatorColor = ambientEquatorColor;
			RenderSettings.ambientGroundColor = ambientGroundColor;
		}
	}

	private void OnValidate()
	{
		if (Application.isPlaying)
		{
			UpdateEnvLighting();
		}
	}
}
