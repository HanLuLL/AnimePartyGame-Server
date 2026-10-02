using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_LandCard : GComponent
{
	public GLoader loader_FrontCard;

	public GTextField txt_Name;

	public GTextField txt_Content;

	public Transition Cut_in;

	public const string URL = "ui://xuaw6o8jpj0z2";

	public static UICom_LandCard CreateInstance()
	{
		return (UICom_LandCard)UIPackage.CreateObject("Common", "Com_LandCard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_FrontCard = (GLoader)GetChildAt(1);
		txt_Name = (GTextField)GetChildAt(2);
		txt_Content = (GTextField)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
