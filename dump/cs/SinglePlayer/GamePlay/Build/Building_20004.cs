using UnityEngine;
using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Build;

[Preserve]
public class Building_20004 : BuildingBase, IMapMissionEffect
{
	protected override void OnCreate()
	{
		base.OnCreate();
		Game.GetModel<GlobalSignal>().MissionStatusChange.AddListener(MissionStatusChange);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Game.GetModel<GlobalSignal>().MissionStatusChange.RemoveListener(MissionStatusChange);
	}

	protected override void OnDice(int count)
	{
		base.OnDice(count);
		this.ChangeGold(GetGoldMultiValue(0, ParameterType.ThrowDice), BuildingShowType.ThrowDice);
	}

	private void MissionStatusChange(int status)
	{
		if (status == 2)
		{
			Debug.Log($"建筑物：{base.Id} 因为任务成功 获得了增益");
			AddMissionBonusStats();
		}
	}

	public void AddMissionBonusStats()
	{
		ChangeOperateBonus(OperateType.ThrowDiceGold, GetConfigParam(ParameterType.Other, 0));
	}
}
