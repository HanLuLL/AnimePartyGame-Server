using FairyGUI;
using FairyGUI.Utils;
using Tools;

namespace UI;

public class UIDisplayCard_Button_Way : GButton
{
	public Controller avaiable;

	public GTextField txt_Title;

	public GGroup group_Signal;

	public const string URL = "ui://893ze0z8okfee";

	public static UIDisplayCard_Button_Way CreateInstance()
	{
		return (UIDisplayCard_Button_Way)UIPackage.CreateObject("DisplayCard", "DisplayCard_Button_Way");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		avaiable = GetControllerAt(1);
		txt_Title = (GTextField)GetChildAt(0);
		group_Signal = (GGroup)GetChildAt(4);
	}

	public void Refresh(int way)
	{
		if (!StaticConfigure.Way.DataDict.TryGetValue(way, out var value))
		{
			if (StaticConfigure.Way.InfoDict.TryGetValue(0, out var value2))
			{
				txt_Title.SetVar("way", value2.WayID.GetLocal(UIStringType.Way)).FlushVars();
			}
			avaiable.selectedIndex = 1;
			return;
		}
		avaiable.selectedIndex = ((!SimpleSingletonProvider<UIManager>.inst.GoWayAvailable(way)) ? 1 : 0);
		if (StaticConfigure.Way.InfoDict.TryGetValue((int)value.WayType, out var value3))
		{
			txt_Title.SetVar("way", value3.WayID.GetLocal(UIStringType.Way)).FlushVars();
		}
		else
		{
			txt_Title.SetVar("way", 1.GetLocal(UIStringType.Way)).FlushVars();
		}
		group_Signal.x = txt_Title.width + 10f;
		base.onClick.Set((EventCallback0)delegate
		{
			if (avaiable.selectedIndex != 1)
			{
				base.onClick.Retain();
				SimpleSingletonProvider<UIManager>.inst.TryHideWindows();
				SimpleSingletonProvider<UIManager>.inst.GoWayPanel(way);
				base.onClick.Release();
			}
		});
	}
}
