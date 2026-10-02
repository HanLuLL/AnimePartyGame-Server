using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILogin_Button_AgeTip : GButton
{
	public Controller isCn;

	public const string URL = "ui://tgr7xz46dyuj1k";

	public static UILogin_Button_AgeTip CreateInstance()
	{
		return (UILogin_Button_AgeTip)UIPackage.CreateObject("Login", "Login_Button_AgeTip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isCn = GetControllerAt(0);
	}
}
