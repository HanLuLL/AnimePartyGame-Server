using System;
using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChat_Com_SessionOperatePopup : GComponent
{
	public GButton btn_Delete;

	public const string URL = "ui://y0luzhk8sgc41d";

	public void Refresh(Action deleteEvent)
	{
		btn_Delete.onClick.Set((EventCallback0)delegate
		{
			deleteEvent?.Invoke();
		});
	}

	public static UIChat_Com_SessionOperatePopup CreateInstance()
	{
		return (UIChat_Com_SessionOperatePopup)UIPackage.CreateObject("Chat", "Chat_Com_SessionOperatePopup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Delete = (GButton)GetChildAt(1);
	}
}
