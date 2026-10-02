using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_ClueItem : GComponent
{
	public Controller completed;

	public Controller showLine;

	public GTextField txt_Title;

	public GTextField txt_Progress;

	public Transition Cut_in;

	public const string URL = "ui://fxejlqlfmgj008";

	public static UIBattleInfo_Com_ClueItem CreateInstance()
	{
		return (UIBattleInfo_Com_ClueItem)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_ClueItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		completed = GetControllerAt(0);
		showLine = GetControllerAt(1);
		txt_Title = (GTextField)GetChildAt(4);
		txt_Progress = (GTextField)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
