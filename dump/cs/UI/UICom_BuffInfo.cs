using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_BuffInfo : GComponent
{
	public GGraph di;

	public GList list_Buff;

	public const string URL = "ui://xuaw6o8jbczmq3s";

	public static UICom_BuffInfo CreateInstance()
	{
		return (UICom_BuffInfo)UIPackage.CreateObject("Common", "Com_BuffInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di = (GGraph)GetChildAt(0);
		list_Buff = (GList)GetChildAt(1);
	}
}
