using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIWitchWeapon_Button_GoStore : GButton
{
	public GTextField txt_RemainingTime;

	public Transition Cut_in;

	public const string URL = "ui://hn2q98k6kqgjj";

	public static UIWitchWeapon_Button_GoStore CreateInstance()
	{
		return (UIWitchWeapon_Button_GoStore)UIPackage.CreateObject("WitchWeapon", "WitchWeapon_Button_GoStore");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_RemainingTime = (GTextField)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
