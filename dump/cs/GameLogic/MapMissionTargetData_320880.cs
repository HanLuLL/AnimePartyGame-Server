using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.model;

namespace GameLogic;

public class MapMissionTargetData_320880 : MapMissionTargetData, IMapMissionSwitchMap
{
	public MapMissionTargetData_320880(int _mapMissionId, MapMissionTarget targetData)
		: base(_mapMissionId, targetData)
	{
	}

	public override string GetTitle()
	{
		return base.PVEMissionInfo.MissionDescID.GetLocal(UIStringType.PVEMission);
	}

	public async UniTask StartSwitchMap()
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager017 mapGimmickManager)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.pveProgressMultiple.Dispatch(1);
			await mapGimmickManager.SwitchMap(headspace: false);
		}
	}
}
