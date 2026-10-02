using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Unit;

[Serializable]
public class LandGroupManager : Unit
{
	[SerializeField]
	public List<LandGroup> LandGroups = new List<LandGroup>();

	public void SwitchStatus(int groupId, int statusId)
	{
		if (groupId - 1 < 0 || statusId < 0 || LandGroups.Count <= groupId - 1)
		{
			Debug.LogError($"资源组id:{groupId}, 状态ID:{statusId} 查询失败");
		}
		else
		{
			LandGroups[groupId - 1].SwitchStatus(statusId);
		}
	}

	public LandGroup GetLandGroup(int groupId)
	{
		if (groupId - 1 < 0 || LandGroups.Count <= groupId - 1)
		{
			Debug.LogError($"资源组id:{groupId}, 查询失败");
			return null;
		}
		return LandGroups[groupId - 1];
	}
}
