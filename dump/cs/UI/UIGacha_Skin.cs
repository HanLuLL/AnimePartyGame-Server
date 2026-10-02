using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_Skin : GComponent
{
	public Controller poolType;

	public UICacha_Button_Gacha2 btn_GachaOne;

	public UICacha_Button_Gacha2 btn_GachaMulti;

	public UIProgress_Reward progress_reward;

	public UIGacha_Com_UpDesc com_UpDesc;

	public GGraph loader_UpAnimation;

	public Transition Cut_in;

	public const string URL = "ui://j90wpcmnfs8hqq2d";

	public static UIGacha_Skin CreateInstance()
	{
		return (UIGacha_Skin)UIPackage.CreateObject("Gacha", "Gacha_Skin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		poolType = GetControllerAt(0);
		btn_GachaOne = (UICacha_Button_Gacha2)GetChildAt(0);
		btn_GachaMulti = (UICacha_Button_Gacha2)GetChildAt(1);
		progress_reward = (UIProgress_Reward)GetChildAt(3);
		com_UpDesc = (UIGacha_Com_UpDesc)GetChildAt(4);
		loader_UpAnimation = (GGraph)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
