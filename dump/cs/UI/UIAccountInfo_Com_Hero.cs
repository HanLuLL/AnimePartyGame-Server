using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Com_Hero : GComponent
{
	public GGraph mohu;

	public GList list_Hero;

	public GButton btn_Sure;

	public Transition Cut_in;

	public const string URL = "ui://iepldke7hni02";

	public static UIAccountInfo_Com_Hero CreateInstance()
	{
		return (UIAccountInfo_Com_Hero)UIPackage.CreateObject("AccountInfo", "AccountInfo_Com_Hero");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		list_Hero = (GList)GetChildAt(2);
		btn_Sure = (GButton)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
