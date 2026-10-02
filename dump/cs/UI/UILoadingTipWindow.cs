using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILoadingTipWindow : GComponent
{
	public Controller type;

	public GMovieClip amovie_1;

	public GMovieClip amovie_2;

	public GTextField txt_MessageTips;

	public GGroup com_group;

	public Transition cutIn;

	public const string URL = "ui://f0q1w9xpgqyw2";

	public static UILoadingTipWindow CreateInstance()
	{
		BindAll();
		return (UILoadingTipWindow)UIPackage.CreateObject("LoadingTip", "LoadingTipWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://f0q1w9xpgqyw2", typeof(UILoadingTipWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		amovie_1 = (GMovieClip)GetChildAt(1);
		amovie_2 = (GMovieClip)GetChildAt(2);
		txt_MessageTips = (GTextField)GetChildAt(3);
		com_group = (GGroup)GetChildAt(4);
		cutIn = GetTransitionAt(0);
	}
}
