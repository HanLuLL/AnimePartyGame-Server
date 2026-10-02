using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAssistVote_Com_RelicInfo : GComponent
{
	public Controller type;

	public GTextField txt_Target;

	public UIAssistVote_Button_Relic btn_Relic;

	public GTextField txt_RelicName;

	public GTextField txt_RelicDesc;

	public const string URL = "ui://andqlvspec7wz";

	public static UIAssistVote_Com_RelicInfo CreateInstance()
	{
		return (UIAssistVote_Com_RelicInfo)UIPackage.CreateObject("AssistVote", "AssistVote_Com_RelicInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		txt_Target = (GTextField)GetChildAt(0);
		btn_Relic = (UIAssistVote_Button_Relic)GetChildAt(2);
		txt_RelicName = (GTextField)GetChildAt(3);
		txt_RelicDesc = (GTextField)GetChildAt(4);
	}
}
