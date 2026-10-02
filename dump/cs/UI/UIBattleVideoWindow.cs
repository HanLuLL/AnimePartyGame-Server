using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleVideoWindow : GComponent
{
	public GGraph loader_Video;

	public const string URL = "ui://6xl7rv4tgnfh0";

	public static UIBattleVideoWindow CreateInstance()
	{
		BindAll();
		return (UIBattleVideoWindow)UIPackage.CreateObject("BattleVideo", "BattleVideoWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://6xl7rv4tgnfh0", typeof(UIBattleVideoWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Video = (GGraph)GetChildAt(0);
	}
}
