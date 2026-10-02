using System;
using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_AttrTip : GComponent
{
	public Controller fontType;

	public Controller iconType;

	public GTextField txt_OriginValue;

	public GTextField txt_CurValue;

	public GTextField txt_AttrValue;

	public Transition showAttr;

	public const string URL = "ui://1ov1i0v9drks1";

	public void InitAttrChange(int originValue, int curValue, int delta, int index, string PN = "")
	{
		base.visible = true;
		iconType.selectedIndex = index;
		fontType.selectedIndex = ((delta < 0) ? 2 : ((delta != 0) ? 1 : 0));
		if (index == 1 || index == 2)
		{
			txt_OriginValue.text = originValue.ToString();
			txt_CurValue.text = curValue.ToString();
		}
		else
		{
			txt_AttrValue.text = ((PN == "") ? delta.ToString("+#;-#;0") : (PN + delta));
		}
	}

	public void ShowAttrChange(Action onComplete)
	{
		showAttr.Play(delegate
		{
			base.visible = false;
			onComplete?.Invoke();
		});
	}

	public override void Dispose()
	{
		showAttr.Dispose();
		base.Dispose();
	}

	public static UICom_AttrTip CreateInstance()
	{
		return (UICom_AttrTip)UIPackage.CreateObject("Common_Internal", "Com_AttrTip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		fontType = GetControllerAt(0);
		iconType = GetControllerAt(1);
		txt_OriginValue = (GTextField)GetChildAt(0);
		txt_CurValue = (GTextField)GetChildAt(1);
		txt_AttrValue = (GTextField)GetChildAt(6);
		showAttr = GetTransitionAt(0);
	}
}
