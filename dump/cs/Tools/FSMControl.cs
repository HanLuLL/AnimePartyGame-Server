using System.Collections.Generic;

namespace Tools;

public class FSMControl
{
	private StateBase currentState;

	private readonly Dictionary<int, StateBase> allSaveState = new Dictionary<int, StateBase>();

	public StateBase CurrentState => currentState;

	public bool IsRun => allSaveState.Count > 0;

	public void OnTick()
	{
		currentState?.OnUpdate();
	}

	public void AddState(int stateType, StateBase state)
	{
		if (allSaveState.TryAdd(stateType, state) && state != null)
		{
			state.stateType = stateType;
		}
	}

	public void SetState(int stateType)
	{
		if (currentState == allSaveState[stateType])
		{
			OnTick();
			return;
		}
		currentState?.OnExit();
		allSaveState[stateType].PreState = currentState;
		currentState = allSaveState[stateType];
		currentState?.OnEnter();
	}

	public void Dispose()
	{
		allSaveState?.Clear();
	}
}
