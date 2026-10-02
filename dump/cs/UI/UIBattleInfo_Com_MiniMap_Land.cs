using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_MiniMap_Land : GComponent
{
	public Controller player;

	public GGraph shape;

	public const string URL = "ui://fxejlqlfshy231";

	public static UIBattleInfo_Com_MiniMap_Land CreateInstance()
	{
		return (UIBattleInfo_Com_MiniMap_Land)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_MiniMap_Land");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		player = GetControllerAt(0);
		shape = (GGraph)GetChildAt(0);
	}
}
