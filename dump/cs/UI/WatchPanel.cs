using System;
using Core.Net;
using FairyGUI;
using GameLogic;
using Tools;

namespace UI;

public class WatchPanel : BasePanel<UIWatchPanel>
{
	public WatchPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIWatchPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.showResult.selectedIndex = 0;
		base.ui.tab.selectedIndex = 1;
		base.ui.tab.onChanged.Call();
	}

	protected override void InitComponents()
	{
		base.InitComponents();
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.tab.onChanged.Add(SwitchTab);
		base.ui.btn_SerachPlay.onClick.Add(OnSearchPlay);
		base.ui.btn_Watch.onClick.Add(OnWatchPlayer);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.tab.onChanged.Remove(SwitchTab);
		base.ui.btn_SerachPlay.onClick.Remove(OnSearchPlay);
		base.ui.btn_Watch.onClick.Remove(OnWatchPlayer);
	}

	protected override void AddListener()
	{
		base.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
	}

	private void SwitchTab()
	{
		base.ui.tab.onChanged.Retain();
		if (base.ui.tab.selectedIndex != 0 && base.ui.tab.selectedIndex == 1)
		{
			base.ui.showResult.selectedIndex = 0;
			base.ui.txtField_Search.text = "";
		}
		base.ui.tab.onChanged.Release();
	}

	private void OnSearchPlay(EventContext context)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense())
		{
			return;
		}
		if (string.IsNullOrEmpty(base.ui.txtField_Search.text))
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1001.GetLocal(UIStringType.Spectate));
			return;
		}
		base.ui.btn_SerachPlay.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.watch.RequestWatchJoinRoomC2S(base.ui.txtField_Search.text).OnFinished.AddOnce(delegate(RPCAsyncResult _)
		{
			base.ui.btn_SerachPlay.onClick.Release();
			if (_.errId == 0)
			{
				RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
				if (curRoomInfo != null && !curRoomInfo.IsSingleGameModel())
				{
					base.ui.showResult.selectedIndex = 1;
					base.ui.com_player_1.InitData(0, curRoomInfo.GetPlayerBySlot(0), curRoomInfo.MapType);
					base.ui.com_player_2.InitData(1, curRoomInfo.GetPlayerBySlot(1), curRoomInfo.MapType);
					base.ui.com_player_3.InitData(2, curRoomInfo.GetPlayerBySlot(2), curRoomInfo.MapType);
					base.ui.com_player_4.InitData(3, curRoomInfo.GetPlayerBySlot(3), curRoomInfo.MapType);
					DateTime dateTime = (curRoomInfo.StartTime * 1000).StampMillisecondsToDateTime();
					string arg = $"{dateTime.Hour:D2}:{dateTime.Minute:D2}:{dateTime.Second:D2}";
					base.ui.txt_StartTime.text = string.Format(1007.GetLocal(UIStringType.Spectate), arg);
					base.ui.txt_WatchCount.text = string.Format(1006.GetLocal(UIStringType.Spectate), curRoomInfo.WatchCount, StaticGlobalData.ROOM_AUDIENCE_NUMBLIMIT.ToString());
				}
			}
		});
	}

	private async void OnWatchPlayer()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null && !curRoomInfo.IsSingleGameModel())
		{
			if (curRoomInfo.WatchCount >= StaticGlobalData.ROOM_AUDIENCE_NUMBLIMIT)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1008.GetLocal(UIStringType.Spectate));
				return;
			}
			if (!SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
			{
				SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
				return;
			}
			base.ui.btn_Watch.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.RoomHero);
			SimpleSingletonProvider<GameLogicManager>.inst.watch.RequestWatchRefreshRoomStateC2S();
			base.ui.btn_Watch.onClick.Release();
		}
	}
}
