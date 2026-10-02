using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Camera;
using Core.Net;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace UI;

public class CardWindow : BaseWindow
{
	private CardInfoConfigure _UsingCardConfig;

	private const int CARD_NONE = 0;

	private const int CARD_USE = 1;

	private const int CARD_RESULT = 2;

	private const int CARD_PK = 3;

	private int PreCardId;

	private List<int> targetLandIds;

	private int obstacleNum;

	private int obstacleRange;

	private Effect walkStop;

	private readonly List<Effect> effects = new List<Effect>();

	private Action<List<int>> onSelectLand;

	private static int targetPoint;

	private UICard_Button_DiceNumb _curNumBtn;

	public CardWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UICardWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask<CardWindow> ShowCard()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		if (base.contentPane is UICardWindow uICardWindow)
		{
			uICardWindow.showContent.selectedIndex = 0;
		}
		return this;
	}

	public async UniTask<CardWindow> ShowCardResult()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		if (base.contentPane is UICardWindow uICardWindow)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.showActionMask.Dispatch(t: true);
			uICardWindow.showContent.selectedIndex = 2;
		}
		return this;
	}

	protected override void OnShown()
	{
		base.OnShown();
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.dealQuickCard.AddListener(DealQuickCard);
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.SetCancelBtn.AddListener(SetCancelBtn);
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UICardWindow uICardWindow)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.showActionMask.Dispatch(t: false);
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.SetCancelBtn.RemoveListener(SetCancelBtn);
			SimpleSingletonProvider<GameLogicManager>.inst.land.signal.chooseLand.RemoveListener(UpdateLandChoseStatus);
			SimpleSingletonProvider<GameLogicManager>.inst.action.signal.dealQuickCard.RemoveListener(DealQuickCard);
			uICardWindow.showContent.selectedIndex = 0;
			uICardWindow.com_UseCard.btn_Cancel.onClick.Release();
			uICardWindow.com_UseCard.btn_Sure.onClick.Release();
			uICardWindow.com_UseCard.type.selectedIndex = 0;
			uICardWindow.com_CardResult.targetState.selectedIndex = 0;
			Hide_SelectLand();
		}
	}

	public void OnCloseWin()
	{
		HideImmediately();
	}

	public async UniTask RefreshResult(CardInfoConfigure config, long PlayerId, RepeatedField<long> TargetIds, bool reverse)
	{
		GComponent gComponent = base.contentPane;
		UICardWindow win = gComponent as UICardWindow;
		if (win == null)
		{
			return;
		}
		if (!reverse)
		{
			win.com_CardResult.com_reverseCard.visible = false;
		}
		win.com_CardResult.btn_CancelQuickCard.visible = false;
		win.showContent.selectedIndex = 2;
		RefreshPlayerRelation(PlayerId, TargetIds);
		RefreshComCard(PlayerId, config, win.com_CardResult.com_ShowCard as UICom_Card, _showFront: false);
		win.com_CardResult.showCard.Play();
		if (await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => win.com_CardResult.showCard.playing, base.Hide))
		{
			return;
		}
		((UICom_Card)win.com_CardResult.com_ShowCard).Turn();
		if (!reverse)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.CARD, PlayerId);
		}
		if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500, base.Hide)))
		{
			if (!reverse)
			{
				Hide();
			}
			else
			{
				RefreshComCard(PlayerId, config, win.com_CardResult.com_reverseCard as UICom_Card);
			}
		}
	}

	private void RefreshPlayerRelation(long playerId, RepeatedField<long> targetIds)
	{
		if (!(base.contentPane is UICardWindow uICardWindow))
		{
			return;
		}
		if (targetIds != null && targetIds.Count > 0)
		{
			BattlePlayerData curPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (curPlayer == null)
			{
				return;
			}
			uICardWindow.com_CardResult.com_Self.loader.url = curPlayer.player.standingPainting.ProfilePhoto;
			uICardWindow.com_CardResult.com_Self.playerOrder.selectedIndex = curPlayer.player.Slot;
			uICardWindow.com_CardResult.list_Traget.itemRenderer = delegate(int index, GObject item)
			{
				if (item is UICardWin_Com_Loader_Lit uICardWin_Com_Loader_Lit)
				{
					curPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(targetIds[index]);
					if (curPlayer != null)
					{
						uICardWin_Com_Loader_Lit.loader.url = curPlayer.player.standingPainting.ProfilePhoto;
						if (curPlayer.characterType == CharacterType.Monster)
						{
							uICardWin_Com_Loader_Lit.playerOrder.selectedIndex = 4;
						}
						else
						{
							uICardWin_Com_Loader_Lit.playerOrder.selectedIndex = curPlayer.player.Slot;
						}
					}
				}
			};
			uICardWindow.com_CardResult.list_Traget.numItems = targetIds.Count;
			uICardWindow.com_CardResult.targetState.selectedIndex = ((targetIds.Count > 0) ? 1 : 0);
		}
		uICardWindow.com_CardResult.targetState.selectedIndex = ((targetIds != null && targetIds.Count > 0) ? 1 : 0);
	}

	private void DealQuickCard(long _playerId, int preCardId)
	{
		GComponent gComponent = base.contentPane;
		UICardWindow win = gComponent as UICardWindow;
		if (win != null)
		{
			win.com_CardResult.btn_CancelQuickCard.visible = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_playerId);
			PreCardId = preCardId;
			win.com_CardResult.btn_CancelQuickCard.onClick.Release();
			win.com_CardResult.btn_CancelQuickCard.onClick.Set((EventCallback0)delegate
			{
				win.com_CardResult.btn_CancelQuickCard.onClick.Retain();
				win.com_CardResult.btn_CancelQuickCard.visible = false;
				SimpleSingletonProvider<GameLogicManager>.inst.action.UsableCards = null;
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseQuickCardC2S(SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN, 0, 0L);
			});
			win.showContent.selectedIndex = 2;
		}
	}

	public async UniTask RefreshResult_QuickCard(CardInfoConfigure config, long PlayerId, RepeatedField<long> TargetIds, int OriginalCardId)
	{
		GComponent gComponent = base.contentPane;
		UICardWindow win = gComponent as UICardWindow;
		if (win != null)
		{
			if (PreCardId != 0 && PreCardId.GetCardConfigure().CardType != CardType.Counter)
			{
				win.com_CardResult.com_ShowCard.visible = false;
				win.com_CardResult.showWaitReverse.Play();
			}
			RefreshPlayerRelation(PlayerId, TargetIds);
			RefreshComCard(PlayerId, config, win.com_CardResult.com_ShowCard as UICom_Card, _showFront: false);
			CardInfoConfigure cardConfigure = OriginalCardId.GetCardConfigure();
			RefreshComCard(PlayerId, cardConfigure, win.com_CardResult.com_reverseCard as UICom_Card);
			win.com_CardResult.showCard.Play(delegate
			{
				((UICom_Card)win.com_CardResult.com_ShowCard).Turn();
			});
			SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.CARD, PlayerId);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500, base.Hide);
		}
	}

	public void ResetQuickCardData(int originalCardId, long originalPlayerId, MapField<long, bool> originalTargetIds, RepeatedField<QuickCardRecord> history)
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			CardInfoConfigure cardConfigure = originalCardId.GetCardConfigure();
			if (history.Count == 0)
			{
				KeyValuePair<long, bool> keyValuePair = originalTargetIds.FirstOrDefault();
				RefreshPlayerRelation(originalPlayerId, new RepeatedField<long> { keyValuePair.Key });
				RefreshComCard(originalPlayerId, cardConfigure, uICardWindow.com_CardResult.com_ShowCard as UICom_Card);
				uICardWindow.com_CardResult.showCard.Play();
			}
			else
			{
				QuickCardRecord quickCardRecord = history[history.Count - 1];
				RefreshPlayerRelation(quickCardRecord.PlayerId, new RepeatedField<long> { quickCardRecord.TargetId });
				CardInfoConfigure cardConfigure2 = quickCardRecord.CardId.GetCardConfigure();
				RefreshComCard(quickCardRecord.PlayerId, cardConfigure2, uICardWindow.com_CardResult.com_ShowCard as UICom_Card);
				uICardWindow.com_CardResult.showCard.Play();
				uICardWindow.com_CardResult.showWaitReverse.Play();
				RefreshComCard(quickCardRecord.PlayerId, cardConfigure, uICardWindow.com_CardResult.com_reverseCard as UICom_Card);
			}
		}
	}

	public void UpdateConfig(CardInfoConfigure cardInfoConfigure)
	{
		_UsingCardConfig = cardInfoConfigure;
	}

	public void RequestUseEffectCard(long _Sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseEffectCardC2S(_Sn, _UsingCardConfig.Id);
	}

	public void RequestUseQuickCard(long _Sn, long _prePlayerId)
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			uICardWindow.com_CardResult.btn_CancelQuickCard.onClick.Retain();
			uICardWindow.com_CardResult.btn_CancelQuickCard.visible = false;
			SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseQuickCardC2S(_Sn, _UsingCardConfig.Id, _prePlayerId);
		}
	}

	public void CancelUseCard()
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetCardList.Dispatch();
			uICardWindow.com_UseCard.btn_Cancel.onClick.Retain();
			OnCloseWin();
		}
	}

	private void SetCancelBtn(bool _state)
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			uICardWindow.com_CardResult.btn_CancelQuickCard.visible = _state;
		}
	}

	private void RefreshComCard(long _playerId, CardInfoConfigure _Config, UICom_Card _com_Card, bool _showFront = true)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue(_Config.Id, out var value))
		{
			string content = value.CardDescription(_playerId, replaceDamage: true);
			string local = _Config.NameID.GetLocal(UIStringType.Card);
			string cardIndex = ((_Config.CardNumb == 0) ? "" : _Config.CardNumb.GetLocal(UIStringType.Card));
			string cardTips = ((_Config.CommentId == 0) ? "" : _Config.CommentId.GetLocal(UIStringType.Card));
			int costValue = value.GetCostValue(_playerId);
			CommonUIManager.RendererCardInRoom(_playerId, _com_Card, _Config.GetBattlePlayerCardView(_playerId), local, content, cardIndex, cardTips, costValue, _Config.CardType, _Config.CardTargetType, _showFront);
		}
		else
		{
			Debug.LogError($"cardActions TryGet Action is Failure by CardIs:{_Config.Id}");
		}
	}

	public async UniTask ShowPKCard(long playerId, int cardId, int leftCardId, int rightCardId, long actionSn)
	{
		await ShowCard();
		if (base.contentPane is UICardWindow uICardWindow)
		{
			uICardWindow.showContent.selectedIndex = 3;
			CardInfoConfigure cardConfigure = leftCardId.GetCardConfigure();
			RefreshPKCard(playerId, cardConfigure, (UICom_Card)uICardWindow.com_ChooseCard.com_LeftCard);
			CardInfoConfigure cardConfigure2 = rightCardId.GetCardConfigure();
			RefreshPKCard(playerId, cardConfigure2, (UICom_Card)uICardWindow.com_ChooseCard.com_RightCard);
			uICardWindow.com_ChooseCard.mohu.onClick.Set(CancelUseCard);
			uICardWindow.com_ChooseCard.com_LeftCard.onClick.Set((EventCallback0)delegate
			{
				SelectCard(cardId, leftCardId, actionSn);
			});
			uICardWindow.com_ChooseCard.com_RightCard.onClick.Set((EventCallback0)delegate
			{
				SelectCard(cardId, rightCardId, actionSn);
			});
		}
	}

	private void SelectCard(int cardId, int selectCardId, long actionSn)
	{
		if (actionSn == 0L)
		{
			return;
		}
		Hide();
		if (SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue(selectCardId, out var value))
		{
			if (value is IChoiceEffectCard choiceEffectCard)
			{
				choiceEffectCard.ActiveEffectCardAfterChoose(cardId, actionSn);
			}
			else
			{
				Debug.LogError($"卡牌{selectCardId} 并不是一张抉择效果牌，请检查");
			}
		}
	}

	private void RefreshPKCard(long _playerId, CardInfoConfigure _Config, UICom_Card _com_Card, bool _showFront = true)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue(_Config.Id, out var value))
		{
			string content = value.CardDescription(_playerId, replaceDamage: true);
			string local = _Config.NameID.GetLocal(UIStringType.Card);
			string cardIndex = ((_Config.CardNumb == 0) ? "" : _Config.CardNumb.GetLocal(UIStringType.Card));
			string cardTips = ((_Config.CommentId == 0) ? "" : _Config.CommentId.GetLocal(UIStringType.Card));
			int costValue = value.GetCostValue(_playerId);
			_com_Card.grayed = !value.VailStatus();
			_com_Card.touchable = !_com_Card.grayed;
			CommonUIManager.RendererCardInRoom(_playerId, _com_Card, _Config.GetBattlePlayerCardView(_playerId), local, content, cardIndex, cardTips, costValue, _Config.CardType, _Config.CardTargetType, _showFront);
		}
		else
		{
			Debug.LogError($"cardActions TryGet Action is Failure by CardIs:{_Config.Id}");
		}
	}

	public void RefreshCardInfo_SelectLand(int _obstacleNum, int _obstacleRange, long _Sn)
	{
		GComponent gComponent = base.contentPane;
		UICardWindow win = gComponent as UICardWindow;
		if (win == null)
		{
			return;
		}
		InitSelectLandStatus(_obstacleNum, _obstacleRange);
		win.com_UseCard.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			win.com_UseCard.btn_Sure.onClick.Retain();
			if (targetLandIds.Count < _obstacleNum)
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10010);
				win.com_UseCard.btn_Sure.onClick.Release();
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseEffectCardC2S(_Sn, _UsingCardConfig.Id, null, targetLandIds).OnFinishedOnly.AddOnce(delegate
				{
					_Sn = 0L;
					CancelSelectLand();
					win.com_UseCard.btn_Sure.onClick.Release();
				});
			}
		});
		win.com_UseCard.btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			win.com_UseCard.btn_Cancel.onClick.Retain();
			CancelSelectLand();
			CancelUseCard();
			win.com_UseCard.btn_Cancel.onClick.Release();
		});
	}

	public void RefreshSkill_SelectLand(int skillId, int _obstacleNum, int _obstacleRange, long _Sn)
	{
		GComponent gComponent = base.contentPane;
		UICardWindow win = gComponent as UICardWindow;
		if (win == null)
		{
			return;
		}
		InitSelectLandStatus(_obstacleNum, _obstacleRange);
		win.com_UseCard.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			win.com_UseCard.btn_Sure.onClick.Retain();
			if (targetLandIds.Count < _obstacleNum)
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10010);
				win.com_UseCard.btn_Sure.onClick.Release();
			}
			else
			{
				onSelectLand?.Invoke(new List<int>(targetLandIds));
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestReleaseSkillC2S(_Sn, skillId, null, null, null, 0, targetLandIds).OnFinishedOnly.AddOnce(delegate
				{
					CancelSelectLand();
					win.com_UseCard.btn_Sure.onClick.Release();
				});
			}
		});
		win.com_UseCard.btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			win.com_UseCard.btn_Cancel.onClick.Retain();
			CancelSelectLand();
			CancelUseCard();
			SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CARD;
			win.com_UseCard.btn_Cancel.onClick.Release();
		});
	}

	public void RefreshSkill_SelectLand(int skillId, int _obstacleNum, int _obstacleRange, long _Sn, Action<List<int>> _onSelectLand)
	{
		onSelectLand = _onSelectLand;
		RefreshSkill_SelectLand(skillId, _obstacleNum, _obstacleRange, _Sn);
	}

	private void InitSelectLandStatus(int _obstacleNum, int _obstacleRange)
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			PreviewScope(state: true, _obstacleRange);
			targetLandIds = new List<int>();
			obstacleNum = _obstacleNum;
			obstacleRange = _obstacleRange;
			uICardWindow.com_UseCard.type.selectedIndex = 4;
			SimpleSingletonProvider<GameLogicManager>.inst.land.signal.chooseLand.AddListener(UpdateLandChoseStatus);
			Character characterInst = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetCurrentPlayer().CharacterInst;
			SimpleSingletonProvider<CameraManager>.inst.EnableFreeCamera(characterInst.GetCharacterCamera());
			uICardWindow.showContent.selectedIndex = 1;
		}
	}

	private async void UpdateLandChoseStatus(UnitLand land)
	{
		if (!base.isShowing)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UICardWindow win) || (object)land == null)
		{
			return;
		}
		Character characterInst = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetCurrentPlayer().CharacterInst;
		if (SimpleSingletonProvider<LandManager>.inst.CheckDistance(characterInst.standLand.Id, land.Id, obstacleRange, 0))
		{
			if (!targetLandIds.Contains(land.Id))
			{
				if (targetLandIds.Count == obstacleNum)
				{
					targetLandIds.RemoveAt(0);
				}
				targetLandIds.Add(land.Id);
				UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(land.Id);
				if (walkStop != null)
				{
					walkStop.ReleaseEffect();
				}
				walkStop = await SimpleSingletonProvider<EffectManager>.inst.PlayById(30, Vector3.zero, Quaternion.identity, landById.transform);
				win.com_UseCard.txt_LandId.text = land.Id.ToString().PadLeft(2, '0');
			}
			else
			{
				targetLandIds.Remove(land.Id);
				win.com_UseCard.txt_LandId.text = "00";
				if (walkStop != null)
				{
					walkStop.ReleaseEffect();
				}
			}
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(string.Format(10011.GetLocal(UIStringType.Message), obstacleRange));
		}
	}

	public void CancelSelectLand()
	{
		PreviewScope(state: false);
		if (walkStop != null)
		{
			walkStop.ReleaseEffect();
		}
		targetLandIds?.Clear();
	}

	private void Hide_SelectLand()
	{
		if (base.contentPane is UICardWindow uICardWindow && uICardWindow.com_UseCard.type.selectedIndex == 4)
		{
			CancelSelectLand();
		}
	}

	private async void PreviewScope(bool state, int _obstacleRange = 0)
	{
		if (state && effects.Count == 0)
		{
			List<int> lands = SimpleSingletonProvider<LandManager>.inst.GetLandsByScope(_obstacleRange);
			for (int i = 0; i < lands.Count; i++)
			{
				UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(lands[i]);
				Effect item = await SimpleSingletonProvider<EffectManager>.inst.PlayById(29, Vector3.zero, Quaternion.identity, landById.transform);
				effects.Add(item);
			}
			return;
		}
		for (int j = 0; j < effects.Count; j++)
		{
			if (effects[j] != null)
			{
				effects[j].ReleaseEffect();
			}
		}
		effects.Clear();
	}

	public void RefreshCardInfo_SelectPlayer(RepeatedField<long> _playerIds, int targetNum, int rangeLen, long _Sn)
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			uICardWindow.com_UseCard.type.selectedIndex = 1;
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue(_UsingCardConfig.Id, out var value))
			{
				string content = value.CardDescription(selfPlayerData.player.Id, replaceDamage: true);
				string local = _UsingCardConfig.NameID.GetLocal(UIStringType.Card);
				string cardIndex = ((_UsingCardConfig.CardNumb == 0) ? "" : _UsingCardConfig.CardNumb.GetLocal(UIStringType.Card));
				string cardTips = ((_UsingCardConfig.CommentId == 0) ? "" : _UsingCardConfig.CommentId.GetLocal(UIStringType.Card));
				int costValue = value.GetCostValue(selfPlayerData.player.Id);
				CommonUIManager.RendererCardInRoom(selfPlayerData.player.Id, uICardWindow.com_UseCard.com_ShowCard as UICom_Card, _UsingCardConfig.GetBattlePlayerCardView(selfPlayerData.player.Id), local, content, cardIndex, cardTips, costValue, _UsingCardConfig.CardType, _UsingCardConfig.CardTargetType);
			}
			else
			{
				Debug.LogError($"cardActions TryGet Action is Failure by CardIs:{_UsingCardConfig.Id}");
			}
			uICardWindow.showContent.selectedIndex = 1;
			uICardWindow.com_UseCard.cardIn.Play();
			SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CHOOSETARGET_CARD;
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.selectPlayer.Dispatch(_UsingCardConfig.Id, _playerIds, targetNum);
		}
	}

	public void ChangeChild(UIBattleInfo_Button_PlayerInfo _playerBtnInfo)
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			uICardWindow.AddChild(_playerBtnInfo);
		}
	}

	public void CloseSelectPlayer()
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			uICardWindow.com_UseCard.cardOut.Play(base.HideImmediately);
		}
	}

	public void CancelSelectPlayer()
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			uICardWindow.com_UseCard.cardOut.Play(base.HideImmediately);
		}
	}

	public void RefreshCardInfo_ControlMoveCard(party.model.Action _action)
	{
		ThrowDiceResultC2S throwDiceResultC2S = ByteBuf.ReadObject<ThrowDiceResultC2S>(_action.Data.ToByteArray());
		targetPoint = 0;
		GComponent gComponent = base.contentPane;
		UICardWindow win = gComponent as UICardWindow;
		if (win == null)
		{
			return;
		}
		win.com_UseCard.com_SelectPoint.selectType.selectedIndex = 0;
		RefreshPointList(win, throwDiceResultC2S.MaxPoint);
		win.com_UseCard.com_SelectPoint.btn_SurePoint.onClick.Release();
		win.com_UseCard.com_SelectPoint.btn_SurePoint.onClick.Set((EventCallback0)delegate
		{
			if (targetPoint == 0)
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10015);
			}
			else
			{
				win.com_UseCard.com_SelectPoint.btn_SurePoint.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestThrowDiceResultC2S(_action.Sn, targetPoint).OnFinishedOnly.AddOnce(delegate
				{
					Hide();
					_curNumBtn = null;
					win.com_UseCard.com_SelectPoint.btn_SurePoint.onClick.Release();
				});
			}
		});
		win.com_UseCard.btn_Cancel.onClick.Set(CancelUseCard);
		if (OperationTimer.GetOperateTimer(_action.Sn) == null)
		{
			OperationTimer.ActionDownTime(_action.Sn, 5067, delegate
			{
				targetPoint = 1;
				win.com_UseCard.com_SelectPoint.btn_SurePoint.onClick.Call();
			});
		}
	}

	private void RefreshPointList(UICardWindow win, int maxPoint)
	{
		_curNumBtn = null;
		win.showContent.selectedIndex = 1;
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.showActionMask.Dispatch(t: true);
		win.com_UseCard.type.selectedIndex = 3;
		win.com_UseCard.com_SelectPoint.list_DiceNum.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UICard_Button_DiceNumb uICard_Button_DiceNumb)
			{
				uICard_Button_DiceNumb.grayed = true;
				uICard_Button_DiceNumb.selected = false;
				uICard_Button_DiceNumb.numType.selectedIndex = index;
			}
		};
		win.com_UseCard.com_SelectPoint.list_DiceNum.numItems = maxPoint;
		win.com_UseCard.com_SelectPoint.list_DiceNum.onClickItem.Set(SelectDiceNum);
	}

	private void SelectDiceNum(EventContext context)
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			if (_curNumBtn != null)
			{
				_curNumBtn.grayed = !_curNumBtn.grayed;
			}
			_curNumBtn = (UICard_Button_DiceNumb)context.data;
			_curNumBtn.grayed = !_curNumBtn.grayed;
			targetPoint = uICardWindow.com_UseCard.com_SelectPoint.list_DiceNum.selectedIndex + 1;
		}
	}

	public void RefreshCardInfo_SkillSelectPoint(long actionSn, int skillId, int maxPoint)
	{
		GComponent gComponent = base.contentPane;
		UICardWindow win = gComponent as UICardWindow;
		if (win == null)
		{
			return;
		}
		win.com_UseCard.com_SelectPoint.selectType.selectedIndex = 1;
		targetPoint = 0;
		RefreshPointList(win, maxPoint);
		win.com_UseCard.com_SelectPoint.btn_OK.onClick.Release();
		win.com_UseCard.com_SelectPoint.btn_OK.onClick.Set((EventCallback0)delegate
		{
			if (targetPoint == 0)
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10015);
			}
			else
			{
				win.com_UseCard.com_SelectPoint.btn_OK.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestReleaseSkillC2S(actionSn, skillId, null, null, null, targetPoint).OnFinishedOnly.AddOnce(delegate
				{
					Hide();
					_curNumBtn = null;
					win.com_UseCard.com_SelectPoint.btn_OK.onClick.Release();
				});
			}
		});
		win.com_UseCard.btn_Cancel.onClick.Set(CancelSelectPoint);
		win.com_UseCard.com_SelectPoint.btn_Cancel.onClick.Set(CancelSelectPoint);
	}

	private void CancelSelectPoint(EventContext context)
	{
		if (base.contentPane is UICardWindow uICardWindow)
		{
			uICardWindow.com_UseCard.com_SelectPoint.btn_Cancel.onClick.Retain();
			uICardWindow.com_UseCard.btn_Cancel.onClick.Retain();
			CancelUseCard();
			SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CARD;
			uICardWindow.com_UseCard.btn_Cancel.onClick.Release();
			uICardWindow.com_UseCard.com_SelectPoint.btn_Cancel.onClick.Release();
		}
	}
}
