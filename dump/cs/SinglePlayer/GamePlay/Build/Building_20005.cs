using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20005 : BuildingBase
{
	protected override void OnDice(int count)
	{
		base.OnDice(count);
		this.ChangeExp(base.Id, base.Id, GetConfigParam(ParameterType.ThrowDice, 0), BuildingShowType.ThrowDice);
	}
}
