using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_SettleMission_Item : GComponent
{
	public UISinglePlayer_Com_CardQuality com_Quality;

	public GLoader loader_Icon;

	public const string URL = "ui://mi9vm3w0jsh0q8j";

	public static UISinglePlayer_SettleMission_Item CreateInstance()
	{
		return (UISinglePlayer_SettleMission_Item)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_SettleMission_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Quality = (UISinglePlayer_Com_CardQuality)GetChildAt(0);
		loader_Icon = (GLoader)GetChildAt(1);
	}
}
