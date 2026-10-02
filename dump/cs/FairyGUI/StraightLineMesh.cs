using UnityEngine;

namespace FairyGUI;

public class StraightLineMesh : IMeshFactory
{
	public Color color;

	public Vector3 origin;

	public Vector3 end;

	public float lineWidth;

	public bool repeatFill;

	public StraightLineMesh()
	{
		color = Color.black;
		lineWidth = 1f;
	}

	public StraightLineMesh(float lineWidth, Color color, bool repeatFill)
	{
		this.lineWidth = lineWidth;
		this.color = color;
		this.repeatFill = repeatFill;
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		if (!(origin == end))
		{
			float num = Vector2.Distance(origin, end);
			Vector3 vector = Vector3.Cross(end - origin, new Vector3(0f, 0f, 1f));
			vector.Normalize();
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			Vector3 vector5;
			if (repeatFill)
			{
				float x = num / vb.textureSize.x;
				vector2 = VertexBuffer.NormalizedUV[0];
				vector3 = VertexBuffer.NormalizedUV[1];
				vector4 = new Vector2(x, 1f);
				vector5 = new Vector2(x, 0f);
			}
			else
			{
				vector2 = new Vector2(vb.uvRect.xMin, vb.uvRect.yMin);
				vector3 = new Vector2(vb.uvRect.xMin, vb.uvRect.yMax);
				vector4 = new Vector2(vb.uvRect.xMax, vb.uvRect.yMax);
				vector5 = new Vector2(vb.uvRect.xMax, vb.uvRect.yMin);
			}
			vb.AddVert(origin - vector * lineWidth * 0.5f, color, vector2);
			vb.AddVert(origin + vector * lineWidth * 0.5f, color, vector3);
			vb.AddVert(end + vector * lineWidth * 0.5f, color, vector4);
			vb.AddVert(end - vector * lineWidth * 0.5f, color, vector5);
			vb.AddTriangles();
		}
	}
}
