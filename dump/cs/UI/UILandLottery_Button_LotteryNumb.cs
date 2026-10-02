using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandLottery_Button_LotteryNumb : GButton
{
	public Controller choosed;

	public Controller showSeleted;

	public UILandLottery_Com_Number com_Numb;

	public GLoader image_choose;

	public GLoader image_choosed;

	public Transition Cut_in;

	public const string URL = "ui://d6gnxdx1rum72";

	public static UILandLottery_Button_LotteryNumb CreateInstance()
	{
		return (UILandLottery_Button_LotteryNumb)UIPackage.CreateObject("LandLottery", "LandLottery_Button_LotteryNumb");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		choosed = GetControllerAt(1);
		showSeleted = GetControllerAt(2);
		com_Numb = (UILandLottery_Com_Number)GetChildAt(0);
		image_choose = (GLoader)GetChildAt(1);
		image_choosed = (GLoader)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
