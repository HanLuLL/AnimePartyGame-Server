using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class MatchEntrancePanel : BasePanel<UIMatchEntrancePanel>
{
	private readonly List<GameModeInfoConfigure> GameModeInfos = new List<GameModeInfoConfigure>();

	private readonly List<string> GameModeTitles = new List<string>();

	public MatchEntrancePanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIMatchEntrancePanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.pveMatchType.selectedIndex = (CheckMutatorPve() ? 1 : 0);
		if (objs != null && objs.Length != 0 && objs[0] is RepeatedField<int> { Count: not 0 } repeatedField)
		{
			switch ((MapModeType)repeatedField[0])
			{
			case MapModeType.Pve:
				OpenMatchPVE();
				break;
			case MapModeType.Standard:
				OpenMatchPVP();
				break;
			case MapModeType.MutatorPve:
				OpenMatchMutatorPve();
				break;
			}
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
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.btn_RoomList.onClick.Add(OpenRoomList);
		base.ui.btn_MatchPVP.onClick.Add(OpenMatchPVP);
		base.ui.btn_MatchPVE.onClick.Add(OpenMatchPVE);
		base.ui.btn_MatchPVE_Short.onClick.Add(OpenMatchPVE);
		base.ui.btn_MatchPVP_Short.onClick.Add(OpenMatchPVP);
		base.ui.btn_Campaign.onClick.Add(OpenSoloLevel);
		base.ui.btn_MatchSportPVE.onClick.Add(OpenMatchMutatorPve);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.btn_RoomList.onClick.Remove(OpenRoomList);
		base.ui.btn_MatchPVP.onClick.Remove(OpenMatchPVP);
		base.ui.btn_MatchPVE.onClick.Remove(OpenMatchPVP);
		base.ui.btn_MatchPVE_Short.onClick.Remove(OpenMatchPVE);
		base.ui.btn_MatchPVP_Short.onClick.Remove(OpenMatchPVP);
		base.ui.btn_Campaign.onClick.Remove(OpenSoloLevel);
		base.ui.btn_MatchSportPVE.onClick.Remove(OpenMatchMutatorPve);
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
		RetainAllBtn();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel == null || SimpleSingletonProvider<UIManager>.inst.currentPanel is MatchEntrancePanel)
		{
			await SimpleSingletonProvider<UIManager>.inst.ReturnHomePanelDirectly();
		}
		ReleaseAllBtn();
	}

	private async void OpenRoomList()
	{
		RetainAllBtn();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.RoomList);
		ReleaseAllBtn();
	}

	private void OpenMatchPVP()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense())
		{
			List<GameModeInfoConfigure> pVPGameMode = SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.GetPVPGameMode();
			if (pVPGameMode.Count == 0)
			{
				Debug.LogError("无法获取PVP对应的游戏模式，请检查");
			}
			else if (TryCheckMatchStatus(MapModeType.Standard, MapModeType.Ultra, MapModeType.AsymmetricalBattle, MapModeType.LuckyStarBattle))
			{
				RetainAllBtn();
				SimpleSingletonProvider<GameLogicManager>.inst.match.RequestCreateMatchTeamC2S(pVPGameMode[0].MapModeType).OnFinished.AddOnce(FinishMatch);
			}
		}
	}

	private void OpenMatchPVE()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense() && TryCheckMatchStatus(MapModeType.Pve))
		{
			RetainAllBtn();
			SimpleSingletonProvider<GameLogicManager>.inst.match.RequestCreateMatchTeamC2S(MapModeType.Pve).OnFinished.AddOnce(FinishMatch);
		}
	}

	private void OpenMatchMutatorPve()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense() && TryCheckMatchStatus(MapModeType.MutatorPve))
		{
			RetainAllBtn();
			SimpleSingletonProvider<GameLogicManager>.inst.match.RequestCreateMatchTeamC2S(MapModeType.MutatorPve).OnFinished.AddOnce(FinishMatch);
		}
	}

	private async void FinishMatch(RPCAsyncResult result)
	{
		if (result.errId == 0)
		{
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Match);
		}
		ReleaseAllBtn();
	}

	private bool TryCheckMatchStatus(params MapModeType[] allowModes)
	{
		MatchData matchData = SimpleSingletonProvider<GameLogicManager>.inst.match.matchData;
		if (matchData != null && matchData.InTeam)
		{
			MapModeType curMapMode = matchData.GetCurMapMode();
			if (allowModes != null && allowModes.Contains(curMapMode))
			{
				SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Match).Forget();
				SimpleSingletonProvider<GameLogicManager>.inst.match.RequestRefreshMatchTeamInfoC2S();
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1102);
			}
			return false;
		}
		return true;
	}

	private async void OpenSoloLevel()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
		{
			RetainAllBtn();
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.SoloLevel);
			ReleaseAllBtn();
		}
	}

	private void RetainAllBtn()
	{
		base.ui.btn_Return.onClick.Retain();
		base.ui.btn_RoomList.onClick.Retain();
		base.ui.btn_MatchPVP.onClick.Retain();
		base.ui.btn_MatchPVE.onClick.Retain();
		base.ui.btn_MatchSportPVE.onClick.Retain();
		base.ui.btn_Campaign.onClick.Retain();
		base.ui.btn_MatchPVE_Short.onClick.Retain();
		base.ui.btn_MatchPVP_Short.onClick.Retain();
	}

	private void ReleaseAllBtn()
	{
		base.ui.btn_Return.onClick.Release();
		base.ui.btn_RoomList.onClick.Release();
		base.ui.btn_MatchPVP.onClick.Release();
		base.ui.btn_MatchPVE.onClick.Release();
		base.ui.btn_MatchSportPVE.onClick.Release();
		base.ui.btn_Campaign.onClick.Release();
		base.ui.btn_MatchPVE_Short.onClick.Release();
		base.ui.btn_MatchPVP_Short.onClick.Release();
	}

	private void RefreshGameMode()
	{
		List<GameModeInfoConfigure> gameModeInfos = SimpleSingletonProvider<GameLogicManager>.inst.roomList.gameModeInfos;
		GameModeTitles.Clear();
		GameModeInfos.Clear();
		for (int i = 0; i < gameModeInfos.Count; i++)
		{
			if (BattleConfig.IsPVE(gameModeInfos[i].MapModeType) && gameModeInfos[i].IsMatch && TimeHelper.ValidityTime(gameModeInfos[i].BeginTime, gameModeInfos[i].EndTime))
			{
				GameModeTitles.Add(gameModeInfos[i].NameID.GetLocal(UIStringType.GameMode));
				GameModeInfos.Add(gameModeInfos[i]);
			}
		}
	}

	private bool CheckMutatorPve()
	{
		GameModeInfoConfigure gameModeInfoConfigure = 12.GetGameModeInfoConfigure();
		if (gameModeInfoConfigure == null)
		{
			return false;
		}
		return TimeHelper.ValidityTime(gameModeInfoConfigure.BeginTime, gameModeInfoConfigure.EndTime);
	}

	public async UniTask<(Vector2, float, float)> ShowCampaignMask()
	{
		_FocusStatus = false;
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => base.ui.Cut_in.playing, SimpleSingletonProvider<UIManager>.inst.tutorial.Hide);
		Vector2 pt = base.ui.btn_Campaign.LocalToGlobal(Vector2.zero);
		Vector2 vector = GRoot.inst.GlobalToLocal(pt);
		float width = base.ui.btn_Campaign.width;
		float height = base.ui.btn_Campaign.height;
		SimpleSingletonProvider<UIManager>.inst.tutorial.ShowGuideMask(vector, width, height, isRect: true);
		return (vector, width, height);
	}

	public async UniTask FireClickOpenSoloLevel()
	{
		base.ui.btn_Campaign.FireClick(downEffect: true);
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.SoloLevel);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.ShowSoloLevelMask();
	}
}
