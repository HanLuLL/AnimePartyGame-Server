using System;
using UnityEngine;

namespace Core.Unit;

[Serializable]
public class GroupStatusData
{
	[SerializeField]
	public int StatusId;

	[SerializeField]
	public bool Active;

	[SerializeField]
	public LandType LandType;
}
