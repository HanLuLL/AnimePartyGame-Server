using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIWitchWeapon_Com_GoodsItem : GComponent
{
	public Controller BGType;

	public Controller type;

	public GList list_Prop;

	public GTextField txt_Tips;

	public UIWitchWeapon_Button_Purchase btn_Purchase;

	public GGraph graph_FirstTheme;

	public GGraph graph_SecondTheme;

	public GGraph graph_Skin;

	public GTextField txt_RoleTitle;

	public GComponent com_PlayerLabel;

	public GTextField txt_LabelTitle;

	public const string URL = "ui://hn2q98k6kqgj15";

	public static UIWitchWeapon_Com_GoodsItem CreateInstance()
	{
		return (UIWitchWeapon_Com_GoodsItem)UIPackage.CreateObject("WitchWeapon", "WitchWeapon_Com_GoodsItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		BGType = GetControllerAt(0);
		type = GetControllerAt(1);
		list_Prop = (GList)GetChildAt(2);
		txt_Tips = (GTextField)GetChildAt(3);
		btn_Purchase = (UIWitchWeapon_Button_Purchase)GetChildAt(4);
		graph_FirstTheme = (GGraph)GetChildAt(5);
		graph_SecondTheme = (GGraph)GetChildAt(6);
		graph_Skin = (GGraph)GetChildAt(8);
		txt_RoleTitle = (GTextField)GetChildAt(9);
		com_PlayerLabel = (GComponent)GetChildAt(10);
		txt_LabelTitle = (GTextField)GetChildAt(11);
	}
}
