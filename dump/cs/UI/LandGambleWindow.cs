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

public class LandGambleWindow : BaseWindow
{
	private int totalPoint;

	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private long _gambleSn;

	private bool _enableJoinGamble;

	private Dictionary<int, string> _currentAnimationNameDict;

	private Dictionary<long, int> _dicedPointDict;

	public LandGambleWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandGambleWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask<LandGambleWindow> ShowLand()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		return this;
	}

	protected override void OnShown()
	{
		base.OnShown();
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILandGambleWindow uILandGambleWindow)
		{
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uILandGambleWindow.player1.animation);
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uILandGambleWindow.player2.animation);
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uILandGambleWindow.player3.animation);
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uILandGambleWindow.player4.animation);
			SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo?.ClearClientHallInfo();
			uILandGambleWindow.btn_odd.onClick.Remove(OnClickOddButton);
			uILandGambleWindow.btn_even.onClick.Remove(OnClickEventButton);
			uILandGambleWindow.btn_Dice.onClick.Remove(OnClickDiceButton);
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public async UniTask DealLand_Gamble(Action _action)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().player.Id != _action.PlayerId)
		{
			return;
		}
		StartGambleC2S _gambleData = ByteBuf.ReadObject<StartGambleC2S>(_action.Data.ToByteArray());
		await ShowLand();
		await BeginGamble(_action.Sn, _gambleData.Hall, _gambleData.IsExec);
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId) || !_enableJoinGamble || OperationTimer.GetOperateTimer(_action.Sn) != null)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UILandGambleWindow win = gComponent as UILandGambleWindow;
		if (win != null)
		{
			OperationTimer.ActionDownTime(_action.Sn, 5081, delegate
			{
				win.btn_odd.onClick.Call();
			}, null, null, operateCard: false, showTimerToPlayer: false);
		}
	}

	public async UniTask DealLand_GambleDice(Action _action)
	{
		if (!base.isShowing)
		{
			await ShowLand();
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Hall != null)
			{
				Gamble hall = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Hall;
				bool enableJoin = false;
				for (int i = 0; i < hall.Roles.Count; i++)
				{
					if (hall.Roles[i].PlayerId == _action.PlayerId)
					{
						enableJoin = true;
						break;
					}
				}
				await BeginGamble(_action.Sn, hall, enableJoin);
			}
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			return;
		}
		UpdateGambleSn(_action.Sn);
		if (!_enableJoinGamble || OperationTimer.GetOperateTimer(_action.Sn) != null)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UILandGambleWindow win = gComponent as UILandGambleWindow;
		if (win != null)
		{
			OperationTimer.ActionDownTime(_action.Sn, 5083, delegate
			{
				win.btn_Dice.onClick.Call();
			}, null, null, operateCard: false, showTimerToPlayer: false);
		}
	}

	private async UniTask BeginGamble(long gambleSn, Gamble data, bool enableJoin)
	{
		await InitAsync(gambleSn, data, enableJoin);
	}

	public async UniTask InitAsync(long gambleSn, Gamble data, bool enableJoin)
	{
		_gambleSn = gambleSn;
		_enableJoinGamble = enableJoin;
		_currentAnimationNameDict = new Dictionary<int, string>(4);
		_dicedPointDict = new Dictionary<long, int>(4);
		if (base.contentPane is UILandGambleWindow uILandGambleWindow)
		{
			uILandGambleWindow.phase.selectedIndex = 0;
			uILandGambleWindow.player1.c1.selectedIndex = 0;
			uILandGambleWindow.player1_Dice.visible = false;
			uILandGambleWindow.player2.c1.selectedIndex = 0;
			uILandGambleWindow.player2_Dice.visible = false;
			uILandGambleWindow.player3.c1.selectedIndex = 0;
			uILandGambleWindow.player3_Dice.visible = false;
			uILandGambleWindow.player4.c1.selectedIndex = 0;
			uILandGambleWindow.player4_Dice.visible = false;
			uILandGambleWindow.txt_Prize.SetVar("prize", "0").FlushVars();
			totalPoint = 0;
			uILandGambleWindow.showPoint.selectedIndex = 0;
			uILandGambleWindow.pointState.selectedIndex = 0;
			uILandGambleWindow.btn_odd.onClick.Add(OnClickOddButton);
			uILandGambleWindow.btn_even.onClick.Add(OnClickEventButton);
			uILandGambleWindow.btn_Dice.onClick.Add(OnClickDiceButton);
		}
		foreach (GambleRole role in data.Roles)
		{
			_currentAnimationNameDict.TryAdd(role.HeroId, "");
			_dicedPointDict.TryAdd(role.PlayerId, 0);
		}
		await RefreshGambleData(data);
	}

	private void UpdateGambleSn(long gambleSn)
	{
		_gambleSn = gambleSn;
	}

	public async UniTask RefreshGambleData(Gamble data)
	{
		if (data == null)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UILandGambleWindow win))
		{
			return;
		}
		int num = data.BaseGold;
		foreach (GambleRole role in data.Roles)
		{
			num += role.BetGold;
		}
		win.txt_Prize.SetVar("prize", num.ToString()).FlushVars();
		win.btn_odd.gold.text = "- " + data.BetGold;
		win.btn_even.gold.text = "- " + data.BetGold;
		if (data.S == Gamble.Types.state.Guess)
		{
			foreach (GambleRole role2 in data.Roles)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(role2.PlayerId))
				{
					if (role2.GuessCode == 0)
					{
						win.phase.selectedIndex = 1;
						if (role2.IsDie || role2.GoldLack)
						{
							win.btn_odd.grayed = true;
							win.btn_odd.touchable = false;
							win.btn_even.grayed = true;
							win.btn_even.touchable = false;
						}
						else
						{
							win.btn_odd.grayed = false;
							win.btn_odd.touchable = true;
							win.btn_even.grayed = false;
							win.btn_even.touchable = true;
						}
					}
					else
					{
						win.phase.selectedIndex = 2;
					}
				}
				TrySetCharacterAnimation(win, role2, "Idle");
				if (FinishGuest(data.Roles))
				{
					TrySetGuestResult(win, role2);
				}
			}
			return;
		}
		if (data.S == Gamble.Types.state.Throw)
		{
			win.phase.selectedIndex = 2;
			{
				foreach (GambleRole role3 in data.Roles)
				{
					if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(role3.PlayerId))
					{
						if (role3.Point == 0)
						{
							win.phase.selectedIndex = 3;
							win.btn_Dice.visible = true;
							if (role3.IsDie || role3.GoldLack)
							{
								win.btn_Dice.grayed = true;
								win.btn_Dice.touchable = false;
							}
							else
							{
								win.btn_Dice.grayed = false;
								win.btn_Dice.touchable = true;
							}
						}
						else
						{
							win.phase.selectedIndex = 4;
						}
					}
					TrySetCharacterAnimation(win, role3, "Idle");
					TrySetGuestResult(win, role3);
					TrySetDiceResult(win, role3);
				}
				return;
			}
		}
		if (data.S != Gamble.Types.state.Result)
		{
			return;
		}
		UniTask[] array = new UniTask[4];
		for (int i = 0; i < data.Roles.Count; i++)
		{
			TrySetDiceResult(win, data.Roles[i]);
			if (_dicedPointDict.TryGetValue(data.Roles[i].PlayerId, out var value) && value > 0)
			{
				UICom_Point dice = GetPlayerDiceComponent(win, data.Roles[i]);
				array[i] = UniTask.WaitWhile(() => (bool)dice.data);
			}
		}
		if (await SimpleSingletonProvider<DelaySignalManager>.inst.WhenAll(array))
		{
			Hide();
			return;
		}
		foreach (GambleRole role4 in data.Roles)
		{
			if (!role4.IsDie && !role4.GoldLack)
			{
				UILandGamble_Player playerComponent = GetPlayerComponent(win, role4);
				if ((role4.GuessCode == 1 && data.IsOdd) || (role4.GuessCode == 2 && !data.IsOdd))
				{
					TrySetCharacterAnimation(win, role4, "Cheer");
					playerComponent.c1.selectedIndex = 4;
					playerComponent.addGold.text = "+" + role4.GoldChange;
				}
				else
				{
					playerComponent.c1.selectedIndex = 5;
					TrySetCharacterAnimation(win, role4, "Cry");
				}
			}
		}
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000);
		Hide();
	}

	private bool FinishGuest(RepeatedField<GambleRole> roles)
	{
		for (int i = 0; i < roles.Count; i++)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(roles[i].PlayerId))
			{
				return roles[i].GuessCode != 0;
			}
		}
		return false;
	}

	private void TrySetCharacterAnimation(UILandGambleWindow win, GambleRole role, string animationName)
	{
		if (!_currentAnimationNameDict.TryGetValue(role.HeroId, out var value) || !(value == animationName))
		{
			_currentAnimationNameDict[role.HeroId] = animationName;
			UILandGamble_Player playerComponent = GetPlayerComponent(win, role);
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(role.PlayerId);
			Vector2 vector = Vector2.one * 10f;
			if (playerComponent == win.player1 || playerComponent == win.player2)
			{
				vector = new Vector2(-1f, 1f) * 10f;
			}
			SimpleSingletonProvider<CharacterAssetManager>.inst.PlayAnimation(playerDataById, animationName, playerComponent.animation, vector).grayed = role.IsDie || role.GoldLack;
		}
	}

	private void TrySetGuestResult(UILandGambleWindow win, GambleRole role)
	{
		UILandGamble_Player playerComponent = GetPlayerComponent(win, role);
		if (role.IsDie || role.GoldLack)
		{
			playerComponent.c1.selectedIndex = 3;
		}
		else if (role.GuessCode == 0)
		{
			playerComponent.c1.selectedIndex = 0;
		}
		else if (role.GuessCode == 1)
		{
			playerComponent.c1.selectedIndex = 1;
		}
		else if (role.GuessCode == 2)
		{
			playerComponent.c1.selectedIndex = 2;
		}
	}

	private void TrySetDiceResult(UILandGambleWindow win, GambleRole role)
	{
		if (!role.IsDie && !role.GoldLack && role.Point != 0 && (!_dicedPointDict.TryGetValue(role.PlayerId, out var value) || value <= 0))
		{
			totalPoint += role.Point;
			_dicedPointDict[role.PlayerId] = role.Point;
			UICom_Point playerDiceComponent = GetPlayerDiceComponent(win, role);
			playerDiceComponent.visible = true;
			CommonUIManager.PlayDice(playerDiceComponent.player.selectedIndex, role.Point, playerDiceComponent, delegate
			{
				win.totalPoint.text = totalPoint.ToString();
				win.showPoint.selectedIndex = ((totalPoint > 0) ? 1 : 0);
				win.pointState.selectedIndex = ((totalPoint > 0) ? ((totalPoint % 2 != 0) ? 1 : 2) : 0);
			});
		}
	}

	private UILandGamble_Player GetPlayerComponent(UILandGambleWindow win, GambleRole role)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(role.PlayerId).player.Slot switch
		{
			0 => win.player1, 
			1 => win.player2, 
			2 => win.player3, 
			3 => win.player4, 
			_ => null, 
		};
	}

	private UICom_Point GetPlayerDiceComponent(UILandGambleWindow win, GambleRole role)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(role.PlayerId).player.Slot switch
		{
			0 => win.player1_Dice as UICom_Point, 
			1 => win.player2_Dice as UICom_Point, 
			2 => win.player3_Dice as UICom_Point, 
			3 => win.player4_Dice as UICom_Point, 
			_ => null, 
		};
	}

	private void OnClickOddButton()
	{
		if (!_enableJoinGamble)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UILandGambleWindow win = gComponent as UILandGambleWindow;
		if (win != null)
		{
			win.btn_odd.onClick.Retain();
			win.btn_even.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequestStartGambleC2S(_gambleSn, _enableJoinGamble, 1).OnFinishedOnly.AddOnce(delegate
			{
				win.btn_odd.onClick.Release();
				win.btn_even.onClick.Release();
			});
		}
	}

	private void OnClickEventButton()
	{
		if (!_enableJoinGamble)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UILandGambleWindow win = gComponent as UILandGambleWindow;
		if (win != null)
		{
			win.btn_odd.onClick.Retain();
			win.btn_even.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequestStartGambleC2S(_gambleSn, _enableJoinGamble, 2).OnFinishedOnly.AddOnce(delegate
			{
				win.btn_even.onClick.Release();
				win.btn_odd.onClick.Release();
			});
		}
	}

	private void OnClickDiceButton()
	{
		if (!_enableJoinGamble)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UILandGambleWindow win = gComponent as UILandGambleWindow;
		if (win != null && _gambleSn != 0L)
		{
			win.btn_Dice.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequestGambleThrowDicC2S(_gambleSn).OnFinishedOnly.AddOnce(delegate
			{
				_gambleSn = 0L;
				win.btn_Dice.visible = false;
				win.btn_Dice.onClick.Release();
			});
		}
	}
}
