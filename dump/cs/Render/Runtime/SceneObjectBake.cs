using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Render.Runtime;

public class SceneObjectBake : ScriptableRendererFeature
{
	public RenderPassEvent PassEvent = (RenderPassEvent)300;

	public int PassEventOffset;

	private SceneObjectBakePass pass;

	public SceneObjectBakePass Pass
	{
		get
		{
			if (pass == null)
			{
				((ScriptableRendererFeature)this).Create();
			}
			return pass;
		}
	}

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		CameraData cameraData = renderingData.cameraData;
		if (cameraData.cameraType == CameraType.Game && (int)cameraData.renderType == 0 && pass.HasWaitBakeObj)
		{
			pass.UpdataPassSetting(this);
			renderer.EnqueuePass((ScriptableRenderPass)(object)pass);
		}
	}

	public override void Create()
	{
		pass = pass ?? new SceneObjectBakePass();
	}
}
