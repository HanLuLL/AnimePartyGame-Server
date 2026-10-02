using System;
using System.Collections.Generic;
using UnityEngine;

namespace SinglePlayer.GamePlay.Build;

public class BuildingControllerFSM
{
	private readonly Dictionary<Type, IState> _operateStateDict = new Dictionary<Type, IState>();

	public IState CurrentState { get; private set; }

	public BuildingControllerFSM()
	{
		AddState<IdleState>();
		AddState<PressDownState>();
		AddState<DraggingBuildingState>();
		AddState<DraggingCardState>();
		AddState<DraggingUpState>();
		AddState<PressUpState>();
		foreach (KeyValuePair<Type, IState> item in _operateStateDict)
		{
			item.Value.Init();
		}
	}

	public void AddState<T>() where T : IState, new()
	{
		_operateStateDict.Add(typeof(T), new T());
	}

	public void Start<T>() where T : IState
	{
		IState state = GetState<T>();
		if (state == null)
		{
			Debug.LogError($"未找到状态类型为{typeof(T)}的状态");
			return;
		}
		CurrentState = state;
		CurrentState.Enter();
	}

	public void ChangeState<T>() where T : IState
	{
		if (CurrentState == null)
		{
			Debug.LogError($"当前状态为空，无法切换到状态类型为{typeof(T)}的状态");
			return;
		}
		IState state = GetState<T>();
		if (state == null)
		{
			Debug.LogError($"未找到状态类型为{typeof(T)}的状态");
		}
		else if (!(CurrentState.GetType() == typeof(T)))
		{
			CurrentState.Exit();
			CurrentState = state;
			CurrentState.Enter();
		}
	}

	public IState GetState<T>() where T : IState
	{
		if (!_operateStateDict.TryGetValue(typeof(T), out var value))
		{
			Debug.LogError($"未找到状态类型为{typeof(T)}的状态");
			return null;
		}
		return value;
	}
}
