using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIUpgrade_Com_Character : GComponent
{
	public GLoader loader_Character;

	public const string URL = "ui://6vzgbzmwmsij1v";

	public static UIUpgrade_Com_Character CreateInstance()
	{
		return (UIUpgrade_Com_Character)UIPackage.CreateObject("Upgrade", "Upgrade_Com_Character");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Character = (GLoader)GetChildAt(0);
	}
}
