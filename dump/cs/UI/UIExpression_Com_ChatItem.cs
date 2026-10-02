using System;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using UnityEngine;
using UnityTimer;

namespace UI;

public class UIExpression_Com_ChatItem : GComponent
{
	public Timer timer;

	public Controller backgroundType;

	public GLoader loader_Player;

	public GTextField title;

	public GButton btn_ShowSignal;

	public const string URL = "ui://bdqipkfggbr1l";

	public void RefreshInfo(BattleMessage msg)
	{
		title.text = msg.shortInfo;
		loader_Player.url = msg.Sender.player.standingPainting.ProfilePhoto;
		base.visible = true;
		Timer obj = timer;
		if (obj != null)
		{
			obj.Cancel();
		}
		timer = Timer.Register(0f, (float)StaticGlobalData.GAME_CHARACTER_EXPRESSION_DURATION * 0.001f, (Action)delegate
		{
			base.visible = false;
			loader_Player.texture = null;
		}, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, (Action)null, false, -1f, false, (GameObject)null);
		btn_ShowSignal.visible = msg.ShowAddress;
		btn_ShowSignal.onClick.Set((EventCallback0)delegate
		{
			btn_ShowSignal.onClick.Retain();
			msg.ShowAddressInfo();
			btn_ShowSignal.onClick.Release();
		});
		Controller controller = backgroundType;
		int selectedIndex = ((msg.MsgType == MessageType.REFEREE) ? 1 : 0);
		controller.selectedIndex = selectedIndex;
	}

	public void Close()
	{
		loader_Player.texture = null;
		Timer obj = timer;
		if (obj != null)
		{
			obj.Cancel();
		}
		timer = null;
	}

	public static UIExpression_Com_ChatItem CreateInstance()
	{
		return (UIExpression_Com_ChatItem)UIPackage.CreateObject("ExpressionList", "Expression_Com_ChatItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		backgroundType = GetControllerAt(0);
		loader_Player = (GLoader)GetChildAt(0);
		title = (GTextField)GetChildAt(3);
		btn_ShowSignal = (GButton)GetChildAt(4);
	}
}
