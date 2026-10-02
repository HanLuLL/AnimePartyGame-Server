using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Item_Buttom : GComponent
{
	public UISetting_Item_Bg bg;

	public UISetting_Item_Title lable_title;

	public Transition cut_in;

	public const string URL = "ui://iy1joavto1n88";

	public static UISetting_Item_Buttom CreateInstance()
	{
		return (UISetting_Item_Buttom)UIPackage.CreateObject("Setting", "Setting_Item_Buttom");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bg = (UISetting_Item_Bg)GetChildAt(0);
		lable_title = (UISetting_Item_Title)GetChildAt(2);
		cut_in = GetTransitionAt(0);
	}
}
