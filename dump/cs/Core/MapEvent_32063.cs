using Core.MapEvents;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;

namespace Core;

public class MapEvent_32063 : MapEvent
{
	public MapEvent_32063()
	{
		StaticConfigure.MapEvent.InfoDict.TryGetValue(32063, out config);
	}

	public override async UniTask MapEventShowByEvent()
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager016 mapGimmickManager)
		{
			await mapGimmickManager.TryPlayBGM();
		}
	}
}
