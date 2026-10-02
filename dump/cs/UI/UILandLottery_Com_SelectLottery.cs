using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandLottery_Com_SelectLottery : GComponent
{
	public Controller swf;

	public GLoader loader_Paint;

	public GLoader loader_SwfPaint;

	public GList list_SelectNum;

	public UILandLottery_Button_Lottery_Selection btn_Confirm;

	public GLoader loader_Player;

	public GTextField txt_hasLottery;

	public Transition cut_in;

	public const string URL = "ui://d6gnxdx1qzg8y";

	public static UILandLottery_Com_SelectLottery CreateInstance()
	{
		return (UILandLottery_Com_SelectLottery)UIPackage.CreateObject("LandLottery", "LandLottery_Com_SelectLottery");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		swf = GetControllerAt(0);
		loader_Paint = (GLoader)GetChildAt(1);
		loader_SwfPaint = (GLoader)GetChildAt(2);
		list_SelectNum = (GList)GetChildAt(3);
		btn_Confirm = (UILandLottery_Button_Lottery_Selection)GetChildAt(4);
		loader_Player = (GLoader)GetChildAt(6);
		txt_hasLottery = (GTextField)GetChildAt(7);
		cut_in = GetTransitionAt(0);
	}
}
