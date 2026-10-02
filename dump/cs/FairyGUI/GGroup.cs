using System;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GGroup : GObject
{
	private GroupLayoutType _layout;

	private int _lineGap;

	private int _columnGap;

	private bool _excludeInvisibles;

	private bool _autoSizeDisabled;

	private int _mainGridIndex;

	private int _mainGridMinSize;

	private bool _percentReady;

	private bool _boundsChanged;

	private int _mainChildIndex;

	private float _totalSize;

	private int _numChildren;

	internal int _updating;

	private Action _refreshDelegate;

	public GroupLayoutType layout
	{
		get
		{
			return _layout;
		}
		set
		{
			if (_layout != value)
			{
				_layout = value;
				SetBoundsChangedFlag();
			}
		}
	}

	public int lineGap
	{
		get
		{
			return _lineGap;
		}
		set
		{
			if (_lineGap != value)
			{
				_lineGap = value;
				SetBoundsChangedFlag(positionChangedOnly: true);
			}
		}
	}

	public int columnGap
	{
		get
		{
			return _columnGap;
		}
		set
		{
			if (_columnGap != value)
			{
				_columnGap = value;
				SetBoundsChangedFlag(positionChangedOnly: true);
			}
		}
	}

	public bool excludeInvisibles
	{
		get
		{
			return _excludeInvisibles;
		}
		set
		{
			if (_excludeInvisibles != value)
			{
				_excludeInvisibles = value;
				SetBoundsChangedFlag();
			}
		}
	}

	public bool autoSizeDisabled
	{
		get
		{
			return _autoSizeDisabled;
		}
		set
		{
			if (_autoSizeDisabled != value)
			{
				_autoSizeDisabled = value;
				SetBoundsChangedFlag();
			}
		}
	}

	public int mainGridMinSize
	{
		get
		{
			return _mainGridMinSize;
		}
		set
		{
			if (_mainGridMinSize != value)
			{
				_mainGridMinSize = value;
				SetBoundsChangedFlag();
			}
		}
	}

	public int mainGridIndex
	{
		get
		{
			return _mainGridIndex;
		}
		set
		{
			if (_mainGridIndex != value)
			{
				_mainGridIndex = value;
				SetBoundsChangedFlag();
			}
		}
	}

	public GGroup()
	{
		_mainGridIndex = -1;
		_mainChildIndex = -1;
		_mainGridMinSize = 50;
		_refreshDelegate = EnsureBoundsCorrect;
	}

	public void SetBoundsChangedFlag(bool positionChangedOnly = false)
	{
		if (_updating != 0 || base.parent == null)
		{
			return;
		}
		if (!positionChangedOnly)
		{
			_percentReady = false;
		}
		if (!_boundsChanged)
		{
			_boundsChanged = true;
			if (_layout != GroupLayoutType.None)
			{
				UpdateContext.OnBegin -= _refreshDelegate;
				UpdateContext.OnBegin += _refreshDelegate;
			}
		}
	}

	public void EnsureBoundsCorrect()
	{
		if (base.parent != null && _boundsChanged)
		{
			UpdateContext.OnBegin -= _refreshDelegate;
			_boundsChanged = false;
			if (_autoSizeDisabled)
			{
				ResizeChildren(0f, 0f);
				return;
			}
			HandleLayout();
			UpdateBounds();
		}
	}

	private void UpdateBounds()
	{
		int numChildren = base.parent.numChildren;
		float num = 2.1474836E+09f;
		float num2 = 2.1474836E+09f;
		float num3 = -2.1474836E+09f;
		float num4 = -2.1474836E+09f;
		bool flag = true;
		bool flag2 = _layout != GroupLayoutType.None && _excludeInvisibles;
		for (int i = 0; i < numChildren; i++)
		{
			GObject childAt = base.parent.GetChildAt(i);
			if (childAt.group == this && (!flag2 || childAt.internalVisible3))
			{
				float num5 = childAt.xMin;
				if (num5 < num)
				{
					num = num5;
				}
				num5 = childAt.yMin;
				if (num5 < num2)
				{
					num2 = num5;
				}
				num5 = childAt.xMin + childAt.width;
				if (num5 > num3)
				{
					num3 = num5;
				}
				num5 = childAt.yMin + childAt.height;
				if (num5 > num4)
				{
					num4 = num5;
				}
				flag = false;
			}
		}
		float num6;
		float num7;
		if (!flag)
		{
			_updating |= 1;
			SetXY(num, num2);
			_updating &= 2;
			num6 = num3 - num;
			num7 = num4 - num2;
		}
		else
		{
			num6 = (num7 = 0f);
		}
		if ((_updating & 2) == 0)
		{
			_updating |= 2;
			SetSize(num6, num7);
			_updating &= 1;
		}
		else
		{
			_updating &= 1;
			ResizeChildren(_width - num6, _height - num7);
		}
	}

	private void HandleLayout()
	{
		_updating |= 1;
		if (_layout == GroupLayoutType.Horizontal)
		{
			float num = base.x;
			int numChildren = base.parent.numChildren;
			for (int i = 0; i < numChildren; i++)
			{
				GObject childAt = base.parent.GetChildAt(i);
				if (childAt.group == this && (!_excludeInvisibles || childAt.internalVisible3))
				{
					childAt.xMin = num;
					if (childAt.width != 0f)
					{
						num += childAt.width + (float)_columnGap;
					}
				}
			}
		}
		else if (_layout == GroupLayoutType.Vertical)
		{
			float num2 = base.y;
			int numChildren2 = base.parent.numChildren;
			for (int j = 0; j < numChildren2; j++)
			{
				GObject childAt2 = base.parent.GetChildAt(j);
				if (childAt2.group == this && (!_excludeInvisibles || childAt2.internalVisible3))
				{
					childAt2.yMin = num2;
					if (childAt2.height != 0f)
					{
						num2 += childAt2.height + (float)_lineGap;
					}
				}
			}
		}
		_updating &= 2;
	}

	internal void MoveChildren(float dx, float dy)
	{
		if ((_updating & 1) != 0 || base.parent == null)
		{
			return;
		}
		_updating |= 1;
		int numChildren = base.parent.numChildren;
		for (int i = 0; i < numChildren; i++)
		{
			GObject childAt = base.parent.GetChildAt(i);
			if (childAt.group == this)
			{
				childAt.SetXY(childAt.x + dx, childAt.y + dy);
			}
		}
		_updating &= 2;
	}

	internal void ResizeChildren(float dw, float dh)
	{
		if (_layout == GroupLayoutType.None || (_updating & 2) != 0 || base.parent == null)
		{
			return;
		}
		_updating |= 2;
		if (_boundsChanged)
		{
			_boundsChanged = false;
			if (!_autoSizeDisabled)
			{
				UpdateBounds();
				return;
			}
		}
		int numChildren = base.parent.numChildren;
		if (!_percentReady)
		{
			_percentReady = true;
			_numChildren = 0;
			_totalSize = 0f;
			_mainChildIndex = -1;
			int num = 0;
			for (int i = 0; i < numChildren; i++)
			{
				GObject childAt = base.parent.GetChildAt(i);
				if (childAt.group != this)
				{
					continue;
				}
				if (!_excludeInvisibles || childAt.internalVisible3)
				{
					if (num == _mainGridIndex)
					{
						_mainChildIndex = i;
					}
					_numChildren++;
					if (_layout == GroupLayoutType.Horizontal)
					{
						_totalSize += childAt.width;
					}
					else
					{
						_totalSize += childAt.height;
					}
				}
				num++;
			}
			if (_mainChildIndex != -1)
			{
				if (_layout == GroupLayoutType.Horizontal)
				{
					GObject childAt2 = base.parent.GetChildAt(_mainChildIndex);
					_totalSize += (float)_mainGridMinSize - childAt2.width;
					childAt2._sizePercentInGroup = (float)_mainGridMinSize / _totalSize;
				}
				else
				{
					GObject childAt3 = base.parent.GetChildAt(_mainChildIndex);
					_totalSize += (float)_mainGridMinSize - childAt3.height;
					childAt3._sizePercentInGroup = (float)_mainGridMinSize / _totalSize;
				}
			}
			for (int j = 0; j < numChildren; j++)
			{
				GObject childAt4 = base.parent.GetChildAt(j);
				if (childAt4.group == this && j != _mainChildIndex)
				{
					if (_totalSize > 0f)
					{
						childAt4._sizePercentInGroup = ((_layout == GroupLayoutType.Horizontal) ? childAt4.width : childAt4.height) / _totalSize;
					}
					else
					{
						childAt4._sizePercentInGroup = 0f;
					}
				}
			}
		}
		float num2 = 0f;
		float num3 = 1f;
		bool flag = false;
		if (_layout == GroupLayoutType.Horizontal)
		{
			num2 = base.width - (float)((_numChildren - 1) * _columnGap);
			if (_mainChildIndex != -1 && num2 >= _totalSize)
			{
				GObject childAt5 = base.parent.GetChildAt(_mainChildIndex);
				childAt5.SetSize(num2 - (_totalSize - (float)_mainGridMinSize), childAt5._rawHeight + dh, ignorePivot: true);
				num2 -= childAt5.width;
				num3 -= childAt5._sizePercentInGroup;
				flag = true;
			}
			float num4 = base.x;
			for (int k = 0; k < numChildren; k++)
			{
				GObject childAt6 = base.parent.GetChildAt(k);
				if (childAt6.group != this)
				{
					continue;
				}
				if (_excludeInvisibles && !childAt6.internalVisible3)
				{
					childAt6.SetSize(childAt6._rawWidth, childAt6._rawHeight + dh, ignorePivot: true);
					continue;
				}
				if (!flag || k != _mainChildIndex)
				{
					childAt6.SetSize(Mathf.Round(childAt6._sizePercentInGroup / num3 * num2), childAt6._rawHeight + dh, ignorePivot: true);
					num3 -= childAt6._sizePercentInGroup;
					num2 -= childAt6.width;
				}
				childAt6.xMin = num4;
				if (childAt6.width != 0f)
				{
					num4 += childAt6.width + (float)_columnGap;
				}
			}
		}
		else
		{
			num2 = base.height - (float)((_numChildren - 1) * _lineGap);
			if (_mainChildIndex != -1 && num2 >= _totalSize)
			{
				GObject childAt7 = base.parent.GetChildAt(_mainChildIndex);
				childAt7.SetSize(childAt7._rawWidth + dw, num2 - (_totalSize - (float)_mainGridMinSize), ignorePivot: true);
				num2 -= childAt7.height;
				num3 -= childAt7._sizePercentInGroup;
				flag = true;
			}
			float num5 = base.y;
			for (int l = 0; l < numChildren; l++)
			{
				GObject childAt8 = base.parent.GetChildAt(l);
				if (childAt8.group != this)
				{
					continue;
				}
				if (_excludeInvisibles && !childAt8.internalVisible3)
				{
					childAt8.SetSize(childAt8._rawWidth + dw, childAt8._rawHeight, ignorePivot: true);
					continue;
				}
				if (!flag || l != _mainChildIndex)
				{
					childAt8.SetSize(childAt8._rawWidth + dw, Mathf.Round(childAt8._sizePercentInGroup / num3 * num2), ignorePivot: true);
					num3 -= childAt8._sizePercentInGroup;
					num2 -= childAt8.height;
				}
				childAt8.yMin = num5;
				if (childAt8.height != 0f)
				{
					num5 += childAt8.height + (float)_lineGap;
				}
			}
		}
		_updating &= 1;
	}

	protected override void HandleAlphaChanged()
	{
		base.HandleAlphaChanged();
		if (underConstruct || base.parent == null)
		{
			return;
		}
		int numChildren = base.parent.numChildren;
		float num = base.alpha;
		for (int i = 0; i < numChildren; i++)
		{
			GObject childAt = base.parent.GetChildAt(i);
			if (childAt.group == this)
			{
				childAt.alpha = num;
			}
		}
	}

	protected internal override void HandleVisibleChanged()
	{
		if (base.parent == null)
		{
			return;
		}
		int numChildren = base.parent.numChildren;
		for (int i = 0; i < numChildren; i++)
		{
			GObject childAt = base.parent.GetChildAt(i);
			if (childAt.group == this)
			{
				childAt.HandleVisibleChanged();
			}
		}
	}

	public override void Setup_BeforeAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_BeforeAdd(buffer, beginPos);
		buffer.Seek(beginPos, 5);
		_layout = (GroupLayoutType)buffer.ReadByte();
		_lineGap = buffer.ReadInt();
		_columnGap = buffer.ReadInt();
		if (buffer.version >= 2)
		{
			_excludeInvisibles = buffer.ReadBool();
			_autoSizeDisabled = buffer.ReadBool();
			_mainGridIndex = buffer.ReadShort();
		}
	}

	public override void Setup_AfterAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_AfterAdd(buffer, beginPos);
		if (!base.visible)
		{
			HandleVisibleChanged();
		}
	}
}
