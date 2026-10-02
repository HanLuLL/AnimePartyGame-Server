namespace Core.Unit;

public class CharacterShowComponent_117 : CharacterShowComponent
{
	public override float GetMoveSpeedAdditionRate()
	{
		BuffContainer buffContainer = Owner?.player.buffContainer;
		if (buffContainer == null)
		{
			return 1f;
		}
		if (!buffContainer.Contain(1170102))
		{
			return 1f;
		}
		return 1.5f;
	}
}
