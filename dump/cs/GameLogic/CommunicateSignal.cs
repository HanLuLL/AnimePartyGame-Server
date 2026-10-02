using Tools;
using UI;

namespace GameLogic;

public class CommunicateSignal
{
	public readonly Signal<long, int> shortChat = new Signal<long, int>();

	public readonly Signal<long, bool> newMessage = new Signal<long, bool>();

	public readonly Signal<BattleMessage> battleMessage = new Signal<BattleMessage>();

	public readonly Signal newExpression = new Signal();
}
