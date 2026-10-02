using System.Collections.Generic;
using SinglePlayer.GamePlay.Card;
using UnityEngine;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20019 : BuildingBase
{
	protected override void OnDice(int count)
	{
		base.OnDice(count);
		int diceSubTriggerCount = GetDiceSubTriggerCount(0);
		int diceSubTriggerCount2 = GetDiceSubTriggerCount(1);
		if (diceSubTriggerCount > 0)
		{
			for (int i = 0; i < diceSubTriggerCount; i++)
			{
				int configParam = GetConfigParam(ParameterType.ThrowDice, 0);
				int configParam2 = GetConfigParam(ParameterType.ThrowDice, 1);
				List<BuildingBase> buildingsByConfigId = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByConfigId(configParam);
				if (buildingsByConfigId.Count > 0)
				{
					int index = Random.Range(0, buildingsByConfigId.Count);
					SinglePlayer.GamePlay.Card.Card card = buildingsByConfigId[index]?.Card;
					if (card != null && Game.GetSystem<BoardManager>().cardManager.TryRemoveCardOnTheTable(card.UID))
					{
						int exp = card.ConvertToExp();
						this.ChangeExp(base.Id, base.Id, exp, BuildingShowType.ThrowDice);
						ChangeOperateBonus(OperateType.ThrowDiceGold, configParam2);
					}
				}
			}
		}
		if (diceSubTriggerCount2 > 0)
		{
			int gold = GetGoldMultiValue(2, ParameterType.ThrowDice) * diceSubTriggerCount2;
			this.ChangeGold(gold, BuildingShowType.ThrowDice);
		}
	}
}
