using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILoadingWindow : GComponent
{
	public Controller type;

	public UILoading_MagicSchool com_MagicSchool;

	public UILoading_SceneCut com_SceneCut;

	public const string URL = "ui://bzunkg3bq6a5a";

	public static UILoadingWindow CreateInstance()
	{
		BindAll();
		return (UILoadingWindow)UIPackage.CreateObject("Loading", "LoadingWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://bzunkg3bfow93", typeof(UILoading_MagicSchool));
		UIObjectFactory.SetPackageItemExtension("ui://bzunkg3bq6a56", typeof(UILoading_SceneCut));
		UIObjectFactory.SetPackageItemExtension("ui://bzunkg3bq6a5a", typeof(UILoadingWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		com_MagicSchool = (UILoading_MagicSchool)GetChildAt(0);
		com_SceneCut = (UILoading_SceneCut)GetChildAt(1);
	}
}
