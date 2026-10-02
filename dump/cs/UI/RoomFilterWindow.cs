using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class RoomFilterWindow : BaseWindow
{
	private List<int> _DifficultyList;

	public RoomFilterWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIRoomFilterWindow.CreateInstance();
		base.OnInit();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIRoomFilterWindow uIRoomFilterWindow)
		{
			uIRoomFilterWindow.btn_Sure.onClick.Add(SureDifficulty);
			uIRoomFilterWindow.list_Difficulty.scrollPane.onScroll.Add(RefreshArrow);
			uIRoomFilterWindow.btn_prePage.onClick.Add(ScrollLeft);
			uIRoomFilterWindow.btn_nextPage.onClick.Add(ScrollRight);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIRoomFilterWindow uIRoomFilterWindow)
		{
			uIRoomFilterWindow.btn_Sure.onClick.Remove(SureDifficulty);
			uIRoomFilterWindow.list_Difficulty.scrollPane.onScroll.Remove(RefreshArrow);
			uIRoomFilterWindow.btn_prePage.onClick.Remove(ScrollRight);
			uIRoomFilterWindow.btn_nextPage.onClick.Remove(ScrollLeft);
		}
	}

	private void ScrollLeft()
	{
		if (base.contentPane is UIRoomFilterWindow uIRoomFilterWindow)
		{
			uIRoomFilterWindow.btn_nextPage.onClick.Retain();
			uIRoomFilterWindow.list_Difficulty.scrollPane.ScrollLeft(20f, ani: true);
			uIRoomFilterWindow.btn_nextPage.onClick.Release();
		}
	}

	private void ScrollRight()
	{
		if (base.contentPane is UIRoomFilterWindow uIRoomFilterWindow)
		{
			uIRoomFilterWindow.btn_prePage.onClick.Retain();
			uIRoomFilterWindow.list_Difficulty.scrollPane.ScrollRight(20f, ani: true);
			uIRoomFilterWindow.btn_prePage.onClick.Release();
		}
	}

	private async UniTask TryShow()
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

	public async UniTask ShowDifficulty()
	{
		_DifficultyList = SimpleSingletonProvider<GameLogicManager>.inst.roomList.GetValidDifficultyConfig();
		await TryShow();
		if (!(base.contentPane is UIRoomFilterWindow uIRoomFilterWindow))
		{
			return;
		}
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		int achieveDifficulty = playerInfo.UnLockDifficulty;
		uIRoomFilterWindow.list_Difficulty.itemRenderer = delegate(int index, GObject item)
		{
			UIRoomFilter_Button_Difficulty btn_Difficulty = item as UIRoomFilter_Button_Difficulty;
			if (btn_Difficulty != null)
			{
				btn_Difficulty.visible = false;
				btn_Difficulty.CutIn.Play(1, 0.04f * (float)index, delegate
				{
					btn_Difficulty.visible = true;
				}, null);
				btn_Difficulty.Refresh(index, _DifficultyList[index]);
				btn_Difficulty.onClick.Set((EventCallback0)delegate
				{
					if (btn_Difficulty.grayed)
					{
						SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(11025);
					}
					else
					{
						btn_Difficulty.onClick.Retain();
						btn_Difficulty.SwitchStatus();
						RefreshWarning();
						btn_Difficulty.onClick.Release();
					}
				});
				btn_Difficulty.grayed = false;
				if (playerInfo != null && playerInfo.Level < StaticGlobalData.ROOM_PVELOCK_LEVEL)
				{
					btn_Difficulty.grayed = _DifficultyList[index] >= StaticGlobalData.ROOM_PVELOCK_DIFFICULTY && _DifficultyList[index] > achieveDifficulty;
					if (btn_Difficulty.grayed && btn_Difficulty.status.selectedIndex == 1)
					{
						btn_Difficulty.SwitchStatus();
					}
				}
			}
		};
		uIRoomFilterWindow.list_Difficulty.numItems = _DifficultyList.Count;
		RefreshWarning();
		RefreshArrow();
	}

	private void RefreshWarning()
	{
		if (!(base.contentPane is UIRoomFilterWindow uIRoomFilterWindow))
		{
			return;
		}
		List<GObject> children = uIRoomFilterWindow.list_Difficulty._children;
		int num = uIRoomFilterWindow.list_Difficulty.numItems - 1;
		while (num >= 0 && children.Count > num && children[num] is UIRoomFilter_Button_Difficulty uIRoomFilter_Button_Difficulty)
		{
			if (uIRoomFilter_Button_Difficulty.CurrentSelectedStatus && uIRoomFilter_Button_Difficulty.ChooseConfig != null)
			{
				uIRoomFilterWindow.txt__Advise.text = uIRoomFilter_Button_Difficulty.ChooseConfig.WarningID.GetLocal(UIStringType.ChoosingTimeLimit);
				uIRoomFilterWindow.txt__Advise.visible = true;
				return;
			}
			num--;
		}
		uIRoomFilterWindow.txt__Advise.visible = false;
	}

	private void RefreshArrow()
	{
		if (base.contentPane is UIRoomFilterWindow uIRoomFilterWindow)
		{
			uIRoomFilterWindow.btn_prePage.visible = uIRoomFilterWindow.list_Difficulty.numItems > 4 && uIRoomFilterWindow.list_Difficulty.scrollPane.percX > 0.05f;
			uIRoomFilterWindow.btn_nextPage.visible = uIRoomFilterWindow.list_Difficulty.numItems > 4 && uIRoomFilterWindow.list_Difficulty.scrollPane.percX < 0.95f;
		}
	}

	private void SureDifficulty()
	{
		if (base.contentPane is UIRoomFilterWindow uIRoomFilterWindow)
		{
			uIRoomFilterWindow.btn_Sure.onClick.Retain();
			Hide();
			uIRoomFilterWindow.btn_Sure.onClick.Release();
		}
	}
}
