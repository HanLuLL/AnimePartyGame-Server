using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBottomMenuPanel : GComponent
{
	public Controller hide;

	public Controller type;

	public Controller showPlayerLabel;

	public Controller BGSet;

	public GGraph graph_1;

	public GGraph graph_2;

	public GList list_Token;

	public GList list_Function;

	public GComponent com_Label;

	public GProgressBar slider_Exp;

	public Transition Cut_in;

	public const string URL = "ui://ybwxnbf5rvyb0";

	public static UIBottomMenuPanel CreateInstance()
	{
		BindAll();
		return (UIBottomMenuPanel)UIPackage.CreateObject("BottomMenu", "BottomMenuPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://ybwxnbf5qzg81d", typeof(UIBottomMenu_Button_Function));
		UIObjectFactory.SetPackageItemExtension("ui://ybwxnbf5rvyb0", typeof(UIBottomMenuPanel));
		UIObjectFactory.SetPackageItemExtension("ui://ybwxnbf5z52sy", typeof(UIBottomMenu_Button_Token));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		hide = GetControllerAt(0);
		type = GetControllerAt(1);
		showPlayerLabel = GetControllerAt(2);
		BGSet = GetControllerAt(3);
		graph_1 = (GGraph)GetChildAt(0);
		graph_2 = (GGraph)GetChildAt(1);
		list_Token = (GList)GetChildAt(2);
		list_Function = (GList)GetChildAt(3);
		com_Label = (GComponent)GetChildAt(4);
		slider_Exp = (GProgressBar)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
