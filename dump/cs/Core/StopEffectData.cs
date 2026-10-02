using System;
using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;

namespace Core;

public class StopEffectData : IEquatable<StopEffectData>
{
	public readonly int Terminal;

	public int TotalStep;

	private WalkStopEffect WalkStopEffect;

	private List<RoadLineData> PathsData;

	public StopEffectData(int terminal)
	{
		Terminal = terminal;
	}

	public async UniTask CreateEffect(int slot, int totalStep)
	{
		TotalStep = totalStep;
		UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(Terminal);
		EffectInfoConfigure effectDataConfigure = 36.GetEffectDataConfigure();
		WalkStopEffect = (await SimpleSingletonProvider<EffectManager>.inst.PlayByName(effectDataConfigure.EffectName, Vector3.zero, Quaternion.identity, landById.transform)) as WalkStopEffect;
		if (WalkStopEffect != null)
		{
			WalkStopEffect.ReadyRoadLineData(slot, this);
		}
	}

	public void DestroyEffect()
	{
		CloseAllDirections();
		if (WalkStopEffect != null)
		{
			WalkStopEffect.ReleaseEffect();
		}
	}

	public void CloseAllDirections()
	{
		if (PathsData != null)
		{
			for (int i = 0; i < PathsData.Count; i++)
			{
				PathsData[i].DestroyDirection();
			}
		}
		if (WalkStopEffect != null)
		{
			WalkStopEffect.CloseAllDirections();
		}
	}

	public async UniTask TryShowTerminalPath()
	{
		if (WalkStopEffect != null)
		{
			await WalkStopEffect.TryAgainShowDirections();
		}
	}

	public async UniTask ShowTerminalPath()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.battle.curPlayerId != SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID())
		{
			return;
		}
		PathsData = SimpleSingletonProvider<RoadLineManager>.inst.GetTargetPathsByTerminal(Terminal);
		EffectInfoConfigure RoadLine_1 = 2002.GetEffectDataConfigure();
		for (int i = 0; i < PathsData.Count; i++)
		{
			RoadLineData preLines = PathsData[i].PreLines;
			List<int> path = PathsData[i].Path;
			if (await PathsData[i].InstantiateDirection(RoadLine_1.EffectName) == null)
			{
				break;
			}
			if (preLines == null && path != null && path.Count > 1)
			{
				SimpleSingletonProvider<MoveArrowManager>.inst.ShowArrowEffect(path[0], path[1]);
			}
		}
	}

	public bool Equals(StopEffectData other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		return Terminal == other.Terminal;
	}
}
