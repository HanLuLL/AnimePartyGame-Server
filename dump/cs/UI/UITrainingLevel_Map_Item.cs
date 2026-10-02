using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITrainingLevel_Map_Item : GButton
{
	public Controller modeType;

	public GLoader loader_Map;

	public GTextField txt_MapName;

	public GTextField txt_Description;

	public Transition Cut_in;

	public const string URL = "ui://xds9qwo0mgsn1";

	public static UITrainingLevel_Map_Item CreateInstance()
	{
		return (UITrainingLevel_Map_Item)UIPackage.CreateObject("TrainingLevel", "TrainingLevel_Map_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		modeType = GetControllerAt(0);
		loader_Map = (GLoader)GetChildAt(0);
		txt_MapName = (GTextField)GetChildAt(1);
		txt_Description = (GTextField)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
