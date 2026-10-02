using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_10002 : BuildingBase
{
	protected override void OnDice(int count)
	{
		base.OnDice(count);
		this.ChangeGold(GetGoldMultiValue(0, ParameterType.ThrowDice) * count, BuildingShowType.ThrowDice);
		this.ChangeExp(base.Id, base.Id, GetConfigParam(ParameterType.ThrowDice, 1) * count, BuildingShowType.ThrowDice);
	}
}
