using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAssistVoteS7_Com_RelicInfo : GComponent
{
	public Controller type;

	public GTextField txt_Target;

	public const string URL = "ui://50xzye56puf37";

	public static UIAssistVoteS7_Com_RelicInfo CreateInstance()
	{
		return (UIAssistVoteS7_Com_RelicInfo)UIPackage.CreateObject("AssistVoteS7", "AssistVoteS7_Com_RelicInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		txt_Target = (GTextField)GetChildAt(0);
	}
}
