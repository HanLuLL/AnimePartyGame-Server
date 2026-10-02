using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILogin_Button_CleanCache : GButton
{
	public GTextField txt;

	public const string URL = "ui://tgr7xz46t0k31e";

	public static UILogin_Button_CleanCache CreateInstance()
	{
		return (UILogin_Button_CleanCache)UIPackage.CreateObject("Login", "Login_Button_CleanCache");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt = (GTextField)GetChildAt(1);
	}
}
