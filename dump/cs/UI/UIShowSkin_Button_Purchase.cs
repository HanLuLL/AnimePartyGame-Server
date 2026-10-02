using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIShowSkin_Button_Purchase : GButton
{
	public GLoader loader_TokenSymbol;

	public const string URL = "ui://zfulrgf7qdq53";

	public static UIShowSkin_Button_Purchase CreateInstance()
	{
		return (UIShowSkin_Button_Purchase)UIPackage.CreateObject("ShowSkin", "ShowSkin_Button_Purchase");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_TokenSymbol = (GLoader)GetChildAt(2);
	}
}
