using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICachaReward_Button_Receive : GButton
{
	public Controller isReceive;

	public GTextField txt_receive;

	public GTextField txt_uncomplet;

	public GTextField txt_progress;

	public GTextField txt_complet;

	public const string URL = "ui://q1a022ssjrp5qq38";

	public static UICachaReward_Button_Receive CreateInstance()
	{
		return (UICachaReward_Button_Receive)UIPackage.CreateObject("GachaRookieReward", "CachaReward_Button_Receive");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isReceive = GetControllerAt(1);
		txt_receive = (GTextField)GetChildAt(2);
		txt_uncomplet = (GTextField)GetChildAt(3);
		txt_progress = (GTextField)GetChildAt(4);
		txt_complet = (GTextField)GetChildAt(5);
	}
}
