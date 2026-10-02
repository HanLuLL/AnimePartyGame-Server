using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace UI;

public class UICom_UniqueNum : GComponent
{
	public Controller status;

	public Controller type;

	public GGroup group_Counter;

	public GTextField txt_Counter;

	public const string URL = "ui://1ov1i0v9kxm5s8d";

	public void Refresh(int count)
	{
		txt_Counter.visible = count > 0;
		group_Counter.visible = count > 0;
		if (count > 0)
		{
			txt_Counter.text = count.ToString();
			status.selectedIndex = Mathf.Clamp(count - 1, 0, 3);
		}
	}

	public static UICom_UniqueNum CreateInstance()
	{
		return (UICom_UniqueNum)UIPackage.CreateObject("Common_Internal", "Com_UniqueNum");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		type = GetControllerAt(1);
		group_Counter = (GGroup)GetChildAt(5);
		txt_Counter = (GTextField)GetChildAt(6);
	}
}
