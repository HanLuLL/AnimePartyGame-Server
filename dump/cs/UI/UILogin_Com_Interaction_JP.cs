using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILogin_Com_Interaction_JP : GComponent
{
	public Controller version;

	public Controller dmm;

	public GGraph graph_Effect;

	public GTextField txt_Version;

	public GTextField txt_Version_01;

	public UILogin_Button_ShowNotice btn_ShowNotice;

	public UILogin_Button_CleanCache btn_CleanCache;

	public GTextInput Input;

	public GButton btn_StartGame;

	public GButton btn_SwitchTransfer;

	public GButton btn_Logout;

	public GButton btn_ApplicationQuit;

	public Transition t0;

	public const string URL = "ui://tgr7xz46somd16";

	public static UILogin_Com_Interaction_JP CreateInstance()
	{
		return (UILogin_Com_Interaction_JP)UIPackage.CreateObject("Login", "Login_Com_Interaction_JP");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		version = GetControllerAt(0);
		dmm = GetControllerAt(1);
		graph_Effect = (GGraph)GetChildAt(0);
		txt_Version = (GTextField)GetChildAt(2);
		txt_Version_01 = (GTextField)GetChildAt(3);
		btn_ShowNotice = (UILogin_Button_ShowNotice)GetChildAt(4);
		btn_CleanCache = (UILogin_Button_CleanCache)GetChildAt(5);
		Input = (GTextInput)GetChildAt(9);
		btn_StartGame = (GButton)GetChildAt(11);
		btn_SwitchTransfer = (GButton)GetChildAt(12);
		btn_Logout = (GButton)GetChildAt(13);
		btn_ApplicationQuit = (GButton)GetChildAt(14);
		t0 = GetTransitionAt(0);
	}
}
