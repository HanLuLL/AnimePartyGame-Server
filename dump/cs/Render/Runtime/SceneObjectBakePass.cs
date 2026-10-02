using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Render.Runtime;

public class SceneObjectBakePass : ScriptableRenderPass
{
	private List<SceneBakeObject> waitBakeObjects = new List<SceneBakeObject>();

	private Action onBakeFinish;

	private ShaderTagId shaderTagId;

	private int dealyFrame;

	public bool HasWaitBakeObj => waitBakeObjects.Count > 0;

	public SceneObjectBakePass()
	{
		shaderTagId = new ShaderTagId("Bake");
	}

	public void UpdataPassSetting(SceneObjectBake setting)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		((ScriptableRenderPass)this).renderPassEvent = (RenderPassEvent)(setting.PassEvent + setting.PassEventOffset);
	}

	public void SetSceneBakeObjects(IEnumerable<SceneBakeObject> sceneBakeObjets, int _dealyFrame, Action cb)
	{
		waitBakeObjects.Clear();
		waitBakeObjects.AddRange(sceneBakeObjets);
		onBakeFinish = cb;
		dealyFrame = _dealyFrame;
	}

	private void Bake(ScriptableRenderContext context, ref RenderingData renderingData)
	{
		CommandBuffer commandBuffer = CommandBufferPool.Get("SceneObjectBakePass");
		try
		{
			DrawingSettings drawingSettings = ((ScriptableRenderPass)this).CreateDrawingSettings(shaderTagId, ref renderingData, SortingCriteria.None);
			for (int i = 0; i < waitBakeObjects.Count; i++)
			{
				waitBakeObjects[i].Bake(context, commandBuffer, renderingData.cullResults, ref drawingSettings);
			}
			commandBuffer.SetRenderTarget(new RenderTargetIdentifier(-1), new RenderTargetIdentifier(-1));
			context.ExecuteCommandBuffer(commandBuffer);
			commandBuffer.Clear();
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			CommandBufferPool.Release(commandBuffer);
		}
	}

	public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
	{
		if (waitBakeObjects.Count > 0)
		{
			Bake(context, ref renderingData);
			dealyFrame--;
			if (dealyFrame <= 0)
			{
				waitBakeObjects.Clear();
				onBakeFinish?.Invoke();
				onBakeFinish = null;
			}
		}
	}
}
