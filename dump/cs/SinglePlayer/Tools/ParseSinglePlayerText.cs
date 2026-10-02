using System.Text.RegularExpressions;
using SinglePlayer.GamePlay.Build;
using Tools;
using UI;
using UnityEngine;

namespace SinglePlayer.Tools;

public static class ParseSinglePlayerText
{
	private static readonly Regex AstralRegex = new Regex("\\[Astral\\](.*?)\\[/Astral\\]", RegexOptions.Compiled);

	public static string ParseAstralCardDesc(int cardId, int cardLevel)
	{
		if (StaticConfigure.SinglePlayer.CardDict.TryGetValue(cardId, out var value))
		{
			string local = value.DesID.GetLocal(UIStringType.SinglePlayer);
			SinglePlayerCardConfigureItem cardItemConfig = value.SinglePlayerCardConfigureItems.GetSafeByIndex(cardLevel - 1);
			if (cardItemConfig == null)
			{
				return "";
			}
			return AstralRegex.Replace(local, delegate(Match match)
			{
				string value2 = match.Groups[1].Value;
				if (value2 == "addPrice")
				{
					return cardItemConfig.AddPrice.ToString();
				}
				string[] array = value2.Split(':');
				if (array.Length != 2)
				{
					Debug.LogError("非法 Astral 表达式: " + value2);
				}
				string text = array[0];
				int.TryParse(array[1].Split('*')[0].Trim(), out var result);
				return (text switch
				{
					"TriggerPoint" => cardItemConfig.TriggerPoint.GetSafeByIndex(result), 
					"TriggerParam" => cardItemConfig.TriggerParam.GetSafeByIndex(result), 
					"OtherParam" => cardItemConfig.OtherParam.GetSafeByIndex(result), 
					"StopParam" => cardItemConfig.StopParam.GetSafeByIndex(result), 
					"WalkParam" => cardItemConfig.WalkParam.GetSafeByIndex(result), 
					_ => 0, 
				}).ToString();
			});
		}
		return "";
	}

	public static string ParseAstralBuildingDesc(BuildingBase building)
	{
		string local = building.Card.CardConfigure.DesID.GetLocal(UIStringType.SinglePlayer);
		SinglePlayerCardConfigureItem cardItemConfig = building.GetConfigureItem();
		return AstralRegex.Replace(local, delegate(Match match)
		{
			string value = match.Groups[1].Value;
			if (value == "addPrice")
			{
				return cardItemConfig.AddPrice.ToString();
			}
			string[] array = value.Split(':');
			if (array.Length != 2)
			{
				Debug.LogError("非法 Astral 表达式: " + value);
			}
			string text = array[0];
			string text2 = array[1];
			int result = 0;
			int num = 0;
			if (text2.Contains('*'))
			{
				string[] array2 = text2.Split('*');
				int.TryParse(array2[0], out result);
				if (array2[1] == "Gold")
				{
					num = text switch
					{
						"TriggerPoint" => cardItemConfig.TriggerPoint.GetSafeByIndex(result), 
						"OtherParam" => cardItemConfig.OtherParam.GetSafeByIndex(result), 
						"TriggerParam" => building.GetGoldMultiValue(result, ParameterType.ThrowDice), 
						"StopParam" => building.GetGoldMultiValue(result, ParameterType.Stay), 
						"WalkParam" => building.GetGoldMultiValue(result, ParameterType.Pass), 
						_ => 0, 
					};
					if (text == "TriggerParam" && building.HasAttributeValue(OperateType.ThrowDiceGold) != 0)
					{
						return "[color=#0078ff]" + num + "[/color]";
					}
					if (text == "StopParam" && building.HasAttributeValue(OperateType.StayGold) != 0)
					{
						return "[color=#0078ff]" + num + "[/color]";
					}
					if (text == "WalkParam" && building.HasAttributeValue(OperateType.PassGold) != 0)
					{
						return "[color=#0078ff]" + num + "[/color]";
					}
				}
				else
				{
					num = text switch
					{
						"TriggerPoint" => cardItemConfig.TriggerPoint.GetSafeByIndex(result), 
						"OtherParam" => cardItemConfig.OtherParam.GetSafeByIndex(result), 
						"TriggerParam" => building.GetConfigParam(ParameterType.ThrowDice, result), 
						"StopParam" => building.GetConfigParam(ParameterType.Stay, result), 
						"WalkParam" => building.GetConfigParam(ParameterType.Pass, result), 
						_ => 0, 
					};
				}
			}
			else
			{
				int.TryParse(text2, out result);
				num = text switch
				{
					"TriggerPoint" => cardItemConfig.TriggerPoint.GetSafeByIndex(result), 
					"OtherParam" => cardItemConfig.OtherParam.GetSafeByIndex(result), 
					"TriggerParam" => building.GetConfigParam(ParameterType.ThrowDice, result), 
					"StopParam" => building.GetConfigParam(ParameterType.Stay, result), 
					"WalkParam" => building.GetConfigParam(ParameterType.Pass, result), 
					_ => 0, 
				};
			}
			return num.ToString();
		});
	}
}
