using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Button_Banner : GButton
{
	public Controller style;

	public GLoader loader_Image;

	public GTextField txt_Title_1;

	public GTextField txt_Title_2;

	public GTextField txt_Title_3;

	public GRichTextField txt_Price;

	public const string URL = "ui://u7xbdcgursc722";

	public static UIHome_Button_Banner CreateInstance()
	{
		return (UIHome_Button_Banner)UIPackage.CreateObject("Home", "Home_Button_Banner");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		style = GetControllerAt(1);
		loader_Image = (GLoader)GetChildAt(0);
		txt_Title_1 = (GTextField)GetChildAt(1);
		txt_Title_2 = (GTextField)GetChildAt(2);
		txt_Title_3 = (GTextField)GetChildAt(3);
		txt_Price = (GRichTextField)GetChildAt(4);
	}
}
