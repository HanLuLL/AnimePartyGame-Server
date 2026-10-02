using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UpgradeWindow : BaseWindow
{
	private readonly int[] effectIds = new int[4] { 41, 42, 43, 44 };

	private string effectKey;

	public async UniTask ShowPVETip(bool isWin)
	{
		await TryShowAsync();
		if (base.contentPane is UIUpgradeWindow uIUpgradeWindow)
		{
			uIUpgradeWindow.com_PVELabel.GG.Play();
			uIUpgradeWindow.type.selectedIndex = 1;
			uIUpgradeWindow.com_PVELabel.GameResult.selectedIndex = ((!isWin) ? 1 : 0);
			string animationName = (isWin ? "Cheer" : "Cry");
			BattlePlayerData playerBySlot = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerBySlot(0);
			RendererPVEAnime(playerBySlot, animationName, uIUpgradeWindow.com_PVELabel.graph_FirstPlayer);
			BattlePlayerData playerBySlot2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerBySlot(1);
			RendererPVEAnime(playerBySlot2, animationName, uIUpgradeWindow.com_PVELabel.graph_SecondPlayer);
			BattlePlayerData playerBySlot3 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerBySlot(2);
			RendererPVEAnime(playerBySlot3, animationName, uIUpgradeWindow.com_PVELabel.graph_ThirdPlayer);
			BattlePlayerData playerBySlot4 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerBySlot(3);
			RendererPVEAnime(playerBySlot4, animationName, uIUpgradeWindow.com_PVELabel.graph_FourthPlayer);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000);
			Hide();
		}
	}

	private void RendererPVEAnime(BattlePlayerData playerData, string animationName, GGraph graphAnime)
	{
		if (playerData == null)
		{
			graphAnime.visible = false;
		}
		else
		{
			SimpleSingletonProvider<CharacterAssetManager>.inst.PlayAnimation(playerData, animationName, graphAnime, Vector2.one * 15f);
		}
	}

	private void HidePVE()
	{
		if (base.contentPane is UIUpgradeWindow uIUpgradeWindow)
		{
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uIUpgradeWindow.com_PVELabel.graph_FirstPlayer);
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uIUpgradeWindow.com_PVELabel.graph_SecondPlayer);
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uIUpgradeWindow.com_PVELabel.graph_ThirdPlayer);
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uIUpgradeWindow.com_PVELabel.graph_FourthPlayer);
		}
	}

	public async UniTask ShowPVPTip(long playerId)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UIUpgradeWindow win = gComponent as UIUpgradeWindow;
		if (win == null)
		{
			return;
		}
		win.type.selectedIndex = 0;
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		effectKey = effectIds[playerDataById.player.Slot].GetEffectDataConfigure().EffectName;
		win.com_PVPLabel.Slot.selectedIndex = playerDataById.player.Slot;
		win.com_PVPLabel.loader_Character.loader_Character.url = playerDataById.player.standingPainting.GetCharacterLevelUp();
		win.com_PVPLabel.com_Level_1.Star.SetHook("Effect", delegate
		{
			ShowEffect(win.com_PVPLabel.com_Level_1.graph_Effect);
		});
		win.com_PVPLabel.com_Level_2.Star.SetHook("Effect", delegate
		{
			ShowEffect(win.com_PVPLabel.com_Level_2.graph_Effect);
		});
		win.com_PVPLabel.com_Level_3.Star.SetHook("Effect", delegate
		{
			ShowEffect(win.com_PVPLabel.com_Level_3.graph_Effect);
		});
		if (playerDataById.Property.level.Value == 1)
		{
			win.com_PVPLabel.Cut_in_Lv1.Play();
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => win.com_PVPLabel.Cut_in_Lv1.playing, base.Hide))
			{
				return;
			}
		}
		else if (playerDataById.Property.level.Value == 2)
		{
			win.com_PVPLabel.Cut_in_Lv2.Play();
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => win.com_PVPLabel.Cut_in_Lv2.playing, base.Hide))
			{
				return;
			}
		}
		else if (playerDataById.Property.level.Value == 3)
		{
			win.com_PVPLabel.Cut_in_Lv3.Play();
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => win.com_PVPLabel.Cut_in_Lv3.playing, base.Hide))
			{
				return;
			}
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.LEVELUP, playerId);
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(100);
		Hide();
		SimpleSingletonProvider<GameLogicManager>.inst.campaign.TryShowTutorial(playerId, 5077);
	}

	private void ShowEffect(GGraph graphEffect)
	{
		SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectKey, graphEffect, 200f).Forget();
	}

	private void HidePVP()
	{
		if (base.contentPane is UIUpgradeWindow uIUpgradeWindow)
		{
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(uIUpgradeWindow.com_PVPLabel.com_Level_1.graph_Effect);
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(uIUpgradeWindow.com_PVPLabel.com_Level_2.graph_Effect);
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(uIUpgradeWindow.com_PVPLabel.com_Level_3.graph_Effect);
		}
	}

	public async UniTask ShowTeamBattleTip(long teamId)
	{
		await TryShowAsync();
		if (base.contentPane is UIUpgradeWindow uIUpgradeWindow)
		{
			uIUpgradeWindow.com_PVELabel.GG.Play();
			uIUpgradeWindow.type.selectedIndex = 1;
			uIUpgradeWindow.com_PVELabel.GameResult.selectedIndex = 0;
			List<BattlePlayerData> playerDataByTeamId = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataByTeamId(teamId);
			for (int i = 0; i < playerDataByTeamId.Count; i++)
			{
				RendererTeamBattleAnime(playerDataByTeamId[i], "Cheer", GetGraphPlayerItem(i));
			}
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000);
			Hide();
		}
	}

	private GGraph GetGraphPlayerItem(int index)
	{
		if (!(base.contentPane is UIUpgradeWindow uIUpgradeWindow))
		{
			return null;
		}
		return index switch
		{
			0 => uIUpgradeWindow.com_PVELabel.graph_FirstPlayer, 
			1 => uIUpgradeWindow.com_PVELabel.graph_SecondPlayer, 
			2 => uIUpgradeWindow.com_PVELabel.graph_ThirdPlayer, 
			3 => uIUpgradeWindow.com_PVELabel.graph_FourthPlayer, 
			_ => null, 
		};
	}

	private void RendererTeamBattleAnime(BattlePlayerData playerData, string animationName, GGraph graphAnime)
	{
		if (graphAnime != null)
		{
			if (playerData == null)
			{
				graphAnime.visible = false;
			}
			else
			{
				SimpleSingletonProvider<CharacterAssetManager>.inst.PlayAnimation(playerData, animationName, graphAnime, Vector2.one * 15f);
			}
		}
	}

	private void HideTeamBattle()
	{
		if (base.contentPane is UIUpgradeWindow uIUpgradeWindow)
		{
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uIUpgradeWindow.com_PVELabel.graph_FirstPlayer);
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uIUpgradeWindow.com_PVELabel.graph_SecondPlayer);
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uIUpgradeWindow.com_PVELabel.graph_ThirdPlayer);
			SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uIUpgradeWindow.com_PVELabel.graph_FourthPlayer);
		}
	}

	public UpgradeWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIUpgradeWindow.CreateInstance();
		base.OnInit();
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

	protected override void OnHide()
	{
		base.OnHide();
		HidePVP();
		HidePVE();
		HideTeamBattle();
	}
}
