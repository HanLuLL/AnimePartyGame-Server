using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Build;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Map;

public class Land : MonoBehaviour
{
	[SerializeField]
	private int _id;

	[SerializeField]
	private SinglePlayerLandType _landType;

	[SerializeField]
	private List<int> _neighborLandIds;

	[SerializeField]
	private int _buildingFoundationId;

	[SerializeField]
	public Transform LandGRoot;

	public DiceLandComponent landComponent;

	private Animator _animator;

	private readonly string _gRootJump = "GRoot";

	private readonly int _gRootJumpId = Animator.StringToHash("GRoot");

	private readonly Dictionary<string, float> _animationClipLengthDict = new Dictionary<string, float>();

	public int Id => _id;

	public SinglePlayerLandType LandType => _landType;

	public List<int> NeighborLandIds => _neighborLandIds;

	public int BuildingFoundationId => _buildingFoundationId;

	public List<int> GetNextLandIds(int preLandId)
	{
		return (from x in NeighborLandIds
			where x != preLandId
			select (x)).ToList();
	}

	public void Initialize(Type type)
	{
		if (type != null)
		{
			landComponent = (DiceLandComponent)Activator.CreateInstance(type, _id);
		}
		_animator = GetComponentInChildren<Animator>();
		InitAnimationClipData();
	}

	private void InitAnimationClipData()
	{
		if ((UnityEngine.Object)(object)_animator == null || (UnityEngine.Object)(object)_animator.runtimeAnimatorController == null)
		{
			Debug.LogError("UnitView " + base.gameObject.name + " 不存在 Animator组件");
			return;
		}
		AnimationClip[] animationClips = _animator.runtimeAnimatorController.animationClips;
		foreach (AnimationClip val in animationClips)
		{
			_animationClipLengthDict.Add(((UnityEngine.Object)(object)val).name, val.length);
		}
	}

	protected float GetAnimationClipLength(string animationName)
	{
		if (string.IsNullOrEmpty(animationName))
		{
			return 0f;
		}
		return _animationClipLengthDict.GetValueOrDefault(animationName, 0f);
	}

	public async UniTask PlayStepOnEffect(bool wait = false)
	{
		_animator.SetTrigger(_gRootJumpId);
		SimpleSingletonProvider<EffectManager>.inst.PlayById(3002, Vector3.zero, Quaternion.identity, ((Component)(object)_animator).transform).Forget();
		if (wait)
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay((int)(GetAnimationClipLength(_gRootJump) * 1000f));
		}
	}

	public void PlayPassStayEffect()
	{
		Vector3 position = Game.GetSystem<BoardManager>().characterManager.Hero.view.transform.position;
		SimpleSingletonProvider<EffectManager>.inst.PlayById(3009, position, Quaternion.identity).Forget();
	}

	public bool IsSpecialLand()
	{
		BuildingFoundation buildingFoundationById = Game.GetModel<GameData>().MapData.GetBuildingFoundationById(BuildingFoundationId);
		if (buildingFoundationById == null)
		{
			return false;
		}
		return buildingFoundationById.Special;
	}

	public BuildingBase GetBuilding()
	{
		if (Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingByFoundationId(BuildingFoundationId, out var building))
		{
			return building;
		}
		return null;
	}
}
