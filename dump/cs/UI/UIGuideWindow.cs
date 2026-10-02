using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuideWindow : GComponent
{
	public GGraph graph_TransparentMask;

	public UIGuide_Com_Mash com_GuideMask;

	public UIGuide_Com_Dialog com_Dialog;

	public GGraph graph_Arrow;

	public UIGuide_Com_Arrow com_Arrow;

	public const string URL = "ui://kogqu0l2hj7w0";

	public static UIGuideWindow CreateInstance()
	{
		BindAll();
		return (UIGuideWindow)UIPackage.CreateObject("Guide", "GuideWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://kogqu0l2hj7w0", typeof(UIGuideWindow));
		UIObjectFactory.SetPackageItemExtension("ui://kogqu0l2hj7w1", typeof(UIGuide_Com_Mash));
		UIObjectFactory.SetPackageItemExtension("ui://kogqu0l2hj7w2", typeof(UIGuide_Com_Dialog));
		UIObjectFactory.SetPackageItemExtension("ui://kogqu0l2ln3k3", typeof(UIGuide_Com_Arrow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_TransparentMask = (GGraph)GetChildAt(0);
		com_GuideMask = (UIGuide_Com_Mash)GetChildAt(1);
		com_Dialog = (UIGuide_Com_Dialog)GetChildAt(2);
		graph_Arrow = (GGraph)GetChildAt(3);
		com_Arrow = (UIGuide_Com_Arrow)GetChildAt(4);
	}
}
