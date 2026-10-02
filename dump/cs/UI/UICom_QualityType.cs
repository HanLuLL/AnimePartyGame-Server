using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_QualityType : GComponent
{
	public Controller qualityType;

	public const string URL = "ui://m6sn3r22gd4db8";

	public static UICom_QualityType CreateInstance()
	{
		return (UICom_QualityType)UIPackage.CreateObject("Common_External", "Com_QualityType");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		qualityType = GetControllerAt(0);
	}
}
