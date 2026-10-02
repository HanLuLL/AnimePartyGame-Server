using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMessageBoxWindow : GComponent
{
	public Controller showCancelDouble;

	public GComponent mohu;

	public GLabel bottom;

	public GRichTextField content;

	public UIMessageBox_Button_DoubleStatus btn_DoubleStatus;

	public Transition Cut_in;

	public const string URL = "ui://fvoxvgwshc1o0";

	public static UIMessageBoxWindow CreateInstance()
	{
		BindAll();
		return (UIMessageBoxWindow)UIPackage.CreateObject("MessageBox", "MessageBoxWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://fvoxvgwshc1o0", typeof(UIMessageBoxWindow));
		UIObjectFactory.SetPackageItemExtension("ui://fvoxvgwstiv3d", typeof(UIMessageBox_Button_DoubleStatus));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showCancelDouble = GetControllerAt(0);
		mohu = (GComponent)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		content = (GRichTextField)GetChildAt(2);
		btn_DoubleStatus = (UIMessageBox_Button_DoubleStatus)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
