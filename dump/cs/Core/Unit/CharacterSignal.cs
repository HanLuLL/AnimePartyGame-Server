using Tools;

namespace Core.Unit;

public class CharacterSignal
{
	public readonly Signal<bool, string> statusIcon = new Signal<bool, string>();

	public readonly Signal<(int, int, int, int), string> attrChange = new Signal<(int, int, int, int), string>();
}
