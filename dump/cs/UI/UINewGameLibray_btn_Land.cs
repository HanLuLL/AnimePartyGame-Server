using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibray_btn_Land : GButton
{
	public GTextField Text_des;

	public const string URL = "ui://mc0y3plupj0z1j";

	public static UINewGameLibray_btn_Land CreateInstance()
	{
		return (UINewGameLibray_btn_Land)UIPackage.CreateObject("NewGameLibrary", "NewGameLibray_btn_Land");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Text_des = (GTextField)GetChildAt(2);
	}
}
