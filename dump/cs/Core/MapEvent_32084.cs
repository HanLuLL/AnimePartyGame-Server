using Core.MapEvents;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;

namespace Core;

public class MapEvent_32084 : MapEvent
{
	public MapEvent_32084()
	{
		StaticConfigure.MapEvent.InfoDict.TryGetValue(32084, out config);
	}

	public override async UniTask MapEventShowByEvent_Front()
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager017 mapGimmickManager)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.pveProgressMultiple.Dispatch(100);
			await mapGimmickManager.SwitchMap(headspace: true);
		}
	}
}
