using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_Minimap : GComponent
{
	public GImage image_Frame;

	public const string URL = "ui://fxejlqlfvi802u";

	public static UIBattleInfo_Com_Minimap CreateInstance()
	{
		return (UIBattleInfo_Com_Minimap)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_Minimap");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		image_Frame = (GImage)GetChildAt(2);
	}
}
