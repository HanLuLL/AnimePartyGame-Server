using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICacha_Button_Gacha2 : GButton
{
	public GTextField txt_Name;

	public GLoader loader_Icon;

	public GTextField txt_Count;

	public Transition Cut_in;

	public const string URL = "ui://j90wpcmnfs8hqq2b";

	public static UICacha_Button_Gacha2 CreateInstance()
	{
		return (UICacha_Button_Gacha2)UIPackage.CreateObject("Gacha", "Cacha_Button_Gacha2");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Name = (GTextField)GetChildAt(1);
		loader_Icon = (GLoader)GetChildAt(2);
		txt_Count = (GTextField)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
