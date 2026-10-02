using System;
using System.Collections.Generic;

namespace FairyGUI;

[Obsolete("Use GTree and GTreeNode instead")]
public class TreeView : EventDispatcher
{
	public delegate GComponent TreeNodeCreateCellDelegate(TreeNode node);

	public delegate void TreeNodeRenderDelegate(TreeNode node);

	public delegate void TreeNodeWillExpandDelegate(TreeNode node, bool expand);

	public int indent;

	public TreeNodeCreateCellDelegate treeNodeCreateCell;

	public TreeNodeRenderDelegate treeNodeRender;

	public TreeNodeWillExpandDelegate treeNodeWillExpand;

	public GList list { get; private set; }

	public TreeNode root { get; private set; }

	public EventListener onClickNode { get; private set; }

	public EventListener onRightClickNode { get; private set; }

	public TreeView(GList list)
	{
		this.list = list;
		list.onClickItem.Add(__clickItem);
		list.onRightClickItem.Add(__clickItem);
		list.RemoveChildrenToPool();
		root = new TreeNode(hasChild: true);
		root.SetTree(this);
		root.cell = list;
		root.expanded = true;
		indent = 30;
		onClickNode = new EventListener(this, "onClickNode");
		onRightClickNode = new EventListener(this, "onRightClickNode");
	}

	public TreeNode GetSelectedNode()
	{
		if (list.selectedIndex != -1)
		{
			return (TreeNode)list.GetChildAt(list.selectedIndex).data;
		}
		return null;
	}

	public List<TreeNode> GetSelection()
	{
		List<int> selection = this.list.GetSelection();
		int count = selection.Count;
		List<TreeNode> list = new List<TreeNode>();
		for (int i = 0; i < count; i++)
		{
			TreeNode item = (TreeNode)this.list.GetChildAt(selection[i]).data;
			list.Add(item);
		}
		return list;
	}

	public void AddSelection(TreeNode node, bool scrollItToView = false)
	{
		TreeNode parent = node.parent;
		while (parent != null && parent != root)
		{
			parent.expanded = true;
			parent = parent.parent;
		}
		list.AddSelection(list.GetChildIndex(node.cell), scrollItToView);
	}

	public void RemoveSelection(TreeNode node)
	{
		list.RemoveSelection(list.GetChildIndex(node.cell));
	}

	public void ClearSelection()
	{
		list.ClearSelection();
	}

	public int GetNodeIndex(TreeNode node)
	{
		return list.GetChildIndex(node.cell);
	}

	public void UpdateNode(TreeNode node)
	{
		if (node.cell != null && treeNodeRender != null)
		{
			treeNodeRender(node);
		}
	}

	public void UpdateNodes(List<TreeNode> nodes)
	{
		int count = nodes.Count;
		for (int i = 0; i < count; i++)
		{
			TreeNode treeNode = nodes[i];
			if (treeNode.cell == null)
			{
				break;
			}
			if (treeNodeRender != null)
			{
				treeNodeRender(treeNode);
			}
		}
	}

	public void ExpandAll(TreeNode folderNode)
	{
		folderNode.expanded = true;
		int numChildren = folderNode.numChildren;
		for (int i = 0; i < numChildren; i++)
		{
			TreeNode childAt = folderNode.GetChildAt(i);
			if (childAt.isFolder)
			{
				ExpandAll(childAt);
			}
		}
	}

	public void CollapseAll(TreeNode folderNode)
	{
		if (folderNode != root)
		{
			folderNode.expanded = false;
		}
		int numChildren = folderNode.numChildren;
		for (int i = 0; i < numChildren; i++)
		{
			TreeNode childAt = folderNode.GetChildAt(i);
			if (childAt.isFolder)
			{
				CollapseAll(childAt);
			}
		}
	}

	private void CreateCell(TreeNode node)
	{
		if (treeNodeCreateCell != null)
		{
			node.cell = treeNodeCreateCell(node);
		}
		else
		{
			node.cell = list.itemPool.GetObject(list.defaultItem) as GComponent;
		}
		if (node.cell == null)
		{
			throw new Exception("Unable to create tree cell");
		}
		node.cell.data = node;
		GObject child = node.cell.GetChild("indent");
		if (child != null)
		{
			child.width = (node.level - 1) * indent;
		}
		GButton gButton = (GButton)node.cell.GetChild("expandButton");
		if (gButton != null)
		{
			if (node.isFolder)
			{
				gButton.visible = true;
				gButton.onClick.Add(__clickExpandButton);
				gButton.data = node;
				gButton.selected = node.expanded;
			}
			else
			{
				gButton.visible = false;
			}
		}
		if (treeNodeRender != null)
		{
			treeNodeRender(node);
		}
	}

	internal void AfterInserted(TreeNode node)
	{
		CreateCell(node);
		int insertIndexForNode = GetInsertIndexForNode(node);
		list.AddChildAt(node.cell, insertIndexForNode);
		if (treeNodeRender != null)
		{
			treeNodeRender(node);
		}
		if (node.isFolder && node.expanded)
		{
			CheckChildren(node, insertIndexForNode);
		}
	}

