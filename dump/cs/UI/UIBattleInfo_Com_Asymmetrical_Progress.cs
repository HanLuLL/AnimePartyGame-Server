using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_Asymmetrical_Progress : GComponent
{
	public GGraph bar;

	public GList list;

	public const string URL = "ui://fxejlqlfcz379b";

	public static UIBattleInfo_Com_Asymmetrical_Progress CreateInstance()
	{
		return (UIBattleInfo_Com_Asymmetrical_Progress)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_Asymmetrical_Progress");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bar = (GGraph)GetChildAt(1);
		list = (GList)GetChildAt(2);
	}
}
