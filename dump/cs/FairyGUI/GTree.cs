using System;
using System.Collections.Generic;
using FairyGUI.Utils;

namespace FairyGUI;

public class GTree : GList
{
	public delegate void TreeNodeRenderDelegate(GTreeNode node, GComponent obj);

	public delegate void TreeNodeWillExpandDelegate(GTreeNode node, bool expand);

	public TreeNodeRenderDelegate treeNodeRender;

	public TreeNodeWillExpandDelegate treeNodeWillExpand;

	private int _indent;

	private GTreeNode _rootNode;

	private int _clickToExpand;

	private bool _expandedStatusInEvt;

	private static List<int> helperIntList = new List<int>();

	public GTreeNode rootNode => _rootNode;

	public int indent
	{
		get
		{
			return _indent;
		}
		set
		{
			_indent = value;
		}
	}

	public int clickToExpand
	{
		get
		{
			return _clickToExpand;
		}
		set
		{
			_clickToExpand = value;
		}
	}

	public GTree()
	{
		_indent = 30;
		_rootNode = new GTreeNode(hasChild: true);
		_rootNode._SetTree(this);
		_rootNode.expanded = true;
	}

	public GTreeNode GetSelectedNode()
	{
		int num = base.selectedIndex;
		if (num != -1)
		{
			return GetChildAt(num)._treeNode;
		}
		return null;
	}

	public List<GTreeNode> GetSelectedNodes()
	{
		return GetSelectedNodes(null);
	}

	public List<GTreeNode> GetSelectedNodes(List<GTreeNode> result)
	{
		if (result == null)
		{
			result = new List<GTreeNode>();
		}
		helperIntList.Clear();
		List<int> selection = GetSelection(helperIntList);
		int count = selection.Count;
		for (int i = 0; i < count; i++)
		{
			GTreeNode item = GetChildAt(selection[i])._treeNode;
			result.Add(item);
		}
		return result;
	}

	public void SelectNode(GTreeNode node)
	{
		SelectNode(node, scrollItToView: false);
	}

	public void SelectNode(GTreeNode node, bool scrollItToView)
	{
		GTreeNode gTreeNode = node.parent;
		while (gTreeNode != null && gTreeNode != _rootNode)
		{
			gTreeNode.expanded = true;
			gTreeNode = gTreeNode.parent;
		}
		AddSelection(GetChildIndex(node.cell), scrollItToView);
	}

	public void UnselectNode(GTreeNode node)
	{
		RemoveSelection(GetChildIndex(node.cell));
	}

	public void ExpandAll()
	{
		ExpandAll(_rootNode);
	}

	public void ExpandAll(GTreeNode folderNode)
	{
		folderNode.expanded = true;
		int num = folderNode.numChildren;
		for (int i = 0; i < num; i++)
		{
			GTreeNode childAt = folderNode.GetChildAt(i);
			if (childAt.isFolder)
			{
				ExpandAll(childAt);
			}
		}
	}

	public void CollapseAll()
	{
		CollapseAll(_rootNode);
	}

	public void CollapseAll(GTreeNode folderNode)
	{
		if (folderNode != _rootNode)
		{
			folderNode.expanded = false;
		}
		int num = folderNode.numChildren;
		for (int i = 0; i < num; i++)
		{
			GTreeNode childAt = folderNode.GetChildAt(i);
			if (childAt.isFolder)
			{
				CollapseAll(childAt);
			}
		}
	}

	private void CreateCell(GTreeNode node)
	{
		if (!(base.itemPool.GetObject(string.IsNullOrEmpty(node._resURL) ? defaultItem : node._resURL) is GComponent gComponent))
		{
			throw new Exception("FairyGUI: cannot create tree node object.");
		}
		gComponent.displayObject.home = base.displayObject.cachedTransform;
		gComponent._treeNode = node;
		node._cell = gComponent;
		GObject child = node.cell.GetChild("indent");
		if (child != null)
		{
			child.width = (node.level - 1) * indent;
		}
		Controller controller = gComponent.GetController("expanded");
		if (controller != null)
		{
			controller.onChanged.Add(__expandedStateChanged);
			controller.selectedIndex = (node.expanded ? 1 : 0);
		}
		controller = gComponent.GetController("leaf");
		if (controller != null)
		{
			controller.selectedIndex = ((!node.isFolder) ? 1 : 0);
		}
		if (node.isFolder)
		{
			gComponent.onTouchBegin.Add(__cellTouchBegin);
		}
		if (treeNodeRender != null)
		{
			treeNodeRender(node, node._cell);
		}
	}

