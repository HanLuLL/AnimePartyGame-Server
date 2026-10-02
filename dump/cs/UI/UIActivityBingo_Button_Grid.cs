using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityBingo_Button_Grid : GButton
{
	public Controller state;

	public GLoader loader_icon;

	public GTextField txt_icon;

	public GTextField txt_num;

	public GLoader loader_RedPoint;

	public Transition Cut_in;

	public const string URL = "ui://1pgen0em6brrm";

	public static UIActivityBingo_Button_Grid CreateInstance()
	{
		return (UIActivityBingo_Button_Grid)UIPackage.CreateObject("ActivityBingo", "ActivityBingo_Button_Grid");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
		loader_icon = (GLoader)GetChildAt(1);
		txt_icon = (GTextField)GetChildAt(2);
		txt_num = (GTextField)GetChildAt(5);
		loader_RedPoint = (GLoader)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
