using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_Watch_FollowSwitch : GComponent
{
	public GButton btn_switch;

	public Transition Cut_out;

	public const string URL = "ui://fxejlqlfiyw3c8";

	public static UIBattleInfo_Com_Watch_FollowSwitch CreateInstance()
	{
		return (UIBattleInfo_Com_Watch_FollowSwitch)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_Watch_FollowSwitch");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_switch = (GButton)GetChildAt(0);
		Cut_out = GetTransitionAt(0);
	}
}
