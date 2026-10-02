namespace SinglePlayer.GamePlay.Build;

public class Building_20015 : BuildingBase
{
	protected override void OnStay(ref bool trigger)
	{
		base.OnStay(ref trigger);
		int configParam = GetConfigParam(ParameterType.Stay, 1);
		int goldMultiValue = GetGoldMultiValue(0, ParameterType.Stay);
		this.ChangeGold(goldMultiValue, BuildingShowType.Stay);
		Game.GetModel<GameData>().heroProperty.SetForceFirstDicePoint(configParam);
		trigger = true;
	}

	protected override void OnEnter(ref bool trigger)
	{
		base.OnEnter(ref trigger);
		Game.GetSystem<BoardManager>().characterManager.Hero.ForceStopOnDoMove();
		trigger = true;
	}
}
