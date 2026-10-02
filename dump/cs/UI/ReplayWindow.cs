using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using GameLogic.Replay;
using Tools;

namespace UI;

public class ReplayWindow : BaseWindow
{
	private readonly string _roundText = 1000010.GetLocal(UIStringType.GUI);

	private readonly string _turnText = 1000011.GetLocal(UIStringType.GUI);

	private readonly string _monsterText = 1000012.GetLocal(UIStringType.GUI);

	private ReplayNodeListController _nodeController;

	private Dictionary<int, GImage> _eventIcons;

	private UIReplay_Button_TurnItem _currentTurnItem;

	public ReplayWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIReplayWindow.CreateInstance();
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
		if (base.contentPane is UIReplayWindow uIReplayWindow)
		{
			_eventIcons = new Dictionary<int, GImage>
			{
				[2] = uIReplayWindow.image_Lv,
				[4] = uIReplayWindow.image_Monster,
				[1] = uIReplayWindow.image_Gold
			};
			ReplaySession session = SimpleSingletonProvider<GameLogicManager>.inst.replay.Session;
			session.Signal.turnNodeReached.AddListener(OnTurnNodeReached);
			session.Signal.stateChanged.AddListener(OnReplayStateChanged);
			SimpleSingletonProvider<UIManager>.inst.signal.showPanel.AddListener(OnShowPanel);
			SimpleSingletonProvider<GameLogicManager>.inst.action.signal.notifyAllPlayer.AddListener(NotifyCurrentTurn);
			InitNodeList(uIReplayWindow);
			uIReplayWindow.btn_OpenTurn.onClick.Add(OpenTurn);
			uIReplayWindow.btn_Control.onClick.Add(OnClickControl);
			uIReplayWindow.btn_NextTurn.onClick.Add(OnClickNextTurn);
			uIReplayWindow.btn_PreTurn.onClick.Add(OnClickPreTurn);
			uIReplayWindow.btn_NextRound.onClick.Add(OnClickNextRound);
			uIReplayWindow.btn_PreRound.onClick.Add(OnClickPreRound);
			uIReplayWindow.btn_Speed.onClick.Add(OnChangeReplaySpeed);
			uIReplayWindow.showTurn.onChanged.Add(OnShowTurnChange);
			RefreshControlButtons();
			RefreshSpeedButton();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIReplayWindow uIReplayWindow)
		{
			ReplaySession session = SimpleSingletonProvider<GameLogicManager>.inst.replay.Session;
			session.Signal.turnNodeReached.RemoveListener(OnTurnNodeReached);
			session.Signal.stateChanged.RemoveListener(OnReplayStateChanged);
			SimpleSingletonProvider<UIManager>.inst.signal.showPanel.RemoveListener(OnShowPanel);
			SimpleSingletonProvider<GameLogicManager>.inst.action.signal.notifyAllPlayer.RemoveListener(NotifyCurrentTurn);
			uIReplayWindow.btn_OpenTurn.onClick.Remove(OpenTurn);
			uIReplayWindow.btn_Control.onClick.Remove(OnClickControl);
			uIReplayWindow.btn_NextTurn.onClick.Remove(OnClickNextTurn);
			uIReplayWindow.btn_PreTurn.onClick.Remove(OnClickPreTurn);
			uIReplayWindow.btn_NextRound.onClick.Remove(OnClickNextRound);
			uIReplayWindow.btn_PreRound.onClick.Remove(OnClickPreRound);
			uIReplayWindow.btn_Speed.onClick.Remove(OnChangeReplaySpeed);
			uIReplayWindow.showTurn.onChanged.Remove(OnShowTurnChange);
		}
	}

	private void OnShowPanel(UIPanelType panelType)
	{
		if (base.isShowing && panelType == UIPanelType.BattleSettlement)
		{
			CloseReplay();
		}
	}

	public void CloseReplay()
	{
		Hide();
	}

	private async void OnClickControl(EventContext _)
	{
		ReplayLogic replay = SimpleSingletonProvider<GameLogicManager>.inst.replay;
		BlockButton(block: true);
		if (replay.Session.IsPlaying)
		{
			replay.PauseReplay();
		}
		else if (replay.Session.IsPaused)
		{
			replay.ResumeReplay();
		}
		BlockButton(block: false);
		await UniTask.Yield();
	}

	private async void OnClickNextTurn(EventContext _)
	{
		await JumpAsync(() => SimpleSingletonProvider<GameLogicManager>.inst.replay.JumpToNextTurnAsync());
	}

	private async void OnClickPreTurn(EventContext _)
	{
		await JumpAsync(() => SimpleSingletonProvider<GameLogicManager>.inst.replay.JumpToPreTurnAsync());
	}

	private async void OnClickNextRound(EventContext _)
	{
		await JumpAsync(() => SimpleSingletonProvider<GameLogicManager>.inst.replay.JumpToNextRoundAsync());
	}

	private async void OnClickPreRound(EventContext _)
	{
		await JumpAsync(() => SimpleSingletonProvider<GameLogicManager>.inst.replay.JumpToPreRoundAsync());
	}

	private async UniTask JumpAsync(Func<UniTask<bool>> jump)
	{
		BlockButton(block: true);
		try
		{
			await jump();
		}
		finally
		{
			BlockButton(block: false);
			RefreshControlButtons();
		}
	}

	private void RefreshControlButtons()
	{
		if (base.contentPane is UIReplayWindow uIReplayWindow)
		{
			ReplayLogic replay = SimpleSingletonProvider<GameLogicManager>.inst.replay;
			uIReplayWindow.btn_NextTurn.touchable = replay.CanNextTurn();
			uIReplayWindow.btn_NextTurn.status.selectedIndex = ((!uIReplayWindow.btn_NextTurn.touchable) ? 1 : 0);
			uIReplayWindow.btn_PreTurn.touchable = replay.CanPreTurn();
			uIReplayWindow.btn_PreTurn.status.selectedIndex = ((!uIReplayWindow.btn_PreTurn.touchable) ? 1 : 0);
			uIReplayWindow.btn_NextRound.touchable = replay.CanNextRound();
			uIReplayWindow.btn_NextRound.status.selectedIndex = ((!uIReplayWindow.btn_NextRound.touchable) ? 1 : 0);
			uIReplayWindow.btn_PreRound.touchable = replay.CanPreRound();
			uIReplayWindow.btn_PreRound.status.selectedIndex = ((!uIReplayWindow.btn_PreRound.touchable) ? 1 : 0);
			bool flag = replay.Session.IsPlaying || replay.Session.IsPaused;
			uIReplayWindow.btn_Control.enabled = flag;
			uIReplayWindow.btn_Control.status.selectedIndex = (replay.Session.IsPlaying ? 1 : 0);
		}
	}

	private void OnReplayStateChanged(ReplayState oldState, ReplayState newState)
	{
		RefreshControlButtons();
	}

	private void OnChangeReplaySpeed()
	{
		ChangePlaySpeed(1);
	}

	private void ChangePlaySpeed(int step)
	{
		ReplayLogic replay = SimpleSingletonProvider<GameLogicManager>.inst.replay;
		float playSpeedMultiplier = replay.Session.PlaySpeedMultiplier;
		float[] pLAY_SPEED_SEQUENCE = ReplayConfig.PLAY_SPEED_SEQUENCE;
		int num = Array.IndexOf(pLAY_SPEED_SEQUENCE, playSpeedMultiplier);
		if (num < 0)
		{
			num = 0;
		}
		num = (num + step + pLAY_SPEED_SEQUENCE.Length) % pLAY_SPEED_SEQUENCE.Length;
		float num2 = pLAY_SPEED_SEQUENCE[num];
		replay.SetPlaySpeed(num2);
		RefreshSpeedButton(num2);
	}

	private void RefreshSpeedButton(float multiplier = -1f)
	{
		if (base.contentPane is UIReplayWindow uIReplayWindow)
		{
			if (multiplier < 0f)
			{
				multiplier = SimpleSingletonProvider<GameLogicManager>.inst.replay.Session.PlaySpeedMultiplier;
			}
			uIReplayWindow.btn_Speed.title = $"{multiplier}×";
		}
	}

	private void InitNodeList(UIReplayWindow win)
	{
		if (_nodeController == null)
		{
			_nodeController = new ReplayNodeListController();
		}
		ReplaySession session = SimpleSingletonProvider<GameLogicManager>.inst.replay.Session;
		_nodeController.Build(session);
		GList list_Nodes = win.list_Nodes;
		list_Nodes.SetVirtual();
		list_Nodes.itemProvider = (int index) => (_nodeController.Items[index].Type != ReplayNodeItemType.Round) ? "ui://dw3tmgbem0hx8" : "ui://dw3tmgbem0hx7";
		list_Nodes.itemRenderer = RenderNodeItem;
		list_Nodes.onClickItem.Set(OnClickNodeItem);
		RefreshList();
	}

	private void RenderNodeItem(int index, GObject obj)
	{
		if (index < 0 || index >= _nodeController.Items.Count)
		{
			return;
		}
		ReplayNodeListItemVM replayNodeListItemVM = _nodeController.Items[index];
		if (replayNodeListItemVM is ReplayRoundItemVM replayRoundItemVM)
		{
			if (obj is UIReplay_Button_RoundItem uIReplay_Button_RoundItem)
			{
				uIReplay_Button_RoundItem.IsExpanded.selectedIndex = (replayRoundItemVM.IsExpanded ? 1 : 0);
				uIReplay_Button_RoundItem.data = replayRoundItemVM;
				uIReplay_Button_RoundItem.txt_Round.text = replayRoundItemVM.Title;
			}
		}
		else
		{
			if (!(replayNodeListItemVM is ReplayTurnItemVM replayTurnItemVM) || !(obj is UIReplay_Button_TurnItem uIReplay_Button_TurnItem))
			{
				return;
			}
			uIReplay_Button_TurnItem.data = replayTurnItemVM;
			if (replayTurnItemVM.roleType == CharacterType.Hero)
			{
				uIReplay_Button_TurnItem.type.selectedIndex = 0;
				uIReplay_Button_TurnItem.loader_Role.url = replayTurnItemVM.URL;
				uIReplay_Button_TurnItem.txt_Desc.text = _turnText;
				List<GImage> urls = CollectEventIconUrls(replayTurnItemVM.Turn.Behaviors);
				uIReplay_Button_TurnItem.list_Event.itemRenderer = delegate(int idx, GObject child)
				{
					if (idx >= 0 && idx < urls.Count && child is GImage gImage)
					{
						gImage.texture = urls[idx].texture;
					}
				};
				uIReplay_Button_TurnItem.list_Event.numItems = urls.Count;
			}
			else
			{
				uIReplay_Button_TurnItem.txt_MonsterDesc.text = _monsterText;
				uIReplay_Button_TurnItem.type.selectedIndex = 1;
			}
			if (replayTurnItemVM.IsCurrent)
			{
				SetTurnItemStatus(uIReplay_Button_TurnItem);
			}
			else
			{
				uIReplay_Button_TurnItem.status.selectedIndex = 0;
			}
		}
	}

	private List<GImage> CollectEventIconUrls(TurnBehavior behaviors)
	{
		List<GImage> list = new List<GImage>();
		if (behaviors == TurnBehavior.NONE || _eventIcons == null)
		{
			return list;
		}
		foreach (KeyValuePair<int, GImage> eventIcon in _eventIcons)
		{
			if (((uint)eventIcon.Key & (uint)behaviors) != 0)
			{
				list.Add(eventIcon.Value);
			}
		}
		return list;
	}

	private async void OnClickNodeItem(EventContext ctx)
	{
		if (ctx.data is UIReplay_Button_RoundItem uIReplay_Button_RoundItem)
		{
			if (uIReplay_Button_RoundItem.data is ReplayRoundItemVM roundVM)
			{
				_nodeController.ToggleRound(roundVM);
				RefreshList();
				TryHighlightCurrentTurn();
			}
			return;
		}
		BlockButton(block: true);
		try
		{
			if (ctx.data is UIReplay_Button_TurnItem uIReplay_Button_TurnItem)
			{
				SetTurnItemStatus(uIReplay_Button_TurnItem);
				if (uIReplay_Button_TurnItem.data is ReplayTurnItemVM { Turn: not null } replayTurnItemVM)
				{
					await SimpleSingletonProvider<GameLogicManager>.inst.replay.JumpToTurnAsync(replayTurnItemVM.Turn);
				}
			}
		}
		finally
		{
			BlockButton(block: false);
			RefreshControlButtons();
		}
	}

	private void SetCurrentTurnClose()
	{
		if (_currentTurnItem != null)
		{
			_currentTurnItem.status.selectedIndex = 0;
		}
	}

	private void SetTurnItemStatus(UIReplay_Button_TurnItem turnItem)
	{
		SetCurrentTurnClose();
		_currentTurnItem = turnItem;
		if (turnItem != null)
		{
			turnItem.status.selectedIndex = 1;
		}
	}

	private void RefreshList()
	{
		if (base.contentPane is UIReplayWindow uIReplayWindow)
		{
			uIReplayWindow.list_Nodes.numItems = _nodeController.Items.Count;
		}
	}

	private void OpenTurn(EventContext context)
	{
		if (base.contentPane is UIReplayWindow uIReplayWindow)
		{
			uIReplayWindow.btn_OpenTurn.onClick.Retain();
			int selectedIndex = uIReplayWindow.showTurn.selectedIndex;
			uIReplayWindow.showTurn.selectedIndex = ((selectedIndex == 0) ? 1 : 0);
			uIReplayWindow.btn_OpenTurn.onClick.Release();
		}
	}

	private void BlockButton(bool block)
	{
		if (base.contentPane is UIReplayWindow uIReplayWindow)
		{
			if (block)
			{
				uIReplayWindow.list_Nodes.onClickItem.Retain();
				uIReplayWindow.btn_Control.onClick.Retain();
				uIReplayWindow.btn_NextRound.onClick.Retain();
				uIReplayWindow.btn_NextTurn.onClick.Retain();
				uIReplayWindow.btn_PreRound.onClick.Retain();
				uIReplayWindow.btn_PreTurn.onClick.Retain();
			}
			else
			{
				uIReplayWindow.list_Nodes.onClickItem.Release();
				uIReplayWindow.btn_Control.onClick.Release();
				uIReplayWindow.btn_NextRound.onClick.Release();
				uIReplayWindow.btn_NextTurn.onClick.Release();
				uIReplayWindow.btn_PreRound.onClick.Release();
				uIReplayWindow.btn_PreTurn.onClick.Release();
			}
		}
	}

	private void NotifyCurrentTurn(long currenPlayerId)
	{
		if (!(base.contentPane is UIReplayWindow uIReplayWindow))
		{
			return;
		}
		int? num = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo?.Round;
		if (num.HasValue)
		{
			uIReplayWindow.btn_OpenTurn.txt_Round.text = string.Format(_roundText, num.Value);
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(currenPlayerId);
			if (playerDataById != null)
			{
				uIReplayWindow.btn_OpenTurn.loader_Role.url = playerDataById.player.characterConfig.CharacterMap;
				uIReplayWindow.btn_OpenTurn.txt_Desc.text = _turnText;
			}
		}
	}

	private void OnShowTurnChange()
	{
		if (base.contentPane is UIReplayWindow uIReplayWindow && uIReplayWindow.showTurn.selectedIndex != 0)
		{
			if (_nodeController.EnsureCurrentRoundExpanded())
			{
				RefreshList();
			}
			TryHighlightCurrentTurn(scroll: true);
		}
	}

	private void TryHighlightCurrentTurn(bool scroll = false)
	{
		if (!(base.contentPane is UIReplayWindow uIReplayWindow))
		{
			return;
		}
		ReplayTurnNode currentTurnNode = _nodeController.CurrentTurnNode;
		if (currentTurnNode == null)
		{
			return;
		}
		int num = _nodeController.SetCurrentTurn(currentTurnNode);
		if (num < 0)
		{
			SetCurrentTurnClose();
			return;
		}
		if (scroll)
		{
			uIReplayWindow.list_Nodes.ScrollToView(num);
		}
		int num2 = uIReplayWindow.list_Nodes.ItemIndexToChildIndex(num);
		if (num2 >= 0 && num2 < uIReplayWindow.list_Nodes._children.Count && uIReplayWindow.list_Nodes.GetChildAt(num2) is UIReplay_Button_TurnItem turnItemStatus)
		{
			SetTurnItemStatus(turnItemStatus);
		}
	}

	private void OnTurnNodeReached(ReplayTurnNode node)
	{
		if (base.contentPane is UIReplayWindow uIReplayWindow)
		{
			_nodeController.SetCurrentTurn(node);
			if (uIReplayWindow.showTurn.selectedIndex != 0)
			{
				TryHighlightCurrentTurn();
			}
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
	}

	public async UniTask ShowOperate()
	{
		if (!base.isShowing)
		{
			await TryShowAsync();
		}
	}
}
