using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Render.Runtime;
using Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UI;

public static class ShadowTextureCreator
{
	private static readonly Color _defaultShadowColor = new Color(0.05f, 0.08f, 0.22f, 0.6f);

	public static UniTask<NTexture> CreateShadowTexture(NTexture nTex)
	{
		return CreateShadowTexture(nTex, _defaultShadowColor);
	}

	public static async UniTask<NTexture> CreateShadowTexture(NTexture nTex, Color color, float scale = 0.5f, float blurRadius = 0.5f, int iterateCount = 2)
	{
		Material soliderColorMat = await SimpleSingletonProvider<Core.MaterialManager>.inst.GetMaterial("UISolidColor");
		Material blurMat = await SimpleSingletonProvider<Core.MaterialManager>.inst.GetMaterial("UIDualKawaseBlur");
		Texture2D tex = nTex.nativeTexture as Texture2D;
		Rect uvRect = nTex.uvRect;
		return new NTexture(CreateShadowTexture(tex, color, scale, blurRadius, iterateCount, soliderColorMat, blurMat, uvRect));
	}

	public static Texture2D CreateShadowTexture(Texture2D tex, Color color, float scale, float blurRadius, int iterateCount, Material soliderColorMat, Material blurMat, Rect uvRect)
	{
		Mesh fullscreenMesh = RenderingUtils.fullscreenMesh;
		int value = Mathf.RoundToInt((float)tex.width * scale * uvRect.width);
		int value2 = Mathf.RoundToInt((float)tex.height * scale * uvRect.height);
		value = Mathf.Clamp(value, 1, 8192);
		value2 = Mathf.Clamp(value2, 1, 8192);
		CommandBuffer commandBuffer = CommandBufferPool.Get("CreateShadowTexture");
		RenderTextureDescriptor desc = new RenderTextureDescriptor(value, value2, RenderTextureFormat.ARGB32);
		desc.sRGB = true;
		List<RenderTexture> list = new List<RenderTexture>();
		int num = value;
		int num2 = value2;
		for (int i = 0; i < iterateCount + 1; i++)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(desc);
			list.Add(temporary);
			num = Mathf.Max(1, num / 2);
			num2 = Mathf.Max(1, num2 / 2);
			desc.width = num;
			desc.height = num2;
		}
		Vector4 value3 = new Vector4(uvRect.xMin, uvRect.yMin, uvRect.width, uvRect.height);
		commandBuffer.SetRenderTarget(list[0]);
		commandBuffer.SetGlobalTexture(ShaderConstant._MainTex, tex);
		commandBuffer.SetGlobalColor(ShaderConstant._Color, color);
		commandBuffer.SetGlobalVector(ShaderConstant._UVRect, value3);
		commandBuffer.DrawMesh(fullscreenMesh, Matrix4x4.identity, soliderColorMat, 0, 0);
		commandBuffer.SetGlobalFloat(AppShaderConstant._BlurRaidus, blurRadius);
		for (int j = 0; j < iterateCount; j++)
		{
			commandBuffer.SetRenderTarget(list[j + 1]);
			commandBuffer.SetGlobalTexture(AppShaderConstant._MainTex, list[j]);
			commandBuffer.DrawMesh(fullscreenMesh, Matrix4x4.identity, blurMat, 0, 1);
		}
		for (int num3 = iterateCount - 1; num3 >= 0; num3--)
		{
			commandBuffer.SetRenderTarget(list[num3]);
			commandBuffer.SetGlobalTexture(AppShaderConstant._MainTex, list[num3 + 1]);
			commandBuffer.DrawMesh(fullscreenMesh, Matrix4x4.identity, blurMat, 0, 2);
		}
		GraphicsFence fence = commandBuffer.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.AllGPUOperations);
		Graphics.ExecuteCommandBuffer(commandBuffer);
		CommandBufferPool.Release(commandBuffer);
		if (SystemInfo.supportsGraphicsFence)
		{
			Graphics.WaitOnAsyncGraphicsFence(fence);
		}
		Texture2D texture2D = new Texture2D(value, value2, TextureFormat.RGBA32, mipChain: false, linear: false);
		texture2D.wrapMode = TextureWrapMode.Clamp;
		texture2D.filterMode = FilterMode.Bilinear;
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = list[0];
		texture2D.ReadPixels(new Rect(0f, 0f, value, value2), 0, 0);
		texture2D.Apply();
		RenderTexture.active = active;
		for (int k = 0; k < list.Count; k++)
		{
			RenderTexture.ReleaseTemporary(list[k]);
		}
		list.Clear();
		return texture2D;
	}
}
