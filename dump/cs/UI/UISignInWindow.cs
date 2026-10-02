using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISignInWindow : GComponent
{
	public GGraph mohu;

	public GLoader loader_bg;

	public GButton btn_back;

	public GTextField txt_headline;

	public GTextField txt_desc;

	public GTextField txt_duration;

	public UISignIn_Button_SignIn btn_day1;

	public UISignIn_Button_SignIn btn_day2;

	public UISignIn_Button_SignIn btn_day3;

	public UISignIn_Button_SignIn btn_day4;

	public UISignIn_Button_SignIn btn_day5;

	public UISignIn_Button_SignIn btn_day6;

	public UISignIn_Button_SignIn btn_day7;

	public GProgressBar progress_reward;

	public Transition Cut_in;

	public const string URL = "ui://dqu5kxbpg9kn0";

	public static UISignInWindow CreateInstance()
	{
		BindAll();
		return (UISignInWindow)UIPackage.CreateObject("SignIn", "SignInWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://dqu5kxbpg9kn0", typeof(UISignInWindow));
		UIObjectFactory.SetPackageItemExtension("ui://dqu5kxbpg9kn3", typeof(UISignIn_Button_SignIn));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		loader_bg = (GLoader)GetChildAt(1);
		btn_back = (GButton)GetChildAt(2);
		txt_headline = (GTextField)GetChildAt(3);
		txt_desc = (GTextField)GetChildAt(4);
		txt_duration = (GTextField)GetChildAt(5);
		btn_day1 = (UISignIn_Button_SignIn)GetChildAt(7);
		btn_day2 = (UISignIn_Button_SignIn)GetChildAt(8);
		btn_day3 = (UISignIn_Button_SignIn)GetChildAt(9);
		btn_day4 = (UISignIn_Button_SignIn)GetChildAt(10);
		btn_day5 = (UISignIn_Button_SignIn)GetChildAt(11);
		btn_day6 = (UISignIn_Button_SignIn)GetChildAt(12);
		btn_day7 = (UISignIn_Button_SignIn)GetChildAt(13);
		progress_reward = (GProgressBar)GetChildAt(15);
		Cut_in = GetTransitionAt(0);
	}
}
