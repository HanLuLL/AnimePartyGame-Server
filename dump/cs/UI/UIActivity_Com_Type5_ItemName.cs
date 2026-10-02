using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_ItemName : GComponent
{
	public GTextField txt_Title;

	public const string URL = "ui://vckl96ksg402em";

	public static UIActivity_Com_Type5_ItemName CreateInstance()
	{
		return (UIActivity_Com_Type5_ItemName)UIPackage.CreateObject("Activity", "Activity_Com_Type5_ItemName");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(0);
	}
}
