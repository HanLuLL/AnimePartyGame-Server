using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_10003 : BuildingBase
{
	protected override void OnDice(int count)
	{
		base.OnDice(count);
		this.ChangeGold(GetGoldMultiValue(0, ParameterType.ThrowDice) * count, BuildingShowType.ThrowDice);
	}
}
