using UnityEngine;
using UnityEngine.Rendering;

namespace FairyGUI;

public class BlendModeUtils
{
	public class BlendFactor
	{
		public UnityEngine.Rendering.BlendMode srcFactor;

		public UnityEngine.Rendering.BlendMode dstFactor;

		public bool pma;

		public BlendFactor(UnityEngine.Rendering.BlendMode srcFactor, UnityEngine.Rendering.BlendMode dstFactor, bool pma = false)
		{
			this.srcFactor = srcFactor;
			this.dstFactor = dstFactor;
			this.pma = pma;
		}
	}

	public static BlendFactor[] Factors = new BlendFactor[13]
	{
		new BlendFactor(UnityEngine.Rendering.BlendMode.SrcAlpha, UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha),
		new BlendFactor(UnityEngine.Rendering.BlendMode.One, UnityEngine.Rendering.BlendMode.One),
		new BlendFactor(UnityEngine.Rendering.BlendMode.SrcAlpha, UnityEngine.Rendering.BlendMode.One),
		new BlendFactor(UnityEngine.Rendering.BlendMode.DstColor, UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha, pma: true),
		new BlendFactor(UnityEngine.Rendering.BlendMode.One, UnityEngine.Rendering.BlendMode.OneMinusSrcColor, pma: true),
		new BlendFactor(UnityEngine.Rendering.BlendMode.Zero, UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha),
		new BlendFactor(UnityEngine.Rendering.BlendMode.Zero, UnityEngine.Rendering.BlendMode.SrcAlpha),
		new BlendFactor(UnityEngine.Rendering.BlendMode.OneMinusDstAlpha, UnityEngine.Rendering.BlendMode.DstAlpha),
		new BlendFactor(UnityEngine.Rendering.BlendMode.One, UnityEngine.Rendering.BlendMode.Zero),
		new BlendFactor(UnityEngine.Rendering.BlendMode.One, UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha),
		new BlendFactor(UnityEngine.Rendering.BlendMode.SrcAlpha, UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha),
		new BlendFactor(UnityEngine.Rendering.BlendMode.SrcAlpha, UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha),
		new BlendFactor(UnityEngine.Rendering.BlendMode.SrcAlpha, UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha)
	};

	public static void Apply(Material mat, BlendMode blendMode)
	{
		BlendFactor blendFactor = Factors[(int)blendMode];
		mat.SetFloat(ShaderConfig.ID_BlendSrcFactor, (float)blendFactor.srcFactor);
		mat.SetFloat(ShaderConfig.ID_BlendDstFactor, (float)blendFactor.dstFactor);
		if (blendFactor.pma)
		{
			mat.SetFloat(ShaderConfig.ID_ColorOption, 1f);
		}
		else
		{
			mat.SetFloat(ShaderConfig.ID_ColorOption, 0f);
		}
	}

	public static void Override(BlendMode blendMode, UnityEngine.Rendering.BlendMode srcFactor, UnityEngine.Rendering.BlendMode dstFactor)
	{
		BlendFactor obj = Factors[(int)blendMode];
		obj.srcFactor = srcFactor;
		obj.dstFactor = dstFactor;
	}
}
