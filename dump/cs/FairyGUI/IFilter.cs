namespace FairyGUI;

public interface IFilter
{
	DisplayObject target { get; set; }

	void Update();

	void Dispose();
}
