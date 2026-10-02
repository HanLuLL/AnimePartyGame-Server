using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIShowSkin_Com_Preivew : GComponent
{
	public Controller LabelStatus;

	public GComponent com_PlayerLabel;

	public UIShowSkin_Button_Video btn_Video;

	public GTextField txt_LabelName;

	public GList list_Way;

	public GGroup group_Way;

	public const string URL = "ui://zfulrgf7cabpqqc";

	public static UIShowSkin_Com_Preivew CreateInstance()
	{
		return (UIShowSkin_Com_Preivew)UIPackage.CreateObject("ShowSkin", "ShowSkin_Com_Preivew");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		LabelStatus = GetControllerAt(0);
		com_PlayerLabel = (GComponent)GetChildAt(0);
		btn_Video = (UIShowSkin_Button_Video)GetChildAt(1);
		txt_LabelName = (GTextField)GetChildAt(2);
		list_Way = (GList)GetChildAt(6);
		group_Way = (GGroup)GetChildAt(7);
	}
}
