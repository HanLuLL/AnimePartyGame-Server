using System;
using System.Collections.Generic;
using Core;
using Core.Mark;
using Core.Tutorial;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using GameLogic.Replay;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class HandCardPanel : BasePanel<UIHandCardPanel>
{
	private BattlePlayerData _SelfPlayer;

	private readonly List<UIHandCard_Button_Card> cardItemList = new List<UIHandCard_Button_Card>();

	private readonly List<UIHandCard_Button_Card> removeCardItemList = new List<UIHandCard_Button_Card>();

	private UICom_SuggestMove _comSuggestMove;

	private List<UniTask> _cardSwitchEffects = new List<UniTask>();

	private float CenterRadius => 918f;

	private Vector2 centerPoint => new Vector2(base.ui.container_Card.width * 0.5f, base.ui.container_Card.height + 810f);

	public HandCardPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIHandCardPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		_SelfPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		base.Show(objs);
		ReadyFight(hide: false, UIPanelType.None);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.launchHotZone.selectedIndex = 0;
		RefreshCardInfo();
	}

	public override void Refresh()
	{
		base.Refresh();
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapType == 10)
		{
			base.ui.com_Skill.btn_Skill.visible = false;
		}
		UpdateSelfCard(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID());
		ReplaySession replaySession = SimpleSingletonProvider<GameLogicManager>.inst?.replay?.Session;
		if (replaySession != null && replaySession.IsReplay)
		{
			base.ui.btn_Move.visible = false;
		}
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.com_Skill.btn_Skill.onClick.Add(ReleaseActiveSkill);
		base.ui.btn_Move.onClick.Add(OperatorDice);
		Stage.inst.onTouchBegin.Add(ZoomCard);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.com_Skill.btn_Skill.onClick.Remove(ReleaseActiveSkill);
		base.ui.btn_Move.onClick.Remove(OperatorDice);
		Stage.inst.onTouchBegin.Remove(ZoomCard);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetCardList.AddListener(ResetCardList);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.recycleCard.AddListener(RecycleCard);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.cancelUse.AddListener(OnCancelUseEffectC2S);
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.AddListener(ReadyFight);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roundChange.AddListener(RefreshRound);
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.dealThrowDice.AddListener(DealThrowDiceUI);
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.dealEffectCard.AddListener(DealUseEffectCard);
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.dealQuickCard.AddListener(DealQuickCard);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.LookCard.AddListener(LookCardByZoom);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.ZoomOutCard.AddListener(ZoomOutCard);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.deadDeal.AddListener(PlayerDead);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetSelfCardList.AddListener(UpdateSelfCard);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.MarkSignal.MarkEnter.AddListener(OnMarkEnter);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.MarkSignal.MarkExit.AddListener(OnMarkExit);
		SimpleSingletonProvider<GameLogicManager>.inst.watch.SubscribePlayerId.AddListener(RefreshSubscribePlayerUI, excuteImmediately: false);
		AddSelfPlayerListener();
	}

	private void AddSelfPlayerListener()
	{
		if (_SelfPlayer != null)
		{
			_SelfPlayer.Property.CardMaxVailUseCount.AddListener(RefreshCardInfo);
			_SelfPlayer.Property.activeSkillCD.AddListener(RefreshSkillCD);
			_SelfPlayer.Property.cardUseTimes.AddListener(RefreshCardInfo);
		}
		else
		{
			Debug.LogError("对手牌数和技能CD的监听事件注册失败");
		}
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetCardList.RemoveListener(ResetCardList);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.recycleCard.RemoveListener(RecycleCard);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.cancelUse.RemoveListener(OnCancelUseEffectC2S);
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.RemoveListener(ReadyFight);
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.dealThrowDice.RemoveListener(DealThrowDiceUI);
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.dealEffectCard.RemoveListener(DealUseEffectCard);
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.dealQuickCard.RemoveListener(DealQuickCard);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roundChange.RemoveListener(RefreshRound);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.LookCard.RemoveListener(LookCardByZoom);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.ZoomOutCard.RemoveListener(ZoomOutCard);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.deadDeal.RemoveListener(PlayerDead);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetSelfCardList.RemoveListener(UpdateSelfCard);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.MarkSignal.MarkEnter.RemoveListener(OnMarkEnter);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.MarkSignal.MarkExit.RemoveListener(OnMarkExit);
		SimpleSingletonProvider<GameLogicManager>.inst.watch.SubscribePlayerId.RemoveListener(RefreshSubscribePlayerUI);
		RemoveSelfPlayerListener();
	}

	private void RemoveSelfPlayerListener()
	{
		if (_SelfPlayer != null)
		{
			_SelfPlayer.Property.CardMaxVailUseCount.RemoveListener(RefreshCardInfo);
			_SelfPlayer.Property.activeSkillCD.RemoveListener(RefreshSkillCD);
			_SelfPlayer.Property.cardUseTimes.RemoveListener(RefreshCardInfo);
		}
		else
		{
			Debug.LogError("对手牌数和技能CD的监听事件移除失败");
		}
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void ReadyFight(bool hide, UIPanelType panelType)
	{
		if (panelType == UIPanelType.None || panelType == UIPanelType.HandCard)
		{
			base.ui.container_Card.touchable = !hide;
			int num = ((!hide) ? 1 : 0);
			base.ui.SetScale(num, num);
		}
	}

	private void PlayerDead(long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			base.ui.btn_Move.grayed = true;
			base.ui.btn_Move.touchable = false;
			base.ui.com_Skill.btn_Skill.grayed = true;
			base.ui.com_Skill.btn_Skill.touchable = false;
			RefreshUsableCards(_isSelf: false);
		}
	}

	private void OnMarkEnter()
	{
		base.ui.com_Skill.OnMarkTipShow();
	}

	private void OnMarkExit()
	{
		base.ui.com_Skill.OnMarkTipHide();
	}

	private void RefreshRound(int range)
	{
		RefreshSkillCDByRound(range);
		RefreshCardInfo();
	}

	private void UpdateSelfCard(long playerId)
	{
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData != null && selfPlayerData.player.Id == playerId && (!SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher() || SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.IsPVE()))
		{
			UpdateCardList();
		}
	}

	private void RecycleCard()
	{
		CardPool.ReleaseAllCard(cardItemList);
	}

	private void ResetCardList()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher() || SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.IsPVE())
		{
			ResetCardList(isSelf: true);
		}
	}

	private void ResetCardList(bool isSelf)
	{
		base.ui.com_Skill.btn_Skill.onClick.Release();
		base.ui.btn_Move.onClick.Release();
		UpdateCardList();
		RefreshUsableCards(isSelf);
		if (_comSuggestMove != null && SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial)
		{
			TutorialPlayerActionFSM system = TutorialGame.GetSystem<TutorialPlayerActionFSM>();
			if (system != null && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(system.PlayerId) && system.CurrentState == PlayerActionType.Idle)
			{
				_comSuggestMove.visible = true;
			}
		}
	}

	private void UpdateCardList()
	{
		base.ui.container_Card.touchable = false;
		List<HandCardData> handCards = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().cardContainer._HandCards;
		List<UIHandCard_Button_Card> list = new List<UIHandCard_Button_Card>();
		for (int i = 0; i < handCards.Count; i++)
		{
			UIHandCard_Button_Card uIHandCard_Button_Card = GetCardItem(handCards[i]) ?? CardPool.Get(base.ui.container_Card);
			uIHandCard_Button_Card.sortingOrder = i;
			list.Add(uIHandCard_Button_Card);
			CardPool.SetDragEvent(uIHandCard_Button_Card, DragStartEvent, DragEndEvent, DragMoveEvent);
			uIHandCard_Button_Card.InitData_EffectCard(i, handCards[i]);
			uIHandCard_Button_Card.UpdateCustomRotation(CardHelper.CalculateRotation(i, handCards.Count));
			uIHandCard_Button_Card.UpdateCustomPosition(CardHelper.CalculatePosition(centerPoint, CenterRadius, uIHandCard_Button_Card.CustomRotation));
			uIHandCard_Button_Card.PlayTransition(base.ui.width, base.ui.height, uIHandCard_Button_Card.IsDistribute());
		}
		CardPool.ReleaseAllCard(cardItemList);
		CardPool.ReleaseAllCard(removeCardItemList);
		cardItemList.AddRange(list);
		base.ui.container_Card.touchable = true;
	}

	private UIHandCard_Button_Card GetCardItem(HandCardData showCard)
	{
		if (removeCardItemList.Count > 0)
		{
			int num = removeCardItemList.FindIndex((UIHandCard_Button_Card item) => item.CardData.Guid == showCard.Guid);
			if (num >= 0)
			{
				UIHandCard_Button_Card uIHandCard_Button_Card = removeCardItemList[num];
				uIHandCard_Button_Card.com_Card.visible = true;
				removeCardItemList.RemoveAt(num);
				return uIHandCard_Button_Card;
			}
		}
		if (cardItemList.Count > 0)
		{
			int num2 = cardItemList.FindIndex((UIHandCard_Button_Card item) => item.CardData.Guid == showCard.Guid);
			if (num2 >= 0)
			{
				UIHandCard_Button_Card uIHandCard_Button_Card2 = cardItemList[num2];
				uIHandCard_Button_Card2.com_Card.visible = true;
				cardItemList.RemoveAt(num2);
				return uIHandCard_Button_Card2;
			}
		}
		return null;
	}

	private void RefreshUsableCards(bool _isSelf)
	{
		base.ui.container_Card.touchable = true;
		RepeatedField<int> usableCards = SimpleSingletonProvider<GameLogicManager>.inst.action.UsableCards;
		long cardSN = SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN;
		int num = -1;
		if (_isSelf && GameSettings.CardSuggest)
		{
			int num2 = ((_SelfPlayer != null) ? _SelfPlayer.Property.cardUseTimes.Value : 0);
			if (((_SelfPlayer == null) ? 1 : _SelfPlayer.Property.CardMaxVailUseCount.Value) - num2 > 0 && cardItemList.Count > 0)
			{
				num = SimpleSingletonProvider<GameLogicManager>.inst.action.TryGetCardSuggestCardId(usableCards);
			}
		}
		for (int i = 0; i < cardItemList.Count; i++)
		{
			UIHandCard_Button_Card uIHandCard_Button_Card = cardItemList[i];
			bool enableUse = _isSelf && usableCards != null && usableCards.Contains(uIHandCard_Button_Card._config.Id) && cardSN != 0;
			if (num == uIHandCard_Button_Card._config.Id)
			{
				num = -1;
				cardItemList[i].ShowSuggestCard(isSuggest: true);
			}
			else
			{
				cardItemList[i].ShowSuggestCard(isSuggest: false);
			}
			uIHandCard_Button_Card.UpdateUsableState(enableUse).Forget();
		}
	}

	private void LookCardByZoom(bool isZoom)
	{
		CardPool.ChangeCardZoomStatus(cardItemList, isZoom);
	}

	private void ZoomOutCard()
	{
		CardPool.ZoomOutCard(cardItemList);
	}

	private void ZoomCard(EventContext context)
	{
		if (GRoot.inst.touchTarget == null)
		{
			LookCardByZoom(isZoom: false);
		}
	}

	private void DragStartEvent(EventContext context)
	{
		base.ui.launchHotZone.selectedIndex = 1;
		if (context.sender is UIHandCard_Button_Card uIHandCard_Button_Card)
		{
			uIHandCard_Button_Card.DisplayCard();
		}
	}

	private async void DragEndEvent(EventContext context)
	{
		base.ui.launchHotZone.selectedIndex = 0;
		EventDispatcher sender = context.sender;
		if (!(sender is UIHandCard_Button_Card item))
		{
			return;
		}
		item.CardScope(state: false);
		Vector2 vector = GRoot.inst.GlobalToLocal(base.ui.hotZone.LocalToGlobal(Vector2.zero));
		Vector2 vector2 = GRoot.inst.GlobalToLocal(item.LocalToGlobal(Vector2.zero));
		if (vector2.x > vector.x && vector2.y > vector.y && vector2.x < vector.x + base.ui.hotZone.actualWidth && vector2.y < vector.y + base.ui.hotZone.actualHeight && SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN != 0L)
		{
			ForbidOperate();
			item.effectOutline.displayObject.gameObject.SetActive(value: true);
			if (StaticConfigure.Effect.InfoDict.TryGetValue(23, out var value))
			{
				await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, item.effectOutline, 80f);
			}
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(100);
			item.com_Card.visible = false;
			item.ShowUseCardWin(SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(400);
			cardItemList.Remove(item);
			removeCardItemList.Add(item);
			LookCardByZoom(isZoom: false);
		}
		else
		{
			UpdateCardList();
			RefreshUsableCards(_isSelf: true);
		}
	}

	private void ForbidOperate()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.SetCancelBtn.Dispatch(t: false);
		base.ui.com_Skill.btn_Skill.onClick.Retain();
		base.ui.btn_Move.onClick.Retain();
		RefreshUsableCards(_isSelf: false);
		base.ui.container_Card.touchable = false;
		if (_comSuggestMove != null)
		{
			_comSuggestMove.visible = false;
		}
	}

	private void DragMoveEvent(EventContext context)
	{
	}

	private void RefreshSkillCDByRound(int round)
	{
		if (_SelfPlayer != null)
		{
			RefreshSkillCD(_SelfPlayer.Property.activeSkillCD.Value);
		}
		else
		{
			Debug.LogError("无法获取自己的角色，更新CD失败");
		}
	}

	private void RefreshSkillCD(int cd)
	{
		base.ui.com_Skill.txt_CD.text = Mathf.Max(0, cd).ToString();
		base.ui.com_Skill.showCD.selectedIndex = ((cd != 0) ? 1 : 0);
	}

	private void ReleaseActiveSkill()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN != 0L && !MarkInputConsume.IsConsumed())
		{
			ForbidOperate();
			RefreshUsableCards(_isSelf: false);
			if (_SelfPlayer?.CharacterInst != null && _SelfPlayer.CharacterInst.skill != null)
			{
				_SelfPlayer.CharacterInst.skill.SkillReleaseAction(SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN);
			}
			else
			{
				Debug.LogError("无法释放技能, 数据可能被销毁");
			}
		}
	}

	private void DealThrowDiceUI(long _actionPlayerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_actionPlayerId))
		{
			base.ui.btn_Move.grayed = false;
			base.ui.btn_Move.touchable = true;
			base.ui.btn_Move.onClick.Release();
			OperationTimer.ActionDownTime(SimpleSingletonProvider<GameLogicManager>.inst.action.throwDiceSn, 5021, OnCompleteThrowDiceC2S, OnCancelThrowDiceC2S);
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_actionPlayerId, 11001);
			base.ui.btn_Move.grayed = true;
			base.ui.btn_Move.touchable = false;
			base.ui.com_Skill.btn_Skill.grayed = true;
			base.ui.com_Skill.btn_Skill.touchable = false;
		}
	}

	private void OnCompleteThrowDiceC2S()
	{
		if (base.ui.btn_Move.touchable && SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction == PlayerActionEnum.MOVE)
		{
			base.ui.btn_Move.touchable = false;
			base.ui.btn_Move.grayed = true;
			base.ui.com_Skill.btn_Skill.touchable = false;
			base.ui.com_Skill.btn_Skill.grayed = true;
			SimpleSingletonProvider<GameLogicManager>.inst.battle.RequestThrowDiceC2S(SimpleSingletonProvider<GameLogicManager>.inst.action.throwDiceSn).OnFinishedOnly.AddOnce(OnCancelThrowDiceC2S);
		}
	}

	private void OnCancelThrowDiceC2S()
	{
		ActionLogic action = SimpleSingletonProvider<GameLogicManager>.inst.action;
		if (action != null)
		{
			action.throwDiceSn = 0L;
			action.playerAction = PlayerActionEnum.NONE;
			action.UsableCards = null;
		}
		else
		{
			Debug.LogError("GameLogicManager.inst.action is null");
		}
		if (base.ui != null)
		{
			base.ui.btn_Move.onClick.Release();
			RefreshUsableCards(_isSelf: true);
		}
		else
		{
			Debug.LogError("HandCardPanel.ui is null");
		}
	}

	private void OperatorDice()
	{
		PlayerActionEnum playerAction = SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction;
		if (playerAction == PlayerActionEnum.MOVE || playerAction == PlayerActionEnum.CARD)
		{
			base.ui.com_Skill.btn_Skill.touchable = false;
			base.ui.com_Skill.btn_Skill.grayed = true;
			base.ui.btn_Move.touchable = false;
			base.ui.btn_Move.grayed = true;
		}
		switch (SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction)
		{
		case PlayerActionEnum.MOVE:
			if (SimpleSingletonProvider<GameLogicManager>.inst.action.throwDiceSn != 0L)
			{
				base.ui.btn_Move.onClick.Retain();
				if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
				{
					OnCancelThrowDiceC2S();
					SimpleSingletonProvider<UIManager>.inst.guide.HideGuideMask();
					SimpleSingletonProvider<UIManager>.inst.guide.HideDialog();
					SimpleSingletonProvider<GameLogicManager>.inst.guide.GuidanceThrowDice(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID(), SimpleSingletonProvider<GameLogicManager>.inst.guide.playerMovePoint);
				}
				else if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 10)
				{
					OnCancelThrowDiceC2S();
					SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestThrowDiceC2S(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID());
				}
				else
				{
					SimpleSingletonProvider<GameLogicManager>.inst.battle.RequestThrowDiceC2S(SimpleSingletonProvider<GameLogicManager>.inst.action.throwDiceSn).OnFinishedOnly.AddOnce(OnCancelThrowDiceC2S);
				}
			}
			break;
		case PlayerActionEnum.CARD:
			if (SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN != 0L)
			{
				base.ui.btn_Move.onClick.Retain();
				if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
				{
					OnCancelThrowDiceC2S();
					SimpleSingletonProvider<GameLogicManager>.inst.guide.GuidanceThrowDice(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID(), SimpleSingletonProvider<GameLogicManager>.inst.guide.playerMovePoint);
				}
				else if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 10)
				{
					OnCancelThrowDiceC2S();
					SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestThrowDiceC2S(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID());
				}
				else
				{
					SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseEffectCardC2S(SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN, 0).OnFinishedOnly.AddOnce(OnCancelThrowDiceC2S);
				}
			}
			break;
		}
	}

	private void DealUseEffectCard(long _actionPlayerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_actionPlayerId))
		{
			RefreshUsableCards(_isSelf: true);
			base.ui.btn_Move.touchable = !SimpleSingletonProvider<GameLogicManager>.inst.action.NotMove;
			base.ui.btn_Move.grayed = SimpleSingletonProvider<GameLogicManager>.inst.action.NotMove;
			base.ui.btn_Move.onClick.Release();
			base.ui.com_Skill.btn_Skill.onClick.Release();
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_actionPlayerId);
			bool flag = false;
			if (playerDataById?.CharacterInst != null && playerDataById?.CharacterInst.skill != null)
			{
				flag = playerDataById.CharacterInst.skill.SkillUsable(_actionPlayerId);
			}
			base.ui.com_Skill.btn_Skill.touchable = flag;
			base.ui.com_Skill.btn_Skill.grayed = !flag;
			RefreshCardInfo();
			OperationTimer.ActionDownTime(SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN, 5055, OnCompleteUseEffectC2S, OnCancelUseEffectC2S, null, operateCard: true);
		}
		else
		{
			RefreshUsableCards(_isSelf: false);
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_actionPlayerId, 11001);
			base.ui.btn_Move.grayed = true;
			base.ui.btn_Move.touchable = false;
			base.ui.com_Skill.btn_Skill.touchable = false;
			base.ui.com_Skill.btn_Skill.grayed = true;
		}
	}

	private void OnCompleteUseEffectC2S()
	{
		base.ui.btn_Move.touchable = false;
		base.ui.btn_Move.grayed = true;
		base.ui.com_Skill.btn_Skill.touchable = false;
		base.ui.com_Skill.btn_Skill.grayed = true;
		base.ui.launchHotZone.selectedIndex = 0;
		if (SimpleSingletonProvider<UIManager>.inst.cardWindow.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.cardWindow.OnCloseWin();
		}
		if (SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.Hide();
		}
		SimpleSingletonProvider<GameLogicManager>.inst.action.UsableCards = null;
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.closeSelectPlayer.Dispatch();
		SimpleSingletonProvider<UIManager>.inst.cardWindow.CancelSelectLand();
		SimpleSingletonProvider<UIManager>.inst.loseCard.Hide();
		SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseEffectCardC2S(SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN, 0);
	}

	private void OnCancelUseEffectC2S()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN = 0L;
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.NONE;
		base.ui.launchHotZone.selectedIndex = 0;
		base.ui.btn_Move.onClick.Release();
		base.ui.com_Skill.btn_Skill.onClick.Release();
		base.ui.com_Skill.btn_Skill.touchable = false;
		base.ui.com_Skill.btn_Skill.grayed = true;
	}

	private void DealQuickCard(long _actionPlayerId, int preCardId)
	{
		base.ui.btn_Move.touchable = false;
		base.ui.btn_Move.grayed = true;
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_actionPlayerId))
		{
			RefreshUsableCards(_isSelf: true);
			OperationTimer.ActionDownTime(SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN, 5073, OnCompleteUseQuickC2S, OnCancelUseEffectC2S);
		}
		else
		{
			RefreshUsableCards(_isSelf: false);
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_actionPlayerId, 11001);
		}
	}

	private void OnCompleteUseQuickC2S()
	{
		if (SimpleSingletonProvider<UIManager>.inst.cardWindow.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.cardWindow.OnCloseWin();
		}
		if (SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.Hide();
		}
		SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseQuickCardC2S(SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN, 0, 0L);
	}

	private void RefreshCardInfo(int count = 0)
	{
		int num = ((_SelfPlayer != null) ? _SelfPlayer.Property.cardUseTimes.Value : 0);
		int num2 = ((_SelfPlayer == null) ? 1 : _SelfPlayer.Property.CardMaxVailUseCount.Value);
		base.ui.txt_CardState.SetVar("curCard", num.ToString()).SetVar("maxCard", num2.ToString()).FlushVars();
	}

	public void GuideTriggerSkill()
	{
		Vector2 pos = base.ui.TransformPoint(base.ui.com_Skill.xy, GRoot.inst);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(pos, base.ui.com_Skill.width, base.ui.com_Skill.height + base.ui.txt_CardState.height, _needTransparentMask: false, isRect: true).Forget();
	}

	public void GuideTriggerMove()
	{
		Vector2 pos = base.ui.TransformPoint(base.ui.btn_Move.xy, GRoot.inst);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(pos, base.ui.btn_Move.width, base.ui.btn_Move.height, _needTransparentMask: false, isRect: false).Forget();
	}

	public void GuideUseCard()
	{
		base.ui.btn_Move.touchable = false;
		base.ui.com_Skill.btn_Skill.touchable = false;
		RefreshUsableCards(_isSelf: true);
	}

	public void ShowReleaseSkill()
	{
		Vector2 pt = base.ui.com_Skill.btn_Skill.LocalToGlobal(Vector2.zero);
		Vector2 pos = GRoot.inst.GlobalToLocal(pt);
		SimpleSingletonProvider<UIManager>.inst.tutorial.ShowGuideMask(pos, base.ui.com_Skill.btn_Skill.width, base.ui.com_Skill.btn_Skill.height, isRect: true);
	}

	public void ChangeSuggestMove(bool suggest)
	{
		if (_comSuggestMove == null)
		{
			_comSuggestMove = UICom_SuggestMove.CreateInstance();
			_comSuggestMove.txt_Tutorial.text = 2.GetLocal(UIStringType.Tutorial);
			base.ui.AddChild(_comSuggestMove);
			_comSuggestMove.x = base.ui.btn_Move.x + (base.ui.btn_Move.width - _comSuggestMove.width) * 0.5f;
			_comSuggestMove.y = base.ui.btn_Move.y - _comSuggestMove.height;
			_comSuggestMove.AddRelation(base.ui, FairyGUI.RelationType.Bottom_Bottom);
			_comSuggestMove.AddRelation(base.ui, FairyGUI.RelationType.Right_Right);
		}
		_comSuggestMove.visible = suggest;
	}

	public async UniTask OnCardConvertChanged(HashSet<int> handCardIds)
	{
		try
		{
			_cardSwitchEffects.Clear();
			foreach (UIHandCard_Button_Card cardItem in cardItemList)
			{
				if (handCardIds.Contains(cardItem.CardData.Guid))
				{
					int cardId = cardItem.CardData.CardId;
					if (cardId == 21022 || cardId == 21020)
					{
						_cardSwitchEffects.Add(cardItem.PlayCardSwitchEffect());
					}
				}
			}
			await UniTask.WhenAll(_cardSwitchEffects);
			UpdateCardList();
		}
		catch (Exception arg)
		{
			Debug.LogError($"#卡牌ID变更# 卡牌切换效果异常 {arg}");
		}
	}

	private void RefreshSubscribePlayerUI(long playerId)
	{
		if (playerId != 0L && SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher() && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.IsPVE())
		{
			if (_SelfPlayer != null && _SelfPlayer.player.Id == playerId)
			{
				ResetCardList(isSelf: false);
				return;
			}
			RemoveSelfPlayerListener();
			_SelfPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			AddSelfPlayerListener();
			ResetCardList(isSelf: false);
			RefreshCardInfo();
		}
	}
}
