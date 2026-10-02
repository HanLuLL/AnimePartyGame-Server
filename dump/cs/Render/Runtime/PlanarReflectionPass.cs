using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Render.Runtime;

public class PlanarReflectionPass : ScriptableRenderPass
{
	private PlanarReflection setting;

	private ShaderTagId shaderTagId;

	private List<RenderTargetHandle> planarReflectionTexList;

	private RenderTargetHandle planarReflectionDepth;

	private int texCount => setting.IterateCount + 1;

	public PlanarReflectionPass(PlanarReflection _setting)
	{
		setting = _setting;
		((ScriptableRenderPass)this).renderPassEvent = (RenderPassEvent)250;
		planarReflectionTexList = new List<RenderTargetHandle>();
		((RenderTargetHandle)(ref planarReflectionDepth)).Init("_PlanarReflectionDepth");
		shaderTagId = new ShaderTagId("PlanarReflectionCaster");
	}

	public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		((ScriptableRenderPass)this).Configure(cmd, cameraTextureDescriptor);
		if (planarReflectionTexList.Count < texCount)
		{
			int num = texCount - planarReflectionTexList.Count;
			for (int i = 0; i < num; i++)
			{
				RenderTargetHandle item = default(RenderTargetHandle);
				int count = planarReflectionTexList.Count;
				((RenderTargetHandle)(ref item)).Init($"_PlanarReflectionTex{count}");
				planarReflectionTexList.Add(item);
			}
		}
		int num2 = Mathf.Max(setting.Width, 1);
		int num3 = Mathf.Max(setting.Height, 1);
		RenderTextureDescriptor desc = new RenderTextureDescriptor(num2, num3, RenderTextureFormat.RGB111110Float);
		for (int j = 0; j < texCount; j++)
		{
			RenderTargetHandle val = planarReflectionTexList[j];
			cmd.GetTemporaryRT(((RenderTargetHandle)(ref val)).id, desc, FilterMode.Bilinear);
			num2 = Mathf.Max(num2 / 2, 1);
			num3 = Mathf.Max(num3 / 2, 1);
			desc.width = num2;
			desc.height = num3;
		}
		cmd.GetTemporaryRT(desc: new RenderTextureDescriptor(setting.Width, setting.Height, RenderTextureFormat.Depth, 16), nameID: ((RenderTargetHandle)(ref planarReflectionDepth)).id);
	}

	public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		CommandBuffer commandBuffer = CommandBufferPool.Get("PlanarReflectionPass");
		commandBuffer.SetViewMatrix(((CameraData)(ref renderingData.cameraData)).GetViewMatrix(0));
		RenderTargetHandle val = planarReflectionTexList[0];
		commandBuffer.SetRenderTarget(((RenderTargetHandle)(ref val)).Identifier(), ((RenderTargetHandle)(ref planarReflectionDepth)).Identifier());
		commandBuffer.ClearRenderTarget(clearDepth: true, clearColor: true, Color.black);
		commandBuffer.SetGlobalFloat(ShaderConstant._PlanePosY, setting.PlaneY);
		commandBuffer.SetGlobalFloat(ShaderConstant._PlanarReflectionStrength, setting.PlanarReflectionStrength);
		context.ExecuteCommandBuffer(commandBuffer);
		commandBuffer.Clear();
		DrawingSettings drawingSettings = ((ScriptableRenderPass)this).CreateDrawingSettings(shaderTagId, ref renderingData, SortingCriteria.CommonOpaque);
		FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
		context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
		Mesh fullscreenMesh = RenderingUtils.fullscreenMesh;
		commandBuffer.SetGlobalFloat(ShaderConstant._BlurRaidus, setting.BlurRadius);
		for (int i = 0; i < setting.IterateCount; i++)
		{
			val = planarReflectionTexList[i + 1];
			commandBuffer.SetRenderTarget(((RenderTargetHandle)(ref val)).id);
			int mainTex = ShaderConstant._MainTex;
			val = planarReflectionTexList[i];
			commandBuffer.SetGlobalTexture(mainTex, ((RenderTargetHandle)(ref val)).id);
			commandBuffer.DrawMesh(fullscreenMesh, Matrix4x4.identity, setting.BlurMaterial, 0, 1);
		}
		commandBuffer.SetViewMatrix(((CameraData)(ref renderingData.cameraData)).GetViewMatrix(0));
		commandBuffer.SetRenderTarget(setting.CurrentRenderer.cameraColorTarget, setting.CurrentRenderer.cameraDepthTarget);
		context.ExecuteCommandBuffer(commandBuffer);
		commandBuffer.Clear();
		CommandBufferPool.Release(commandBuffer);
	}

	public override void OnCameraCleanup(CommandBuffer cmd)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		((ScriptableRenderPass)this).OnCameraCleanup(cmd);
		for (int i = 0; i < texCount; i++)
		{
			RenderTargetHandle val = planarReflectionTexList[i];
			cmd.ReleaseTemporaryRT(((RenderTargetHandle)(ref val)).id);
		}
		cmd.ReleaseTemporaryRT(((RenderTargetHandle)(ref planarReflectionDepth)).id);
		cmd.SetGlobalFloat(ShaderConstant._PlanarReflectionStrength, 0f);
	}
}
