using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_SkinQuality : GComponent
{
	public Controller appearanceType;

	public const string URL = "ui://m6sn3r22o812c5";

	public static UICom_SkinQuality CreateInstance()
	{
		return (UICom_SkinQuality)UIPackage.CreateObject("Common_External", "Com_SkinQuality");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		appearanceType = GetControllerAt(0);
	}
}
