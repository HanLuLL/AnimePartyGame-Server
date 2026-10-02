using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_GoodsQuality : GComponent
{
	public Controller quality;

	public Controller isEmpty;

	public const string URL = "ui://m6sn3r22ot0wbc";

	public static UICom_GoodsQuality CreateInstance()
	{
		return (UICom_GoodsQuality)UIPackage.CreateObject("Common_External", "Com_GoodsQuality");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		quality = GetControllerAt(0);
		isEmpty = GetControllerAt(1);
	}
}
