namespace Core.Unit;

public interface IMoveAnimRule
{
	bool ShouldStopMoveAnim();

	string GetWalkAnimeName(string defaultAnimeName = "Walk")
	{
		return defaultAnimeName;
	}

	string GetWalkBackAnimeName(string defaultAnimeName = "Walk-Back")
	{
		return defaultAnimeName;
	}

	bool KeepMoveAnim()
	{
		return false;
	}
}
