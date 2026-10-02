using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibray_btn_code : GButton
{
	public Controller redPoint;

	public GTextField Text_des;

	public const string URL = "ui://mc0y3plupj0z1i";

	public static UINewGameLibray_btn_code CreateInstance()
	{
		return (UINewGameLibray_btn_code)UIPackage.CreateObject("NewGameLibrary", "NewGameLibray_btn_code");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		Text_des = (GTextField)GetChildAt(2);
	}
}
