using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Character;

namespace SinglePlayer.GamePlay;

public class AttributeChangeInfo
{
	public (AttributeChangeSource type, int id) Source;

	public (AttributeChangeTarget type, int id) Target;

	public int ConfigId;

	public int ChangeStar;

	public int ChangeHP;

	public int ChangeGold;

	public int Exp;

	public BuildingShowType BuildingShowType { get; set; }

	public void TryChangeCharacterProperty()
	{
		HeroProperty heroProperty = Game.GetModel<GameData>().heroProperty;
		if (ChangeHP != 0)
		{
			heroProperty.ChangeHP(ChangeHP);
		}
		if (ChangeGold != 0)
		{
			heroProperty.ChangeGold(ChangeGold);
			heroProperty.AddSourceGold(Source.type, ConfigId, ChangeGold);
		}
	}
}
