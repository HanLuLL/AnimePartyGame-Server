using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UI;

public static class BlurKVTextureCreator
{
	public static void CreateBlurKVTexture(Material kvCopyMat, Material blurMat, int width, int height, float blurRadius, int iterateCount)
	{
		_ = RenderingUtils.fullscreenMesh;
		width = Mathf.Clamp(width, 1, 8192);
		height = Mathf.Clamp(height, 1, 8192);
		CommandBufferPool.Get("CreateShadowTexture");
		RenderTextureDescriptor desc = new RenderTextureDescriptor(width, height, RenderTextureFormat.ARGB32);
		desc.sRGB = true;
		List<RenderTexture> list = new List<RenderTexture>();
		int num = width;
		int num2 = height;
		for (int i = 0; i < iterateCount + 1; i++)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(desc);
			list.Add(temporary);
			num = Mathf.Max(1, num / 2);
			num2 = Mathf.Max(1, num2 / 2);
			desc.width = num;
			desc.height = num2;
		}
	}
}
