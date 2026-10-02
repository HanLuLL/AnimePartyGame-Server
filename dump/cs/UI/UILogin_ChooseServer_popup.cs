using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILogin_ChooseServer_popup : GComponent
{
	public GList list;

	public const string URL = "ui://tgr7xz46idvq7";

	public static UILogin_ChooseServer_popup CreateInstance()
	{
		return (UILogin_ChooseServer_popup)UIPackage.CreateObject("Login", "Login_ChooseServer_popup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list = (GList)GetChildAt(1);
	}
}
