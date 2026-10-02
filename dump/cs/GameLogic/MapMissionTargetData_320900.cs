using Core.Unit;
using Tools;
using party.model;

namespace GameLogic;

public class MapMissionTargetData_320900 : MapMissionTargetData, IMapMissionCreateDestroyCharacter
{
	public MapMissionTargetData_320900(int _mapMissionId, MapMissionTarget targetData)
		: base(_mapMissionId, targetData)
	{
	}

	public void DestroyCheongsam()
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager017 mapGimmickManager)
		{
			mapGimmickManager.DestroyCheongsam();
		}
	}
}
