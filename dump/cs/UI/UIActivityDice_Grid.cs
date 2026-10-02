using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityDice_Grid : GComponent
{
	public GTextField txt_id;

	public GLoader loader_icon;

	public Transition Cut_in;

	public const string URL = "ui://lypih982eo8h3";

	public static UIActivityDice_Grid CreateInstance()
	{
		return (UIActivityDice_Grid)UIPackage.CreateObject("ActivityDice", "ActivityDice_Grid");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_id = (GTextField)GetChildAt(2);
		loader_icon = (GLoader)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
