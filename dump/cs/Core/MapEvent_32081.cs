using Core.MapEvents;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;

namespace Core;

public class MapEvent_32081 : MapEvent
{
	public MapEvent_32081()
	{
		StaticConfigure.MapEvent.InfoDict.TryGetValue(32081, out config);
	}

	public override async UniTask MapEventShowByEvent_Front()
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager017 mapGimmickManager)
		{
			mapGimmickManager.TryPlayBGM(170).Forget();
			await UniTask.CompletedTask;
		}
	}
}
