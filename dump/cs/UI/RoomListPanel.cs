using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace UI;

public class RoomListPanel : BasePanel<UIRoomListPanel>
{
	private const float REFRESH_COOLDOWN = 5f;

	private SwipeGesture _swipeGesture;

	private int requestQuickJoinTime;

	private List<RoomShortInfo> curPageRoomInfos;

	private int currentPageIndex;

	private const int pageSize = 12;

	private readonly List<GameModeInfoConfigure> gameModeInfos = new List<GameModeInfoConfigure>();

	private UICom_CreateRoom Com_CreateRoom => base.ui.com_CreateRoom as UICom_CreateRoom;

	public RoomListPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIRoomListPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		_swipeGesture = new SwipeGesture(GRoot.inst);
		if (objs != null && objs.Length != 0 && objs[0] is RepeatedField<int> { Count: >0 } repeatedField)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.roomList.mapModeType = repeatedField[0];
		}
		RequestRefreshList();
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		RefreshGameMode();
		base.ui.tab.selectedIndex = 0;
		base.ui.btn_JoinTargetRoom.grayed = true;
		base.ui.btn_JoinTargetRoom.touchable = false;
		base.ui.txtField_Search.text = "";
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.list_Room.itemRenderer = RendererRoomInfo;
		base.ui.list_GameMode.itemRenderer = RendererGameMode;
		Com_CreateRoom.CreateRoom_InitComponents();
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.btn_OpenCreateRoom.onClick.Add(OpenRoomCreate);
		base.ui.btn_JoinTargetRoom.onClick.Add(JoinTargetRoom);
		base.ui.btn_Refresh.onClick.Add(RefreshData);
		base.ui.list_Room.onClickItem.Add(DoubleListItem);
		base.ui.btn_prePage.onClick.Add(PreviousPage);
		base.ui.btn_nextPage.onClick.Add(NextPage);
		base.ui.txtField_Search.onChanged.Add(OnInputTextChanged);
		base.ui.com_QuickJoin.btn_QuickJoin.onClick.Add(QuickJoinRoom);
		base.ui.com_QuickJoin.btn_SelectDifficulty.onClick.Add(SelectDifficulty);
		base.ui.btn_Watch.onClick.Add(OpenWatch);
		base.ui.list_GameMode.onClickItem.Add(SwitchGameMode);
		base.ui.btn_ExplainMode.onClick.Add(ExplainMode);
		Com_CreateRoom.CreateRoom_AddEvent();
		if (_swipeGesture != null)
		{
			_swipeGesture.onAction.Add(OnSwipeGestureRefreshRoom);
		}
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.btn_OpenCreateRoom.onClick.Remove(OpenRoomCreate);
		base.ui.btn_JoinTargetRoom.onClick.Remove(JoinTargetRoom);
		base.ui.btn_Refresh.onClick.Remove(RefreshData);
		base.ui.list_Room.onClickItem.Remove(DoubleListItem);
		base.ui.btn_prePage.onClick.Remove(PreviousPage);
		base.ui.btn_nextPage.onClick.Remove(NextPage);
		base.ui.txtField_Search.onChanged.Remove(OnInputTextChanged);
		base.ui.com_QuickJoin.btn_QuickJoin.onClick.Remove(QuickJoinRoom);
		base.ui.com_QuickJoin.btn_SelectDifficulty.onClick.Remove(SelectDifficulty);
		base.ui.btn_Watch.onClick.Remove(OpenWatch);
		base.ui.list_GameMode.onClickItem.Remove(SwitchGameMode);
		base.ui.btn_ExplainMode.onClick.Remove(ExplainMode);
		Com_CreateRoom.CreateRoom_RemoveEvent();
		if (_swipeGesture != null)
		{
			_swipeGesture.onAction.Remove(OnSwipeGestureRefreshRoom);
		}
	}

	protected override void AddListener()
	{
		base.AddListener();
		Com_CreateRoom.CreateRoom_AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		Com_CreateRoom.CreateRoom_RemoveListener();
	}

	public override void Close()
	{
		if (base.ui != null)
		{
			base.ui.list_Room.numItems = 0;
			Com_CreateRoom.CreateRoom_Close();
		}
		base.Close();
		if (_swipeGesture != null)
		{
			_swipeGesture.Dispose();
			_swipeGesture = null;
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		SimpleSingletonProvider<GameLogicManager>.inst.roomList.mapModeType = 0;
		base.ui.btn_Return.onClick.Release();
	}

	private async void OpenWatch(EventContext context)
	{
		base.ui.btn_Watch.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Watch);
		base.ui.btn_Watch.onClick.Release();
	}

	private void DoubleListItem(EventContext context)
	{
		if (context.data is UIRoomList_Item_Button uIRoomList_Item_Button)
		{
			if (base.ui.list_Room.selectedIndex == -1)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1000);
				return;
			}
			base.ui.list_Room.onClickItem.Retain();
			uIRoomList_Item_Button.ClickScale.Play();
			RoomShortInfo roomLabelById = SimpleSingletonProvider<GameLogicManager>.inst.roomList.GetRoomLabelById((long)uIRoomList_Item_Button.data);
			JoinPanel(roomLabelById);
		}
	}

	public void JoinPanel(RoomShortInfo info)
	{
		if (info == null)
		{
			base.ui.list_Room.onClickItem.Release();
			return;
		}
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		int unLockDifficulty = playerInfo.UnLockDifficulty;
		if (playerInfo != null && playerInfo.Level < StaticGlobalData.ROOM_PVELOCK_LEVEL && info.Difficulty >= StaticGlobalData.ROOM_PVELOCK_DIFFICULTY && info.Difficulty > unLockDifficulty)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(11025);
			base.ui.list_Room.onClickItem.Release();
		}
		else if (!SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense())
		{
			base.ui.list_Room.onClickItem.Release();
		}
		else if (info.PlayerCount >= RoomInfo.GetRoomPlayerMaxCount(info.MapType))
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1003);
			base.ui.list_Room.onClickItem.Release();
		}
		else if (!info.IsPwd)
		{
			RequestJoinByPwd(info.Id, "", info.RoomServerId);
		}
		else
		{
			ShowInputPswTip(info.Id, info.RoomServerId);
			base.ui.list_Room.onClickItem.Release();
		}
	}

	private void TryRefreshList(RPCAsyncResult result)
	{
		if (result.errId != 0)
		{
			RequestRefreshList();
		}
	}

	private void OpenRoomCreate()
	{
		base.ui.btn_OpenCreateRoom.onClick.Retain();
		base.ui.tab.selectedIndex = 1;
		Com_CreateRoom.CreateRoom_Refresh((MapModeType)SimpleSingletonProvider<GameLogicManager>.inst.roomList.mapModeType, 0, delegate
		{
			base.ui.tab.selectedIndex = 0;
		});
		base.ui.btn_OpenCreateRoom.onClick.Release();
	}

	public void ShowInputPswTip(long roomId, int roomServerId)
	{
		SimpleSingletonProvider<UIManager>.inst.input.OpenPwd(delegate(string pwd)
		{
			RequestJoinByPwd(roomId, pwd, roomServerId);
		});
	}

	private void RequestJoinByPwd(long roomId, string pwd, int roomServerId)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
		{
			base.ui.list_Room.onClickItem.Release();
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestJoinRoomC2S(roomId, pwd, roomServerId).OnFinished.AddOnce(delegate(RPCAsyncResult _result)
		{
			TryRefreshList(_result);
			base.ui.list_Room.onClickItem.Release();
		});
	}

	private void JoinTargetRoom()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch() && long.TryParse(base.ui.txtField_Search.text, out var result))
		{
			base.ui.btn_JoinTargetRoom.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.roomList.RequestJoinTargetRoom(result).OnFinishedOnly.AddOnce(delegate
			{
				base.ui.btn_JoinTargetRoom.onClick.Release();
			});
		}
	}

	private void OnInputTextChanged()
	{
		if (string.IsNullOrEmpty(base.ui.txtField_Search.text))
		{
			base.ui.btn_JoinTargetRoom.grayed = true;
			base.ui.btn_JoinTargetRoom.touchable = false;
		}
		else
		{
			base.ui.btn_JoinTargetRoom.grayed = false;
			base.ui.btn_JoinTargetRoom.touchable = true;
		}
	}

	private async void QuickJoinRoom(EventContext context)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense() && SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
		{
			if (BattleConfig.IsPVE(SimpleSingletonProvider<GameLogicManager>.inst.roomList.mapModeType) && LocalCache.GetPVEDifficulty() == 0)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1059);
				return;
			}
			base.ui.com_QuickJoin.btn_QuickJoin.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShow(_delayStatus: false);
			requestQuickJoinTime = 0;
			SimpleSingletonProvider<GameLogicManager>.inst.roomList.RequestQuickJoin().OnFinished.AddOnce(DealQuickJoin);
		}
	}

	private async void DealQuickJoin(RPCAsyncResult result)
	{
		requestQuickJoinTime++;
		if (result.errId != 0)
		{
			if (requestQuickJoinTime > 3)
			{
				SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
				base.ui.com_QuickJoin.btn_QuickJoin.onClick.Release();
				MonoSingletonProvider<NetManager>.inst.HandleRPCCustomACKErrorCode(result.errId);
				return;
			}
			float time = 0f;
			do
			{
				await UniTask.NextFrame();
				if (SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom || SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.InTeam || SimpleSingletonProvider<UIManager>.inst.invite.isShowing)
				{
					SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
					base.ui.com_QuickJoin.btn_QuickJoin.onClick.Release();
					return;
				}
				time += Time.deltaTime;
			}
			while (!(time >= 3f));
			SimpleSingletonProvider<GameLogicManager>.inst.roomList.RequestQuickJoin().OnFinished.AddOnce(DealQuickJoin);
		}
		else
		{
			base.ui.com_QuickJoin.btn_QuickJoin.onClick.Release();
			SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
		}
	}

	private void RefreshData()
	{
		if (Time.time - SimpleSingletonProvider<GameLogicManager>.inst.roomList.GetCurrentRefreshTime() < 5f)
		{
			if (!SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
			}
			if (curPageRoomInfos != null)
			{
				List<RoomShortInfo> list = curPageRoomInfos;
				if (list != null && list.Count > 0)
				{
					base.ui.list_Room.numItems = 0;
					base.ui.list_Room.numItems = curPageRoomInfos.Count;
				}
			}
		}
		else
		{
			RequestRefreshList();
		}
	}

	private async void RequestRefreshList()
	{
		base.ui.btn_Refresh.onClick.Retain();
		base.ui.list_GameMode.onClickItem.Retain();
		if (!SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
		{
			await SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShow();
		}
		SimpleSingletonProvider<GameLogicManager>.inst.roomList.RequestQueryRoomC2S().OnFinishedOnly.AddOnce(InitRoomLabelList);
	}

	private void InitRoomLabelList()
	{
		base.ui.tab.selectedIndex = 0;
		currentPageIndex = 0;
		RefreshRoomLabel(0);
		base.ui.btn_Refresh.onClick.Release();
		base.ui.list_GameMode.onClickItem.Release();
		SimpleSingletonProvider<GameLogicManager>.inst.roomList.UpdateCurrentRefreshTime(Time.time);
		SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
	}

	private void RendererRoomInfo(int index, GObject item)
	{
		if (item is UIRoomList_Item_Button uIRoomList_Item_Button && curPageRoomInfos.Count > index)
		{
			RoomShortInfo roomShortInfo = curPageRoomInfos[index];
			int roomPlayerMaxCount = RoomInfo.GetRoomPlayerMaxCount(roomShortInfo.MapType);
			uIRoomList_Item_Button.state.selectedIndex = ((roomShortInfo.PlayerCount == roomPlayerMaxCount) ? 2 : (roomShortInfo.IsPwd ? 1 : 0));
			uIRoomList_Item_Button.txt_Num.text = roomShortInfo.PlayerCount.ToString();
			uIRoomList_Item_Button.txt_MaxNum.text = roomPlayerMaxCount.ToString();
			MapInfoConfigure mapDataConfigure = roomShortInfo.MapId.GetMapDataConfigure();
			uIRoomList_Item_Button.txt_MapName.text = mapDataConfigure.MapName.GetLocal(UIStringType.Map);
			RefreshMasterLabel(uIRoomList_Item_Button, roomShortInfo.HeadIcon, roomShortInfo.MasterBackBoard);
			RefreshRoomInfo(uIRoomList_Item_Button, roomShortInfo);
			uIRoomList_Item_Button.data = roomShortInfo.Id;
			bool visible = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(roomShortInfo.MasterId);
			uIRoomList_Item_Button.image_isBlack.visible = visible;
		}
	}

	private void RefreshMasterLabel(UIRoomList_Item_Button label, int HeadIconId, int labelId)
	{
		HeadIconId = ((HeadIconId == 0) ? SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[1] : HeadIconId);
		label.loader_HeadIcon.url = HeadIconId.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
		labelId = ((labelId == 0) ? SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[2] : labelId);
		(string, bool) playerLabel = labelId.GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
		UIRoomList_PlayerLabel_Loader com_PlayerLabel = label.com_PlayerLabel;
		if (playerLabel.Item2)
		{
			com_PlayerLabel.type.selectedIndex = 1;
			if (!((string)com_PlayerLabel.loader_Video.data == playerLabel.Item1))
			{
				com_PlayerLabel.loader_Video.data = playerLabel.Item1;
				CommonUIManager.TryAddVideoGraph(UIType.Panel, (int)base.config.PanelType, com_PlayerLabel.loader_Video);
				SimpleSingletonProvider<CriMovieManager>.inst.Play(playerLabel.Item1, com_PlayerLabel.loader_Video).Forget();
			}
		}
		else
		{
			com_PlayerLabel.loader_Video.data = "";
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(com_PlayerLabel.loader_Video);
			com_PlayerLabel.type.selectedIndex = 0;
			com_PlayerLabel.loader_Image.url = playerLabel.Item1;
		}
	}

	private void RefreshRoomInfo(UIRoomList_Item_Button label, RoomShortInfo roomInfo)
	{
		label.list_RoomSetting.numItems = 0;
		if (StaticConfigure.ChoosingTimeLimit.InfoDict.TryGetValue(roomInfo.TimePlan, out var value))
		{
			UIRoomList_Com_ThinkTime uIRoomList_Com_ThinkTime = UIRoomList_Com_ThinkTime.CreateInstance();
			uIRoomList_Com_ThinkTime.txt_TimePlan.text = value.DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit);
			label.list_RoomSetting.AddChild(uIRoomList_Com_ThinkTime);
		}
		if (!BattleConfig.IsPVE(roomInfo.MapType) && StaticConfigure.Upgrade.DataDict.TryGetValue(roomInfo.UpgradePlan, out var value2))
		{
			UIRoomList_Com_GoldCount uIRoomList_Com_GoldCount = UIRoomList_Com_GoldCount.CreateInstance();
			uIRoomList_Com_GoldCount.txt_UpgradePlan.text = value2.GoldSTRid.GetLocal(UIStringType.ChoosingTimeLimit);
			label.list_RoomSetting.AddChild(uIRoomList_Com_GoldCount);
		}
		if (BattleConfig.IsPVE(roomInfo.MapType))
		{
			ChoosingTimeLimitdifficultyConfigure choosingTimeLimitDifficultyConfigure = roomInfo.Difficulty.GetChoosingTimeLimitDifficultyConfigure();
			UIRoomList_Com_Difficulty uIRoomList_Com_Difficulty = UIRoomList_Com_Difficulty.CreateInstance();
			uIRoomList_Com_Difficulty.txt_Content.text = choosingTimeLimitDifficultyConfigure.DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit);
			uIRoomList_Com_Difficulty.setColor.selectedIndex = ((roomInfo.Difficulty == 3) ? 1 : 0);
			label.list_RoomSetting.AddChild(uIRoomList_Com_Difficulty);
		}
		if (StaticConfigure.ChoosingTimeLimit.RoomsettingDict.TryGetValue(roomInfo.MapType, out var value3) && value3.HasLabel.Count > roomInfo.RoomLabel && roomInfo.RoomLabel != 0)
		{
			int num = value3.HasLabel[roomInfo.RoomLabel];
			if (num != 0)
			{
				UIRoomList_Com_Difficulty uIRoomList_Com_Difficulty2 = UIRoomList_Com_Difficulty.CreateInstance();
				uIRoomList_Com_Difficulty2.setColor.selectedIndex = 1;
				uIRoomList_Com_Difficulty2.txt_Content.text = num.GetLocal(UIStringType.ChoosingTimeLimit);
				label.list_RoomSetting.AddChild(uIRoomList_Com_Difficulty2);
			}
		}
	}

	private void PreviousPage()
	{
		base.ui.btn_prePage.onClick.Retain();
		currentPageIndex = Mathf.Max(currentPageIndex - 1, 0);
		int startIndex = currentPageIndex * 12;
		RefreshRoomLabel(startIndex);
		base.ui.btn_prePage.onClick.Release();
	}

	private void NextPage()
	{
		base.ui.btn_nextPage.onClick.Retain();
		currentPageIndex++;
		int startIndex = currentPageIndex * 12;
		RefreshRoomLabel(startIndex);
		base.ui.btn_nextPage.onClick.Release();
	}

	private void RefreshRoomLabel(int startIndex)
	{
		base.ui.list_Room.numItems = 0;
		List<RoomShortInfo> roomLabelInfos = SimpleSingletonProvider<GameLogicManager>.inst.roomList.GetRoomLabelInfos();
		if (roomLabelInfos == null)
		{
			Debug.LogError("Try get room data is failure in RoomListPanel");
			return;
		}
		int num = Mathf.Min(startIndex + 12, roomLabelInfos.Count);
		curPageRoomInfos = roomLabelInfos.GetRange(startIndex, num - startIndex);
		base.ui.list_Room.numItems = curPageRoomInfos.Count;
		UpdatePageBtn(roomLabelInfos.Count);
	}

	private void UpdatePageBtn(int total)
	{
		bool visible = currentPageIndex > 0;
		bool visible2 = currentPageIndex * 12 + 12 < total;
		base.ui.btn_prePage.visible = visible;
		base.ui.btn_nextPage.visible = visible2;
	}

	private void RefreshGameMode()
	{
		List<GameModeInfoConfigure> list = SimpleSingletonProvider<GameLogicManager>.inst.roomList.gameModeInfos;
		gameModeInfos.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			if (TimeHelper.ValidityTime(list[i].BeginTime, list[i].EndTime))
			{
				gameModeInfos.Add(list[i]);
			}
		}
		int mapModeType = SimpleSingletonProvider<GameLogicManager>.inst.roomList.mapModeType;
		RefreshQuickJoin(mapModeType);
		base.ui.list_GameMode.numItems = gameModeInfos.Count;
		if (StaticConfigure.GameMode.InfoDict.TryGetValue(mapModeType, out var value))
		{
			base.ui.btn_ExplainMode.visible = value.RulesID != 0;
		}
	}

	private void RendererGameMode(int index, GObject item)
	{
		if (item is UIRoomList_Button_GameMode uIRoomList_Button_GameMode)
		{
			GameModeInfoConfigure gameModeInfoConfigure = gameModeInfos[index];
			uIRoomList_Button_GameMode.txt_Title.text = gameModeInfoConfigure.NameID.GetLocal(UIStringType.GameMode);
			uIRoomList_Button_GameMode.txt_Explain.text = gameModeInfoConfigure.ModeDesID.GetLocal(UIStringType.GameMode);
			uIRoomList_Button_GameMode.GameMode.selectedIndex = (BattleConfig.IsPVE(gameModeInfoConfigure.MapModeType) ? 1 : 0);
			uIRoomList_Button_GameMode.selected = SimpleSingletonProvider<GameLogicManager>.inst.roomList.mapModeType == (int)gameModeInfoConfigure.MapModeType;
		}
	}

	private void SwitchGameMode()
	{
		GameModeInfoConfigure gameModeInfoConfigure = gameModeInfos[base.ui.list_GameMode.selectedIndex];
		if (!TimeHelper.ValidityTime(gameModeInfoConfigure.BeginTime, gameModeInfoConfigure.EndTime))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.roomList.mapModeType = 0;
			RefreshGameMode();
			RequestRefreshList();
			return;
		}
		base.ui.btn_ExplainMode.visible = gameModeInfos[base.ui.list_GameMode.selectedIndex].RulesID != 0;
		int mapModeType = SimpleSingletonProvider<GameLogicManager>.inst.roomList.mapModeType;
		if (gameModeInfoConfigure.MapModeType == (MapModeType)mapModeType)
		{
			return;
		}
		RefreshQuickJoin((int)gameModeInfoConfigure.MapModeType);
		SimpleSingletonProvider<GameLogicManager>.inst.roomList.mapModeType = (int)gameModeInfoConfigure.MapModeType;
		if (Time.time - SimpleSingletonProvider<GameLogicManager>.inst.roomList.GetCurrentRefreshTime() < 5f)
		{
			base.ui.list_GameMode.onClickItem.Retain();
			if (!SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
			}
			base.ui.tab.selectedIndex = 0;
			currentPageIndex = 0;
			RefreshRoomLabel(0);
			base.ui.list_GameMode.onClickItem.Release();
		}
		else
		{
			RequestRefreshList();
		}
	}

	private async void ExplainMode()
	{
		base.ui.btn_ExplainMode.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.rule.TryShow(gameModeInfos[base.ui.list_GameMode.selectedIndex].NameID.GetLocal(UIStringType.GameMode), gameModeInfos[base.ui.list_GameMode.selectedIndex].RulesID.GetLocal(UIStringType.GameMode));
		base.ui.btn_ExplainMode.onClick.Release();
	}

	private void RefreshQuickJoin(int mapMode)
	{
		base.ui.com_QuickJoin.GameMode.selectedIndex = (BattleConfig.IsPVE(mapMode) ? 1 : 0);
	}

	private async void SelectDifficulty()
	{
		base.ui.com_QuickJoin.btn_SelectDifficulty.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.roomFilter.ShowDifficulty();
		base.ui.com_QuickJoin.btn_SelectDifficulty.onClick.Release();
	}

	private void OnSwipeGestureRefreshRoom(EventContext context)
	{
		if (base.ui.tab.selectedIndex == 1 || _swipeGesture == null || !(Mathf.Abs(Vector2.Angle(_swipeGesture.velocity, Vector2.right) - 90f) > 45f))
		{
			return;
		}
		if (_swipeGesture.velocity.x < 0f)
		{
			if (base.ui.btn_nextPage.visible)
			{
				NextPage();
			}
		}
		else if (_swipeGesture.velocity.x > 0f && base.ui.btn_prePage.visible)
		{
			PreviousPage();
		}
	}
}
