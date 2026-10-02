using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Loader_Hero : GComponent
{
	public GLoader loader_Character;

	public const string URL = "ui://iepldke7hni07";

	public static UIAccountInfo_Loader_Hero CreateInstance()
	{
		return (UIAccountInfo_Loader_Hero)UIPackage.CreateObject("AccountInfo", "AccountInfo_Loader_Hero");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Character = (GLoader)GetChildAt(0);
	}
}
