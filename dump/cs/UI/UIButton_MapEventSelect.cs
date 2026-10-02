using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_MapEventSelect : GButton
{
	public GGraph zhezhao;

	public const string URL = "ui://m6sn3r229536q3c";

	public static UIButton_MapEventSelect CreateInstance()
	{
		return (UIButton_MapEventSelect)UIPackage.CreateObject("Common_External", "Button_MapEventSelect");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zhezhao = (GGraph)GetChildAt(0);
	}
}
