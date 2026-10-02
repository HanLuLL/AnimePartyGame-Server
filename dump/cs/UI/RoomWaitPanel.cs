using System;
using System.Collections.Generic;
using GameLogic;
using Tools;
using UnityEngine;
using UnityTimer;
using party.protocol;

namespace UI;

public class RoomWaitPanel : BasePanel<UIRoomWaitPanel>
{
	private Timer roomTimer;

	private Timer masterKickTimer;

	public RoomWaitPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIRoomWaitPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		SimpleSingletonProvider<UIManager>.inst.systemTips.CloseChatSignal();
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		GetRoomSetting().InitComponents();
		((UICom_RoomPlayer)base.ui.com_Player).InitComponents();
	}

	public override void Refresh()
	{
		base.Refresh();
		ShowMode();
		GetRoomSetting().Wait_Refresh();
		RefreshMaster();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		GetRoomSetting().AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.btn_Start.onClick.Add(RequsetStart);
		base.ui.btn_Leave.onClick.Add(LevelRoom);
		((UICom_RoomPlayer)base.ui.com_Player).AddEvent();
		base.ui.btn_Ready.onClick.Add(SwitchRoomReady);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		GetRoomSetting().RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.btn_Start.onClick.Remove(RequsetStart);
		base.ui.btn_Leave.onClick.Remove(LevelRoom);
		((UICom_RoomPlayer)base.ui.com_Player).RemoveEvent();
		base.ui.btn_Ready.onClick.Remove(SwitchRoomReady);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomPlayerChange.AddListener(RefreshWaitPlayerList);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomSettingRefresh.AddListener(RefreshRoomSetting);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.masterChange.AddListener(RefreshMaster);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomPlayerChange.RemoveListener(RefreshWaitPlayerList);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomSettingRefresh.RemoveListener(RefreshRoomSetting);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.masterChange.RemoveListener(RefreshMaster);
	}

	public override void Close()
	{
		((UICom_RoomPlayer)base.ui.com_Player).Close();
		CancelTime();
		GetRoomSetting().Close();
		base.Close();
	}

	public override void Dispose()
	{
		((UICom_RoomPlayer)base.ui.com_Player).DisposeCom();
		GetRoomSetting().Dispose();
		base.Dispose();
	}

	private UICom_RoomSetting GetRoomSetting()
	{
		return (UICom_RoomSetting)base.ui.com_SetRoom;
	}

	private async void RequsetStart()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		foreach (RoomPlayer player in curRoomInfo.Players)
		{
			if (player.Id != curRoomInfo.MasterId && !player.RoomReady)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1021);
				return;
			}
		}
		if (curRoomInfo.Players.Count < curRoomInfo.RoomPlayerMaxCount)
		{
			if (curRoomInfo.Players.Count == 1)
			{
				await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1044, delegate
				{
					CancelTime();
					base.ui.btn_Start.onClick.Retain();
					SimpleSingletonProvider<GameLogicManager>.inst.room.RequestStartGameC2S(isAddBot: true).OnFinishedOnly.AddOnce(delegate
					{
						base.ui.btn_Start.onClick.Release();
					});
				});
			}
			else if (curRoomInfo.MapType != 7)
			{
				await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1017, delegate
				{
					CancelTime();
					base.ui.btn_Start.onClick.Retain();
					SimpleSingletonProvider<GameLogicManager>.inst.room.RequestStartGameC2S(isAddBot: true).OnFinishedOnly.AddOnce(delegate
					{
						base.ui.btn_Start.onClick.Release();
					});
				});
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1027);
			}
		}
		else
		{
			CancelTime();
			base.ui.btn_Start.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.room.RequestStartGameC2S(isAddBot: false).OnFinishedOnly.AddOnce(delegate
			{
				base.ui.btn_Start.onClick.Release();
			});
		}
	}

	private void SwitchRoomReady()
	{
		base.ui.btn_Ready.onClick.Retain();
		bool roomReady = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.GetPlayerById(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID()).RoomReady;
		base.ui.btn_Ready.status.selectedIndex = ((!roomReady) ? 1 : 0);
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestRoomReadyC2S(!roomReady).OnFinishedOnly.AddOnce(delegate
		{
			base.ui.btn_Ready.onClick.Release();
		});
	}

	private void RefreshRoomSetting()
	{
		AdjustStartTimer();
		GetRoomSetting().Wait_Refresh();
		ShowMode();
		RefreshButtonReadyStatus();
	}

	private void RefreshWaitPlayerList()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		((UICom_RoomPlayer)base.ui.com_Player).RefreshPlayer(curRoomInfo.Players, curRoomInfo.MasterId, (MapModeType)curRoomInfo.MapType);
		base.ui.roomMaster.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(curRoomInfo.MasterId) ? 1 : 0);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(curRoomInfo.MasterId))
		{
			AdjustStartTimer();
		}
	}

	private void AdjustStartTimer()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (!string.IsNullOrWhiteSpace(curRoomInfo.Pwd))
		{
			base.ui.masterKick.selectedIndex = 0;
			Timer obj = masterKickTimer;
			if (obj != null)
			{
				obj.Cancel();
			}
			return;
		}
		if (curRoomInfo.Players.Count < curRoomInfo.RoomPlayerMaxCount)
		{
			base.ui.masterKick.selectedIndex = 0;
			Timer obj2 = masterKickTimer;
			if (obj2 != null)
			{
				obj2.Cancel();
			}
			return;
		}
		List<long> member = new List<long>();
		for (int i = 0; i < curRoomInfo.Players.Count; i++)
		{
			if (curRoomInfo.Players[i].Id != curRoomInfo.MasterId && !curRoomInfo.Players[i].RoomReady)
			{
				base.ui.masterKick.selectedIndex = 0;
				Timer obj3 = masterKickTimer;
				if (obj3 != null)
				{
					obj3.Cancel();
				}
				return;
			}
			if (curRoomInfo.Players[i].Id != curRoomInfo.MasterId)
			{
				member.Add(curRoomInfo.Players[i].Id);
			}
		}
		base.ui.masterKick.selectedIndex = 1;
		base.ui.progress_KickMaster.max = StaticGlobalData.ROOM_KICK_TIMELIMIT;
		masterKickTimer = Timer.Register(0f, (float)StaticGlobalData.ROOM_KICK_TIMELIMIT, (Action)delegate
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1028);
			int index = UnityEngine.Random.Range(0, member.Count);
			SimpleSingletonProvider<GameLogicManager>.inst.room.RequestAbdicationC2S(member[index]);
		}, (Action)null, (Action)delegate
		{
			base.ui.masterKick.selectedIndex = 0;
			base.ui.progress_KickMaster.value = 0.0;
		}, (Action)null, (Action)null, (Action<float>)delegate(float _)
		{
			base.ui.progress_KickMaster.value = base.ui.progress_KickMaster.max - (double)_;
		}, (Action)null, false, -1f, false, (GameObject)null);
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1009, delegate
		{
			CancelTime();
			SimpleSingletonProvider<GameLogicManager>.inst.room.RequestExitRoomC2S(ExitRoomC2S.Types.ForceExitType.None).OnFinishedOnly.AddOnce(RequestListData);
		});
		base.ui.btn_Return.onClick.Release();
	}

	private async void LevelRoom()
	{
		base.ui.btn_Leave.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1019, delegate
		{
			CancelTime();
			SimpleSingletonProvider<GameLogicManager>.inst.room.RequestExitRoomC2S(ExitRoomC2S.Types.ForceExitType.None).OnFinishedOnly.AddOnce(RequestListData);
		});
		base.ui.btn_Leave.onClick.Release();
	}

	private async void RequestListData()
	{
		base.ui.btn_Return.onClick.Retain();
		base.ui.btn_Leave.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
		base.ui.btn_Leave.onClick.Release();
	}

	private void RefreshMaster()
	{
		RefreshWaitPlayerList();
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		RefreshButtonReadyStatus();
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(curRoomInfo.MasterId))
		{
			Timer obj = roomTimer;
			if (obj != null)
			{
				obj.Cancel();
			}
			roomTimer = Timer.Register(0f, (float)(StaticGlobalData.ROOM_WAIT_TIMELIMIT * 60), (Action)delegate
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1022);
				SimpleSingletonProvider<GameLogicManager>.inst.room.RequestExitRoomC2S(ExitRoomC2S.Types.ForceExitType.None).OnFinishedOnly.AddOnce(RequestListData);
			}, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, (Action)null, false, -1f, false, (GameObject)null);
		}
	}

	private void RefreshButtonReadyStatus()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null)
		{
			RoomPlayer selfInfo = curRoomInfo.GetSelfInfo();
			if (selfInfo != null)
			{
				base.ui.btn_Ready.status.selectedIndex = (selfInfo.RoomReady ? 1 : 0);
			}
		}
	}

	private void CancelTime()
	{
		Timer obj = roomTimer;
		if (obj != null)
		{
			obj.Cancel();
		}
		Timer obj2 = masterKickTimer;
		if (obj2 != null)
		{
			obj2.Cancel();
		}
	}

	private void ShowMode()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.info.SpeedType == 2)
		{
			base.ui.speedMode.selectedIndex = 1;
		}
		else
		{
			base.ui.speedMode.selectedIndex = 0;
		}
	}
}
