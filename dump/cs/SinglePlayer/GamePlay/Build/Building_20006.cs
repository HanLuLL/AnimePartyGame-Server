using System.Collections.Generic;
using Google.Protobuf.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20006 : BuildingBase
{
	protected override void OnDice(int count)
	{
		base.OnDice(count);
		_ = Game.GetSystem<BoardManager>().gameManager.DicePoints;
		RepeatedField<int> repeatedField = GetConfigureItem()?.TriggerPoint;
		if (repeatedField == null || repeatedField.Count != 2)
		{
			Debug.LogError("建筑Building_20006 投点参数 不符合功能需求，请检查");
			return;
		}
		List<BuildingBase> buildingsByTags = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByTags(SinglePlayerTagType.Prosperity);
		List<BuildingBase> buildingsByTags2 = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByTags(SinglePlayerTagType.Dice);
		int diceSubTriggerCount = GetDiceSubTriggerCount(0);
		if (diceSubTriggerCount > 0)
		{
			this.ChangeGold(GetGoldMultiValue(0, ParameterType.ThrowDice, buildingsByTags.Count) * diceSubTriggerCount, BuildingShowType.ThrowDice);
		}
		int diceSubTriggerCount2 = GetDiceSubTriggerCount(1);
		if (diceSubTriggerCount2 > 0)
		{
			this.ChangeGold(GetGoldMultiValue(0, ParameterType.ThrowDice, buildingsByTags2.Count) * diceSubTriggerCount2, BuildingShowType.ThrowDice);
		}
	}

	protected override void OnStay(ref bool trigger)
	{
		base.OnStay(ref trigger);
	}
}
