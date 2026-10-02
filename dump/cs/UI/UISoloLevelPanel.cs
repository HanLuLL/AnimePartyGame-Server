using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISoloLevelPanel : GComponent
{
	public GButton btn_Return;

	public GButton btn_BasicMode;

	public UISoloLevel_Button_Training btn_TrainingMode;

	public UISoloLevel_Button_ToDevelop btn_ToSoloMode;

	public Transition Cut_in;

	public const string URL = "ui://xuxzg1y1mgsn0";

	public static UISoloLevelPanel CreateInstance()
	{
		BindAll();
		return (UISoloLevelPanel)UIPackage.CreateObject("SoloLevel", "SoloLevelPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://xuxzg1y1cgxz11", typeof(UISoloLevel_Button_ToDevelop));
		UIObjectFactory.SetPackageItemExtension("ui://xuxzg1y1mgsn0", typeof(UISoloLevelPanel));
		UIObjectFactory.SetPackageItemExtension("ui://xuxzg1y1mgsn2", typeof(UISoloLevel_Button_Training));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Return = (GButton)GetChildAt(0);
		btn_BasicMode = (GButton)GetChildAt(1);
		btn_TrainingMode = (UISoloLevel_Button_Training)GetChildAt(2);
		btn_ToSoloMode = (UISoloLevel_Button_ToDevelop)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
