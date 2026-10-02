using UI;

namespace Core.Unit;

public class CharacterWindow
{
	public UICom_AttrInfo Com_AttrInfo;

	public UICom_PlayerAttrInfo Com_PlayerAttrInfo;

	private Character Owner;

	public CharacterWindow(Character character)
	{
		Owner = character;
		UpdateAttrInfo();
		UpdateMonsterAttrInfo();
	}

	public void UpdateMonsterAttrInfo()
	{
		if (Owner.player.characterType == CharacterType.Monster)
		{
			Com_AttrInfo = UICom_AttrInfo.CreateInstance();
			Com_AttrInfo.ShowAttrInfo(Owner.player);
		}
	}

	public void UpdateAttrInfo()
	{
		Com_PlayerAttrInfo = UICom_PlayerAttrInfo.CreateInstance();
		Com_PlayerAttrInfo.ShowAttrInfo(Owner.player);
		Owner.showComponent?.RefreshAttrInfo();
	}

	public void Dispose()
	{
	}
}
