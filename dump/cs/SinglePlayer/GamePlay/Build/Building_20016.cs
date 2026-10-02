using System.Collections.Generic;
using System.Linq;

namespace SinglePlayer.GamePlay.Build;

public class Building_20016 : BuildingBase
{
	protected override void OnDice(int count)
	{
		base.OnDice(count);
		int[] source = new int[2]
		{
			GetConfigParam(ParameterType.ThrowDice, 0),
			GetConfigParam(ParameterType.ThrowDice, 1)
		};
		List<BuildingBase> neighborBuildings = GetNeighborBuildings();
		for (int i = 0; i < count; i++)
		{
			foreach (BuildingBase item in neighborBuildings)
			{
				if (source.Contains(item.Card.CardConfigure.Id))
				{
					item.AddForceExecuteStayQueue();
				}
			}
		}
	}

	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		trigger = true;
		int goldMultiValue = GetGoldMultiValue(0, ParameterType.Pass);
		this.ChangeGold(goldMultiValue, BuildingShowType.Pass);
		int configParam = GetConfigParam(ParameterType.Pass, 1);
		Game.GetSystem<BoardManager>().cardManager.AddCardToBag(configParam);
	}
}
