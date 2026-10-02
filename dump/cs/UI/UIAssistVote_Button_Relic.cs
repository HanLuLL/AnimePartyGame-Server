using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAssistVote_Button_Relic : GButton
{
	public GComponent com_Quality;

	public GLoader loader_Relic;

	public const string URL = "ui://andqlvspec7w2";

	public static UIAssistVote_Button_Relic CreateInstance()
	{
		return (UIAssistVote_Button_Relic)UIPackage.CreateObject("AssistVote", "AssistVote_Button_Relic");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Quality = (GComponent)GetChildAt(0);
		loader_Relic = (GLoader)GetChildAt(2);
	}
}