	private int GetInsertIndexForNode(TreeNode node)
	{
		TreeNode treeNode = node.GetPrevSibling();
		if (treeNode == null)
		{
			treeNode = node.parent;
		}
		int num = list.GetChildIndex(treeNode.cell) + 1;
		int level = node.level;
		int numChildren = list.numChildren;
		for (int i = num; i < numChildren && ((TreeNode)list.GetChildAt(i).data).level > level; i++)
		{
			num++;
		}
		return num;
	}

	internal void AfterRemoved(TreeNode node)
	{
		RemoveNode(node);
	}

	internal void AfterExpanded(TreeNode node)
	{
		if (node != root && treeNodeWillExpand != null)
		{
			treeNodeWillExpand(node, expand: true);
		}
		if (node.cell == null)
		{
			return;
		}
		if (node != root)
		{
			if (treeNodeRender != null)
			{
				treeNodeRender(node);
			}
			GButton gButton = (GButton)node.cell.GetChild("expandButton");
			if (gButton != null)
			{
				gButton.selected = true;
			}
		}
		if (node.cell.parent != null)
		{
			CheckChildren(node, list.GetChildIndex(node.cell));
		}
	}

	internal void AfterCollapsed(TreeNode node)
	{
		if (node != root && treeNodeWillExpand != null)
		{
			treeNodeWillExpand(node, expand: false);
		}
		if (node.cell == null)
		{
			return;
		}
		if (node != root)
		{
			if (treeNodeRender != null)
			{
				treeNodeRender(node);
			}
			GButton gButton = (GButton)node.cell.GetChild("expandButton");
			if (gButton != null)
			{
				gButton.selected = false;
			}
		}
		if (node.cell.parent != null)
		{
			HideFolderNode(node);
		}
	}

	internal void AfterMoved(TreeNode node)
	{
		if (!node.isFolder)
		{
			list.RemoveChild(node.cell);
		}
		else
		{
			HideFolderNode(node);
		}
		int insertIndexForNode = GetInsertIndexForNode(node);
		list.AddChildAt(node.cell, insertIndexForNode);
		if (node.isFolder && node.expanded)
		{
			CheckChildren(node, insertIndexForNode);
		}
	}

	private int CheckChildren(TreeNode folderNode, int index)
	{
		int numChildren = folderNode.numChildren;
		for (int i = 0; i < numChildren; i++)
		{
			index++;
			TreeNode childAt = folderNode.GetChildAt(i);
			if (childAt.cell == null)
			{
				CreateCell(childAt);
			}
			if (childAt.cell.parent == null)
			{
				list.AddChildAt(childAt.cell, index);
			}
			if (childAt.isFolder && childAt.expanded)
			{
				index = CheckChildren(childAt, index);
			}
		}
		return index;
	}

	private void HideFolderNode(TreeNode folderNode)
	{
		int numChildren = folderNode.numChildren;
		for (int i = 0; i < numChildren; i++)
		{
			TreeNode childAt = folderNode.GetChildAt(i);
			if (childAt.cell != null)
			{
				if (childAt.cell.parent != null)
				{
					list.RemoveChild(childAt.cell);
				}
				list.itemPool.ReturnObject(childAt.cell);
				childAt.cell.data = null;
				childAt.cell = null;
			}
			if (childAt.isFolder && childAt.expanded)
			{
				HideFolderNode(childAt);
			}
		}
	}

	private void RemoveNode(TreeNode node)
	{
		if (node.cell != null)
		{
			if (node.cell.parent != null)
			{
				list.RemoveChild(node.cell);
			}
			list.itemPool.ReturnObject(node.cell);
			node.cell.data = null;
			node.cell = null;
		}
		if (node.isFolder)
		{
			int numChildren = node.numChildren;
			for (int i = 0; i < numChildren; i++)
			{
				TreeNode childAt = node.GetChildAt(i);
				RemoveNode(childAt);
			}
		}
	}

	private void __clickExpandButton(EventContext context)
	{
		context.StopPropagation();
		GButton gButton = (GButton)context.sender;
		TreeNode treeNode = (TreeNode)gButton.parent.data;
		if (list.scrollPane != null)
		{
			float posY = list.scrollPane.posY;
			if (gButton.selected)
			{
				treeNode.expanded = true;
			}
			else
			{
				treeNode.expanded = false;
			}
			list.scrollPane.posY = posY;
			list.scrollPane.ScrollToView(treeNode.cell);
		}
		else if (gButton.selected)
		{
			treeNode.expanded = true;
		}
		else
		{
			treeNode.expanded = false;
		}
	}

	private void __clickItem(EventContext context)
	{
		float posY = 0f;
		if (list.scrollPane != null)
		{
			posY = list.scrollPane.posY;
		}
		TreeNode treeNode = (TreeNode)((GObject)context.data).data;
		if (context.type == list.onRightClickItem.type)
		{
			onRightClickNode.Call(treeNode);
		}
		else
		{
			onClickNode.Call(treeNode);
		}
		if (list.scrollPane != null)
		{
			list.scrollPane.posY = posY;
			list.scrollPane.ScrollToView(treeNode.cell);
		}
	}
}
