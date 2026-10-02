using System;
using SinglePlayer.GamePlay.Character;
using Tools;

namespace SinglePlayer.GamePlay.BuffSystem;

public abstract class BuffBase
{
	private GamePlayManager _gamePlayManager;

	private GameData _gameData;

	public Signal onStart;

	public Signal onEnd;

	public Signal onRemove;

	public BuffConfigure Configure { get; private set; }

	public uint Id => Configure.Id;

	public CharacterLogic Caster { get; private set; }

	public CharacterLogic Target { get; private set; }

	public uint Layer { get; private set; }

	private RoundTimer Timer { get; set; }

	public void Initialize(BuffConfigure configure, CharacterLogic releaser, CharacterLogic target, uint layer)
	{
		_gamePlayManager = Game.GetSystem<GamePlayManager>();
		_gameData = Game.GetModel<GameData>();
		Configure = configure;
		Layer = layer;
		Caster = releaser;
		Target = target;
		onStart = new Signal();
		onEnd = new Signal();
		onRemove = new Signal();
		InitializeTimer();
		_gamePlayManager.Signal.RoundStartBefore.AddListener(OnRoundStartBefore);
		_gamePlayManager.Signal.RoundStart.AddListener(OnRoundStart);
		_gamePlayManager.Signal.RoundStartAfter.AddListener(OnRoundStartAfter);
		_gamePlayManager.Signal.RoundEndBefore.AddListener(OnRoundEndBefore);
		_gamePlayManager.Signal.RoundEnd.AddListener(OnRoundEnd);
		_gamePlayManager.Signal.RoundEndAfter.AddListener(OnRoundEndAfter);
	}

	public void Tick()
	{
		TickTimer();
	}

	public void Dispose()
	{
		_gamePlayManager.Signal.RoundStartBefore.RemoveListener(OnRoundStartBefore);
		_gamePlayManager.Signal.RoundStart.RemoveListener(OnRoundStart);
		_gamePlayManager.Signal.RoundStartAfter.RemoveListener(OnRoundStartAfter);
		_gamePlayManager.Signal.RoundEndBefore.RemoveListener(OnRoundEndBefore);
		_gamePlayManager.Signal.RoundEnd.RemoveListener(OnRoundEnd);
		_gamePlayManager.Signal.RoundEndAfter.RemoveListener(OnRoundEndAfter);
		onStart.RemoveAllListeners();
		onEnd.RemoveAllListeners();
		onRemove.RemoveAllListeners();
		_gamePlayManager = null;
		_gameData = null;
		Configure = null;
		Layer = 0u;
		Caster = null;
		Target = null;
		Timer = null;
	}

	protected virtual void OnStart()
	{
		onStart.Dispatch();
	}

	protected virtual void OnIntervalExecute(uint count)
	{
	}

	protected virtual void OnEnd()
	{
		onEnd.Dispatch();
	}

	public virtual void OnRemove()
	{
		onRemove.Dispatch();
	}

	public virtual void RefreshLayer(uint layer, uint delta)
	{
		Layer = layer;
		switch (Configure.AddType)
		{
		case BuffAddType.Override:
			ReStartTimer();
			break;
		default:
			throw new ArgumentOutOfRangeException();
		case BuffAddType.Stack:
			break;
		}
	}

	protected virtual void OnRoundStartBefore()
	{
		if (Configure.TriggerType == BuffTriggerType.RoundStartBefore)
		{
			Start();
		}
	}

	protected virtual void OnRoundStart()
	{
		if (Configure.TriggerType == BuffTriggerType.RoundStart)
		{
			Start();
		}
	}

	protected virtual void OnRoundStartAfter()
	{
		if (Configure.TriggerType == BuffTriggerType.RoundStartAfter)
		{
			Start();
		}
	}

	protected virtual void OnRoundEndBefore()
	{
		if (Configure.TriggerType == BuffTriggerType.RoundEndBefore)
		{
			Start();
		}
	}

	protected virtual void OnRoundEnd()
	{
		if (Configure.TriggerType == BuffTriggerType.RoundEnd)
		{
			Start();
		}
	}

	protected virtual void OnRoundEndAfter()
	{
		if (Configure.TriggerType == BuffTriggerType.RoundEndAfter)
		{
			Start();
		}
	}

	private void InitializeTimer()
	{
		Timer = new RoundTimer();
		Timer.Delay = Configure.Delay;
		Timer.Duration = Configure.Duration;
		Timer.Interval = Configure.Interval;
		Timer.Start = OnStart;
		Timer.IntervalExecute = OnIntervalExecute;
		Timer.Completed = OnEnd;
		Timer.Cancelled = OnRemove;
	}

	private void TickTimer()
	{
		uint value = (uint)_gameData.Round.Value;
		if (Timer.EndRound <= value)
		{
			OnEnd();
			return;
		}
		if (Timer.StartRound == value)
		{
			OnStart();
		}
		if (Timer.Interval != 0 && Timer.TickCount % Timer.Interval == 0)
		{
			OnIntervalExecute(Timer.TickCount / Timer.Interval + 1);
		}
		Timer.TickCount++;
	}

	private void Start()
	{
		Timer.StartRound = Configure.Delay + (uint)_gameData.Round.Value;
		Timer.EndRound = Timer.StartRound + Configure.Duration;
	}

	private void ReStartTimer()
	{
		Timer.StartRound = (uint)_gameData.Round.Value;
		Timer.EndRound = Timer.StartRound + Configure.Duration;
		Timer.TickCount = 0u;
	}
}
