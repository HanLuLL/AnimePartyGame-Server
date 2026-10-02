using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAssistVoteS7_ProgressBar : GProgressBar
{
	public Controller color;

	public const string URL = "ui://50xzye56puf3p";

	public static UIAssistVoteS7_ProgressBar CreateInstance()
	{
		return (UIAssistVoteS7_ProgressBar)UIPackage.CreateObject("AssistVoteS7", "AssistVoteS7_ProgressBar");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		color = GetControllerAt(0);
	}
}
