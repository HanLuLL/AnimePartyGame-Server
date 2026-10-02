using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Render.Runtime;

public class PlanarReflection : ScriptableRendererFeature
{
	public float PlaneY;

	public float PlanarReflectionStrength = 1f;

	[Range(1f, 1024f)]
	public int Width = 512;

	[Range(1f, 1024f)]
	public int Height = 512;

	public float BlurRadius = 1f;

	public Material BlurMaterial;

	private PlanarReflectionPass pass;

	public int IterateCount => 1;

	public ScriptableRenderer CurrentRenderer { get; private set; }

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		if (IsValid() && renderingData.cameraData.cameraType == CameraType.Game)
		{
			CurrentRenderer = renderer;
			renderer.EnqueuePass((ScriptableRenderPass)(object)pass);
		}
	}

	private bool IsValid()
	{
		return BlurMaterial != null;
	}

	public override void Create()
	{
		pass = pass ?? new PlanarReflectionPass(this);
	}
}
