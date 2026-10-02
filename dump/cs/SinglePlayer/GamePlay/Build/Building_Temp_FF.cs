namespace SinglePlayer.GamePlay.Build;

public class Building_Temp_FF : BuildingBase
{
	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		int goldMultiValue = GetGoldMultiValue(0, ParameterType.Pass);
		this.ChangeGold(goldMultiValue, BuildingShowType.Pass);
		trigger = true;
	}

	protected override void OnDice(int count)
	{
		base.OnDice(count);
		int gold = GetGoldMultiValue(0, ParameterType.ThrowDice) * count;
		this.ChangeGold(gold, BuildingShowType.ThrowDice);
	}
}
