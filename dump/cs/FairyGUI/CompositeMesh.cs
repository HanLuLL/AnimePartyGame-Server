using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class CompositeMesh : IMeshFactory, IHitTest
{
	public readonly List<IMeshFactory> elements;

	public int activeIndex;

	public CompositeMesh()
	{
		elements = new List<IMeshFactory>();
		activeIndex = -1;
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		int count = elements.Count;
		if (count == 1)
		{
			elements[0].OnPopulateMesh(vb);
			return;
		}
		VertexBuffer vertexBuffer = VertexBuffer.Begin(vb);
		for (int i = 0; i < count; i++)
		{
			if (activeIndex == -1 || i == activeIndex)
			{
				vertexBuffer.Clear();
				elements[i].OnPopulateMesh(vertexBuffer);
				vb.Append(vertexBuffer);
			}
		}
		vertexBuffer.End();
	}

	public bool HitTest(Rect contentRect, Vector2 point)
	{
		if (!contentRect.Contains(point))
		{
			return false;
		}
		bool result = false;
		int count = elements.Count;
		for (int i = 0; i < count; i++)
		{
			if (activeIndex != -1 && i != activeIndex)
			{
				continue;
			}
			if (elements[i] is IHitTest hitTest)
			{
				if (hitTest.HitTest(contentRect, point))
				{
					return true;
				}
			}
			else
			{
				result = true;
			}
		}
		return result;
	}
}
