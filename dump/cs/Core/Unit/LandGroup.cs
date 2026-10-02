using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Unit;

[Serializable]
public class LandGroup
{
	[SerializeField]
	public int LandGroupId;

	[SerializeField]
	public List<GroupStatusData> LandStatus = new List<GroupStatusData>();

	[SerializeField]
	public List<UnitLand> Lands = new List<UnitLand>();

	public void SwitchStatus(int statusId)
	{
		if (LandStatus.Count <= statusId)
		{
			return;
		}
		foreach (UnitLand land in Lands)
		{
			land.SwitchStatus(LandStatus[statusId]);
		}
	}
}
