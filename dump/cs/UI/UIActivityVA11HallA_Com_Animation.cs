using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityVA11HallA_Com_Animation : GComponent
{
	public Controller ShowMovie;

	public const string URL = "ui://zlysd2gugu0j76";

	public static UIActivityVA11HallA_Com_Animation CreateInstance()
	{
		return (UIActivityVA11HallA_Com_Animation)UIPackage.CreateObject("ActivityVA11HallA", "ActivityVA11HallA_Com_Animation");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		ShowMovie = GetControllerAt(0);
	}
}
