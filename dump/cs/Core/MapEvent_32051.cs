using Core.MapEvents;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;

namespace Core;

public class MapEvent_32051 : MapEvent
{
	public MapEvent_32051()
	{
		StaticConfigure.MapEvent.InfoDict.TryGetValue(32051, out config);
	}

	public override async UniTask MapEventShowByEvent()
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager015 mapGimmickManager)
		{
			mapGimmickManager.PlayCrabDown();
		}
		await UniTask.CompletedTask;
	}
}
