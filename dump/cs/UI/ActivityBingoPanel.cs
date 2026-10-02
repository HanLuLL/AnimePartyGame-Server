using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityBingoPanel : BasePanel<UIActivityBingoPanel>
{
	private BingoActivityData activityData;

	private Model_UIActivityBingo_Grid[] cardGrids;

	private Model_UIActivityBingo_Grid[] rewards;

	private int currentPage;

	private List<MissionData> missionList;

	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private const int btnDoneTitleId = 6100102;

	public ActivityBingoPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityBingoPanel.CreateInstance();
		base.Create();
		cardGrids = new Model_UIActivityBingo_Grid[16]
		{
			new Model_UIActivityBingo_Grid(base.ui.list_grid.GetChildAt(0)),
			new Model_UIActivityBingo_Grid(base.ui.list_grid.GetChildAt(1)),
			new Model_UIActivityBingo_Grid(base.ui.list_grid.GetChildAt(2)),
			new Model_UIActivityBingo_Grid(base.ui.exGrid_0),
			new Model_UIActivityBingo_Grid(base.ui.list_grid.GetChildAt(3)),
			new Model_UIActivityBingo_Grid(base.ui.list_grid.GetChildAt(4)),
			new Model_UIActivityBingo_Grid(base.ui.list_grid.GetChildAt(5)),
			new Model_UIActivityBingo_Grid(base.ui.exGrid_1),
			new Model_UIActivityBingo_Grid(base.ui.list_grid.GetChildAt(6)),
			new Model_UIActivityBingo_Grid(base.ui.list_grid.GetChildAt(7)),
			new Model_UIActivityBingo_Grid(base.ui.list_grid.GetChildAt(8)),
			new Model_UIActivityBingo_Grid(base.ui.exGrid_2),
			new Model_UIActivityBingo_Grid(base.ui.exGrid_6),
			new Model_UIActivityBingo_Grid(base.ui.exGrid_5),
			new Model_UIActivityBingo_Grid(base.ui.exGrid_4),
			new Model_UIActivityBingo_Grid(base.ui.exGrid_3)
		};
		rewards = new Model_UIActivityBingo_Grid[7]
		{
			new Model_UIActivityBingo_Grid(base.ui.com_reward.grid_0),
			new Model_UIActivityBingo_Grid(base.ui.com_reward.grid_1),
			new Model_UIActivityBingo_Grid(base.ui.com_reward.grid_2),
			new Model_UIActivityBingo_Grid(base.ui.com_reward.grid_3),
			new Model_UIActivityBingo_Grid(base.ui.com_reward.grid_4),
			new Model_UIActivityBingo_Grid(base.ui.com_reward.grid_5),
			new Model_UIActivityBingo_Grid(base.ui.com_reward.grid_6)
		};
	}

	protected override void InitData(params object[] objs)
	{
		activityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(610010) as BingoActivityData;
		int pageCompleteCount = activityData.GetPageCompleteCount();
		currentPage = Mathf.Min(pageCompleteCount, activityData.RoundCount - 1);
		for (int i = 0; i < cardGrids.Length; i++)
		{
			cardGrids[i].ChangeCardDesc(activityData, i, currentPage, isProgressReward: false);
		}
		for (int j = 0; j < rewards.Length; j++)
		{
			rewards[j].ChangeCardDesc(activityData, j, 0, isProgressReward: true);
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.com_reward.visible = false;
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.list_mission.SetVirtual();
		base.ui.list_mission.itemRenderer = RendererMission;
		base.ui.mohu.FullScreen();
		base.ui.bg.MallScreen();
	}

	private void RefreshCostItemCount()
	{
		int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(activityData.CostItemId);
		base.ui.txt_coin.text = $"x{itemCount}";
	}

	private void RefreshRewardButton()
	{
		int a = activityData.GetPageCompleteCount() + 1;
		int roundCount = activityData.RoundCount;
		base.ui.btn_done.title = string.Format(6100102.GetLocal(UIStringType.Activity), Mathf.Min(a, roundCount), roundCount);
		base.ui.btn_done.loader_RedPoint.visible = activityData.GetProgressRewardStatus();
	}

	private void RefreshActivityTitle()
	{
		DateTime dateTime = activityData.activityConfig.BeginTime.ToDateTime();
		DateTime dateTime2 = activityData.activityConfig.EndTime.ToDateTime();
		base.ui.txt_date.text = $"{dateTime:MM.dd}~{dateTime2:MM.dd}";
		base.ui.txt_title.text = activityData.activityConfig.NameID.GetLocal(UIStringType.Activity);
	}

	public override void Refresh()
	{
		base.Refresh();
		Model_UIActivityBingo_Grid[] array = cardGrids;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Refresh();
		}
		RefreshMissionList();
		base.ui.list_mission.scrollPane.percY = 0f;
		RefreshCostItemCount();
		RefreshRewardButton();
		RefreshActivityTitle();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		Model_UIActivityBingo_Grid[] array = cardGrids;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].AddEvent();
		}
		array = rewards;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].AddEvent();
		}
		base.ui.btn_done.onClick.Add(OnBtnDoneClick);
		base.ui.com_reward.btn_quit.onClick.Add(OnBtnQuitClick);
		base.ui.com_reward.bg.onClick.Add(OnBtnQuitClick);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		Model_UIActivityBingo_Grid[] array = cardGrids;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].RemoveEvent();
		}
		array = rewards;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].RemoveEvent();
		}
		base.ui.btn_done.onClick.Remove(OnBtnDoneClick);
		base.ui.com_reward.btn_quit.onClick.Remove(OnBtnQuitClick);
		base.ui.com_reward.bg.onClick.Remove(OnBtnQuitClick);
	}

	private void OnItemCountChanged(int id, int count)
	{
		if (id == activityData.CostItemId)
		{
			RefreshCostItemCount();
		}
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.onItemCountChanged.AddListener(OnItemCountChanged);
		SimpleSingletonProvider<GameLogicManager>.inst.task.OnTaskChange.AddListener(RefreshMissionList);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.onItemCountChanged.RemoveListener(OnItemCountChanged);
		SimpleSingletonProvider<GameLogicManager>.inst.task.OnTaskChange.RemoveListener(RefreshMissionList);
	}

	public override void Close()
	{
		HideReward();
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void RefreshCardGrids(int pageIndex)
	{
		if (activityData.IsVaildPage(pageIndex))
		{
			currentPage = pageIndex;
			for (int i = 0; i < cardGrids.Length; i++)
			{
				Model_UIActivityBingo_Grid obj = cardGrids[i];
				obj.ChangeCardDesc(activityData, i, currentPage, isProgressReward: false);
				obj.Refresh();
			}
		}
	}

	private void OnBtnDoneClick()
	{
		ShowReward();
	}

	private void OnBtnQuitClick()
	{
		HideReward();
	}

	private async void ShowReward()
	{
		await blurBgCtrl.CreateBlurTex((GameCameraFlag)2);
		base.ui.com_reward.visible = true;
		base.ui.com_reward.bg.MallScreen();
		blurBgCtrl.OnShown(base.ui.com_reward.bg);
		if (SimpleSingletonProvider<UIManager>.inst.activityHubPanel is ActivityHubPanel activityHubPanel)
		{
			activityHubPanel.UIObject.AddChild(base.ui.com_reward);
		}
		RefreshProgressReward();
	}

	private void RefreshProgressReward()
	{
		int pageCompleteCount = activityData.GetPageCompleteCount();
		for (int i = 0; i < rewards.Length; i++)
		{
			rewards[i].Refresh();
			rewards[i].Com.loader_RedPoint.visible = i < pageCompleteCount && activityData.GetProgressStatus(i);
		}
	}

	public void RefreshProgressInfo()
	{
		RefreshRewardButton();
		RefreshProgressReward();
	}

	private void HideReward()
	{
		if (base.ui.com_reward.visible)
		{
			blurBgCtrl.OnHide();
			base.ui.com_reward.visible = false;
			base.ui.AddChild(base.ui.com_reward);
		}
	}

	private void RefreshMissionList()
	{
		missionList = activityData.GetSortedMissionList();
		base.ui.list_mission.numItems = missionList.Count;
	}

	private void RendererMission(int index, GObject item)
	{
		if (missionList != null && item is UIActivityBingo_Com_Mission com)
		{
			RefreshMissionItem(index, com);
		}
	}

	private void RefreshMissionItem(int index, UIActivityBingo_Com_Mission com)
	{
		MissionData missionData = missionList[index];
		KeyValuePair<int, int> reward = missionData.rewards[0];
		CommonUIManager.RendererLitItem((UICom_LitItem)com.rewardItem, reward.Key, reward.Value);
		bool flag = IsTaskRunning(missionData);
		int selectedIndex = (missionData._FinishStatus ? 2 : ((!flag) ? 1 : 0));
		com.Status.selectedIndex = selectedIndex;
		com.btn_taskStatus.Status.selectedIndex = selectedIndex;
		com.txt_taskDesc.text = missionData.GetTaskDesc();
		com.txt_RefreshType.visible = missionData.GetTaskRefreshType() != TaskRefreshType.None;
		com.txt_RefreshType.text = missionData.GetTaskRefreshTypeLocal();
		com.bar_task.max = missionData.TaskTarget;
		com.bar_task.min = 0.0;
		com.bar_task.value = Mathf.Min(missionData._Progress, missionData.TaskTarget);
		com.rewardItem.onClick.Set((EventCallback0)delegate
		{
			com.rewardItem.onClick.Retain();
			SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(reward.Key, reward.Value, _Usable: true).Forget();
			com.rewardItem.onClick.Release();
		});
		com.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			OnTaskStatusClick(missionData, com);
		});
	}

	private async void OnTaskStatusClick(MissionData missionData, UIActivityBingo_Com_Mission com)
	{
		bool num = IsTaskRunning(missionData);
		bool flag = CanClaimTask(missionData);
		com.btn_taskStatus.onClick.Retain();
		if (num)
		{
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(missionData.config.Way);
			com.btn_taskStatus.onClick.Release();
		}
		else if (flag)
		{
			int activityId = activityData.activityConfig.Id;
			SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(activityId, missionData).OnFinishedOnly.AddOnce(delegate
			{
				SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.Dispatch(activityId);
				com.btn_taskStatus.onClick.Release();
			});
		}
		else
		{
			com.btn_taskStatus.onClick.Release();
		}
	}

	private bool CanClaimTask(BaseTaskData taskData)
	{
		if (taskData._FinishStatus)
		{
			return false;
		}
		if (taskData is MissionData missionData)
		{
			bool num = missionData.AchieveProgress >= missionData.TaskTarget;
			bool flag = missionData.Status == 2;
			return num || flag;
		}
		return !taskData.TaskRunning;
	}

	private bool IsTaskRunning(BaseTaskData taskData)
	{
		if (taskData._FinishStatus)
		{
			return false;
		}
		if (taskData is MissionData missionData)
		{
			if (missionData.AchieveProgress >= missionData.TaskTarget)
			{
				return false;
			}
			return missionData.Status != 2;
		}
		return taskData.TaskRunning;
	}

	public void RefreshOnFlipCardS2C(IEnumerable<int> cards)
	{
		List<BingoActivityGrid> list = new List<BingoActivityGrid>();
		foreach (int card in cards)
		{
			int num = card % activityData.NumPerPage;
			BingoActivityGrid bingoActivityGridData = cardGrids[num].GetBingoActivityGridData();
			list.Add(bingoActivityGridData);
		}
		if (list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				BingoActivityGrid bingoActivityGrid = list[i];
				SimpleSingletonProvider<UIManager>.inst.reward.ShowSingleReward(bingoActivityGrid.Config.ItemID, bingoActivityGrid.Config.ItemNum);
			}
		}
		RefreshRewardButton();
		int pageIndex = Mathf.Min(activityData.GetPageCompleteCount(), activityData.RoundCount - 1);
		RefreshCardGrids(pageIndex);
	}
}
