using FairyGUI;
using FairyGUI.Utils;
using SinglePlayer.GamePlay;

namespace UI;

public class UISinglePlayer_Com_AttrTip : GComponent
{
	private enum FontType
	{
		None,
		Add,
		Sub
	}

	public Controller fontType;

	public Controller iconType;

	public GTextField txt_OriginValue;

	public GTextField txt_CurValue;

	public GTextField txt_AttrValue;

	public Transition showAttr;

	public const string URL = "ui://mi9vm3w0as5tq46";

	public bool InitBuildingAttrChange(AttributeChangeInfo message)
	{
		if (message.ChangeHP > 0)
		{
			fontType.selectedIndex = ((message.ChangeHP > 0) ? 1 : 2);
			iconType.selectedIndex = 5;
			txt_AttrValue.text = message.ChangeHP.ToString("+0;-0;0");
			return true;
		}
		if (message.ChangeGold != 0)
		{
			fontType.selectedIndex = ((message.ChangeGold > 0) ? 1 : 2);
			iconType.selectedIndex = 4;
			txt_AttrValue.text = message.ChangeGold.ToString("+0;-0;0");
			return true;
		}
		return false;
	}

	public void InitCharacterAttrChange(PropertyType type, int value)
	{
		fontType.selectedIndex = ((value > 0) ? 1 : 2);
		iconType.selectedIndex = (int)type;
		txt_AttrValue.text = value.ToString("+0;-0;0");
	}

	public static UISinglePlayer_Com_AttrTip CreateInstance()
	{
		return (UISinglePlayer_Com_AttrTip)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_AttrTip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		fontType = GetControllerAt(0);
		iconType = GetControllerAt(1);
		txt_OriginValue = (GTextField)GetChildAt(0);
		txt_CurValue = (GTextField)GetChildAt(1);
		txt_AttrValue = (GTextField)GetChildAt(6);
		showAttr = GetTransitionAt(0);
	}
}
