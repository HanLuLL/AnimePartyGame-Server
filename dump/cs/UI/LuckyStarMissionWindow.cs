using System.Collections.Generic;
using System.Linq;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;

namespace UI;

public class LuckyStarMissionWindow : BaseWindow
{
	public LuckyStarMissionWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILuckyStarMissionWindow.CreateInstance();
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

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UILuckyStarMissionWindow uILuckyStarMissionWindow)
		{
			uILuckyStarMissionWindow.btn_mohu.onClick.Add(base.Hide);
			SimpleSingletonProvider<GameLogicManager>.inst.gameModePlay.signal.luckyStarMission.AddListener(RefreshMissionProcess);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILuckyStarMissionWindow uILuckyStarMissionWindow)
		{
			uILuckyStarMissionWindow.btn_mohu.onClick.Remove(base.Hide);
			SimpleSingletonProvider<GameLogicManager>.inst.gameModePlay.signal.luckyStarMission.RemoveListener(RefreshMissionProcess);
		}
	}

	private void RefreshMissionProcess(int obj)
	{
		if (base.contentPane is UILuckyStarMissionWindow uILuckyStarMissionWindow && uILuckyStarMissionWindow.stage.selectedIndex == 1)
		{
			TryOpenRoomMission().Forget();
		}
	}

	public async UniTask ShowRoomMissionAtGameStart()
	{
		await TryOpenRoomMission();
		if (base.contentPane is UILuckyStarMissionWindow uILuckyStarMissionWindow)
		{
			uILuckyStarMissionWindow.btn_mohu.touchable = false;
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(3000);
			Hide();
		}
	}

	public async UniTask TryOpenRoomMission()
	{
		BattlePlayerData selfPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayer?.player == null)
		{
			return;
		}
		await TryShowAsync();
		if (!(base.contentPane is UILuckyStarMissionWindow uILuckyStarMissionWindow))
		{
			return;
		}
		uILuckyStarMissionWindow.stage.selectedIndex = 1;
		List<LuckyStarMissionData> missions = selfPlayer.player.LuckyStarMissions.Where((LuckyStarMissionData mission) => mission.State == 1).ToList();
		uILuckyStarMissionWindow.list_Card.itemRenderer = delegate(int index, GObject item)
		{
			if (index < missions.Count && item is UILuckyStarMission_Com_Card com_Card)
			{
				RefreshMissionCom(missions[index], selfPlayer, com_Card);
			}
		};
		uILuckyStarMissionWindow.list_Card.numItems = missions.Count;
		uILuckyStarMissionWindow.btn_mohu.touchable = true;
	}

	public async UniTask TryShowFinishMission(long _playerId, LuckyStarMissionData mission)
	{
		if (mission == null)
		{
			return;
		}
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (playerData != null)
		{
			await TryShowAsync();
			if (base.contentPane is UILuckyStarMissionWindow uILuckyStarMissionWindow)
			{
				uILuckyStarMissionWindow.stage.selectedIndex = 2;
				uILuckyStarMissionWindow.loader_FinishPlayer.url = playerData.GetCharacterHeadUrl();
				uILuckyStarMissionWindow.txt_Finish.text = 1000009.GetLocal(UIStringType.GUI);
				RefreshMissionCom(mission, playerData, uILuckyStarMissionWindow.com_ShowFinishMIssion);
				uILuckyStarMissionWindow.MissionComplete.Play();
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
				Hide();
			}
		}
	}

	private void RefreshMissionCom(LuckyStarMissionData mission, BattlePlayerData selfPlayer, UILuckyStarMission_Com_Card com_Card)
	{
		if (base.contentPane is UILuckyStarMissionWindow uILuckyStarMissionWindow)
		{
			com_Card.loader_Icon.url = mission.Config.Image;
			com_Card.list_Star.numItems = mission.Config.LuckyStarReward;
			if (uILuckyStarMissionWindow.stage.selectedIndex == 1)
			{
				com_Card.txt_Content.text = mission.GetMissionDesc();
				com_Card.txt_Progress.text = mission.GetProgressDesc();
				com_Card.txt_Progress.visible = true;
			}
			else if (uILuckyStarMissionWindow.stage.selectedIndex == 2)
			{
				com_Card.txt_Content.text = mission.GetRewardDesc();
				com_Card.txt_Progress.visible = false;
			}
			com_Card.txt_Title.text = mission.Config.NameID.GetLocal(UIStringType.LuckyStarBattle);
		}
	}
}
