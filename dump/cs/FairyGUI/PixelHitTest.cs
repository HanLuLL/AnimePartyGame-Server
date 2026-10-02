using UnityEngine;

namespace FairyGUI;

public class PixelHitTest : IHitTest
{
	public int offsetX;

	public int offsetY;

	public float sourceWidth;

	public float sourceHeight;

	private PixelHitTestData _data;

	public PixelHitTest(PixelHitTestData data, int offsetX, int offsetY, float sourceWidth, float sourceHeight)
	{
		_data = data;
		this.offsetX = offsetX;
		this.offsetY = offsetY;
		this.sourceWidth = sourceWidth;
		this.sourceHeight = sourceHeight;
	}

	public bool HitTest(Rect contentRect, Vector2 localPoint)
	{
		if (!contentRect.Contains(localPoint))
		{
			return false;
		}
		int num = Mathf.FloorToInt((localPoint.x * sourceWidth / contentRect.width - (float)offsetX) * _data.scale);
		int num2 = Mathf.FloorToInt((localPoint.y * sourceHeight / contentRect.height - (float)offsetY) * _data.scale);
		if (num < 0 || num2 < 0 || num >= _data.pixelWidth)
		{
			return false;
		}
		int num3 = num2 * _data.pixelWidth + num;
		int num4 = num3 / 8;
		int num5 = num3 % 8;
		if (num4 >= 0 && num4 < _data.pixelsLength)
		{
			return ((_data.pixels[_data.pixelsOffset + num4] >> num5) & 1) > 0;
		}
		return false;
	}
}
