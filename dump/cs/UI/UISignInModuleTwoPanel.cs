using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISignInModuleTwoPanel : GComponent
{
	public Controller language;

	public GLoader loader_BG;

	public UISignInModuleTwo_Button_Reward day1;

	public UISignInModuleTwo_Button_Reward day2;

	public UISignInModuleTwo_Button_Reward day3;

	public UISignInModuleTwo_Button_Reward day4;

	public UISignInModuleTwo_Button_Reward day5;

	public UISignInModuleTwo_Button_Reward day6;

	public UISignInModuleTwo_Button_Reward day7;

	public GGroup ActivitySevenDaySignIn_Day;

	public GTextField txt_Duration;

	public UISignInModuleTwo_Progress Progress_SignIn;

	public Transition Cut_In;

	public const string URL = "ui://ik9iuwgyb1ak3d";

	public static UISignInModuleTwoPanel CreateInstance()
	{
		BindAll();
		return (UISignInModuleTwoPanel)UIPackage.CreateObject("SignInModuleTwo", "SignInModuleTwoPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://ik9iuwgyb1ak1q", typeof(UISignInModuleTwo_Progress));
		UIObjectFactory.SetPackageItemExtension("ui://ik9iuwgyb1ak1y", typeof(UISignInModuleTwo_Button_Reward));
		UIObjectFactory.SetPackageItemExtension("ui://ik9iuwgyb1ak3d", typeof(UISignInModuleTwoPanel));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		day1 = (UISignInModuleTwo_Button_Reward)GetChildAt(3);
		day2 = (UISignInModuleTwo_Button_Reward)GetChildAt(4);
		day3 = (UISignInModuleTwo_Button_Reward)GetChildAt(5);
		day4 = (UISignInModuleTwo_Button_Reward)GetChildAt(6);
		day5 = (UISignInModuleTwo_Button_Reward)GetChildAt(7);
		day6 = (UISignInModuleTwo_Button_Reward)GetChildAt(8);
		day7 = (UISignInModuleTwo_Button_Reward)GetChildAt(9);
		ActivitySevenDaySignIn_Day = (GGroup)GetChildAt(10);
		txt_Duration = (GTextField)GetChildAt(12);
		Progress_SignIn = (UISignInModuleTwo_Progress)GetChildAt(21);
		Cut_In = GetTransitionAt(0);
	}
}
