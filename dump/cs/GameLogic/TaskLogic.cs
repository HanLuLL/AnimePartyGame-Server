using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class TaskLogic : IRPCSync, IReadPoint
{
	public int weekLiveProgress;

	public bool finishTeach;

	public Dictionary<int, TaskData> taskDataDict;

	public List<int> taskIds;

	public List<int> taskFinishIds;

	public Dictionary<int, WeekTaskData> weekTaskDataDict;

	public List<int> weekTaskIds;

	public List<int> weekTaskFinishIds;

	public List<int> weekLivenessFinishIds;

	public Dictionary<int, AchieveData> achieveDataDict;

	public List<int> achieveIds;

	public List<int> achieveFinishIds;

	private Dictionary<int, int> playerAchieveInfo_1;

	private Dictionary<int, ConditionData> playerAchieveInfo_2;

	private Dictionary<int, int> playerWeekAchieveInfo;

	public SevenDayData sevenDayData;

	public GloryData gloryData;

	public RookieTaskDatas rookieTaskData;

	private int taskId;

	private int type;

	public readonly ReactiveProperty<bool> taskRedSignal = new ReactiveProperty<bool>(initialValue: false);

	public readonly Signal OnTaskChange = new Signal();

	public readonly Dictionary<int, MissionData> MissionRecordsDict = new Dictionary<int, MissionData>();

	public void InitFromServer(TaskInfo playerTask, MissionMod MissionMod)
	{
		playerAchieveInfo_1 = new Dictionary<int, int>();
		playerAchieveInfo_2 = new Dictionary<int, ConditionData>();
		playerWeekAchieveInfo = new Dictionary<int, int>(playerTask.WeekCondition.Count);
		taskDataDict = new Dictionary<int, TaskData>(StaticConfigure.Task.Beginners.Count);
		taskIds = new List<int>(taskDataDict.Count);
		taskFinishIds = new List<int>(taskDataDict.Count);
		weekTaskDataDict = new Dictionary<int, WeekTaskData>(StaticConfigure.Task.Weeklys.Count);
		weekTaskIds = new List<int>(weekTaskDataDict.Count);
		weekTaskFinishIds = new List<int>(weekTaskDataDict.Count);
		weekLivenessFinishIds = new List<int>(StaticConfigure.Task.WeeklyProgresss.Count);
		achieveDataDict = new Dictionary<int, AchieveData>(StaticConfigure.Achieve.Globals.Count);
		achieveIds = new List<int>(achieveDataDict.Count);
		achieveFinishIds = new List<int>(achieveDataDict.Count);
		UpdateTaskInfo(playerTask);
		foreach (TaskBeginnerConfigure beginner in StaticConfigure.Task.Beginners)
		{
			taskDataDict.TryAdd(beginner.Id, new TaskData(beginner));
			taskIds.Add(beginner.Id);
		}
		foreach (TaskWeeklyConfigure weekly in StaticConfigure.Task.Weeklys)
		{
			weekTaskDataDict.TryAdd(weekly.Id, new WeekTaskData(weekly));
			weekTaskIds.Add(weekly.Id);
		}
		foreach (AchieveGlobalConfigure global in StaticConfigure.Achieve.Globals)
		{
			achieveDataDict.TryAdd(global.Id, new AchieveData(global));
			achieveIds.Add(global.Id);
		}
		sevenDayData = new SevenDayData();
		gloryData = new GloryData();
		if (MissionMod != null && MissionMod.ActivityTasks.Count > 0)
		{
			foreach (TaskDSO activityTask in MissionMod.ActivityTasks)
			{
				UpdateMission(activityTask.Id, activityTask.Progress, activityTask.Status);
			}
		}
		rookieTaskData = new RookieTaskDatas();
		rookieTaskData.UpdateRookieTask();
	}

	private void UpdateTaskInfo(TaskInfo playerTask)
	{
		finishTeach = playerTask.IsTeaching;
		UpdatePlayerAchieve_1(playerTask.Condition, Noop: true);
		UpdatePlayerAchieve_2(playerTask.Condition1);
		UpdatePlayerWeekAchieve(playerTask.WeekCondition, Noop: true);
		UpdataPlayerTaskRewardStatus(playerTask.TaskRewardIs);
		UpdataPlayerWeekTaskRewardStatus(playerTask.WeekTaskRewardIs);
		UpdataPlayerAchieveRewardStatus(playerTask.AchieveRewardIs);
		UpdatePlayerWeekLivenessRewardStatus(playerTask.ProgressReward);
	}

	private void UpdatePlayerAchieve_1(MapField<int, int> Condition, bool Noop)
	{
		if (Noop)
		{
			playerAchieveInfo_1.Clear();
		}
		foreach (KeyValuePair<int, int> item in Condition)
		{
			playerAchieveInfo_1[item.Key] = item.Value;
		}
	}

	public int GetAchieveInfo_1(int _type)
	{
		if (!playerAchieveInfo_1.TryGetValue(_type, out var value))
		{
			return 0;
		}
		return value;
	}

	private void UpdatePlayerAchieve_2(MapField<int, ConditionData> Condition)
	{
		playerAchieveInfo_2.Clear();
		foreach (KeyValuePair<int, ConditionData> item in Condition)
		{
			playerAchieveInfo_2[item.Key] = item.Value;
		}
	}

	private void UpdatePlayerAchieve_2(ConditionData Condition)
	{
		if (Condition != null)
		{
			if (playerAchieveInfo_2.ContainsKey(Condition.CondType))
			{
				playerAchieveInfo_2[Condition.CondType] = Condition;
			}
			else
			{
				playerAchieveInfo_2.TryAdd(Condition.CondType, Condition);
			}
		}
	}

	public ConditionParams GetAchieveInfo_2(int _type, int _param)
	{
		if (playerAchieveInfo_2.TryGetValue(_type, out var value))
		{
			foreach (ConditionParams item in value.Params)
			{
				if (item.Param1 == _param)
				{
					return item;
				}
			}
		}
		return null;
	}

	private void UpdatePlayerWeekAchieve(MapField<int, int> Condition, bool Noop)
	{
		if (Noop)
		{
			playerWeekAchieveInfo.Clear();
		}
		foreach (KeyValuePair<int, int> item in Condition)
		{
			playerWeekAchieveInfo[item.Key] = item.Value;
		}
	}

	public int GetWeekAchieveInfo(int _type)
	{
		if (!playerWeekAchieveInfo.TryGetValue(_type, out var value))
		{
			return 0;
		}
		return value;
	}

	private void UpdataPlayerTaskRewardStatus(RepeatedField<int> rewardIds)
	{
		taskFinishIds.Clear();
		foreach (int rewardId in rewardIds)
		{
			taskFinishIds.Add(rewardId);
		}
	}

	private void UpdataPlayerWeekTaskRewardStatus(RepeatedField<int> rewardIds)
	{
		weekTaskFinishIds.Clear();
		foreach (int rewardId in rewardIds)
		{
			weekTaskFinishIds.Add(rewardId);
		}
	}

	private void UpdataPlayerAchieveRewardStatus(RepeatedField<int> rewardIds)
	{
		achieveFinishIds.Clear();
		foreach (int rewardId in rewardIds)
		{
			achieveFinishIds.Add(rewardId);
		}
	}

	private void UpdatePlayerWeekLivenessRewardStatus(int progressReward)
	{
		weekLivenessFinishIds.Clear();
		foreach (TaskWeeklyProgressConfigure item in StaticConfigure.Task.WeeklyProgresss)
		{
			if ((progressReward & item.Id) != 0)
			{
				weekLivenessFinishIds.Add(item.Id);
			}
		}
	}

	public List<int> GetTaskId()
	{
		taskIds.Sort((int x, int y) => CompareTo(taskDataDict[x], taskDataDict[y]));
		return taskIds;
	}

	public List<int> GetWeekTaskId()
	{
		weekTaskIds.Sort((int x, int y) => CompareTo(weekTaskDataDict[x], weekTaskDataDict[y]));
		return weekTaskIds;
	}

	public List<int> GetAchieveIds()
	{
		achieveIds.Sort((int x, int y) => CompareTo(achieveDataDict[x], achieveDataDict[y]));
		return achieveIds;
	}

	public int CompareTo(BaseTaskData x, BaseTaskData y)
	{
		if (!x._FinishStatus && !x.TaskRunning)
		{
			if (y._FinishStatus || y.TaskRunning)
			{
				return -1;
			}
			return 1;
		}
		if (!x._FinishStatus && x.TaskRunning)
		{
			if (!y._FinishStatus && !y.TaskRunning)
			{
				return 1;
			}
			if (!y._FinishStatus && y.TaskRunning)
			{
				if (x._Id <= y._Id)
				{
					return -1;
				}
				return 1;
			}
			return -1;
		}
		if (!y._FinishStatus || !y.TaskRunning)
		{
			return 1;
		}
		return -1;
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.TaskConditionS2C.OnTaskConditionS2CServerCallBackAsync = OnTaskConditionS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.TaskInfoS2C.OnTaskInfoS2CServerCallBackAsync = OnTaskInfoS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.TaskRewardS2C.OnTaskRewardS2CServerCallBackAsync = OnTaskRewardS2CServerCallBack;
		Connect_Mission();
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.TaskConditionS2C.OnTaskConditionS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.TaskInfoS2C.OnTaskInfoS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.TaskRewardS2C.OnTaskRewardS2CServerCallBackAsync = null;
		Disconnect_Mission();
	}

	private async UniTask OnTaskConditionS2CServerCallBack(TaskConditionS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			if (model.Type == 1)
			{
				UpdatePlayerAchieve_1(model.Cond, Noop: false);
				UpdatePlayerAchieve_2(model.Cond1);
			}
			else if (model.Type == 2)
			{
				UpdatePlayerWeekAchieve(model.Cond, Noop: false);
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnTaskInfoS2CServerCallBack(TaskInfoS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			UpdateTaskInfo(model.Task);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestTaskRewardC2S(int _taskId, int _type)
	{
		taskId = _taskId;
		type = _type;
		return MonoSingletonProvider<NetManager>.inst.RPC.TaskRewardC2S.TaskRewardC2SCall(new TaskRewardC2S
		{
			DefId = taskId,
			Type = type
		});
	}

	private async UniTask OnTaskRewardS2CServerCallBack(TaskRewardS2C model, int errId, bool isdispatch)
	{
		if (errId != 0)
		{
			return;
		}
		if (type == 1 || type == 5)
		{
			if (!taskFinishIds.Contains(taskId))
			{
				taskFinishIds.Add(taskId);
			}
		}
		else if (type == 2)
		{
			if (!weekTaskFinishIds.Contains(taskId))
			{
				weekTaskFinishIds.Add(taskId);
			}
		}
		else if (type == 3)
		{
			if (!achieveFinishIds.Contains(taskId))
			{
				achieveFinishIds.Add(taskId);
			}
		}
		else if (type == 4)
		{
			UpdatePlayerWeekLivenessRewardStatus(model.ProgressReward);
		}
		await UniTask.CompletedTask;
	}

	public void RegisterRed()
	{
		taskRedSignal.Value = GetSystemStatus();
	}

	public bool GetSystemStatus()
	{
		if (GetTaskSystemStatus())
		{
			return true;
		}
		if (GetWeekTaskSystemStatus())
		{
			return true;
		}
		if (GetAchieveSystemStatus())
		{
			return true;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.acquisition.GetSystemStatus())
		{
			return true;
		}
		return false;
	}

	public bool GetTaskSystemStatus()
	{
		foreach (KeyValuePair<int, TaskData> item in taskDataDict)
		{
			if (!item.Value._FinishStatus && !item.Value.TaskRunning)
			{
				return true;
			}
		}
		return false;
	}

	public bool GetWeekTaskSystemStatus()
	{
		int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(52);
		RepeatedField<TaskWeeklyProgressConfigure> weeklyProgresss = StaticConfigure.Task.WeeklyProgresss;
		if (itemCount < weeklyProgresss[weeklyProgresss.Count - 1].Progress)
		{
			foreach (KeyValuePair<int, WeekTaskData> item in weekTaskDataDict)
			{
				if (!item.Value._FinishStatus && !item.Value.TaskRunning)
				{
					return true;
				}
			}
		}
		foreach (TaskWeeklyProgressConfigure item2 in StaticConfigure.Task.WeeklyProgresss)
		{
			if (itemCount >= item2.Progress && !weekLivenessFinishIds.Contains(item2.Id))
			{
				return true;
			}
		}
		return false;
	}

	public bool GetAchieveSystemStatus()
	{
		foreach (KeyValuePair<int, AchieveData> item in achieveDataDict)
		{
			if (!item.Value._FinishStatus && !item.Value.TaskRunning)
			{
				return true;
			}
		}
		return false;
	}

	public List<int> GetReachAchieveids()
	{
		List<int> list = new List<int>();
		foreach (AchieveGlobalConfigure global in StaticConfigure.Achieve.Globals)
		{
			if (achieveDataDict.TryGetValue(global.Id, out var value) && !value.TaskRunning)
			{
				list.Add(global.Id);
			}
		}
		return list;
	}

	public void UpdateMission(int missionId, int missionProgress, int status)
	{
		if (StaticConfigure.Mission.DataDict.TryGetValue(missionId, out var value))
		{
			if (!MissionRecordsDict.TryGetValue(missionId, out var value2))
			{
				value2 = new MissionData(value);
				MissionRecordsDict.Add(missionId, value2);
			}
			value2.UpdateData(missionProgress, status);
		}
		else
		{
			Debug.LogError($"MissionId:{missionId} does not exist");
		}
	}

	public MissionData TryGetMissionById(int missionId)
	{
		if (StaticConfigure.Mission.DataDict.TryGetValue(missionId, out var value))
		{
			if (!MissionRecordsDict.TryGetValue(missionId, out var value2))
			{
				value2 = new MissionData(value);
				MissionRecordsDict.Add(missionId, value2);
			}
			return value2;
		}
		Debug.LogError($"MissionId:{missionId} does not exist");
		return null;
	}

	private void Connect_Mission()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.PlayerTaskNotifyS2C.OnPlayerTaskNotifyS2CServerCallBackAsync = OnPlayerTaskNotifyS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.ActivityMissionRewardS2C.OnActivityMissionRewardS2CServerCallBackAsync = OnActivityMissionRewardS2CServerCallBackAsync;
	}

	private void Disconnect_Mission()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.PlayerTaskNotifyS2C.OnPlayerTaskNotifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ActivityMissionRewardS2C.OnActivityMissionRewardS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestActivityMissionRewardC2S(int Id)
	{
		Debug.Log($"[Mission] 请求领取奖励 TaskId={Id}");
		MissionData missionData = TryGetMissionById(Id);
		if (missionData == null)
		{
			Debug.LogError($"[Mission] 任务不存在 TaskId={Id}");
			return null;
		}
		Debug.Log($"[Mission] 当前任务状态 TaskId={Id}, Status={missionData.Status}, Progress={missionData.AchieveProgress}/{missionData.TaskTarget}, TaskRunning={missionData.TaskRunning}, _FinishStatus={missionData._FinishStatus}");
		if (missionData.Status == 3)
		{
			Debug.LogWarning($"[Mission] 任务已领取 TaskId={Id}");
			return null;
		}
		bool flag = missionData.Status == 2;
		bool flag2 = missionData.AchieveProgress >= missionData.TaskTarget;
		if (!flag && !flag2)
		{
			Debug.LogError($"[Mission] 任务未完成: TaskId={Id}, Status={missionData.Status}(需要{2}), Progress={missionData.AchieveProgress}/{missionData.TaskTarget}");
			return null;
		}
		if (flag2 && !flag)
		{
			Debug.LogWarning($"[Mission] 任务进度已达成，但状态未同步: TaskId={Id}, Status={missionData.Status}(需要{2}), Progress={missionData.AchieveProgress}/{missionData.TaskTarget}。尝试发送请求，让服务器判断。");
		}
		Debug.Log($"[Mission] 发送领取请求 TaskId={Id}");
		return MonoSingletonProvider<NetManager>.inst.RPC.ActivityMissionRewardC2S.ActivityMissionRewardC2SCall(new ActivityMissionRewardC2S
		{
			TaskId = Id
		});
	}

	public RPCAsyncResult LaborActDiceC2S(bool isUseDice)
	{
		int diceType = ((!isUseDice) ? 1 : 2);
		return MonoSingletonProvider<NetManager>.inst.RPC.LaborActDiceC2S.LaborActDiceC2SCall(new LaborActDiceC2S
		{
			DiceType = diceType
		});
	}

	private async UniTask OnActivityMissionRewardS2CServerCallBackAsync(ActivityMissionRewardS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			Debug.LogError($"[Mission] 服务器拒绝领取: TaskId={model?.TaskId ?? 0}, ErrorId={errId}");
		}
		else if (model != null)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(model.TaskId)?.UpdateStatus(3);
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnPlayerTaskNotifyS2CServerCallBackAsync(PlayerTaskNotifyS2C model, int errId, bool isDispatch)
	{
		if (errId != 0 || model == null || model.Tasks == null)
		{
			return;
		}
		for (int i = 0; i < model.Tasks.Count; i++)
		{
			TaskDSO taskDSO = model.Tasks[i];
			int num = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(taskDSO.Id)?.Status ?? (-1);
			UpdateMission(taskDSO.Id, taskDSO.Progress, taskDSO.Status);
			if (num != taskDSO.Status)
			{
				Debug.Log($"[Mission] 服务器状态更新: TaskId={taskDSO.Id}, Status {num}→{taskDSO.Status}, Progress={taskDSO.Progress}");
			}
		}
		OnTaskChange.Dispatch();
		await UniTask.CompletedTask;
	}
}
