using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Core;
using Core.Camera;
using Core.Scene;
using Core.Unit;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using GameLogic.Replay;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class BattleInfoPanel : BasePanel<UIBattleInfoPanel>
{
	private LongPressGesture longPressGesture;

	private UIBattleInfo_Com_PVEProgress com_PVEProgress;

	private UIBattleInfo_Com_Asymmetrical com_Asymmetrical;

	private UIBattleInfo_Com_LuckyStar com_LuckyStar;

	private List<PropertyData<int>> propertyBuffList = new List<PropertyData<int>>();

	private List<Buff> showBuffs;

	private BattlePlayerData playerData;

	private UniTaskCompletionSource showTimeTsc;

	private CancellationTokenSource cts;

	private readonly Dictionary<long, UIBattleInfo_Com_MiniMap_Icon> miniMapIcons = new Dictionary<long, UIBattleInfo_Com_MiniMap_Icon>();

	private readonly Dictionary<int, Vector2> miniLandPos = new Dictionary<int, Vector2>();

	private Matrix4x4 rotateMat;

	private Vector3 mapCenter;

	private Vector3 mapFarthest;

	private float miniMapwidth;

	private float miniMapheight;

	private float max_Distance;

	private RoomInfo roomInfo => SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;

	public BattleInfoPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIBattleInfoPanel.CreateInstance();
		if (roomInfo.IsPVE())
		{
			if (com_PVEProgress == null)
			{
				com_PVEProgress = UIBattleInfo_Com_PVEProgress.CreateInstance();
			}
			com_PVEProgress.scale = Vector2.one * 1.2f;
			float xv = base.ui.com_GameModeComponent.width * 0.5f - com_PVEProgress.width * 0.5f * 1.2f;
			base.ui.com_GameModeComponent.AddChild(com_PVEProgress);
			com_PVEProgress.SetXY(xv, 0f, topLeftValue: true);
		}
		else if (roomInfo.MapType == 7)
		{
			if (com_Asymmetrical == null)
			{
				com_Asymmetrical = UIBattleInfo_Com_Asymmetrical.CreateInstance();
			}
			base.ui.com_GameModeComponent.AddChild(com_Asymmetrical);
			float xv2 = base.ui.com_GameModeComponent.width * 0.5f - com_Asymmetrical.width * 0.5f;
			com_Asymmetrical.SetXY(xv2, 0f, topLeftValue: true);
		}
		else if (roomInfo.MapType == 11)
		{
			base.ui.com_MapInfo.visible = false;
			if (com_LuckyStar == null)
			{
				com_LuckyStar = UIBattleInfo_Com_LuckyStar.CreateInstance();
			}
			base.ui.com_GameModeComponent.AddChild(com_LuckyStar);
			float xv3 = base.ui.com_GameModeComponent.width * 0.5f - com_LuckyStar.width * 0.5f;
			com_LuckyStar.SetXY(xv3, 0f, topLeftValue: true);
		}
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		base.ui.com_BattlePlayer.Init();
	}

	public override void Show(params object[] objs)
	{
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.list_Buff.itemRenderer = RendererBuff;
		com_PVEProgress?.InitComponents();
		com_Asymmetrical?.InitComponents();
		com_LuckyStar?.InitComponents();
		base.ui.com_Clue?.InitComponents();
		longPressGesture = new LongPressGesture(GRoot.inst);
		longPressGesture.holdRangeRadius = (int)GRoot.inst.width;
		longPressGesture.trigger = 0.1f;
		base.ui.com_BattlePlayer.InitComponent();
	}

	public override void Refresh()
	{
		base.Refresh();
		if (roomInfo.IsPVE())
		{
			base.ui.GameMode.selectedIndex = 1;
			com_PVEProgress.Refresh();
		}
		else if (roomInfo.MapType == 7)
		{
			base.ui.GameMode.selectedIndex = 2;
			com_Asymmetrical?.Refresh();
		}
		else if (roomInfo.MapType == 11)
		{
			com_LuckyStar?.Refresh();
		}
		else
		{
			base.ui.GameMode.selectedIndex = 0;
		}
		base.ui.platform.selectedIndex = 1;
		RefreshHeroSkin();
		UpdataATK(playerData.Property.ATK.Value);
		UpdataDEF(playerData.Property.DEF.Value);
		Minimap_Refresh();
		base.ui.btn_ShowRelic.visible = roomInfo.IsPVE();
		base.ui.btn_VictoryCondition.visible = !roomInfo.IsCampaign() && !roomInfo.IsNovice();
		if (roomInfo.IsPVE() && StaticConfigure.Map.InfoDict.TryGetValue(roomInfo.MapId, out var value) && value.TutorialId != 0)
		{
			base.ui.btn_VictoryCondition.txt_Title.text = 20.GetLocal(UIStringType.Map);
		}
		else
		{
			base.ui.btn_VictoryCondition.txt_Title.text = 21.GetLocal(UIStringType.Map);
		}
		RefreshRound(roomInfo.Round);
		base.ui.Refresh();
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001)
		{
			if (com_PVEProgress != null)
			{
				com_PVEProgress.visible = false;
			}
			base.ui.com_MapInfo.visible = false;
			base.ui.btn_ShowRelic.visible = false;
		}
		base.ui.btn_terms.visible = roomInfo.IsTerms;
		if (roomInfo.IsClue())
		{
			base.ui.com_Clue.Refresh();
		}
		else
		{
			base.ui.com_Clue.visible = false;
		}
		ReplaySession replaySession = SimpleSingletonProvider<GameLogicManager>.inst.replay?.Session;
		if (replaySession == null || !replaySession.IsReplay)
		{
			RefreshWatchFollowSwitchBtn(SimpleSingletonProvider<GameLogicManager>.inst.watch.IsFollow.Value);
		}
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		Minimap_AddEvent();
		base.ui.btn_VictoryCondition.onClick.Add(OpenVictoryCondition);
		base.ui.btn_ESC.onClick.Set(OpenSettingPanel);
		com_PVEProgress?.AddEvent();
		com_Asymmetrical?.AddEvent();
		com_LuckyStar?.AddEvent();
		base.ui.com_Clue.AddEvent();
		base.ui.btn_ShowRelic.onClick.Add(ShowRelic);
		longPressGesture?.onBegin.Add(OnLongPressGestureBegin);
		longPressGesture?.onEnd.Add(OnLongPressGestureEnd);
		base.ui.com_BattlePlayer.AddEvent();
		base.ui.btn_terms.onClick.Add(ShowTerms);
		base.ui.btn_WatchFollowSwitch.btn_switch.onClick.Add(OnClickWatchFollowSwitch);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		Minimap_RemoveEvent();
		base.ui.btn_VictoryCondition.onClick.Remove(OpenVictoryCondition);
		base.ui.btn_ESC.onClick.Remove(OpenSettingPanel);
		com_PVEProgress?.RemoveEvent();
		com_Asymmetrical?.RemoveEvent();
		com_LuckyStar?.RemoveEvent();
		base.ui.com_Clue.RemoveEvent();
		base.ui.btn_ShowRelic.onClick.Remove(ShowRelic);
		longPressGesture?.onBegin.Remove(OnLongPressGestureBegin);
		longPressGesture?.onEnd.Remove(OnLongPressGestureEnd);
		base.ui.com_BattlePlayer.RemoveEvent();
		base.ui.btn_terms.onClick.Remove(ShowTerms);
		base.ui.btn_WatchFollowSwitch.btn_switch.onClick.Remove(OnClickWatchFollowSwitch);
	}

	protected override void AddListener()
	{
		base.AddListener();
		Minimap_AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roundChange.AddListener(RefreshRound);
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.AddListener(ReadyFight);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.showLandTip.AddListener(ShowLandTip);
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.AddListener(RefreshBuff);
		SimpleSingletonProvider<GameLogicManager>.inst.watch.SubscribePlayerId.AddListener(RefreshPlayerUI);
		AddListenerProperty();
		com_PVEProgress?.AddListener();
		com_Asymmetrical?.AddListener();
		com_LuckyStar?.AddListener();
		base.ui.com_Clue.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.campaign.signal.campaignCondition.AddListener(RefreshCampaignVictory);
		base.ui.com_BattlePlayer.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.ThrowDice.AddListener(ThrowDice);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.MarkSignal.MarkEnter.AddListener(OnMarkEnter);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.MarkSignal.MarkExit.AddListener(OnMarkExit);
		SimpleSingletonProvider<GameLogicManager>.inst.watch.IsFollow.AddListener(WatchFollowSwitch);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		Minimap_RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roundChange.RemoveListener(RefreshRound);
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.RemoveListener(ReadyFight);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.showLandTip.RemoveListener(ShowLandTip);
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.RemoveListener(RefreshBuff);
		SimpleSingletonProvider<GameLogicManager>.inst.watch.SubscribePlayerId.RemoveListener(RefreshPlayerUI);
		RemoveListenerProperty();
		com_PVEProgress?.RemoveListener();
		com_Asymmetrical?.RemoveListener();
		com_LuckyStar?.RemoveListener();
		base.ui.com_Clue.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.campaign.signal.campaignCondition.RemoveListener(RefreshCampaignVictory);
		base.ui.com_BattlePlayer.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.ThrowDice.RemoveListener(ThrowDice);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.MarkSignal.MarkEnter.RemoveListener(OnMarkEnter);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.MarkSignal.MarkExit.RemoveListener(OnMarkExit);
		SimpleSingletonProvider<GameLogicManager>.inst.watch.IsFollow.RemoveListener(WatchFollowSwitch);
	}

	private void AddListenerProperty()
	{
		playerData.Property.ATK.AddListener(UpdataATK);
		playerData.Property.DEF.AddListener(UpdataDEF);
	}

	private void RemoveListenerProperty()
	{
		playerData.Property.ATK.RemoveListener(UpdataATK);
		playerData.Property.DEF.RemoveListener(UpdataDEF);
	}

	public override void Close()
	{
		if (base.ui != null)
		{
			if (com_PVEProgress != null)
			{
				com_PVEProgress.Close();
			}
			if (com_Asymmetrical != null)
			{
				com_Asymmetrical.Close();
			}
			base.ui.com_Clue.Close();
			base.Close();
		}
	}

	public override void Dispose()
	{
		longPressGesture?.Dispose();
		if (com_PVEProgress != null)
		{
			com_PVEProgress.DisposeProgress();
		}
		if (com_Asymmetrical != null)
		{
			com_Asymmetrical.DisposeProgress();
		}
		MinimapDispose();
		base.Dispose();
	}

	public override void AdultMode(bool inAdultMode)
	{
		RefreshHeroSkin();
	}

	public override void InitTouchable()
	{
		base.InitTouchable();
		base.ui.loader_Skin.touchableAll = false;
	}

	public override void CoverMode(bool inCoverMode)
	{
		base.CoverMode(inCoverMode);
		base.ui.com_BattlePlayer.CoverMode(inCoverMode);
	}

	private void ShowLandTip(int landId)
	{
		UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(landId);
		if (landById == null)
		{
			return;
		}
		LandInfoConfigure landInfoConfigure = StaticConfigure.Land.InfoDict[(int)landById.LandType];
		if (landInfoConfigure == null)
		{
			return;
		}
		base.ui.com_LandTip.txt_LandName.text = landInfoConfigure.NameID.GetLocal(UIStringType.Land);
		base.ui.com_LandTip.loader_Land.url = landById.GetLandIcon();
		base.ui.com_LandTip.visible = true;
		Stage.inst.PlayOneShotSound(landInfoConfigure.LandSfx);
		base.ui.com_LandTip.showInfo.Play(delegate
		{
			if (base.ui?.com_LandTip?.showInfo != null)
			{
				base.ui.com_LandTip.visible = false;
			}
		});
	}

	private void OnKeyDown(EventContext context)
	{
		if (_FocusStatus && context.inputEvent.keyCode == KeyCode.Escape)
		{
			OpenSettingPanel();
		}
	}

	private async void OpenSettingPanel()
	{
		if (!(SimpleSingletonProvider<UIManager>.inst.currentPanel is BattleSettlementPanel))
		{
			base.ui.btn_ESC.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.settingInBattle.ShowSettingInBattle();
			base.ui.btn_ESC.onClick.Release();
		}
	}

	private void RefreshRound(int round)
	{
		RefreshBuff();
		if (com_Asymmetrical != null)
		{
			com_Asymmetrical.RefreshRound(round);
		}
		base.ui.txt_Round.text = round.ToString().PadLeft(2, '0');
	}

	private void ReadyFight(bool state, UIPanelType panelType)
	{
		if (panelType == UIPanelType.None || panelType == UIPanelType.BattleInfo)
		{
			int num = ((!state) ? 1 : 0);
			base.ui.SetScale(num, num);
		}
	}

	private void RefreshBuff()
	{
		propertyBuffList.Clear();
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData != null && !(selfPlayerData.CharacterInst == null) && selfPlayerData.Property != null)
		{
			(showBuffs, propertyBuffList) = selfPlayerData.buffContainer.GetShowBuffs(playerData, isRegister: true);
			base.ui.list_Buff.numItems = showBuffs.Count + propertyBuffList.Count;
		}
	}

	private void RendererBuff(int index, GObject item)
	{
		UIButton_Buff buffItem = item as UIButton_Buff;
		if (buffItem == null)
		{
			return;
		}
		if (propertyBuffList.Count > index)
		{
			buffItem.RefreshProperty(propertyBuffList[index], dynamic: true);
		}
		else
		{
			int num = index - propertyBuffList.Count;
			if (showBuffs.Count > num)
			{
				buffItem.RefreshBuff(showBuffs[num]);
			}
		}
		buffItem.onTouchBegin.Set((EventCallback0)delegate
		{
			if (buffItem.BuffConfig != null)
			{
				CommonUIManager.RefreshHyperlinkDesc(buffItem.BuffConfig.DescId.GetLocal(UIStringType.Buff), base.ui.txt_Buff, roomInfo.Difficulty);
				base.ui.txt_Buff.SetTextAdaptiveMinHeight(138f);
				base.ui.showBuff.selectedIndex = 1;
				base.ui.loader_BuffIcon.url = buffItem.BuffConfig.Icon;
				int nameId = buffItem.BuffConfig.NameId;
				if (nameId != 0)
				{
					base.ui.txt_BuffTitle.text = nameId.GetLocal(UIStringType.Buff);
				}
			}
		});
		buffItem.onTouchEnd.Set((EventCallback0)delegate
		{
			base.ui.txt_BuffTitle.text = "";
			base.ui.txt_Buff.text = "";
			base.ui.loader_BuffIcon.url = "";
			base.ui.showBuff.selectedIndex = 0;
		});
	}

	private void UpdataATK(int value)
	{
		base.ui.txt_ATK.text = value.ToString();
	}

	private void UpdataDEF(int value)
	{
		base.ui.txt_DEF.text = value.ToString();
	}

	private void RefreshHeroSkin()
	{
		(string, bool) characterInGame = playerData.player.standingPainting.GetCharacterInGame();
		Vector2 offset = Vector2.zero;
		Vector2 scale = Vector2.one;
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(characterInGame.Item1, out var value))
		{
			offset = value.bustSkinOffset;
			scale = new Vector2(value.bustSkinScale * (float)((!value.reversalAxisX) ? 1 : (-1)), value.bustSkinScale);
		}
		CommonUIManager.RendererSkin(UIType.Panel, (int)base.config.PanelType, (UICom_HeroSkin)base.ui.loader_Skin, characterInGame.Item1, characterInGame.Item2, offset, scale);
	}

	public async void OpenVictoryCondition()
	{
		if (roomInfo == null)
		{
			return;
		}
		int mapType = roomInfo.MapType;
		if (mapType == 1 || mapType == 3 || mapType == 8)
		{
			await SimpleSingletonProvider<UIManager>.inst.explain.ShowPVPTip(roomInfo.UpgradePlan);
			return;
		}
		mapType = roomInfo.MapType;
		if (mapType == 4 || mapType == 9 || mapType == 11 || mapType == 12)
		{
			await SimpleSingletonProvider<UIManager>.inst.explain.ShowPVETutorial(roomInfo.MapId);
		}
		else if (roomInfo.MapType == 7)
		{
			await SimpleSingletonProvider<UIManager>.inst.explain.ShowTutorial(1002);
		}
	}

	private void RefreshPlayerUI(long playerID)
	{
		if (playerData != null)
		{
			RemoveListenerProperty();
		}
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		AddListenerProperty();
		RefreshBuff();
		RefreshHeroSkin();
		UpdataATK(playerData.Property.ATK.Value);
		UpdataDEF(playerData.Property.DEF.Value);
	}

	public void RefreshWatchFollowSwitchBtn(bool isFollow)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher())
		{
			base.ui.btn_WatchFollowSwitch.visible = true;
			base.ui.btn_WatchFollowSwitch.btn_switch.selected = isFollow;
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(isFollow ? 11033 : 11034);
		}
		else
		{
			base.ui.btn_WatchFollowSwitch.visible = false;
		}
	}

	private void OnClickWatchFollowSwitch()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.watch.SwitchFollow(base.ui.btn_WatchFollowSwitch.btn_switch.selected);
	}

	private void WatchFollowSwitch(bool isFollow)
	{
		RefreshWatchFollowSwitchBtn(isFollow);
	}

	private void ShowRelic()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.IsPVE())
		{
			base.ui.btn_ShowRelic.onClick.Retain();
			SimpleSingletonProvider<UIManager>.inst.battleRelicInfo.ShowRelic();
			base.ui.btn_ShowRelic.onClick.Release();
		}
	}

	public async void GuideOpenVictoryCondition()
	{
		Vector2 screenPos = base.ui.TransformPoint(base.ui.btn_VictoryCondition.xy, GRoot.inst);
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(screenPos, base.ui.btn_VictoryCondition.width, base.ui.btn_VictoryCondition.height, _needTransparentMask: false, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrow(screenPos, base.ui.btn_VictoryCondition.width / 3f, base.ui.btn_VictoryCondition.height);
	}

	private void RefreshCampaignVictory()
	{
		CampaignData campaignData = SimpleSingletonProvider<GameLogicManager>.inst.campaign.campaignData;
		if (campaignData?.campaignTaskData != null)
		{
			base.ui.com_CampaignTask.visible = true;
			base.ui.com_CampaignTask.txt_Task.text = campaignData.campaignTaskData.GetTaskDesc();
		}
	}

	public void RegisterAttrInfo(UICom_AttrInfo Com_AttrInfo)
	{
		base.ui.com_AttrInfos.AddChild(Com_AttrInfo);
		base.ui.InvalidateBatchingState();
	}

	public void RegisterPlayerAttrInfo(UICom_PlayerAttrInfo Com_PlayerAttrInfo)
	{
		base.ui.com_PlayerAttrInfos.AddChild(Com_PlayerAttrInfo);
		base.ui.InvalidateBatchingState();
	}

	public bool AttrContainerIsAncestorOf(DisplayObject obj)
	{
		if (base.ui == null || obj == null)
		{
			return false;
		}
		return base.ui.com_AttrInfos.container.IsAncestorOf(Stage.inst.touchTarget);
	}

	public void UpdatePlayerAttrCom()
	{
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (playerData.CharacterInst != null)
			{
				playerData.CharacterInst.window.UpdateAttrInfo();
			}
		}
	}

	public UIBattleInfo_Com_UpgradeTips RegisterUpgradeTips(Transform target)
	{
		UIBattleInfo_Com_UpgradeTips uIBattleInfo_Com_UpgradeTips = UIBattleInfo_Com_UpgradeTips.CreateInstance();
		uIBattleInfo_Com_UpgradeTips.ShowUpgradeTips(target);
		base.ui.com_UpgradeTips.AddChild(uIBattleInfo_Com_UpgradeTips);
		base.ui.InvalidateBatchingState();
		return uIBattleInfo_Com_UpgradeTips;
	}

	public void RemoveUpgradeTips(UIBattleInfo_Com_UpgradeTips Com_UpgradeTips)
	{
		if (Com_UpgradeTips != null)
		{
			base.ui.com_UpgradeTips.RemoveChild(Com_UpgradeTips, dispose: true);
		}
	}

	private void OnLongPressGestureBegin()
	{
		base.ui?.SwitchMonsterInfo(status: true);
	}

	private void OnLongPressGestureEnd()
	{
		base.ui?.SwitchMonsterInfo(status: false);
	}

	public async UniTask PlayerDebut()
	{
		await SimpleSingletonProvider<EffectManager>.inst.PreLoadEffect(8001301.GetEffectDataConfigure().EffectName);
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		for (int i = 0; i < playerDatas.Count; i++)
		{
			RoomPlayer player = playerDatas[i].player;
			SimpleSingletonProvider<GameLogicManager>.inst.action.signal.notifyAllPlayer.Dispatch(player.Id);
			int skinPendant = player.Hero.SkinPendant;
			if (skinPendant != 0)
			{
				SkinSkinPendantConfigure skinPendantConfig = player.standingPainting.ItemID.GetSkinPendantConfig();
				if (skinPendantConfig != null && skinPendantConfig.PendantId == skinPendant)
				{
					await SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo.PlayShowTime(player.Id, skinPendantConfig.ShowVideo, skinPendantConfig.ReplacePerform);
				}
			}
			else
			{
				await perform.PlayPlayerShow(player.Id, player.standingPainting.FanfarePerform, "玩家登场");
				if (perform.isCancel)
				{
					return;
				}
			}
		}
		RoomPlayer roomPlayer = playerDatas.GetSafeByIndex(0)?.player;
		if (roomPlayer != null)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.action.signal.notifyAllPlayer.Dispatch(roomPlayer.Id);
		}
	}

	private async UniTask PlayShowTime(long playerId, string showVideo, int perform)
	{
		showTimeTsc = new UniTaskCompletionSource();
		await SimpleSingletonProvider<CriMovieManager>.inst.Play(showVideo, base.ui.graph_ShowTime, null, delegate
		{
			showTimeTsc.TrySetResult();
			base.ui.graph_ShowTime.shape.visible = false;
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(base.ui.graph_ShowTime);
		}, delegate(EventPoint point, Player player)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			if (point.type == 0)
			{
				showTimeTsc.TrySetResult();
			}
			else if (point.type == 1)
			{
				if (point.paramString != IntPtr.Zero)
				{
					if (int.TryParse(Marshal.PtrToStringAnsi(point.paramString, (int)point.paramStringSize), out var result))
					{
						Stage.inst.PlayOneShotSound(result);
					}
				}
				else
				{
					Debug.LogError("IntPtr is null");
				}
			}
		});
		await showTimeTsc.Task;
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(playerId, perform, "玩家登场");
	}

	public Vector2 GetESCPosition()
	{
		Vector2 pt = base.ui.btn_ESC.LocalToGlobal(Vector2.zero);
		return GRoot.inst.GlobalToLocal(pt);
	}

	private async void ThrowDice(long playerId, int totalPoint, int movePoint, bool controlled)
	{
		base.ui.txt_DicePoint.text = totalPoint.ToString();
		base.ui.showDicePoint.Play();
		bool flag = await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => base.ui != null && base.ui.showDicePoint.playing);
		if (totalPoint != movePoint && !flag)
		{
			base.ui.txt_DicePoint.visible = false;
			base.ui.txt_DicePoint.text = movePoint.ToString();
			base.ui.showDicePoint.Play();
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => base.ui != null && base.ui.showDicePoint.playing);
		}
		if (!controlled)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.performTriggerLogic.signal.heroMovePointsSignal.Dispatch(playerId, movePoint);
		}
	}

	public async void GuideOpenPlayerDetail()
	{
		UIBattleInfo_Button_PlayerInfo com_Player_ = base.ui.com_BattlePlayer.com_Container.com_Player_1;
		Vector2 vector = base.ui.TransformPoint(com_Player_.xy, GRoot.inst);
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(vector + new Vector2(com_Player_.btn_Head.width, 0f), com_Player_.btn_Label.width, com_Player_.btn_Label.height, _needTransparentMask: false, isRect: true);
	}

	public void DisableCancelSelect()
	{
		if (base.ui.com_BattlePlayer.btn_CancelSelect != null)
		{
			base.ui.com_BattlePlayer.btn_CancelSelect.visible = false;
		}
	}

	public async void GuideSelectPlayer()
	{
		Vector2 pos = base.ui.TransformPoint(base.ui.com_BattlePlayer.com_Container.com_Player_2.xy, GRoot.inst);
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(pos, base.ui.com_BattlePlayer.com_Container.com_Player_2.width, base.ui.com_BattlePlayer.com_Container.com_Player_2.height, _needTransparentMask: false, isRect: true);
	}

	public void ShowPlayerBattleLabel()
	{
		UIBattleInfo_Button_PlayerInfo com_Player_ = base.ui.com_BattlePlayer.com_Container.com_Player_1;
		Vector2 pt = com_Player_.LocalToGlobal(Vector2.zero);
		Vector2 pos = GRoot.inst.GlobalToLocal(pt);
		float h = base.ui.com_BattlePlayer.com_Container.com_Player_3.y - com_Player_.y;
		SimpleSingletonProvider<UIManager>.inst.tutorial.ShowGuideMask(pos, com_Player_.width, h, isRect: true);
	}

	public void ShowPVEProgress()
	{
		if (com_PVEProgress != null)
		{
			Vector2 pt = com_PVEProgress.LocalToGlobal(Vector2.zero);
			Vector2 pos = GRoot.inst.GlobalToLocal(pt);
			float w = com_PVEProgress.width * com_PVEProgress.scaleX;
			float h = com_PVEProgress.height * com_PVEProgress.scaleY;
			SimpleSingletonProvider<UIManager>.inst.tutorial.ShowGuideMask(pos, w, h, isRect: true);
		}
	}

	public (Vector2, float, float) ShowOpenBattleInfo()
	{
		GGraph btn_Label = base.ui.com_BattlePlayer.com_Container.com_Player_1.btn_Label;
		Vector2 pt = btn_Label.LocalToGlobal(Vector2.zero);
		Vector2 vector = GRoot.inst.GlobalToLocal(pt);
		SimpleSingletonProvider<UIManager>.inst.tutorial.ShowGuideMask(vector, btn_Label.width, btn_Label.height, isRect: true);
		return (vector, btn_Label.width, btn_Label.height);
	}

	public void RefreshPVEProgress(MapField<int, int> delayProgressMapEvent)
	{
		if (com_PVEProgress != null)
		{
			com_PVEProgress.AddProgressMapEvent(delayProgressMapEvent);
			com_PVEProgress.RefreshProgress();
		}
	}

	private void ShowTerms()
	{
		base.ui.CT_Cut_in.Play();
		SimpleSingletonProvider<UIManager>.inst.RoomTerms.ShowWinScreenTerms(roomInfo.RoomTerms, delegate
		{
			if (base.ui != null)
			{
				base.ui.CT_Cut_out.Play();
				base.ui.btn_terms.visible = true;
			}
		}).Forget();
	}

	private void OnMarkEnter()
	{
		base.ui.btn_ShowRelic.OnMarkTipShow();
		if (com_PVEProgress != null)
		{
			com_PVEProgress.OnMarkTipShow();
		}
		base.ui.com_BattlePlayer.OnMarkTipShow();
	}

	private void OnMarkExit()
	{
		base.ui.btn_ShowRelic.OnMarkTipHide();
		if (com_PVEProgress != null)
		{
			com_PVEProgress.OnMarkTipHide();
		}
		base.ui.com_BattlePlayer.OnMarkTipHide();
	}

	private void Minimap_Refresh()
	{
		if (!PlatformTarget.IsMobileTarget)
		{
			rotateMat = Matrix4x4.identity;
			rotateMat[0, 0] = Mathf.Cos((float)Math.PI / 4f);
			rotateMat[0, 1] = Mathf.Sin((float)Math.PI / 4f);
			rotateMat[1, 0] = 0f - Mathf.Sin((float)Math.PI / 4f);
			rotateMat[1, 1] = Mathf.Cos((float)Math.PI / 4f);
			mapCenter = BattleSceneController.inst.mapRangeManage.miniMapCenter;
			mapFarthest = BattleSceneController.inst.mapRangeManage.miniMapFarthest;
			max_Distance = 2f * Distance(mapCenter, mapFarthest);
			miniMapwidth = base.ui.com_MapInfo.com_Minimap.width;
			miniMapheight = base.ui.com_MapInfo.com_Minimap.height;
			CreateMinimap();
			miniMapIcons.Clear();
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
			{
				base.ui.com_MapInfo.btn_ShowMap.visible = false;
			}
		}
	}

	private void Minimap_AddEvent()
	{
		if (!PlatformTarget.IsMobileTarget)
		{
			base.ui.com_MapInfo.com_Minimap.onClick.Add(OnClickMiniMap);
			base.ui.com_MapInfo.btn_ShowMap.onClick.Add(SwitchMap);
		}
	}

	private void Minimap_RemoveEvent()
	{
		if (!PlatformTarget.IsMobileTarget)
		{
			base.ui.com_MapInfo.com_Minimap.onClick.Remove(OnClickMiniMap);
			base.ui.com_MapInfo.btn_ShowMap.onClick.Remove(SwitchMap);
		}
	}

	private void SwitchMap(EventContext context)
	{
		if (!PlatformTarget.IsMobileTarget)
		{
			base.ui.com_MapInfo.showMap.selectedIndex = ((base.ui.com_MapInfo.showMap.selectedIndex == 0) ? 1 : 0);
		}
	}

	private void Minimap_AddListener()
	{
		if (!PlatformTarget.IsMobileTarget)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapMove.AddListener(Minimap_Move);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapUpdate.AddListener(Minimap_Update);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapDelete.AddListener(Minimap_DeleteItem);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.limitCameraControl.AddListener(limitOnClickMinimap);
		}
	}

	private void Minimap_RemoveListener()
	{
		if (!PlatformTarget.IsMobileTarget)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapMove.RemoveListener(Minimap_Move);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapUpdate.RemoveListener(Minimap_Update);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapDelete.RemoveListener(Minimap_DeleteItem);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.limitCameraControl.RemoveListener(limitOnClickMinimap);
		}
	}

	private void MinimapDispose()
	{
		cts?.Dispose();
		foreach (KeyValuePair<long, UIBattleInfo_Com_MiniMap_Icon> miniMapIcon in miniMapIcons)
		{
			miniMapIcon.Value.Dispose();
		}
	}

	private void CreateMinimap()
	{
		miniLandPos.Clear();
		foreach (KeyValuePair<int, UnitLand> item in SimpleSingletonProvider<LandManager>.inst.NodeDict)
		{
			Vector3 position = item.Value.transform.position;
			Vector2 vector = new Vector2(position.x - mapCenter.x, position.z - mapCenter.z);
			Vector4 vector2 = rotateMat * vector;
			float num = vector2.x / max_Distance * miniMapwidth + miniMapwidth / 2f;
			float num2 = vector2.y / max_Distance * miniMapheight + miniMapheight / 2f;
			miniLandPos.Add(item.Key, new Vector2(num, miniMapheight - num2));
			UIBattleInfo_Com_MiniMap_Land uIBattleInfo_Com_MiniMap_Land = UIBattleInfo_Com_MiniMap_Land.CreateInstance();
			base.ui.com_MapInfo.com_Minimap.AddChild(uIBattleInfo_Com_MiniMap_Land);
			if (item.Value.LandType == LandType.Born)
			{
				uIBattleInfo_Com_MiniMap_Land.player.selectedIndex = item.Value.PlayerSerialNumber + 1;
			}
			else
			{
				uIBattleInfo_Com_MiniMap_Land.player.selectedIndex = 0;
			}
			uIBattleInfo_Com_MiniMap_Land.SetXY(num, miniMapheight - num2);
		}
	}

	private static float Distance(Vector3 p1, Vector3 p2)
	{
		float num = p1.x - p2.x;
		float num2 = p1.z - p2.z;
		return Mathf.Sqrt(num * num + num2 * num2);
	}

	private void OnClickMiniMap(EventContext context)
	{
		base.ui.com_MapInfo.com_Minimap.onClick.Retain();
		Vector2 localPos = ((GObject)context.sender).GlobalToLocal(context.inputEvent.position);
		ShowScopeFrame(localPos);
		Vector2 vector = new Vector2(localPos.x - miniMapwidth / 2f, miniMapheight / 2f - localPos.y);
		Vector2 vector2 = new Vector2(vector.x / miniMapwidth * max_Distance, vector.y / miniMapheight * max_Distance);
		Vector4 vector3 = rotateMat.inverse * vector2;
		Vector3 pos = new Vector3(vector3.x + mapCenter.x, 0f, vector3.y + mapCenter.z);
		SimpleSingletonProvider<CameraManager>.inst.ControlFreeCamera(pos);
		base.ui.com_MapInfo.com_Minimap.onClick.Release();
	}

	private async void ShowScopeFrame(Vector2 localPos)
	{
		base.ui.com_MapInfo.com_Minimap.image_Frame.SetXY(localPos.x, localPos.y);
		base.ui.com_MapInfo.com_Minimap.image_Frame.visible = true;
		if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000)))
		{
			base.ui.com_MapInfo.com_Minimap.image_Frame.visible = false;
		}
	}

	private void CreateMinimapIcon(BattlePlayerData data)
	{
		if (!(data.CharacterInst == null) && (data.characterType != CharacterType.Monster || data.Property.HP.Value != 0) && !miniMapIcons.ContainsKey(data.player.Id))
		{
			UIBattleInfo_Com_MiniMap_Icon uIBattleInfo_Com_MiniMap_Icon = UIBattleInfo_Com_MiniMap_Icon.CreateInstance();
			miniMapIcons.Add(data.player.Id, uIBattleInfo_Com_MiniMap_Icon);
			base.ui.com_MapInfo.com_Minimap.AddChild(uIBattleInfo_Com_MiniMap_Icon);
			int num = ((data.CharacterInst == null) ? data.player.NodeId : data.CharacterInst.standLand.Id);
			uIBattleInfo_Com_MiniMap_Icon.InitData(data, miniLandPos[num], num);
		}
	}

	private void Minimap_Move(long playerId, int landID)
	{
		if (miniMapIcons.TryGetValue(playerId, out var value))
		{
			value.Move(miniLandPos[landID], landID);
		}
		Minimap_Deploy();
	}

	private void Minimap_Update()
	{
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			CreateMinimapIcon(playerData);
		}
	}

	private void Minimap_DeleteItem(long playerId)
	{
		if (miniMapIcons.TryGetValue(playerId, out var value))
		{
			value.Dispose();
			miniMapIcons.Remove(playerId);
		}
	}

	private void limitOnClickMinimap(bool state)
	{
		if (state)
		{
			base.ui.com_MapInfo.com_Minimap.onClick.Retain();
		}
		else
		{
			base.ui.com_MapInfo.com_Minimap.onClick.Release();
		}
	}

	private void Minimap_Deploy()
	{
		foreach (KeyValuePair<long, UIBattleInfo_Com_MiniMap_Icon> miniMapIcon in miniMapIcons)
		{
			miniMapIcon.Value.SetDeploy(miniMapIcons);
		}
	}
}
