using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibray_btn_Event : GButton
{
	public GTextField Text_des;

	public const string URL = "ui://mc0y3plupj0z1k";

	public static UINewGameLibray_btn_Event CreateInstance()
	{
		return (UINewGameLibray_btn_Event)UIPackage.CreateObject("NewGameLibrary", "NewGameLibray_btn_Event");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Text_des = (GTextField)GetChildAt(2);
	}
}
