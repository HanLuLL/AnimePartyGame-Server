using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.model;

namespace GameLogic;

public class MapMissionData
{
	public int MissionId;

	public int state;

	public readonly List<MapMissionTargetData> targetsData = new List<MapMissionTargetData>();

	public async UniTask UpdateMission(MapMission mission)
	{
		bool flag = MissionId != 0 && state == 1 && mission.MissionState == MapMission.Types.State.Complete && targetsData.Count > 0;
		state = (int)mission.MissionState;
		MissionId = mission.MissionId;
		targetsData.Clear();
		Type type = Type.GetType($"GameLogic.MapMissionTargetData_{MissionId}");
		foreach (MapMissionTarget target in mission.Targets)
		{
			MapMissionTargetData item = ((!(type != null)) ? new MapMissionTargetData(MissionId, target) : (Activator.CreateInstance(type, MissionId, target) as MapMissionTargetData));
			targetsData.Add(item);
		}
		if (flag && targetsData.Count > 0)
		{
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowPVETaskTip(targetsData[0], 1.5f);
			if (targetsData[0] is IMapMissionFinishDestroyCharacter mapMissionFinishDestroyCharacter)
			{
				mapMissionFinishDestroyCharacter.DestroyCheongsam();
			}
			if (targetsData[0] is IMapMissionSwitchMap mapMissionSwitchMap)
			{
				await mapMissionSwitchMap.StartSwitchMap();
			}
		}
		if (MissionId != 0 && mission.MissionState == MapMission.Types.State.Accept && targetsData.Count > 0 && targetsData[0] is IMapMissionCreateDestroyCharacter mapMissionCreateDestroyCharacter)
		{
			mapMissionCreateDestroyCharacter.DestroyCheongsam();
		}
	}
}
