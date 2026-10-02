using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using party.protocol;

namespace Core.Tutorial;

public abstract class BaseMapMission
{
	protected PVEMissionInfoConfigure _missionInfo;

	protected MapMissionNotifyS2C _mapMissionNotifyS2C;

	public abstract UniTask TryActiveMapMission();

	protected virtual async UniTask SendMapMissionData()
	{
		await MonoSingletonProvider<NetManager>.inst.RPC.MapMissionNotifyS2C.OnMapMissionNotifyS2CServerCallBackAsync(_mapMissionNotifyS2C, 0, isDispatch: true);
	}

	public virtual UniTask GrantRewards()
	{
		return UniTask.CompletedTask;
	}
}
