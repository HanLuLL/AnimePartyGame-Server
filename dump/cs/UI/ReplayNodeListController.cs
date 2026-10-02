using System.Collections.Generic;
using GameLogic;
using GameLogic.Replay;
using Tools;

namespace UI;

public class ReplayNodeListController
{
	private readonly List<ReplayNodeListItemVM> _items = new List<ReplayNodeListItemVM>();

	private readonly Dictionary<ReplayTurnNode, ReplayTurnItemVM> _turnMap = new Dictionary<ReplayTurnNode, ReplayTurnItemVM>();

	private readonly Dictionary<ReplayRoundNode, ReplayRoundItemVM> _roundMap = new Dictionary<ReplayRoundNode, ReplayRoundItemVM>();

	private ReplayTurnItemVM _currentTurn;

	private ReplayTurnNode _currentTurnNode;

	public IReadOnlyList<ReplayNodeListItemVM> Items => _items;

	public ReplayTurnNode CurrentTurnNode => _currentTurnNode;

	public void Build(ReplaySession session)
	{
		_items.Clear();
		_turnMap.Clear();
		_roundMap.Clear();
		if (session?.CurrentPackage?.Rounds == null)
		{
			return;
		}
		ReplayTurnNode replayTurnNode = FindCurrentTurn(session);
		foreach (ReplayRoundNode round in session.CurrentPackage.Rounds)
		{
			bool flag = replayTurnNode != null && round.Turns.Contains(replayTurnNode);
			ReplayRoundItemVM replayRoundItemVM = new ReplayRoundItemVM(round)
			{
				IsExpanded = flag
			};
			_items.Add(replayRoundItemVM);
			_roundMap[round] = replayRoundItemVM;
			if (flag)
			{
				AddTurns(round);
			}
		}
		RefreshTitles();
	}

	private void AddTurns(ReplayRoundNode round)
	{
		foreach (ReplayTurnNode turn in round.Turns)
		{
			ReplayTurnItemVM replayTurnItemVM = new ReplayTurnItemVM(turn);
			_items.Add(replayTurnItemVM);
			_turnMap[turn] = replayTurnItemVM;
		}
	}

	public void ToggleRound(ReplayRoundItemVM roundVM)
	{
		if (roundVM.IsExpanded)
		{
			Collapse(roundVM);
		}
		else
		{
			Expand(roundVM);
		}
		RefreshTitles();
	}

	private void Expand(ReplayRoundItemVM roundVM)
	{
		int num = _items.IndexOf(roundVM);
		if (num < 0)
		{
			return;
		}
		int num2 = num + 1;
		foreach (ReplayTurnNode turn in roundVM.Round.Turns)
		{
			ReplayTurnItemVM replayTurnItemVM = new ReplayTurnItemVM(turn);
			_items.Insert(num2++, replayTurnItemVM);
			_turnMap[turn] = replayTurnItemVM;
		}
		roundVM.IsExpanded = true;
	}

	private void Collapse(ReplayRoundItemVM roundVM)
	{
		for (int num = _items.Count - 1; num >= 0; num--)
		{
			if (_items[num] is ReplayTurnItemVM replayTurnItemVM && roundVM.Round.Turns.Contains(replayTurnItemVM.Turn))
			{
				_turnMap.Remove(replayTurnItemVM.Turn);
				_items.RemoveAt(num);
			}
		}
		roundVM.IsExpanded = false;
	}

	public int SetCurrentTurn(ReplayTurnNode turn)
	{
		if (_currentTurn != null)
		{
			_currentTurn.IsCurrent = false;
		}
		_currentTurnNode = turn;
		if (turn == null)
		{
			_currentTurn = null;
			return -1;
		}
		if (!_turnMap.TryGetValue(turn, out var value))
		{
			return -1;
		}
		value.IsCurrent = true;
		_currentTurn = value;
		RefreshTitles();
		return _items.IndexOf(value);
	}

	public bool EnsureCurrentRoundExpanded()
	{
		if (_currentTurnNode == null)
		{
			return false;
		}
		foreach (ReplayNodeListItemVM item in _items)
		{
			if (item is ReplayRoundItemVM replayRoundItemVM && replayRoundItemVM.Round.Turns.Contains(_currentTurnNode))
			{
				if (replayRoundItemVM.IsExpanded)
				{
					return false;
				}
				Expand(replayRoundItemVM);
				RefreshTitles();
				return true;
			}
		}
		return false;
	}

	private void RefreshTitles()
	{
		foreach (ReplayNodeListItemVM item in _items)
		{
			if (item is ReplayRoundItemVM replayRoundItemVM)
			{
				replayRoundItemVM.Title = string.Format(1000010.GetLocal(UIStringType.GUI), replayRoundItemVM.Round.Round);
			}
			else if (item is ReplayTurnItemVM replayTurnItemVM)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(replayTurnItemVM.Turn.PlayerId);
				if (playerDataById != null)
				{
					replayTurnItemVM.roleType = playerDataById.player.characterType;
					replayTurnItemVM.URL = playerDataById.player.characterConfig.CharacterMap;
				}
			}
		}
	}

	private static ReplayTurnNode FindCurrentTurn(ReplaySession session)
	{
		List<ReplayTurnNode> turnNodes = session.CurrentPackage.TurnNodes;
		if (turnNodes == null || turnNodes.Count == 0)
		{
			return null;
		}
		int num = session.CurrentPlayer?.CurrentFrameIndex ?? session.PlaybackStartFrameIndex;
		ReplayTurnNode result = null;
		foreach (ReplayTurnNode item in turnNodes)
		{
			if (item.FrameIndex <= num)
			{
				result = item;
				continue;
			}
			break;
		}
		return result;
	}
}
