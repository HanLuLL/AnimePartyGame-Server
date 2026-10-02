using System;
using UnityEngine;

namespace FairyGUI;

public class EllipseMesh : IMeshFactory, IHitTest
{
	public Rect? drawRect;

	public float lineWidth;

	public Color32 lineColor;

	public Color32? centerColor;

	public Color32? fillColor;

	public float startDegree;

	public float endDegreee;

	private static int[] SECTOR_CENTER_TRIANGLES = new int[24]
	{
		0, 4, 1, 0, 3, 4, 0, 2, 3, 0,
		8, 5, 0, 7, 8, 0, 6, 7, 6, 5,
		2, 2, 1, 6
	};

	public EllipseMesh()
	{
		lineColor = Color.black;
		startDegree = 0f;
		endDegreee = 360f;
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		Rect rect = (drawRect.HasValue ? drawRect.Value : vb.contentRect);
		Color32 color = (fillColor.HasValue ? fillColor.Value : vb.vertexColor);
		float num = Mathf.Clamp(startDegree, 0f, 360f);
		float num2 = Mathf.Clamp(endDegreee, 0f, 360f);
		bool flag = num > 0f || num2 < 360f;
		num *= (float)Math.PI / 180f;
		num2 *= (float)Math.PI / 180f;
		Color32 color2 = ((!centerColor.HasValue) ? color : centerColor.Value);
		float num3 = rect.width / 2f;
		float num4 = rect.height / 2f;
		int value = Mathf.CeilToInt((float)Math.PI * (num3 + num4) / 4f);
		value = Mathf.Clamp(value, 40, 800);
		float num5 = (float)Math.PI * 2f / (float)value;
		float num6 = 0f;
		float num7 = 0f;
		if (lineWidth > 0f && flag)
		{
			num7 = lineWidth / Mathf.Max(num3, num4);
			num += num7;
			num2 -= num7;
		}
		int currentVertCount = vb.currentVertCount;
		float num8 = rect.x + num3;
		float num9 = rect.y + num4;
		vb.AddVert(new Vector3(num8, num9, 0f), color2);
		for (int i = 0; i < value; i++)
		{
			if (num6 < num)
			{
				num6 = num;
			}
			else if (num6 > num2)
			{
				num6 = num2;
			}
			Vector3 position = new Vector3(Mathf.Cos(num6) * (num3 - lineWidth) + num8, Mathf.Sin(num6) * (num4 - lineWidth) + num9, 0f);
			vb.AddVert(position, color);
			if (lineWidth > 0f)
			{
				vb.AddVert(position, lineColor);
				vb.AddVert(new Vector3(Mathf.Cos(num6) * num3 + num8, Mathf.Sin(num6) * num4 + num9, 0f), lineColor);
			}
			num6 += num5;
		}
		if (lineWidth > 0f)
		{
			int num10 = value * 3;
			for (int j = 0; j < num10; j += 3)
			{
				if (j != num10 - 3)
				{
					vb.AddTriangle(0, j + 1, j + 4);
					vb.AddTriangle(j + 5, j + 2, j + 3);
					vb.AddTriangle(j + 3, j + 6, j + 5);
				}
				else if (!flag)
				{
					vb.AddTriangle(0, j + 1, 1);
					vb.AddTriangle(2, j + 2, j + 3);
					vb.AddTriangle(j + 3, 3, 2);
				}
				else
				{
					vb.AddTriangle(0, j + 1, j + 1);
					vb.AddTriangle(j + 2, j + 2, j + 3);
					vb.AddTriangle(j + 3, j + 3, j + 2);
				}
			}
		}
		else
		{
			for (int k = 0; k < value; k++)
			{
				if (k != value - 1)
				{
					vb.AddTriangle(0, k + 1, k + 2);
				}
				else if (!flag)
				{
					vb.AddTriangle(0, k + 1, 1);
				}
				else
				{
					vb.AddTriangle(0, k + 1, k + 1);
				}
			}
		}
		if (lineWidth > 0f && flag)
		{
			vb.AddVert(new Vector3(num3, num4, 0f), lineColor);
			float num11 = lineWidth * 0.5f;
			num -= num7;
			num6 = num + num7 * 0.5f + (float)Math.PI / 2f;
			vb.AddVert(new Vector3(Mathf.Cos(num6) * num11 + num3, Mathf.Sin(num6) * num11 + num4, 0f), lineColor);
			num6 -= (float)Math.PI;
			vb.AddVert(new Vector3(Mathf.Cos(num6) * num11 + num3, Mathf.Sin(num6) * num11 + num4, 0f), lineColor);
			vb.AddVert(new Vector3(Mathf.Cos(num) * num3 + num3, Mathf.Sin(num) * num4 + num4, 0f), lineColor);
			vb.AddVert(vb.GetPosition(currentVertCount + 3), lineColor);
			num2 += num7;
			num6 = num2 - num7 * 0.5f + (float)Math.PI / 2f;
			vb.AddVert(new Vector3(Mathf.Cos(num6) * num11 + num3, Mathf.Sin(num6) * num11 + num4, 0f), lineColor);
			num6 -= (float)Math.PI;
			vb.AddVert(new Vector3(Mathf.Cos(num6) * num11 + num3, Mathf.Sin(num6) * num11 + num4, 0f), lineColor);
			vb.AddVert(vb.GetPosition(currentVertCount + value * 3), lineColor);
			vb.AddVert(new Vector3(Mathf.Cos(num2) * num3 + num3, Mathf.Sin(num2) * num4 + num4, 0f), lineColor);
			vb.AddTriangles(SECTOR_CENTER_TRIANGLES, value * 3 + 1);
		}
	}

	public bool HitTest(Rect contentRect, Vector2 point)
	{
		if (!contentRect.Contains(point))
		{
			return false;
		}
		float num = contentRect.width * 0.5f;
		float num2 = contentRect.height * 0.5f;
		float num3 = point.x - num - contentRect.x;
		float num4 = point.y - num2 - contentRect.y;
		if (Mathf.Pow(num3 / num, 2f) + Mathf.Pow(num4 / num2, 2f) < 1f)
		{
			if (startDegree != 0f || endDegreee != 360f)
			{
				float num5 = Mathf.Atan2(num4, num3) * 57.29578f;
				if (num5 < 0f)
				{
					num5 += 360f;
				}
				if (num5 >= startDegree)
				{
					return num5 <= endDegreee;
				}
				return false;
			}
			return true;
		}
		return false;
	}
}
