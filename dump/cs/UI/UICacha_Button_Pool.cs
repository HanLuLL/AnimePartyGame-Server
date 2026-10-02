using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICacha_Button_Pool : GButton
{
	public Controller redPoint;

	public GLoader loader_Item;

	public const string URL = "ui://j90wpcmnqzg81";

	public static UICacha_Button_Pool CreateInstance()
	{
		return (UICacha_Button_Pool)UIPackage.CreateObject("Gacha", "Cacha_Button_Pool");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		loader_Item = (GLoader)GetChildAt(2);
	}
}
