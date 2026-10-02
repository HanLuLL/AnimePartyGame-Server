using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using GameLogic.Replay;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using UnityTimer;
using party.protocol;

namespace UI;

public class BattleSettlementPanel : BasePanel<UIBattleSettlementPanel>
{
	private BattleSettlement _BattleSettlement;

	private Timer operateTimer;

	private BattlePlayerData _SelfData;

	public BattleSettlementPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIBattleSettlementPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (objs != null && objs.Length != 0)
		{
			_BattleSettlement = objs[0] as BattleSettlement;
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle)
		{
			SimpleSingletonProvider<UIManager>.inst.expression.CloseMapChat();
			List<BattlePlayerData> winners = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataByTeamId(_BattleSettlement.WinTeamId);
			if (winners.Count > 0 && winners[0].characterType == CharacterType.Hero)
			{
				base.ui.com_Victory.visible = true;
				base.ui.com_Victory.Victory_Cut_in.SetHook("ShowNext", delegate
				{
					ShowNext(winners);
				});
				base.ui.com_Victory.Victory_Cut_in.Play(delegate
				{
					base.ui.com_Victory.visible = false;
					SimpleSingletonProvider<UIManager>.inst.CreditWarning.ShowExemptedTip().Forget();
				});
			}
			else
			{
				ShowNext(winners);
			}
			base.ui.btn_Return.visible = false;
		}
		else
		{
			base.ui.btn_Return.visible = true;
		}
	}

	private void ShowNext(List<BattlePlayerData> winners)
	{
		if (_BattleSettlement.IsPVP && winners.Count == 1)
		{
			ShowWinner(winners[0]);
		}
		else
		{
			ShowTime_Achieve();
		}
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
		Stage.inst.onTouchBegin.Add(CloseAchievementInfo);
		base.ui.btn_Return.onClick.Add(ReturnLastPanel);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		Stage.inst.onTouchBegin.Remove(CloseAchievementInfo);
		base.ui.btn_Return.onClick.Remove(ReturnLastPanel);
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
		CloseSettlementEffect();
		CommonUIManager.StopAllVideo();
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public override void PlayBGM()
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle)
		{
			base.PlayBGM();
		}
	}

	private void ReturnLastPanel(EventContext context)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
		{
			base.ui.btn_Return.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.replay.ExitReplay();
			SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
			base.ui.btn_Return.onClick.Release();
		}
	}

	private void RefreshPlayerLabel(UICom_PlayerLabel com_Label, BattlePlayerData playerData)
	{
		CommonUIManager.RendererLabelInfo(com_Label, playerData.player.GetNick(showRemark: true), playerData.player.Level);
		(string, bool) tuple = playerData.player.AccountBackgroundURL();
		CommonUIManager.RendererLabel(UIType.Panel, (int)base.config.PanelType, com_Label, tuple.Item1, tuple.Item2);
		string headShot = playerData.player.HeadURL();
		CommonUIManager.RendererHeadShot(com_Label, headShot, isVideo: false);
	}

	private void RefreshBustRole(UIBattleSettlement_Com_ShowRole com_ShowRole, string url)
	{
		Vector2 offset = Vector2.zero;
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(url, out var value))
		{
			offset = value.skinOffset;
		}
		RendererSkin(com_ShowRole.loader_Role_1, url, offset, Vector2.one);
		RendererSkin(com_ShowRole.loader_Role_2, url, offset, Vector2.one);
		RendererSkin(com_ShowRole.loader_Role_3, url, offset, Vector2.one);
	}

	private void RendererSkin(GLoader _loader, string _URL, Vector2 _offset, Vector2 _scale)
	{
		if (!(_loader.url == _URL))
		{
			_loader.customOffset = _offset;
			_loader.customScale = _scale;
			_loader.url = _URL;
		}
	}

	private void TryPraise(GGraph vfx, long playerId)
	{
		SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(54.GetEffectDataConfigure().EffectName, vfx, 20f).Forget();
		SimpleSingletonProvider<GameLogicManager>.inst.battleResult.RequestPraisePlayerC2S(new List<long> { playerId }).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (result.errId == 0)
			{
				ChatPraise(playerId);
			}
		});
	}

	private void ChatPraise(long playerId)
	{
		BattlePlayerData sender = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (sender != null)
		{
			List<long> list = (from x in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas
				where x.characterType == CharacterType.Hero && !x.player.OffLine && !x.player.IsBot && x.player.Id != sender.player.Id
				select x.player.Id).ToList();
			if (list != null && list.Count != 0)
			{
				BattlePraiseMessage msgData = new BattlePraiseMessage(sender, playerId);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestPlayerChatC2S(msgData, list, CD_CHECK: false);
			}
		}
	}

	private void CloseSettlementEffect()
	{
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(base.ui.graph_Effect);
	}

	private void ShowResultEffect()
	{
		SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(35.GetEffectDataConfigure().EffectName, base.ui.graph_Effect, 80f).Forget();
	}

	private void AutoNext(Action complete)
	{
		BattlePlayerData currentPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetCurrentPlayer();
		string headUrl = ((currentPlayer != null) ? currentPlayer.GetCharacterHeadUrl() : "");
		int totalTime = StaticGlobalData.GAME_RESULT_TIME_LIMIT_MAX;
		Timer obj = operateTimer;
		if (obj != null)
		{
			obj.Cancel();
		}
		operateTimer = Timer.Register(0f, (float)totalTime, (Action)delegate
		{
			complete?.Invoke();
			SimpleSingletonProvider<UIManager>.inst.operateTime.UpdateOperationProgress(0, 0f, headUrl);
		}, (Action)null, (Action)delegate
		{
			SimpleSingletonProvider<UIManager>.inst.operateTime.UpdateOperationProgress(0, 0f, headUrl);
		}, (Action)null, (Action)null, (Action<float>)delegate(float time)
		{
			SimpleSingletonProvider<UIManager>.inst.operateTime.UpdateOperationProgress(totalTime, time, headUrl);
		}, (Action)null, false, -1f, false, (GameObject)null);
	}

	public void ShowAchieveData()
	{
		RefreshAchieveInfo();
		RefreshPlayerItemStep(1);
		base.ui.com_ShowTime.ShowTime.Play(delegate
		{
			base.ui.step.selectedIndex = 2;
			RefreshPlayerItemStep(2);
			PlayerItemShowData(delegate
			{
				base.ui.com_ShowTime.btn_Next.onClick.Release();
				base.ui.com_ShowTime.btn_Next.onClick.Set(StartReplay);
			});
		});
	}

	private async void StartReplay()
	{
		base.ui.btn_Return.onClick.Retain();
		base.ui.com_ShowTime.btn_Next.onClick.Retain();
		if (!(await SimpleSingletonProvider<GameLogicManager>.inst.replay.StartPlaybackAsync()))
		{
			base.ui.btn_Return.onClick.Release();
			base.ui.btn_Return.onClick.Call();
		}
	}

	private void ShowBalance()
	{
		ReplaySession replaySession = SimpleSingletonProvider<GameLogicManager>.inst.replay?.Session;
		if (replaySession != null && replaySession.IsReplay)
		{
			FinishBalance();
			return;
		}
		base.ui.step.selectedIndex = 3;
		ShowResultEffect();
		CommonUIManager.StopVideo(UIType.Panel, (int)base.config.PanelType);
		base.ui.com_Balance.Cut_in.Play();
		RefreshBalanceAward();
		RefreshCampScore();
		_SelfData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		RefreshPlayerLabel((UICom_PlayerLabel)base.ui.com_Balance.com_PlayerLabel, _SelfData);
		RefreshBustRole(base.ui.com_Balance.com_ShowRole, _SelfData.player.standingPainting.GetCharacter().Item1);
		int preExp = SimpleSingletonProvider<GameLogicManager>.inst.battleResult.preExp;
		ShowProgress(_SelfData.player.Level, preExp, _BattleSettlement.GetExpCount());
	}

	private void RefreshCampScore()
	{
		Int32KvPair campScore = _BattleSettlement.CampScore;
		if (campScore != null && campScore.Key > 0)
		{
			base.ui.com_Balance.com_Camp.txt_AddPoint.text = campScore.Value.ToString();
			if (SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster.Id == campScore.Key)
			{
				base.ui.com_Balance.com_Camp.camp.selectedIndex = 0;
			}
			else if (SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster.Id == campScore.Key)
			{
				base.ui.com_Balance.com_Camp.camp.selectedIndex = 1;
			}
			base.ui.com_Balance.com_Camp.visible = true;
		}
		else
		{
			base.ui.com_Balance.com_Camp.visible = false;
		}
	}

	private void RefreshBalanceAward()
	{
		List<KeyValuePair<int, int>> awardItems = _BattleSettlement.AwardItems;
		base.ui.com_Balance.list_Reward.itemProvider = (int index) => (awardItems.Count > index && awardItems[index].Key == _BattleSettlement.PVECoinItemId) ? "ui://avgradidw0q93a" : "ui://avgradidw0q93c";
		base.ui.com_Balance.list_Reward.itemRenderer = delegate(int index, GObject item)
		{
			if (awardItems.Count > index)
			{
				ItemInfoConfigure itemInfoConfigure = awardItems[index].Key.GetItemInfoConfigure();
				string local = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
				int rookieBonusAwardByItemId = _BattleSettlement.GetRookieBonusAwardByItemId(awardItems[index].Key);
				int retrurnBonusAwardByItemId = _BattleSettlement.GetRetrurnBonusAwardByItemId(awardItems[index].Key);
				if (item is UIBattleSettlement_Com_Reward reward)
				{
					RefreshReward(reward, local, itemInfoConfigure.ShowIcon, awardItems[index].Value, rookieBonusAwardByItemId, retrurnBonusAwardByItemId);
				}
				if (item is UIBattleSettlement_Com_LimitReward uIBattleSettlement_Com_LimitReward)
				{
					WeeklyLimitPropData weeklyLimitPropData = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetWeeklyLimitPropData(_BattleSettlement.PVECoinItemId);
					RefreshReward(uIBattleSettlement_Com_LimitReward.com_Reward, local, itemInfoConfigure.ShowIcon, awardItems[index].Value, rookieBonusAwardByItemId, retrurnBonusAwardByItemId);
					uIBattleSettlement_Com_LimitReward.txt_LimitCount.text = $"{1057.GetLocal(UIStringType.Message)} {weeklyLimitPropData.Count}/{weeklyLimitPropData.LimitCount}";
				}
			}
		};
		base.ui.com_Balance.list_Reward.numItems = awardItems.Count;
		base.ui.com_Balance.list_Reward.ResizeToFit();
	}

	private static void RefreshReward(UIBattleSettlement_Com_Reward reward, string _name, string icon, int awardCount, int addCount, int returnAddCount)
	{
		reward.txt_Name.text = _name;
		reward.loader_Icon.url = icon;
		reward.txt_Count.text = $"+{awardCount}";
		int num = addCount + returnAddCount;
		UIRoomPlayer_Button_RewardUp uIRoomPlayer_Button_RewardUp = (UIRoomPlayer_Button_RewardUp)reward.com_NoviceUP;
		if (addCount > 0 && returnAddCount > 0)
		{
			uIRoomPlayer_Button_RewardUp.state.selectedIndex = 2;
		}
		else if (addCount > 0)
		{
			uIRoomPlayer_Button_RewardUp.state.selectedIndex = 0;
		}
		else if (returnAddCount > 0)
		{
			uIRoomPlayer_Button_RewardUp.state.selectedIndex = 1;
		}
		reward.com_NoviceUP.visible = num > 0;
		reward.com_NoviceUP.title = $"+{num}";
	}

	private void ShowProgress(int lv, int curExp, int residueExp)
	{
		PlayerLevelConfigure playerLevelConfigure = lv.GetPlayerLevelConfigure();
		if (playerLevelConfigure == null)
		{
			AutoNext(FinishBalance);
			EnableNextButton();
			return;
		}
		int needExp = playerLevelConfigure.NeedExp;
		base.ui.com_Balance.slider_Exp.max = needExp;
		base.ui.com_Balance.slider_Exp.value = curExp;
		int _residue = residueExp + curExp - needExp;
		int num = ((residueExp + curExp > needExp) ? needExp : (residueExp + curExp));
		base.ui.com_Balance.slider_Exp.TweenValue(num, 1f).OnComplete((GTweenCallback)delegate
		{
			if (_residue < 0)
			{
				AutoNext(FinishBalance);
				EnableNextButton();
			}
			else
			{
				int num2 = lv + 1;
				UICom_PlayerLabel com_Label = (UICom_PlayerLabel)base.ui.com_Balance.com_PlayerLabel;
				RepeatedField<PlayerLevelConfigure> levels = StaticConfigure.Player.Levels;
				CommonUIManager.RendererLabelInfo(_playerLv: Mathf.Min(num2, levels[levels.Count - 1].Level), com_Label: com_Label, _playerName: _SelfData.player.GetNick());
				ShowProgress(num2, 0, _residue);
			}
		});
	}

	private void EnableNextButton()
	{
		base.ui.com_Balance.btn_Next.visible = true;
		base.ui.com_Balance.btn_Next.onClick.Release();
		base.ui.com_Balance.btn_Next.onClick.Set((EventCallback0)delegate
		{
			base.ui.com_Balance.btn_Next.onClick.Retain();
			FinishBalance();
		});
	}

	private void FinishBalance()
	{
		Timer obj = operateTimer;
		if (obj != null)
		{
			obj.Cancel();
		}
		base.ui.com_Balance.btn_Next.visible = false;
		SimpleSingletonProvider<GameLogicManager>.inst.battleResult.FinishGame();
	}

	private void ShowTime_Achieve()
	{
		CloseSettlementEffect();
		CommonUIManager.StopVideo(UIType.Panel, (int)base.config.PanelType);
		RefreshAchieveInfo();
		base.ui.com_ShowTime.ShowTime.Play(delegate
		{
			RefreshPlayerItemStep(1);
			AutoNext(ShowTime_Data);
			base.ui.com_ShowTime.btn_Next.onClick.Release();
		});
		base.ui.com_ShowTime.btn_Next.onClick.Retain();
		base.ui.com_ShowTime.btn_Next.onClick.Set((EventCallback0)delegate
		{
			base.ui.com_ShowTime.btn_Next.onClick.Retain();
			Timer obj = operateTimer;
			if (obj != null)
			{
				obj.Cancel();
			}
			ShowTime_Data();
		});
		base.ui.com_ShowTime.btn_ScreenShot.onClick.Set(ScreenShot);
	}

	private void RefreshAchieveInfo()
	{
		base.ui.step.selectedIndex = 2;
		RefreshPlayerItem(base.ui.com_ShowTime.com_Player1, _BattleSettlement.GetPlayerSettlement(0));
		RefreshPlayerItem(base.ui.com_ShowTime.com_Player2, _BattleSettlement.GetPlayerSettlement(1));
		RefreshPlayerItem(base.ui.com_ShowTime.com_Player3, _BattleSettlement.GetPlayerSettlement(2));
		RefreshPlayerItem(base.ui.com_ShowTime.com_Player4, _BattleSettlement.GetPlayerSettlement(3));
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return;
		}
		GameModeInfoConfigure gameModeInfoConfigure = curRoomInfo.MapType.GetGameModeInfoConfigure();
		string text = ((gameModeInfoConfigure != null && !curRoomInfo.IsTerms) ? ("(" + gameModeInfoConfigure.NameID.GetLocal(UIStringType.GameMode) + ")") : "");
		MapInfoConfigure mapDataConfigure = curRoomInfo.MapId.GetMapDataConfigure();
		base.ui.com_ShowTime.com_Map.loader_Map.url = mapDataConfigure.MapSceneImage;
		base.ui.com_ShowTime.com_Map.txt_MapName.text = mapDataConfigure.MapName.GetLocal(UIStringType.Map) + text;
		base.ui.com_ShowTime.com_Map.txt_Round.SetVar("round", curRoomInfo.Round.ToString().PadLeft(2, '0')).FlushVars();
		if (!curRoomInfo.IsPVE())
		{
			return;
		}
		RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = mapDataConfigure.DifficultyIds[0].GetMapGameDifficultyItems();
		if (mapGameDifficultyItems != null)
		{
			for (int i = 0; i < mapGameDifficultyItems.Count; i++)
			{
				if (mapGameDifficultyItems[i].Index == curRoomInfo.Difficulty)
				{
					ChoosingTimeLimitdifficultyConfigure choosingTimeLimitDifficultyConfigure = mapGameDifficultyItems[i].Index.GetChoosingTimeLimitDifficultyConfigure();
					base.ui.com_ShowTime.com_Map.txt_Difficulty.text = choosingTimeLimitDifficultyConfigure.DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit);
					break;
				}
			}
		}
		base.ui.com_ShowTime.com_Map.list_terms.itemRenderer = RendererTermItem;
		base.ui.com_ShowTime.com_Map.list_terms.numItems = curRoomInfo.RoomTerms?.Count ?? 0;
	}

	private async void ScreenShot()
	{
		base.ui.com_ShowTime.btn_ScreenShot.onClick.Retain();
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		string textureName = $"{serverTime.Year:D}{serverTime.Month:D2}{serverTime.Day:D2}{serverTime.Hour:D2}{serverTime.Minute:D2}{serverTime.Second:D2}.png";
		string savePath = Application.persistentDataPath + "/Temp/ScreenShot";
		if (!Directory.Exists(savePath))
		{
			Directory.CreateDirectory(savePath);
		}
		SimpleSingletonProvider<UIManager>.inst.systemTips.HideImmediately();
		ScreenCapture.CaptureScreenshot(savePath + "/" + textureName);
		await UniTask.DelayFrame(2);
		SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(string.Format(1008.GetLocal(UIStringType.Achieve), savePath + "/" + textureName), 2f);
		base.ui.com_ShowTime.btn_ScreenShot.onClick.Release();
	}

	private void RefreshPlayerItem(UIBattleSettlement_Com_Item com_Player, PlayerSettlement playerSettlement)
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (playerSettlement == null)
		{
			if (roomInfo != null && roomInfo.IsMutatorPve())
			{
				int mapModeType = 12;
				com_Player.RefreshPlaceholderBot(mapModeType.GetGameModeNPCPlayerConfigure());
			}
			com_Player.visible = false;
			return;
		}
		bool showPVP = false;
		if (roomInfo != null)
		{
			showPVP = roomInfo.IsLuckyStarBattle() || roomInfo.IsPVP() || roomInfo.IsAsymmetricalBattle();
		}
		com_Player.Refresh(playerSettlement, showPVP);
		com_Player.RefreshBattleData(_BattleSettlement.BattleTotalData, _BattleSettlement.maxValues);
		RefreshPlayerLabel((UICom_PlayerLabel)com_Player.com_PlayerLabel, playerSettlement.PlayerData);
		com_Player.btn_Praise.onClick.Set((EventCallback0)delegate
		{
			if (com_Player.btn_Praise.isFinish.selectedIndex != 1)
			{
				com_Player.btn_Praise.onClick.Retain();
				com_Player.btn_Praise.Cut_in.Play(delegate
				{
					com_Player.btn_Praise.isFinish.selectedIndex = 1;
				});
				TryPraise(com_Player.btn_Praise.Vfx, playerSettlement.PlayerData.player.Id);
			}
		});
		bool flag = SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle;
		bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher();
		UIBattleSettlement_Button_OperatePlayer btn_Praise = com_Player.btn_Praise;
		bool visible = (com_Player.btn_AddFriend.visible = flag && !flag2);
		btn_Praise.visible = visible;
	}

	private void ShowTime_Data()
	{
		base.ui.step.selectedIndex = 2;
		RefreshPlayerItemStep(2);
		PlayerItemShowData(delegate
		{
			AutoNext(ShowBalance);
			base.ui.com_ShowTime.btn_Next.onClick.Release();
			base.ui.com_ShowTime.btn_Next.onClick.Set((EventCallback0)delegate
			{
				base.ui.com_ShowTime.btn_Next.onClick.Retain();
				Timer obj = operateTimer;
				if (obj != null)
				{
					obj.Cancel();
				}
				ShowBalance();
			});
		});
	}

	private void PlayerItemShowData(Action onFinish)
	{
		base.ui.com_ShowTime.com_Player1.CJ_Cut_in.Stop();
		base.ui.com_ShowTime.com_Player1.SJ_Cut_in.Play(onFinish.Invoke);
		base.ui.com_ShowTime.com_Player2.CJ_Cut_in.Stop();
		base.ui.com_ShowTime.com_Player2.SJ_Cut_in.Play();
		base.ui.com_ShowTime.com_Player3.CJ_Cut_in.Stop();
		base.ui.com_ShowTime.com_Player3.SJ_Cut_in.Play();
		base.ui.com_ShowTime.com_Player4.CJ_Cut_in.Stop();
		base.ui.com_ShowTime.com_Player4.SJ_Cut_in.Play();
	}

	private void RefreshPlayerItemStep(int index)
	{
		base.ui.com_ShowTime.com_Player1.step.selectedIndex = index;
		base.ui.com_ShowTime.com_Player2.step.selectedIndex = index;
		base.ui.com_ShowTime.com_Player3.step.selectedIndex = index;
		base.ui.com_ShowTime.com_Player4.step.selectedIndex = index;
	}

	private void CloseAchievementInfo()
	{
		base.ui.com_ShowTime.com_Player1.CloseAchievementInfo();
		base.ui.com_ShowTime.com_Player2.CloseAchievementInfo();
		base.ui.com_ShowTime.com_Player3.CloseAchievementInfo();
		base.ui.com_ShowTime.com_Player4.CloseAchievementInfo();
	}

	private void RendererTermItem(int index, GObject item)
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (roomInfo == null)
		{
			return;
		}
		RepeatedField<int> roomTerms = roomInfo.RoomTerms;
		if (roomTerms != null)
		{
			MutatorInfoConfigure mutatorInfoConfigure = roomTerms[index].GetMutatorInfoConfigure();
			item.icon = mutatorInfoConfigure.Icon;
			item.onClick.Add((EventCallback0)delegate
			{
				SimpleSingletonProvider<UIManager>.inst.RoomTerms.ShowWinScreenTerms(roomInfo.RoomTerms, null).Forget();
			});
		}
	}

	private void ShowWinner(BattlePlayerData winner)
	{
		base.ui.step.selectedIndex = 1;
		base.ui.com_Winner.Victory_Cut_in.SetHook("Finish", delegate
		{
			AutoNext(ShowTime_Achieve);
			base.ui.com_Winner.btn_Next.visible = true;
			base.ui.com_Winner.btn_Next.onClick.Release();
			base.ui.com_Winner.btn_Next.onClick.Set((EventCallback0)delegate
			{
				base.ui.com_Winner.btn_Next.onClick.Retain();
				Timer obj = operateTimer;
				if (obj != null)
				{
					obj.Cancel();
				}
				ShowTime_Achieve();
			});
		});
		ShowResultEffect();
		base.ui.com_Winner.Victory_Cut_in.Play();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.WIN, winner.player.Id);
		RefreshPlayerLabel((UICom_PlayerLabel)base.ui.com_Winner.com_PlayerLabel, winner);
		RefreshBustRole(base.ui.com_Winner.com_ShowRole, winner.player.standingPainting.GetCharacter().Item1);
		base.ui.com_Winner.isWinner.selectedIndex = ((!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(winner.player.Id)) ? 1 : 0);
		PlayerSettlement playerSettlementById = _BattleSettlement.GetPlayerSettlementById(winner.player.Id);
		base.ui.com_Winner.txt_WinnerCount.text = string.Format(1007.GetLocal(UIStringType.Achieve), playerSettlementById.TotalWinCount);
		base.ui.com_Winner.btn_Praise.grayed = false;
		base.ui.com_Winner.btn_Praise.onClick.Set((EventCallback0)delegate
		{
			base.ui.com_Winner.btn_Praise.onClick.Retain();
			base.ui.com_Winner.btn_Praise.Cut_in.Play(delegate
			{
				base.ui.com_Winner.btn_Praise.grayed = true;
			});
			TryPraise(base.ui.com_Winner.btn_Praise.Vfx, winner.player.Id);
		});
	}
}
