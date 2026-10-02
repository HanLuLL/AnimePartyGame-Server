using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace UI;

public class UIOverallColorController
{
	private GObject sourceObj;

	private List<UIColorRecorder> colorRecorders = new List<UIColorRecorder>();

	private HashSet<GObject> ignoreObjs;

	public UIOverallColorController(GObject comp, HashSet<GObject> ignores = null)
	{
		sourceObj = comp;
		if (ignores != null && ignores.Count > 0)
		{
			ignoreObjs = ignores;
		}
		RecordChildColor();
	}

	private void RecordChildColor()
	{
		colorRecorders.Clear();
		Stack<GObject> stack = new Stack<GObject>();
		if (sourceObj != null)
		{
			stack.Push(sourceObj);
		}
		while (stack.Count > 0)
		{
			GObject gObject = stack.Pop();
			if (ignoreObjs != null && ignoreObjs.Contains(gObject))
			{
				continue;
			}
			if (gObject is IColorGear colorGear)
			{
				colorRecorders.Add(new UIColorRecorder(colorGear, colorGear.color));
			}
			if (!(gObject is GComponent { numChildren: var numChildren } gComponent))
			{
				continue;
			}
			for (int i = 0; i < numChildren; i++)
			{
				GObject childAt = gComponent.GetChildAt(i);
				if (childAt != null)
				{
					stack.Push(childAt);
				}
			}
		}
	}

	public void SetOverallColor(Color color)
	{
		foreach (UIColorRecorder colorRecorder in colorRecorders)
		{
			colorRecorder.ColorGear.color = colorRecorder.OriginColor * color;
		}
	}

	public void Recover()
	{
		foreach (UIColorRecorder colorRecorder in colorRecorders)
		{
			colorRecorder.ColorGear.color = colorRecorder.OriginColor;
		}
	}
}
