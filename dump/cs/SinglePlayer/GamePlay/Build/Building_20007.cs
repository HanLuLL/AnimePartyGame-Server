using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20007 : BuildingBase
{
	private readonly int[] EffectTargetBuildingIds = new int[2] { 10002, 20005 };

	private readonly int[] SellingBuildingIds = new int[2] { 10002, 20005 };

	protected override void OnCreate()
	{
		base.OnCreate();
		Game.GetModel<GlobalSignal>().SellingCard.AddListener(OnSellingCard);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Game.GetModel<GlobalSignal>().SellingCard.RemoveListener(OnSellingCard);
	}

	protected override void OnDice(int count)
	{
		base.OnDice(count);
		for (int i = 0; i < EffectTargetBuildingIds.Length; i++)
		{
			List<BuildingBase> buildingsByConfigId = Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByConfigId(EffectTargetBuildingIds[i]);
			for (int j = 0; j < buildingsByConfigId.Count; j++)
			{
				this.ChangeExp(buildingsByConfigId[j].Id, base.Id, GetConfigParam(ParameterType.ThrowDice, 0), BuildingShowType.ThrowDice);
			}
		}
	}

	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		this.ChangeGold(GetGoldMultiValue(0, ParameterType.Pass), BuildingShowType.Pass);
		trigger = true;
	}

	private void OnSellingCard(int configId, int buildingConfigId)
	{
		SinglePlayerCardConfigureItem configureItem = GetConfigureItem();
		if (configureItem != null)
		{
			int safeByIndex = configureItem.OtherParam.GetSafeByIndex(0);
			if (SellingBuildingIds.Contains(configId))
			{
				ChangeOperateBonus(OperateType.PassGold, safeByIndex);
			}
		}
	}
}
