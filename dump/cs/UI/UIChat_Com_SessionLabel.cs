using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using UnityEngine;

namespace UI;

public class UIChat_Com_SessionLabel : GComponent
{
	private string _nick;

	private string _remark;

	public Controller isChoosed;

	public Controller redStatus;

	public Controller status;

	public GLoader loader_Player;

	public GTextField txt_Name;

	public GTextField txt_Online;

	public GTextField txt_Offline;

	public GGraph btn_Select;

	public GGraph btn_Operate;

	public Transition close;

	public Transition open;

	public const string URL = "ui://y0luzhk8njjna";

	public void Refresh(SessionData sessionData)
	{
		(_nick, _remark) = sessionData.GetTargetDisplay();
		UpdateColor(selectStatus: false);
		RefreshStatus(sessionData);
	}

	public void RefreshStatus(SessionData sessionData)
	{
		loader_Player.url = sessionData.TargetHeadURL;
		redStatus.selectedIndex = (sessionData.isHaveUnReadMsg ? 1 : 0);
		status.selectedIndex = ((!sessionData.TargetOnline) ? 1 : 0);
	}

	public void UpdateColor(bool selectStatus)
	{
		string text = (selectStatus ? "#ffffff" : "#000000");
		string text2 = (selectStatus ? "#ffff66" : "#FC1494");
		if (!string.IsNullOrWhiteSpace(_remark))
		{
			txt_Name.text = "[color=" + text2 + "](" + _remark + ")[/color][color=" + text + "]" + _nick + "[/color]";
		}
		else
		{
			txt_Name.text = "[color=" + text + "]" + _nick + "[/color]";
		}
		string htmlString = (selectStatus ? "#ffffff" : "#1AC60A");
		string htmlString2 = (selectStatus ? "#ffffff" : "#FF0101");
		if (ColorUtility.TryParseHtmlString(htmlString, out var color))
		{
			txt_Online.color = color;
		}
		if (ColorUtility.TryParseHtmlString(htmlString2, out var color2))
		{
			txt_Offline.color = color2;
		}
	}

	public static UIChat_Com_SessionLabel CreateInstance()
	{
		return (UIChat_Com_SessionLabel)UIPackage.CreateObject("Chat", "Chat_Com_SessionLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isChoosed = GetControllerAt(0);
		redStatus = GetControllerAt(1);
		status = GetControllerAt(2);
		loader_Player = (GLoader)GetChildAt(4);
		txt_Name = (GTextField)GetChildAt(5);
		txt_Online = (GTextField)GetChildAt(6);
		txt_Offline = (GTextField)GetChildAt(7);
		btn_Select = (GGraph)GetChildAt(10);
		btn_Operate = (GGraph)GetChildAt(11);
		close = GetTransitionAt(0);
		open = GetTransitionAt(1);
	}
}
