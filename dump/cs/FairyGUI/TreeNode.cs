using System;
using System.Collections.Generic;

namespace FairyGUI;

[Obsolete("Use GTree and GTreeNode instead")]
public class TreeNode
{
	public object data;

	private List<TreeNode> _children;

	private bool _expanded;

	public TreeNode parent { get; private set; }

	public TreeView tree { get; private set; }

	public GComponent cell { get; internal set; }

	public int level { get; private set; }

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
					tree.AfterExpanded(this);
				}
				else
				{
					tree.AfterCollapsed(this);
				}
			}
		}
	}

	public bool isFolder => _children != null;

	public string text
	{
		get
		{
			if (cell != null)
			{
				return cell.text;
			}
			return null;
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

	public TreeNode(bool hasChild)
	{
		if (hasChild)
		{
			_children = new List<TreeNode>();
		}
	}

	public TreeNode AddChild(TreeNode child)
	{
		AddChildAt(child, _children.Count);
		return child;
	}

	public TreeNode AddChildAt(TreeNode child, int index)
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
				child.level = level + 1;
				child.SetTree(tree);
				if (cell != null && cell.parent != null && _expanded)
				{
					tree.AfterInserted(child);
				}
			}
			return child;
		}
		throw new Exception("Invalid child index");
	}

	public TreeNode RemoveChild(TreeNode child)
	{
		int num = _children.IndexOf(child);
		if (num != -1)
		{
			RemoveChildAt(num);
		}
		return child;
	}

	public TreeNode RemoveChildAt(int index)
	{
		if (index >= 0 && index < numChildren)
		{
			TreeNode treeNode = _children[index];
			_children.RemoveAt(index);
			treeNode.parent = null;
			if (tree != null)
			{
				treeNode.SetTree(null);
				tree.AfterRemoved(treeNode);
			}
			return treeNode;
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

	public TreeNode GetChildAt(int index)
	{
		if (index >= 0 && index < numChildren)
		{
			return _children[index];
		}
		throw new Exception("Invalid child index");
	}

	public int GetChildIndex(TreeNode child)
	{
		return _children.IndexOf(child);
	}

	public TreeNode GetPrevSibling()
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

	public TreeNode GetNextSibling()
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

	public void SetChildIndex(TreeNode child, int index)
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
			if (cell != null && cell.parent != null && _expanded)
			{
				tree.AfterMoved(child);
			}
		}
	}

	public void SwapChildren(TreeNode child1, TreeNode child2)
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
		TreeNode child = _children[index1];
		TreeNode child2 = _children[index2];
		SetChildIndex(child, index2);
		SetChildIndex(child2, index1);
	}

	internal void SetTree(TreeView value)
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
				TreeNode treeNode = _children[i];
				treeNode.level = level + 1;
				treeNode.SetTree(value);
			}
		}
	}
}
