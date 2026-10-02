using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;
using party.model;
using party.protocol;

namespace GameLogic;

public class AcquisitionData
{
	public readonly AcquisitionInfoConfigure AcquisitionInfo;

	private int InviteFinishCount;

	public readonly Dictionary<int, AcquisitionTaskData> taskDataDict;

	public readonly List<int> taskFinishIds = new List<int>();

	public string InvitedCode { get; private set; }

	public string InvitePlayerCode { get; private set; }

	public AcquisitionData(InviteInfo InviteInfo)
	{
		if (InviteInfo != null)
		{
			taskFinishIds.AddRange(InviteInfo.TaskFinishIds);
		}
		AcquisitionInfo = StaticConfigure.Acquisition.Infos[0];
		taskDataDict = new Dictionary<int, AcquisitionTaskData>();
		TryAddTask(AcquisitionInfo.InvitedTaskIDs);
		TryAddTask(AcquisitionInfo.InviteTaskIDs);
	}

	private void TryAddTask(RepeatedField<int> TaskIDs)
	{
		foreach (int TaskID in TaskIDs)
		{
			if (StaticConfigure.Acquisition.TaskDict.TryGetValue(TaskID, out var value))
			{
				taskDataDict.TryAdd(TaskID, new AcquisitionTaskData(AcquisitionInfo.Id, value));
			}
		}
	}

	public void UpdateInviteInfo(InviteInfoNotifyS2C info)
	{
		InvitePlayerCode = info.InviteCode;
		InvitedCode = info.Inviter;
		InviteFinishCount = info.Num;
	}

	public void UpdateInviter(int _inviter)
	{
	}

	public void UpdateInvitedCode(string _invitedCode)
	{
		InvitedCode = _invitedCode;
	}

	public void UpdateInviteFinishCount(int _Count)
	{
		InviteFinishCount = _Count;
	}

	public void UpdateFinishTask(int _taskId)
	{
		if (!taskFinishIds.Contains(_taskId))
		{
			taskFinishIds.Add(_taskId);
		}
	}

	public bool IsFinishInvited()
	{
		return !string.IsNullOrEmpty(InvitedCode);
	}

	public bool GetAcceptInviteTaskStatus()
	{
		RepeatedField<int> invitedTaskIDs = AcquisitionInfo.InvitedTaskIDs;
		return GetTaskStatus(invitedTaskIDs);
	}

	public bool GetInviteTaskStatus()
	{
		RepeatedField<int> inviteTaskIDs = AcquisitionInfo.InviteTaskIDs;
		return GetTaskStatus(inviteTaskIDs);
	}

	private bool GetTaskStatus(RepeatedField<int> TaskIDs)
	{
		foreach (int TaskID in TaskIDs)
		{
			if (taskDataDict.TryGetValue(TaskID, out var value) && !value._FinishStatus && !value.TaskRunning)
			{
				return true;
			}
		}
		return false;
	}

	public List<int> GetTaskId(RepeatedField<int> taskIds)
	{
		List<int> list = new List<int>();
		list.AddRange(taskIds);
		list.Sort((int x, int y) => CompareTo(taskDataDict[x], taskDataDict[y]));
		return list;
	}

	private int CompareTo(AcquisitionTaskData x, AcquisitionTaskData y)
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
				return x._Config.OrderWeight - y._Config.OrderWeight;
			}
			return -1;
		}
		if (!y._FinishStatus || !y.TaskRunning)
		{
			return 1;
		}
		return -1;
	}

	public int GetProgress(int taskID, int _type)
	{
		switch (_type)
		{
		case 307:
			return InviteFinishCount;
		case 300:
			if (!IsFinishInvited())
			{
				return 0;
			}
			return SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo()?.Level ?? 0;
		default:
			return 0;
		}
	}

	public bool GetStatus(int _taskId)
	{
		return taskFinishIds.Contains(_taskId);
	}
}
