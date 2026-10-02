using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_MapTag : GComponent
{
	public Controller tag;

	public GLoader loader_icon;

	public const string URL = "ui://m6sn3r22l500qq49";

	public static UICom_MapTag CreateInstance()
	{
		return (UICom_MapTag)UIPackage.CreateObject("Common_External", "Com_MapTag");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tag = GetControllerAt(0);
		loader_icon = (GLoader)GetChildAt(0);
	}
}
