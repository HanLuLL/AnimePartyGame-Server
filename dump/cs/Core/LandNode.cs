using System;
using System.Collections.Generic;
using Core.Unit;
using UnityEngine;

namespace Core;

[Serializable]
public class LandNode
{
	[SerializeField]
	private int _id;

	[SerializeField]
	private LandType _landType;

	[SerializeField]
	private int _initDirLandId;

	[SerializeField]
	private int _playerSerialNumber;

	[SerializeField]
	private int _JumpNodeId;

	[SerializeField]
	private List<GimmickData> _GimmickData;

	[SerializeField]
	private List<int> _neighborLandIds;

	public int id => _id;

	public LandType landType => _landType;

	public int initDirLandId => _initDirLandId;

	public int playerSerialNumber => _playerSerialNumber;

	public int jumpNodeId => _JumpNodeId;

	public List<GimmickData> gimmickData => _GimmickData;

	public List<int> neighborLandIds => _neighborLandIds;
}
