using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibray_btn_relic : GButton
{
	public GTextField Text_des;

	public const string URL = "ui://mc0y3plupj0z1h";

	public static UINewGameLibray_btn_relic CreateInstance()
	{
		return (UINewGameLibray_btn_relic)UIPackage.CreateObject("NewGameLibrary", "NewGameLibray_btn_relic");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Text_des = (GTextField)GetChildAt(2);
	}
}
