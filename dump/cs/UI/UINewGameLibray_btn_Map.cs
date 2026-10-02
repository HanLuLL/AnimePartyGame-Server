using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibray_btn_Map : GButton
{
	public GTextField Text_des;

	public const string URL = "ui://mc0y3plupj0z1l";

	public static UINewGameLibray_btn_Map CreateInstance()
	{
		return (UINewGameLibray_btn_Map)UIPackage.CreateObject("NewGameLibrary", "NewGameLibray_btn_Map");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Text_des = (GTextField)GetChildAt(2);
	}
}
