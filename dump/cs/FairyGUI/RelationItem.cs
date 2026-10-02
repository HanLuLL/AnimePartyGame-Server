using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

internal class RelationItem
{
	private GObject _owner;

	private GObject _target;

	private List<RelationDef> _defs;

	private Vector4 _targetData;

	public GObject target
	{
		get
		{
			return _target;
		}
		set
		{
			if (_target != value)
			{
				if (_target != null)
				{
					ReleaseRefTarget(_target);
				}
				_target = value;
				if (_target != null)
				{
					AddRefTarget(_target);
				}
			}
		}
	}

	public bool isEmpty => _defs.Count == 0;

	public RelationItem(GObject owner)
	{
		_owner = owner;
		_defs = new List<RelationDef>();
	}

	public void Add(RelationType relationType, bool usePercent)
	{
		if (relationType == RelationType.Size)
		{
			Add(RelationType.Width, usePercent);
			Add(RelationType.Height, usePercent);
			return;
		}
		int count = _defs.Count;
		for (int i = 0; i < count; i++)
		{
			if (_defs[i].type == relationType)
			{
				return;
			}
		}
		InternalAdd(relationType, usePercent);
	}

	public void InternalAdd(RelationType relationType, bool usePercent)
	{
		if (relationType == RelationType.Size)
		{
			InternalAdd(RelationType.Width, usePercent);
			InternalAdd(RelationType.Height, usePercent);
			return;
		}
		RelationDef relationDef = new RelationDef();
		relationDef.percent = usePercent;
		relationDef.type = relationType;
		relationDef.axis = ((relationType > RelationType.Right_Right && relationType != RelationType.Width && (relationType < RelationType.LeftExt_Left || relationType > RelationType.RightExt_Right)) ? 1 : 0);
		_defs.Add(relationDef);
	}

	public void Remove(RelationType relationType)
	{
		if (relationType == RelationType.Size)
		{
			Remove(RelationType.Width);
			Remove(RelationType.Height);
			return;
		}
		int count = _defs.Count;
		for (int i = 0; i < count; i++)
		{
			if (_defs[i].type == relationType)
			{
				_defs.RemoveAt(i);
				break;
			}
		}
	}

	public void CopyFrom(RelationItem source)
	{
		target = source.target;
		_defs.Clear();
		foreach (RelationDef def in source._defs)
		{
			RelationDef relationDef = new RelationDef();
			relationDef.copyFrom(def);
			_defs.Add(relationDef);
		}
	}

	public void Dispose()
	{
		if (_target != null)
		{
			ReleaseRefTarget(_target);
			_target = null;
		}
	}

	public void ApplyOnSelfSizeChanged(float dWidth, float dHeight, bool applyPivot)
	{
		int count = _defs.Count;
		if (count == 0)
		{
			return;
		}
		float x = _owner.x;
		float y = _owner.y;
		for (int i = 0; i < count; i++)
		{
			switch (_defs[i].type)
			{
			case RelationType.Center_Center:
				_owner.x -= (0.5f - (applyPivot ? _owner.pivotX : 0f)) * dWidth;
				break;
			case RelationType.Right_Left:
			case RelationType.Right_Center:
			case RelationType.Right_Right:
				_owner.x -= (1f - (applyPivot ? _owner.pivotX : 0f)) * dWidth;
				break;
			case RelationType.Middle_Middle:
				_owner.y -= (0.5f - (applyPivot ? _owner.pivotY : 0f)) * dHeight;
				break;
			case RelationType.Bottom_Top:
			case RelationType.Bottom_Middle:
			case RelationType.Bottom_Bottom:
				_owner.y -= (1f - (applyPivot ? _owner.pivotY : 0f)) * dHeight;
				break;
			}
		}
		if (Mathf.Approximately(x, _owner.x) && Mathf.Approximately(y, _owner.y))
		{
			return;
		}
		x = _owner.x - x;
		y = _owner.y - y;
		_owner.UpdateGearFromRelations(1, x, y);
		if (_owner.parent != null)
		{
			int count2 = _owner.parent._transitions.Count;
			for (int j = 0; j < count2; j++)
			{
				_owner.parent._transitions[j].UpdateFromRelations(_owner.id, x, y);
			}
		}
	}

