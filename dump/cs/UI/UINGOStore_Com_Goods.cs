using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINGOStore_Com_Goods : GComponent
{
	public UINGOStore_Com_GoodsItem com_AngelChan;

	public UINGOStore_Com_GoodsItem com_TangTang;

	public UINGOStore_Com_GoodsItem com_Pack;

	public Transition Cut_in;

	public const string URL = "ui://na6sy4s6kqgj14";

	public static UINGOStore_Com_Goods CreateInstance()
	{
		return (UINGOStore_Com_Goods)UIPackage.CreateObject("NGOStore", "NGOStore_Com_Goods");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_AngelChan = (UINGOStore_Com_GoodsItem)GetChildAt(0);
		com_TangTang = (UINGOStore_Com_GoodsItem)GetChildAt(1);
		com_Pack = (UINGOStore_Com_GoodsItem)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
