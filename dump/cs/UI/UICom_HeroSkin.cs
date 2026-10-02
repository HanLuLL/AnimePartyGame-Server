using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_HeroSkin : GComponent
{
	public Controller type;

	public GLoader loader_Skin_Image;

	public GGraph loader_Skin_Video;

	public const string URL = "ui://xuaw6o8jak421h";

	public static UICom_HeroSkin CreateInstance()
	{
		return (UICom_HeroSkin)UIPackage.CreateObject("Common", "Com_HeroSkin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		loader_Skin_Image = (GLoader)GetChildAt(0);
		loader_Skin_Video = (GGraph)GetChildAt(1);
	}
}
