using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIInputWindow : GComponent
{
	public Controller type;

	public GGraph mohu;

	public GLabel bottom;

	public GTextInput textField_PWD;

	public GTextInput textField_AccountNick;

	public GTextInput textField_AccountPWD;

	public const string URL = "ui://24mdqppqns6kb";

	public static UIInputWindow CreateInstance()
	{
		BindAll();
		return (UIInputWindow)UIPackage.CreateObject("Input", "InputWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://24mdqppqns6kb", typeof(UIInputWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		mohu = (GGraph)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		textField_PWD = (GTextInput)GetChildAt(4);
		textField_AccountNick = (GTextInput)GetChildAt(8);
		textField_AccountPWD = (GTextInput)GetChildAt(11);
	}
}
