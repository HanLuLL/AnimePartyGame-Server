using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20011 : BuildingBase
{
	private readonly int[] PassEffectCardIds = new int[2] { 10002, 20005 };

	protected override void OnDice(int count)
	{
		base.OnDice(count);
		int configParam = GetConfigParam(ParameterType.ThrowDice, 0);
		List<BuildingBase> randomBuildings = Game.GetSystem<BoardManager>().buildingManager.GetRandomBuildings(configParam, this);
		randomBuildings.Add(this);
		for (int i = 0; i < randomBuildings.Count; i++)
		{
			this.ChangeExp(randomBuildings[i].Id, base.Id, GetConfigParam(ParameterType.ThrowDice, 1) * count, BuildingShowType.ThrowDice);
		}
	}

	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		int num = Random.Range(0, PassEffectCardIds.Length);
		Game.GetSystem<BoardManager>().cardManager.AddCardToBag(PassEffectCardIds[num]);
		trigger = true;
	}
}
