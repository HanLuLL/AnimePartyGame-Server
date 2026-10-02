using System;
using UnityEngine;

namespace FairyGUI;

public class RegularPolygonMesh : IMeshFactory, IHitTest
{
	public Rect? drawRect;

	public int sides;

	public float lineWidth;

	public Color32 lineColor;

	public Color32? centerColor;

	public Color32? fillColor;

	public float[] distances;

	public float rotation;

	public RegularPolygonMesh()
	{
		sides = 3;
		lineColor = Color.black;
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		if (distances != null && distances.Length < sides)
		{
			Debug.LogError("distances.Length<sides");
			return;
		}
		Rect rect = (drawRect.HasValue ? drawRect.Value : vb.contentRect);
		Color32 color = (fillColor.HasValue ? fillColor.Value : vb.vertexColor);
		float num = (float)Math.PI * 2f / (float)sides;
		float num2 = rotation * ((float)Math.PI / 180f);
		float num3 = Mathf.Min(rect.width / 2f, rect.height / 2f);
		float num4 = num3 + rect.x;
		float num5 = num3 + rect.y;
		vb.AddVert(new Vector3(num4, num5, 0f), (!centerColor.HasValue) ? color : centerColor.Value);
		for (int i = 0; i < sides; i++)
		{
			float num6 = num3;
			if (distances != null)
			{
				num6 *= distances[i];
			}
			float num7 = Mathf.Cos(num2) * (num6 - lineWidth);
			float num8 = Mathf.Sin(num2) * (num6 - lineWidth);
			Vector3 position = new Vector3(num7 + num4, num8 + num5, 0f);
			vb.AddVert(position, color);
			if (lineWidth > 0f)
			{
				vb.AddVert(position, lineColor);
				num7 = Mathf.Cos(num2) * num6 + num4;
				num8 = Mathf.Sin(num2) * num6 + num5;
				vb.AddVert(new Vector3(num7, num8, 0f), lineColor);
			}
			num2 += num;
		}
		if (lineWidth > 0f)
		{
			int num9 = sides * 3;
			for (int j = 0; j < num9; j += 3)
			{
				if (j != num9 - 3)
				{
					vb.AddTriangle(0, j + 1, j + 4);
					vb.AddTriangle(j + 5, j + 2, j + 3);
					vb.AddTriangle(j + 3, j + 6, j + 5);
				}
				else
				{
					vb.AddTriangle(0, j + 1, 1);
					vb.AddTriangle(2, j + 2, j + 3);
					vb.AddTriangle(j + 3, 3, 2);
				}
			}
		}
		else
		{
			for (int k = 0; k < sides; k++)
			{
				vb.AddTriangle(0, k + 1, (k == sides - 1) ? 1 : (k + 2));
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
