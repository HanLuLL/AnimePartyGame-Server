using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Com_Token : GComponent
{
	public Controller switchActivity;

	public GLoader loader_ActivityProp;

	public GTextField txt_ActivityPropCount;

	public const string URL = "ui://begz6gfv7vwmq";

	public static UIActivityStoreSeason_Com_Token CreateInstance()
	{
		return (UIActivityStoreSeason_Com_Token)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_Token");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		switchActivity = GetControllerAt(0);
		loader_ActivityProp = (GLoader)GetChildAt(0);
		txt_ActivityPropCount = (GTextField)GetChildAt(1);
	}
}
