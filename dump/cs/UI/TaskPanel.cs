using System;
using System.Collections.Generic;
using System.Linq;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class TaskPanel : BasePanel<UITaskPanel>
{
	private UITask_Button_Tab btn_AchieveTab;

	private List<int> achieveIds;

	private UITask_Button_Tab btn_AcquisitionTab;

	private AcquisitionData AcquisitionData;

	private UITask_Button_Acquisition tab_AcceptInvite;

	private UITask_Button_Acquisition tab_Invite;

	private List<int> AcquisitionTaskId;

	private readonly WelfareType WelfareType_Acquisition = WelfareType.Acquisition;

	private List<GloryTaskData> gloryTasksData;

	private UITask_Button_Tab btn_NoviceTaskTab;

	private List<int> taskIds;

	private List<SevenTaskData> sevenTasksData;

	private UITask_Button_Tab btn_WeekTaskTab;

	private int liveness;

	private int _InitTabType;

	private int _labelDataId;

	private List<int> weekTaskIds;

	private GloryData gloryData => SimpleSingletonProvider<GameLogicManager>.inst.task.gloryData;

	private int LV => SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level;

	private SevenDayData sevenDayTask => SimpleSingletonProvider<GameLogicManager>.inst.task.sevenDayData;

	private void InitComponents_Achieve()
	{
		base.ui.list_Achieve.SetVirtual();
		base.ui.list_Achieve.itemRenderer = RendererAchieve;
	}

	private void Refresh_Achieve(UITask_Button_Tab btnTab)
	{
		btn_AchieveTab = btnTab;
		btnTab.redStatus.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveSystemStatus() ? 1 : 0);
	}

	private void AddEvent_Achieve()
	{
	}

	private void RemoveEvent_Achieve()
	{
	}

	private void RefreshAchieveData()
	{
		Dictionary<int, AchieveData> achieveDict = SimpleSingletonProvider<GameLogicManager>.inst.task.achieveDataDict;
		List<int> source = SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveIds();
		achieveIds = source.Where(delegate(int id)
		{
			if (!achieveDict.TryGetValue(id, out var value))
			{
				return false;
			}
			AchieveGlobalConfigure achieveGlobalConfigure = value._Config;
			ConditionType conditionType = achieveGlobalConfigure.ConditionType;
			return (conditionType != ConditionType.TotalCharacterWin && conditionType != ConditionType.TotalCharacterDone) || !StaticConfigure.Achieve.ShieldDict.ContainsKey(achieveGlobalConfigure.Ref) || SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(achieveGlobalConfigure.Ref).IsHas;
		}).ToList();
		base.ui.list_Achieve.numItems = achieveIds.Count;
		SetTargetLabel_Achieve();
	}

	private void SetTargetLabel_Achieve()
	{
		if (_labelDataId == 0)
		{
			return;
		}
		for (int i = 0; i < achieveIds.Count; i++)
		{
			if (achieveIds[i] == _labelDataId)
			{
				base.ui.list_Achieve.ScrollToView(i);
				break;
			}
		}
		_labelDataId = 0;
	}

	private void RendererAchieve(int index, GObject item)
	{
		UITask_Com_AchieveLabel label = item as UITask_Com_AchieveLabel;
		if (label == null)
		{
			return;
		}
		AchieveData achieveData = SimpleSingletonProvider<GameLogicManager>.inst.task.achieveDataDict[achieveIds[index]];
		label.load_Icon.url = achieveData._Config.Icon.GetImageLocalization();
		RendererRewardItem(label.list_reward, achieveData.rewards);
		label.Status.selectedIndex = (achieveData._FinishStatus ? 2 : ((!achieveData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!achieveData.TaskRunning) ? 1 : 0);
		ConditionType conditionType = achieveData._Config.ConditionType;
		if (conditionType == ConditionType.TotalCharacterDone || conditionType == ConditionType.TotalCharacterWin)
		{
			label.txt_taskDesc.text = string.Format(achieveData._Config.DescId.GetLocal(UIStringType.Achieve), achieveData._Config.Param, CharacterHandle.GetCharacterNickName(achieveData._Config.Ref));
		}
		else
		{
			label.txt_taskDesc.text = achieveData._Config.DescId.GetLocal(UIStringType.Achieve);
		}
		label.txt_taskTitle.text = achieveData._Config.NameID.GetLocal(UIStringType.Achieve);
		RefreshBar(label.bar_task, achieveData._Config.Param, achieveData._Progress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (!achieveData.TaskRunning && !achieveData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.task.RequestTaskRewardC2S(achieveData._Config.Id, 3).OnFinishedOnly.AddOnce(delegate
				{
					if (btn_AchieveTab != null)
					{
						btn_AchieveTab.redStatus.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveSystemStatus() ? 1 : 0);
					}
					RefreshAchieveData();
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
	}

	private void InitComponents_Acquisition()
	{
		base.ui.com_Acquisition.com_AcceptInvite.list_Task.SetVirtual();
		base.ui.com_Acquisition.com_AcceptInvite.list_Task.itemRenderer = RendererAcceptInviteTask;
		base.ui.com_Acquisition.com_Invite.list_Task.SetVirtual();
		base.ui.com_Acquisition.com_Invite.list_Task.itemRenderer = RendererInviteTask;
	}

	private void Refresh_Acquisition(UITask_Button_Tab btnTab)
	{
		btn_AcquisitionTab = btnTab;
		RefreshAcquisitionTabStatus();
	}

	private void RefreshAcquisitionTabStatus()
	{
		if (btn_AcquisitionTab != null)
		{
			btn_AcquisitionTab.redStatus.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.acquisition.GetSystemStatus() ? 1 : 0);
		}
	}

	private void AddEvent_Acquisition()
	{
		base.ui.com_Acquisition.list_Menu.onClickItem.Add(SelectAcquisitionTab);
		base.ui.com_Acquisition.com_AcceptInvite.btn_SureCode.onClick.Add(SureAcceptCode);
		base.ui.com_Acquisition.com_Invite.btn_CopyCode.onClick.Add(CopyInviteCode);
		base.ui.com_Acquisition.btn_Rule.onClick.Add(ShowRule);
		base.ui.com_Acquisition.com_AcceptInvite.Input_Code.onChanged.Add(ShowUpperText);
	}

	private void RemoveEvent_Acquisition()
	{
		base.ui.com_Acquisition.list_Menu.onClickItem.Remove(SelectAcquisitionTab);
		base.ui.com_Acquisition.com_AcceptInvite.btn_SureCode.onClick.Remove(SureAcceptCode);
		base.ui.com_Acquisition.com_Invite.btn_CopyCode.onClick.Remove(CopyInviteCode);
		base.ui.com_Acquisition.btn_Rule.onClick.Remove(ShowRule);
		base.ui.com_Acquisition.com_AcceptInvite.Input_Code.onChanged.Remove(ShowUpperText);
	}

	private void SelectAcquisitionTab(EventContext context)
	{
		base.ui.com_Acquisition.list_Menu.onClickItem.Retain();
		if (context.data is UITask_Button_Acquisition uITask_Button_Acquisition)
		{
			if (uITask_Button_Acquisition.type.selectedIndex == 0)
			{
				base.ui.com_Acquisition.tab.selectedIndex = 0;
				RefreshAcceptInviteInfo();
			}
			else if (uITask_Button_Acquisition.type.selectedIndex == 1)
			{
				base.ui.com_Acquisition.tab.selectedIndex = 1;
				RefreshInviteInfo();
			}
			base.ui.com_Acquisition.list_Menu.onClickItem.Release();
		}
	}

	private void RefreshAcquisitionData()
	{
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		if (playerInfo != null)
		{
			AcquisitionData = SimpleSingletonProvider<GameLogicManager>.inst.acquisition.AcquisitionData;
			base.ui.com_Acquisition.loader_Character.url = AcquisitionData.AcquisitionInfo.CharacterImage;
			base.ui.com_Acquisition.list_Menu.numItems = 0;
			if (AcquisitionData.AcquisitionInfo.CaptchaValidLv > playerInfo.Level || AcquisitionData.IsFinishInvited())
			{
				tab_AcceptInvite = AddItemFromPool();
				tab_AcceptInvite.type.selectedIndex = 0;
				tab_AcceptInvite.title = 1.GetLocal(UIStringType.Acquisition);
				RefreshAcceptInviteTab();
			}
			tab_Invite = AddItemFromPool();
			tab_Invite.type.selectedIndex = 1;
			tab_Invite.title = 2.GetLocal(UIStringType.Acquisition);
			base.ui.com_Acquisition.Cut_in.Play();
			base.ui.com_Acquisition.list_Menu.GetChildAt(0).onClick.Call();
			RefreshInviteTab();
		}
	}

	public void RefreshAcquisition()
	{
		RefreshAcquisitionTabStatus();
		if (base.ui.tab.selectedIndex == (int)(WelfareType_Acquisition - 1))
		{
			if (base.ui.com_Acquisition.tab.selectedIndex == 0)
			{
				RefreshAcceptInviteTab();
				RefreshAcceptInviteInfo();
			}
			else if (base.ui.com_Acquisition.tab.selectedIndex == 1)
			{
				RefreshInviteInfo();
				RefreshInviteTab();
			}
		}
	}

	private async void ShowRule()
	{
		base.ui.com_Acquisition.btn_Rule.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.rule.TryShow(3.GetLocal(UIStringType.Acquisition), 4.GetLocal(UIStringType.Acquisition));
		base.ui.com_Acquisition.btn_Rule.onClick.Release();
	}

	private void SureAcceptCode(EventContext context)
	{
		if (AcquisitionData.IsFinishInvited())
		{
			return;
		}
		string code = base.ui.com_Acquisition.com_AcceptInvite.Input_Code.text;
		if (string.IsNullOrEmpty(code) || code.Length != 13)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1070);
			return;
		}
		base.ui.com_Acquisition.com_AcceptInvite.btn_SureCode.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.acquisition.RequestAcquisitionC2S(AcquisitionData.AcquisitionInfo.Id, code).OnFinished.AddOnce(delegate(RPCAsyncResult msg)
		{
			base.ui.com_Acquisition.com_AcceptInvite.btn_SureCode.onClick.Release();
			if (msg.errId == 0)
			{
				AcquisitionData.UpdateInvitedCode(code);
				RefreshAcceptInviteInfo();
			}
		});
	}

	private void RefreshAcceptInviteTab()
	{
		if (tab_AcceptInvite != null)
		{
			tab_AcceptInvite.redStatus.selectedIndex = (AcquisitionData.GetAcceptInviteTaskStatus() ? 1 : 0);
		}
	}

	private void RefreshAcceptInviteInfo()
	{
		bool flag = AcquisitionData.IsFinishInvited();
		base.ui.com_Acquisition.com_AcceptInvite.status.selectedIndex = (flag ? 1 : 0);
		base.ui.com_Acquisition.com_AcceptInvite.Input_Code.touchable = !flag;
		base.ui.com_Acquisition.com_AcceptInvite.Input_Code.text = AcquisitionData.InvitedCode;
		RendererFinishInvitedReward();
		RefreshAcceptInviteTask();
	}

	private void RefreshAcceptInviteTask()
	{
		AcquisitionTaskId = AcquisitionData.GetTaskId(AcquisitionData.AcquisitionInfo.InvitedTaskIDs);
		base.ui.com_Acquisition.com_AcceptInvite.list_Task.numItems = AcquisitionTaskId.Count;
	}

	private void RendererFinishInvitedReward()
	{
		KeyValuePair<int, int> keyValuePair = AcquisitionData.AcquisitionInfo.Reward.ElementAt(0);
		ItemInfoConfigure itemInfoConfigure = keyValuePair.Key.GetItemInfoConfigure();
		base.ui.com_Acquisition.com_AcceptInvite.loader_Icon.url = itemInfoConfigure.Icon;
		base.ui.com_Acquisition.com_AcceptInvite.txt_itemNum.text = $"x{keyValuePair.Value}";
	}

	private void RendererAcceptInviteTask(int index, GObject item)
	{
		AcquisitionTaskData taskData = AcquisitionData.taskDataDict[AcquisitionTaskId[index]];
		RendererAcquisitionTask(item, taskData, delegate
		{
			RefreshAcceptInviteTask();
			RefreshAcceptInviteTab();
			RefreshAcquisitionTabStatus();
		});
	}

	private void ShowUpperText(EventContext context)
	{
		GTextInput input_Code = base.ui.com_Acquisition.com_AcceptInvite.Input_Code;
		input_Code.onChanged.Retain();
		input_Code.text = input_Code.text.ToUpper();
		input_Code.onChanged.Release();
	}

	private void CopyInviteCode(EventContext context)
	{
		base.ui.com_Acquisition.com_Invite.btn_CopyCode.onClick.Retain();
		GUIUtility.systemCopyBuffer = AcquisitionData.InvitePlayerCode;
		SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1009.GetLocal(UIStringType.Spectate));
		base.ui.com_Acquisition.com_Invite.btn_CopyCode.onClick.Release();
	}

	private void RefreshInviteTab()
	{
		if (tab_AcceptInvite != null)
		{
			tab_Invite.redStatus.selectedIndex = (AcquisitionData.GetInviteTaskStatus() ? 1 : 0);
		}
	}

	private void RefreshInviteInfo()
	{
		base.ui.com_Acquisition.com_Invite.txt_InviteCode.text = AcquisitionData.InvitePlayerCode;
		AcquisitionTaskId = AcquisitionData.GetTaskId(AcquisitionData.AcquisitionInfo.InviteTaskIDs);
		base.ui.com_Acquisition.com_Invite.list_Task.numItems = AcquisitionTaskId.Count;
	}

	private void RendererInviteTask(int index, GObject item)
	{
		AcquisitionTaskData taskData = AcquisitionData.taskDataDict[AcquisitionTaskId[index]];
		RendererAcquisitionTask(item, taskData, RefreshAcquisition);
	}

	private UITask_Button_Acquisition AddItemFromPool()
	{
		return base.ui.com_Acquisition.list_Menu.AddItemFromPool("ui://hhpzjcmzh38b4e") as UITask_Button_Acquisition;
	}

	private void RendererAcquisitionTask(GObject item, AcquisitionTaskData taskData, System.Action UpdateEvent)
	{
		UIAcquisition_Com_Task label = item as UIAcquisition_Com_Task;
		if (label == null)
		{
			return;
		}
		RendererRewardItem(label.list_reward, taskData.rewards);
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!taskData.TaskRunning) ? 1 : 0);
		label.txt_taskTitle.text = taskData.GetTaskDesc();
		RefreshBar(label.bar_task, taskData._Config.Params[0], taskData._Progress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (!taskData.TaskRunning && !taskData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.acquisition.RequestAcquisitionRewardC2S(taskData._Config.Id).OnFinishedOnly.AddOnce(delegate
				{
					AcquisitionData.UpdateFinishTask(taskData._Config.Id);
					UpdateEvent();
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
	}

	private void InitComponents_GloryTask()
	{
	}

	private void Refresh_GloryTask()
	{
	}

	private void AddEvent_GloryTask()
	{
	}

	private void RemoveEvent_GloryTask()
	{
	}

	private void RefreshGloryData()
	{
	}

	private void RendererGloryTask(int index, GObject item)
	{
	}

	private void InitComponents_NoviceTask()
	{
		base.ui.list_Task.SetVirtual();
		base.ui.list_Task.itemRenderer = RendererTask;
	}

	private void Refresh_NoviceTask(UITask_Button_Tab btnTab)
	{
		btn_NoviceTaskTab = btnTab;
		btnTab.redStatus.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.task.GetTaskSystemStatus() ? 1 : 0);
	}

	private void AddEvent_NoviceTask()
	{
	}

	private void RemoveEvent_NoviceTask()
	{
	}

	private void RefreshTaskData()
	{
		taskIds = SimpleSingletonProvider<GameLogicManager>.inst.task.GetTaskId();
		base.ui.list_Task.numItems = taskIds.Count;
		SetTargetLabel_Task();
	}

	private void SetTargetLabel_Task()
	{
		if (_labelDataId == 0)
		{
			return;
		}
		for (int i = 0; i < taskIds.Count; i++)
		{
			if (taskIds[i] == _labelDataId)
			{
				base.ui.list_Task.ScrollToView(i);
			}
		}
		_labelDataId = 0;
	}

	private void RendererTask(int index, GObject item)
	{
		UITask_Com_NoviceLabel label = item as UITask_Com_NoviceLabel;
		if (label == null)
		{
			return;
		}
		TaskData taskData = SimpleSingletonProvider<GameLogicManager>.inst.task.taskDataDict[taskIds[index]];
		RendererRewardItem(label.list_reward, taskData.rewards);
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!taskData.TaskRunning) ? 1 : 0);
		if (taskData._Config.ConditionType == ConditionType.CampaignLevelPass)
		{
			label.txt_taskDesc.text = string.Format(taskData._Config.DescId.GetLocal(UIStringType.Task), taskData._Config.Ref.GetCampaignLevelConfigure().Name.GetLocal(UIStringType.Campaign));
			label.txt_taskTitle.text = string.Format(taskData._Config.DescId.GetLocal(UIStringType.Task), taskData._Config.Ref.GetCampaignLevelConfigure().Name.GetLocal(UIStringType.Campaign));
		}
		else
		{
			label.txt_taskDesc.text = taskData._Config.DescId.GetLocal(UIStringType.Task);
			label.txt_taskTitle.text = taskData._Config.NameID.GetLocal(UIStringType.Task);
		}
		RefreshBar(label.bar_task, taskData._Config.Param, taskData._Progress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (!taskData.TaskRunning && !taskData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.task.RequestTaskRewardC2S(taskData._Config.Id, 1).OnFinishedOnly.AddOnce(delegate
				{
					if (btn_NoviceTaskTab != null)
					{
						btn_NoviceTaskTab.redStatus.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.task.GetTaskSystemStatus() ? 1 : 0);
					}
					RefreshTaskData();
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
		label.btn_GoWay.visible = taskData._Config.Way != 0;
		label.btn_GoWay.onClick.Set((EventCallback0)delegate
		{
			GoTargetPanel(label.btn_GoWay, taskData);
		});
	}

	private void InitComponents_SevenTask()
	{
		base.ui.com_Seven.list_Task.itemRenderer = RendererSevenTask;
	}

	private void Refresh_SevenTask(UITask_Button_Tab btnTab)
	{
		int runningTaskDay = sevenDayTask.GetRunningTaskDay();
		base.ui.com_Seven.com_SelectDay.TabDay.selectedIndex = runningTaskDay - 1;
	}

	private void AddEvent_SevenTask()
	{
		base.ui.com_Seven.com_SelectDay.TabDay.onChanged.Add(SwitchDayTask);
	}

	private void RemoveEvent_SevenTask()
	{
		base.ui.com_Seven.com_SelectDay.TabDay.onChanged.Remove(SwitchDayTask);
	}

	private void RefreshSevenData()
	{
		base.ui.com_Seven.com_SelectDay.TabDay.onChanged.Call();
		for (int i = 0; i < 7; i++)
		{
			UITask_Button_SevenDay sevenDay = GetSevenDay(i);
			sevenDay.Status.selectedIndex = ((sevenDayTask.SystemProgress > i) ? 1 : 0);
			sevenDay.redStatus.selectedIndex = (sevenDayTask.GetTaskSystemStatusByDay(i + 1) ? 1 : 0);
		}
	}

	private void SwitchDayTask()
	{
		base.ui.com_Seven.com_SelectDay.TabDay.onChanged.Retain();
		int num = base.ui.com_Seven.com_SelectDay.TabDay.selectedIndex + 1;
		sevenTasksData = sevenDayTask.GetSevenDayTaskByDay(num);
		base.ui.com_Seven.list_Task.numItems = sevenTasksData.Count;
		base.ui.com_Seven.taskStatus.selectedIndex = ((sevenDayTask.SystemProgress >= num) ? 1 : 0);
		base.ui.com_Seven.com_SelectDay.TabDay.onChanged.Release();
	}

	private UITask_Button_SevenDay GetSevenDay(int index)
	{
		return index switch
		{
			0 => base.ui.com_Seven.com_SelectDay.btn_0, 
			1 => base.ui.com_Seven.com_SelectDay.btn_1, 
			2 => base.ui.com_Seven.com_SelectDay.btn_2, 
			3 => base.ui.com_Seven.com_SelectDay.btn_3, 
			4 => base.ui.com_Seven.com_SelectDay.btn_4, 
			5 => base.ui.com_Seven.com_SelectDay.btn_5, 
			6 => base.ui.com_Seven.com_SelectDay.btn_6, 
			_ => null, 
		};
	}

	private void RendererSevenTask(int index, GObject item)
	{
		UITask_Com_NoviceLabel label = item as UITask_Com_NoviceLabel;
		if (label == null)
		{
			return;
		}
		SevenTaskData taskData = sevenTasksData[index];
		RendererRewardItem(label.list_reward, taskData.rewards);
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!taskData.TaskRunning) ? 1 : 0);
		if (taskData._Config.ConditionType == ConditionType.TotalMapPlayCount)
		{
			label.txt_taskDesc.text = string.Format(taskData._Config.DescId.GetLocal(UIStringType.Task), taskData._Config.Param, taskData._Config.Ref.GetMapDataConfigure().MapName.GetLocal(UIStringType.Map));
		}
		else if (taskData._Config.ConditionType == ConditionType.TotalMapModePlayCount)
		{
			label.txt_taskDesc.text = string.Format(taskData._Config.DescId.GetLocal(UIStringType.Task), taskData._Config.Param, taskData._Config.Ref.GetGameModeInfoConfigure().NameID.GetLocal(UIStringType.GameMode));
		}
		else
		{
			label.txt_taskDesc.text = string.Format(taskData._Config.DescId.GetLocal(UIStringType.Task), taskData._Config.Param);
		}
		label.txt_taskTitle.text = taskData._Config.NameID.GetLocal(UIStringType.Task);
		RefreshBar(label.bar_task, taskData._Config.Param, taskData._Progress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (!taskData.TaskRunning && !taskData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.task.RequestTaskRewardC2S(taskData._Config.Id, 5).OnFinishedOnly.AddOnce(delegate
				{
					RefreshSevenData();
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
		label.btn_GoWay.visible = taskData._Config.Way != 0;
	}

	private void InitComponents_WeekTask()
	{
		base.ui.list_weekTask.SetVirtual();
		base.ui.list_weekTask.itemRenderer = RendererWeekTask;
		base.ui.com_liveness.list_Box.itemRenderer = RendererLivenessBox;
	}

	private void Refresh_WeekTask(UITask_Button_Tab btnTab)
	{
		btn_WeekTaskTab = btnTab;
		btnTab.redStatus.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.task.GetWeekTaskSystemStatus() ? 1 : 0);
	}

	private void AddEvent_WeekTask()
	{
	}

	private void RemoveEvent_WeekTask()
	{
	}

	private void RefreshWeekTaskData()
	{
		RefreshLiveness();
		RefreshWeekTask();
	}

	private void RefreshWeekTask()
	{
		weekTaskIds = SimpleSingletonProvider<GameLogicManager>.inst.task.GetWeekTaskId();
		base.ui.list_weekTask.numItems = weekTaskIds.Count;
		SetTargetLabel_WeekTask();
	}

	private void SetTargetLabel_WeekTask()
	{
		if (_labelDataId == 0)
		{
			return;
		}
		for (int i = 0; i < weekTaskIds.Count; i++)
		{
			if (weekTaskIds[i] == _labelDataId)
			{
				base.ui.list_weekTask.ScrollToView(i);
			}
		}
		_labelDataId = 0;
	}

	private void RendererWeekTask(int index, GObject item)
	{
		UITask_Com_WeekLabel label = item as UITask_Com_WeekLabel;
		if (label == null)
		{
			return;
		}
		WeekTaskData taskData = SimpleSingletonProvider<GameLogicManager>.inst.task.weekTaskDataDict[weekTaskIds[index]];
		RendererRewardItem(label.list_reward, taskData.rewards);
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!taskData.TaskRunning) ? 1 : 0);
		label.txt_taskDesc.text = taskData._Config.DescId.GetLocal(UIStringType.Task);
		label.txt_taskTitle.text = taskData._Config.NameID.GetLocal(UIStringType.Task);
		RefreshBar(label.bar_task, taskData._Config.Param, taskData._Progress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (!taskData.TaskRunning && !taskData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.task.RequestTaskRewardC2S(taskData._Config.Id, 2).OnFinishedOnly.AddOnce(delegate
				{
					if (btn_WeekTaskTab != null)
					{
						btn_WeekTaskTab.redStatus.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.task.GetWeekTaskSystemStatus() ? 1 : 0);
					}
					RefreshWeekTask();
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
	}

	private void RefreshLiveness()
	{
		liveness = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(52);
		GProgressBar bar_liveness = base.ui.com_liveness.bar_liveness;
		RepeatedField<TaskWeeklyProgressConfigure> weeklyProgresss = StaticConfigure.Task.WeeklyProgresss;
		bar_liveness.max = weeklyProgresss[weeklyProgresss.Count - 1].Progress;
		base.ui.com_liveness.bar_liveness.min = 0.0;
		base.ui.com_liveness.bar_liveness.value = liveness;
		base.ui.com_liveness.list_Box.numItems = StaticConfigure.Task.WeeklyProgresss.Count;
		GTextField gTextField = base.ui.com_liveness.txt_liveValue.SetVar("cur", liveness.ToString());
		RepeatedField<TaskWeeklyProgressConfigure> weeklyProgresss2 = StaticConfigure.Task.WeeklyProgresss;
		gTextField.SetVar("max", weeklyProgresss2[weeklyProgresss2.Count - 1].Progress.ToString()).FlushVars();
	}

	private void RendererLivenessBox(int index, GObject item)
	{
		UITask_Button_LivenessBox box = item as UITask_Button_LivenessBox;
		if (box == null)
		{
			return;
		}
		TaskWeeklyProgressConfigure livenessData = StaticConfigure.Task.WeeklyProgresss[index];
		bool finish = SimpleSingletonProvider<GameLogicManager>.inst.task.weekLivenessFinishIds.Contains(livenessData.Id);
		bool running = livenessData.Progress > liveness;
		box.title = livenessData.Progress.ToString();
		box.Status.selectedIndex = (finish ? 2 : ((!running) ? 1 : 0));
		box.touchable = box.Status.selectedIndex != 2;
		box.grayed = box.Status.selectedIndex == 2;
		box.onClick.Set((EventCallback0)delegate
		{
			if (running && !finish)
			{
				SimpleSingletonProvider<UIManager>.inst.boxProp.ShowGift(livenessData.Reward).Forget();
			}
			else if (!(running || finish))
			{
				box.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.task.RequestTaskRewardC2S(livenessData.Id, 4).OnFinishedOnly.AddOnce(delegate
				{
					if (btn_WeekTaskTab != null)
					{
						btn_WeekTaskTab.redStatus.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.task.GetWeekTaskSystemStatus() ? 1 : 0);
					}
					box.touchable = false;
					box.grayed = true;
					box.Status.selectedIndex = 2;
					box.onClick.Release();
				});
			}
		});
	}

	public TaskPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UITaskPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (objs != null && objs.Length != 0 && objs[0] is RepeatedField<int> repeatedField)
		{
			_InitTabType = repeatedField[0];
			_labelDataId = ((repeatedField.Count > 1) ? repeatedField[1] : 0);
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		InitComponents_NoviceTask();
		InitComponents_Achieve();
		InitComponents_WeekTask();
		InitComponents_SevenTask();
		InitComponents_GloryTask();
		InitComponents_Acquisition();
	}

	public override void Refresh()
	{
		base.Refresh();
		RepeatedField<WelfareInfoConfigure> infos = StaticConfigure.Welfare.Infos;
		List<WelfareInfoConfigure> _list = new List<WelfareInfoConfigure>();
		foreach (WelfareInfoConfigure item in infos)
		{
			if (item.IsShow)
			{
				_list.Add(item);
			}
		}
		base.ui.list_Menu.itemRenderer = delegate(int index2, GObject item)
		{
			if (item is UITask_Button_Tab uITask_Button_Tab)
			{
				uITask_Button_Tab.title = _list[index2].Name.GetLocal(UIStringType.Welfare);
				uITask_Button_Tab.data = (int)_list[index2].WelfareType;
				switch (_list[index2].WelfareType)
				{
				case WelfareType.Beginner:
					Refresh_NoviceTask(uITask_Button_Tab);
					break;
				case WelfareType.Weekly:
					Refresh_WeekTask(uITask_Button_Tab);
					break;
				case WelfareType.Achieve:
					Refresh_Achieve(uITask_Button_Tab);
					break;
				case WelfareType.Beginner7Days:
					Refresh_SevenTask(uITask_Button_Tab);
					break;
				case WelfareType.Acquisition:
					Refresh_Acquisition(uITask_Button_Tab);
					break;
				case WelfareType.Glory:
					break;
				}
			}
		};
		base.ui.list_Menu.numItems = _list.Count;
		int index = 0;
		if (_InitTabType != 0)
		{
			for (int num = 0; num < _list.Count; num++)
			{
				if (_list[num].WelfareType == (WelfareType)_InitTabType)
				{
					index = num;
					break;
				}
			}
			_InitTabType = 0;
		}
		base.ui.list_Menu.GetChildAt(index).onClick.Call();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.list_Menu.onClickItem.Add(SelectMenu);
		AddEvent_WeekTask();
		AddEvent_SevenTask();
		AddEvent_Acquisition();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.list_Menu.onClickItem.Remove(SelectMenu);
		RemoveEvent_WeekTask();
		RemoveEvent_SevenTask();
		RemoveEvent_Acquisition();
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.AddListener(RefreshLiveness);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.RemoveListener(RefreshLiveness);
	}

	public override void Close()
	{
		base.ui.list_Task.scrollPane.percY = 0f;
		base.ui.list_weekTask.scrollPane.percY = 0f;
		base.ui.list_Achieve.scrollPane.percY = 0f;
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
	}

	private void SelectMenu(EventContext context)
	{
		if (context.data is UITask_Button_Tab { data: var data } && data is int num)
		{
			base.ui.list_Menu.onClickItem.Retain();
			base.ui.tab.selectedIndex = num - 1;
			switch ((WelfareType)num)
			{
			case WelfareType.Beginner:
				RefreshTaskData();
				break;
			case WelfareType.Weekly:
				RefreshWeekTaskData();
				break;
			case WelfareType.Achieve:
				RefreshAchieveData();
				break;
			case WelfareType.Beginner7Days:
				RefreshSevenData();
				break;
			case WelfareType.Glory:
				RefreshGloryData();
				break;
			case WelfareType.Acquisition:
				RefreshAcquisitionData();
				break;
			}
			base.ui.list_Menu.onClickItem.Release();
		}
	}

	private void RefreshBar(GProgressBar bar_task, int configParam, int taskDataProgress)
	{
		bar_task.max = configParam;
		bar_task.min = 0.0;
		bar_task.value = Mathf.Min(taskDataProgress, configParam);
	}

	private void RendererRewardItem(GList list, List<KeyValuePair<int, int>> rewards)
	{
		list.itemRenderer = delegate(int i, GObject o)
		{
			KeyValuePair<int, int> keyValuePair = rewards[i];
			CommonUIManager.RendererLitItem((UICom_LitItem)o, keyValuePair.Key, keyValuePair.Value);
		};
		list.numItems = rewards.Count;
		list.scrollPane.touchEffect = rewards.Count > 3;
	}

	private async void GoTargetPanel(GButton btn, TaskData taskData)
	{
		if (taskData._Config.Way != 0 && taskData.TaskRunning && !taskData._FinishStatus)
		{
			btn.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(taskData._Config.Way);
			btn.onClick.Release();
		}
	}
}
