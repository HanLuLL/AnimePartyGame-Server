namespace SinglePlayer.GamePlay.Build;

public class Building_20014 : BuildingBase
{
	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		trigger = true;
		int goldMultiValue = GetGoldMultiValue(0, ParameterType.Pass);
		this.ChangeGold(goldMultiValue, BuildingShowType.Pass);
	}

	protected override void OnDice(int count)
	{
		base.OnDice(count);
		int gold = GetGoldMultiValue(0, ParameterType.ThrowDice) * count;
		this.ChangeGold(gold, BuildingShowType.ThrowDice);
	}

	protected override void OnStay(ref bool trigger)
	{
		base.OnStay(ref trigger);
		trigger = true;
		int goldMultiValue = GetGoldMultiValue(0, ParameterType.Stay);
		this.ChangeGold(goldMultiValue, BuildingShowType.Stay);
	}
}
