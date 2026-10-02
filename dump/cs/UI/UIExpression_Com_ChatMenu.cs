using Core.Mark;
using Core.Unit;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UIExpression_Com_ChatMenu : GComponent
{
	public IMarkTarget MarkTarget;

	public Controller type;

	public Controller showTip;

	public GImage image_Exclamation;

	public GImage image_Praise;

	public GImage image_Move;

	public GImage image_HP;

	public GImage image_Card;

	public GImage image_Gold;

	public GImage image_Attack;

	public UIExpression_Com_Preview com_Preview;

	public UIExpression_Com_MenuTabFour com_TabFour;

	public UIExpression_Com_MenuTabThree com_TabThree;

	public const string URL = "ui://mp1ylwytvkgo13";

	public Vector2 GetMarkTargetPos()
	{
		if (MarkTarget == null)
		{
			return Vector2.zero;
		}
		return MarkTarget.GetPosition();
	}

	private void RefreshItem(UIExpression_Button_MenuItem btn_MapChat, GImage imageRight, NTexture texture)
	{
		btn_MapChat.loader_Icon.texture = texture;
		GImage image_Hover = btn_MapChat.image_Hover;
		GImage image_Frame = btn_MapChat.image_Frame;
		float num = (btn_MapChat.loader_Icon.rotation = 0f - btn_MapChat.rotation);
		float num3 = (image_Frame.rotation = num);
		image_Hover.rotation = num3;
		imageRight.visible = false;
	}

	public void ShowLandChat(IMarkTarget target, UnitLand land)
	{
		MarkTarget = target;
		showTip.selectedIndex = 0;
		type.selectedIndex = 0;
		RepeatedField<ChatMarkConfigure> marks = StaticConfigure.Chat.Marks;
		RefreshLandMenuItem(com_TabFour.btn_Top, com_TabFour.image_Top, image_Exclamation.texture, marks[0], land.Id);
		RefreshLandMenuItem(com_TabFour.btn_Bottom, com_TabFour.image_Bottom, image_Move.texture, marks[1], land.Id);
		RefreshLandMenuItem(com_TabFour.btn_Left, com_TabFour.image_Left, image_Attack.texture, marks[2], land.Id);
		RefreshLandMenuItem(com_TabFour.btn_Right, com_TabFour.image_Right, image_Praise.texture, marks[3], land.Id);
		base.touchable = true;
	}

	private void RefreshLandMenuItem(UIExpression_Button_MenuItem btn_MapChat, GImage image, NTexture texture, ChatMarkConfigure mark, int landId)
	{
		RefreshItem(btn_MapChat, image, texture);
		btn_MapChat.onClick.Set((EventCallback0)delegate
		{
			base.touchable = false;
			base.visible = false;
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (selfPlayerData != null)
			{
				BattleLandMessage msgData = new BattleLandMessage(selfPlayerData, mark.ID, landId);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestSendMessageC2S(msgData, mark.ID);
				base.touchable = true;
			}
		});
		btn_MapChat.onRollOver.Set((EventCallback0)delegate
		{
			com_Preview.txt_Tip.text = mark.ChatInfo;
			showTip.selectedIndex = 1;
			image.visible = true;
		});
		btn_MapChat.onRollOut.Set((EventCallback0)delegate
		{
			showTip.selectedIndex = 0;
			image.visible = false;
		});
	}

	public void ShowPlayerMenu(IMarkTarget target, long playerId)
	{
		MarkTarget = target;
		type.selectedIndex = 1;
		showTip.selectedIndex = 0;
		base.touchable = true;
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (playerDataById != null && selfPlayerData != null)
		{
			if (playerDataById.player.TeamId != selfPlayerData.player.TeamId)
			{
				RefreshPlayerMenuItem(com_TabThree.btn_Top, com_TabThree.image_Top, image_Exclamation.texture, 40012, playerId);
				RefreshPlayerMenuItem(com_TabThree.btn_Left, com_TabThree.image_Left, image_Attack.texture, 40013, playerId);
				RefreshPlayerMenuItem(com_TabThree.btn_Right, com_TabThree.image_Right, image_Praise.texture, 40014, playerId);
			}
			else if (playerDataById.player.Id != selfPlayerData.player.Id)
			{
				RefreshPlayerMenuItem(com_TabThree.btn_Top, com_TabThree.image_Top, image_HP.texture, 40008, playerId);
				RefreshPlayerMenuItem(com_TabThree.btn_Left, com_TabThree.image_Left, image_Gold.texture, 40009, playerId);
				RefreshPlayerMenuItem(com_TabThree.btn_Right, com_TabThree.image_Right, image_Card.texture, 40007, playerId);
			}
			else
			{
				RefreshPlayerMenuItem(com_TabThree.btn_Top, com_TabThree.image_Top, image_HP.texture, 40005, playerId);
				RefreshPlayerMenuItem(com_TabThree.btn_Left, com_TabThree.image_Left, image_Gold.texture, 40006, playerId);
				RefreshPlayerMenuItem(com_TabThree.btn_Right, com_TabThree.image_Right, image_Card.texture, 40017, playerId);
			}
		}
	}

	private void RefreshPlayerMenuItem(UIExpression_Button_MenuItem btn_MapChat, GImage image, NTexture texture, int chatId, long playerId)
	{
		RefreshItem(btn_MapChat, image, texture);
		btn_MapChat.onClick.Set((EventCallback0)delegate
		{
			base.touchable = false;
			base.visible = false;
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (selfPlayerData != null)
			{
				BattlePlayerMarkMessage msgData = new BattlePlayerMarkMessage(selfPlayerData, playerId, chatId);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestSendMessageC2S(msgData, chatId);
				base.touchable = true;
			}
		});
		btn_MapChat.onRollOver.Set((EventCallback0)delegate
		{
			com_Preview.txt_Tip.text = BattlePlayerMarkMessage.GetMsg(chatId, playerId);
			showTip.selectedIndex = 1;
			image.visible = true;
		});
		btn_MapChat.onRollOut.Set((EventCallback0)delegate
		{
			showTip.selectedIndex = 0;
			image.visible = false;
		});
	}

	public static UIExpression_Com_ChatMenu CreateInstance()
	{
		return (UIExpression_Com_ChatMenu)UIPackage.CreateObject("Expression", "Expression_Com_ChatMenu");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		showTip = GetControllerAt(1);
		image_Exclamation = (GImage)GetChildAt(0);
		image_Praise = (GImage)GetChildAt(1);
		image_Move = (GImage)GetChildAt(2);
		image_HP = (GImage)GetChildAt(3);
		image_Card = (GImage)GetChildAt(4);
		image_Gold = (GImage)GetChildAt(5);
		image_Attack = (GImage)GetChildAt(6);
		com_Preview = (UIExpression_Com_Preview)GetChildAt(12);
		com_TabFour = (UIExpression_Com_MenuTabFour)GetChildAt(13);
		com_TabThree = (UIExpression_Com_MenuTabThree)GetChildAt(14);
	}
}
