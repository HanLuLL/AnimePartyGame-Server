using UnityEngine;

namespace FairyGUI;

public class BlurFilter : IFilter
{
	public float blurSize;

	private DisplayObject _target;

	private Material _blitMaterial;

	public DisplayObject target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
			_target.EnterPaintingMode(1, null);
			_target.onPaint += OnRenderImage;
			_blitMaterial = new Material(ShaderConfig.GetShader("FairyGUI/BlurFilter"));
			_blitMaterial.hideFlags = DisplayObject.hideFlags;
		}
	}

	public BlurFilter()
	{
		blurSize = 1f;
	}

	public void Dispose()
	{
		_target.LeavePaintingMode(1);
		_target.onPaint -= OnRenderImage;
		_target = null;
		if (Application.isPlaying)
		{
			Object.Destroy(_blitMaterial);
		}
		else
		{
			Object.DestroyImmediate(_blitMaterial);
		}
	}

	public void Update()
	{
	}

	private void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = blurSize * (float)iteration + 0.5f;
		Graphics.BlitMultiTap(source, dest, _blitMaterial, new Vector2(0f - num, 0f - num), new Vector2(0f - num, num), new Vector2(num, num), new Vector2(num, 0f - num));
	}

	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, _blitMaterial, new Vector2(num, num), new Vector2(0f - num, num), new Vector2(num, num), new Vector2(num, 0f - num));
	}

	private void OnRenderImage()
	{
		if (!((double)blurSize < 0.01))
		{
			RenderTexture renderTexture = (RenderTexture)_target.paintingGraphics.texture.nativeTexture;
			int width = renderTexture.width / 8;
			int height = renderTexture.height / 8;
			RenderTexture renderTexture2 = RenderTexture.GetTemporary(width, height, 0);
			DownSample4x(renderTexture, renderTexture2);
			for (int i = 0; i < 2; i++)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
				FourTapCone(renderTexture2, temporary, i);
				RenderTexture.ReleaseTemporary(renderTexture2);
				renderTexture2 = temporary;
			}
			Graphics.Blit(renderTexture2, renderTexture);
			RenderTexture.ReleaseTemporary(renderTexture2);
		}
	}
}
