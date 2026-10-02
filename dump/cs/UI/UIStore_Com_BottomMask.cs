using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_BottomMask : GComponent
{
	public GImage zheZhao;

	public const string URL = "ui://zyd0rl00r3jjqq4l";

	public static UIStore_Com_BottomMask CreateInstance()
	{
		return (UIStore_Com_BottomMask)UIPackage.CreateObject("Store", "Store_Com_BottomMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zheZhao = (GImage)GetChildAt(0);
	}
}
