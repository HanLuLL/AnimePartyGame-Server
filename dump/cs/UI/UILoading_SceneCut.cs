using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILoading_SceneCut : GComponent
{
	public Controller language;

	public GLoader loader_Bg;

	public Transition LoopBG;

	public Transition Loading;

	public const string URL = "ui://bzunkg3bq6a56";

	public static UILoading_SceneCut CreateInstance()
	{
		return (UILoading_SceneCut)UIPackage.CreateObject("Loading", "Loading_SceneCut");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		loader_Bg = (GLoader)GetChildAt(1);
		LoopBG = GetTransitionAt(0);
		Loading = GetTransitionAt(1);
	}
}
