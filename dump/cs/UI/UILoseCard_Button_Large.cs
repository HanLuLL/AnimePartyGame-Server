using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UILoseCard_Button_Large : GButton
{
	public Controller selectedStatus;

	public GComponent com_Card;

	public const string URL = "ui://vxuz3wboorg71";

	public int index;

	public HandCardData handCardData;

	public static UILoseCard_Button_Large CreateInstance()
	{
		return (UILoseCard_Button_Large)UIPackage.CreateObject("LoseCard", "LoseCard_Button_Large");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		selectedStatus = GetControllerAt(1);
		com_Card = (GComponent)GetChildAt(0);
	}

	public void InitDate(int _index, HandCardData _handCardData)
	{
		index = _index;
		handCardData = _handCardData;
		CardInfoConfigure config = _handCardData.Config;
		selectedStatus.selectedIndex = 0;
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue(config.Id, out var value))
		{
			string content = value.CardDescription(selfPlayerData.player.Id, _handCardData);
			string local = config.NameID.GetLocal(UIStringType.Card);
			string cardIndex = ((config.CardNumb == 0) ? "" : config.CardNumb.GetLocal(UIStringType.Card));
			string cardTips = ((config.CommentId == 0) ? "" : config.CommentId.GetLocal(UIStringType.Card));
			int costValue = value.GetCostValue(selfPlayerData.player.Id, _handCardData);
			CommonUIManager.RendererCardInRoom(selfPlayerData.player.Id, com_Card as UICom_Card, config.GetBattlePlayerCardView(selfPlayerData.player.Id), local, content, cardIndex, cardTips, costValue, config.CardType, config.CardTargetType);
		}
		else
		{
			Debug.LogError($"cardActions TryGet Action is Failure by CardIs:{config.Id}");
		}
	}
}