	private void ApplyOnXYChanged(RelationDef info, float dx, float dy)
	{
		switch (info.type)
		{
		case RelationType.Left_Left:
		case RelationType.Left_Center:
		case RelationType.Left_Right:
		case RelationType.Center_Center:
		case RelationType.Right_Left:
		case RelationType.Right_Center:
		case RelationType.Right_Right:
			_owner.x += dx;
			break;
		case RelationType.Top_Top:
		case RelationType.Top_Middle:
		case RelationType.Top_Bottom:
		case RelationType.Middle_Middle:
		case RelationType.Bottom_Top:
		case RelationType.Bottom_Middle:
		case RelationType.Bottom_Bottom:
			_owner.y += dy;
			break;
		case RelationType.LeftExt_Left:
		case RelationType.LeftExt_Right:
			if (_owner != _target.parent)
			{
				float yMin = _owner.xMin;
				_owner.width = _owner._rawWidth - dx;
				_owner.xMin = yMin + dx;
			}
			else
			{
				_owner.width = _owner._rawWidth - dx;
			}
			break;
		case RelationType.RightExt_Left:
		case RelationType.RightExt_Right:
			if (_owner != _target.parent)
			{
				float yMin = _owner.xMin;
				_owner.width = _owner._rawWidth + dx;
				_owner.xMin = yMin;
			}
			else
			{
				_owner.width = _owner._rawWidth + dx;
			}
			break;
		case RelationType.TopExt_Top:
		case RelationType.TopExt_Bottom:
			if (_owner != _target.parent)
			{
				float yMin = _owner.yMin;
				_owner.height = _owner._rawHeight - dy;
				_owner.yMin = yMin + dy;
			}
			else
			{
				_owner.height = _owner._rawHeight - dy;
			}
			break;
		case RelationType.BottomExt_Top:
		case RelationType.BottomExt_Bottom:
			if (_owner != _target.parent)
			{
				float yMin = _owner.yMin;
				_owner.height = _owner._rawHeight + dy;
				_owner.yMin = yMin;
			}
			else
			{
				_owner.height = _owner._rawHeight + dy;
			}
			break;
		case RelationType.Width:
		case RelationType.Height:
			break;
		}
	}

