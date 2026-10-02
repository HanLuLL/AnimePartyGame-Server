using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class SelectionShape : DisplayObject, IMeshFactory
{
	public readonly List<Rect> rects;

	public Color color
	{
		get
		{
			return base.graphics.color;
		}
		set
		{
			base.graphics.color = value;
			base.graphics.Tint();
		}
	}

	public SelectionShape()
	{
		CreateGameObject("SelectionShape");
		base.graphics = new NGraphics(base.gameObject);
		base.graphics.texture = NTexture.Empty;
		base.graphics.meshFactory = this;
		rects = new List<Rect>();
	}

	public void Refresh()
	{
		int count = rects.Count;
		if (count > 0)
		{
			Rect rect = default(Rect);
			rect = rects[0];
			for (int i = 1; i < count; i++)
			{
				Rect rect2 = rects[i];
				rect = ToolSet.Union(ref rect, ref rect2);
			}
			SetSize(rect.xMax, rect.yMax);
		}
		else
		{
			SetSize(0f, 0f);
		}
		base.graphics.SetMeshDirty();
	}

	public void Clear()
	{
		rects.Clear();
		base.graphics.SetMeshDirty();
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		int count = rects.Count;
		if (count != 0 && !(color == Color.clear))
		{
			for (int i = 0; i < count; i++)
			{
				vb.AddQuad(rects[i]);
			}
			vb.AddTriangles();
		}
	}

	protected override DisplayObject HitTest()
	{
		Vector2 point = WorldToLocal(HitTestContext.worldPoint, HitTestContext.direction);
		if (_contentRect.Contains(point))
		{
			int count = rects.Count;
			for (int i = 0; i < count; i++)
			{
				if (rects[i].Contains(point))
				{
					return this;
				}
			}
		}
		return null;
	}
}
