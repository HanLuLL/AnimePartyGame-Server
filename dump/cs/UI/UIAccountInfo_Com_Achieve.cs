using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Com_Achieve : GComponent
{
	public Controller hasAchieveData;

	public GGraph mohu;

	public GList list_Achieve;

	public GButton btn_Sure;

	public Transition Cut_in;

	public const string URL = "ui://iepldke7zhztb";

	public static UIAccountInfo_Com_Achieve CreateInstance()
	{
		return (UIAccountInfo_Com_Achieve)UIPackage.CreateObject("AccountInfo", "AccountInfo_Com_Achieve");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		hasAchieveData = GetControllerAt(0);
		mohu = (GGraph)GetChildAt(0);
		list_Achieve = (GList)GetChildAt(6);
		btn_Sure = (GButton)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
	}
}
