using System;
using System.Collections.Generic;
using Core.Net;
using Google.Protobuf.WellKnownTypes;
using Tools;

namespace GameLogic;

public abstract class BaseTaskData
{
	public List<KeyValuePair<int, int>> rewards;

	public Timestamp BeginTime;

	public Timestamp EndTime;

	public int _Id => ConfigID();

	public int _Progress => GetProgress();

	public bool _FinishStatus => GetStatus();

	public bool TaskRunning => Running();

	public int TaskTarget => GetTarget();

	protected abstract int ConfigID();

	protected abstract bool Running();

	protected abstract int GetProgress();

	protected abstract bool GetStatus();

	public abstract int GetOrderWeight();

	public abstract int GetWay();

	protected abstract int GetTarget();

	public abstract TaskRefreshType GetTaskRefreshType();

	public abstract string GetTaskRefreshTypeLocal();

	public virtual bool ValidityTime()
	{
		return TimeHelper.ValidityTime(BeginTime, EndTime);
	}

	public virtual string GetTime()
	{
		if ((object)EndTime == null)
		{
			return "";
		}
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		DateTime endTime = EndTime.ToDateTime();
		return TimeHelper.RefreshTimeText(1033, 1034, serverTime, endTime);
	}

	public virtual string GetTaskTitle()
	{
		return "";
	}

	public virtual string GetTaskDesc()
	{
		return "";
	}
}
