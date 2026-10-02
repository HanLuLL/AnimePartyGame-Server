using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISignIn_Button_SignIn : GButton
{
	public Controller get;

	public GLoader loader_icon;

	public GTextField txt_number;

	public const string URL = "ui://dqu5kxbpg9kn3";

	public static UISignIn_Button_SignIn CreateInstance()
	{
		return (UISignIn_Button_SignIn)UIPackage.CreateObject("SignIn", "SignIn_Button_SignIn");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		get = GetControllerAt(1);
		loader_icon = (GLoader)GetChildAt(4);
		txt_number = (GTextField)GetChildAt(5);
	}
}
