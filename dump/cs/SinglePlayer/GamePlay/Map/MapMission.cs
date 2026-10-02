using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Map;

public abstract class MapMission
{
	public int LevelId;

	private MapMissionStatus _status;

	private Int32Encryptor _missionProgressEncryptor = new Int32Encryptor();

	private Int32Encryptor deadlineEncryptor = new Int32Encryptor();

	private SinglePlayerLevelProgressConfigureItem _missionConfig;

	public int Id { get; private set; }

	public MapMissionStatus Status
	{
		get
		{
			return _status;
		}
		private set
		{
			if (_status != value)
			{
				_status = value;
				Game.GetModel<GlobalSignal>().MissionStatusChange.Dispatch((int)_status);
			}
		}
	}

	protected int _missionProgress
	{
		get
		{
			return _missionProgressEncryptor.DecryptGet();
		}
		set
		{
			_missionProgressEncryptor.EncryptSet(value);
		}
	}

	public int MissionProgress => _missionProgress;

	public int MissionTarget { get; protected set; }

	public int Deadline
	{
		get
		{
			return deadlineEncryptor.DecryptGet();
		}
		protected set
		{
			deadlineEncryptor.EncryptSet(value);
		}
	}

	public SinglePlayerLevelProgressConfigureItem MissionConfig
	{
		get
		{
			return _missionConfig;
		}
		private set
		{
			_missionConfig = value;
			Id = MissionConfig.MissionIndex;
			MissionTarget = _missionConfig.NeedMoney;
			Deadline = _missionConfig.ProgressValue;
		}
	}

	public virtual async UniTask StartMission()
	{
		if (_status != MapMissionStatus.None)
		{
			Debug.LogError($"关卡{LevelId} 第{Id}个 任务处于{_status}， 不应该再触发任务开始");
			return;
		}
		Status = MapMissionStatus.Running;
		Game.GetModel<GlobalSignal>().MissionStart.Dispatch();
		await UniTask.CompletedTask;
	}

	public virtual MapMissionStatus GetStatus()
	{
		if (Game.GetModel<GameData>().GameProgress.Value >= Deadline)
		{
			if (MissionProgress < MissionTarget)
			{
				return MapMissionStatus.Failure;
			}
			return MapMissionStatus.Success;
		}
		return Status;
	}

	public virtual MapMissionStatus SettleMissionStatus()
	{
		if (Game.GetModel<GameData>().GameProgress.Value >= Deadline)
		{
			Status = ((MissionProgress >= MissionTarget) ? MapMissionStatus.Success : MapMissionStatus.Failure);
		}
		return Status;
	}

	public void Initialize(int levelId, SinglePlayerLevelProgressConfigureItem item, bool isFinish)
	{
		Id = levelId;
		MissionConfig = item;
		if (isFinish)
		{
			_status = MapMissionStatus.Success;
			_missionProgress = MissionTarget;
		}
	}

	public void InitializeProgress(int missionProgress)
	{
		_missionProgress = missionProgress;
	}

	public void GMSkipProgress(int gmProgress)
	{
		if (GMConfig._Enable && gmProgress > Deadline)
		{
			_status = MapMissionStatus.Success;
		}
	}
}
