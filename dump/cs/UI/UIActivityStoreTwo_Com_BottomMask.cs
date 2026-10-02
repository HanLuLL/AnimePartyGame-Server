using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreTwo_Com_BottomMask : GComponent
{
	public GImage zheZhao;

	public const string URL = "ui://6dt5s4htqw8h15";

	public static UIActivityStoreTwo_Com_BottomMask CreateInstance()
	{
		return (UIActivityStoreTwo_Com_BottomMask)UIPackage.CreateObject("ActivityStoreTwo", "ActivityStoreTwo_Com_BottomMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zheZhao = (GImage)GetChildAt(0);
	}
}
