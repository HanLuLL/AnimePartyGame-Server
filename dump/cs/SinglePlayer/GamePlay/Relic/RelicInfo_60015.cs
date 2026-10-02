using SinglePlayer.GamePlay.Build;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Relic;

[Preserve]
public class RelicInfo_60015 : RelicBuff
{
	public class RelicBuffModule_60015 : BaseRelicBuffModule
	{
		public override bool TryAddChargeOnBuildTriggerEffect(RelicInfo info, int configId, ParameterType effectType)
		{
			int relicParam = info.GetRelicParam(2);
			int num;
			if (configId == relicParam)
			{
				num = ((effectType == ParameterType.Stay) ? 1 : 0);
				if (num != 0)
				{
					info.BuffData.ChargeCount++;
				}
			}
			else
			{
				num = 0;
			}
			return (byte)num != 0;
		}

		public override void Apply(RelicInfo info)
		{
			int relicParam = info.GetRelicParam(0);
			if (info.BuffData.ChargeCount >= relicParam)
			{
				info.BuffData.ChargeCount -= relicParam;
				int relicParam2 = info.GetRelicParam(1);
				info.ChangeGold(relicParam2);
			}
		}
	}

	public override void Initialize(int relicId)
	{
		if (StaticConfigure.SinglePlayer.RelicDict.TryGetValue(relicId, out Config))
		{
			Id = relicId;
			OnBuildingTriggerStayEffect = new RelicBuffModule_60015();
		}
	}
}
