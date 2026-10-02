using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_ComboBox : GComboBox
{
	public Controller SetArrow;

	public const string URL = "ui://iy1joavto1n8f";

	protected override void OnUpdate()
	{
		base.OnUpdate();
		SetArrow.selectedIndex = ((dropdown.parent == null) ? 1 : 0);
	}

	public static UISetting_Com_ComboBox CreateInstance()
	{
		return (UISetting_Com_ComboBox)UIPackage.CreateObject("Setting", "Setting_Com_ComboBox");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetArrow = GetControllerAt(1);
	}
}
