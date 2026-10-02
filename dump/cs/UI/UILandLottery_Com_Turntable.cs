using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandLottery_Com_Turntable : GComponent
{
	public GTextField num1;

	public GTextField num2;

	public GTextField num3;

	public GTextField num4;

	public GTextField num5;

	public GTextField num6;

	public GTextField num7;

	public GTextField num8;

	public GTextField num9;

	public GTextField num10;

	public GTextField num11;

	public GTextField num12;

	public const string URL = "ui://d6gnxdx1rum7o";

	public static UILandLottery_Com_Turntable CreateInstance()
	{
		return (UILandLottery_Com_Turntable)UIPackage.CreateObject("LandLottery", "LandLottery_Com_Turntable");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		num1 = (GTextField)GetChildAt(4);
		num2 = (GTextField)GetChildAt(5);
		num3 = (GTextField)GetChildAt(6);
		num4 = (GTextField)GetChildAt(7);
		num5 = (GTextField)GetChildAt(8);
		num6 = (GTextField)GetChildAt(9);
		num7 = (GTextField)GetChildAt(10);
		num8 = (GTextField)GetChildAt(11);
		num9 = (GTextField)GetChildAt(12);
		num10 = (GTextField)GetChildAt(13);
		num11 = (GTextField)GetChildAt(14);
		num12 = (GTextField)GetChildAt(15);
	}
}
