using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.Rendering;

namespace Render.Runtime;

public class SpecialHotScenePlanarReflectionController : MonoBehaviour
{
	private string blurMatKey = "UIDualKawaseBlur";

	public float BlurRadius = 1f;

	public float PlaneY = 2f;

	public float PlanarReflectionStrength = 1f;

	public int Width = 512;

	public int Height = 512;

	private RenderPipelineAsset GetUltraPipelineAsset()
	{
		int num = 3;
		if (GraphicsSettings.allConfiguredRenderPipelines.Length <= num)
		{
			return null;
		}
		return GraphicsSettings.allConfiguredRenderPipelines[num];
	}

	public void AddOrSetPlanarReflectionRenderFeature()
	{
	}

	private void RemovePlanarReflectionRenderFeature()
	{
	}

	private async UniTask SetPlanaerReflection(PlanarReflection planarReflection)
	{
		planarReflection.BlurMaterial = await SimpleSingletonProvider<MaterialManager>.inst.GetMaterial(blurMatKey);
		planarReflection.BlurRadius = BlurRadius;
		planarReflection.PlaneY = PlaneY;
		planarReflection.PlanarReflectionStrength = PlanarReflectionStrength;
		planarReflection.Width = Mathf.Clamp(Width, 1, 4096);
		planarReflection.Height = Mathf.Clamp(Height, 1, 4096);
		await UniTask.CompletedTask;
	}

	private void OnEnable()
	{
		AddOrSetPlanarReflectionRenderFeature();
	}

	private void OnDisable()
	{
		RemovePlanarReflectionRenderFeature();
	}
}
