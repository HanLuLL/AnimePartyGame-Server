using Core.Unit;
using Tools;
using party.model;

namespace GameLogic;

public class MapMissionTargetData_320830 : MapMissionTargetData, IMapMissionFinishDestroyCharacter
{
	public MapMissionTargetData_320830(int _mapMissionId, MapMissionTarget targetData)
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
