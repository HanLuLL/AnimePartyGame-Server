using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Com_Rank : GComponent
{
	public Controller rand;

	public GList list_Level;

	public GTextField txt_Gold;

	public GImage image_First;

	public GImage image_Second;

	public GImage image_Third;

	public GImage image_Forth;

	public const string URL = "ui://iepldke7zhztj";

	public static UIAccountInfo_Com_Rank CreateInstance()
	{
		return (UIAccountInfo_Com_Rank)UIPackage.CreateObject("AccountInfo", "AccountInfo_Com_Rank");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		rand = GetControllerAt(0);
		list_Level = (GList)GetChildAt(0);
		txt_Gold = (GTextField)GetChildAt(2);
		image_First = (GImage)GetChildAt(3);
		image_Second = (GImage)GetChildAt(4);
		image_Third = (GImage)GetChildAt(5);
		image_Forth = (GImage)GetChildAt(6);
	}
}
