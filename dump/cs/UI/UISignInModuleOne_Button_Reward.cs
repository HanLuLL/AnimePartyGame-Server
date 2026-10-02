using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISignInModuleOne_Button_Reward : GButton
{
	public Controller get;

	public GTextField text_no;

	public GTextField text_yes;

	public GLoader icon1;

	public GTextField txt_number;

	public const string URL = "ui://3bkg2bqqpjcy3";

	public static UISignInModuleOne_Button_Reward CreateInstance()
	{
		return (UISignInModuleOne_Button_Reward)UIPackage.CreateObject("SignInModuleOne", "SignInModuleOne_Button_Reward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		get = GetControllerAt(1);
		text_no = (GTextField)GetChildAt(3);
		text_yes = (GTextField)GetChildAt(4);
		icon1 = (GLoader)GetChildAt(5);
		txt_number = (GTextField)GetChildAt(6);
	}
}
