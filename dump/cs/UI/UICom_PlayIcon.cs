using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_PlayIcon : GComponent
{
	public GLoader loader_PlayerPhoto;

	public const string URL = "ui://xuaw6o8jz1wk2";

	public static UICom_PlayIcon CreateInstance()
	{
		return (UICom_PlayIcon)UIPackage.CreateObject("Common", "Com_PlayIcon");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_PlayerPhoto = (GLoader)GetChildAt(1);
	}
}
