using Core.MapEvents;
using Cysharp.Threading.Tasks;
using party.protocol;

namespace Core;

public class MapEvent_31003 : MapEvent
{
	public MapEvent_31003()
	{
		StaticConfigure.MapEvent.InfoDict.TryGetValue(31003, out config);
	}

	public override async UniTask MapEventShowByAttr(UpdateHeroAttrS2C model)
	{
		await TrainManager.inst.ActiveTrain(model);
	}
}
