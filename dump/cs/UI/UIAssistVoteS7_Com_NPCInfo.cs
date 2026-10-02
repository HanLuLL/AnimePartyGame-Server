using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAssistVoteS7_Com_NPCInfo : GComponent
{
	public Controller type;

	public Controller status;

	public Controller diceScale;

	public GTextField txt_Title;

	public GList list_SelectLeft;

	public GTextField txt_PointTotal;

	public Transition Number;

	public Transition Choose;

	public const string URL = "ui://50xzye56puf3a";

	public static UIAssistVoteS7_Com_NPCInfo CreateInstance()
	{
		return (UIAssistVoteS7_Com_NPCInfo)UIPackage.CreateObject("AssistVoteS7", "AssistVoteS7_Com_NPCInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		status = GetControllerAt(1);
		diceScale = GetControllerAt(2);
		txt_Title = (GTextField)GetChildAt(4);
		list_SelectLeft = (GList)GetChildAt(5);
		txt_PointTotal = (GTextField)GetChildAt(6);
		Number = GetTransitionAt(0);
		Choose = GetTransitionAt(1);
	}
}
