using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_MediumItem : GComponent
{
	public Controller type;

	public GLoader loader_Image;

	public GGraph loader_Video;

	public GTextField txt_title;

	public const string URL = "ui://ssf8xg9njz2425";

	public static UIBattlePass_Com_MediumItem CreateInstance()
	{
		return (UIBattlePass_Com_MediumItem)UIPackage.CreateObject("BattlePass", "BattlePass_Com_MediumItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		loader_Image = (GLoader)GetChildAt(0);
		loader_Video = (GGraph)GetChildAt(1);
		txt_title = (GTextField)GetChildAt(2);
	}
}
