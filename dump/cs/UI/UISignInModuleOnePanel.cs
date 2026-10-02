using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISignInModuleOnePanel : GComponent
{
	public Controller language;

	public GLoader loader_BG;

	public GLoader loader_Character_Color;

	public GLoader loader_Character;

	public UISignInModuleOne_Button_Reward day1;

	public UISignInModuleOne_Button_Reward day2;

	public UISignInModuleOne_Button_Reward day3;

	public UISignInModuleOne_Button_Reward day4;

	public UISignInModuleOne_Button_Reward day5;

	public UISignInModuleOne_Button_Reward day6;

	public UISignInModuleOne_Button_Reward day7;

	public GGroup ActivitySevenDaySignIn_Day;

	public GTextField txt_Explain;

	public GTextField txt_Duration;

	public UISignInModuleOne_Progress Progress_SignIn;

	public Transition Cut_In;

	public const string URL = "ui://3bkg2bqqkei21c";

	public static UISignInModuleOnePanel CreateInstance()
	{
		BindAll();
		return (UISignInModuleOnePanel)UIPackage.CreateObject("SignInModuleOne", "SignInModuleOnePanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://3bkg2bqqkei21b", typeof(UISignInModuleOne_Progress));
		UIObjectFactory.SetPackageItemExtension("ui://3bkg2bqqkei21c", typeof(UISignInModuleOnePanel));
		UIObjectFactory.SetPackageItemExtension("ui://3bkg2bqqpjcy3", typeof(UISignInModuleOne_Button_Reward));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		loader_Character_Color = (GLoader)GetChildAt(1);
		loader_Character = (GLoader)GetChildAt(2);
		day1 = (UISignInModuleOne_Button_Reward)GetChildAt(4);
		day2 = (UISignInModuleOne_Button_Reward)GetChildAt(5);
		day3 = (UISignInModuleOne_Button_Reward)GetChildAt(6);
		day4 = (UISignInModuleOne_Button_Reward)GetChildAt(7);
		day5 = (UISignInModuleOne_Button_Reward)GetChildAt(8);
		day6 = (UISignInModuleOne_Button_Reward)GetChildAt(9);
		day7 = (UISignInModuleOne_Button_Reward)GetChildAt(10);
		ActivitySevenDaySignIn_Day = (GGroup)GetChildAt(11);
		txt_Explain = (GTextField)GetChildAt(15);
		txt_Duration = (GTextField)GetChildAt(16);
		Progress_SignIn = (UISignInModuleOne_Progress)GetChildAt(18);
		Cut_In = GetTransitionAt(0);
	}
}
