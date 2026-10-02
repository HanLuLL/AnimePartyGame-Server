using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using party.model;
using party.protocol;

namespace UI;

public class ChooseRoundCardWindow : BaseWindow
{
	private readonly List<UICard_Com_RoundCard> _roundCardItems = new List<UICard_Com_RoundCard>();

	private RepeatedField<int> _roundCardIds;

	private int _chooseCardIndex = -1;

	private long _sn;

	public ChooseRoundCardWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIChooseRoundCardWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIChooseRoundCardWindow uIChooseRoundCardWindow)
		{
			uIChooseRoundCardWindow.btn_ChooseCard.onClick.Add(ChooseRoundCard);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIChooseRoundCardWindow uIChooseRoundCardWindow)
		{
			uIChooseRoundCardWindow.btn_ChooseCard.onClick.Remove(ChooseRoundCard);
			_sn = 0L;
		}
	}

	public async UniTask ShowRoundCard(Action action)
	{
		SelectRewardCardC2S selectRewardCard = ByteBuf.ReadObject<SelectRewardCardC2S>(action.Data.ToByteArray());
		if (selectRewardCard?.CardIds == null || selectRewardCard.CardIds.Count == 0)
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			if (_sn != action.Sn)
			{
				await TryShowAsync();
				_sn = action.Sn;
				_roundCardIds = selectRewardCard.CardIds;
				if (base.contentPane is UIChooseRoundCardWindow uIChooseRoundCardWindow)
				{
					_chooseCardIndex = -1;
					_roundCardItems.Clear();
					uIChooseRoundCardWindow.list_card.itemRenderer = RendererRoundCard;
					uIChooseRoundCardWindow.list_card.numItems = selectRewardCard.CardIds.Count;
					uIChooseRoundCardWindow.btn_ChooseCard.onClick.Release();
					OperationTimer.ActionDownTime(action.Sn, 5377, AutoChooseRoundCard, null, null, operateCard: false, showTimerToPlayer: false);
				}
			}
		}
		else
		{
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowMultiplePlayerThink(action.PlayerId, 11032);
		}
	}

	private void ChooseRoundCard()
	{
		if (_sn != 0L && base.contentPane is UIChooseRoundCardWindow uIChooseRoundCardWindow)
		{
			if (_chooseCardIndex < 0)
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10014);
				return;
			}
			uIChooseRoundCardWindow.btn_ChooseCard.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.card.RequestRoundCard(_sn, _roundCardIds, _chooseCardIndex);
		}
	}

	private void AutoChooseRoundCard()
	{
		if (_chooseCardIndex < 0)
		{
			_chooseCardIndex = 0;
		}
		ChooseRoundCard();
	}

	private void RendererRoundCard(int index, GObject item)
	{
		if (item is UICard_Com_RoundCard uICard_Com_RoundCard)
		{
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			CardInfoConfigure cardConfigure = _roundCardIds[index].GetCardConfigure();
			uICard_Com_RoundCard.data = index;
			if (SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue(cardConfigure.Id, out var value))
			{
				string content = value.CardDescription(selfPlayerData.player.Id, replaceDamage: true);
				string local = cardConfigure.NameID.GetLocal(UIStringType.Card);
				string cardIndex = ((cardConfigure.CardNumb == 0) ? "" : cardConfigure.CardNumb.GetLocal(UIStringType.Card));
				string cardTips = ((cardConfigure.CommentId == 0) ? "" : cardConfigure.CommentId.GetLocal(UIStringType.Card));
				int costValue = value.GetCostValue(selfPlayerData.player.Id);
				CommonUIManager.RendererCardInRoom(selfPlayerData.player.Id, uICard_Com_RoundCard.com_card as UICom_Card, cardConfigure.GetBattlePlayerCardView(selfPlayerData.player.Id), local, content, cardIndex, cardTips, costValue, cardConfigure.CardType, cardConfigure.CardTargetType);
			}
			if (StaticConfigure.Effect.InfoDict.TryGetValue(8000701, out var value2))
			{
				SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value2.EffectName, uICard_Com_RoundCard.downEffect, 100f).Forget();
			}
			uICard_Com_RoundCard.downEffect.visible = false;
			_roundCardItems.Add(uICard_Com_RoundCard);
			uICard_Com_RoundCard.onClick.Set(OnClickRoundCardItem);
		}
	}

	private void OnClickRoundCardItem(EventContext context)
	{
		if (context.sender is UICard_Com_RoundCard uICard_Com_RoundCard)
		{
			_chooseCardIndex = (int)uICard_Com_RoundCard.data;
			RefreshRoundCards();
		}
	}

	private void RefreshRoundCards()
	{
		for (int i = 0; i < _roundCardItems.Count; i++)
		{
			UICard_Com_RoundCard uICard_Com_RoundCard = _roundCardItems[i];
			int num = (int)uICard_Com_RoundCard.data;
			uICard_Com_RoundCard.downEffect.visible = num == _chooseCardIndex;
		}
	}
}
