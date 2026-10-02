using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILogin_Button_ShowNotice : GButton
{
	public GTextField txt;

	public const string URL = "ui://tgr7xz46t0k31d";

	public static UILogin_Button_ShowNotice CreateInstance()
	{
		return (UILogin_Button_ShowNotice)UIPackage.CreateObject("Login", "Login_Button_ShowNotice");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt = (GTextField)GetChildAt(1);
	}
}
