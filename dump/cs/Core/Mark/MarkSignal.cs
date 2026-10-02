using Tools;

namespace Core.Mark;

public class MarkSignal
{
	public readonly Signal MarkEnter = new Signal();

	public readonly Signal MarkExit = new Signal();

	public readonly Signal MarkHoverEnter = new Signal();

	public readonly Signal MarkHoverExit = new Signal();

	public readonly Signal ShowWaiting = new Signal();

	public readonly Signal HideWaiting = new Signal();

	public readonly Signal<string> ShowPreview = new Signal<string>();
}
