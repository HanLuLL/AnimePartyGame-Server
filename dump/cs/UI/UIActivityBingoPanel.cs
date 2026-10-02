using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityBingoPanel : GComponent
{
	public GGraph mohu;

	public GLoader bg;

	public GList list_grid;

	public UIActivityBingo_Button_Grid exGrid_0;

	public UIActivityBingo_Button_Grid exGrid_1;

	public UIActivityBingo_Button_Grid exGrid_2;

	public UIActivityBingo_Button_Grid exGrid_3;

	public UIActivityBingo_Button_Grid exGrid_4;

	public UIActivityBingo_Button_Grid exGrid_5;

	public UIActivityBingo_Button_Grid exGrid_6;

	public UIActivityBingo_Button_Done btn_done;

	public GTextField txt_title;

	public GTextField txt_date;

	public GTextField txt_coin;

	public GList list_mission;

	public UIActivityBingo_Com_Reward com_reward;

	public Transition Cut_in;

	public const string URL = "ui://1pgen0em6brr0";

	public static UIActivityBingoPanel CreateInstance()
	{
		BindAll();
		return (UIActivityBingoPanel)UIPackage.CreateObject("ActivityBingo", "ActivityBingoPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://1pgen0em6brr0", typeof(UIActivityBingoPanel));
		UIObjectFactory.SetPackageItemExtension("ui://1pgen0em6brr13", typeof(UIActivityBingo_Com_Mission));
		UIObjectFactory.SetPackageItemExtension("ui://1pgen0em6brr19", typeof(UIActivityBingo_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://1pgen0em6brr1h", typeof(UIActivityBingo_Com_Reward));
		UIObjectFactory.SetPackageItemExtension("ui://1pgen0em6brrm", typeof(UIActivityBingo_Button_Grid));
		UIObjectFactory.SetPackageItemExtension("ui://1pgen0em6brro", typeof(UIActivityBingo_Button_Done));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		bg = (GLoader)GetChildAt(1);
		list_grid = (GList)GetChildAt(4);
		exGrid_0 = (UIActivityBingo_Button_Grid)GetChildAt(5);
		exGrid_1 = (UIActivityBingo_Button_Grid)GetChildAt(6);
		exGrid_2 = (UIActivityBingo_Button_Grid)GetChildAt(7);
		exGrid_3 = (UIActivityBingo_Button_Grid)GetChildAt(8);
		exGrid_4 = (UIActivityBingo_Button_Grid)GetChildAt(9);
		exGrid_5 = (UIActivityBingo_Button_Grid)GetChildAt(10);
		exGrid_6 = (UIActivityBingo_Button_Grid)GetChildAt(11);
		btn_done = (UIActivityBingo_Button_Done)GetChildAt(12);
		txt_title = (GTextField)GetChildAt(15);
		txt_date = (GTextField)GetChildAt(16);
		txt_coin = (GTextField)GetChildAt(19);
		list_mission = (GList)GetChildAt(20);
		com_reward = (UIActivityBingo_Com_Reward)GetChildAt(21);
		Cut_in = GetTransitionAt(0);
	}
}
