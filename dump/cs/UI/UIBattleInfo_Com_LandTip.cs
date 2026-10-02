using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_LandTip : GComponent
{
	public GTextField txt_LandName;

	public GLoader loader_Land;

	public Transition showInfo;

	public const string URL = "ui://fxejlqlfp4oi5t";

	public static UIBattleInfo_Com_LandTip CreateInstance()
	{
		return (UIBattleInfo_Com_LandTip)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_LandTip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_LandName = (GTextField)GetChildAt(0);
		loader_Land = (GLoader)GetChildAt(1);
		showInfo = GetTransitionAt(0);
	}
}
