using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Com_SportInfoItem : GComponent
{
	public GTextField txt_Times;

	public GTextField txt_Title;

	public const string URL = "ui://begz6gfvtaoa3b";

	public static UIActivityStoreSeason_Com_SportInfoItem CreateInstance()
	{
		return (UIActivityStoreSeason_Com_SportInfoItem)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_SportInfoItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Times = (GTextField)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(2);
	}
}
