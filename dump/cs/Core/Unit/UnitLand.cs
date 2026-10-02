using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace Core.Unit;

[Serializable]
public class UnitLand : Unit
{
	[SerializeField]
	private LandNode nodeInfo;

	private List<int> _AdjacencyLandIds = new List<int>();

	[Header("RunTime")]
	[Space(8f)]
	public LandType _RunTimeLandType;

	public bool Disable;

	private bool flashState;

	private RoomPlayer playerInfo;

	private HastenRoadManager HastenRoadManager;

	private int HastenRoadNodeId = -1;

	public int Id => nodeInfo.id;

	public LandType LandType
	{
		get
		{
			if (_RunTimeLandType != LandType.None)
			{
				return _RunTimeLandType;
			}
			return nodeInfo.landType;
		}
	}

	public int PlayerSerialNumber => nodeInfo.playerSerialNumber;

	public List<int> AdjacencyLandIds
	{
		get
		{
			_AdjacencyLandIds.Clear();
			for (int i = 0; i < nodeInfo.neighborLandIds.Count; i++)
			{
				int num = nodeInfo.neighborLandIds[i];
				UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(num);
				if ((object)landById != null && !landById.Disable)
				{
					_AdjacencyLandIds.Add(num);
				}
			}
			return _AdjacencyLandIds;
		}
	}

	public float X => base.transform.position.x;

	public float Y => base.transform.position.y;

	public float Z => base.transform.position.z;

	public float LocalX => base.transform.localPosition.x;

	public float LocalY => base.transform.localPosition.y;

	public float LocalZ => base.transform.localPosition.z;

	private PlatformFrame _PlatformFrame => base.transform.GetComponentInChildren<PlatformFrame>();

	public LandNode GetNodeInfo()
	{
		return nodeInfo;
	}

	public void SwitchStatus(GroupStatusData status)
	{
		_RunTimeLandType = status.LandType;
		Disable = !status.Active;
	}

	public bool IsSelectedDir()
	{
		return AdjacencyLandIds.Count > 2;
	}

	public int GetFromLandId(RepeatedField<int> FrontIds)
	{
		foreach (int adjacencyLandId in AdjacencyLandIds)
		{
			if (!FrontIds.Contains(adjacencyLandId))
			{
				return adjacencyLandId;
			}
		}
		return -1;
	}

	public int GetNextLandId(int fromLandId)
	{
		foreach (int adjacencyLandId in AdjacencyLandIds)
		{
			if (adjacencyLandId != fromLandId)
			{
				return adjacencyLandId;
			}
		}
		return -1;
	}

	public int GetNextLandId(List<int> _frontNodeIds)
	{
		foreach (int adjacencyLandId in AdjacencyLandIds)
		{
			if (!_frontNodeIds.Contains(adjacencyLandId))
			{
				return adjacencyLandId;
			}
		}
		return 0;
	}

	public List<int> CanSelectedLandId(int fromLandId)
	{
		List<int> list = new List<int>();
		list.AddRange(AdjacencyLandIds);
		for (int num = list.Count; num > 0; num--)
		{
			int index = num - 1;
			if (list[index] == fromLandId)
			{
				list.RemoveAt(index);
			}
		}
		return list;
	}

	public bool IsNeedStop()
	{
		LandType landType = LandType;
		return landType == LandType.Born || landType == LandType.FillingStation || landType == LandType.Shop || landType == LandType.Pveshop || landType == LandType.Gift || landType == LandType.Relic;
	}

	public void ActiveNeighborLand()
	{
		foreach (int neighborLandId in nodeInfo.neighborLandIds)
		{
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(neighborLandId);
			if (landById != null)
			{
				landById.Disable = false;
			}
		}
	}

	public void LandShowFlash()
	{
		if (!flashState)
		{
			_PlatformFrame.SetStampIntensity(enable: true);
			flashState = true;
		}
	}

	public void LandCloseFlash()
	{
		if (flashState)
		{
			_PlatformFrame.SetStampIntensity(enable: false);
			flashState = false;
		}
	}

	public void InitBornMaterial(RoomPlayer _playerInfo, bool init)
	{
		playerInfo = _playerInfo;
		string landTexName = playerInfo.characterConfig?.LandTex;
		if (HackerConfig.IsValid())
		{
			SimpleSingletonProvider<CharacterAssetManager>.inst.TryGetLandTexWithGM(ref landTexName, playerInfo.standingPainting);
		}
		if (!string.IsNullOrEmpty(landTexName))
		{
			Texture texture = SimpleSingletonProvider<CharacterAssetManager>.inst.GetTexture(landTexName);
			if (!(texture == null))
			{
				_PlatformFrame.SetStampTex(texture);
				_PlatformFrame.SetStampEmissionTex(texture);
				flashState = init;
			}
		}
	}

	public string GetLandIcon()
	{
		if (LandType == LandType.Born)
		{
			if (playerInfo == null)
			{
				return StaticConfigure.Land.InfoDict[2].LandIcon;
			}
			return playerInfo.standingPainting.LandIcon;
		}
		return StaticConfigure.Land.InfoDict[(int)LandType].LandIcon;
	}

	public void RefreshDisplayByGimmick()
	{
		RepeatedField<int> platformMainTexOffset = StaticConfigure.Land.InfoDict[(int)LandType].PlatformMainTexOffset;
		_PlatformFrame.SetStampUV(platformMainTexOffset[0], platformMainTexOffset[1]);
	}

	public void RegisterHastenRoad(HastenRoadManager _hastenRoadManager, int _hastenRoadNodeId)
	{
		HastenRoadManager = _hastenRoadManager;
		HastenRoadNodeId = _hastenRoadNodeId;
	}

	public async UniTask<bool> EnableHastenRoadManager(Character _owner, UnitLand targetLand)
	{
		if (targetLand.Id != HastenRoadNodeId)
		{
			return false;
		}
		if (HastenRoadManager == null)
		{
			HastenRoadManager.MovingTransform = null;
			return false;
		}
		Vector3 targetDir = targetLand.transform.localPosition - base.transform.localPosition;
		HastenRoadManager.MovingTransform = _owner.transform;
		return await HastenRoadManager.Turn(targetDir);
	}

	public async UniTask<Effect> PlayByName(string effectName, Vector3 position, Quaternion rotation, Action onComplete = null, float scale = 1f)
	{
		return await SimpleSingletonProvider<EffectManager>.inst.PlayByName(effectName, position, rotation, base.transform, onComplete, scale);
	}

	public async UniTask<Effect> PlayById(int effectId, Vector3 position, Quaternion rotation, Action onComplete = null, float scale = 1f)
	{
		return await SimpleSingletonProvider<EffectManager>.inst.PlayById(effectId, position, rotation, base.transform, onComplete, scale);
	}
}
