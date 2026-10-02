namespace SinglePlayer.GamePlay.Character;

public class Monster_5003 : Monster
{
	public Monster_5003(MonsterProperty property)
		: base(property)
	{
	}

	protected override void OnThrowDice()
	{
		base.OnThrowDice();
		Game.GetSystem<BoardManager>().characterManager.ChangeCharacterAttribute(new AttributeChangeInfo
		{
			Source = (type: AttributeChangeSource.DiceCalculate, id: 2),
			ChangeGold = GetTriggerParam(0)
		});
	}
}
