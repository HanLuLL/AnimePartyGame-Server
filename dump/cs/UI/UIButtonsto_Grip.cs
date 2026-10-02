using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButtonsto_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://zyd0rl00fxqjqq5h";

	public static UIButtonsto_Grip CreateInstance()
	{
		return (UIButtonsto_Grip)UIPackage.CreateObject("Store", "Buttonsto_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
