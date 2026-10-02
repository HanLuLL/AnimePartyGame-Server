using System;
using System.Collections.Generic;

namespace FairyGUI;

public class GTreeNode
{
	public object data;

	private List<GTreeNode> _children;

	private bool _expanded;

	private int _level;

	internal GComponent _cell;

	internal string _resURL;

	public GTreeNode parent { get; private set; }

	public GTree tree { get; private set; }

	public GComponent cell => _cell;

	public int level => _level;

	public bool expanded
	{
		get
		{
			return _expanded;
		}
		set
		{
			if (_children == null || _expanded == value)
			{
				return;
			}
			_expanded = value;
			if (tree != null)
			{
				if (_expanded)
				{
					tree._AfterExpanded(this);
				}
				else
				{
					tree._AfterCollapsed(this);
				}
			}
		}
	}

	public bool isFolder => _children != null;

	public string text
	{
		get
		{
			if (_cell != null)
			{
				return _cell.text;
			}
			return null;
		}
		set
		{
			if (_cell != null)
			{
				_cell.text = value;
			}
		}
	}

	public string icon
	{
		get
		{
			if (_cell != null)
			{
				return _cell.icon;
			}
			return null;
		}
		set
		{
			if (_cell != null)
			{
				_cell.icon = value;
			}
		}
	}

	public int numChildren
	{
		get
		{
			if (_children != null)
			{
				return _children.Count;
			}
			return 0;
		}
	}

	public GTreeNode(bool hasChild)
		: this(hasChild, null)
	{
	}

	public GTreeNode(bool hasChild, string resURL)
	{
		if (hasChild)
		{
			_children = new List<GTreeNode>();
		}
		_resURL = resURL;
	}

	public void ExpandToRoot()
	{
		for (GTreeNode gTreeNode = this; gTreeNode != null; gTreeNode = gTreeNode.parent)
		{
			gTreeNode.expanded = true;
		}
	}

	public GTreeNode AddChild(GTreeNode child)
	{
		AddChildAt(child, _children.Count);
		return child;
	}

	public GTreeNode AddChildAt(GTreeNode child, int index)
	{
		if (child == null)
		{
			throw new Exception("child is null");
		}
		int count = _children.Count;
		if (index >= 0 && index <= count)
		{
			if (child.parent == this)
			{
				SetChildIndex(child, index);
			}
			else
			{
				if (child.parent != null)
				{
					child.parent.RemoveChild(child);
				}
				int count2 = _children.Count;
				if (index == count2)
				{
					_children.Add(child);
				}
				else
				{
					_children.Insert(index, child);
				}
				child.parent = this;
				child._level = _level + 1;
				child._SetTree(tree);
				if ((tree != null && this == tree.rootNode) || (_cell != null && _cell.parent != null && _expanded))
				{
					tree._AfterInserted(child);
				}
			}
			return child;
		}
		throw new Exception("Invalid child index");
	}

	public GTreeNode RemoveChild(GTreeNode child)
	{
		int num = _children.IndexOf(child);
		if (num != -1)
		{
			RemoveChildAt(num);
		}
		return child;
	}

	public GTreeNode RemoveChildAt(int index)
	{
		if (index >= 0 && index < numChildren)
		{
			GTreeNode gTreeNode = _children[index];
			_children.RemoveAt(index);
			gTreeNode.parent = null;
			if (tree != null)
			{
				gTreeNode._SetTree(null);
				tree._AfterRemoved(gTreeNode);
			}
			return gTreeNode;
		}
		throw new Exception("Invalid child index");
	}

	public void RemoveChildren(int beginIndex = 0, int endIndex = -1)
	{
		if (endIndex < 0 || endIndex >= numChildren)
		{
			endIndex = numChildren - 1;
		}
		for (int i = beginIndex; i <= endIndex; i++)
		{
			RemoveChildAt(beginIndex);
		}
	}

	public GTreeNode GetChildAt(int index)
	{
		if (index >= 0 && index < numChildren)
		{
			return _children[index];
		}
		throw new Exception("Invalid child index");
	}

	public int GetChildIndex(GTreeNode child)
	{
		return _children.IndexOf(child);
	}

	public GTreeNode GetPrevSibling()
	{
		if (parent == null)
		{
			return null;
		}
		int num = parent._children.IndexOf(this);
		if (num <= 0)
		{
			return null;
		}
		return parent._children[num - 1];
	}

	public GTreeNode GetNextSibling()
	{
		if (parent == null)
		{
			return null;
		}
		int num = parent._children.IndexOf(this);
		if (num < 0 || num >= parent._children.Count - 1)
		{
			return null;
		}
		return parent._children[num + 1];
	}

	public void SetChildIndex(GTreeNode child, int index)
	{
		int num = _children.IndexOf(child);
		if (num == -1)
		{
			throw new Exception("Not a child of this container");
		}
		int count = _children.Count;
		if (index < 0)
		{
			index = 0;
		}
		else if (index > count)
		{
			index = count;
		}
		if (num != index)
		{
			_children.RemoveAt(num);
			_children.Insert(index, child);
			if ((tree != null && this == tree.rootNode) || (_cell != null && _cell.parent != null && _expanded))
			{
				tree._AfterMoved(child);
			}
		}
	}

	public void SwapChildren(GTreeNode child1, GTreeNode child2)
	{
		int num = _children.IndexOf(child1);
		int num2 = _children.IndexOf(child2);
		if (num == -1 || num2 == -1)
		{
			throw new Exception("Not a child of this container");
		}
		SwapChildrenAt(num, num2);
	}

	public void SwapChildrenAt(int index1, int index2)
	{
		GTreeNode child = _children[index1];
		GTreeNode child2 = _children[index2];
		SetChildIndex(child, index2);
		SetChildIndex(child2, index1);
	}

	internal void _SetTree(GTree value)
	{
		tree = value;
		if (tree != null && tree.treeNodeWillExpand != null && _expanded)
		{
			tree.treeNodeWillExpand(this, expand: true);
		}
		if (_children != null)
		{
			int count = _children.Count;
			for (int i = 0; i < count; i++)
			{
				GTreeNode gTreeNode = _children[i];
				gTreeNode._level = _level + 1;
				gTreeNode._SetTree(value);
			}
		}
	}
}
