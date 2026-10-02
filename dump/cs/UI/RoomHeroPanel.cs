using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using UnityTimer;
using party.model;

namespace UI;

public class RoomHeroPanel : BasePanel<UIRoomHeroPanel>
{
	private List<HeroCardData> HeroCardDatas;

	private Timer timeLimiter;

	private const int _confirmAdvanceTime = 1;

	private HeroBarBox _heroBarBox;

	private RoomInfo roomInfo;

	private readonly List<UIRoomHero_Button_SelectHero_2> allHeroItem = new List<UIRoomHero_Button_SelectHero_2>();

	private UIRoomHero_Com_Player ownerHeroCom;

	private RepeatedField<int> banCharacterIds;

	private UIRoomHero_Com_BubbleTip _bubbleTip;

	private UIRoomHero_Com_MedalTip _medalTip;

	private List<SkinStandingPaintingConfigureItem> _StandingPaintings;

	private UIRoomHero_Button_SelectSkin _SelectSkinItem;

	public RoomHeroPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIRoomHeroPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (SimpleSingletonProvider<UIManager>.inst.messageBox.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.messageBox.Hide();
		}
		if (SimpleSingletonProvider<UIManager>.inst.AccountInfo.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.AccountInfo.QuitInfoWindow();
		}
		if (SimpleSingletonProvider<UIManager>.inst.PlayerRenameWindow.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.PlayerRenameWindow.OnClose();
		}
		base.ui.step.selectedIndex = 0;
		ShowBubbleTip(null, isShow: false);
		this.roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		for (int i = 0; i < 4; i++)
		{
			GetPlayerComponent(i).InitData(i, this.roomInfo.GetPlayerBySlot(i), ref ownerHeroCom, this.roomInfo.MapType, _medalTip);
		}
		RoomInfo roomInfo = this.roomInfo;
		if (roomInfo != null && roomInfo.MapType == 10)
		{
			base.ui.com_player_1.x = base.ui.com_player_2.x;
			base.ui.com_player_2.x = base.ui.com_player_3.x;
		}
		ReadyCharacterInfo();
		int num = (int)Mathf.Ceil((float)HeroCardDatas.Count / 12f);
		if (num % 2 != 0)
		{
			num++;
		}
		base.ui.list_RendererSelectRole.numItems = num * 12;
		base.ui.list_RendererSelectRole.scrollPane.touchEffect = num > 2;
		base.ui.list_RendererSelectRole.ResizeToFit(24);
		base.ui.list_RendererSelectRole.scrollPane.pageController = new Controller();
		DoSpecialEffect();
		if (SimpleSingletonProvider<UIManager>.inst.AccountInfo.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.AccountInfo.QuitInfoWindow();
		}
		if (SimpleSingletonProvider<UIManager>.inst.PlayerRenameWindow.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.PlayerRenameWindow.OnClose();
		}
	}

	public override void Show(params object[] objs)
	{
		GameSettings.PlayTipsVibrate();
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		_bubbleTip = UIRoomHero_Com_BubbleTip.CreateInstance();
		_medalTip = UIRoomHero_Com_MedalTip.CreateInstance();
		base.ui.list_RendererSelectRole.itemRenderer = RendererSelectHero;
		base.ui.List_SelectSkin.itemRenderer = RendererSelectSkin;
	}

	public override void Refresh()
	{
		base.Refresh();
		if (roomInfo.State == Room.Types.State.Running)
		{
			RefreshHeroList(roomInfo.Box, result: false);
			ShowBeginTips();
			base.ui.progress_OperationTime.value = 0.0;
			base.ui.list_RendererSelectRole.touchable = false;
			base.ui.btn_SureHero.onClick.Retain();
		}
		else
		{
			base.ui.CutIn.Play();
			Timer obj = timeLimiter;
			if (obj != null)
			{
				obj.Cancel();
			}
			int totalTime = StaticGlobalData.SELECT_ROLE_TIMELIMIT;
			float beginTime = 0f;
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.IsTerms)
			{
				SimpleSingletonProvider<UIManager>.inst.RoomTerms.ShowFullScreenTerms(SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.RoomTerms).Forget();
				totalTime += StaticGlobalData.GAME_MUTATOR_SHOW_TIME / 1000;
				beginTime = (float)StaticGlobalData.GAME_MUTATOR_SHOW_TIME / 1000f;
			}
			timeLimiter = Timer.Register(0f, (float)totalTime, (System.Action)RangeChoiceHero, (System.Action)null, (System.Action)null, (System.Action)null, (System.Action)null, (Action<float>)delegate(float time)
			{
				if ((float)totalTime - time < 1f)
				{
					base.ui.list_RendererSelectRole.touchable = false;
				}
				time = Mathf.Max(0f, time - beginTime);
				UpdateOperationProgress(totalTime - 1, time);
			}, (System.Action)null, false, -1f, false, (GameObject)null);
			base.ui.list_RendererSelectRole.touchable = true;
			base.ui.btn_SureHero.onClick.Release();
			EnableChangeSlotInPVE(base.ui.com_player_1);
			EnableChangeSlotInPVE(base.ui.com_player_2);
			EnableChangeSlotInPVE(base.ui.com_player_3);
			EnableChangeSlotInPVE(base.ui.com_player_4);
		}
		base.ui.btn_terms.visible = roomInfo.IsTerms;
	}

	public override void InitTouchable()
	{
		base.InitTouchable();
		if (roomInfo.State == Room.Types.State.Running)
		{
			base.ui.list_RendererSelectRole.touchable = false;
		}
		else
		{
			base.ui.list_RendererSelectRole.touchable = true;
		}
	}

	private void ReadyCharacterInfo()
	{
		banCharacterIds = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.GetForbiddenHeroIds();
		HeroCardDatas = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetHeroCards(checkStatus: false);
		HeroCardDatas.Sort(CompareTo);
	}

	private int CompareTo(HeroCardData x, HeroCardData y)
	{
		bool flag = x.heroStatus != HeroStatus.None && !banCharacterIds.Contains(x.HeroId);
		bool flag2 = y.heroStatus != HeroStatus.None && !banCharacterIds.Contains(y.HeroId);
		if (roomInfo.IsMutatorPve())
		{
			bool flag3 = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData.HeroMapIsUnlockByHeroId(x.HeroId, roomInfo.MapId);
			bool flag4 = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData.HeroMapIsUnlockByHeroId(y.HeroId, roomInfo.MapId);
			if (flag3 != flag4 && ((x.IsHas && y.IsHas) || (flag && flag2)))
			{
				if (!flag3)
				{
					return 1;
				}
				return -1;
			}
		}
		if (flag == flag2)
		{
			if (x.CollectStatus == y.CollectStatus)
			{
				if (x.IsHas && !y.IsHas)
				{
					return -1;
				}
				if (!x.IsHas && y.IsHas)
				{
					return 1;
				}
				return x.InfoConfig.OrderWeight.CompareTo(y.InfoConfig.OrderWeight);
			}
			return -x.CollectStatus.CompareTo(y.CollectStatus);
		}
		if (!flag)
		{
			return 1;
		}
		return -1;
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_SureHero.onClick.Add(RequestSureHero);
		base.ui.btn_SureSkin.onClick.Add(RequestSureSkin);
		for (int i = 0; i < allHeroItem.Count; i++)
		{
			allHeroItem[i].onClick.Add(RequestChoiceHero);
		}
		base.ui.list_RendererSelectRole.scrollPane.onScroll.Add(DoSpecialEffect);
		base.ui.List_SelectSkin.onClickItem.Add(OnSelectSkin);
		base.ui.btn_Up.onClick.Add(OnMoveUpSelectHeroList);
		base.ui.btn_Down.onClick.Add(OnMoveDownSelectHeroList);
		base.ui.btn_terms.onClick.Add(ShowTerms);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_SureHero.onClick.Remove(RequestSureHero);
		base.ui.btn_SureSkin.onClick.Remove(RequestSureSkin);
		base.ui.list_RendererSelectRole.onClick.Remove(RequestChoiceHero);
		for (int i = 0; i < allHeroItem.Count; i++)
		{
			allHeroItem[i].onClick.Remove(RequestChoiceHero);
		}
		base.ui.list_RendererSelectRole.scrollPane.onScroll.Remove(DoSpecialEffect);
		base.ui.List_SelectSkin.onClickItem.Remove(OnSelectSkin);
		base.ui.btn_Up.onClick.Remove(OnMoveUpSelectHeroList);
		base.ui.btn_Down.onClick.Remove(OnMoveDownSelectHeroList);
		base.ui.btn_terms.onClick.Remove(ShowTerms);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomPlayerChange.AddListener(RefreshPlayerSlots);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.refreshHeroList.AddListener(RefreshHeroList);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.SureHero.AddListener(SureHeroInfo);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.SureSkin.AddListener(SureSkinInfo);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.HeroLoadReady.AddListener(SwitchReady);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.UpdateHeroProgress.AddListener(RefreshLoadProgress);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.applyChangeSlot.AddListener(ChangePlayerSlots);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomPlayerChange.RemoveListener(RefreshPlayerSlots);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.refreshHeroList.RemoveListener(RefreshHeroList);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.SureHero.RemoveListener(SureHeroInfo);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.SureSkin.RemoveListener(SureSkinInfo);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.HeroLoadReady.RemoveListener(SwitchReady);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.UpdateHeroProgress.RemoveListener(RefreshLoadProgress);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.applyChangeSlot.RemoveListener(ChangePlayerSlots);
	}

	public override void Close()
	{
		_heroBarBox = null;
		Timer obj = timeLimiter;
		if (obj != null)
		{
			obj.Cancel();
		}
		for (int i = 0; i < allHeroItem.Count; i++)
		{
			allHeroItem[i].Dispose();
		}
		allHeroItem.Clear();
		CommonUIManager.StopAllVideo();
		GRoot.inst.HidePopup(_bubbleTip);
		GRoot.inst.HidePopup(_medalTip);
		base.Close();
	}

	public override void Dispose()
	{
		GRoot.inst.HidePopup(_bubbleTip);
		_bubbleTip?.Dispose();
		_bubbleTip = null;
		GRoot.inst.HidePopup(_medalTip);
		_medalTip?.Dispose();
		_medalTip = null;
		base.Dispose();
	}

	private void RequestSureHero()
	{
		if (base.ui.list_RendererSelectRole.selectedIndex == -1 || ownerHeroCom == null || ownerHeroCom.selectHero == null)
		{
			return;
		}
		if (ownerHeroCom.selectHero.heroStatus == HeroStatus.None)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1007, 2f);
			return;
		}
		if (GetHeroHasChoice(ownerHeroCom.selectHero.HeroId))
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1006, 2f);
			return;
		}
		base.ui.btn_SureHero.onClick.Retain();
		base.ui.list_RendererSelectRole.touchable = false;
		Timer obj = timeLimiter;
		if (obj != null)
		{
			obj.Pause();
		}
		UpdateOperationProgress(0, 0f);
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestAffirmHeroC2S(_auto: false);
	}

	private void RequestChoiceHero(EventContext context)
	{
		EventDispatcher sender = context.sender;
		UIRoomHero_Button_SelectHero_2 btn = sender as UIRoomHero_Button_SelectHero_2;
		if (btn == null)
		{
			return;
		}
		for (int i = 0; i < allHeroItem.Count; i++)
		{
			if (btn != allHeroItem[i])
			{
				allHeroItem[i].selected = false;
				allHeroItem[i].com_Hero.stateChange.selectedIndex = 0;
			}
		}
		if (btn.isLock.selectedIndex == 1 && !btn.grayed)
		{
			ShowBubbleTip(btn, isShow: true);
		}
		else
		{
			ShowBubbleTip(btn, isShow: false);
		}
		if (btn.HeroCard == null || btn.grayed || btn.isLock.selectedIndex == 1)
		{
			return;
		}
		ownerHeroCom.RefreshDetailInfo(btn.HeroCard.HeroId, btn.HeroCard.PveData.GetBattlePveLevel(), btn.HeroCard.PveData.GetLastLockTalentId(), isLocal: true);
		btn.selected = true;
		btn.onClick.Retain();
		HeroBar heroState = GetHeroState(btn.HeroCard.HeroId);
		if (heroState != null && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(heroState.PlayerId))
		{
			btn.onClick.Release();
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestChoiceHeroC2S2(btn.HeroCard.HeroId).OnFinishedOnly.AddOnce(delegate
		{
			btn.onClick.Release();
		});
	}

	private void DoSpecialEffect()
	{
		float posY = base.ui.list_RendererSelectRole.scrollPane.posY;
		int numChildren = base.ui.list_RendererSelectRole.numChildren;
		for (int i = 0; i < numChildren; i++)
		{
			GObject childAt = base.ui.list_RendererSelectRole.GetChildAt(i);
			float num = childAt.height + (float)base.ui.list_RendererSelectRole.lineGap;
			float num2 = childAt.width + (float)base.ui.list_RendererSelectRole.columnGap;
			float num3 = (1f - (childAt.y - posY) / num) * 30f;
			childAt.x = num3 + (float)(i % 12) * num2;
		}
		base.ui.btn_Up.visible = base.ui.list_RendererSelectRole.scrollPane.percY > 0.1f;
		base.ui.btn_Down.visible = base.ui.list_RendererSelectRole.scrollPane.percY < 0.9f;
	}

	private void OnMoveUpSelectHeroList()
	{
		base.ui.btn_Up.onClick.Retain();
		base.ui.list_RendererSelectRole.scrollPane.ScrollUp(1f, ani: true);
		base.ui.btn_Up.onClick.Release();
	}

	private void OnMoveDownSelectHeroList()
	{
		base.ui.btn_Down.onClick.Retain();
		base.ui.list_RendererSelectRole.scrollPane.ScrollDown(1f, ani: true);
		base.ui.btn_Down.onClick.Release();
	}

	private void UpdateOperationProgress(int operationTimeTotal, float _)
	{
		base.ui.progress_OperationTime.max = operationTimeTotal;
		base.ui.progress_OperationTime.min = 0.0;
		base.ui.progress_OperationTime.value = Mathf.Min(operationTimeTotal, _);
	}

	private void RangeChoiceHero()
	{
		base.ui.list_RendererSelectRole.touchable = false;
		Timer obj = timeLimiter;
		if (obj != null)
		{
			obj.Cancel();
		}
		UpdateOperationProgress(0, 0f);
		base.ui.btn_SureHero.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestAffirmHeroC2S(_auto: true).OnFinishedOnly.AddOnce(delegate
		{
			base.ui.list_RendererSelectRole.touchable = false;
			base.ui.btn_SureHero.grayed = true;
			base.ui.btn_SureHero.touchable = false;
			base.ui.btn_SureHero.onClick.Release();
		});
	}

	private void RefreshHeroList(HeroBarBox _data, bool result)
	{
		_heroBarBox = _data;
		if (_heroBarBox != null)
		{
			if (_heroBarBox.Box.TryGetValue(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID(), out var value))
			{
				base.ui.btn_SureHero.grayed = result || value.Affirm;
				base.ui.btn_SureHero.touchable = !result && !value.Affirm;
			}
			if (!result)
			{
				foreach (KeyValuePair<long, HeroBar> item in _heroBarBox.Box)
				{
					RoomPlayer playerById = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.GetPlayerById(item.Key);
					if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerById.Id) || item.Value.Affirm)
					{
						RendererHero(playerById.Slot);
					}
				}
			}
		}
		if (!result)
		{
			RefreshSelectHero();
		}
	}

	private void SureHeroInfo(int heroId, long playerId, bool hasChoice)
	{
		RoomPlayer playerById = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.GetPlayerById(playerId);
		if (playerById == null)
		{
			return;
		}
		if (playerById.IsBot)
		{
			GetPlayerComponent(playerById.Slot).RefreshHeroAnimation(playerById.standingPainting, affirmed: true).Forget();
		}
		else
		{
			GetPlayerComponent(playerById.Slot).SureHero(heroId, hasChoice);
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			return;
		}
		if (hasChoice)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1008, 2f);
			Timer obj = timeLimiter;
			if (obj != null)
			{
				obj.Resume();
			}
			base.ui.list_RendererSelectRole.touchable = true;
			base.ui.btn_SureHero.onClick.Release();
		}
		else
		{
			RefreshSelectSkin();
		}
	}

	private UIRoomHero_Com_Player GetPlayerComponent(int index)
	{
		return index switch
		{
			0 => base.ui.com_player_1, 
			1 => base.ui.com_player_2, 
			2 => base.ui.com_player_3, 
			3 => base.ui.com_player_4, 
			_ => null, 
		};
	}

	private void RefreshPlayerSlots()
	{
		for (int i = 0; i < 4; i++)
		{
			GetPlayerComponent(i).InitData(i, roomInfo.GetPlayerBySlot(i), ref ownerHeroCom, roomInfo.MapType, _medalTip);
		}
		if (_heroBarBox != null)
		{
			RefreshHeroList(_heroBarBox, roomInfo.State == Room.Types.State.Running);
		}
	}

	private void RendererHero(int index)
	{
		UIRoomHero_Com_Player playerComponent = GetPlayerComponent(index);
		if (playerComponent != null)
		{
			if (roomInfo.Players.Count > index)
			{
				playerComponent.RefreshHero(_heroBarBox);
				return;
			}
			playerComponent.com_PlayerLabel.visible = false;
			playerComponent.txt_progress.visible = false;
			playerComponent.loader_Animation.visible = false;
			playerComponent.group_Ok.visible = false;
		}
	}

	private void RendererSelectHero(int index, GObject item)
	{
		if (!(item is UIRoomHero_Button_SelectHero_2 uIRoomHero_Button_SelectHero_))
		{
			return;
		}
		if (HeroCardDatas.Count > index)
		{
			if (uIRoomHero_Button_SelectHero_.HeroCard == null)
			{
				uIRoomHero_Button_SelectHero_.InitData(index, HeroCardDatas[index]);
			}
		}
		else
		{
			uIRoomHero_Button_SelectHero_.InitData(index, null);
		}
		allHeroItem.Add(uIRoomHero_Button_SelectHero_);
	}

	private void RefreshSelectHero()
	{
		for (int i = 0; i < allHeroItem.Count; i++)
		{
			if (HeroCardDatas.Count > i)
			{
				allHeroItem[i].RefreshData(GetHeroState(HeroCardDatas[i].HeroId));
			}
		}
	}

	private HeroBar GetHeroState(int heroId)
	{
		if (_heroBarBox != null)
		{
			foreach (KeyValuePair<long, HeroBar> item in _heroBarBox.Box)
			{
				if (item.Value.HeroId == heroId)
				{
					return item.Value;
				}
			}
		}
		return null;
	}

	private bool GetHeroHasChoice(int heroId)
	{
		if (_heroBarBox != null)
		{
			foreach (KeyValuePair<long, HeroBar> item in _heroBarBox.Box)
			{
				if (item.Value.HeroId == heroId && item.Value.Affirm)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void RefreshSelectSkin()
	{
		if (ownerHeroCom == null || ownerHeroCom.selectHero == null)
		{
			return;
		}
		base.ui.step.selectedIndex = 1;
		int standingPainting = ownerHeroCom.selectHero.InfoConfig.StandingPainting;
		_StandingPaintings = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetStandingPaintingsById(standingPainting);
		if (_StandingPaintings != null && _StandingPaintings.Count > 0)
		{
			base.ui.List_SelectSkin.numItems = _StandingPaintings.Count;
			foreach (GObject child in base.ui.List_SelectSkin._children)
			{
				if (child is UIRoomHero_Button_SelectSkin uIRoomHero_Button_SelectSkin)
				{
					uIRoomHero_Button_SelectSkin.selected = uIRoomHero_Button_SelectSkin.SkinItemId == ownerHeroCom.selectHero.standingPainting.ItemID;
					if (uIRoomHero_Button_SelectSkin.selected)
					{
						_SelectSkinItem = uIRoomHero_Button_SelectSkin;
					}
				}
			}
		}
		Timer obj = timeLimiter;
		if (obj != null)
		{
			obj.Cancel();
		}
		int totalTime = StaticGlobalData.SELECT_ROLESKIN_TIMELIMIT;
		timeLimiter = Timer.Register(0f, (float)totalTime, (System.Action)delegate
		{
			base.ui.btn_SureSkin.onClick.Retain();
			base.ui.List_SelectSkin.touchable = false;
			Timer obj2 = timeLimiter;
			if (obj2 != null)
			{
				obj2.Cancel();
			}
			UpdateOperationProgress(0, 0f);
			int valueOrDefault = (ownerHeroCom?.selectHero?.HeroId).GetValueOrDefault();
			SimpleSingletonProvider<GameLogicManager>.inst.room.RequestChooseSkinC2S(valueOrDefault, 0, affirmed: true);
		}, (System.Action)null, (System.Action)null, (System.Action)null, (System.Action)null, (Action<float>)delegate(float time)
		{
			if ((float)totalTime - time < 1f)
			{
				base.ui.List_SelectSkin.touchable = false;
				UIRoomHero_Com_ChangeSlot com_ChangeSlot = base.ui.com_player_1.com_ChangeSlot;
				UIRoomHero_Com_ChangeSlot com_ChangeSlot2 = base.ui.com_player_2.com_ChangeSlot;
				UIRoomHero_Com_ChangeSlot com_ChangeSlot3 = base.ui.com_player_3.com_ChangeSlot;
				bool flag = (base.ui.com_player_4.com_ChangeSlot.visible = false);
				bool flag3 = (com_ChangeSlot3.visible = flag);
				bool visible = (com_ChangeSlot2.visible = flag3);
				com_ChangeSlot.visible = visible;
			}
			UpdateOperationProgress(totalTime - 1, time);
		}, (System.Action)null, false, -1f, false, (GameObject)null);
		base.ui.List_SelectSkin.touchable = true;
		base.ui.btn_SureSkin.grayed = false;
		base.ui.btn_SureSkin.touchable = true;
		base.ui.btn_SureSkin.onClick.Release();
	}

	private void SureSkinInfo(long playerId, bool affirm)
	{
		RoomPlayer playerById = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.GetPlayerById(playerId);
		if (playerById == null || playerById.standingPainting == null)
		{
			return;
		}
		GetPlayerComponent(playerById.Slot).RefreshHeroAnimation(playerById.standingPainting, affirm).Forget();
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			return;
		}
		foreach (GObject child in base.ui.List_SelectSkin._children)
		{
			if (child is UIRoomHero_Button_SelectSkin uIRoomHero_Button_SelectSkin)
			{
				uIRoomHero_Button_SelectSkin.selected = uIRoomHero_Button_SelectSkin.SkinItemId == playerById.standingPainting.ItemID;
			}
		}
		if (affirm)
		{
			base.ui.List_SelectSkin.touchable = false;
			base.ui.btn_SureSkin.grayed = true;
			base.ui.btn_SureSkin.touchable = false;
		}
	}

	private void RendererSelectSkin(int index, GObject item)
	{
		if (_StandingPaintings != null && _StandingPaintings.Count > index && item is UIRoomHero_Button_SelectSkin uIRoomHero_Button_SelectSkin)
		{
			uIRoomHero_Button_SelectSkin.RefreshSkinItem(_StandingPaintings[index]);
		}
	}

	private void RequestSureSkin(EventContext context)
	{
		if (_SelectSkinItem != null && _SelectSkinItem.IsHas)
		{
			base.ui.btn_SureSkin.onClick.Retain();
			base.ui.List_SelectSkin.touchable = false;
			Timer obj = timeLimiter;
			if (obj != null)
			{
				obj.Cancel();
			}
			UpdateOperationProgress(0, 0f);
			int valueOrDefault = (ownerHeroCom?.selectHero?.HeroId).GetValueOrDefault();
			SimpleSingletonProvider<GameLogicManager>.inst.room.RequestChooseSkinC2S(valueOrDefault, 0, affirmed: true);
		}
	}

	private void OnSelectSkin(EventContext context)
	{
		if (!(context.data is UIRoomHero_Button_SelectSkin { StandingPainting: not null } uIRoomHero_Button_SelectSkin))
		{
			return;
		}
		ownerHeroCom.RefreshHeroAnimation(uIRoomHero_Button_SelectSkin.StandingPainting, affirmed: false).Forget();
		base.ui.btn_SureSkin.grayed = !uIRoomHero_Button_SelectSkin.IsHas;
		base.ui.btn_SureSkin.touchable = uIRoomHero_Button_SelectSkin.IsHas;
		if (uIRoomHero_Button_SelectSkin.IsHas)
		{
			_SelectSkinItem = uIRoomHero_Button_SelectSkin;
			base.ui.List_SelectSkin.touchable = false;
			SimpleSingletonProvider<GameLogicManager>.inst.room.RequestChooseSkinC2S(ownerHeroCom.selectHero.HeroId, uIRoomHero_Button_SelectSkin.SkinItemId, affirmed: false).OnFinishedOnly.AddOnce(delegate
			{
				base.ui.List_SelectSkin.touchable = true;
			});
		}
	}

	private void SwitchReady()
	{
		ShowBeginTips();
		CommonUIManager.TryAddVideoGraph(UIType.Panel, (int)base.config.PanelType, base.ui.loader_ReadyAnime);
		SimpleSingletonProvider<CriMovieManager>.inst.Play(200.GetVideoKey(), base.ui.loader_ReadyAnime).Forget();
	}

	private void RefreshLoadProgress()
	{
		roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		foreach (RoomPlayer player in roomInfo.Players)
		{
			UIRoomHero_Com_Player playerComponent = GetPlayerComponent(player.Slot);
			playerComponent.txt_progress.visible = true;
			playerComponent.txt_progress.SetVar("progress", player.Progress.ToString()).FlushVars();
		}
	}

	private void ShowBeginTips()
	{
		List<int> tipsIds = StaticConfigure.BeginTips.GetBeginTipsIds(roomInfo.MapType);
		base.ui.StartLoad.Play(delegate
		{
			int index = UnityEngine.Random.Range(0, tipsIds.Count - 1);
			base.ui.txt_Explain.text = tipsIds[index].GetLocal(UIStringType.BeginTips);
		});
	}

	private void EnableChangeSlotInPVE(UIRoomHero_Com_Player com_Player)
	{
		if (BattleConfig.IsPVE(roomInfo.MapType))
		{
			com_Player.InitChangeSlotComponent();
		}
		else
		{
			com_Player.com_ChangeSlot.visible = false;
		}
	}

	private void ChangePlayerSlots()
	{
		RoomPlayer selfInfo = roomInfo.GetSelfInfo();
		if (selfInfo == null || ownerHeroCom == null)
		{
			return;
		}
		if (ownerHeroCom.p.selectedIndex != selfInfo.Slot)
		{
			for (int i = 0; i < allHeroItem.Count; i++)
			{
				allHeroItem[i].RefreshSlotIndex(selfInfo.Slot);
			}
		}
		if (base.ui.com_player_1.IsChangePlayer)
		{
			base.ui.com_player_1.InitData(0, roomInfo.GetPlayerBySlot(0), ref ownerHeroCom, roomInfo.MapType, _medalTip);
		}
		if (base.ui.com_player_2.IsChangePlayer)
		{
			base.ui.com_player_2.InitData(1, roomInfo.GetPlayerBySlot(1), ref ownerHeroCom, roomInfo.MapType, _medalTip);
		}
		if (base.ui.com_player_3.IsChangePlayer)
		{
			base.ui.com_player_3.InitData(2, roomInfo.GetPlayerBySlot(2), ref ownerHeroCom, roomInfo.MapType, _medalTip);
		}
		if (base.ui.com_player_4.IsChangePlayer)
		{
			base.ui.com_player_4.InitData(3, roomInfo.GetPlayerBySlot(3), ref ownerHeroCom, roomInfo.MapType, _medalTip);
		}
		base.ui.com_player_1.RefreshChangeSlotComponent();
		base.ui.com_player_2.RefreshChangeSlotComponent();
		base.ui.com_player_3.RefreshChangeSlotComponent();
		base.ui.com_player_4.RefreshChangeSlotComponent();
	}

	private void ShowTerms()
	{
		base.ui.CTCut_in_.Play();
		SimpleSingletonProvider<UIManager>.inst.RoomTerms.ShowWinScreenTerms(roomInfo.RoomTerms, delegate
		{
			if (base.ui != null)
			{
				base.ui.CTCut_out.Play();
				base.ui.btn_terms.visible = true;
			}
		}).Forget();
	}

	public void ShowBubbleTip(UIRoomHero_Button_SelectHero_2 btn, bool isShow)
	{
		if (isShow)
		{
			GRoot.inst.ShowPopup(_bubbleTip, btn, PopupDirection.Down);
			_bubbleTip.SetXY(_bubbleTip.x + btn.width / 3f, _bubbleTip.y - btn.height - _bubbleTip.height / 2f);
			_bubbleTip.touchable = false;
		}
		else
		{
			GRoot.inst.HidePopup(_bubbleTip);
		}
	}
}
