using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.PlotTree;

[Serializable]
public abstract class PlotComposite : PlotTreeBase
{
	protected int CurrentIndex;

	[SerializeReference]
	public List<PlotTreeBase> ChildNodes = new List<PlotTreeBase>();

	public int TryRemove(PlotTreeBase nodeData)
	{
		for (int i = 0; i < ChildNodes.Count; i++)
		{
			if (ChildNodes[i].Guid == nodeData.Guid)
			{
				ChildNodes.RemoveAt(i);
				return i;
			}
		}
		return -1;
	}

	public int InsertBefore(PlotTreeBase nodeBase, PlotTreeBase select)
	{
		int num = ChildNodes.IndexOf(select);
		ChildNodes.Insert(num, nodeBase);
		return num;
	}

	public int InsertAfter(PlotTreeBase nodeBase, PlotTreeBase select)
	{
		int num = ChildNodes.IndexOf(select);
		ChildNodes.Insert(num + 1, nodeBase);
		return num + 1;
	}
}
