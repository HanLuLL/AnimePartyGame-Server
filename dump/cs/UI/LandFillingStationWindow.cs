using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class LandFillingStationWindow : BaseWindow
{
	private BattlePlayerData playerData;

	private long _actionSn;

	public LandFillingStationWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandFillingStationWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask<LandFillingStationWindow> ShowLand()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		return this;
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UILandFillingStationWindow uILandFillingStationWindow)
		{
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001)
			{
				uILandFillingStationWindow.btn_Continue.grayed = true;
				uILandFillingStationWindow.btn_Continue.touchable = false;
			}
			else
			{
				uILandFillingStationWindow.btn_Continue.grayed = false;
				uILandFillingStationWindow.btn_Continue.touchable = true;
			}
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILandFillingStationWindow uILandFillingStationWindow)
		{
			uILandFillingStationWindow.btn_Stop.Loop.Stop();
			uILandFillingStationWindow.btn_Stop.scale = Vector2.one;
		}
	}

	public async void DealLand_StopOrContinue(Action _action)
	{
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (playerData.CharacterInst != null)
		{
			await playerData.CharacterInst.SwitchCamera();
			if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
			{
				if (playerData.CharacterInst.standLand.LandType == LandType.Born)
				{
					SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_action.PlayerId, 11002);
				}
				if (playerData.CharacterInst.standLand.LandType == LandType.FillingStation)
				{
					SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_action.PlayerId, 11002);
				}
				return;
			}
		}
		await ShowLand();
		RefreshBornData(_action.Sn);
	}

	public async void ShowStopOrContinueLand(long playerId, long _actionSn)
	{
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerData.CharacterInst != null)
		{
			await playerData.CharacterInst.SwitchCamera();
		}
		RefreshBornData(_actionSn);
	}

	public void RefreshBornData(long _Sn)
	{
		_actionSn = _Sn;
		GComponent gComponent = base.contentPane;
		UILandFillingStationWindow win = gComponent as UILandFillingStationWindow;
		if (win == null)
		{
			return;
		}
		win.btn_Continue.visible = true;
		win.btn_Stop.visible = true;
		win.btn_Continue.onClick.Set(ContinueMove_Born);
		win.btn_Stop.onClick.Set(StopMove_Born);
		win.btn_Stop.txt_Title.text = 101.GetLocal(UIStringType.Land);
		win.btn_Stop.txt_Desc.visible = true;
		if (playerData.CharacterInst.standLand.LandType == LandType.Gift)
		{
			int num = ((BattleConfig.AsymmetricalSpeedRound > SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round) ? BattleConfig.AsymmetricalFirstStageGiftCount : BattleConfig.AsymmetricalFinalStageGiftCount);
			int num2 = ((playerData.player.TeamId == BattleConfig.AsymmetricalDefenderTeamId) ? 105 : 104);
			win.btn_Stop.txt_Desc.text = string.Format(num2.GetLocal(UIStringType.Land), num);
		}
		else
		{
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			if (curRoomInfo != null && curRoomInfo.MapType == 7)
			{
				if (playerData.player.TeamId == BattleConfig.AsymmetricalDefenderTeamId)
				{
					win.btn_Stop.txt_Desc.visible = false;
				}
				else
				{
					win.btn_Stop.txt_Desc.text = 103.GetLocal(UIStringType.Land);
					int value = playerData.Property.Score.Value;
					win.btn_Stop.txt_Desc.SetVar("gift", value.ToString()).FlushVars();
					if (value > 0)
					{
						win.btn_Stop.Loop.Play();
					}
				}
			}
			else if (playerData.Property.level.Value < 3)
			{
				int num3 = StaticConfigure.Upgrade.DataDict[SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.UpgradePlan].UpgradeDataConfigureItems[playerData.Property.level.Value].Gold - playerData.Property.gold.Value;
				win.btn_Stop.txt_Desc.text = 102.GetLocal(UIStringType.Land);
				win.btn_Stop.txt_Desc.SetVar("needGold", ((num3 >= 0) ? num3 : 0).ToString()).FlushVars();
				if (num3 <= 0)
				{
					win.btn_Stop.Loop.Play();
				}
			}
			else
			{
				win.btn_Stop.txt_Desc.text = 108.GetLocal(UIStringType.Land);
			}
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.player.Id))
		{
			OperationTimer.ActionDownTime(_actionSn, 5077, delegate
			{
				win.btn_Continue.onClick.Call();
			});
		}
	}

	private void ContinueMove_Born()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 10)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestStopOrContinueC2S(stop: false);
		}
		else
		{
			if (_actionSn == 0L)
			{
				return;
			}
			GComponent gComponent = base.contentPane;
			UILandFillingStationWindow win = gComponent as UILandFillingStationWindow;
			if (win != null)
			{
				win.btn_Continue.onClick.Retain();
				win.btn_Stop.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestStopOrContinueC2S(_actionSn, stop: false).OnFinishedOnly.AddOnce(delegate
				{
					_actionSn = 0L;
					win.btn_Stop.onClick.Release();
					win.btn_Continue.onClick.Release();
				});
			}
		}
	}

	private void StopMove_Born()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.guide.SureMoveStop();
		}
		else if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 10)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestStopOrContinueC2S(stop: true);
		}
		else
		{
			if (_actionSn == 0L)
			{
				return;
			}
			GComponent gComponent = base.contentPane;
			UILandFillingStationWindow win = gComponent as UILandFillingStationWindow;
			if (win != null)
			{
				win.btn_Stop.onClick.Retain();
				win.btn_Continue.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestStopOrContinueC2S(_actionSn, stop: true).OnFinishedOnly.AddOnce(delegate
				{
					_actionSn = 0L;
					win.btn_Continue.onClick.Release();
					win.btn_Stop.onClick.Release();
				});
			}
		}
	}

	public async UniTask UpdateBornWin(long playerId, bool stop)
	{
		if (stop)
		{
			playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			await playerData.CharacterInst.StopMove();
		}
		HideImmediately();
	}

	public async void GuideTriggerStop()
	{
		if (base.contentPane is UILandFillingStationWindow uILandFillingStationWindow)
		{
			Vector2 pos = uILandFillingStationWindow.TransformPoint(uILandFillingStationWindow.btn_Stop.xy, GRoot.inst);
			await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(pos, uILandFillingStationWindow.btn_Stop.width, uILandFillingStationWindow.btn_Stop.height, _needTransparentMask: false, isRect: true);
		}
	}

	public void ShowAskReviveTeammate(Action action)
	{
		_actionSn = action.Sn;
		GComponent gComponent = base.contentPane;
		UILandFillingStationWindow win = gComponent as UILandFillingStationWindow;
		if (win == null)
		{
			return;
		}
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(action.PlayerId);
		win.btn_Stop.txt_Title.text = 106.GetLocal(UIStringType.Land);
		win.btn_Stop.txt_Desc.text = 107.GetLocal(UIStringType.Land);
		win.btn_Continue.visible = true;
		win.btn_Stop.visible = true;
		win.btn_Stop.onClick.Set((EventCallback0)delegate
		{
			if (_actionSn != 0L)
			{
				win.btn_Continue.onClick.Retain();
				win.btn_Stop.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestAskReviveTeammateC2S(_actionSn, _IsRevive: true).OnFinishedOnly.AddOnce(delegate
				{
					_actionSn = 0L;
					win.btn_Stop.onClick.Release();
					win.btn_Continue.onClick.Release();
				});
			}
		});
		win.btn_Continue.onClick.Set((EventCallback0)delegate
		{
			if (_actionSn != 0L)
			{
				win.btn_Stop.onClick.Retain();
				win.btn_Continue.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestAskReviveTeammateC2S(_actionSn, _IsRevive: false).OnFinishedOnly.AddOnce(delegate
				{
					_actionSn = 0L;
					win.btn_Continue.onClick.Release();
					win.btn_Stop.onClick.Release();
				});
			}
		});
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.player.Id))
		{
			OperationTimer.ActionDownTime(_actionSn, 5233, delegate
			{
				win.btn_Continue.onClick.Call();
			});
		}
	}

	public void ShowSelectMechanism(Action action)
	{
		_actionSn = action.Sn;
		GComponent gComponent = base.contentPane;
		UILandFillingStationWindow win = gComponent as UILandFillingStationWindow;
		if (win == null)
		{
			return;
		}
		win.btn_Stop.txt_Title.text = "启动";
		win.btn_Stop.txt_Desc.text = "";
		win.btn_Continue.visible = true;
		win.btn_Stop.visible = true;
		win.btn_Stop.onClick.Set((EventCallback0)delegate
		{
			if (_actionSn != 0L)
			{
				win.btn_Continue.onClick.Retain();
				win.btn_Stop.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestSelectMechanismC2S(_actionSn, select: true).OnFinishedOnly.AddOnce(delegate
				{
					_actionSn = 0L;
					win.btn_Stop.onClick.Release();
					win.btn_Continue.onClick.Release();
				});
			}
		});
		win.btn_Continue.onClick.Set((EventCallback0)delegate
		{
			if (_actionSn != 0L)
			{
				win.btn_Stop.onClick.Retain();
				win.btn_Continue.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestSelectMechanismC2S(_actionSn, select: false).OnFinishedOnly.AddOnce(delegate
				{
					_actionSn = 0L;
					win.btn_Continue.onClick.Release();
					win.btn_Stop.onClick.Release();
				});
			}
		});
		OperationTimer.ActionDownTime(_actionSn, 5259, delegate
		{
			win.btn_Continue.onClick.Call();
		});
	}
}
