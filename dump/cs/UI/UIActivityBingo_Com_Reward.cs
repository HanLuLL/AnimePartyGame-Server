using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityBingo_Com_Reward : GComponent
{
	public GLoader bg;

	public UIActivityBingo_Button_Grid grid_0;

	public UIActivityBingo_Button_Grid grid_1;

	public UIActivityBingo_Button_Grid grid_2;

	public UIActivityBingo_Button_Grid grid_3;

	public UIActivityBingo_Button_Grid grid_4;

	public UIActivityBingo_Button_Grid grid_5;

	public UIActivityBingo_Button_Grid grid_6;

	public GButton btn_quit;

	public GTextField txt_title;

	public Transition Cut_in;

	public const string URL = "ui://1pgen0em6brr1h";

	public static UIActivityBingo_Com_Reward CreateInstance()
	{
		return (UIActivityBingo_Com_Reward)UIPackage.CreateObject("ActivityBingo", "ActivityBingo_Com_Reward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bg = (GLoader)GetChildAt(0);
		grid_0 = (UIActivityBingo_Button_Grid)GetChildAt(3);
		grid_1 = (UIActivityBingo_Button_Grid)GetChildAt(4);
		grid_2 = (UIActivityBingo_Button_Grid)GetChildAt(5);
		grid_3 = (UIActivityBingo_Button_Grid)GetChildAt(6);
		grid_4 = (UIActivityBingo_Button_Grid)GetChildAt(7);
		grid_5 = (UIActivityBingo_Button_Grid)GetChildAt(8);
		grid_6 = (UIActivityBingo_Button_Grid)GetChildAt(9);
		btn_quit = (GButton)GetChildAt(10);
		txt_title = (GTextField)GetChildAt(11);
		Cut_in = GetTransitionAt(0);
	}
}
