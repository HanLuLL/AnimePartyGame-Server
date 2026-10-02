using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace UI;

public class UICom_ModifyCounter : GComponent
{
	public Controller status;

	public GTextField txt_Counter;

	public GGroup group_Counter;

	public Transition Shake;

	public const string URL = "ui://1ov1i0v9qle6c1";

	public void Refresh(int count)
	{
		if (count == 1)
		{
			txt_Counter.color = new Color(0.8f, 0f, 0.05f, 1f);
			Shake.Play(-1, 0f, null);
		}
		else
		{
			txt_Counter.color = new Color(0.44f, 0.66f, 0f, 1f);
			Shake.Stop();
		}
		txt_Counter.text = count.ToString();
		group_Counter.visible = count > 0;
		status.selectedIndex = Mathf.Clamp(count - 1, 0, 2);
	}

	public static UICom_ModifyCounter CreateInstance()
	{
		return (UICom_ModifyCounter)UIPackage.CreateObject("Common_Internal", "Com_ModifyCounter");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		txt_Counter = (GTextField)GetChildAt(3);
		group_Counter = (GGroup)GetChildAt(4);
		Shake = GetTransitionAt(0);
	}
}
