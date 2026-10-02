using Tools;
using party.model;

namespace GameLogic;

public class ComebackSignal
{
	public readonly Signal<ReturnInfo> infoUpdated = new Signal<ReturnInfo>();

	public readonly Signal<bool> freeGiftClaimed = new Signal<bool>();

	public readonly Signal<ReturnSignIn> signInUpdated = new Signal<ReturnSignIn>();

	public readonly Signal<int> surveyStateUpdated = new Signal<int>();
}
