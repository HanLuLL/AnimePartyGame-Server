using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIVA11HallA_Button_Purchase : GButton
{
	public GTextField txt_Countdown;

	public GTextField txt_Topic;

	public GRichTextField txt_Price;

	public const string URL = "ui://zlsk81wwgu0j2e";

	public static UIVA11HallA_Button_Purchase CreateInstance()
	{
		return (UIVA11HallA_Button_Purchase)UIPackage.CreateObject("VA11HallA", "VA11HallA_Button_Purchase");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Countdown = (GTextField)GetChildAt(2);
		txt_Topic = (GTextField)GetChildAt(3);
		txt_Price = (GRichTextField)GetChildAt(4);
	}
}
