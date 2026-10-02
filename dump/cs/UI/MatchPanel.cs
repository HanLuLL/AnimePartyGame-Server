using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class MatchPanel : BasePanel<UIMatchPanel>
{
	private MapModeType _currentMapMode;

	private UIMatch_Com_MapItem _selectedMapItem;

	private UIMatch_Button_DifficultyItem _selectedDifficultyItem;

	private UIMatch_Button_PVPModeItem _selectedModeItem;

	private MatchData _matchData => SimpleSingletonProvider<GameLogicManager>.inst.match.matchData;

	public MatchPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIMatchPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		_currentMapMode = _matchData.GetCurMapMode();
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		((UICom_RoomPlayer)base.ui.com_Player).InitComponents();
		base.ui.btn_CancelMatch.title = 1020019.GetLocal(UIStringType.GUI);
		base.ui.btn_ExitTeam.title = 1020019.GetLocal(UIStringType.GUI);
		base.ui.btn_StartMatch.title = 1020015.GetLocal(UIStringType.GUI);
	}

	public override void Refresh()
	{
		base.Refresh();
		RefreshComponentStatus(lockStatus: false);
		RefreshMatchInfo();
		RefreshMatchText();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		((UICom_RoomPlayer)base.ui.com_Player).AddEvent();
		base.ui.btn_StartMatch.onClick.Add(OnClickMatchBtn);
		base.ui.btn_CancelMatch.onClick.Add(ExitTeam);
		base.ui.btn_ExitTeam.onClick.Add(ExitTeam);
		base.ui.btn_Ready.onClick.Add(OnRequestReady);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		((UICom_RoomPlayer)base.ui.com_Player).RemoveEvent();
		base.ui.btn_StartMatch.onClick.Remove(OnClickMatchBtn);
		base.ui.btn_CancelMatch.onClick.Remove(ExitTeam);
		base.ui.btn_ExitTeam.onClick.Remove(ExitTeam);
		base.ui.btn_Ready.onClick.Remove(OnRequestReady);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.match.signal.StartMatch.AddListener(OnSignalStartMatch);
		SimpleSingletonProvider<GameLogicManager>.inst.match.signal.CancelMatch.AddListener(OnSignalCancelMatch);
		SimpleSingletonProvider<GameLogicManager>.inst.match.signal.TeamChange.AddListener(OnSignalTeamChange);
		SimpleSingletonProvider<GameLogicManager>.inst.match.signal.UpdateReady.AddListener(OnSignalUpdateReady);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.match.signal.StartMatch.RemoveListener(OnSignalStartMatch);
		SimpleSingletonProvider<GameLogicManager>.inst.match.signal.CancelMatch.RemoveListener(OnSignalCancelMatch);
		SimpleSingletonProvider<GameLogicManager>.inst.match.signal.TeamChange.RemoveListener(OnSignalTeamChange);
		SimpleSingletonProvider<GameLogicManager>.inst.match.signal.UpdateReady.RemoveListener(OnSignalUpdateReady);
	}

	public override void Close()
	{
		base.ui.list_Relation.numItems = 0;
		((UICom_RoomPlayer)base.ui.com_Player).Close();
		base.Close();
	}

	public override void Dispose()
	{
		((UICom_RoomPlayer)base.ui.com_Player).DisposeCom();
		base.Dispose();
	}

	private void OnSignalStartMatch()
	{
		UpdateTimerStatus(enable: true);
		RefreshComponentStatus(lockStatus: true);
	}

	private void OnSignalTeamChange()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_matchData.MatchLeaderId))
		{
			RefreshCurMapItem();
			RefreshCurPVPMode();
		}
		if (BattleConfig.IsPVE((int)_matchData.GetCurMapMode()))
		{
			RefreshCurPVEDifficulty();
		}
		RefreshCurMapItemPVEMonster();
		RefreshTeamInfo();
	}

	private void OnSignalCancelMatch(MatchTeamInfo.Types.State preMatchStatus)
	{
		if (preMatchStatus == MatchTeamInfo.Types.State.Matching && _matchData.MatchStatus == MatchTeamInfo.Types.State.Waiting)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1100);
		}
		UpdateTimerStatus(enable: false);
		RefreshComponentStatus(lockStatus: false);
		RefreshTeamInfo();
	}

	private void OnRequestReady()
	{
		long playerId = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		RoomPlayer roomPlayer = _matchData.TeamPlayers.Find((RoomPlayer x) => x.Id == playerId);
		base.ui.btn_Ready.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.match.RequestMatchTeamReadyC2S(_matchData.TeamId, playerId, !roomPlayer.RoomReady);
		base.ui.btn_Ready.onClick.Release();
	}

	private void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		if (_matchData.InTeam && _matchData.TeamPlayers.Count == 1)
		{
			MatchTeamInfo.Types.State matchStatus = _matchData.MatchStatus;
			if (matchStatus == MatchTeamInfo.Types.State.None || matchStatus == MatchTeamInfo.Types.State.Waiting)
			{
				ExitTeam();
				goto IL_0056;
			}
		}
		ReturnToLastPanel();
		goto IL_0056;
		IL_0056:
		base.ui.btn_Return.onClick.Release();
	}

	public void ReturnToLastPanel()
	{
		SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
	}

	private void OnClickMatchBtn()
	{
		if (_matchData.MatchStatus == MatchTeamInfo.Types.State.Matching)
		{
			OnRequestCancelMatch();
		}
		else
		{
			OnRequestStartMatch();
		}
	}

	private void OnRequestStartMatch()
	{
		long playerId = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		if (_matchData.TeamPlayers.Find((RoomPlayer x) => !x.RoomReady && x.Id != playerId) != null)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1021);
			return;
		}
		int matchMapId = _matchData.MatchMapId;
		bool flag = true;
		if (matchMapId != 0)
		{
			MapInfoConfigure mapDataConfigure = _matchData.MatchMapId.GetMapDataConfigure();
			flag = mapDataConfigure != null && mapDataConfigure.BeginTime != null && mapDataConfigure.EndTime != null && TimeHelper.ValidityTime(mapDataConfigure.BeginTime, mapDataConfigure.EndTime);
		}
		if (!BattleConfig.IsPVE((int)_currentMapMode) || flag)
		{
			base.ui.btn_StartMatch.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.match.RequestStartMatchC2S(_matchData.TeamId).OnFinishedOnly.AddOnce(delegate
			{
				base.ui.btn_StartMatch.onClick.Release();
			});
			return;
		}
		base.ui.btn_StartMatch.onClick.Retain();
		var (mapId, difficulty) = _matchData.GetDefaultMapInfo((int)_currentMapMode);
		SimpleSingletonProvider<GameLogicManager>.inst.match.RequestChangeMatchTeamC2S(_currentMapMode, mapId, difficulty, _matchData.TeamId).OnFinishedOnly.AddOnce(delegate
		{
			RefreshMatchInfo();
			base.ui.btn_StartMatch.onClick.Release();
		});
	}

	private void OnRequestCancelMatch()
	{
		base.ui.btn_StartMatch.onClick.Retain();
		if (_matchData.MatchStatus == MatchTeamInfo.Types.State.Matching)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.match.RequestCancelMatchC2S(_matchData.TeamId).OnFinishedOnly.AddOnce(delegate
			{
				UpdateTimerStatus(enable: false);
				base.ui.btn_StartMatch.onClick.Release();
			});
		}
		else
		{
			base.ui.btn_StartMatch.onClick.Release();
		}
	}

	private void OnRequestChangeMatchTeam(int mapId, int difficulty, System.Action changeAction)
	{
		if (_matchData.MatchMapId != mapId || difficulty != _matchData.MatchDifficulty || _currentMapMode != _matchData.GetCurMapMode())
		{
			SimpleSingletonProvider<GameLogicManager>.inst.match.RequestChangeMatchTeamC2S(_currentMapMode, mapId, difficulty, _matchData.TeamId).OnFinishedOnly.AddListener(delegate
			{
				changeAction?.Invoke();
			});
		}
	}

	private (int, int) GetCurrentSelectedMapInfo()
	{
		int item = _selectedMapItem?.MapId ?? 0;
		int item2 = (int)((_selectedDifficultyItem?.Info != null) ? _selectedDifficultyItem.Info.GameDifficultyType : GameDifficultyType.Easy);
		UIMatch_Com_MapItem selectedMapItem = _selectedMapItem;
		if (selectedMapItem != null && selectedMapItem.IsExtreme)
		{
			item2 = 4;
		}
		return (item, item2);
	}

	private void UpdateTimerStatus(bool enable)
	{
		if (enable)
		{
			SimpleSingletonProvider<UIManager>.inst.MatchInfo.StartMatch().Forget();
		}
		RefreshMatchText();
	}

	private void ExitTeam()
	{
		base.ui.btn_ExitTeam.onClick.Retain();
		if (_matchData.MatchStatus == MatchTeamInfo.Types.State.Matching)
		{
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1101, delegate
			{
				RequestExitMatchTeam(delegate
				{
					base.ui.btn_ExitTeam.onClick.Release();
				});
			}, delegate
			{
				base.ui.btn_ExitTeam.onClick.Release();
			}).Forget();
		}
		else if (_matchData.MatchStatus == MatchTeamInfo.Types.State.Waiting)
		{
			RequestExitMatchTeam(delegate
			{
				base.ui.btn_ExitTeam.onClick.Release();
			});
		}
		else
		{
			base.ui.btn_ExitTeam.onClick.Release();
		}
	}

	private void RequestExitMatchTeam(System.Action action)
	{
		long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		SimpleSingletonProvider<GameLogicManager>.inst.match.RequestExitMatchTeamC2S(playerID, _matchData.TeamId).OnFinishedOnly.AddOnce(delegate
		{
			UpdateTimerStatus(enable: false);
			ReturnToLastPanel();
			action?.Invoke();
		});
	}

	private void RefreshMatchInfo()
	{
		if (_currentMapMode == MapModeType.Pve)
		{
			base.ui.mode.selectedIndex = 0;
			RefreshPVEDifficulty();
		}
		else if (_currentMapMode == MapModeType.MutatorPve)
		{
			base.ui.mode.selectedIndex = 2;
			RefreshPVEDifficulty();
		}
		else
		{
			base.ui.mode.selectedIndex = 1;
			RefreshPVPMode();
		}
		RefreshMap();
		RefreshTeamInfo();
	}

	private void RefreshCurMapItem()
	{
		int curMapId = _matchData.MatchMapId;
		GObject mapItem = base.ui.list_Map._children.Find(delegate(GObject x)
		{
			if (!(x is UIMatch_Com_MapItem uIMatch_Com_MapItem))
			{
				return false;
			}
			if (uIMatch_Com_MapItem.MapId != curMapId)
			{
				return false;
			}
			return (_matchData.MatchDifficulty == 4) ? uIMatch_Com_MapItem.IsExtreme : (!uIMatch_Com_MapItem.IsExtreme);
		});
		RefreshMapItem(mapItem);
	}

	private void RefreshMapItem(GObject _MapItem)
	{
		if (_MapItem is UIMatch_Com_MapItem selectedMapItem)
		{
			MapModeType curMapMode = _matchData.GetCurMapMode();
			if (_selectedMapItem != null)
			{
				_selectedMapItem.RefreshSelectStatus(status: false);
			}
			_selectedMapItem = selectedMapItem;
			_selectedMapItem.RefreshSelectStatus(status: true);
			if (BattleConfig.IsPVE(curMapMode))
			{
				RefreshPVEDifficultyStatus(_selectedMapItem.IsExtreme);
			}
			RefreshMapShow(_selectedMapItem.Info);
		}
	}

	private void RefreshMap()
	{
		base.ui.txt_SelectMap.text = 1020007.GetLocal(UIStringType.GUI);
		MapModeType curMapMode = _matchData.GetCurMapMode();
		base.ui.list_Map.lineGap = (BattleConfig.IsPVE(curMapMode) ? (-38) : 0);
		base.ui.list_Map.itemRenderer = delegate(int index, GObject item)
		{
			UIMatch_Com_MapItem btn_Map = item as UIMatch_Com_MapItem;
			if (btn_Map != null)
			{
				btn_Map.visible = false;
				btn_Map.CutIn.Play(1, 0.01f * (float)index, delegate
				{
					btn_Map.visible = true;
				}, null);
				btn_Map.RefreshMapItem(_matchData.MapIds[index], curMapMode);
				btn_Map.RefreshMonsterHead(_matchData.MatchDifficulty);
				if (_selectedMapItem != null)
				{
					_selectedMapItem.RefreshSelectStatus(status: false);
				}
				btn_Map.SetEvent(delegate
				{
					if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_matchData.MatchLeaderId) && _matchData.IsVailOperate())
					{
						RefreshMapItem(btn_Map);
						(int, int) currentSelectedMapInfo = GetCurrentSelectedMapInfo();
						OnRequestChangeMatchTeam(currentSelectedMapInfo.Item1, currentSelectedMapInfo.Item2, null);
					}
				});
			}
		};
		base.ui.list_Map.numItems = _matchData.MapIds.Count;
		RefreshCurMapItem();
	}

	private void RefreshMapShow(MapInfoConfigure info)
	{
		if (info == null)
		{
			base.ui.loader_Detail.visible = false;
			base.ui.loader_Map.url = "ui://qxwapsemr26s1i";
		}
		else
		{
			base.ui.loader_Map.url = info.MapImage;
			MapModeType currentMapMode = _currentMapMode;
			if (currentMapMode == MapModeType.Pve || currentMapMode == MapModeType.Standard || currentMapMode == MapModeType.AsymmetricalBattle || currentMapMode == MapModeType.LuckyStarBattle || currentMapMode == MapModeType.MutatorPve)
			{
				base.ui.loader_Detail.visible = true;
				base.ui.loader_Detail.onClick.Set(ShowMapDetail);
				base.ui.loader_Map.onClick.Set(ShowMapDetail);
			}
			else
			{
				base.ui.loader_Detail.visible = false;
			}
		}
		base.ui.loader_Map.touchable = base.ui.loader_Detail.visible;
		void ShowMapDetail()
		{
			base.ui.loader_Detail.onClick.Retain();
			base.ui.loader_Map.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.map.ShowMapDetail(_currentMapMode, info);
			base.ui.loader_Detail.onClick.Release();
			base.ui.loader_Map.onClick.Release();
		}
	}

	private void RefreshCurMapItemPVEMonster()
	{
		if (_selectedMapItem != null)
		{
			_selectedMapItem.RefreshMonsterHead(_matchData.MatchDifficulty);
		}
	}

	private void RefreshCurPVEDifficulty()
	{
		if (BattleConfig.IsPVE(_matchData.GetCurMapMode()))
		{
			int curDifficulty = _matchData.MatchDifficulty;
			GObject difficultyItem = base.ui.list_Relation._children.Find((GObject x) => (x is UIMatch_Button_DifficultyItem uIMatch_Button_DifficultyItem && uIMatch_Button_DifficultyItem.Info.GameDifficultyType == (GameDifficultyType)curDifficulty) ? true : false);
			RefreshDifficultyItem(difficultyItem);
		}
	}

	private void RefreshDifficultyItem(GObject difficultyItem)
	{
		if (difficultyItem is UIMatch_Button_DifficultyItem selectedDifficultyItem)
		{
			if (_selectedDifficultyItem != null)
			{
				_selectedDifficultyItem.status.selectedIndex = 0;
			}
			_selectedDifficultyItem = selectedDifficultyItem;
			_selectedDifficultyItem.status.selectedIndex = 1;
			RefreshWarning();
		}
	}

	private void RefreshPVEDifficulty()
	{
		base.ui.list_Relation.itemProvider = (int _) => "ui://qxwapsemgib2u";
		base.ui.list_Relation.itemRenderer = delegate(int index, GObject item)
		{
			UIMatch_Button_DifficultyItem btn_Difficulty = item as UIMatch_Button_DifficultyItem;
			if (btn_Difficulty != null)
			{
				int difficultyType = _matchData.DefaultDifficultyConfigTypes[index];
				btn_Difficulty.Refresh(difficultyType);
				btn_Difficulty.touchable = true;
				btn_Difficulty.UpdateStatus();
				btn_Difficulty.onClick.Set((EventCallback0)delegate
				{
					if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_matchData.MatchLeaderId) && _matchData.IsVailOperate())
					{
						if (btn_Difficulty.grayed)
						{
							SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(11025);
						}
						else
						{
							btn_Difficulty.onClick.Retain();
							RefreshDifficultyItem(btn_Difficulty);
							(int, int) currentSelectedMapInfo = GetCurrentSelectedMapInfo();
							OnRequestChangeMatchTeam(currentSelectedMapInfo.Item1, currentSelectedMapInfo.Item2, null);
							btn_Difficulty.onClick.Release();
						}
					}
				});
			}
		};
		base.ui.list_Relation.numItems = _matchData.DefaultDifficultyConfigTypes.Count;
		RefreshCurPVEDifficulty();
		base.ui.txt_SelectRelation.text = 1020008.GetLocal(UIStringType.GUI);
	}

	private void RefreshPVEDifficultyStatus(bool isExtreme)
	{
		foreach (GObject child in base.ui.list_Relation._children)
		{
			if (!(child is UIMatch_Button_DifficultyItem uIMatch_Button_DifficultyItem))
			{
				return;
			}
			if (isExtreme)
			{
				uIMatch_Button_DifficultyItem.touchable = false;
				uIMatch_Button_DifficultyItem.grayed = true;
			}
			else
			{
				uIMatch_Button_DifficultyItem.touchable = true;
				uIMatch_Button_DifficultyItem.UpdateStatus();
			}
			uIMatch_Button_DifficultyItem.status.selectedIndex = 0;
		}
		if (isExtreme)
		{
			base.ui.txt__Advise.visible = false;
		}
		if (!isExtreme && _selectedDifficultyItem != null)
		{
			_selectedDifficultyItem.status.selectedIndex = 1;
		}
	}

	private void RefreshWarning()
	{
		List<GObject> children = base.ui.list_Relation._children;
		int num = base.ui.list_Relation.numItems - 1;
		while (num >= 0 && children.Count > num && children[num] is UIMatch_Button_DifficultyItem uIMatch_Button_DifficultyItem)
		{
			if (uIMatch_Button_DifficultyItem.CurrentSelectedStatus && uIMatch_Button_DifficultyItem.Info != null)
			{
				base.ui.txt__Advise.text = uIMatch_Button_DifficultyItem.Info.WarningID.GetLocal(UIStringType.ChoosingTimeLimit);
				base.ui.txt__Advise.visible = true;
				return;
			}
			num--;
		}
		base.ui.txt__Advise.visible = false;
	}

	private void RefreshCurPVPMode()
	{
		MapModeType mapMode = _matchData.GetCurMapMode();
		if (!BattleConfig.IsPVE(mapMode) && base.ui.list_Relation._children.Find(delegate(GObject x)
		{
			if (x is UIMatch_Button_PVPModeItem uIMatch_Button_PVPModeItem)
			{
				GameModeInfoConfigure info = uIMatch_Button_PVPModeItem.Info;
				if (info != null && info.MapModeType == mapMode)
				{
					return true;
				}
			}
			return false;
		}) is UIMatch_Button_PVPModeItem selectedModeItem)
		{
			if (_selectedModeItem != null)
			{
				_selectedModeItem.status.selectedIndex = 0;
			}
			_selectedModeItem = selectedModeItem;
			_selectedModeItem.status.selectedIndex = 1;
		}
	}

	private void RefreshPVPMode()
	{
		base.ui.txt__Advise.visible = false;
		List<GameModeInfoConfigure> gameModes = SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.GetPVPGameMode();
		base.ui.list_Relation.itemProvider = (int _) => "ui://qxwapsemr26s1m";
		base.ui.list_Relation.itemRenderer = delegate(int index, GObject item)
		{
			UIMatch_Button_PVPModeItem btn_ModeITem = item as UIMatch_Button_PVPModeItem;
			if (btn_ModeITem != null)
			{
				btn_ModeITem.Refresh(gameModes[index]);
				btn_ModeITem.touchable = true;
				btn_ModeITem.grayed = _matchData.MatchStatus != MatchTeamInfo.Types.State.Waiting;
				btn_ModeITem.onClick.Set((EventCallback0)delegate
				{
					if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_matchData.MatchLeaderId) && _matchData.IsVailOperate() && _matchData.GetCurMapMode() != btn_ModeITem.Info.MapModeType)
					{
						int maxTeammateCountByMode = _matchData.GetMaxTeammateCountByMode(btn_ModeITem.Info.MapModeType);
						if (_matchData.TeamPlayers.Count <= maxTeammateCountByMode)
						{
							btn_ModeITem.onClick.Retain();
							if (_selectedModeItem != null)
							{
								_selectedModeItem.status.selectedIndex = 0;
							}
							_selectedModeItem = btn_ModeITem;
							_currentMapMode = btn_ModeITem.Info.MapModeType;
							btn_ModeITem.status.selectedIndex = 1;
							(int, int) defaultMapInfo = _matchData.GetDefaultMapInfo((int)_currentMapMode);
							OnRequestChangeMatchTeam(defaultMapInfo.Item1, defaultMapInfo.Item2, RefreshMap);
							btn_ModeITem.onClick.Release();
						}
					}
				});
			}
		};
		base.ui.list_Relation.numItems = gameModes.Count;
		RefreshCurPVPMode();
		base.ui.txt_SelectRelation.text = 1020009.GetLocal(UIStringType.GUI);
	}

	private void RefreshTeamInfo()
	{
		base.ui.leader.selectedIndex = ((!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_matchData.MatchLeaderId)) ? 1 : 0);
		((UICom_RoomPlayer)base.ui.com_Player).RefreshPlayer(_matchData.TeamPlayers, _matchData.MatchLeaderId, _matchData.GetCurMapMode());
		RefreshReadyText();
	}

	private void OnSignalUpdateReady(long playerId)
	{
		((UICom_RoomPlayer)base.ui.com_Player).RefreshReadyStatus(playerId);
		RefreshReadyText();
	}

	public void RefreshReadyText()
	{
		long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		RoomPlayer roomPlayer = _matchData.TeamPlayers.Find((RoomPlayer x) => x.Id == playerID);
		base.ui.btn_Ready.title = ((roomPlayer != null && roomPlayer.RoomReady) ? 1020018.GetLocal(UIStringType.GUI) : 1020017.GetLocal(UIStringType.GUI));
	}

	public void RefreshMatchText()
	{
		bool flag = _matchData.MatchStatus == MatchTeamInfo.Types.State.Matching;
		base.ui.btn_StartMatch.title = (flag ? 1020016.GetLocal(UIStringType.GUI) : 1020015.GetLocal(UIStringType.GUI));
		base.ui.btn_Ready.grayed = _matchData.MatchStatus == MatchTeamInfo.Types.State.Matching;
		base.ui.btn_Ready.touchable = _matchData.MatchStatus != MatchTeamInfo.Types.State.Matching;
	}

	public override void InitTouchable()
	{
		base.InitTouchable();
		base.ui.btn_Ready.touchable = _matchData.MatchStatus != MatchTeamInfo.Types.State.Matching;
	}

	private void RefreshComponentStatus(bool lockStatus)
	{
		foreach (GObject child in base.ui.list_Relation._children)
		{
			if (child is UIMatch_Button_DifficultyItem uIMatch_Button_DifficultyItem)
			{
				if (lockStatus)
				{
					uIMatch_Button_DifficultyItem.grayed = true;
					child.touchable = false;
				}
				else if (_selectedMapItem != null && _selectedMapItem.IsExtreme)
				{
					uIMatch_Button_DifficultyItem.grayed = true;
					child.touchable = false;
				}
				else
				{
					uIMatch_Button_DifficultyItem.UpdateStatus();
					child.touchable = true;
				}
			}
			else
			{
				child.grayed = lockStatus;
				child.touchable = !lockStatus;
			}
		}
		foreach (GObject child2 in base.ui.list_Map._children)
		{
			child2.touchable = !lockStatus;
			child2.grayed = lockStatus;
		}
	}
}
