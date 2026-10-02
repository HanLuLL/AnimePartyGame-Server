using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityFreePanel : BasePanel<UIActivityFreePanel>
{
	private int _activityId;

	private ActivityInfoConfigure _currentActivityConfig;

	private TaskActivityData _CurTActivityreData;

	public ActivityFreePanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityFreePanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (objs != null && objs.Length != 0 && objs[0] is RepeatedField<int> { Count: >0 } repeatedField)
		{
			_activityId = repeatedField[0];
		}
		if (_activityId == 0)
		{
			Debug.LogError("[ActivityReceivePanel] 未传入活动ID，且当前 _activityId 为0，无法初始化活动配置");
		}
		else if (!StaticConfigure.Activity.InfoDict.TryGetValue(_activityId, out _currentActivityConfig))
		{
			Debug.LogError($"[ActivityReceivePanel] 无法获取活动配置 {_activityId}");
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.language.selectedIndex = GameSettings.GetDataForLanguage(1, 2, 0, 0);
		RefreshActivity(_activityId);
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.switchTokenList.Dispatch(_currentActivityConfig.CurrencyBar);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.FreeBackGround.MallScreen();
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.Receive_btn.onClick.Add(OnClickBtn);
		base.ui.Item_Btn.onClick.Add(ShowItem);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.Receive_btn.onClick.Remove(OnClickBtn);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.AddListener(OnActivityStatusChanged);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.RemoveListener(OnActivityStatusChanged);
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void OnClickBtn()
	{
		base.ui.Receive_btn.onClick.Retain();
		await RequestReward();
		base.ui.Receive_btn.onClick.Release();
	}

	private async void ShowItem()
	{
		base.ui.Item_Btn.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(99004, 3, _Usable: true);
		base.ui.Item_Btn.onClick.Release();
	}

	private async UniTask RequestReward()
	{
		if (_CurTActivityreData == null)
		{
			return;
		}
		foreach (KeyValuePair<int, BaseTaskData> item in _CurTActivityreData.taskDataDict)
		{
			BaseTaskData value = item.Value;
			if (value == null || !value.ValidityTime() || value._FinishStatus)
			{
				continue;
			}
			bool flag2;
			if (value is MissionData missionData)
			{
				bool num = missionData.AchieveProgress >= missionData.TaskTarget;
				bool flag = missionData.Status == 2;
				flag2 = num || flag;
			}
			else
			{
				flag2 = !value.TaskRunning;
			}
			if (flag2)
			{
				TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(_CurTActivityreData.activityConfig.Id, value).OnFinishedOnly.AddOnce(delegate
				{
					SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.Dispatch(_CurTActivityreData.activityConfig.Id);
					tcs.TrySetResult(result: true);
				});
				await tcs.Task;
			}
		}
		RefreshActivity(_activityId);
	}

	public void RefreshActivity(int activityCfgId)
	{
		_CurTActivityreData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityCfgId);
		if (_CurTActivityreData == null)
		{
			Debug.LogError($"活动数据为空，请检查活动数据；活动ID: {activityCfgId}");
			return;
		}
		RefreshGetRewardButton();
		if (_currentActivityConfig != null)
		{
			base.ui.RebateFree_Time.text = TimeHelper.RefreshTimeText(1033, 1034, MonoSingletonProvider<NetManager>.inst.ServerTime, _currentActivityConfig.EndTime.ToDateTime());
		}
	}

	private void RefreshGetRewardButton()
	{
		bool activityStatus = _CurTActivityreData.GetActivityStatus();
		base.ui.Receive_btn.grayed = !activityStatus;
		base.ui.Receive_btn.touchable = activityStatus;
		base.ui.Receive_btn.StatusRe.selectedIndex = ((!activityStatus) ? 1 : 0);
		base.ui.StatusRe.selectedIndex = ((!activityStatus) ? 1 : 0);
	}

	private void OnActivityStatusChanged(int actId)
	{
		if (actId == _activityId)
		{
			RefreshActivity(_activityId);
		}
	}
}
