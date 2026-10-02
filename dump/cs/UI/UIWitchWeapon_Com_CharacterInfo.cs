using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIWitchWeapon_Com_CharacterInfo : GLabel
{
	public GTextField txt_Name;

	public GButton btn_Preview;

	public GGraph loader_Animation;

	public const string URL = "ui://hn2q98k6kqgjm";

	public static UIWitchWeapon_Com_CharacterInfo CreateInstance()
	{
		return (UIWitchWeapon_Com_CharacterInfo)UIPackage.CreateObject("WitchWeapon", "WitchWeapon_Com_CharacterInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Name = (GTextField)GetChildAt(1);
		btn_Preview = (GButton)GetChildAt(2);
		loader_Animation = (GGraph)GetChildAt(3);
	}
}