	internal void _AfterInserted(GTreeNode node)
	{
		if (node._cell == null)
		{
			CreateCell(node);
		}
		int insertIndexForNode = GetInsertIndexForNode(node);
		AddChildAt(node.cell, insertIndexForNode);
		if (treeNodeRender != null)
		{
			treeNodeRender(node, node._cell);
		}
		if (node.isFolder && node.expanded)
		{
			CheckChildren(node, insertIndexForNode);
		}
	}

	private int GetInsertIndexForNode(GTreeNode node)
	{
		GTreeNode prevSibling = node.GetPrevSibling();
		if (prevSibling == null)
		{
			prevSibling = node.parent;
		}
		int num = GetChildIndex(prevSibling.cell) + 1;
		int level = node.level;
		int num2 = base.numChildren;
		for (int i = num; i < num2 && GetChildAt(i)._treeNode.level > level; i++)
		{
			num++;
		}
		return num;
	}

	internal void _AfterRemoved(GTreeNode node)
	{
		RemoveNode(node);
	}

	internal void _AfterExpanded(GTreeNode node)
	{
		if (node == _rootNode)
		{
			CheckChildren(_rootNode, 0);
			return;
		}
		if (treeNodeWillExpand != null)
		{
			treeNodeWillExpand(node, expand: true);
		}
		if (node._cell != null)
		{
			if (treeNodeRender != null)
			{
				treeNodeRender(node, node._cell);
			}
			Controller controller = node._cell.GetController("expanded");
			if (controller != null)
			{
				controller.selectedIndex = 1;
			}
			if (node._cell.parent != null)
			{
				CheckChildren(node, GetChildIndex(node._cell));
			}
		}
	}

	internal void _AfterCollapsed(GTreeNode node)
	{
		if (node == _rootNode)
		{
			CheckChildren(_rootNode, 0);
			return;
		}
		if (treeNodeWillExpand != null)
		{
			treeNodeWillExpand(node, expand: false);
		}
		if (node._cell != null)
		{
			if (treeNodeRender != null)
			{
				treeNodeRender(node, node._cell);
			}
			Controller controller = node._cell.GetController("expanded");
			if (controller != null)
			{
				controller.selectedIndex = 0;
			}
			if (node._cell.parent != null)
			{
				HideFolderNode(node);
			}
		}
	}

	internal void _AfterMoved(GTreeNode node)
	{
		int childIndex = GetChildIndex(node._cell);
		int num = ((!node.isFolder) ? (childIndex + 1) : GetFolderEndIndex(childIndex, node.level));
		int insertIndexForNode = GetInsertIndexForNode(node);
		int num2 = num - childIndex;
		if (insertIndexForNode < childIndex)
		{
			for (int i = 0; i < num2; i++)
			{
				GObject childAt = GetChildAt(childIndex + i);
				SetChildIndex(childAt, insertIndexForNode + i);
			}
		}
		else
		{
			for (int j = 0; j < num2; j++)
			{
				GObject childAt2 = GetChildAt(childIndex);
				SetChildIndex(childAt2, insertIndexForNode);
			}
		}
	}

	private int GetFolderEndIndex(int startIndex, int level)
	{
		int num = base.numChildren;
		for (int i = startIndex + 1; i < num; i++)
		{
			if (GetChildAt(i)._treeNode.level <= level)
			{
				return i;
			}
		}
		return num;
	}

