using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRule_Com_Excel_01_Item : GComponent
{
	public GTextField txt_Name;

	public GTextField txt_Data;

	public const string URL = "ui://232dx96no8125";

	public static UIRule_Com_Excel_01_Item CreateInstance()
	{
		return (UIRule_Com_Excel_01_Item)UIPackage.CreateObject("Rule", "Rule_Com_Excel_01_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Name = (GTextField)GetChildAt(2);
		txt_Data = (GTextField)GetChildAt(3);
	}
}
