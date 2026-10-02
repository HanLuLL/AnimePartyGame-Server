using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRule_Com_Content : GComponent
{
	public GTextField txt_Content;

	public const string URL = "ui://232dx96nn9cw3";

	public static UIRule_Com_Content CreateInstance()
	{
		return (UIRule_Com_Content)UIPackage.CreateObject("Rule", "Rule_Com_Content");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Content = (GTextField)GetChildAt(0);
	}
}
