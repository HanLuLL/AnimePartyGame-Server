using UnityEngine;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Map;

[Preserve]
public class MapMission_10002 : MapMission, IMapMission_Gold
{
	public void UpdateGoldProgress(int deltaGold)
	{
		int num = Mathf.Max(0, deltaGold);
		base._missionProgress += num;
		Game.GetModel<GlobalSignal>().MissionProgressChange.Dispatch(base.MissionProgress, base.MissionTarget);
	}
}
