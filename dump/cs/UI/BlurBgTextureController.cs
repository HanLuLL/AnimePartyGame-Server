using Cysharp.Threading.Tasks;
using FairyGUI;
using UnityEngine;

namespace UI;

public class BlurBgTextureController
{
	private NTexture bgNTexture;

	private DynamicBlurRenderTextureRequest dynamicBlurRequest;

	public bool IsVaild => bgNTexture != null;

	public async UniTask CreateBlurTex()
	{
		await CreateBlurTex((GameCameraFlag)2);
	}

	public async UniTask CreateDynmicBlurTex(GameCameraFlag flag)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (bgNTexture != null)
		{
			bgNTexture.Dispose();
			bgNTexture = null;
		}
		if (dynamicBlurRequest == null)
		{
			dynamicBlurRequest = GraphicEventManager.Instance.CreateBlurRTRequest(flag, 512, 256);
			dynamicBlurRequest.BlurRenderTexture.wrapMode = TextureWrapMode.Clamp;
		}
		bgNTexture = new NTexture(dynamicBlurRequest.BlurRenderTexture);
		bgNTexture.destroyMethod = DestroyMethod.ReleaseTemp;
		for (int i = 0; i < 10; i++)
		{
			if (dynamicBlurRequest?.IsRenderSuccess ?? false)
			{
				break;
			}
			await UniTask.DelayFrame(1);
		}
	}

	public async UniTask CreateBlurTex(GameCameraFlag flag)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (bgNTexture != null)
		{
			bgNTexture.Dispose();
			bgNTexture = null;
		}
		GraphicEventManager.Instance.CaptureBlurRequest(flag, (CaptureBlurCallback)delegate(Texture2D tex)
		{
			tex.wrapMode = TextureWrapMode.Clamp;
			bgNTexture = new NTexture(tex);
		});
		for (int i = 0; i < 10; i++)
		{
			if (bgNTexture != null)
			{
				break;
			}
			await UniTask.DelayFrame(1);
		}
	}

	public void OnShown(BaseWindow win)
	{
		win.BgLoader.texture = bgNTexture;
	}

	public void OnShown(GLoader loader)
	{
		loader.texture = bgNTexture;
	}

	public void OnHide()
	{
		if (bgNTexture != null)
		{
			bgNTexture.Dispose();
			bgNTexture = null;
		}
		if (dynamicBlurRequest != null)
		{
			GraphicEventManager.Instance.RemoveBlurRTRequest(dynamicBlurRequest);
			dynamicBlurRequest = null;
		}
	}
}
