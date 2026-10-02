using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStore_Com_BottomMask : GComponent
{
	public GImage zheZhao;

	public const string URL = "ui://88m1yfwgtik52y";

	public static UIActivityStore_Com_BottomMask CreateInstance()
	{
		return (UIActivityStore_Com_BottomMask)UIPackage.CreateObject("ActivityStore", "ActivityStore_Com_BottomMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zheZhao = (GImage)GetChildAt(0);
	}
}
