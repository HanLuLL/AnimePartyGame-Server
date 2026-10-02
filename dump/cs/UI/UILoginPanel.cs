using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILoginPanel : GComponent
{
	public Controller logoVersion;

	public Controller angelMode;

	public Controller country;

	public GLoader loader_bg;

	public GGraph graph_DynamicBg;

	public GMovieClip aMoive_Loading;

	public Transition t0;

	public const string URL = "ui://tgr7xz46gd4dv";

	public static UILoginPanel CreateInstance()
	{
		BindAll();
		return (UILoginPanel)UIPackage.CreateObject("Login", "LoginPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://tgr7xz465g1r0", typeof(UILogin_Com_Interaction));
		UIObjectFactory.SetPackageItemExtension("ui://tgr7xz46dyuj1k", typeof(UILogin_Button_AgeTip));
		UIObjectFactory.SetPackageItemExtension("ui://tgr7xz46gd4dv", typeof(UILoginPanel));
		UIObjectFactory.SetPackageItemExtension("ui://tgr7xz46idvq7", typeof(UILogin_ChooseServer_popup));
		UIObjectFactory.SetPackageItemExtension("ui://tgr7xz46somd16", typeof(UILogin_Com_Interaction_JP));
		UIObjectFactory.SetPackageItemExtension("ui://tgr7xz46t0k31d", typeof(UILogin_Button_ShowNotice));
		UIObjectFactory.SetPackageItemExtension("ui://tgr7xz46t0k31e", typeof(UILogin_Button_CleanCache));
		UIObjectFactory.SetPackageItemExtension("ui://tgr7xz46uspw1j", typeof(UILogin_Com_Interaction_CN));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		logoVersion = GetControllerAt(0);
		angelMode = GetControllerAt(1);
		country = GetControllerAt(2);
		loader_bg = (GLoader)GetChildAt(0);
		graph_DynamicBg = (GGraph)GetChildAt(1);
		aMoive_Loading = (GMovieClip)GetChildAt(5);
		t0 = GetTransitionAt(0);
	}
}
