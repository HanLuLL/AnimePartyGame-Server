using Tools;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class BattleSignal
{
	public readonly Signal<long, int> minimapMove = new Signal<long, int>();

	public readonly Signal minimapUpdate = new Signal();

	public readonly Signal<long> minimapDelete = new Signal<long>();

	public readonly Signal<bool> limitCameraControl = new Signal<bool>();

	public readonly Signal<bool> showActionMask = new Signal<bool>();

	public readonly Signal<int> showLandTip = new Signal<int>();

	public readonly Signal playerInfoRefresh = new Signal();

	public readonly Signal<long> deadDeal = new Signal<long>();

	public readonly Signal mapMissionChange = new Signal();

	public readonly Signal clueChange = new Signal();

	public readonly Signal progressChange = new Signal();

	public readonly Signal CrabHit = new Signal();

	public readonly Signal<int, bool, Transform> ShowTimelineEffect = new Signal<int, bool, Transform>();

	public readonly Signal<int> HideTimelineEffect = new Signal<int>();

	public readonly Signal<long, int, int, bool> ThrowDice = new Signal<long, int, int, bool>();

	public readonly Signal<int> pveProgressMultiple = new Signal<int>();

	public readonly Signal<HeroHpChangeS2C, bool> playerHpChange = new Signal<HeroHpChangeS2C, bool>();

	public readonly Signal curPlayerOperate = new Signal();
}
