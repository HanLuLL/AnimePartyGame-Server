using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAssistVote_Com_NPCInfo : GComponent
{
	public Controller type;

	public Controller status;

	public Controller diceScale;

	public GTextField txt_Title;

	public GList list_SelectLeft;

	public GTextField txt_PointTotal;

	public Transition Number;

	public Transition Choose;

	public const string URL = "ui://andqlvspec7wy";

	public static UIAssistVote_Com_NPCInfo CreateInstance()
	{
		return (UIAssistVote_Com_NPCInfo)UIPackage.CreateObject("AssistVote", "AssistVote_Com_NPCInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		status = GetControllerAt(1);
		diceScale = GetControllerAt(2);
		txt_Title = (GTextField)GetChildAt(3);
		list_SelectLeft = (GList)GetChildAt(4);
		txt_PointTotal = (GTextField)GetChildAt(5);
		Number = GetTransitionAt(0);
		Choose = GetTransitionAt(1);
	}
}
