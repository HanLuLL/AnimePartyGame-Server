using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAnniversary_2nd_Com_Goods : GComponent
{
	public UIAnniversary_2nd_Com_Item com_FirstGoods;

	public UIAnniversary_2nd_Com_Item com_SecondGoods;

	public UIAnniversary_2nd_Com_Item com_ThirdGoods;

	public GButton btn_ReturnMain;

	public Transition Cut_in;

	public const string URL = "ui://k49wk9ftnqmf2a";

	public static UIAnniversary_2nd_Com_Goods CreateInstance()
	{
		return (UIAnniversary_2nd_Com_Goods)UIPackage.CreateObject("Anniversary_2Nd", "Anniversary_2nd_Com_Goods");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_FirstGoods = (UIAnniversary_2nd_Com_Item)GetChildAt(0);
		com_SecondGoods = (UIAnniversary_2nd_Com_Item)GetChildAt(1);
		com_ThirdGoods = (UIAnniversary_2nd_Com_Item)GetChildAt(2);
		btn_ReturnMain = (GButton)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
