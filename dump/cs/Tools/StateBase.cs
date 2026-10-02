namespace Tools;

public abstract class StateBase
{
	public int stateType;

	public StateBase PreState;

	public abstract void OnEnter();

	public abstract void OnUpdate();

	public abstract void OnExit();
}
