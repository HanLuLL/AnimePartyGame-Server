using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_MapInfo : GComponent
{
	public GLoader loader_Map;

	public GList list_MapEventSelect;

	public GList list_MapEvent;

	public const string URL = "ui://m6sn3r22px78j9j";

	public static UICom_MapInfo CreateInstance()
	{
		return (UICom_MapInfo)UIPackage.CreateObject("Common_External", "Com_MapInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Map = (GLoader)GetChildAt(2);
		list_MapEventSelect = (GList)GetChildAt(3);
		list_MapEvent = (GList)GetChildAt(4);
	}
}
