namespace FairyGUI;

public interface IUISource
{
	string fileName { get; }

	bool loaded { get; set; }

	void Load(UILoadCallback callback);
}
