using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExplainWindow : GComponent
{
	public Controller type;

	public GGraph graph_mask;

	public UIExplain_Com_PVP com_PVP;

	public UIExplain_Com_PVE com_PVE;

	public const string URL = "ui://5wai1nhywwpj0";

	public static UIExplainWindow CreateInstance()
	{
		BindAll();
		return (UIExplainWindow)UIPackage.CreateObject("Explain", "ExplainWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://5wai1nhyl6o66", typeof(UIExplain_Com_PVP));
		UIObjectFactory.SetPackageItemExtension("ui://5wai1nhyl6o6at", typeof(UIExplain_Com_PVE));
		UIObjectFactory.SetPackageItemExtension("ui://5wai1nhywwpj0", typeof(UIExplainWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		graph_mask = (GGraph)GetChildAt(0);
		com_PVP = (UIExplain_Com_PVP)GetChildAt(1);
		com_PVE = (UIExplain_Com_PVE)GetChildAt(2);
	}
}
