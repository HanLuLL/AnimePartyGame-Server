using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class TipsWindow : BaseWindow
{
	private CoroutineManager.CoroutineState _tipsCoroutine;

	private readonly List<long> thinkPlayers = new List<long>(4);

	public TipsWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UITipsWindow.CreateInstance();
		base.OnInit();
	}

	public override void Dispose()
	{
		CoroutineManager.CoroutineState tipsCoroutine = _tipsCoroutine;
		if (tipsCoroutine != null && tipsCoroutine.Running)
		{
			_tipsCoroutine.Stop();
		}
		base.Dispose();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	public async void ShowTips(int msgId, float duration = 1f)
	{
		if (duration <= 0f)
		{
			return;
		}
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UITipsWindow win = gComponent as UITipsWindow;
		if (win != null)
		{
			win.common.visible = true;
			win.common.txt_Content.text = msgId.GetLocal(UIStringType.Message);
			CoroutineManager.CoroutineState tipsCoroutine = _tipsCoroutine;
			if (tipsCoroutine != null && tipsCoroutine.Running)
			{
				_tipsCoroutine.Stop();
			}
			_tipsCoroutine = MonoSingletonProvider<CoroutineManager>.inst.CreateInvoke(duration, delegate
			{
				win.common.visible = false;
			});
			_tipsCoroutine.Start();
		}
	}

	public async void ShowTips(string content, float duration = 1f)
	{
		if (duration <= 0f)
		{
			return;
		}
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UITipsWindow win = gComponent as UITipsWindow;
		if (win != null)
		{
			win.common.visible = true;
			win.common.txt_Content.text = content;
			CoroutineManager.CoroutineState tipsCoroutine = _tipsCoroutine;
			if (tipsCoroutine != null && tipsCoroutine.Running)
			{
				_tipsCoroutine.Stop();
			}
			_tipsCoroutine = MonoSingletonProvider<CoroutineManager>.inst.CreateInvoke(duration, delegate
			{
				win.common.visible = false;
			});
			_tipsCoroutine.Start();
		}
	}

	public async UniTask ShowTipsAndWait(string content, float duration = 1f)
	{
		if (!(duration <= 0f))
		{
			await TryShowAsync();
			GComponent gComponent = base.contentPane;
			if (gComponent is UITipsWindow win)
			{
				win.common.visible = true;
				win.common.txt_Content.text = content;
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
				win.common.visible = false;
			}
		}
	}

	public async void ShowSingleStart(int slot, float duration = 1.5f)
	{
		if (!(duration <= 0f))
		{
			await TryShowAsync();
			GComponent gComponent = base.contentPane;
			if (gComponent is UITipsWindow win)
			{
				win.yourRoundStart.visible = true;
				win.yourRoundStart.Player.selectedIndex = slot;
				win.yourRoundStart.Show.Play();
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
				win.yourRoundStart.visible = false;
			}
		}
	}

	public async UniTask ShowRoundRewardTip(string _goldNum, string _cardNum, float duration = 0.5f)
	{
		if (!(duration <= 0f))
		{
			await TryShowAsync();
			GComponent gComponent = base.contentPane;
			if (gComponent is UITipsWindow win)
			{
				win.roundReward.visible = true;
				win.roundReward.txt_CardNum.text = _cardNum;
				win.roundReward.txt_GoldNum.text = _goldNum;
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
				win.roundReward.visible = false;
			}
		}
	}

	public async UniTask ShowRoundStartTip(string nickname, int slotIndex, float duration = 0.5f)
	{
		if (!(duration <= 0f))
		{
			await TryShowAsync();
			GComponent gComponent = base.contentPane;
			if (gComponent is UITipsWindow win)
			{
				win.com_RoundStart.visible = true;
				string text = GameConfig.HTMLStringRGB(slotIndex);
				win.com_RoundStart.txt_Title.text = string.Format(10999.GetLocal(UIStringType.Message), "[color=#" + text + "]" + nickname + "[/color]");
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
				win.com_RoundStart.visible = false;
			}
		}
	}

	public async UniTask ShowTopTip(string content)
	{
		await TryShowAsync();
		if (base.contentPane is UITipsWindow uITipsWindow)
		{
			uITipsWindow.com_RoundStart.visible = true;
			uITipsWindow.com_RoundStart.txt_Title.text = content;
		}
	}

	public void HideTopTip()
	{
		if (base.contentPane is UITipsWindow uITipsWindow)
		{
			uITipsWindow.com_RoundStart.visible = false;
		}
	}

	public async UniTask ShowPassiveSkillTip(string skillName, float duration = 0.5f)
	{
		if (!(duration <= 0f))
		{
			await TryShowAsync();
			GComponent gComponent = base.contentPane;
			if (gComponent is UITipsWindow win)
			{
				win.passiveSkillTrigger.visible = true;
				win.passiveSkillTrigger.txt_PassiveSkillName.text = skillName;
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
				win.passiveSkillTrigger.visible = false;
			}
		}
	}

	public async UniTask ShowLevelUPlTip(long playerId, float duration = 0.5f)
	{
		if (!(duration <= 0f))
		{
			await TryShowAsync();
			GComponent gComponent = base.contentPane;
			if (gComponent is UITipsWindow win)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
				((UICom_PlayerInfo)win.levelUp.com_playerInfo).RefreshData(playerDataById);
				win.levelUp.visible = true;
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
				win.levelUp.visible = false;
			}
		}
	}

	public async void ShowThinkingTip(long playerId, int thinkId)
	{
		await TryShowAsync();
		if (base.contentPane is UITipsWindow uITipsWindow)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById != null && !(playerDataById.CharacterInst == null) && playerDataById.characterType != CharacterType.Monster)
			{
				string text = GameConfig.HTMLStringRGB(playerDataById.player.Slot);
				uITipsWindow.com_Thinking.txt_Title.text = string.Format(thinkId.GetLocal(UIStringType.Message), "[color=#" + text + "]" + playerDataById.player.GetNick() + "[/color]");
				uITipsWindow.com_Thinking.visible = true;
				await playerDataById.CharacterInst.PlayThinkingEffect();
			}
		}
	}

	public void HideThinkingTip(long playerId)
	{
		if (base.contentPane is UITipsWindow uITipsWindow)
		{
			uITipsWindow.com_Thinking.visible = false;
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById != null && !(playerDataById.CharacterInst == null))
			{
				playerDataById.CharacterInst.StopThinkEffect();
			}
		}
	}

	public async void ShowChangeActionTip(float duration = 1.5f)
	{
		if (!(duration <= 0f))
		{
			await TryShowAsync();
			GComponent gComponent = base.contentPane;
			if (gComponent is UITipsWindow win)
			{
				win.changeActionOrder.visible = true;
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
				win.changeActionOrder.visible = false;
			}
		}
	}

	public async UniTask<bool> ShowLottery(float duration = 1f)
	{
		if (duration <= 0f)
		{
			return true;
		}
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UITipsWindow win))
		{
			return true;
		}
		win.lotteryRound.visible = true;
		bool num = await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
		win.lotteryRound.visible = false;
		return !num;
	}

	public async UniTask<bool> ShowChosenOne(string title, string Count, float duration = 1f)
	{
		if (duration <= 0f)
		{
			return true;
		}
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UITipsWindow win))
		{
			return true;
		}
		win.com_ChosenOne.txt_Title.text = title;
		win.com_ChosenOne.txt_GoldNum.text = Count;
		win.com_ChosenOne.visible = true;
		bool num = await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
		win.com_ChosenOne.visible = false;
		return !num;
	}

	public async UniTask ShowPVETaskTip(MapMissionTargetData targetData, float duration = 0.5f, bool missionFailed = false)
	{
		if (duration <= 0f)
		{
			return;
		}
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		if (gComponent is UITipsWindow win)
		{
			win.com_PveTaskTip.visible = true;
			PVEMissionInfoConfigure pVEMissionInfo = targetData.PVEMissionInfo;
			win.com_PveTaskTip.txt_Title.text = targetData.GetTitle();
			win.com_PveTaskTip.txt_Progress.text = targetData.GetProgressDesc();
			win.com_PveTaskTip.isComplete.selectedIndex = (missionFailed ? 1 : 0);
			if (missionFailed)
			{
				win.com_PveTaskTip.txt_Desc.text = "";
				win.com_PveTaskTip.txt_Desc_Failed.text = pVEMissionInfo.FailedDescID.GetLocal(UIStringType.PVEMission);
			}
			else
			{
				win.com_PveTaskTip.txt_Desc.text = pVEMissionInfo.RewardDescID.GetLocal(UIStringType.PVEMission);
				win.com_PveTaskTip.txt_Desc_Failed.text = "";
			}
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
			win.com_PveTaskTip.visible = false;
		}
	}

	public async UniTask ShowMultiplePlayerThink(long playerId, int thinkId)
	{
		if (thinkPlayers.Contains(playerId))
		{
			return;
		}
		await TryShowAsync();
		if (base.contentPane is UITipsWindow uITipsWindow)
		{
			thinkPlayers.Add(playerId);
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById != null && !(playerDataById.CharacterInst == null) && playerDataById.characterType != CharacterType.Monster)
			{
				uITipsWindow.com_Thinking.visible = true;
				RefreshMultiplePlayerThink(thinkId);
				await playerDataById.CharacterInst.PlayThinkingEffect();
			}
		}
	}

	public void HideMultiplePlayerThink(long playerId, int thinkId)
	{
		if (base.contentPane is UITipsWindow uITipsWindow)
		{
			if (thinkPlayers.Contains(playerId))
			{
				thinkPlayers.Remove(playerId);
			}
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById != null && !(playerDataById.CharacterInst == null))
			{
				playerDataById.CharacterInst.StopThinkEffect();
				RefreshMultiplePlayerThink(thinkId);
				uITipsWindow.com_Thinking.visible = thinkPlayers.Count > 0;
			}
		}
	}

	private void RefreshMultiplePlayerThink(int thinkId)
	{
		if (!(base.contentPane is UITipsWindow uITipsWindow))
		{
			return;
		}
		BattleLogic battleLogic = SimpleSingletonProvider<GameLogicManager>.inst?.battle;
		if (battleLogic == null)
		{
			Debug.LogError("[RefreshMultiplePlayerThink] Battle is null");
			uITipsWindow.com_Thinking.txt_Title.text = "";
		}
		else
		{
			if (thinkPlayers.Count <= 0)
			{
				return;
			}
			string text = "";
			for (int i = 0; i < thinkPlayers.Count; i++)
			{
				long num = thinkPlayers[i];
				BattlePlayerData playerDataById = battleLogic.GetPlayerDataById(num);
				if (playerDataById?.player == null)
				{
					Debug.LogError($"获取玩家：{num} 数据, data: {playerDataById == null}, player: {playerDataById?.player == null}");
					continue;
				}
				string nick = playerDataById.player.GetNick();
				if (string.IsNullOrEmpty(nick))
				{
					Debug.LogError($"获取玩家：{num} 数据, playerNick = null");
					continue;
				}
				string text2 = GameConfig.HTMLStringRGB(playerDataById.player.Slot);
				text = text + "[color=#" + text2 + "]" + nick + "[/color]" + ((thinkPlayers.Count == i + 1) ? "" : "、");
			}
			uITipsWindow.com_Thinking.txt_Title.text = string.Format(thinkId.GetLocal(UIStringType.Message), text);
		}
	}

	public async UniTask ShowPVEClueTaskTip(ClueMissionTargetData targetData, bool showFact = false, float duration = 1.1f)
	{
		if (duration <= 0f)
		{
			return;
		}
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		if (gComponent is UITipsWindow win)
		{
			win.com_PveClueTaskTip.visible = true;
			win.com_PveClueTaskTip.fact.selectedIndex = (showFact ? 1 : 0);
			if (targetData != null && !showFact)
			{
				win.com_PveClueTaskTip.txt_Title.text = targetData.GetTitle();
			}
			if (showFact)
			{
				win.com_PveClueTaskTip.Clue_Cut_in.Stop();
				win.com_PveClueTaskTip.ZXJL_Cut_in.Play();
			}
			else
			{
				win.com_PveClueTaskTip.ZXJL_Cut_in.Stop();
				win.com_PveClueTaskTip.Clue_Cut_in.Play();
			}
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
			win.com_PveClueTaskTip.visible = false;
			Controller language = win.com_PveClueTaskTip.Language;
			language.selectedIndex = GameSettings.languageType switch
			{
				LanguageType.SimplifiedChinese => 0, 
				LanguageType.TraditionalChinese => 0, 
				LanguageType.Japanese => 1, 
				_ => 2, 
			};
		}
	}
}
