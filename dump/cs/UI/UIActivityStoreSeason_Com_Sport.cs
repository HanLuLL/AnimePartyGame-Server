using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Com_Sport : GComponent
{
	public GList list_Hero;

	public GButton com_NextSignal;

	public GButton com_PreSignal;

	public GButton btn_Detail;

	public GButton btn_GoSport;

	public GProgressBar progress_Star;

	public UIActivityStoreSeason_Button_Reward btn_Reward1;

	public UIActivityStoreSeason_Button_Reward btn_Reward2;

	public UIActivityStoreSeason_Button_Reward btn_Reward3;

	public UIActivityStoreSeason_Button_Reward btn_Reward4;

	public UIActivityStoreSeason_Button_Reward btn_Reward5;

	public Transition Cut_in;

	public const string URL = "ui://begz6gfvtaoa31";

	public static UIActivityStoreSeason_Com_Sport CreateInstance()
	{
		return (UIActivityStoreSeason_Com_Sport)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_Sport");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Hero = (GList)GetChildAt(1);
		com_NextSignal = (GButton)GetChildAt(2);
		com_PreSignal = (GButton)GetChildAt(3);
		btn_Detail = (GButton)GetChildAt(4);
		btn_GoSport = (GButton)GetChildAt(5);
		progress_Star = (GProgressBar)GetChildAt(7);
		btn_Reward1 = (UIActivityStoreSeason_Button_Reward)GetChildAt(8);
		btn_Reward2 = (UIActivityStoreSeason_Button_Reward)GetChildAt(9);
		btn_Reward3 = (UIActivityStoreSeason_Button_Reward)GetChildAt(10);
		btn_Reward4 = (UIActivityStoreSeason_Button_Reward)GetChildAt(11);
		btn_Reward5 = (UIActivityStoreSeason_Button_Reward)GetChildAt(12);
		Cut_in = GetTransitionAt(0);
	}
}
