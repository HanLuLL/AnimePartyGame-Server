using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Expression : GComponent
{
	public Controller type;

	public GLoader loader_Expression_Image;

	public GGraph loader_Expression_Video;

	public const string URL = "ui://xuaw6o8ju43ct";

	public static UICom_Expression CreateInstance()
	{
		return (UICom_Expression)UIPackage.CreateObject("Common", "Com_Expression");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		loader_Expression_Image = (GLoader)GetChildAt(0);
		loader_Expression_Video = (GGraph)GetChildAt(1);
	}
}
