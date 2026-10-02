namespace FairyGUI;

public interface IAnimationGear
{
	bool playing { get; set; }

	int frame { get; set; }

	float timeScale { get; set; }

	bool ignoreEngineTimeScale { get; set; }

	void Advance(float time);
}
