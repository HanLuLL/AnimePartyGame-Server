using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISkinSell_Com_Goods : GComponent
{
	public GList list_Hero;

	public GButton btn_ReturnMain;

	public UISkinSell_Button_Price btn_Purchase;

	public GComponent com_Label;

	public GGraph graph_LabelGray;

	public GTextField txt_GetLabel;

	public GButton btn_prePage;

	public GButton btn_nextPage;

	public GRichTextField txt_Explain;

	public Transition Cut_in;

	public const string URL = "ui://hmljxqy1ub0x6";

	public static UISkinSell_Com_Goods CreateInstance()
	{
		return (UISkinSell_Com_Goods)UIPackage.CreateObject("SkinSell", "SkinSell_Com_Goods");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Hero = (GList)GetChildAt(1);
		btn_ReturnMain = (GButton)GetChildAt(2);
		btn_Purchase = (UISkinSell_Button_Price)GetChildAt(3);
		com_Label = (GComponent)GetChildAt(4);
		graph_LabelGray = (GGraph)GetChildAt(5);
		txt_GetLabel = (GTextField)GetChildAt(6);
		btn_prePage = (GButton)GetChildAt(8);
		btn_nextPage = (GButton)GetChildAt(9);
		txt_Explain = (GRichTextField)GetChildAt(10);
		Cut_in = GetTransitionAt(0);
	}
}
