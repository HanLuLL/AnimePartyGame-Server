using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_ItemType : GComponent
{
	public Controller itemType;

	public const string URL = "ui://m6sn3r22hcgi26";

	public static UICom_ItemType CreateInstance()
	{
		return (UICom_ItemType)UIPackage.CreateObject("Common_External", "Com_ItemType");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		itemType = GetControllerAt(0);
	}
}