	private void ApplyOnSizeChanged(RelationDef info)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		if (info.axis == 0)
		{
			if (_target != _owner.parent)
			{
				num = _target.x;
				if (_target.pivotAsAnchor)
				{
					num2 = _target.pivotX;
				}
			}
			if (info.percent)
			{
				if (_targetData.z != 0f)
				{
					num3 = _target._width / _targetData.z;
				}
			}
			else
			{
				num3 = _target._width - _targetData.z;
			}
		}
		else
		{
			if (_target != _owner.parent)
			{
				num = _target.y;
				if (_target.pivotAsAnchor)
				{
					num2 = _target.pivotY;
				}
			}
			if (info.percent)
			{
				if (_targetData.w != 0f)
				{
					num3 = _target._height / _targetData.w;
				}
			}
			else
			{
				num3 = _target._height - _targetData.w;
			}
		}
		switch (info.type)
		{
		case RelationType.Left_Left:
			if (info.percent)
			{
				_owner.xMin = num + (_owner.xMin - num) * num3;
			}
			else if (num2 != 0f)
			{
				_owner.x += num3 * (0f - num2);
			}
			break;
		case RelationType.Left_Center:
			if (info.percent)
			{
				_owner.xMin = num + (_owner.xMin - num) * num3;
			}
			else
			{
				_owner.x += num3 * (0.5f - num2);
			}
			break;
		case RelationType.Left_Right:
			if (info.percent)
			{
				_owner.xMin = num + (_owner.xMin - num) * num3;
			}
			else
			{
				_owner.x += num3 * (1f - num2);
			}
			break;
		case RelationType.Center_Center:
			if (info.percent)
			{
				_owner.xMin = num + (_owner.xMin + _owner._rawWidth * 0.5f - num) * num3 - _owner._rawWidth * 0.5f;
			}
			else
			{
				_owner.x += num3 * (0.5f - num2);
			}
			break;
		case RelationType.Right_Left:
			if (info.percent)
			{
				_owner.xMin = num + (_owner.xMin + _owner._rawWidth - num) * num3 - _owner._rawWidth;
			}
			else if (num2 != 0f)
			{
				_owner.x += num3 * (0f - num2);
			}
			break;
		case RelationType.Right_Center:
			if (info.percent)
			{
				_owner.xMin = num + (_owner.xMin + _owner._rawWidth - num) * num3 - _owner._rawWidth;
			}
			else
			{
				_owner.x += num3 * (0.5f - num2);
			}
			break;
		case RelationType.Right_Right:
			if (info.percent)
			{
				_owner.xMin = num + (_owner.xMin + _owner._rawWidth - num) * num3 - _owner._rawWidth;
			}
			else
			{
				_owner.x += num3 * (1f - num2);
			}
			break;
		case RelationType.Top_Top:
			if (info.percent)
			{
				_owner.yMin = num + (_owner.yMin - num) * num3;
			}
			else if (num2 != 0f)
			{
				_owner.y += num3 * (0f - num2);
			}
			break;
		case RelationType.Top_Middle:
			if (info.percent)
			{
				_owner.yMin = num + (_owner.yMin - num) * num3;
			}
			else
			{
				_owner.y += num3 * (0.5f - num2);
			}
			break;
		case RelationType.Top_Bottom:
			if (info.percent)
			{
				_owner.yMin = num + (_owner.yMin - num) * num3;
			}
			else
			{
				_owner.y += num3 * (1f - num2);
			}
			break;
		case RelationType.Middle_Middle:
			if (info.percent)
			{
				_owner.yMin = num + (_owner.yMin + _owner._rawHeight * 0.5f - num) * num3 - _owner._rawHeight * 0.5f;
			}
			else
			{
				_owner.y += num3 * (0.5f - num2);
			}
			break;
		case RelationType.Bottom_Top:
			if (info.percent)
			{
				_owner.yMin = num + (_owner.yMin + _owner._rawHeight - num) * num3 - _owner._rawHeight;
			}
			else if (num2 != 0f)
			{
				_owner.y += num3 * (0f - num2);
			}
			break;
		case RelationType.Bottom_Middle:
			if (info.percent)
			{
				_owner.yMin = num + (_owner.yMin + _owner._rawHeight - num) * num3 - _owner._rawHeight;
			}
			else
			{
				_owner.y += num3 * (0.5f - num2);
			}
			break;
		case RelationType.Bottom_Bottom:
			if (info.percent)
			{
				_owner.yMin = num + (_owner.yMin + _owner._rawHeight - num) * num3 - _owner._rawHeight;
			}
			else
			{
				_owner.y += num3 * (1f - num2);
			}
			break;
		case RelationType.Width:
		{
			float num4 = ((!_owner.underConstruct || _owner != _target.parent) ? (_owner._rawWidth - _targetData.z) : ((float)(_owner.sourceWidth - _target.initWidth)));
			if (info.percent)
			{
				num4 *= num3;
			}
			if (_target == _owner.parent)
			{
				if (_owner.pivotAsAnchor)
				{
					float yMin = _owner.xMin;
					_owner.SetSize(_target._width + num4, _owner._rawHeight, ignorePivot: true);
					_owner.xMin = yMin;
				}
				else
				{
					_owner.SetSize(_target._width + num4, _owner._rawHeight, ignorePivot: true);
				}
			}
			else
			{
				_owner.width = _target._width + num4;
			}
			break;
		}
		case RelationType.Height:
		{
			float num4 = ((!_owner.underConstruct || _owner != _target.parent) ? (_owner._rawHeight - _targetData.w) : ((float)(_owner.sourceHeight - _target.initHeight)));
			if (info.percent)
			{
				num4 *= num3;
			}
			if (_target == _owner.parent)
			{
				if (_owner.pivotAsAnchor)
				{
					float yMin = _owner.yMin;
					_owner.SetSize(_owner._rawWidth, _target._height + num4, ignorePivot: true);
					_owner.yMin = yMin;
				}
				else
				{
					_owner.SetSize(_owner._rawWidth, _target._height + num4, ignorePivot: true);
				}
			}
			else
			{
				_owner.height = _target._height + num4;
			}
			break;
		}
		case RelationType.LeftExt_Left:
		{
			float yMin = _owner.xMin;
			float num4 = ((!info.percent) ? (num3 * (0f - num2)) : (num + (yMin - num) * num3 - yMin));
			_owner.width = _owner._rawWidth - num4;
			_owner.xMin = yMin + num4;
			break;
		}
		case RelationType.LeftExt_Right:
		{
			float yMin = _owner.xMin;
			float num4 = ((!info.percent) ? (num3 * (1f - num2)) : (num + (yMin - num) * num3 - yMin));
			_owner.width = _owner._rawWidth - num4;
			_owner.xMin = yMin + num4;
			break;
		}
		case RelationType.RightExt_Left:
		{
			float yMin = _owner.xMin;
			float num4 = ((!info.percent) ? (num3 * (0f - num2)) : (num + (yMin + _owner._rawWidth - num) * num3 - (yMin + _owner._rawWidth)));
			_owner.width = _owner._rawWidth + num4;
			_owner.xMin = yMin;
			break;
		}
		case RelationType.RightExt_Right:
		{
			float yMin = _owner.xMin;
			if (info.percent)
			{
				if (_owner == _target.parent)
				{
					if (_owner.underConstruct)
					{
						_owner.width = num + _target._width - _target._width * num2 + ((float)_owner.sourceWidth - num - (float)_target.initWidth + (float)_target.initWidth * num2) * num3;
					}
					else
					{
						_owner.width = num + (_owner._rawWidth - num) * num3;
					}
				}
				else
				{
					float num4 = num + (yMin + _owner._rawWidth - num) * num3 - (yMin + _owner._rawWidth);
					_owner.width = _owner._rawWidth + num4;
					_owner.xMin = yMin;
				}
			}
			else if (_owner == _target.parent)
			{
				if (_owner.underConstruct)
				{
					_owner.width = (float)_owner.sourceWidth + (_target._width - (float)_target.initWidth) * (1f - num2);
				}
				else
				{
					_owner.width = _owner._rawWidth + num3 * (1f - num2);
				}
			}
			else
			{
				float num4 = num3 * (1f - num2);
				_owner.width = _owner._rawWidth + num4;
				_owner.xMin = yMin;
			}
			break;
		}
		case RelationType.TopExt_Top:
		{
			float yMin = _owner.yMin;
			float num4 = ((!info.percent) ? (num3 * (0f - num2)) : (num + (yMin - num) * num3 - yMin));
			_owner.height = _owner._rawHeight - num4;
			_owner.yMin = yMin + num4;
			break;
		}
		case RelationType.TopExt_Bottom:
		{
			float yMin = _owner.yMin;
			float num4 = ((!info.percent) ? (num3 * (1f - num2)) : (num + (yMin - num) * num3 - yMin));
			_owner.height = _owner._rawHeight - num4;
			_owner.yMin = yMin + num4;
			break;
		}
		case RelationType.BottomExt_Top:
		{
			float yMin = _owner.yMin;
			float num4 = ((!info.percent) ? (num3 * (0f - num2)) : (num + (yMin + _owner._rawHeight - num) * num3 - (yMin + _owner._rawHeight)));
			_owner.height = _owner._rawHeight + num4;
			_owner.yMin = yMin;
			break;
		}
		case RelationType.BottomExt_Bottom:
		{
			float yMin = _owner.yMin;
			if (info.percent)
			{
				if (_owner == _target.parent)
				{
					if (_owner.underConstruct)
					{
						_owner.height = num + _target._height - _target._height * num2 + ((float)_owner.sourceHeight - num - (float)_target.initHeight + (float)_target.initHeight * num2) * num3;
					}
					else
					{
						_owner.height = num + (_owner._rawHeight - num) * num3;
					}
				}
				else
				{
					float num4 = num + (yMin + _owner._rawHeight - num) * num3 - (yMin + _owner._rawHeight);
					_owner.height = _owner._rawHeight + num4;
					_owner.yMin = yMin;
				}
			}
			else if (_owner == _target.parent)
			{
				if (_owner.underConstruct)
				{
					_owner.height = (float)_owner.sourceHeight + (_target._height - (float)_target.initHeight) * (1f - num2);
				}
				else
				{
					_owner.height = _owner._rawHeight + num3 * (1f - num2);
				}
			}
			else
			{
				float num4 = num3 * (1f - num2);
				_owner.height = _owner._rawHeight + num4;
				_owner.yMin = yMin;
			}
			break;
		}
		}
	}

	private void AddRefTarget(GObject target)
	{
		if (target != _owner.parent)
		{
			target.onPositionChanged.Add(__targetXYChanged);
		}
		target.onSizeChanged.Add(__targetSizeChanged);
		_targetData.x = _target.x;
		_targetData.y = _target.y;
		_targetData.z = _target._width;
		_targetData.w = _target._height;
	}

	private void ReleaseRefTarget(GObject target)
	{
		target.onPositionChanged.Remove(__targetXYChanged);
		target.onSizeChanged.Remove(__targetSizeChanged);
	}

	private void __targetXYChanged(EventContext context)
	{
		if (_owner.relations.handling != null || (_owner.group != null && _owner.group._updating != 0))
		{
			_targetData.x = _target.x;
			_targetData.y = _target.y;
			return;
		}
		_owner.relations.handling = (GObject)context.sender;
		float x = _owner.x;
		float y = _owner.y;
		float dx = _target.x - _targetData.x;
		float dy = _target.y - _targetData.y;
		int count = _defs.Count;
		for (int i = 0; i < count; i++)
		{
			ApplyOnXYChanged(_defs[i], dx, dy);
		}
		_targetData.x = _target.x;
		_targetData.y = _target.y;
		if (!Mathf.Approximately(x, _owner.x) || !Mathf.Approximately(y, _owner.y))
		{
			x = _owner.x - x;
			y = _owner.y - y;
			_owner.UpdateGearFromRelations(1, x, y);
			if (_owner.parent != null)
			{
				int count2 = _owner.parent._transitions.Count;
				for (int j = 0; j < count2; j++)
				{
					_owner.parent._transitions[j].UpdateFromRelations(_owner.id, x, y);
				}
			}
		}
		_owner.relations.handling = null;
	}

	private void __targetSizeChanged(EventContext context)
	{
		if (_owner.relations.handling != null || (_owner.group != null && _owner.group._updating != 0))
		{
			_targetData.z = _target._width;
			_targetData.w = _target._height;
			return;
		}
		_owner.relations.handling = (GObject)context.sender;
		float x = _owner.x;
		float y = _owner.y;
		float rawWidth = _owner._rawWidth;
		float rawHeight = _owner._rawHeight;
		int count = _defs.Count;
		for (int i = 0; i < count; i++)
		{
			ApplyOnSizeChanged(_defs[i]);
		}
		_targetData.z = _target._width;
		_targetData.w = _target._height;
		if (!Mathf.Approximately(x, _owner.x) || !Mathf.Approximately(y, _owner.y))
		{
			x = _owner.x - x;
			y = _owner.y - y;
			_owner.UpdateGearFromRelations(1, x, y);
			if (_owner.parent != null)
			{
				int count2 = _owner.parent._transitions.Count;
				for (int j = 0; j < count2; j++)
				{
					_owner.parent._transitions[j].UpdateFromRelations(_owner.id, x, y);
				}
			}
		}
		if (!Mathf.Approximately(rawWidth, _owner._rawWidth) || !Mathf.Approximately(rawHeight, _owner._rawHeight))
		{
			rawWidth = _owner._rawWidth - rawWidth;
			rawHeight = _owner._rawHeight - rawHeight;
			_owner.UpdateGearFromRelations(2, rawWidth, rawHeight);
		}
		_owner.relations.handling = null;
	}
}
