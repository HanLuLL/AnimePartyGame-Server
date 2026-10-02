using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_PVEProgressList : GComponent
{
	public Controller taskStatus;

	public GList list_Progress;

	public const string URL = "ui://fxejlqlfr4n9c7";

	public static UIBattleInfo_Com_PVEProgressList CreateInstance()
	{
		return (UIBattleInfo_Com_PVEProgressList)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_PVEProgressList");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		taskStatus = GetControllerAt(0);
		list_Progress = (GList)GetChildAt(2);
	}
}
