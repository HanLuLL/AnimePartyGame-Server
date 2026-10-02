using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleinfo_Com_Speedani : GComponent
{
	public Transition Cut_in;

	public const string URL = "ui://fxejlqlfjf4abl";

	public static UIBattleinfo_Com_Speedani CreateInstance()
	{
		return (UIBattleinfo_Com_Speedani)UIPackage.CreateObject("BattleInfo", "Battleinfo_Com_Speedani");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
