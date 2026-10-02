using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Item : GButton
{
	public Controller qualityType;

	public Controller isDeleteByTime;

	public Controller isNew;

	public Controller isEmpty;

	public Controller isSelected;

	public Controller isShowNum;

	public Controller isOwn;

	public GLoader loader_Icon;

	public UICom_ItemType com_ItemType;

	public GTextField txt_itemNum;

	public Transition Cut_in;

	public const string URL = "ui://m6sn3r22jn4e9a";

	public static UICom_Item CreateInstance()
	{
		return (UICom_Item)UIPackage.CreateObject("Common_External", "Com_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		qualityType = GetControllerAt(0);
		isDeleteByTime = GetControllerAt(1);
		isNew = GetControllerAt(2);
		isEmpty = GetControllerAt(3);
		isSelected = GetControllerAt(4);
		isShowNum = GetControllerAt(6);
		isOwn = GetControllerAt(7);
		loader_Icon = (GLoader)GetChildAt(5);
		com_ItemType = (UICom_ItemType)GetChildAt(6);
		txt_itemNum = (GTextField)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
