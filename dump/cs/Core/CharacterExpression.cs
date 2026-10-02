using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using UI;
using UnityEngine;

namespace Core;

public class CharacterExpression
{
	public readonly int _roleId;

	public readonly CharacterExpressionPackConfigure _ExpressionPackConfig;

	private readonly Dictionary<int, ExpressionData> _ExpressionDict;

	public Dictionary<int, ExpressionData> expressionDict => _ExpressionDict;

	public CharacterExpression(int roleId)
	{
		_roleId = roleId;
		_ExpressionDict = new Dictionary<int, ExpressionData>();
		CharacterInfoConfigure heroCharacterConfigure = CharacterHandle.GetHeroCharacterConfigure(_roleId);
		if (heroCharacterConfigure == null)
		{
			Debug.LogError($"初始化表情数据时刻，未能通过roleID{roleId}获取角色配置表");
		}
		else if (!StaticConfigure.Character.ExpressionPackDict.TryGetValue(heroCharacterConfigure.ExpressionPackID, out _ExpressionPackConfig))
		{
			Debug.LogError($"初始化表情数据时刻，未能通过表情包ID{heroCharacterConfigure.ExpressionPackID}获取角色表情配置表");
		}
	}

	public async UniTask ReadyExpression(bool checkStatus)
	{
		if (_ExpressionPackConfig == null)
		{
			Debug.LogError("加载表情数据时刻，发现当前表情数据配置为空，未能正确加载");
			return;
		}
		foreach (CharacterExpressionPackConfigureItem expressionConfigItem in _ExpressionPackConfig.CharacterExpressionPackConfigureItems)
		{
			ItemInfoConfigure itemInfoConfigure = expressionConfigItem.ItemID.GetItemInfoConfigure();
			if (itemInfoConfigure != null && itemInfoConfigure.IsClientShow && (!checkStatus || itemInfoConfigure.IsVailItem()))
			{
				_ExpressionDict.Remove(expressionConfigItem.ItemID);
				ExpressionData expressionData = new ExpressionData(_ExpressionPackConfig.Id, expressionConfigItem);
				await expressionData.LoadResource();
				_ExpressionDict.TryAdd(expressionConfigItem.ItemID, expressionData);
			}
		}
	}

	public ExpressionData GetExpressionData(int itemId)
	{
		return _ExpressionDict.GetValueOrDefault(itemId);
	}

	public void Dispose()
	{
		if (_ExpressionPackConfig == null)
		{
			return;
		}
		if (_ExpressionDict.Count > 0)
		{
			foreach (KeyValuePair<int, ExpressionData> item in _ExpressionDict)
			{
				item.Value.Dispose();
			}
		}
		_ExpressionDict.Clear();
		string packageIdOrName = "Expression" + _ExpressionPackConfig.Id;
		if (UIPackage.ExistPackage(packageIdOrName))
		{
			UIPackage.RemovePackage(packageIdOrName);
		}
	}
}
