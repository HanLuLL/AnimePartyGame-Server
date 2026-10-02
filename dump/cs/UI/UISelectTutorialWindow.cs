using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISelectTutorialWindow : GComponent
{
	public GComponent mohu;

	public GLabel bottom;

	public GButton btn_GuideLevel1;

	public GButton btn_GuideLevel2;

	public Transition Cut_in;

	public const string URL = "ui://pr2pbkngeduv1";

	public static UISelectTutorialWindow CreateInstance()
	{
		BindAll();
		return (UISelectTutorialWindow)UIPackage.CreateObject("SelectTutorial", "SelectTutorialWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://pr2pbkngeduv1", typeof(UISelectTutorialWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GComponent)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		btn_GuideLevel1 = (GButton)GetChildAt(2);
		btn_GuideLevel2 = (GButton)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
