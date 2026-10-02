using UnityEngine;

namespace FairyGUI;

public class RectMesh : IMeshFactory, IHitTest
{
	public Rect? drawRect;

	public float lineWidth;

	public Color32 lineColor;

	public Color32? fillColor;

	public Color32[] colors;

	public RectMesh()
	{
		lineColor = Color.black;
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		Rect vertRect = (drawRect.HasValue ? drawRect.Value : vb.contentRect);
		Color32 color = (fillColor.HasValue ? fillColor.Value : vb.vertexColor);
		if (lineWidth == 0f)
		{
			if (color.a != 0)
			{
				vb.AddQuad(vertRect, color);
			}
		}
		else
		{
			Rect vertRect2 = new Rect(vertRect.x, vertRect.y, lineWidth, vertRect.height);
			vb.AddQuad(vertRect2, lineColor);
			vertRect2 = new Rect(vertRect.xMax - lineWidth, vertRect.y, lineWidth, vertRect.height);
			vb.AddQuad(vertRect2, lineColor);
			vertRect2 = new Rect(vertRect.x + lineWidth, vertRect.y, vertRect.width - lineWidth * 2f, lineWidth);
			vb.AddQuad(vertRect2, lineColor);
			vertRect2 = new Rect(vertRect.x + lineWidth, vertRect.yMax - lineWidth, vertRect.width - lineWidth * 2f, lineWidth);
			vb.AddQuad(vertRect2, lineColor);
			if (color.a != 0)
			{
				vertRect2 = Rect.MinMaxRect(vertRect.x + lineWidth, vertRect.y + lineWidth, vertRect.xMax - lineWidth, vertRect.yMax - lineWidth);
				if (vertRect2.width > 0f && vertRect2.height > 0f)
				{
					vb.AddQuad(vertRect2, color);
				}
			}
		}
		if (colors != null)
		{
			vb.RepeatColors(colors, 0, vb.currentVertCount);
		}
		vb.AddTriangles();
	}

	public bool HitTest(Rect contentRect, Vector2 point)
	{
		return contentRect.Contains(point);
	}
}
