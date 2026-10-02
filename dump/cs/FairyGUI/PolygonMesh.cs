using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class PolygonMesh : IMeshFactory, IHitTest
{
	public readonly List<Vector2> points;

	public readonly List<Vector2> texcoords;

	public float lineWidth;

	public Color32 lineColor;

	public Color32? fillColor;

	public Color32[] colors;

	public bool usePercentPositions;

	private static List<int> sRestIndices = new List<int>();

	public PolygonMesh()
	{
		points = new List<Vector2>();
		texcoords = new List<Vector2>();
	}

	public void Add(Vector2 point)
	{
		points.Add(point);
	}

	public void Add(Vector2 point, Vector2 texcoord)
	{
		points.Add(point);
		texcoords.Add(texcoord);
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		int count = points.Count;
		if (count < 3)
		{
			return;
		}
		Color32 color = (fillColor.HasValue ? fillColor.Value : vb.vertexColor);
		float width = vb.contentRect.width;
		float height = vb.contentRect.height;
		bool flag = texcoords.Count >= count;
		bool flag2 = true;
		for (int i = 0; i < count; i++)
		{
			Vector3 position = new Vector3(points[i].x, points[i].y, 0f);
			if (usePercentPositions)
			{
				position.x *= width;
				position.y *= height;
			}
			if (flag)
			{
				Vector2 uv = texcoords[i];
				if ((uv.x != 0f && uv.x != 1f) || (uv.y != 0f && uv.y != 1f))
				{
					flag2 = false;
				}
				uv.x = Mathf.Lerp(vb.uvRect.x, vb.uvRect.xMax, uv.x);
				uv.y = Mathf.Lerp(vb.uvRect.y, vb.uvRect.yMax, uv.y);
				vb.AddVert(position, color, uv);
			}
			else
			{
				vb.AddVert(position, color);
			}
		}
		if (flag && flag2 && count == 4)
		{
			vb._isArbitraryQuad = true;
		}
		sRestIndices.Clear();
		for (int j = 0; j < count; j++)
		{
			sRestIndices.Add(j);
		}
		int num = 0;
		int num2 = count;
		while (num2 > 3)
		{
			bool flag3 = false;
			int num3 = sRestIndices[num % num2];
			int num4 = sRestIndices[(num + 1) % num2];
			int num5 = sRestIndices[(num + 2) % num2];
			Vector2 a = points[num3];
			Vector2 b = points[num4];
			Vector2 c = points[num5];
			if ((a.y - b.y) * (c.x - b.x) + (b.x - a.x) * (c.y - b.y) >= 0f)
			{
				flag3 = true;
				for (int k = 3; k < num2; k++)
				{
					int index = sRestIndices[(num + k) % num2];
					Vector2 p = points[index];
					if (IsPointInTriangle(ref p, ref a, ref b, ref c))
					{
						flag3 = false;
						break;
					}
				}
			}
			if (flag3)
			{
				vb.AddTriangle(num3, num4, num5);
				sRestIndices.RemoveAt((num + 1) % num2);
				num2--;
				num = 0;
			}
			else
			{
				num++;
				if (num == num2)
				{
					break;
				}
			}
		}
		vb.AddTriangle(sRestIndices[0], sRestIndices[1], sRestIndices[2]);
		if (colors != null)
		{
			vb.RepeatColors(colors, 0, vb.currentVertCount);
		}
		if (lineWidth > 0f)
		{
			DrawOutline(vb);
		}
	}

	private void DrawOutline(VertexBuffer vb)
	{
		int count = points.Count;
		int num = vb.currentVertCount - count;
		int num2 = vb.currentVertCount;
		for (int i = 0; i < count; i++)
		{
			Vector3 vector = vb.vertices[num + i];
			vector.y = 0f - vector.y;
			Vector3 vector2 = ((i >= count - 1) ? vb.vertices[num] : vb.vertices[num + i + 1]);
			vector2.y = 0f - vector2.y;
			Vector3 vector3 = Vector3.Cross(vector2 - vector, new Vector3(0f, 0f, 1f));
			vector3.Normalize();
			vb.AddVert(vector - vector3 * lineWidth * 0.5f, lineColor);
			vb.AddVert(vector + vector3 * lineWidth * 0.5f, lineColor);
			vb.AddVert(vector2 - vector3 * lineWidth * 0.5f, lineColor);
			vb.AddVert(vector2 + vector3 * lineWidth * 0.5f, lineColor);
			num2 += 4;
			vb.AddTriangle(num2 - 4, num2 - 3, num2 - 1);
			vb.AddTriangle(num2 - 4, num2 - 1, num2 - 2);
			if (i != 0)
			{
				vb.AddTriangle(num2 - 6, num2 - 5, num2 - 3);
				vb.AddTriangle(num2 - 6, num2 - 3, num2 - 4);
			}
			if (i == count - 1)
			{
				num += count;
				vb.AddTriangle(num2 - 2, num2 - 1, num + 1);
				vb.AddTriangle(num2 - 2, num + 1, num);
			}
		}
	}

	private bool IsPointInTriangle(ref Vector2 p, ref Vector2 a, ref Vector2 b, ref Vector2 c)
	{
		float num = c.x - a.x;
		float num2 = c.y - a.y;
		float num3 = b.x - a.x;
		float num4 = b.y - a.y;
		float num5 = p.x - a.x;
		float num6 = p.y - a.y;
		float num7 = num * num + num2 * num2;
		float num8 = num * num3 + num2 * num4;
		float num9 = num * num5 + num2 * num6;
		float num10 = num3 * num3 + num4 * num4;
		float num11 = num3 * num5 + num4 * num6;
		float num12 = 1f / (num7 * num10 - num8 * num8);
		float num13 = (num10 * num9 - num8 * num11) * num12;
		float num14 = (num7 * num11 - num8 * num9) * num12;
		if (num13 >= 0f && num14 >= 0f)
		{
			return num13 + num14 < 1f;
		}
		return false;
	}

	public bool HitTest(Rect contentRect, Vector2 point)
	{
		if (!contentRect.Contains(point))
		{
			return false;
		}
		int count = points.Count;
		int index = count - 1;
		bool flag = false;
		float width = contentRect.width;
		float height = contentRect.height;
		for (int i = 0; i < count; i++)
		{
			float num = points[i].x;
			float num2 = points[i].y;
			float x = points[index].x;
			float y = points[index].y;
			if (usePercentPositions)
			{
				num *= width;
				num2 *= height;
				num *= width;
				num2 *= height;
			}
			if (((num2 < point.y && y >= point.y) || (y < point.y && num2 >= point.y)) && (num <= point.x || x <= point.x) && num + (point.y - num2) / (y - num2) * (x - num) < point.x)
			{
				flag = !flag;
			}
			index = i;
		}
		return flag;
	}
}
