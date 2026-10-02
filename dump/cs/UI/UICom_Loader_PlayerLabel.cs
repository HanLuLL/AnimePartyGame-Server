using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Loader_PlayerLabel : GComponent
{
	public Controller type;

	public GLoader loader_Image;

	public GGraph loader_Video;

	public const string URL = "ui://xuaw6o8jheh5a5";

	public static UICom_Loader_PlayerLabel CreateInstance()
	{
		return (UICom_Loader_PlayerLabel)UIPackage.CreateObject("Common", "Com_Loader_PlayerLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		loader_Image = (GLoader)GetChildAt(0);
		loader_Video = (GGraph)GetChildAt(1);
	}
}
