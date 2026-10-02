using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Slider_Progress : GProgressBar
{
	public GRichTextField txt_MissionProgress;

	public const string URL = "ui://mi9vm3w0tvj1q7e";

	public static UISinglePlayer_Slider_Progress CreateInstance()
	{
		return (UISinglePlayer_Slider_Progress)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Slider_Progress");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_MissionProgress = (GRichTextField)GetChildAt(3);
	}
}
