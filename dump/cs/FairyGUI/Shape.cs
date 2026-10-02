using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class Shape : DisplayObject
{
	public Color color
	{
		get
		{
			return base.graphics.color;
		}
		set
		{
			base.graphics.color = value;
			base.graphics.SetMeshDirty();
		}
	}

	public bool isEmpty => base.graphics.meshFactory == null;

	public Shape()
	{
		CreateGameObject("Shape");
		base.graphics = new NGraphics(base.gameObject);
		base.graphics.texture = NTexture.Empty;
		base.graphics.meshFactory = null;
	}

	public void DrawRect(float lineSize, Color lineColor, Color fillColor)
	{
		RectMesh meshFactory = base.graphics.GetMeshFactory<RectMesh>();
		meshFactory.lineWidth = lineSize;
		meshFactory.lineColor = lineColor;
		meshFactory.fillColor = null;
		meshFactory.colors = null;
		base.graphics.color = fillColor;
		base.graphics.SetMeshDirty();
	}

	public void DrawRect(float lineSize, Color32[] colors)
	{
		RectMesh meshFactory = base.graphics.GetMeshFactory<RectMesh>();
		meshFactory.lineWidth = lineSize;
		meshFactory.colors = colors;
		base.graphics.SetMeshDirty();
	}

	public void DrawRoundRect(float lineSize, Color lineColor, Color fillColor, float topLeftRadius, float topRightRadius, float bottomLeftRadius, float bottomRightRadius)
	{
		RoundedRectMesh meshFactory = base.graphics.GetMeshFactory<RoundedRectMesh>();
		meshFactory.lineWidth = lineSize;
		meshFactory.lineColor = lineColor;
		meshFactory.fillColor = null;
		meshFactory.topLeftRadius = topLeftRadius;
		meshFactory.topRightRadius = topRightRadius;
		meshFactory.bottomLeftRadius = bottomLeftRadius;
		meshFactory.bottomRightRadius = bottomRightRadius;
		base.graphics.color = fillColor;
		base.graphics.SetMeshDirty();
	}

	public void DrawEllipse(Color fillColor)
	{
		EllipseMesh meshFactory = base.graphics.GetMeshFactory<EllipseMesh>();
		meshFactory.lineWidth = 0f;
		meshFactory.startDegree = 0f;
		meshFactory.endDegreee = 360f;
		meshFactory.fillColor = null;
		meshFactory.centerColor = null;
		base.graphics.color = fillColor;
		base.graphics.SetMeshDirty();
	}

	public void DrawEllipse(float lineSize, Color centerColor, Color lineColor, Color fillColor, float startDegree, float endDegree)
	{
		EllipseMesh meshFactory = base.graphics.GetMeshFactory<EllipseMesh>();
		meshFactory.lineWidth = lineSize;
		if (centerColor.Equals(fillColor))
		{
			meshFactory.centerColor = null;
		}
		else
		{
			meshFactory.centerColor = centerColor;
		}
		meshFactory.lineColor = lineColor;
		meshFactory.fillColor = null;
		meshFactory.startDegree = startDegree;
		meshFactory.endDegreee = endDegree;
		base.graphics.color = fillColor;
		base.graphics.SetMeshDirty();
	}

	public void DrawPolygon(IList<Vector2> points, Color fillColor)
	{
		PolygonMesh meshFactory = base.graphics.GetMeshFactory<PolygonMesh>();
		meshFactory.points.Clear();
		meshFactory.points.AddRange(points);
		meshFactory.fillColor = null;
		meshFactory.colors = null;
		base.graphics.color = fillColor;
		base.graphics.SetMeshDirty();
	}

	public void DrawPolygon(IList<Vector2> points, Color32[] colors)
	{
		PolygonMesh meshFactory = base.graphics.GetMeshFactory<PolygonMesh>();
		meshFactory.points.Clear();
		meshFactory.points.AddRange(points);
		meshFactory.fillColor = null;
		meshFactory.colors = colors;
		base.graphics.SetMeshDirty();
	}

	public void DrawPolygon(IList<Vector2> points, Color fillColor, float lineSize, Color lineColor)
	{
		PolygonMesh meshFactory = base.graphics.GetMeshFactory<PolygonMesh>();
		meshFactory.points.Clear();
		meshFactory.points.AddRange(points);
		meshFactory.fillColor = null;
		meshFactory.lineWidth = lineSize;
		meshFactory.lineColor = lineColor;
		meshFactory.colors = null;
		base.graphics.color = fillColor;
		base.graphics.SetMeshDirty();
	}

	public void DrawRegularPolygon(int sides, float lineSize, Color centerColor, Color lineColor, Color fillColor, float rotation, float[] distances)
	{
		RegularPolygonMesh meshFactory = base.graphics.GetMeshFactory<RegularPolygonMesh>();
		meshFactory.sides = sides;
		meshFactory.lineWidth = lineSize;
		meshFactory.centerColor = centerColor;
		meshFactory.lineColor = lineColor;
		meshFactory.fillColor = null;
		meshFactory.rotation = rotation;
		meshFactory.distances = distances;
		base.graphics.color = fillColor;
		base.graphics.SetMeshDirty();
	}

	public void Clear()
	{
		base.graphics.meshFactory = null;
	}

	protected override DisplayObject HitTest()
	{
		if (base.graphics.meshFactory == null)
		{
			return null;
		}
		Vector2 vector = WorldToLocal(HitTestContext.worldPoint, HitTestContext.direction);
		if (base.graphics.meshFactory is IHitTest hitTest)
		{
			if (!hitTest.HitTest(_contentRect, vector))
			{
				return null;
			}
			return this;
		}
		if (_contentRect.Contains(vector))
		{
			return this;
		}
		return null;
	}
}
