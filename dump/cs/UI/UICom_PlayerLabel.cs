using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_PlayerLabel : GComponent
{
	public Controller showLevel;

	public UICom_PlayerLabel_Loader loader_Label;

	public UICom_PlayIcon com_PlayerPhoto;

	public GImage image_Icon;

	public UICom_PlayerName com_Name;

	public GTextField txt_lv;

	public const string URL = "ui://xuaw6o8jz1wk0";

	public static UICom_PlayerLabel CreateInstance()
	{
		return (UICom_PlayerLabel)UIPackage.CreateObject("Common", "Com_PlayerLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showLevel = GetControllerAt(0);
		loader_Label = (UICom_PlayerLabel_Loader)GetChildAt(1);
		com_PlayerPhoto = (UICom_PlayIcon)GetChildAt(2);
		image_Icon = (GImage)GetChildAt(3);
		com_Name = (UICom_PlayerName)GetChildAt(4);
		txt_lv = (GTextField)GetChildAt(6);
	}
}
