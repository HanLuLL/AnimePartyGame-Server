using Tools;
using party.model;

namespace GameLogic;

public class RoomSignal
{
	public readonly Signal roomPlayerChange = new Signal();

	public readonly Signal HeroLoadReady = new Signal();

	public readonly Signal UpdateHeroProgress = new Signal();

	public readonly Signal<HeroBarBox, bool> refreshHeroList = new Signal<HeroBarBox, bool>();

	public readonly Signal<int, long, bool> SureHero = new Signal<int, long, bool>();

	public readonly Signal<long, bool> SureSkin = new Signal<long, bool>();

	public readonly Signal masterChange = new Signal();

	public readonly Signal roomSettingRefresh = new Signal();

	public readonly Signal applyChangeSlot = new Signal();

	public readonly Signal<int> roundChange = new Signal<int>();
}
