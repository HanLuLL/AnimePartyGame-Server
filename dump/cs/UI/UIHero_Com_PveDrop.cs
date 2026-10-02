using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_PveDrop : GComponent
{
	public Controller showDelete;

	public Controller showWay;

	public GButton btn_Item;

	public GTextField txt_Count;

	public GButton btn_Delete;

	public GGraph btn_GoWay;

	public const string URL = "ui://7qkd4lqxot0wq2d";

	public static UIHero_Com_PveDrop CreateInstance()
	{
		return (UIHero_Com_PveDrop)UIPackage.CreateObject("Hero", "Hero_Com_PveDrop");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showDelete = GetControllerAt(0);
		showWay = GetControllerAt(1);
		btn_Item = (GButton)GetChildAt(0);
		txt_Count = (GTextField)GetChildAt(1);
		btn_Delete = (GButton)GetChildAt(2);
		btn_GoWay = (GGraph)GetChildAt(6);
	}
}
