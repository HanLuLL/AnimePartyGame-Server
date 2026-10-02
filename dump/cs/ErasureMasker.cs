using FairyGUI;
using UnityEngine;

public class ErasureMasker : MonoBehaviour
{
	[Header("是否启用")]
	public bool isEnable = true;

	[Header("毛刷大小")]
	public int brushSize = 50;

	[Header("擦拭比例")]
	public int erasuredRatio = 75;

	private GLoader _loader;

	private Texture2D _modifyTex;

	private int totalPixelCount;

	private int erasuredPixelCount;

	private SwipeGesture _swipeGesture;

	public void Init(GLoader loader)
	{
		_loader = loader;
		CreateSwipeGesture();
		Reset();
	}

	public void Reset()
	{
		_loader.displayObject.gameObject.SetActive(value: true);
		isEnable = true;
		Texture2D texture2D = (Texture2D)_loader.texture.nativeTexture;
		_modifyTex = new Texture2D(texture2D.width, texture2D.height, TextureFormat.ARGB32, mipChain: false);
		_modifyTex.SetPixels(texture2D.GetPixels());
		_modifyTex.Apply();
		_loader.texture.Reload(_modifyTex, null);
		totalPixelCount = _modifyTex.GetPixels().Length;
		erasuredPixelCount = 0;
	}

	private void CreateSwipeGesture()
	{
		TryDisposeSwipeGesture();
		_swipeGesture = new SwipeGesture(_loader);
		_swipeGesture.onBegin.Add(OnSwipeGestureBegin);
		_swipeGesture.onMove.Add(OnSwipeGestureMove);
		_swipeGesture.onEnd.Add(OnSwipeGestureEnd);
	}

	public void TryDisposeSwipeGesture()
	{
		if (_swipeGesture != null)
		{
			_swipeGesture.onBegin.Remove(OnSwipeGestureBegin);
			_swipeGesture.onMove.Remove(OnSwipeGestureMove);
			_swipeGesture.onEnd.Remove(OnSwipeGestureEnd);
			_swipeGesture.Dispose();
			_swipeGesture = null;
		}
	}

	private void OnSwipeGestureBegin(EventContext context)
	{
	}

	private void OnSwipeGestureMove(EventContext context)
	{
		if (!isEnable || !(context.sender is SwipeGesture swipeGesture))
		{
			return;
		}
		Vector2 vector = new Vector2(swipeGesture.point.x, (float)_modifyTex.height - swipeGesture.point.y);
		int num = Mathf.Max(0, (int)vector.x - brushSize);
		int num2 = Mathf.Min((int)vector.x + brushSize, _modifyTex.width);
		int num3 = Mathf.Max(0, (int)vector.y - brushSize);
		int num4 = Mathf.Min((int)vector.y + brushSize, _modifyTex.height);
		for (int i = num; i < num2; i++)
		{
			for (int j = num3; j < num4; j++)
			{
				Color pixel = _modifyTex.GetPixel(i, j);
				if (pixel.a > 0f)
				{
					pixel.a = 0f;
					_modifyTex.SetPixel(i, j, pixel);
					erasuredPixelCount++;
				}
			}
		}
		_modifyTex.Apply();
		if ((float)erasuredPixelCount / (float)totalPixelCount >= (float)erasuredRatio)
		{
			isEnable = false;
			_loader.displayObject.gameObject.SetActive(value: false);
		}
	}

	private void OnSwipeGestureEnd(EventContext context)
	{
		_ = isEnable;
	}
}
