using System.Collections.Generic;
using System.Linq;
using SinglePlayer.GamePlay.Build;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Relic;

[Preserve]
public class RelicInfo_60016 : RelicBuff
{
	public class RelicBuffModule_60016 : BaseRelicBuffModule
	{
		public override bool TryAddChargeOnThrowDice(RelicInfo info, List<int> dicePoints)
		{
			int targetPoint = info.GetRelicParam(0);
			int num = dicePoints.Count((int p) => p == targetPoint);
			info.BuffData.ChargeCount += num;
			return num > 0;
		}

		public override void Apply(RelicInfo info)
		{
			int relicParam = info.GetRelicParam(1);
			if (info.BuffData.ChargeCount < relicParam)
			{
				return;
			}
			info.BuffData.ChargeCount -= relicParam;
			int[] configIds = new int[2]
			{
				info.GetRelicParam(2),
				info.GetRelicParam(3)
			};
			foreach (BuildingBase item in Game.GetSystem<BoardManager>().buildingManager.GetBuildingsByConfigId(configIds))
			{
				item.AddForceExecuteStayQueue();
			}
		}
	}

	public override void Initialize(int relicId)
	{
		if (StaticConfigure.SinglePlayer.RelicDict.TryGetValue(relicId, out Config))
		{
			Id = relicId;
			OnThrowDice = new RelicBuffModule_60016();
		}
	}
}
