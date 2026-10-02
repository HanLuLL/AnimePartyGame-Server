using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITrainingLevelPanel : GComponent
{
	public Controller tab;

	public GButton btn_Return;

	public GList list_Maps;

	public GComponent com_CreateRoom;

	public const string URL = "ui://xds9qwo0mgsn0";

	public static UITrainingLevelPanel CreateInstance()
	{
		BindAll();
		return (UITrainingLevelPanel)UIPackage.CreateObject("TrainingLevel", "TrainingLevelPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://xds9qwo0mgsn0", typeof(UITrainingLevelPanel));
		UIObjectFactory.SetPackageItemExtension("ui://xds9qwo0mgsn1", typeof(UITrainingLevel_Map_Item));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		btn_Return = (GButton)GetChildAt(0);
		list_Maps = (GList)GetChildAt(1);
		com_CreateRoom = (GComponent)GetChildAt(2);
	}
}
