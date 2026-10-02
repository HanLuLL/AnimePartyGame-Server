using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class LineMesh : IMeshFactory
{
	public GPath path;

	public float lineWidth;

	public AnimationCurve lineWidthCurve;

	public Gradient gradient;

	public bool roundEdge;

	public float fillStart;

	public float fillEnd;

	public float pointDensity;

	public bool repeatFill;

	private static List<Vector3> points = new List<Vector3>();

	private static List<float> ts = new List<float>();

	public LineMesh()
	{
		path = new GPath();
		lineWidth = 2f;
		fillStart = 0f;
		fillEnd = 1f;
		pointDensity = 0.1f;
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		Vector2 position = vb.uvRect.position;
		Vector2 uv = new Vector2(vb.uvRect.xMax, vb.uvRect.yMax);
		float num = path.length / vb.textureSize.x;
		int segmentCount = path.segmentCount;
		float num2 = 0f;
		float num3 = lineWidth;
		for (int i = 0; i < segmentCount; i++)
		{
			float num4 = path.GetSegmentLength(i) / path.length;
			float num5 = Mathf.Clamp(fillStart - num2, 0f, num4) / num4;
			float num6 = Mathf.Clamp(fillEnd - num2, 0f, num4) / num4;
			if (num5 >= num6)
			{
				num2 += num4;
				continue;
			}
			points.Clear();
			ts.Clear();
			path.GetPointsInSegment(i, num5, num6, points, ts, pointDensity);
			int count = points.Count;
			Color color = vb.vertexColor;
			Color color2 = vb.vertexColor;
			if (gradient != null)
			{
				color = gradient.Evaluate(num2);
			}
			if (lineWidthCurve != null)
			{
				num3 = lineWidthCurve.Evaluate(num2);
			}
			if (roundEdge && i == 0 && num5 == 0f)
			{
				DrawRoundEdge(vb, points[0], points[1], num3, color, position);
			}
			int currentVertCount = vb.currentVertCount;
			for (int j = 1; j < count; j++)
			{
				Vector3 vector = points[j - 1];
				Vector3 vector2 = points[j];
				int num7 = currentVertCount + (j - 1) * 2;
				float num8 = num2 + num4 * ts[j];
				Vector3 vector3 = Vector3.Cross(vector2 - vector, new Vector3(0f, 0f, 1f));
				vector3.Normalize();
				float x;
				if (j == 1)
				{
					x = ((!repeatFill) ? Mathf.Lerp(position.x, uv.x, num2 + num4 * ts[j - 1]) : (num8 * num * uv.x));
					vb.AddVert(vector - vector3 * num3 * 0.5f, color, new Vector2(x, uv.y));
					vb.AddVert(vector + vector3 * num3 * 0.5f, color, new Vector2(x, position.y));
					if (i != 0)
					{
						vb.AddTriangle(num7 - 2, num7 - 1, num7 + 1);
						vb.AddTriangle(num7 - 2, num7 + 1, num7);
					}
				}
				if (gradient != null)
				{
					color2 = gradient.Evaluate(num8);
				}
				if (lineWidthCurve != null)
				{
					num3 = lineWidthCurve.Evaluate(num8);
				}
				x = ((!repeatFill) ? Mathf.Lerp(position.x, uv.x, num8) : (num8 * num * uv.x));
				vb.AddVert(vector2 - vector3 * num3 * 0.5f, color2, new Vector2(x, uv.y));
				vb.AddVert(vector2 + vector3 * num3 * 0.5f, color2, new Vector2(x, position.y));
				vb.AddTriangle(num7, num7 + 1, num7 + 3);
				vb.AddTriangle(num7, num7 + 3, num7 + 2);
			}
			if (roundEdge && i == segmentCount - 1 && num6 == 1f)
			{
				DrawRoundEdge(vb, points[count - 1], points[count - 2], num3, color2, uv);
			}
			num2 += num4;
		}
	}

	private void DrawRoundEdge(VertexBuffer vb, Vector2 p0, Vector2 p1, float lw, Color32 color, Vector2 uv)
	{
		Vector2 vector = Vector3.Cross(p0 - p1, new Vector3(0f, 0f, 1f));
		vector.Normalize();
		vector = vector * lw / 2f;
		Vector2 vector2 = (p0 - p1).normalized * lw / 2f;
		int num = Mathf.CeilToInt((float)Math.PI * lw / 2f);
		if (num < 6)
		{
			num = 6;
		}
		int currentVertCount = vb.currentVertCount;
		float num2 = (float)Math.PI / (float)(num - 1);
		vb.AddVert(p0, color, uv);
		vb.AddVert(p0 + vector, color, uv);
		for (int i = 0; i < num; i++)
		{
			vb.AddVert(p0 + Mathf.Cos(num2 * (float)i) * vector + Mathf.Sin(num2 * (float)i) * vector2, color, uv);
			vb.AddTriangle(currentVertCount, currentVertCount + 1 + i, currentVertCount + 2 + i);
		}
	}
}
