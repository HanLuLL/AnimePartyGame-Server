using Core.Unit;
using Tools;

namespace GameLogic;

public class LandSignal
{
	public readonly Signal<UnitLand> overLand = new Signal<UnitLand>();

	public readonly Signal<UnitLand> chooseLand = new Signal<UnitLand>();
}
