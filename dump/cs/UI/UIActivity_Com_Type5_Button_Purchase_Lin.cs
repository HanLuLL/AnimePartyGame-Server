using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_Button_Purchase_Lin : GButton
{
	public GTextField txt_Title;

	public GLoader loader_Token;

	public GTextField txt_TokenCount;

	public const string URL = "ui://vckl96ksxcsllsq7n";

	public static UIActivity_Com_Type5_Button_Purchase_Lin CreateInstance()
	{
		return (UIActivity_Com_Type5_Button_Purchase_Lin)UIPackage.CreateObject("Activity", "Activity_Com_Type5_Button_Purchase_Lin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(1);
		loader_Token = (GLoader)GetChildAt(2);
		txt_TokenCount = (GTextField)GetChildAt(3);
	}
}
