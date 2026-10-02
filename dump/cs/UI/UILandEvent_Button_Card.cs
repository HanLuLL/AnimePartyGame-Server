using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandEvent_Button_Card : GButton
{
	public GComponent com_Card;

	public GGraph downEffect;

	public const string URL = "ui://duzt7gzkrum71";

	public static UILandEvent_Button_Card CreateInstance()
	{
		return (UILandEvent_Button_Card)UIPackage.CreateObject("LandEvent", "LandEvent_Button_Card");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Card = (GComponent)GetChildAt(0);
		downEffect = (GGraph)GetChildAt(1);
	}
}
