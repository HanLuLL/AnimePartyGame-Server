using System;
using UnityEngine;

namespace Core;

[Serializable]
public class MapRangeManage : MonoBehaviour
{
	[SerializeField]
	private Transform _MoveCenter;

	[SerializeField]
	private Transform _MoveFarthest;

	[SerializeField]
	private Transform _MiniMapCenter;

	[SerializeField]
	private Transform _MiniMapFarthest;

	public Vector3 moveCenter => _MoveCenter.position;

	public Vector3 moveFarthest => _MoveFarthest.position;

	public Vector3 miniMapCenter => _MiniMapCenter.position;

	public Vector3 miniMapFarthest => _MiniMapFarthest.position;
}
