using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandLottery_Com_Cheer : GComponent
{
	public GGraph loader_Animation;

	public const string URL = "ui://d6gnxdx1rum7r";

	public static UILandLottery_Com_Cheer CreateInstance()
	{
		return (UILandLottery_Com_Cheer)UIPackage.CreateObject("LandLottery", "LandLottery_Com_Cheer");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Animation = (GGraph)GetChildAt(0);
	}
}
