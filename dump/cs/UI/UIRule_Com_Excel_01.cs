using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRule_Com_Excel_01 : GComponent
{
	public GTextField txt_Second;

	public GTextField txt_First;

	public GList list_Content;

	public const string URL = "ui://232dx96no8124";

	public static UIRule_Com_Excel_01 CreateInstance()
	{
		return (UIRule_Com_Excel_01)UIPackage.CreateObject("Rule", "Rule_Com_Excel_01");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Second = (GTextField)GetChildAt(1);
		txt_First = (GTextField)GetChildAt(3);
		list_Content = (GList)GetChildAt(4);
	}
}
