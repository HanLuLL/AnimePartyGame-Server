using Core.MapEvents;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using party.protocol;

namespace Core;

public class MapEvent_31006 : MapEvent
{
	public MapEvent_31006()
	{
		StaticConfigure.MapEvent.InfoDict.TryGetValue(31006, out config);
	}

	public override async UniTask MapEventShowByAttr(UpdateHeroAttrS2C model)
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager015 mapGimmickManager)
		{
			await mapGimmickManager.ActiveCrab();
		}
	}
}
