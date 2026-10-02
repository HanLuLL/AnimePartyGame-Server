using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityReceivePanel : BasePanel<UIActivityReceivePanel>
{
	private enum BtnStatus
	{
		GoMatch,
		CanRecv,
		Recved
	}

	private int _activityId;

	private ActivityInfoConfigure _activityCfg;

	private TaskActivityData curTActivityreData;

	private BtnStatus _btnStatus;

	private int _bgLoadVersion;

	public ActivityReceivePanel(UIPanelConfigure cfg)
		: base(cfg)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityReceivePanel.CreateInstance();
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
		else if (!StaticConfigure.Activity.InfoDict.TryGetValue(_activityId, out _activityCfg))
		{
			Debug.LogError($"[ActivityReceivePanel] 无法获取活动配置 {_activityId}");
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		RefreshActivity(_activityId);
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.switchTokenList.Dispatch(_activityCfg.CurrencyBar);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.ReceiveBackGround.MallScreen();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.Go_btn?.onClick.Add(OnClickBtn);
		base.ui.pre_btn?.onClick.Add(OnClickPreBtn);
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.AddListener(OnActivityStatusChanged);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.Go_btn?.onClick.Remove(OnClickBtn);
		base.ui.pre_btn?.onClick.Remove(OnClickPreBtn);
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.RemoveListener(OnActivityStatusChanged);
	}

	public void RefreshActivity(int activityCfgId)
	{
		curTActivityreData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityCfgId);
		if (curTActivityreData == null)
		{
			Debug.LogError($"活动数据为空，请检查活动数据；活动ID: {activityCfgId}");
			return;
		}
		UpdateBtnStatusFromData();
		ApplyBtnStatus();
		if (_activityCfg != null)
		{
			base.ui.Rebate_Time.text = TimeHelper.RefreshTimeText(1033, 1034, MonoSingletonProvider<NetManager>.inst.ServerTime, _activityCfg.EndTime.ToDateTime());
		}
		base.ui.tile_txt.text = 1103.GetLocal(UIStringType.Message);
		base.ui.Condition_txt.text = 1104.GetLocal(UIStringType.Message);
		RefreshBgAndHeroFromConfig();
	}

	private void UpdateBtnStatusFromData()
	{
		if (curTActivityreData == null)
		{
			_btnStatus = BtnStatus.GoMatch;
			return;
		}
		bool flag = false;
		bool flag2 = true;
		foreach (KeyValuePair<int, BaseTaskData> item in curTActivityreData.taskDataDict)
		{
			BaseTaskData value = item.Value;
			if (value == null || !value.ValidityTime())
			{
				continue;
			}
			if (!value._FinishStatus)
			{
				flag2 = false;
			}
			if (value._FinishStatus)
			{
				continue;
			}
			if (value is MissionData missionData)
			{
				bool num = missionData.AchieveProgress >= missionData.TaskTarget;
				bool flag3 = missionData.Status == 2;
				if (num || flag3)
				{
					flag = true;
					break;
				}
			}
			else if (!value.TaskRunning)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			_btnStatus = BtnStatus.CanRecv;
		}
		else if (flag2)
		{
			_btnStatus = BtnStatus.Recved;
		}
		else
		{
			_btnStatus = BtnStatus.GoMatch;
		}
	}

	private void ApplyBtnStatus()
	{
		if (base.ui.Go_btn != null)
		{
			base.ui.Go_btn.StatusRe.selectedIndex = (int)_btnStatus;
			base.ui.Go_btn.enabled = true;
		}
	}

	private void RefreshBgAndHeroFromConfig()
	{
		if (base.ui == null || _activityCfg == null)
		{
			return;
		}
		if (base.ui.ReceiveBackGround != null)
		{
			RepeatedField<string> bgList = _activityCfg.BgList;
			if (bgList != null && bgList.Count > 0 && !string.IsNullOrEmpty(bgList[0]))
			{
				int myVer = ++_bgLoadVersion;
				string url = bgList[0];
				SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(url, delegate(NTexture texture)
				{
					if (base.ui != null && myVer == _bgLoadVersion)
					{
						base.ui.ReceiveBackGround.Background(texture);
					}
				}, null).Forget();
			}
		}
		if (base.ui.Load_Hero != null)
		{
			string text = (GameSettings.angelMode ? _activityCfg.ActivityImageSFW : _activityCfg.ActivityImage);
			if (!string.IsNullOrEmpty(text))
			{
				base.ui.Load_Hero.url = text;
			}
		}
	}

	private async void OnClickPreBtn()
	{
		base.ui.pre_btn.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(210111);
		base.ui.pre_btn.onClick.Release();
	}

	private async void OnClickBtn()
	{
		base.ui.Go_btn.onClick.Retain();
		switch (_btnStatus)
		{
		case BtnStatus.GoMatch:
			await GoMatch();
			break;
		case BtnStatus.CanRecv:
			await RequestReward();
			break;
		}
		base.ui.Go_btn.onClick.Release();
	}

	private async UniTask GoMatch()
	{
		if (curTActivityreData == null)
		{
			return;
		}
		BaseTaskData baseTaskData = null;
		foreach (KeyValuePair<int, BaseTaskData> item in curTActivityreData.taskDataDict)
		{
			BaseTaskData value = item.Value;
			if (value != null && value.ValidityTime() && !value._FinishStatus)
			{
				baseTaskData = value;
				break;
			}
		}
		if (baseTaskData != null)
		{
			int way = baseTaskData.GetWay();
			if (way != 0)
			{
				await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(way);
			}
			else
			{
				Debug.LogWarning($"[ActivityReceivePanel] Task {baseTaskData._Id} has no wayId configured.");
			}
		}
		else
		{
			Debug.LogWarning("[ActivityReceivePanel] No valid task found to go target panel.");
		}
	}

	private async UniTask RequestReward()
	{
		if (curTActivityreData == null)
		{
			return;
		}
		foreach (KeyValuePair<int, BaseTaskData> item in curTActivityreData.taskDataDict)
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
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(curTActivityreData.activityConfig.Id, value).OnFinishedOnly.AddOnce(delegate
				{
					SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.Dispatch(curTActivityreData.activityConfig.Id);
					tcs.TrySetResult(result: true);
				});
				await tcs.Task;
			}
		}
		RefreshActivity(_activityId);
	}

	private void OnActivityStatusChanged(int actId)
	{
		if (actId == _activityId)
		{
			RefreshActivity(_activityId);
		}
	}
}
