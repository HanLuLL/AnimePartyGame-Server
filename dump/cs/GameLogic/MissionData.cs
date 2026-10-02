using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class MissionData : BaseTaskData
{
	public enum TaskState
	{
		TaskStateLocked,
		TaskStateUnlocked,
		TaskStateCompleted,
		TaskStateClaimed,
		TaskStateOneCompleted,
		TaskStateOneClaimed
	}

	public MissionDataConfigure config;

	public int Status;

	public readonly Signal<MissionData> StateChange = new Signal<MissionData>();

	public int AchieveProgress;

	public MissionData(MissionDataConfigure configData)
	{
		config = configData;
		UpdateConfigInfo();
	}

	private void UpdateConfigInfo()
	{
		BeginTime = config.BeginTime;
		EndTime = config.EndTime;
		rewards = new List<KeyValuePair<int, int>>(config.Reward.Count);
		foreach (KeyValuePair<int, int> item in config.Reward)
		{
			rewards.Add(new KeyValuePair<int, int>(item.Key, item.Value));
		}
	}

	public void UpdateData(int missionProgress, int status)
	{
		AchieveProgress = missionProgress;
		Status = status;
		StateChange.Dispatch(this);
	}

	public void UpdateStatus(int status)
	{
		Status = status;
		StateChange.Dispatch(this);
	}

	protected override int ConfigID()
	{
		return config.Id;
	}

	protected override bool Running()
	{
		int status = Status;
		if (status != 2)
		{
			return status != 4;
		}
		return false;
	}

	protected override int GetProgress()
	{
		return AchieveProgress;
	}

	public override int GetWay()
	{
		return config.Way;
	}

	protected override int GetTarget()
	{
		return config.ParamProgress;
	}

	protected override bool GetStatus()
	{
		return Status == 3;
	}

	public override int GetOrderWeight()
	{
		return config.OrderWeight;
	}

	public override string GetTaskTitle()
	{
		return config.NameID.GetLocal(UIStringType.Mission);
	}

	public override string GetTaskDesc()
	{
		string local = config.DescId.GetLocal(UIStringType.Mission);
		return FormatString(local, config.ParamProgress, config.ParamKey, config.ParamValue);
	}

	private string FormatString(string template, int progress, RepeatedField<MissionParamType> keys, RepeatedField<int> values)
	{
		object[] array = new object[template.Split('{').Length - 1];
		for (int i = 0; i < array.Length; i++)
		{
			if (keys.Count > i)
			{
				if (values.Count <= i)
				{
					Debug.LogError($"Task:{config.Id} dont use MissionParamType.None param, please check STRMission");
					return "";
				}
				switch (keys[i])
				{
				case MissionParamType.None:
					Debug.LogError($"Task:{config.Id} dont use MissionParamType.None param, please check STRMission");
					return "";
				case MissionParamType.Role:
				case MissionParamType.Unit:
					array[i] = CharacterHandle.GetCharacterName(values[i]);
					break;
				case MissionParamType.GameMode:
					array[i] = values[i].GetGameModeInfoConfigure().NameID.GetLocal(UIStringType.GameMode);
					break;
				case MissionParamType.Map:
					array[i] = values[i].GetMapDataConfigure().MapName.GetLocal(UIStringType.Map);
					break;
				case MissionParamType.DifficultyFrom:
				case MissionParamType.DifficultyEqual:
					array[i] = values[i].GetChoosingTimeLimitDifficultyConfigure().DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit);
					break;
				case MissionParamType.GameRank:
				case MissionParamType.UseCandyCount:
					array[i] = values[i];
					break;
				case MissionParamType.Item:
					array[i] = values[i].GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item);
					break;
				case MissionParamType.LevelPve:
					array[i] = values[i];
					break;
				case MissionParamType.SignalPlayerLevel:
					array[i] = values[i].GetSignalPlayerDataConfigure().NameID.GetLocal(UIStringType.SinglePlayer);
					break;
				}
				continue;
			}
			array[i] = progress;
			break;
		}
		return string.Format(template, array);
	}

	public override TaskRefreshType GetTaskRefreshType()
	{
		return config.TaskRefreshType;
	}

	public override string GetTaskRefreshTypeLocal()
	{
		return config.TaskRefreshType switch
		{
			TaskRefreshType.None => null, 
			TaskRefreshType.Daily => 101.GetLocal(UIStringType.Mission), 
			TaskRefreshType.Weekly => 102.GetLocal(UIStringType.Mission), 
			TaskRefreshType.Monthly => 103.GetLocal(UIStringType.Mission), 
			_ => null, 
		};
	}
}
