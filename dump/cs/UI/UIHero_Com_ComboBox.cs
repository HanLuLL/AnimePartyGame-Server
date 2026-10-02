using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_ComboBox : GComboBox
{
	public Controller SetArrow;

	public const string URL = "ui://7qkd4lqxjhclq4i";

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (dropdown != null && dropdown.parent != null && dropdown is UIHero_Com_ComboBox_popup uIHero_Com_ComboBox_popup && uIHero_Com_ComboBox_popup.list.selectedIndex != base.selectedIndex)
		{
			uIHero_Com_ComboBox_popup.list.selectedIndex = base.selectedIndex;
		}
	}

	public static UIHero_Com_ComboBox CreateInstance()
	{
		return (UIHero_Com_ComboBox)UIPackage.CreateObject("Hero", "Hero_Com_ComboBox");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetArrow = GetControllerAt(1);
	}
}
