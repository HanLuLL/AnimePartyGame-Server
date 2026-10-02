using System;
using UnityEngine;

namespace FairyGUI;

public class RoundedRectMesh : IMeshFactory, IHitTest
{
	public Rect? drawRect;

	public float lineWidth;

	public Color32 lineColor;

	public Color32? fillColor;

	public float topLeftRadius;

	public float topRightRadius;

	public float bottomLeftRadius;

	public float bottomRightRadius;

	public RoundedRectMesh()
	{
		lineColor = Color.black;
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		Rect rect = (drawRect.HasValue ? drawRect.Value : vb.contentRect);
		Color32 color = (fillColor.HasValue ? fillColor.Value : vb.vertexColor);
		float num = rect.width / 2f;
		float num2 = rect.height / 2f;
		float a = Mathf.Min(num, num2);
		float x = num + rect.x;
		float y = num2 + rect.y;
		vb.AddVert(new Vector3(x, y, 0f), color);
		int currentVertCount = vb.currentVertCount;
		for (int i = 0; i < 4; i++)
		{
			float b = 0f;
			switch (i)
			{
			case 0:
				b = bottomRightRadius;
				break;
			case 1:
				b = bottomLeftRadius;
				break;
			case 2:
				b = topLeftRadius;
				break;
			case 3:
				b = topRightRadius;
				break;
			}
			b = Mathf.Min(a, b);
			float num3 = rect.x;
			float num4 = rect.y;
			if (i == 0 || i == 3)
			{
				num3 = rect.xMax - b * 2f;
			}
			if (i == 0 || i == 1)
			{
				num4 = rect.yMax - b * 2f;
			}
			if (b != 0f)
			{
				int num5 = Mathf.Max(1, Mathf.CeilToInt((float)Math.PI * b / 8f)) + 1;
				float num6 = (float)Math.PI / 2f / (float)num5;
				float num7 = (float)Math.PI / 2f * (float)i;
				float num8 = num7;
				for (int j = 1; j <= num5; j++)
				{
					if (j == num5)
					{
						num7 = num8 + (float)Math.PI / 2f;
					}
					Vector3 position = new Vector3(num3 + Mathf.Cos(num7) * (b - lineWidth) + b, num4 + Mathf.Sin(num7) * (b - lineWidth) + b, 0f);
					vb.AddVert(position, color);
					if (lineWidth != 0f)
					{
						vb.AddVert(position, lineColor);
						vb.AddVert(new Vector3(num3 + Mathf.Cos(num7) * b + b, num4 + Mathf.Sin(num7) * b + b, 0f), lineColor);
					}
					num7 += num6;
				}
			}
			else
			{
				Vector3 position2 = new Vector3(num3, num4, 0f);
				if (lineWidth != 0f)
				{
					num3 = ((i != 0 && i != 3) ? (num3 + lineWidth) : (num3 - lineWidth));
					num4 = ((i != 0 && i != 1) ? (num4 + lineWidth) : (num4 - lineWidth));
					Vector3 position3 = new Vector3(num3, num4, 0f);
					vb.AddVert(position3, color);
					vb.AddVert(position3, lineColor);
					vb.AddVert(position2, lineColor);
				}
				else
				{
					vb.AddVert(position2, color);
				}
			}
		}
		currentVertCount = vb.currentVertCount - currentVertCount;
		if (lineWidth > 0f)
		{
			for (int k = 0; k < currentVertCount; k += 3)
			{
				if (k != currentVertCount - 3)
				{
					vb.AddTriangle(0, k + 1, k + 4);
					vb.AddTriangle(k + 5, k + 2, k + 3);
					vb.AddTriangle(k + 3, k + 6, k + 5);
				}
				else
				{
					vb.AddTriangle(0, k + 1, 1);
					vb.AddTriangle(2, k + 2, k + 3);
					vb.AddTriangle(k + 3, 3, 2);
				}
			}
		}
		else
		{
			for (int l = 0; l < currentVertCount; l++)
			{
				vb.AddTriangle(0, l + 1, (l == currentVertCount - 1) ? 1 : (l + 2));
			}
		}
	}

	public bool HitTest(Rect contentRect, Vector2 point)
	{
		if (drawRect.HasValue)
		{
			return drawRect.Value.Contains(point);
		}
		return contentRect.Contains(point);
	}
}
