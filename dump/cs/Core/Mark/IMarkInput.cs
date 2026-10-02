namespace Core.Mark;

public interface IMarkInput
{
	void EnterMark();

	void ExitMark();

	void Update();

	void ToggleMode();
}
