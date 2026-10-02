using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_BuildingLevelSlider : GComponent
{
	public Controller type;

	public GImage img_Slider;

	public const string URL = "ui://mi9vm3w0kgpqq6x";

	public static UISinglePlayer_Com_BuildingLevelSlider CreateInstance()
	{
		return (UISinglePlayer_Com_BuildingLevelSlider)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_BuildingLevelSlider");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		img_Slider = (GImage)GetChildAt(1);
	}
}
