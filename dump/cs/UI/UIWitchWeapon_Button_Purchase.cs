using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIWitchWeapon_Button_Purchase : GButton
{
	public GTextField txt_Countdown;

	public GTextField txt_Topic;

	public GRichTextField txt_Price;

	public const string URL = "ui://hn2q98k6kqgj1a";

	public static UIWitchWeapon_Button_Purchase CreateInstance()
	{
		return (UIWitchWeapon_Button_Purchase)UIPackage.CreateObject("WitchWeapon", "WitchWeapon_Button_Purchase");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Countdown = (GTextField)GetChildAt(2);
		txt_Topic = (GTextField)GetChildAt(3);
		txt_Price = (GRichTextField)GetChildAt(4);
	}
}
