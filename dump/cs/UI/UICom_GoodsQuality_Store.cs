using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_GoodsQuality_Store : GComponent
{
	public Controller quality;

	public Controller isEmpty;

	public const string URL = "ui://m6sn3r22h0atqq40";

	public static UICom_GoodsQuality_Store CreateInstance()
	{
		return (UICom_GoodsQuality_Store)UIPackage.CreateObject("Common_External", "Com_GoodsQuality_Store");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		quality = GetControllerAt(0);
		isEmpty = GetControllerAt(1);
	}
}
