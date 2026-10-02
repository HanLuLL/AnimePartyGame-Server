using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Scene;
using Core.Tutorial.Tools;
using Core.Unit;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class FightWindow : BaseWindow
{
	private const int ATTACKER = 0;

	private const int DEFENDER = 1;

	private const int AUDIENCE = 2;

	private const int STEP_NONE = 0;

	private const int STEP_CHALLENGE = 1;

	private const int STEP_SHOWTIME = 2;

	private const int STEP_USECARD = 3;

	private const int STEP_DICE = 4;

	private const int STEP_CHOOSEACTIVE = 5;

	private const int STEP_RESULT = 6;

	private BattlePlayerData _AttackerData;

	private BattlePlayerData _DefenderData;

	private Texture2D blurTex;

	private readonly List<UIHandCard_Button_Card> cardItemList = new List<UIHandCard_Button_Card>();

	private int attackCardCost;

	private int defendCardCost;

	private long useBattleCardSn;

	private long challengeActionSn;

	private readonly string[] _fightBGMaterialKeys = new string[5] { "UIFight_BG_Red", "UIFight_BG_Green", "UIFight_BG_Blue", "UIFight_BG_Orange", "UIFight_BG_Purple" };

	private long attackThrowDiceSn;

	private long dodgeChoice;

	private bool dodgeNoAvailable;

	private UniTaskCompletionSource fightStartTsc;

	private UIFight_Com_Value_Card[] atkCards;

	private UIFight_Com_Value_Card[] vicCards;

	private BattleFightData fightData => SimpleSingletonProvider<GameLogicManager>.inst.fight.battleFightData;

	private BattlePlayerData attackerData
	{
		get
		{
			if (_AttackerData == null)
			{
				Debug.LogError("attackerData is null, try Update attackerData");
				if (fightData?.attackerInfo != null)
				{
					List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
					for (int i = 0; i < playerDatas.Count; i++)
					{
						Debug.LogError(playerDatas[i].player.Id + $"player[i].CharacterInst is null: {playerDatas[i].CharacterInst == null}");
					}
					Debug.LogError($"fightData.attackerInfo.PlayerId :{fightData.attackerInfo.PlayerId}");
					_AttackerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(fightData.attackerInfo.PlayerId);
				}
				else
				{
					Debug.LogError($"fightData is null : {fightData == null}  or fightData?.attackerInfo is null : {fightData?.attackerInfo == null}");
				}
			}
			return _AttackerData;
		}
	}

	private BattlePlayerData defenderData
	{
		get
		{
			if (_DefenderData == null)
			{
				Debug.LogError("defenderData is null, try Update defenderData");
				if (fightData?.defenderInfo != null)
				{
					List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
					for (int i = 0; i < playerDatas.Count; i++)
					{
						Debug.LogError(playerDatas[i].player.Id + $"player[i].CharacterInst is null: {playerDatas[i].CharacterInst == null}");
					}
					Debug.LogError($"fightData.attackerInfo.PlayerId :{fightData.defenderInfo.PlayerId}");
					_DefenderData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(fightData.defenderInfo.PlayerId);
				}
				else
				{
					Debug.LogError($"fightData is null : {fightData == null}  or fightData?.defenderInfo is null : {fightData?.defenderInfo == null}");
				}
			}
			return _DefenderData;
		}
	}

	private int AttackerResidueCost
	{
		get
		{
			if (fightData?.attackerInfo == null)
			{
				return 0;
			}
			return fightData.attackerInfo.Cost;
		}
	}

	private int DefenderResidueCost
	{
		get
		{
			if (fightData?.defenderInfo == null)
			{
				return 0;
			}
			return fightData.defenderInfo.Cost;
		}
	}

	private float CenterRadius => 918f;

	private Vector2 centerPoint => new Vector2(base.width * 0.5f, base.height + 810f);

	public FightWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIFightWindow.CreateInstance();
		InitCardData();
		base.OnInit();
	}

	private async UniTask CreateBlurTex()
	{
		ReleaseBlurTex();
		GraphicEventManager.Instance.CaptureBlurRequest((GameCameraFlag)0, (CaptureBlurCallback)delegate(Texture2D tex)
		{
			tex.wrapMode = TextureWrapMode.Clamp;
			blurTex = tex;
		});
		for (int i = 0; i < 10; i++)
		{
			if (blurTex != null)
			{
				break;
			}
			await UniTask.DelayFrame(1);
		}
	}

	private void ReleaseBlurTex()
	{
		if (blurTex != null)
		{
			Object.DestroyImmediate(blurTex);
			blurTex = null;
		}
	}

	public async UniTask<FightWindow> ShowWin()
	{
		if (!base.isShowing)
		{
			await CreateBlurTex();
		}
		Show();
		if (!base.initialized)
		{
			string videoKey = 3.GetVideoKey();
			await SimpleSingletonProvider<CriMovieManager>.inst.Load(videoKey);
			await UniTask.WaitUntil(() => base.initialized);
		}
		return this;
	}

	protected override void OnShown()
	{
		base.OnShown();
		SimpleSingletonProvider<UIManager>.inst.CloseAllUnFightWin();
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.com_LaunchPK.img_Defender.visible = false;
			uIFightWindow.com_LaunchPK.txt_HideTip.text = 11023.GetLocal(UIStringType.Message);
			uIFightWindow.com_LaunchPK.Hide.selectedIndex = 0;
			uIFightWindow.loader_FightShow.visible = false;
			uIFightWindow.com_LaunchPK.btn_Leave.onClick.Add(RequestClosePKWin);
			uIFightWindow.com_LaunchPK.btn_PK.onClick.Add(SureLaunch);
			uIFightWindow.com_LaunchPK.btn_ShowWin.onClick.Add(EnableThrough);
			uIFightWindow.com_LaunchPK.btn_HideWin.onClick.Add(CloseThrough);
			uIFightWindow.com_value.visible = false;
			uIFightWindow.com_value.touchable = false;
			Card_AddEvent();
			Dice_AddEvent();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.step.selectedIndex = 0;
			uIFightWindow.loader_FightShow.visible = false;
			uIFightWindow.com_LaunchPK.btn_Leave.onClick.Remove(RequestClosePKWin);
			uIFightWindow.com_LaunchPK.btn_PK.onClick.Remove(SureLaunch);
			uIFightWindow.com_LaunchPK.btn_ShowWin.onClick.Remove(EnableThrough);
			uIFightWindow.com_LaunchPK.btn_HideWin.onClick.Remove(CloseThrough);
			Card_RemoveEvent();
			Dice_RemoveEvent();
			uIFightWindow.btn_Dodge.visible = true;
			uIFightWindow.btn_Defend.visible = true;
			uIFightWindow.com_Card.btn_FinishPkCard.visible = true;
			uIFightWindow.com_Card.com_CostLabel.visible = true;
			uIFightWindow.com_Show.graph_Mask.alpha = 0f;
			attackCardCost = 0;
			defendCardCost = 0;
			StopAndDisposeFightShow();
			ReleaseBlurTex();
		}
	}

	public override void Dispose()
	{
		CardPool.Clear();
		base.Dispose();
	}

	public bool IsSelf(long playerId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId);
	}

	public async void ShowFightVideo(string videoKey)
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			StopAndDisposeFightShow();
			uIFightWindow.loader_FightShow.FullScreen();
			(await SimpleSingletonProvider<CriMovieManager>.inst.Play(videoKey, uIFightWindow.loader_FightShow, ShowFightVideoLoader, CloseFightVideo)).SetSpeed(BattleConfig.RoleAnimatorSpeed);
		}
	}

	public void StopFightVideo()
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.loader_FightShow.visible = false;
		}
	}

	private void ShowFightVideoLoader(Player source, int status)
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.loader_FightShow.visible = true;
		}
	}

	private void CloseFightVideo(Player source, int status)
	{
		StopFightVideo();
	}

	private void StopAndDisposeFightShow()
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.loader_FightShow.visible = false;
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uIFightWindow.loader_FightShow);
		}
	}

	private void InitCardData()
	{
	}

	private void Card_AddEvent()
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.com_Card.btn_FinishPkCard.onClick.Add(FinishPKCard);
			Stage.inst.onTouchBegin.Add(ZoomCard);
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.LookCard.AddListener(LookCardByZoom);
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.ZoomOutCard.AddListener(ZoomOutCard);
		}
	}

	private void Card_RemoveEvent()
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.com_Card.btn_FinishPkCard.onClick.Remove(FinishPKCard);
			Stage.inst.onTouchBegin.Remove(ZoomCard);
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.LookCard.RemoveListener(LookCardByZoom);
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.ZoomOutCard.RemoveListener(ZoomOutCard);
		}
	}

	private void FinishPKCard()
	{
		GComponent gComponent = base.contentPane;
		UIFightWindow win = gComponent as UIFightWindow;
		if (win == null)
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
		{
			RecycleCard();
			win.com_Card.btn_FinishPkCard.visible = false;
			win.com_Card.com_CostLabel.visible = false;
			SimpleSingletonProvider<UIManager>.inst.guide.HideDialog();
			SimpleSingletonProvider<GameLogicManager>.inst.guide.GuidanceBattleCardFinish(attackerData, defenderData);
		}
		else
		{
			if (!IsCurBattlePlayer() || useBattleCardSn == 0L)
			{
				return;
			}
			win.com_Card.btn_FinishPkCard.onClick.Retain();
			RecycleCard();
			RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
			if (roomInfo != null && roomInfo.MapType == 10)
			{
				long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
				SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestBattleUseCardC2S(playerID, useBattleCardSn, 0).Forget();
				win.com_Card.btn_FinishPkCard.visible = false;
				win.com_Card.com_CostLabel.visible = false;
				win.com_Card.btn_FinishPkCard.onClick.Release();
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.fight.RequestBattleUseCardC2S(useBattleCardSn, 0).OnFinishedOnly.AddOnce(delegate
				{
					win.com_Card.btn_FinishPkCard.visible = false;
					win.com_Card.com_CostLabel.visible = false;
					win.com_Card.btn_FinishPkCard.onClick.Release();
					useBattleCardSn = 0L;
				});
			}
		}
	}

	private void RecycleCard()
	{
		CardPool.ReleaseAllCard(cardItemList);
	}

	private void DragStartEvent(EventContext context)
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.com_Card.showHotZone.selectedIndex = 1;
			if (context.sender is UIHandCard_Button_Card uIHandCard_Button_Card)
			{
				uIHandCard_Button_Card.DisplayCard();
			}
		}
	}

	private async void DragEndEvent(EventContext context)
	{
		GComponent gComponent = base.contentPane;
		UIFightWindow win = gComponent as UIFightWindow;
		if (win == null)
		{
			return;
		}
		win.com_Card.showHotZone.selectedIndex = 0;
		EventDispatcher sender = context.sender;
		UIHandCard_Button_Card item = sender as UIHandCard_Button_Card;
		if (item == null)
		{
			return;
		}
		Vector2 vector = GRoot.inst.GlobalToLocal(win.com_Card.com_CardHot.LocalToGlobal(Vector2.zero));
		Vector2 vector2 = GRoot.inst.GlobalToLocal(item.LocalToGlobal(Vector2.zero));
		if (vector2.x > vector.x && vector2.y > vector.y && vector2.x < vector.x + win.com_Card.com_CardHot.hotZone.actualWidth && vector2.y < vector.y + win.com_Card.com_CardHot.hotZone.actualHeight)
		{
			if (IsSelf(attackerData.player.Id))
			{
				int cost = GetCost(attackerData.player.Id, item._config.Id, item.CardData);
				if (cost > AttackerResidueCost)
				{
					RendererCard(GetVailCard());
					SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(string.Format(10018.GetLocal(UIStringType.Message), attackCardCost));
					return;
				}
				attackCardCost += cost;
			}
			if (IsSelf(defenderData.player.Id))
			{
				int cost2 = GetCost(defenderData.player.Id, item._config.Id, item.CardData);
				if (cost2 > DefenderResidueCost)
				{
					RendererCard(GetVailCard());
					SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(string.Format(10018.GetLocal(UIStringType.Message), defendCardCost));
					return;
				}
				defendCardCost += cost2;
			}
			if (IsCurBattlePlayer() && useBattleCardSn != 0L)
			{
				win.com_Card.btn_FinishPkCard.onClick.Retain();
				SetCardUseless();
				item.effectOutline.displayObject.gameObject.SetActive(value: true);
				if (StaticConfigure.Effect.InfoDict.TryGetValue(23, out var value))
				{
					await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, item.effectOutline, 80f);
				}
				item.touchable = false;
				if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(100))
				{
					return;
				}
				item.com_Card.visible = false;
				if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(400))
				{
					return;
				}
				item.UpdateUsableState(enableUse: false).Forget();
				if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
				{
					useBattleCardSn = 0L;
					item.visible = false;
					item.com_Card.visible = true;
					CardPool.Release(item);
					cardItemList.Remove(item);
					SimpleSingletonProvider<GameLogicManager>.inst.guide.GuidanceBattleCardUse(attackerData, defenderData, item._config.Id);
					win.com_Card.btn_FinishPkCard.onClick.Release();
					return;
				}
				RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
				if (roomInfo != null && roomInfo.MapType == 10)
				{
					CardPool.Release(item);
					cardItemList.Remove(item);
					item.visible = false;
					item.com_Card.visible = true;
					long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
					SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestBattleUseCardC2S(playerID, useBattleCardSn, item.CardData.Guid).Forget();
					win.com_Card.btn_FinishPkCard.onClick.Release();
					return;
				}
				SimpleSingletonProvider<GameLogicManager>.inst.fight.RequestBattleUseCardC2S(useBattleCardSn, item.CardData.Guid).OnFinishedOnly.AddOnce(delegate
				{
					item.visible = false;
					item.com_Card.visible = true;
					CardPool.Release(item);
					cardItemList.Remove(item);
					win.com_Card.btn_FinishPkCard.onClick.Release();
					useBattleCardSn = 0L;
				});
				LookCardByZoom(isZoom: false);
				return;
			}
		}
		RendererCard(GetVailCard());
	}

	private void DragMoveEvent(EventContext context)
	{
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
		if (GRoot.inst.touchTarget is UIFight_Card)
		{
			LookCardByZoom(isZoom: false);
		}
	}

	private List<HandCardData> GetVailCard()
	{
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		List<HandCardData> handCards = selfPlayerData.cardContainer._HandCards;
		EffectType effectType = ((attackerData.player.Id == selfPlayerData.player.Id) ? EffectType.Attack : EffectType.Defense);
		List<HandCardData> list = new List<HandCardData>();
		foreach (HandCardData item in handCards)
		{
			if (item.Config.EffectType == effectType)
			{
				list.Add(item);
			}
		}
		return list;
	}

	private bool IsCurBattlePlayer()
	{
		if (!IsSelf(attackerData.player.Id))
		{
			return IsSelf(defenderData.player.Id);
		}
		return true;
	}

	private void SetCardUseless()
	{
		for (int i = 0; i < cardItemList.Count; i++)
		{
			cardItemList[i].UpdateUsableState(enableUse: false).Forget();
		}
	}

	private void RendererCard(List<HandCardData> showCards)
	{
		if (!(base.contentPane is UIFightWindow uIFightWindow) || showCards == null)
		{
			return;
		}
		List<int> usableCards = showCards.Select((HandCardData x) => x.CardId).ToList();
		int num = SimpleSingletonProvider<GameLogicManager>.inst.action.TryGetCardSuggestCardId(usableCards);
		List<UIHandCard_Button_Card> list = new List<UIHandCard_Button_Card>();
		int num2 = Mathf.Min(showCards.Count, 11);
		for (int num3 = 0; num3 < num2; num3++)
		{
			HandCardData handCardData = showCards[num3];
			UIHandCard_Button_Card uIHandCard_Button_Card = GetCardItem() ?? CardPool.Get(uIFightWindow.com_Card);
			list.Add(uIHandCard_Button_Card);
			if (num == handCardData.CardId)
			{
				num = -1;
				uIHandCard_Button_Card.ShowSuggestCard(isSuggest: true);
			}
			else
			{
				uIHandCard_Button_Card.ShowSuggestCard(isSuggest: false);
			}
			CardPool.SetDragEvent(uIHandCard_Button_Card, DragStartEvent, DragEndEvent, DragMoveEvent);
			uIHandCard_Button_Card.InitData_BattleCard(num3, handCardData);
			uIHandCard_Button_Card.UpdateCustomRotation(CardHelper.CalculateRotation(num3, num2));
			uIHandCard_Button_Card.UpdateCustomPosition(CardHelper.CalculatePosition(centerPoint, CenterRadius, uIHandCard_Button_Card.CustomRotation));
			uIHandCard_Button_Card.PlayTransition(uIFightWindow.width, uIFightWindow.height, uIHandCard_Button_Card.IsDistribute());
		}
		for (int num4 = 0; num4 < cardItemList.Count; num4++)
		{
			CardPool.Release(cardItemList[num4]);
		}
		cardItemList.AddRange(list);
		RefreshCardUsability(attackerData.player.Id, AttackerResidueCost);
		RefreshCardUsability(defenderData.player.Id, DefenderResidueCost);
	}

	private UIHandCard_Button_Card GetCardItem()
	{
		if (cardItemList.Count > 0)
		{
			UIHandCard_Button_Card result = cardItemList[0];
			cardItemList.RemoveAt(0);
			return result;
		}
		return null;
	}

	public void InitPKCard(long playerId)
	{
		if (!(base.contentPane is UIFightWindow uIFightWindow) || uIFightWindow.step.selectedIndex == 3)
		{
			return;
		}
		uIFightWindow.step.selectedIndex = 3;
		bool flag = IsSelf(attackerData.player.Id);
		bool flag2 = IsSelf(defenderData.player.Id);
		int selectedIndex = (flag ? 1 : (flag2 ? 2 : 0));
		uIFightWindow.com_Card.com_CostLabel.list_Cost.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UICom_CostPoint uICom_CostPoint)
			{
				uICom_CostPoint.CpTyper.selectedIndex = selectedIndex;
				uICom_CostPoint.graph_Effect.visible = false;
				SimpleSingletonProvider<GameObjectManager>.inst.Stop(uICom_CostPoint.graph_Effect);
			}
		};
		uIFightWindow.com_Card.com_CostLabel.list_Cost.numItems = (flag ? AttackerResidueCost : DefenderResidueCost);
		uIFightWindow.com_Card.com_CostLabel.list_Cost.ResizeToFit();
	}

	public void RefreshPKCard(long _playerId, long _actionSn)
	{
		GComponent gComponent = base.contentPane;
		UIFightWindow win = gComponent as UIFightWindow;
		if (win == null || !IsSelf(_playerId))
		{
			return;
		}
		win.step.selectedIndex = 3;
		useBattleCardSn = _actionSn;
		if (OperationTimer.GetOperateTimer(useBattleCardSn) == null)
		{
			RendererCard(GetVailCard());
			OperationTimer.ActionDownTime(useBattleCardSn, 5035, delegate
			{
				win.com_Card.btn_FinishPkCard.onClick.Call();
			}, null, null, operateCard: false, showTimerToPlayer: false);
		}
	}

	public void RefreshCardWinData()
	{
		if (fightData != null)
		{
			BattleRole attackerInfo = fightData.attackerInfo;
			if (attackerInfo != null)
			{
				CalculateATK(attackerInfo.PlayerId, attackerInfo.MinAtk, attackerInfo.MaxAtk, attackerInfo.UseCards);
			}
			BattleRole defenderInfo = fightData.defenderInfo;
			if (defenderInfo != null)
			{
				CalculateDEF(defenderInfo.PlayerId, defenderInfo.MinDef, defenderInfo.MaxDef, defenderInfo.UseCards);
			}
		}
	}

	private void CalculateATK(long playerId, int minAtk, int maxAtk, RepeatedField<int> cards = null)
	{
		if (base.contentPane is UIFightWindow uIFightWindow && IsSelf(attackerData.player.Id))
		{
			attackCardCost = GetCardCost(playerId, cards);
			uIFightWindow.com_Card.com_CostLabel.txt_Cost.SetVar("curCost", $"{AttackerResidueCost}").FlushVars();
			RefreshCostUIState(attackCardCost);
			RefreshCardUsability(attackerData.player.Id, AttackerResidueCost);
		}
		BattleSceneController.inst.directorManager.attacker._UI.RefreshAttackAttr(attackerData.Property.HP.Value, minAtk, maxAtk);
	}

	private void CalculateDEF(long playerId, int minDef, int maxDef, RepeatedField<int> cards = null)
	{
		if (base.contentPane is UIFightWindow uIFightWindow && IsSelf(defenderData.player.Id))
		{
			defendCardCost = GetCardCost(playerId, cards);
			uIFightWindow.com_Card.com_CostLabel.txt_Cost.SetVar("curCost", $"{DefenderResidueCost}").FlushVars();
			RefreshCostUIState(defendCardCost);
			RefreshCardUsability(defenderData.player.Id, DefenderResidueCost);
		}
		BattleSceneController.inst.directorManager.victim._UI.RefreshDefendAttr(defenderData.Property.HP.Value, minDef, maxDef);
	}

	private int GetCardCost(long _playerId, RepeatedField<int> cards = null)
	{
		if (cards == null || cards.Count == 0)
		{
			return 0;
		}
		int num = 0;
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (playerDataById == null)
		{
			Debug.LogError($"GetCardCost 未找到玩家数据 PlayerId{_playerId}");
			return 0;
		}
		for (int i = 0; i < cards.Count; i++)
		{
			int num2 = cards[i];
			if (!playerDataById.cardContainer._HandCardData.TryGetValue(num2, out var value))
			{
				Debug.LogError($"GetCardCost 未找到卡牌数据 CardGuid{num2}");
			}
			else
			{
				num += GetCost(_playerId, value.CardId, value);
			}
		}
		return num;
	}

	private int GetCost(long playerId, int cardId, HandCardData handCardData)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue(cardId, out var value))
		{
			return value.GetCostValue(playerId, handCardData);
		}
		return 0;
	}

	private void RefreshCardUsability(long playerId, int residueCost)
	{
		if (!IsSelf(playerId))
		{
			return;
		}
		for (int i = 0; i < cardItemList.Count; i++)
		{
			if (GetCost(playerId, cardItemList[i]._config.Id, cardItemList[i].CardData) > residueCost)
			{
				cardItemList[i].UpdateUsableState(enableUse: false).Forget();
			}
		}
	}

	private void RefreshCostUIState(int costPoint)
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			GList list_Cost = uIFightWindow.com_Card.com_CostLabel.list_Cost;
			for (int i = 0; i < costPoint && i < list_Cost.numItems; i++)
			{
				ShowCost((UICom_CostPoint)list_Cost.GetChildAt(i));
			}
		}
	}

	private async void ShowCost(UICom_CostPoint costItem)
	{
		if (!costItem.graph_Effect.visible)
		{
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(53.GetEffectDataConfigure().EffectName, costItem.graph_Effect, 15f);
		}
		costItem.CpTyper.selectedIndex = 0;
	}

	public async void GuideReadyCard()
	{
		if (base.contentPane is UIFightWindow uIFightWindow && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
		{
			Vector2 pt = uIFightWindow.com_Card.btn_FinishPkCard.LocalToGlobal(Vector2.zero);
			pt = GRoot.inst.GlobalToLocal(pt);
			await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(pt, uIFightWindow.com_Card.btn_FinishPkCard.width, uIFightWindow.com_Card.btn_FinishPkCard.height, _needTransparentMask: false, isRect: false);
		}
	}

	public async UniTask TutorialRefreshPKCard(long playerId, int sn, int tutorialCost)
	{
		if (!(base.contentPane is UIFightWindow uIFightWindow))
		{
			return;
		}
		uIFightWindow.step.selectedIndex = 3;
		uIFightWindow.playerState.selectedIndex = 1;
		uIFightWindow.com_Card.btn_FinishPkCard.visible = false;
		uIFightWindow.com_Card.com_CostLabel.visible = false;
		HandCardData tutorialCard = GetTutorialCard(playerId, tutorialCost);
		UIHandCard_Button_Card selectCardItem = null;
		if (tutorialCard != null)
		{
			RendererCard(new List<HandCardData> { tutorialCard });
			selectCardItem = cardItemList.GetSafeByIndex(0);
		}
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001)
		{
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(10007);
		}
		if (selectCardItem != null)
		{
			selectCardItem.DoTutorialUseCard(delegate
			{
				TutorialUseCard(playerId, sn, selectCardItem).Forget();
			}).Forget();
		}
		else
		{
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestBattleUseCardC2S(playerId, sn, 0).Forget();
		}
	}

	private HandCardData GetTutorialCard(long playerId, int tutorialCost)
	{
		List<HandCardData> handCards = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).cardContainer._HandCards;
		EffectType effectType = ((attackerData.player.Id == playerId) ? EffectType.Attack : EffectType.Defense);
		foreach (HandCardData item in handCards)
		{
			if (item.Config.EffectType == effectType && item.Config.Cost <= tutorialCost)
			{
				return item;
			}
		}
		return null;
	}

	private async UniTask TutorialUseCard(long playerId, long sn, UIHandCard_Button_Card selectCardItem)
	{
		SetCardUseless();
		selectCardItem.effectOutline.displayObject.gameObject.SetActive(value: true);
		if (StaticConfigure.Effect.InfoDict.TryGetValue(23, out var value))
		{
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, selectCardItem.effectOutline, 80f);
		}
		selectCardItem.touchable = false;
		if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(100)))
		{
			selectCardItem.com_Card.visible = false;
			if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(400)))
			{
				selectCardItem.UpdateUsableState(enableUse: false).Forget();
				CardPool.Release(selectCardItem);
				cardItemList.Remove(selectCardItem);
				selectCardItem.visible = false;
				selectCardItem.com_Card.visible = true;
				SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestBattleUseCardC2S(playerId, sn, selectCardItem.CardData.Guid).Forget();
				SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestBattleUseCardC2S(playerId, sn, 0).Forget();
			}
		}
	}

	public void OpenChallengeWin(long attackerPlayerId, long defenderPlayerId, long _actionSn)
	{
		GComponent gComponent = base.contentPane;
		UIFightWindow win = gComponent as UIFightWindow;
		if (win == null)
		{
			return;
		}
		challengeActionSn = _actionSn;
		long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		_AttackerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(attackerPlayerId);
		_DefenderData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(defenderPlayerId);
		RefreshPlayerInfo();
		win.com_LaunchPK.country.selectedIndex = GameSettings.COUNTRY;
		win.com_LaunchPK.txt_RoleName.text = attackerData.player.GetNick();
		win.com_LaunchPK.playerState.selectedIndex = ((attackerPlayerId != playerID) ? ((defenderPlayerId == playerID) ? 1 : 2) : 0);
		win.playerState.selectedIndex = ((attackerPlayerId != playerID) ? ((defenderPlayerId == playerID) ? 1 : 2) : 0);
		win.com_LaunchPK.loader_Icon.url = ((StaticGlobalData.GAME_FIGHT_BUST_Gold_THRESHOLD > defenderData.Property.gold.Value) ? defenderData.player.standingPainting.GetBust()[0] : defenderData.player.standingPainting.GetBust()[1]);
		win.step.selectedIndex = 1;
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(attackerData.CharacterInst, willMove: false);
		if (IsSelf(attackerPlayerId))
		{
			OperationTimer.ActionDownTime(challengeActionSn, 5047, delegate
			{
				win.com_LaunchPK.btn_Leave.onClick.Call();
			});
		}
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapType == 10)
		{
			win.com_LaunchPK.btn_Leave.touchable = false;
			win.com_LaunchPK.btn_Leave.grayed = true;
			win.com_LaunchPK.btn_ShowWin.visible = false;
		}
	}

	public async void GuideFightPK()
	{
		if (base.contentPane is UIFightWindow uIFightWindow && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
		{
			Vector2 pt = uIFightWindow.com_LaunchPK.btn_PK.LocalToGlobal(Vector2.zero);
			pt = GRoot.inst.GlobalToLocal(pt);
			await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(pt, uIFightWindow.com_LaunchPK.btn_PK.width, uIFightWindow.com_LaunchPK.btn_PK.height, _needTransparentMask: false, isRect: false);
			base.touchable = true;
		}
	}

	private void RefreshPlayerInfo()
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.com_LaunchPK.com_P1.visible = false;
			uIFightWindow.com_LaunchPK.com_P2.visible = false;
			uIFightWindow.com_LaunchPK.com_P3.visible = false;
			uIFightWindow.com_LaunchPK.com_P4.visible = false;
			uIFightWindow.com_LaunchPK.group_Attr.visible = false;
			if (defenderData.characterType == CharacterType.Hero)
			{
				SetDefenderBGColor(defenderData.player.Slot).Forget();
				RefreshPlayerInfo((UICom_PlayerInfo)uIFightWindow.com_LaunchPK.com_P1, 0);
				RefreshPlayerInfo((UICom_PlayerInfo)uIFightWindow.com_LaunchPK.com_P2, 1);
				RefreshPlayerInfo((UICom_PlayerInfo)uIFightWindow.com_LaunchPK.com_P3, 2);
				RefreshPlayerInfo((UICom_PlayerInfo)uIFightWindow.com_LaunchPK.com_P4, 3);
			}
			else
			{
				SetDefenderBGColor(4).Forget();
				uIFightWindow.com_LaunchPK.txt_HP.text = defenderData.Property.HP.Value.ToString();
				uIFightWindow.com_LaunchPK.txt_ATK.text = defenderData.Property.ATK.Value.ToString();
				uIFightWindow.com_LaunchPK.txt_DEF.text = defenderData.Property.DEF.Value.ToString();
				GComponent com_Counter = uIFightWindow.com_LaunchPK.com_Counter;
				bool flag = (uIFightWindow.com_LaunchPK.txt_Counter.visible = defenderData.Property.Counter.Value);
				com_Counter.visible = flag;
				uIFightWindow.com_LaunchPK.group_Attr.visible = true;
			}
		}
	}

	private void RefreshPlayerInfo(UICom_PlayerInfo component, int index)
	{
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		if (playerDatas.Count > index && playerDatas[index].characterType == CharacterType.Hero)
		{
			component.RefreshData(playerDatas[index]);
			component.visible = true;
		}
	}

	private void EnableThrough()
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.com_LaunchPK.btn_ShowWin.onClick.Retain();
			uIFightWindow.com_LaunchPK.opaque = false;
			uIFightWindow.com_LaunchPK.btn_ShowWin.onClick.Release();
		}
	}

	private void CloseThrough()
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.com_LaunchPK.btn_HideWin.onClick.Retain();
			uIFightWindow.com_LaunchPK.opaque = true;
			uIFightWindow.com_LaunchPK.btn_HideWin.onClick.Release();
		}
	}

	private void SureLaunch()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.guide.SureGuidanceFight(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID());
		}
		else if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 10)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestAskBattleC2S(challengeActionSn, is_battle: true, isBackFight: false).Forget();
			challengeActionSn = 0L;
		}
		else
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.replay.Session.IsReplay)
			{
				return;
			}
			GComponent gComponent = base.contentPane;
			UIFightWindow win = gComponent as UIFightWindow;
			if (win != null)
			{
				win.com_LaunchPK.btn_PK.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.fight.RequestAskBattleC2S(challengeActionSn, is_battle: true).OnFinishedOnly.AddOnce(delegate
				{
					challengeActionSn = 0L;
					win.com_LaunchPK.btn_PK.onClick.Release();
				});
			}
		}
	}

	private void RequestClosePKWin()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2 || SimpleSingletonProvider<GameLogicManager>.inst.replay.Session.IsReplay)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UIFightWindow win = gComponent as UIFightWindow;
		if (win != null)
		{
			win.com_LaunchPK.btn_Leave.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.fight.RequestAskBattleC2S(challengeActionSn, is_battle: false).OnFinishedOnly.AddOnce(delegate
			{
				challengeActionSn = 0L;
				win.com_LaunchPK.btn_Leave.onClick.Release();
			});
		}
	}

	private async UniTask SetDefenderBGColor(int index)
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UIFightWindow win)
		{
			int num = Mathf.Min(index, _fightBGMaterialKeys.Length - 1);
			Material material = await SimpleSingletonProvider<Core.MaterialManager>.inst.GetMaterial(_fightBGMaterialKeys[num]);
			win.com_LaunchPK.img_Defender.visible = true;
			win.com_LaunchPK.img_Defender.material = material;
		}
	}

	private void Dice_AddEvent()
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.btn_Attack.onClick.Add(RequestConditionDice);
			uIFightWindow.btn_Dodge.onClick.Add((EventCallback0)delegate
			{
				ChooseActive(result: true);
			});
			uIFightWindow.btn_Defend.onClick.Add((EventCallback0)delegate
			{
				ChooseActive(result: false);
			});
			SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.dodgeShow.AddListener(RefreshDodgeInfo);
		}
	}

	private void Dice_RemoveEvent()
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.btn_Attack.onClick.Remove(RequestConditionDice);
			uIFightWindow.btn_Dodge.onClick.Remove((EventCallback0)delegate
			{
				ChooseActive(result: true);
			});
			uIFightWindow.btn_Defend.onClick.Remove((EventCallback0)delegate
			{
				ChooseActive(result: false);
			});
			SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.dodgeShow.RemoveListener(RefreshDodgeInfo);
		}
	}

	private void RequestConditionDice()
	{
		GComponent gComponent = base.contentPane;
		UIFightWindow win = gComponent as UIFightWindow;
		if (win != null && attackThrowDiceSn != 0L)
		{
			win.btn_Attack.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.fight.RequestBattleThrowDiceC2S(attackThrowDiceSn).OnFinishedOnly.AddOnce(delegate
			{
				attackThrowDiceSn = 0L;
				win.btn_Attack.onClick.Release();
			});
		}
	}

	private void ChooseActive(bool result)
	{
		if (result && dodgeNoAvailable)
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10003, 2f);
			return;
		}
		GComponent gComponent = base.contentPane;
		UIFightWindow win = gComponent as UIFightWindow;
		if (win == null)
		{
			return;
		}
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapType == 10)
		{
			win.btn_Dodge.visible = false;
			win.btn_Defend.visible = false;
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestBattleChoiceC2S(dodgeChoice, result).Forget();
		}
		else if (dodgeChoice != 0L)
		{
			win.btn_Dodge.onClick.Retain();
			win.btn_Defend.onClick.Retain();
			win.btn_Dodge.visible = false;
			win.btn_Defend.visible = false;
			SimpleSingletonProvider<GameLogicManager>.inst.fight.RequestBattleChoiceC2S(dodgeChoice, result).OnFinishedOnly.AddOnce(delegate
			{
				dodgeChoice = 0L;
				win.btn_Dodge.onClick.Release();
				win.btn_Defend.onClick.Release();
			});
		}
	}

	private void OnCompleteAttackBattleThrow()
	{
		if (IsSelf(attackerData.CharacterInst.player.Id) && base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.btn_Attack.onClick.Call();
		}
	}

	public void RefreshThrowDice(long _playerId, long _actionSn)
	{
		RecycleCard();
		attackThrowDiceSn = _actionSn;
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			if (attackerData.characterType == CharacterType.Hero)
			{
				uIFightWindow.txt_DiceTip.SetVar("color", ColorUtility.ToHtmlStringRGB(GameConfig.slotColor[attackerData.player.Slot])).SetVar("playerName", attackerData.player.GetNick()).FlushVars();
			}
			else
			{
				uIFightWindow.txt_DiceTip.SetVar("color", ColorUtility.ToHtmlStringRGB(GameConfig.slotColor[4])).SetVar("playerName", CharacterHandle.GetCharacterName(attackerData.player.characterConfig.Id, attackerData.player.characterConfig.CharacterType)).FlushVars();
			}
			uIFightWindow.step.selectedIndex = 4;
			uIFightWindow.com_value.visible = false;
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_playerId))
			{
				RequestConditionDice();
			}
		}
	}

	private void OnCompleteDefendBattleThrow()
	{
		if (IsSelf(defenderData.CharacterInst.player.Id) && base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.btn_Defend.onClick.Call();
		}
	}

	public void RefreshDefendReadyChoice(long _Sn, long _playerId, bool _NoDodge)
	{
		dodgeChoice = _Sn;
		dodgeNoAvailable = _NoDodge;
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.btn_Dodge.available.selectedIndex = (dodgeNoAvailable ? 1 : 0);
			if (defenderData.characterType == CharacterType.Hero)
			{
				uIFightWindow.txt_ChoiceTip.SetVar("color", ColorUtility.ToHtmlStringRGB(GameConfig.slotColor[defenderData.player.Slot])).SetVar("playerName", defenderData.player.GetNick()).FlushVars();
			}
			else
			{
				uIFightWindow.txt_ChoiceTip.SetVar("color", ColorUtility.ToHtmlStringRGB(GameConfig.slotColor[4])).SetVar("playerName", CharacterHandle.GetCharacterName(defenderData.player.characterConfig.Id, defenderData.player.characterConfig.CharacterType)).FlushVars();
			}
			uIFightWindow.group__audienceChoice.visible = true;
			uIFightWindow.step.selectedIndex = 5;
			uIFightWindow.com_value.visible = false;
			if (IsSelf(_playerId))
			{
				UIFight_Button_Dodge btn_Dodge = uIFightWindow.btn_Dodge;
				bool flag = (uIFightWindow.btn_Defend.visible = true);
				btn_Dodge.visible = flag;
				UIFight_Button_Dodge btn_Dodge2 = uIFightWindow.btn_Dodge;
				flag = (uIFightWindow.btn_Defend.touchable = true);
				btn_Dodge2.touchable = flag;
				OperationTimer.ActionDownTime(dodgeChoice, 5039, OnCompleteDefendBattleThrow, null, null, operateCard: false, showTimerToPlayer: false);
			}
			else
			{
				UIFight_Button_Dodge btn_Dodge3 = uIFightWindow.btn_Dodge;
				bool flag = (uIFightWindow.btn_Defend.visible = false);
				btn_Dodge3.visible = flag;
			}
		}
	}

	private void RefreshDodgeInfo(int point)
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			string value = ((point == StaticGlobalData.GAME_JUDGE_DICE_LIMIT) ? "=" : ">");
			uIFightWindow.btn_Dodge.txt_Point.SetVar("sign", value).SetVar("point", point.ToString()).FlushVars();
		}
	}

	public async void ShowChoiceResult(bool _isDodge)
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UIFightWindow win && defenderData != null)
		{
			win.group__audienceChoice.visible = false;
			if (defenderData.characterType == CharacterType.Hero)
			{
				win.txt_ChoiceResult.SetVar("color", ColorUtility.ToHtmlStringRGB(GameConfig.slotColor[defenderData.player.Slot])).SetVar("playerName", defenderData.player.GetNick()).FlushVars();
			}
			else
			{
				win.txt_ChoiceResult.SetVar("color", ColorUtility.ToHtmlStringRGB(GameConfig.slotColor[4])).SetVar("playerName", CharacterHandle.GetCharacterName(defenderData.player.characterConfig.Id, defenderData.player.characterConfig.CharacterType)).FlushVars();
			}
			win.txt_Defend.visible = !_isDodge;
			win.txt_Dodge.visible = _isDodge;
			win.group__audienceChoiceResult.visible = true;
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
			win.group__audienceChoiceResult.visible = false;
		}
	}

	public void GuideFightResult(int attackPoint, int defendPoint, bool isDodge)
	{
		if (base.contentPane is UIFightWindow uIFightWindow && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
		{
			uIFightWindow.step.selectedIndex = 0;
			SimpleSingletonProvider<GameLogicManager>.inst.guide.GuidanceBattleAttackDice(attackerData, defenderData, attackPoint, defendPoint, isDodge);
		}
	}

	public async UniTask TutorialBattleChoice(long attackerId, long defenderId, bool isBackFight)
	{
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UIFightWindow win))
		{
			return;
		}
		RefreshDefendReadyChoice(UIDGenerator.NextUID(), defenderId, _NoDodge: false);
		UIFight_Button_Dodge btn_Dodge = win.btn_Dodge;
		bool flag = (win.btn_Defend.touchable = false);
		btn_Dodge.touchable = flag;
		int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
		MapGimmickManager mapGimmickManager = SimpleSingletonProvider<LandManager>.inst.MapGimmickManager;
		if (mapGimmickManager is MapGimmickManager_Tutorial1002 gimmick1002)
		{
			if (defenderId == gimmick1002.NPC.player.Id && attackerId == gimmick1002.BossData.player.Id)
			{
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500);
				switch (round)
				{
				case 2:
					await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowSystemInfo(150);
					await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
					win.btn_Defend.FireClick(downEffect: true, clickCall: true);
					return;
				case 3:
					if (isBackFight)
					{
						win.btn_Defend.FireClick(downEffect: true, clickCall: true);
						return;
					}
					await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(20007);
					win.btn_Dodge.FireClick(downEffect: true, clickCall: true);
					return;
				}
			}
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(attackerId);
			if (defenderId == gimmick1002.NPC.player.Id && playerDataById.player.Hero.HeroId == 1005 && round == 3 && !isBackFight)
			{
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500);
				win.btn_Dodge.FireClick(downEffect: true, clickCall: true);
				return;
			}
		}
		else
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
		}
		switch (round)
		{
		case 2:
			win.btn_Defend.FireClick(downEffect: true, clickCall: true);
			break;
		case 3:
			win.btn_Dodge.FireClick(downEffect: true, clickCall: true);
			break;
		}
	}

	public async UniTask ReadyFight(long attackerPlayerId, long defenderPlayerId)
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UIFightWindow win)
		{
			win.step.selectedIndex = 0;
			win.btn_Dodge.visible = true;
			win.btn_Defend.visible = true;
			win.com_Card.btn_FinishPkCard.visible = true;
			win.com_Card.com_CostLabel.visible = true;
			win.loader_FightShow.visible = false;
			attackCardCost = 0;
			defendCardCost = 0;
			win.step.selectedIndex = 2;
			_AttackerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(attackerPlayerId);
			_DefenderData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(defenderPlayerId);
			long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
			win.playerState.selectedIndex = ((attackerPlayerId != playerID) ? ((defenderPlayerId == playerID) ? 1 : 2) : 0);
			SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: true, UIPanelType.None);
			SimpleSingletonProvider<AudioManager>.inst.SendEvent(24, Stage.inst.gameObject);
			fightStartTsc = new UniTaskCompletionSource();
			win.com_Show.loader_Movie.FullScreen();
			string videoKey = 3.GetVideoKey();
			await SimpleSingletonProvider<CriMovieManager>.inst.Play(videoKey, win.com_Show.loader_Movie, null, FightFinish, FightCuePoint);
			await fightStartTsc.Task;
			win.com_Show.loader_Movie.visible = false;
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(win.com_Show.loader_Movie);
			SimpleSingletonProvider<GameLogicManager>.inst.campaign.campaignData?.TriggerFirstFight();
		}
	}

	private async void FightCuePoint(EventPoint point, Player player)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (point.type == 0)
		{
			player.Pause(true);
			BattleSceneController.inst.directorManager.ActiveBattlePlatform(attackerData.player.Id, defenderData.player.Id);
			BattleSceneController.inst.directorManager.battlePlatform.SetCurrentFightBackGroundBlurBg(blurTex);
			await BattleSceneController.inst.directorManager.SyncBuffEffect();
			int value = attackerData.Property.ATK.Value;
			CalculateATK(attackerData.player.Id, value, value);
			int value2 = defenderData.Property.DEF.Value;
			CalculateDEF(defenderData.player.Id, value2, value2);
			player.Pause(false);
		}
		else if (point.type == 1)
		{
			BattleSceneController.inst.directorManager.ActiveAttackerAppear();
		}
	}

	private void FightFinish(Player source, int status)
	{
		fightStartTsc?.TrySetResult();
	}

	public void ShowResultTip(bool isDead)
	{
		if (base.contentPane is UIFightWindow uIFightWindow && defenderData != null)
		{
			if (defenderData.characterType == CharacterType.Hero)
			{
				uIFightWindow.txt_ResultTip.SetVar("color", ColorUtility.ToHtmlStringRGB(GameConfig.slotColor[defenderData.player.Slot])).SetVar("playerName", defenderData.player.GetNick()).FlushVars();
			}
			else
			{
				uIFightWindow.txt_ResultTip.SetVar("color", ColorUtility.ToHtmlStringRGB(GameConfig.slotColor[4])).SetVar("playerName", CharacterHandle.GetCharacterName(defenderData.player.characterConfig.Id, defenderData.player.characterConfig.CharacterType)).FlushVars();
			}
			uIFightWindow.txt_Failure.visible = isDead;
			uIFightWindow.txt_Success.visible = !isDead;
			uIFightWindow.step.selectedIndex = 6;
		}
	}

	public async UniTask<bool> FinishFight()
	{
		GComponent gComponent = base.contentPane;
		UIFightWindow win = gComponent as UIFightWindow;
		if (win == null)
		{
			return true;
		}
		win.step.selectedIndex = 2;
		win.com_Show.fightOver.Play();
		return !(await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntil(() => !win.com_Show.fightOver.playing));
	}

	public async UniTask CloseFightWin()
	{
		if (!base.isShowing)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UIFightWindow win = gComponent as UIFightWindow;
		if (win != null)
		{
			win.com_Show.fightOver.PlayReverse();
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntil(() => !win.com_Show.fightOver.playing);
			Hide();
		}
	}

	private void ShowCard(UICom_Card card, PointInfo pointInfo)
	{
		long playerId = pointInfo.PlayerId;
		Dictionary<int, Card> cardActions = SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions;
		int cardId = pointInfo.CardId;
		if (cardActions.TryGetValue(cardId, out var value))
		{
			CardInfoConfigure cardInfoConfigure = value.Config;
			string content = "";
			string cardIndex = "";
			string cardTips = "";
			string local = cardInfoConfigure.NameID.GetLocal(UIStringType.Card);
			int costValue = value.GetCostValue(playerId);
			CommonUIManager.RendererCardInRoom(playerId, card, cardInfoConfigure.GetBattlePlayerCardView(playerId), local, content, cardIndex, cardTips, costValue, cardInfoConfigure.CardType, cardInfoConfigure.CardTargetType);
		}
	}

	private void RefreshCardList(GList list, List<PointInfo> pointInfos, bool isAtk, float intervalTime)
	{
		int num = 7;
		float min = -100f;
		float max = 0f;
		int num2 = pointInfos.Count - 1;
		int num3 = (list.numItems = Mathf.Clamp(num2, 0, num));
		if (num3 <= 0)
		{
			return;
		}
		float value = (list.width - list.GetChildAt(0).width * (float)num3) / (float)num3;
		value = Mathf.Clamp(value, min, max);
		list.columnGap = Mathf.RoundToInt(value);
		int audioEventID = (isAtk ? 38 : 39);
		for (int i = 0; i < num3; i++)
		{
			int index = (isAtk ? i : (num3 - 1 - i));
			UIFight_Com_Value_Card card_value = list.GetChildAt(index) as UIFight_Com_Value_Card;
			if (card_value == null)
			{
				continue;
			}
			card_value.alpha = 0f;
			card_value.Card_Cut_in.Play(1, intervalTime * (float)i, delegate
			{
				card_value.alpha = 1f;
				Stage.inst.PlayOneShotSound(audioEventID);
			}, delegate
			{
			});
			int num5 = i + 1;
			PointInfo pointInfo = pointInfos[num5];
			int num6 = ((!isAtk) ? 1 : 0);
			if (i == num3 - 1 && num2 > num)
			{
				num6 += 2;
			}
			card_value.state.selectedIndex = num6;
			if (card_value.card is UICom_Card card)
			{
				ShowCard(card, pointInfo);
			}
			switch (num6)
			{
			case 0:
				card_value.title_atk.text = $"+{pointInfo.CurrentPoint}";
				continue;
			case 1:
				card_value.title_def.text = $"+{pointInfo.CurrentPoint}";
				continue;
			}
			int num7 = 0;
			for (int num8 = num5; num8 < pointInfos.Count; num8++)
			{
				num7 += pointInfos[num8].CurrentPoint;
			}
			card_value.title_total.text = $"...+{num7}";
		}
	}

	public async UniTask ShowFightValue(List<PointInfo> atkPoints, List<PointInfo> vicPoints, float intervalTime)
	{
		if (base.contentPane is UIFightWindow uIFightWindow)
		{
			uIFightWindow.com_value.visible = true;
			uIFightWindow.com_value.Cut_in.Play();
			uIFightWindow.com_value.player_atk.title.text = $"{atkPoints[0].CurrentPoint}";
			uIFightWindow.com_value.player_def.title.text = $"{vicPoints[0].CurrentPoint}";
			uIFightWindow.com_value.list_def.childrenRenderOrder = ChildrenRenderOrder.Descent;
			RefreshCardList(uIFightWindow.com_value.list_atk, atkPoints, isAtk: true, intervalTime);
			RefreshCardList(uIFightWindow.com_value.list_def, vicPoints, isAtk: false, intervalTime);
			await UniTask.CompletedTask;
		}
	}

	public void HideFightValue()
	{
		GComponent gComponent = base.contentPane;
		UIFightWindow win = gComponent as UIFightWindow;
		if (win != null)
		{
			win.com_value.Cut_out.Play(delegate
			{
				win.com_value.visible = false;
			});
		}
	}
}
