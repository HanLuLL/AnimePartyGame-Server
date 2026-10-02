using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace GameLogic;

public class ShowingFashion
{
	private MapField<int, int> _fashion;

	private int _planID;

	private int _headShotId;

	private int _labelId;

	private int _cardId;

	private int _diceId;

	private int _killEffectId;

	private int _KvId;

	public int planID => _planID;

	public int headShotId => _headShotId;

	public int labelId => _labelId;

	public int cardId => _cardId;

	public int diceId => _diceId;

	public int killEffectId => _killEffectId;

	public int KvId => _KvId;

	public void UpdatePlan(int plan, MapField<int, int> fashion)
	{
		_planID = plan;
		_fashion = fashion;
		SetFashion();
	}

	public MapField<int, int> HandleInfo()
	{
		return new MapField<int, int>
		{
			{ 1, _headShotId },
			{ 2, _labelId },
			{ 3, _cardId },
			{ 4, _diceId },
			{ 5, _killEffectId },
			{ 6, _KvId }
		};
	}

	public void UpdateItemId(int type, int itemId)
	{
		switch (type)
		{
		case 1:
			_headShotId = itemId;
			break;
		case 2:
			_labelId = itemId;
			break;
		case 3:
			_cardId = itemId;
			break;
		case 4:
			_diceId = itemId;
			break;
		case 5:
			_killEffectId = itemId;
			break;
		case 6:
			_KvId = itemId;
			break;
		case 0:
			break;
		}
	}

	public void SetFashion()
	{
		if (!_fashion.TryGetValue(1, out _headShotId))
		{
			Debug.LogError($"当前取出装扮{_planID}的头像配置id失败，存在问题");
		}
		if (!_fashion.TryGetValue(2, out _labelId))
		{
			Debug.LogError($"当前取出装扮{_planID}的名牌配置id失败，存在问题");
		}
		if (!_fashion.TryGetValue(3, out _cardId))
		{
			Debug.LogError($"当前取出装扮{_planID}的卡牌配置id失败，存在问题");
		}
		if (!_fashion.TryGetValue(4, out _diceId))
		{
			Debug.LogError($"当前取出装扮{_planID}的骰子配置id失败，存在问题");
		}
		if (!_fashion.TryGetValue(5, out _killEffectId))
		{
			Debug.LogError($"当前取出装扮{_planID}的击杀特效配置id失败，存在问题");
		}
		if (!_fashion.TryGetValue(6, out _KvId))
		{
			_KvId = 0;
		}
	}

	public bool IsChange()
	{
		if (_fashion.TryGetValue(1, out var value) && _headShotId != value)
		{
			return true;
		}
		if (_fashion.TryGetValue(2, out var value2) && _labelId != value2)
		{
			return true;
		}
		if (_fashion.TryGetValue(3, out var value3) && _cardId != value3)
		{
			return true;
		}
		if (_fashion.TryGetValue(4, out var value4) && _diceId != value4)
		{
			return true;
		}
		if (_fashion.TryGetValue(5, out var value5) && _killEffectId != value5)
		{
			return true;
		}
		if (_fashion.TryGetValue(6, out var value6) && _KvId != value6)
		{
			return true;
		}
		return false;
	}

	public bool Savelicence()
	{
		if (Singlelicence(_headShotId) && Singlelicence(_labelId) && Singlelicence(_cardId) && Singlelicence(_diceId) && Singlelicence(_killEffectId) && Singlelicence(_KvId))
		{
			return true;
		}
		return false;
	}

	private bool Singlelicence(int ItemId)
	{
		if (ItemId == 0)
		{
			return true;
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(ItemId);
	}
}
