using System;
using Core;
using Core.Mark;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIHandCard_Button_Card : GButton, IMarkTarget
{
	public float CustomRotation;

	private Vector2 CustomPosition;

	private int _CustomSortingOrder;

	private HandCardData _CardData;

	private bool _EnableUse;

	private readonly Vector2 zoomInScale = new Vector2(0.72f, 0.72f);

	private readonly Vector2 zoomOutScale = new Vector2(0.44f, 0.44f);

	public bool IsRelease;

	private bool _isSuggest;

	public GGraph effectOutline_Suggest_Bottom;

	public GComponent com_Card;

	public GGraph effectTempCard;

	public GGraph effectOutline;

	public GGraph effectGuide;

	public GGraph effectCardSwitch;

	public const string URL = "ui://vflhnh8dorg7d";

	private Vector2 _ShowPosition => new Vector2(CustomPosition.x, GRoot.inst.height - base.height * 0.7f * base.scaleX);

	public HandCardData CardData => _CardData;

	public CardInfoConfigure _config => _CardData.Config;

	bool IMarkTarget.HoverWait => false;

	public void InitData_EffectCard(int _index, HandCardData cardData)
	{
		_CardData = cardData;
		int customSortingOrder = (base.sortingOrder = _index);
		_CustomSortingOrder = customSortingOrder;
		bool flag = (base.draggable = false);
		base.selected = flag;
		base.visible = true;
		base.alpha = 1f;
		base.touchable = true;
		com_Card.visible = true;
		effectOutline.visible = false;
		effectOutline.displayObject.gameObject.SetActive(value: false);
		RendererCard();
		UpdateCardTempState();
	}

	public void InitData_BattleCard(int _index, HandCardData cardData)
	{
		_CardData = cardData;
		int customSortingOrder = (base.sortingOrder = _index);
		_CustomSortingOrder = customSortingOrder;
		base.selected = false;
		GComponent gComponent = com_Card;
		bool flag = (base.draggable = true);
		bool flag3 = (base.visible = flag);
		bool enableUse = (gComponent.visible = flag3);
		_EnableUse = enableUse;
		base.alpha = 1f;
		effectOutline.visible = true;
		base.touchable = true;
		RendererCard();
		UpdateCardTempState();
		UpdateUsableState(enableUse: true).Forget();
	}

	private void RendererCard()
	{
		CloseSuggestCard();
		effectGuide.visible = false;
		com_Card.scale = zoomInScale;
		SetScale(1f, 1f);
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue(_config.Id, out var value))
		{
			string content = value.CardDescription(selfPlayerData.player.Id, CardData);
			string local = _config.NameID.GetLocal(UIStringType.Card);
			string cardIndex = ((_config.CardNumb == 0) ? "" : _config.CardNumb.GetLocal(UIStringType.Card));
			string cardTips = ((_config.CommentId == 0) ? "" : _config.CommentId.GetLocal(UIStringType.Card));
			int costValue = value.GetCostValue(selfPlayerData.player.Id, CardData);
			CardView battlePlayerCardView = _config.GetBattlePlayerCardView(selfPlayerData.player.Id);
			UICom_Card uICom_Card = com_Card as UICom_Card;
			CommonUIManager.RendererCardInRoom(selfPlayerData.player.Id, uICom_Card, battlePlayerCardView, local, content, cardIndex, cardTips, costValue, _config.CardType, _config.CardTargetType);
			if (battlePlayerCardView.CardType == 2)
			{
				uICom_Card.ChangeCardDescState(isHide: true, isPlayAni: false);
			}
			else
			{
				uICom_Card.ChangeCardDescState(isHide: false, isPlayAni: false);
			}
		}
		else
		{
			Debug.LogError($"cardActions TryGet Action is Failure by CardIs:{_config.Id}");
		}
		ZoomOutCard();
		base.onClick.Set((EventCallback0)delegate
		{
			if (CardPool.ShowCardStatus == ShowCardStatus.None)
			{
				CardPool.ShowCardStatus = ShowCardStatus.Display;
				SimpleSingletonProvider<GameLogicManager>.inst.card.signal.LookCard.Dispatch(t: true);
			}
			else if (CardPool.ShowCardStatus == ShowCardStatus.Display)
			{
				CardPool.ShowCardStatus = ShowCardStatus.Detail;
				ZoomInCard(isPlayAni: true);
			}
			else if (CardPool.ShowCardStatus == ShowCardStatus.Detail)
			{
				CardPool.ShowCardStatus = ShowCardStatus.Display;
				SimpleSingletonProvider<GameLogicManager>.inst.card.signal.ZoomOutCard.Dispatch();
			}
		});
	}

	public async UniTaskVoid UpdateUsableState(bool enableUse)
	{
		base.touchable = true;
		effectOutline.displayObject.gameObject.SetActive(enableUse);
		base.draggable = enableUse;
		_EnableUse = enableUse;
		if (enableUse)
		{
			int effectID = (_isSuggest ? 61 : 60);
			SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectID.GetEffectDataConfigure().EffectName, effectOutline, (base.sortingOrder != 100) ? 43f : 72f).Forget();
			if (StaticConfigure.Effect.InfoDict.TryGetValue(22, out var value))
			{
				await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, effectGuide, 80f);
			}
		}
		else
		{
			CloseSuggestCard();
		}
		effectGuide.displayObject.gameObject.SetActive(value: false);
	}

	private void UpdateCardTempState()
	{
		if (_CardData.IsTemp)
		{
			if (StaticConfigure.Effect.InfoDict.TryGetValue(11609, out var value))
			{
				effectTempCard.displayObject.gameObject.SetActive(value: true);
				float num = ((base.sortingOrder != 100) ? 43f : 72f);
				SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, effectTempCard, num).Forget();
			}
		}
		else
		{
			effectTempCard.visible = false;
			effectTempCard.displayObject.gameObject.SetActive(value: false);
		}
	}

	private void ZoomInCard(bool isPlayAni = false)
	{
		Stage.inst.PlayOneShotSound(12);
		base.sortingOrder = 100;
		TweenRotate(0f, 0.05f).SetEase(EaseType.Linear);
		TweenMove(_ShowPosition, 0.1f).SetEase(EaseType.Linear);
		com_Card.scale = zoomInScale;
		effectOutline.displayObject.scale = Vector2.one * 72f;
		effectOutline_Suggest_Bottom.displayObject.scale = Vector2.one * 72f;
		effectTempCard.displayObject.scale = Vector2.one * 72f;
		effectGuide.displayObject.gameObject.SetActive(_EnableUse);
		CardScope(state: true);
		if (isPlayAni)
		{
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (_config.GetBattlePlayerCardView(selfPlayerData.player.Id).CardType == 2 && com_Card is UICom_Card uICom_Card)
			{
				uICom_Card.ChangeCardDescState(isHide: false, isPlayAni: true);
			}
		}
	}

	public void ZoomOutCard(bool isPlayAni = false)
	{
		base.sortingOrder = _CustomSortingOrder;
		TweenRotate(CustomRotation, 0.05f).SetEase(EaseType.Linear);
		TweenMove(CustomPosition, 0.1f).SetEase(EaseType.Linear);
		com_Card.scale = zoomOutScale;
		effectOutline.displayObject.scale = Vector2.one * 43f;
		effectOutline_Suggest_Bottom.displayObject.scale = Vector2.one * 43f;
		effectTempCard.displayObject.scale = Vector2.one * 43f;
		effectGuide.displayObject.gameObject.SetActive(value: false);
		CardScope(state: false);
		if (isPlayAni)
		{
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (_config.GetBattlePlayerCardView(selfPlayerData.player.Id).CardType == 2 && com_Card is UICom_Card uICom_Card)
			{
				uICom_Card.ChangeCardDescState(isHide: true, isPlayAni: true);
			}
		}
	}

	public void ShowUseCardWin(long _Sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().cardContainer.PreUseCardGuid = _CardData.Guid;
		SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions[_config.Id].CardAction(_Sn).Forget();
	}

	public void CardScope(bool state)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions[_config.Id].CardScope(state);
	}

	public void Release()
	{
		SetXY(0f, Screen.height);
		base.sortingOrder = 0;
		bool flag = (base.draggable = false);
		base.selected = flag;
		base.visible = false;
		SetScale(1f, 1f);
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(effectOutline);
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(effectGuide);
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(effectOutline_Suggest_Bottom);
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(effectCardSwitch);
		UnRegisterLongPressEvent();
		base.onRollOver.Release();
		base.onRollOut.Release();
		if (com_Card is UICom_Card uICom_Card)
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAuto(uICom_Card.video_FrontCard);
		}
		IsRelease = true;
	}

	public void UpdateCustomRotation(float _rotation)
	{
		CustomRotation = _rotation;
	}

	public void UpdateCustomPosition((float, float) _pos)
	{
		CustomPosition = new Vector2(_pos.Item1, _pos.Item2);
	}

	public void DisplayZoom(bool isZoom)
	{
		float num = (isZoom ? 1.6f : 1f);
		if ((double)Math.Abs(num - base.scaleX) > 0.1)
		{
			CustomPosition.y += (isZoom ? (-100) : 100);
			base.y = CustomPosition.y;
		}
		SetScale(num, num);
		ZoomOutCard();
		if (!isZoom && com_Card is UICom_Card uICom_Card)
		{
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (_config.GetBattlePlayerCardView(selfPlayerData.player.Id).CardType == 2)
			{
				uICom_Card.ChangeCardDescState(isHide: true, isPlayAni: true);
			}
		}
	}

	public bool IsDistribute()
	{
		if (base.x == 0f)
		{
			return Math.Abs(base.y - (float)Screen.height) == 0f;
		}
		return false;
	}

	public void PlayTransition(float w, float h, bool IsDistribute)
	{
		base.onRollOver.Retain();
		InvalidateBatchingState();
		if (IsDistribute)
		{
			TweenMove(new Vector2(w * 0.5f, h * 0.7f), 0.1f).OnComplete(TranslateCard).SetEase(EaseType.Linear);
		}
		else
		{
			TranslateCard();
		}
	}

	private void TranslateCard()
	{
		Vector2 endValue = ((base.sortingOrder != 100) ? CustomPosition : _ShowPosition);
		TweenMove(endValue, 0.2f).OnComplete((GTweenCallback)delegate
		{
			base.onRollOver.Release();
			if (base.sortingOrder != 100)
			{
				TweenRotate(CustomRotation, 0.1f).SetEase(EaseType.Linear);
			}
			InvalidateBatchingState();
		}).SetEase(EaseType.Linear);
	}

	public void FadeCard(float _alpha)
	{
		TweenFade(_alpha, 0.15f).SetEase(EaseType.Linear);
	}

	public void DisplayCard()
	{
		base.onRollOver.Retain();
		base.onRollOut.Retain();
		if (base.sortingOrder != 100)
		{
			ZoomInCard(isPlayAni: true);
			base.sortingOrder = 100;
			base.rotation = 0f;
			base.position = _ShowPosition;
			effectOutline.displayObject.scale = Vector2.one * 72f;
			effectOutline_Suggest_Bottom.displayObject.scale = Vector2.one * 72f;
			effectGuide.displayObject.gameObject.SetActive(_EnableUse);
		}
	}

	public void ShowSuggestCard(bool isSuggest)
	{
		_isSuggest = isSuggest;
		if (_isSuggest)
		{
			SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(62.GetEffectDataConfigure().EffectName, effectOutline_Suggest_Bottom, (base.sortingOrder != 100) ? 43f : 72f).Forget();
		}
	}

	private void CloseSuggestCard()
	{
		effectOutline_Suggest_Bottom.visible = false;
	}

	public async UniTask DoTutorialUseCard(Action onComplete)
	{
		UIHandCard_Button_Card uIHandCard_Button_Card = this;
		bool flag = (base.draggable = false);
		uIHandCard_Button_Card.touchable = flag;
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500);
		ZoomInCard();
		TweenMove(new Vector2(base.parent.width * 0.5f, base.parent.height * 0.5f), 0.2f).OnComplete((GTweenCallback)delegate
		{
			onComplete?.Invoke();
		}).SetEase(EaseType.Linear);
	}

	public async UniTask PlayCardSwitchEffect()
	{
		GameObject gameObject = await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(1012.GetEffectDataConfigure().EffectName, effectCardSwitch, (base.sortingOrder != 100) ? 43f : 72f);
		if (gameObject != null)
		{
			int millisecondsDelay = (int)(gameObject.GetComponent<GameEffect>().duration * 0.5f * 1000f);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(millisecondsDelay);
		}
	}

	public void OnMarkHoverEnter()
	{
		if (com_Card is UICom_Card uICom_Card)
		{
			SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(69.GetEffectDataConfigure().EffectName, uICom_Card.graph_Hover, 100f).Forget();
			uICom_Card.graph_Hover.visible = true;
			if (_config != null)
			{
				string cardMsg = BattleCardMessage.GetCardMsg(_config.Id);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.ShowPreview?.Dispatch(cardMsg);
			}
		}
	}

	public void OnMarkHoverExit()
	{
		if (com_Card is UICom_Card uICom_Card)
		{
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(uICom_Card.graph_Hover);
			uICom_Card.graph_Hover.visible = false;
		}
	}

	public void OnMarkSelected()
	{
		OnMarkHoverExit();
		if (_config != null)
		{
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (selfPlayerData != null)
			{
				BattleCardMessage msgData = new BattleCardMessage(selfPlayerData, _config.Id);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestSendMessageC2S(msgData, BattleCardMessage.MarkId);
			}
		}
	}

	public void TriggerHoverConfirmed()
	{
	}

	public Vector2 GetPosition()
	{
		return LocalToGlobal(Vector2.zero);
	}

	public static UIHandCard_Button_Card CreateInstance()
	{
		return (UIHandCard_Button_Card)UIPackage.CreateObject("HandCard", "HandCard_Button_Card");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		effectOutline_Suggest_Bottom = (GGraph)GetChildAt(0);
		com_Card = (GComponent)GetChildAt(1);
		effectTempCard = (GGraph)GetChildAt(2);
		effectOutline = (GGraph)GetChildAt(3);
		effectGuide = (GGraph)GetChildAt(4);
		effectCardSwitch = (GGraph)GetChildAt(5);
	}
}
