using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_MapInfoSelect : GButton
{
	public GGraph zhezhao;

	public const string URL = "ui://m6sn3r22px78j9i";

	public static UIButton_MapInfoSelect CreateInstance()
	{
		return (UIButton_MapInfoSelect)UIPackage.CreateObject("Common_External", "Button_MapInfoSelect");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zhezhao = (GGraph)GetChildAt(0);
	}
}
