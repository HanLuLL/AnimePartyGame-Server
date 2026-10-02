using System.Collections.Generic;
using Core;
using Core.Net;
using Core.Tutorial;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using GameLogic.Replay;
using Tools;
using UnityEngine;
using party.protocol;

namespace UI;

public class SettingInBattleWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private readonly List<BattlePlayerData> playersData = new List<BattlePlayerData>();

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public SettingInBattleWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UISettingInBattleWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateDynmicBlurTex((GameCameraFlag)0);
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
		if (!(base.contentPane is UISettingInBattleWindow uISettingInBattleWindow))
		{
			return;
		}
		uISettingInBattleWindow.CutIn.Play();
		uISettingInBattleWindow.list_lockExpression.itemRenderer = RendererLockExpression;
		uISettingInBattleWindow.btn_Continue.onClick.Add(ReturnGame);
		uISettingInBattleWindow.btn_OpenSetting.onClick.Add(OpenSettingMenu);
		uISettingInBattleWindow.btn_OpenSetting.title = 1030006.GetLocal(UIStringType.GUI);
		uISettingInBattleWindow.btn_QuitGame.onClick.Add(QuitGame);
		uISettingInBattleWindow.btn_QuitGame.title = 1030004.GetLocal(UIStringType.GUI);
		uISettingInBattleWindow.btn_LeaveRoom.onClick.Add(LeaveRoom);
		uISettingInBattleWindow.btn_LeaveRoom.title = 1030008.GetLocal(UIStringType.GUI);
		uISettingInBattleWindow.btn_AudienceOpenSetting.onClick.Add(AudienceOpenSetting);
		uISettingInBattleWindow.btn_AudienceOpenSetting.title = 1030006.GetLocal(UIStringType.GUI);
		uISettingInBattleWindow.btn_AudienceLeaveRoom.onClick.Add(AudienceLeaveRoom);
		uISettingInBattleWindow.btn_AudienceLeaveRoom.title = 1030005.GetLocal(UIStringType.GUI);
		uISettingInBattleWindow.btn_AudienceContinue.onClick.Add(AudienceContinue);
		uISettingInBattleWindow.btn_AudienceContinue.title = 1030007.GetLocal(UIStringType.GUI);
		uISettingInBattleWindow.btn_AudienceQuitGame.onClick.Add(AudienceQuitGame);
		uISettingInBattleWindow.btn_AudienceQuitGame.title = 1030004.GetLocal(UIStringType.GUI);
		uISettingInBattleWindow.btn_MonsterLibrary.onClick.Add(ShowMonsterLibraryWindow);
		uISettingInBattleWindow.btn_MonsterLibrary.title = 1030001.GetLocal(UIStringType.GUI);
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null)
		{
			if (roomInfo.IsNovice())
			{
				TutorialPlayerActionFSM system = TutorialGame.GetSystem<TutorialPlayerActionFSM>();
				if (!SimpleSingletonProvider<GameLogicManager>.inst.tutorial.IsNovice && system != null && system.CurrentState == PlayerActionType.Idle && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(system.PlayerId))
				{
					uISettingInBattleWindow.btn_LeaveRoom.visible = true;
				}
				else
				{
					uISettingInBattleWindow.btn_LeaveRoom.visible = false;
				}
				uISettingInBattleWindow.btn_QuitGame.visible = false;
				uISettingInBattleWindow.list_lockExpression.visible = false;
				int childIndex = uISettingInBattleWindow.GetChildIndex(uISettingInBattleWindow.list_lockExpression);
				GObject childAt = uISettingInBattleWindow.GetChildAt(childIndex - 1);
				if (childAt != null)
				{
					childAt.visible = false;
				}
				GObject childAt2 = uISettingInBattleWindow.GetChildAt(childIndex + 1);
				if (childAt2 != null)
				{
					childAt2.visible = false;
				}
			}
			List<int> currentConfigMonsterIds = roomInfo.GetCurrentConfigMonsterIds();
			uISettingInBattleWindow.btn_MonsterLibrary.visible = !roomInfo.IsNovice() && currentConfigMonsterIds != null && currentConfigMonsterIds.Count > 0;
		}
		blurBgCtrl.OnShown(this);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.curPlayerOperate.AddListener(ReturnGame);
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UISettingInBattleWindow uISettingInBattleWindow)
		{
			uISettingInBattleWindow.btn_Continue.onClick.Remove(ReturnGame);
			uISettingInBattleWindow.btn_OpenSetting.onClick.Remove(OpenSettingMenu);
			uISettingInBattleWindow.btn_QuitGame.onClick.Remove(QuitGame);
			uISettingInBattleWindow.btn_LeaveRoom.onClick.Remove(LeaveRoom);
			uISettingInBattleWindow.btn_AudienceOpenSetting.onClick.Remove(AudienceOpenSetting);
			uISettingInBattleWindow.btn_AudienceLeaveRoom.onClick.Remove(AudienceLeaveRoom);
			uISettingInBattleWindow.btn_AudienceContinue.onClick.Remove(AudienceContinue);
			uISettingInBattleWindow.btn_AudienceQuitGame.onClick.Remove(AudienceQuitGame);
			uISettingInBattleWindow.btn_MonsterLibrary.onClick.Remove(ShowMonsterLibraryWindow);
			blurBgCtrl.OnHide();
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.curPlayerOperate.RemoveListener(ReturnGame);
		}
	}

	private void ReturnGame()
	{
		if (base.contentPane is UISettingInBattleWindow uISettingInBattleWindow)
		{
			uISettingInBattleWindow.btn_Continue.onClick.Retain();
			Hide();
			uISettingInBattleWindow.btn_Continue.onClick.Release();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (context.inputEvent.keyCode == KeyCode.Escape)
		{
			ReturnGame();
		}
	}

	public async UniTask ShowSettingInBattle()
	{
		await TryShowAsync();
		if (base.contentPane is UISettingInBattleWindow uISettingInBattleWindow)
		{
			uISettingInBattleWindow.playerType.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher() ? 1 : 0);
			RefreshWatchCode();
			RefreshLockExpression();
		}
	}

	private async void OpenSettingMenu()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UISettingInBattleWindow win)
		{
			win.btn_OpenSetting.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.setting.ShowSetting();
			win.btn_OpenSetting.onClick.Release();
		}
	}

	private async void QuitGame()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UISettingInBattleWindow win)
		{
			win.btn_QuitGame.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1014, delegate
			{
				SimpleSingletonProvider<GameManager>.inst.CloseGame();
			});
			win.btn_QuitGame.onClick.Release();
		}
	}

	private void LeaveRoom(EventContext context)
	{
		GComponent gComponent = base.contentPane;
		UISettingInBattleWindow win = gComponent as UISettingInBattleWindow;
		if (win == null)
		{
			return;
		}
		win.btn_LeaveRoom.onClick.Retain();
		int msgId = 1020;
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null && (curRoomInfo.IsPVE() || curRoomInfo.IsAsymmetricalBattle()))
		{
			foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
			{
				RoomPlayer player = playerData.player;
				if (player.characterType != CharacterType.Hero)
				{
					continue;
				}
				bool flag = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(player.Id);
				if (!player.IsBot && !flag)
				{
					if (!player.Property.Online.Value)
					{
						msgId = 1020;
						break;
					}
					msgId = 11020;
				}
			}
		}
		bool noviceRoom = curRoomInfo != null && curRoomInfo.MapType == 10;
		SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(msgId, delegate
		{
			if (noviceRoom)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo(_voluntaryWithdrawal: true);
				SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
				SimpleSingletonProvider<DelaySignalManager>.inst.CancelAllTask();
				SimpleSingletonProvider<GameLogicManager>.inst.battleResult.FinishGame();
				SimpleSingletonProvider<GameLogicManager>.inst.battle.CloseBattleUI();
			}
			else
			{
				win.btn_LeaveRoom.onClick.Retain();
				win.btn_QuitGame.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.room.GiveUpGame(base.Hide, ExitRoomC2S.Types.ForceExitType.VoluntaryExit);
			}
		}, delegate
		{
			win.btn_LeaveRoom.onClick.Release();
		}).Forget();
	}

	private void AudienceContinue()
	{
		if (base.contentPane is UISettingInBattleWindow uISettingInBattleWindow)
		{
			uISettingInBattleWindow.btn_AudienceContinue.onClick.Retain();
			Hide();
			uISettingInBattleWindow.btn_AudienceContinue.onClick.Release();
		}
	}

	private void AudienceQuitGame()
	{
		if (base.contentPane is UISettingInBattleWindow uISettingInBattleWindow)
		{
			uISettingInBattleWindow.btn_AudienceQuitGame.onClick.Retain();
			QuitGame();
			uISettingInBattleWindow.btn_AudienceQuitGame.onClick.Release();
		}
	}

	private void AudienceOpenSetting()
	{
		if (base.contentPane is UISettingInBattleWindow uISettingInBattleWindow)
		{
			uISettingInBattleWindow.btn_AudienceOpenSetting.onClick.Retain();
			OpenSettingMenu();
			uISettingInBattleWindow.btn_AudienceOpenSetting.onClick.Release();
		}
	}

	private async void AudienceLeaveRoom()
	{
		GComponent gComponent = base.contentPane;
		UISettingInBattleWindow win = gComponent as UISettingInBattleWindow;
		if (win == null)
		{
			return;
		}
		await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1003.GetLocal(UIStringType.Spectate), delegate
		{
			win.btn_AudienceLeaveRoom.onClick.Retain();
			win.touchable = false;
			if (SimpleSingletonProvider<GameLogicManager>.inst.replay.Session.IsReplay)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.room.ExitRunningRoom(null);
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.watch.RequestWatchExitRoomC2S().OnFinished.AddOnce(delegate(RPCAsyncResult _)
				{
					if (_.errId != 0)
					{
						win.touchable = true;
						win.btn_AudienceLeaveRoom.onClick.Release();
					}
				});
			}
		}, delegate
		{
			win.touchable = true;
			win.btn_AudienceLeaveRoom.onClick.Release();
		});
	}

	private async void ShowMonsterLibraryWindow()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UISettingInBattleWindow win)
		{
			win.btn_MonsterLibrary.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.NewGameLibrary.TryShowAsync();
			win.btn_MonsterLibrary.onClick.Release();
		}
	}

	private void RefreshWatchCode()
	{
		GComponent gComponent = base.contentPane;
		UISettingInBattleWindow win = gComponent as UISettingInBattleWindow;
		if (win == null)
		{
			return;
		}
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		ReplaySession replaySession = SimpleSingletonProvider<GameLogicManager>.inst.replay?.Session;
		if ((replaySession != null && replaySession.IsReplay) || !room.IsInRoom || string.IsNullOrEmpty(room.curRoomInfo?.watchCode) || room.curRoomInfo.IsSingleGameModel())
		{
			win.showWatchCode.selectedIndex = 0;
			return;
		}
		string watchCode = room.curRoomInfo.watchCode;
		win.txt_Code.text = string.Format(1030003.GetLocal(UIStringType.GUI), watchCode);
		win.btn_Copy.title = 1030002.GetLocal(UIStringType.GUI);
		win.showWatchCode.selectedIndex = 1;
		win.btn_Copy.onClick.Set((EventCallback0)delegate
		{
			win.btn_Copy.onClick.Retain();
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1009.GetLocal(UIStringType.Spectate));
			GUIUtility.systemCopyBuffer = watchCode;
			win.btn_Copy.onClick.Release();
		});
	}

	private void RefreshLockExpression()
	{
		if (!(base.contentPane is UISettingInBattleWindow uISettingInBattleWindow))
		{
			return;
		}
		playersData.Clear();
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		if (playerDatas == null)
		{
			return;
		}
		for (int i = 0; i < playerDatas.Count; i++)
		{
			if (playerDatas[i].characterType == CharacterType.Hero && !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerDatas[i].player.Id))
			{
				playersData.Add(playerDatas[i]);
			}
		}
		uISettingInBattleWindow.list_lockExpression.numItems = playersData.Count;
		uISettingInBattleWindow.txt_Tip.text = 1030010.GetLocal(UIStringType.GUI);
	}

	private void RendererLockExpression(int index, GObject item)
	{
		UISettingInBattle_Button_LockExpression btn_LockExpression = item as UISettingInBattle_Button_LockExpression;
		if (btn_LockExpression != null && playersData.Count > index)
		{
			btn_LockExpression.loader_Head.url = playersData[index].player.standingPainting.ProfilePhoto;
			long playerID = playersData[index].player.Id;
			btn_LockExpression.setLock.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.lockExpressionPlayerID.Contains(playerID) ? 1 : 0);
			btn_LockExpression.onClick.Set((EventCallback0)delegate
			{
				btn_LockExpression.onClick.Retain();
				bool flag = SimpleSingletonProvider<GameLogicManager>.inst.SetLockChatPlayer(playerID);
				btn_LockExpression.setLock.selectedIndex = (flag ? 1 : 0);
				btn_LockExpression.onClick.Release();
			});
		}
	}
}
