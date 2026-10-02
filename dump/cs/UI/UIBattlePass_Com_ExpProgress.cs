using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_ExpProgress : GComponent
{
	public Controller status;

	public GProgressBar progress_Exp;

	public GImage image_Arrow;

	public GTextField txt_NextLevel;

	public const string URL = "ui://ssf8xg9njz2412";

	public static UIBattlePass_Com_ExpProgress CreateInstance()
	{
		return (UIBattlePass_Com_ExpProgress)UIPackage.CreateObject("BattlePass", "BattlePass_Com_ExpProgress");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		progress_Exp = (GProgressBar)GetChildAt(0);
		image_Arrow = (GImage)GetChildAt(2);
		txt_NextLevel = (GTextField)GetChildAt(4);
	}
}
