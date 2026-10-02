using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20001 : BuildingBase
{
	protected override void OnStay(ref bool trigger)
	{
		base.OnStay(ref trigger);
		List<BuildingBase> neighborBuildings = GetNeighborBuildings();
		neighborBuildings.Add(this);
		for (int i = 0; i < neighborBuildings.Count; i++)
		{
			trigger = true;
			this.ChangeExp(neighborBuildings[i].Id, base.Id, GetConfigParam(ParameterType.Stay, 0), BuildingShowType.Stay);
		}
		Game.GetSystem<BoardManager>().characterManager.PlayAttributeShow().Forget();
	}

	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		this.ChangeGold(GetGoldMultiValue(0, ParameterType.Pass), BuildingShowType.Pass);
		trigger = true;
	}
}
