using System;
using UnityEngine;

namespace Core.Unit;

[Serializable]
public class GimmickData
{
	[SerializeField]
	public int GroupId;

	[SerializeField]
	public int StepSize;

	private void OnCheckGroup(int _GroupId)
	{
		LandGroupManager landGroupManager = UnityEngine.Object.FindObjectOfType<LandGroupManager>();
		if (landGroupManager == null)
		{
			Debug.LogError("当前未找到地图组件LandGroup, 无法校验GroupId的正确性");
		}
		else if (_GroupId <= 0 || _GroupId > landGroupManager.LandGroups.Count)
		{
			Debug.LogError($"校验GroupId:{_GroupId} 无效");
		}
	}
}
