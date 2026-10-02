using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIVA11HallA_Com_Goods : GComponent
{
	public GGraph graph_FirstTheme;

	public GGraph graph_Skin;

	public GTextField txt_RoleTitle;

	public GComponent com_DynamicLabel;

	public GComponent com_StaticLabel;

	public GTextField txt_StaticLabelTitle;

	public GTextField txt_DynamicLabelTitle;

	public GList list_Prop;

	public UIVA11HallA_Button_Purchase btn_Purchase;

	public Transition Cut_in;

	public const string URL = "ui://zlsk81wwgu0j2d";

	public static UIVA11HallA_Com_Goods CreateInstance()
	{
		return (UIVA11HallA_Com_Goods)UIPackage.CreateObject("VA11HallA", "VA11HallA_Com_Goods");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_FirstTheme = (GGraph)GetChildAt(0);
		graph_Skin = (GGraph)GetChildAt(1);
		txt_RoleTitle = (GTextField)GetChildAt(2);
		com_DynamicLabel = (GComponent)GetChildAt(3);
		com_StaticLabel = (GComponent)GetChildAt(4);
		txt_StaticLabelTitle = (GTextField)GetChildAt(5);
		txt_DynamicLabelTitle = (GTextField)GetChildAt(6);
		list_Prop = (GList)GetChildAt(7);
		btn_Purchase = (UIVA11HallA_Button_Purchase)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
	}
}
