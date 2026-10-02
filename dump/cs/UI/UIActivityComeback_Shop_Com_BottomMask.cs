using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Shop_Com_BottomMask : GComponent
{
	public GImage zheZhao;

	public const string URL = "ui://hconmwfcy9qnp";

	public static UIActivityComeback_Shop_Com_BottomMask CreateInstance()
	{
		return (UIActivityComeback_Shop_Com_BottomMask)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Shop_Com_BottomMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zheZhao = (GImage)GetChildAt(0);
	}
}
