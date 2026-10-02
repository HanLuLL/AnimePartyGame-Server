using Core.MapEvents;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;

namespace Core;

public class MapEvent_35071 : MapEvent
{
	private const int enventBgmId = 175;

	public MapEvent_35071()
	{
		StaticConfigure.MapEvent.InfoDict.TryGetValue(35071, out config);
	}

	public override async UniTask MapEventShowByEvent()
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager020 mapGimmickManager)
		{
			mapGimmickManager.TryPlayBGM(175).Forget();
			mapGimmickManager.SwitchEffect(MapGimmickManager020.EffectType.Term).Forget();
		}
		await UniTask.CompletedTask;
	}
}
