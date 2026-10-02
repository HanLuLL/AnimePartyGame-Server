using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICacha_Button_RookieReward : GButton
{
	public Controller isReview;

	public GTextField txt_review;

	public GTextField txt_receive;

	public GTextField txt_received;

	public const string URL = "ui://j90wpcmng85eqq2w";

	public static UICacha_Button_RookieReward CreateInstance()
	{
		return (UICacha_Button_RookieReward)UIPackage.CreateObject("Gacha", "Cacha_Button_RookieReward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isReview = GetControllerAt(1);
		txt_review = (GTextField)GetChildAt(2);
		txt_receive = (GTextField)GetChildAt(3);
		txt_received = (GTextField)GetChildAt(4);
	}
}
