using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICacha_Button_Gacha : GButton
{
	public GImage Image_90001_Ten;

	public GTextField txt_Name;

	public GLoader loader_Icon;

	public GTextField txt_Count;

	public Transition Cut_in;

	public const string URL = "ui://j90wpcmnvpjm7";

	public static UICacha_Button_Gacha CreateInstance()
	{
		return (UICacha_Button_Gacha)UIPackage.CreateObject("Gacha", "Cacha_Button_Gacha");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Image_90001_Ten = (GImage)GetChildAt(0);
		txt_Name = (GTextField)GetChildAt(2);
		loader_Icon = (GLoader)GetChildAt(3);
		txt_Count = (GTextField)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
