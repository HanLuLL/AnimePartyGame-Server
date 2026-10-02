using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_MapInfo : GComponent
{
	public Controller showMap;

	public UIBattleInfo_Com_Minimap com_Minimap;

	public GButton btn_ShowMap;

	public Transition showMinimap;

	public Transition closeMinimap;

	public const string URL = "ui://fxejlqlfg1i48n";

	public static UIBattleInfo_Com_MapInfo CreateInstance()
	{
		return (UIBattleInfo_Com_MapInfo)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_MapInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showMap = GetControllerAt(0);
		com_Minimap = (UIBattleInfo_Com_Minimap)GetChildAt(0);
		btn_ShowMap = (GButton)GetChildAt(1);
		showMinimap = GetTransitionAt(0);
		closeMinimap = GetTransitionAt(1);
	}
}