	private int CheckChildren(GTreeNode folderNode, int index)
	{
		int num = folderNode.numChildren;
		for (int i = 0; i < num; i++)
		{
			index++;
			GTreeNode childAt = folderNode.GetChildAt(i);
			if (childAt.cell == null)
			{
				CreateCell(childAt);
			}
			if (childAt.cell.parent == null)
			{
				AddChildAt(childAt.cell, index);
			}
			if (childAt.isFolder && childAt.expanded)
			{
				index = CheckChildren(childAt, index);
			}
		}
		return index;
	}

	private void HideFolderNode(GTreeNode folderNode)
	{
		int num = folderNode.numChildren;
		for (int i = 0; i < num; i++)
		{
			GTreeNode childAt = folderNode.GetChildAt(i);
			if (childAt.cell != null && childAt.cell.parent != null)
			{
				RemoveChild(childAt.cell);
			}
			if (childAt.isFolder && childAt.expanded)
			{
				HideFolderNode(childAt);
			}
		}
	}

	private void RemoveNode(GTreeNode node)
	{
		if (node.cell != null)
		{
			if (node.cell.parent != null)
			{
				RemoveChild(node.cell);
			}
			base.itemPool.ReturnObject(node.cell);
			node._cell._treeNode = null;
			node._cell = null;
		}
		if (node.isFolder)
		{
			int num = node.numChildren;
			for (int i = 0; i < num; i++)
			{
				GTreeNode childAt = node.GetChildAt(i);
				RemoveNode(childAt);
			}
		}
	}

	private void __cellTouchBegin(EventContext context)
	{
		GTreeNode gTreeNode = ((GObject)context.sender)._treeNode;
		_expandedStatusInEvt = gTreeNode.expanded;
	}

	private void __expandedStateChanged(EventContext context)
	{
		Controller controller = (Controller)context.sender;
		controller.parent._treeNode.expanded = controller.selectedIndex == 1;
	}

	protected override void DispatchItemEvent(GObject item, EventContext context)
	{
		if (_clickToExpand != 0)
		{
			GTreeNode gTreeNode = item._treeNode;
			if (gTreeNode != null && _expandedStatusInEvt == gTreeNode.expanded)
			{
				if (_clickToExpand == 2)
				{
					if (context.inputEvent.isDoubleClick)
					{
						gTreeNode.expanded = !gTreeNode.expanded;
					}
				}
				else
				{
					gTreeNode.expanded = !gTreeNode.expanded;
				}
			}
		}
		base.DispatchItemEvent(item, context);
	}

	public override void Setup_BeforeAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_BeforeAdd(buffer, beginPos);
		buffer.Seek(beginPos, 9);
		_indent = buffer.ReadInt();
		_clickToExpand = buffer.ReadByte();
	}

	protected override void ReadItems(ByteBuffer buffer)
	{
		GTreeNode gTreeNode = null;
		int num = 0;
		int num2 = buffer.ReadShort();
		for (int i = 0; i < num2; i++)
		{
			int num3 = buffer.ReadShort();
			num3 += buffer.position;
			string text = buffer.ReadS();
			if (text == null)
			{
				text = defaultItem;
				if (text == null)
				{
					buffer.position = num3;
					continue;
				}
			}
			bool hasChild = buffer.ReadBool();
			int num4 = buffer.ReadByte();
			GTreeNode gTreeNode2 = new GTreeNode(hasChild, text);
			gTreeNode2.expanded = true;
			if (i == 0)
			{
				_rootNode.AddChild(gTreeNode2);
			}
			else if (num4 > num)
			{
				gTreeNode.AddChild(gTreeNode2);
			}
			else if (num4 < num)
			{
				for (int j = num4; j <= num; j++)
				{
					gTreeNode = gTreeNode.parent;
				}
				gTreeNode.AddChild(gTreeNode2);
			}
			else
			{
				gTreeNode.parent.AddChild(gTreeNode2);
			}
			gTreeNode = gTreeNode2;
			num = num4;
			SetupItem(buffer, gTreeNode2.cell);
			buffer.position = num3;
		}
	}
}
