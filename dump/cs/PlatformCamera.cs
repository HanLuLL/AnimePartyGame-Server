using Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(UniversalAdditionalCameraData))]
public class PlatformCamera : MonoBehaviour
{
	[SerializeField]
	private bool _hasDecal;

	private void Awake()
	{
		UniversalAdditionalCameraData component = GetComponent<UniversalAdditionalCameraData>();
		foreach (ScriptableRendererFeature rendererFeature in component.scriptableRenderer.rendererFeatures)
		{
			if (((Object)(object)rendererFeature).name == "DecalRendererFeature" && rendererFeature.isActive != _hasDecal)
			{
				rendererFeature.SetActive(_hasDecal);
			}
		}
		if (GameSettings.graphicsType != GraphicsType.Ultra)
		{
			if (GameSettings.graphicsType == GraphicsType.High)
			{
				component.antialiasing = (AntialiasingMode)0;
			}
			else if (GameSettings.graphicsType == GraphicsType.Middle)
			{
				component.antialiasing = (AntialiasingMode)0;
			}
			else if (GameSettings.graphicsType == GraphicsType.Low)
			{
				component.renderPostProcessing = false;
				component.antialiasing = (AntialiasingMode)0;
				component.renderShadows = false;
			}
		}
	}
}
