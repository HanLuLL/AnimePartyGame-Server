using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Label : GComponent
{
	public Controller labelController;

	public const string URL = "ui://m6sn3r22ot0w7";

	public static UICom_Label CreateInstance()
	{
		return (UICom_Label)UIPackage.CreateObject("Common_External", "Com_Label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		labelController = GetControllerAt(0);
	}
}
