using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;

namespace UI;

public class LoseCardWindow : BaseWindow
{
	private long _ActionSn;

	private List<int> _SelectedCardIds;

	private readonly HashSet<UILoseCard_Button_Large> _SelectedCardButtons = new HashSet<UILoseCard_Button_Large>();

	public LoseCardWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILoseCardWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShow()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetSelfCardList.AddListener(RefreshSelfCard);
	}

	protected override void OnHide()
	{
		base.OnHide();
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetSelfCardList.RemoveListener(RefreshSelfCard);
	}

	private void RefreshSelfCard(long playerId)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId) || SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo == null)
		{
			return;
		}
		int cardInHandLimit = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType.GetGameModeInfoConfigure().CardInHandLimit;
		if (base.contentPane is UILoseCardWindow uILoseCardWindow && uILoseCardWindow.type.selectedIndex == 0)
		{
			List<HandCardData> list = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData()?.cardContainer._HandCards;
			if (list != null && list.Count >= cardInHandLimit)
			{
				_SelectedCardButtons.Clear();
				_SelectedCardIds?.Clear();
				RefreshCardCountText("#ff0000", list.Count, cardInHandLimit);
				uILoseCardWindow.list_Card.numItems = list.Count;
			}
		}
	}

	private void SelectCard(UILoseCard_Button_Large btn_Card, int maxSelectCount)
	{
		if (_SelectedCardButtons.Contains(btn_Card))
		{
			btn_Card.selectedStatus.selectedIndex = 0;
			_SelectedCardButtons.Remove(btn_Card);
		}
		else
		{
			btn_Card.selectedStatus.selectedIndex = 1;
			if (_SelectedCardButtons.Count < maxSelectCount)
			{
				_SelectedCardButtons.Add(btn_Card);
			}
			else
			{
				UILoseCard_Button_Large uILoseCard_Button_Large = _SelectedCardButtons.FirstOrDefault();
				if (uILoseCard_Button_Large != null)
				{
					uILoseCard_Button_Large.selectedStatus.selectedIndex = 0;
					_SelectedCardButtons.Remove(uILoseCard_Button_Large);
					_SelectedCardButtons.Add(btn_Card);
				}
			}
		}
		_SelectedCardIds = _SelectedCardButtons.Select((UILoseCard_Button_Large _btn) => _btn.handCardData.Guid).ToList();
	}

	private void RefreshCardCountText(string colorCode, int leftCardCount, int rightCardCount)
	{
		if (base.contentPane is UILoseCardWindow uILoseCardWindow)
		{
			uILoseCardWindow.txt_CardNum.SetVar("cur", $"[color={colorCode}]{leftCardCount}[/color]").SetVar("max", rightCardCount.ToString()).FlushVars();
		}
	}

	public async UniTask ShowLoseCard(long _actionSn)
	{
		_ActionSn = _actionSn;
		int MaxUsableCardCount = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType.GetGameModeInfoConfigure().CardInHandLimit;
		SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(string.Format(10019.GetLocal(UIStringType.Message), MaxUsableCardCount), 2f);
		await TryShow();
		GComponent gComponent = base.contentPane;
		UILoseCardWindow win = gComponent as UILoseCardWindow;
		if (win == null)
		{
			return;
		}
		win.type.selectedIndex = 0;
		_SelectedCardButtons.Clear();
		_SelectedCardIds?.Clear();
		List<HandCardData> currentCards = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().cardContainer._HandCards;
		RefreshCardCountText("#ff0000", currentCards.Count, MaxUsableCardCount);
		win.btn_Lose.visible = true;
		win.list_Card.itemRenderer = delegate(int index, GObject item)
		{
			UILoseCard_Button_Large btn_Card = item as UILoseCard_Button_Large;
			if (btn_Card != null)
			{
				btn_Card.InitDate(index, currentCards[index]);
				btn_Card.onClick.Set((EventCallback0)delegate
				{
					win.list_Card.touchable = false;
					SelectCard(btn_Card, currentCards.Count - MaxUsableCardCount);
					int num = currentCards.Count - _SelectedCardIds.Count;
					string colorCode = ((num > MaxUsableCardCount) ? "#ff0000" : "#ffffff");
					RefreshCardCountText(colorCode, num, MaxUsableCardCount);
					win.list_Card.touchable = true;
				});
			}
		};
		win.list_Card.numItems = currentCards.Count;
		win.btn_Lose.onClick.Release();
		win.btn_Lose.onClick.Set((EventCallback0)delegate
		{
			win.btn_Lose.onClick.Retain();
			if (currentCards.Count > MaxUsableCardCount && (_SelectedCardIds == null || _SelectedCardIds.Count == 0 || currentCards.Count - _SelectedCardIds.Count != MaxUsableCardCount))
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(string.Format(10019.GetLocal(UIStringType.Message), MaxUsableCardCount));
				win.btn_Lose.onClick.Release();
			}
			else
			{
				if (_SelectedCardIds == null)
				{
					_SelectedCardIds = new List<int>();
				}
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestAbandonCardC2S(_ActionSn, _SelectedCardIds);
			}
		});
		OperationTimer.ActionDownTime(_actionSn, 5075, delegate
		{
			win.btn_Lose.visible = false;
			_SelectedCardButtons.Clear();
			_SelectedCardIds?.Clear();
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10002, 2f);
			int num = currentCards.Count - MaxUsableCardCount;
			for (int i = 0; i < num; i++)
			{
				win.list_Card.GetChildAt(i).onClick.Call();
			}
			win.btn_Lose.onClick.Call();
		});
	}

	public async UniTask ShowBestowCard(long actionSn, int actionId, int minBestowCount, int maxBestowCount, List<long> targetId)
	{
		List<long> selectedPlayer = new List<long> { targetId[0] };
		_ActionSn = actionSn;
		await TryShow();
		GComponent gComponent = base.contentPane;
		UILoseCardWindow win = gComponent as UILoseCardWindow;
		if (win == null)
		{
			return;
		}
		win.type.selectedIndex = 1;
		RefreshCostCardInfo(minBestowCount, maxBestowCount);
		win.btn_Bestow.onClick.Release();
		win.btn_Bestow.onClick.Set((EventCallback0)delegate
		{
			win.btn_Bestow.onClick.Retain();
			if (_SelectedCardIds == null || _SelectedCardIds.Count == 0 || _SelectedCardIds.Count < minBestowCount)
			{
				win.btn_Bestow.onClick.Release();
			}
			else
			{
				Hide();
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestReleaseSkillC2S(_ActionSn, actionId, selectedPlayer, _SelectedCardIds);
			}
		});
	}

	private void RefreshCostCardInfo(int minBestowCount, int maxBestowCount)
	{
		GComponent gComponent = base.contentPane;
		UILoseCardWindow win = gComponent as UILoseCardWindow;
		if (win == null)
		{
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.closeSelectPlayer.Dispatch();
		_SelectedCardButtons.Clear();
		_SelectedCardIds?.Clear();
		List<HandCardData> currentCards = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().cardContainer._HandCards;
		RefreshCardCountText("#ff0000", 0, maxBestowCount);
		win.list_Card.itemRenderer = delegate(int index, GObject item)
		{
			UILoseCard_Button_Large btn_Card = item as UILoseCard_Button_Large;
			if (btn_Card != null)
			{
				btn_Card.InitDate(index, currentCards[index]);
				btn_Card.onClick.Set((EventCallback0)delegate
				{
					win.list_Card.touchable = false;
					SelectCard(btn_Card, maxBestowCount);
					string colorCode = ((minBestowCount > _SelectedCardIds.Count) ? "#ff0000" : "#ffffff");
					RefreshCardCountText(colorCode, _SelectedCardIds.Count, maxBestowCount);
					win.list_Card.touchable = true;
				});
			}
		};
		win.list_Card.numItems = currentCards.Count;
		win.btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CARD;
			Hide();
		});
	}

	public async UniTask ShowMixCard(long sn, int skillId, int cardCount, List<long> targetId = null)
	{
		List<long> selectedPlayer = new List<long>();
		if (targetId != null && targetId.Count > 0)
		{
			selectedPlayer.Add(targetId[0]);
		}
		_ActionSn = sn;
		await TryShow();
		GComponent gComponent = base.contentPane;
		UILoseCardWindow win = gComponent as UILoseCardWindow;
		if (win == null)
		{
			return;
		}
		win.type.selectedIndex = 2;
		RefreshCostCardInfo(cardCount, cardCount);
		win.btn_Mix.onClick.Release();
		win.btn_Mix.onClick.Set((EventCallback0)delegate
		{
			win.btn_Mix.onClick.Retain();
			if (_SelectedCardIds == null || _SelectedCardIds.Count == 0 || _SelectedCardIds.Count < cardCount)
			{
				win.btn_Mix.onClick.Release();
			}
			else
			{
				Hide();
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestReleaseSkillC2S(_ActionSn, skillId, selectedPlayer, _SelectedCardIds);
			}
		});
	}
}
