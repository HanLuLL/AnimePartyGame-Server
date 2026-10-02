using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_10001 : BuildingBase
{
	protected override void OnDice(int count)
	{
		base.OnDice(count);
		this.ChangeGold(GetGoldMultiValue(0, ParameterType.ThrowDice), BuildingShowType.ThrowDice);
	}
}
