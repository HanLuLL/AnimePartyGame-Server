using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_Score : GComponent
{
	public Controller gameState;

	public Controller isMobile;

	public GTextField txt_desc;

	public GButton btn_check;

	public GTextField txt_score;

	public Transition cut_in;

	public const string URL = "ui://iy1joavto1801z";

	public static UISetting_Com_Score CreateInstance()
	{
		return (UISetting_Com_Score)UIPackage.CreateObject("Setting", "Setting_Com_Score");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		gameState = GetControllerAt(0);
		isMobile = GetControllerAt(1);
		txt_desc = (GTextField)GetChildAt(3);
		btn_check = (GButton)GetChildAt(4);
		txt_score = (GTextField)GetChildAt(5);
		cut_in = GetTransitionAt(0);
	}
}
