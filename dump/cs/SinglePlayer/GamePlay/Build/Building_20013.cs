namespace SinglePlayer.GamePlay.Build;

public class Building_20013 : BuildingBase
{
	protected override void OnStay(ref bool trigger)
	{
		base.OnStay(ref trigger);
		int goldMultiValue = GetGoldMultiValue(0, ParameterType.Stay);
		int configParam = GetConfigParam(ParameterType.Stay, 1);
		if (goldMultiValue > 0)
		{
			this.ChangeGold(goldMultiValue, BuildingShowType.Stay);
			ChangeOperateBonus(OperateType.StayGold, configParam);
		}
		trigger = true;
	}
}
