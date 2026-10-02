using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;

namespace Core;

public class Summon
{
	public readonly UnitLand _land;

	public readonly SummonInfoConfigure trapConfig;

	private readonly LandBuffData _landBuffData;

	private Effect _effectObject;

	public SummonBase summonInstance;

	public Summon(int _summonId)
	{
		_land = null;
		trapConfig = _summonId.GetSummonDataConfigure();
		if (!StaticConfigure.Summon.InfoDict.TryGetValue(_summonId, out trapConfig))
		{
			Debug.Log("没有找到召唤物Id");
		}
	}

	public Summon(int _summonId, LandBuffData landBuffData)
	{
		_landBuffData = landBuffData;
		_land = SimpleSingletonProvider<LandManager>.inst.GetLandById(landBuffData.LandId);
		trapConfig = _summonId.GetSummonDataConfigure();
	}

	public async UniTask SetSummonObj()
	{
		Vector3 pos = new Vector3(_land.LocalX, GetSummonHeight(), _land.LocalZ);
		UniTask task = ((trapConfig.SummonType != SummonType.ControllableUnit) ? InitEffectData(trapConfig, _land.transform, pos) : InitSummonObj(trapConfig, _land.transform, pos));
		if (SimpleSingletonProvider<GameLogicManager>.inst.InitShowTime)
		{
			await task;
		}
		else
		{
			task.Forget();
		}
	}

	public async UniTask SetSummonObj(int index, long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById != null)
		{
			Character characterInst = playerDataById.CharacterInst;
			Vector3 pos = characterInst.EffectContainer.position + Vector3.up * (GetSummonHeight() + (float)(10 * index));
			await InitEffectData(trapConfig, characterInst.EffectContainer, pos);
		}
	}

	private float GetSummonHeight()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return 0f;
		}
		return curRoomInfo.SceneConfig?.SummonHeight ?? 0f;
	}

	private async UniTask InitEffectData(SummonInfoConfigure config, Transform parent, Vector3 _pos, string _Version = "01")
	{
		_effectObject = await SimpleSingletonProvider<EffectManager>.inst.PlayById(config.BornEffect, Vector3.zero, Quaternion.identity, parent);
		if (_effectObject != null)
		{
			_effectObject.transform.position = _pos;
		}
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
	}

	public void CloseSummon()
	{
		if (_effectObject != null)
		{
			_effectObject.ReleaseEffect();
		}
		if (summonInstance != null)
		{
			summonInstance.ReleaseSummon();
			summonInstance = null;
		}
	}

	public async UniTask MoveToRole(Vector3 target)
	{
		if (_effectObject == null || !_effectObject.gameObject.activeSelf)
		{
			return;
		}
		Vector3 startPos = _effectObject.transform.position;
		Vector3 midPos = GetMiddlePosition(startPos, target);
		float percent = 0f;
		float percentSpeed = 100f / (target - startPos).magnitude;
		while (_effectObject != null && _effectObject.gameObject.activeSelf)
		{
			percent += percentSpeed * Time.deltaTime;
			_effectObject.transform.position = UnityVector.Bezier(percent, startPos, midPos, target);
			if (percent >= 1f)
			{
				CloseSummon();
			}
			await UniTask.NextFrame();
		}
	}

	private Vector3 GetMiddlePosition(Vector3 a, Vector3 b)
	{
		Vector3 vector = Vector3.Lerp(a, b, 0.1f);
		Vector3 normalized = Vector3.Cross(a, b).normalized;
		float num = UnityEngine.Random.Range(0f, 2f);
		float num2 = 0.3f;
		return vector + (a - b).magnitude * num * num2 * normalized;
	}

	public Transform GetEffectTransform()
	{
		if (!(_effectObject == null))
		{
			return _effectObject.transform;
		}
		return null;
	}

	private async UniTask InitSummonObj(SummonInfoConfigure config, Transform parent, Vector3 pos)
	{
		if (!(summonInstance != null) && config != null && !(parent == null))
		{
			summonInstance = await SimpleSingletonProvider<SummonManager>.inst.CreateSummon<Summon_Default>(config.PrefabName, pos, Quaternion.identity, parent);
			if (!(summonInstance == null))
			{
				summonInstance.InitComponent(config.PrefabName);
				summonInstance.HideObject();
				NormalizeRootScale(parent);
				await PlayBornShow(config);
			}
		}
	}

	private async UniTask PlayBornShow(SummonInfoConfigure config)
	{
		if (summonInstance?.summonShow != null)
		{
			await summonInstance.summonShow.PlaySummonShow(this, config.PerformShow, summonInstance.SummonName + "召唤物 出场", GetSummonShowIgnoreDuration());
		}
	}

	private void NormalizeRootScale(Transform parent)
	{
		if (!(summonInstance == null) && summonInstance.transform.childCount != 0 && !(parent == null))
		{
			Transform child = summonInstance.transform.GetChild(0);
			child.localScale = new Vector3(DivideScale(child.localScale.x, parent.localScale.x), DivideScale(child.localScale.y, parent.localScale.y), DivideScale(child.localScale.z, parent.localScale.z));
		}
	}

	private float DivideScale(float scale, float parentScale)
	{
		if (!Mathf.Approximately(parentScale, 0f))
		{
			return scale / parentScale;
		}
		return scale;
	}

	public bool GetSummonShowIgnoreDuration()
	{
		BattlePlayerData battlePlayerData = _landBuffData.TryGetBuffOwner();
		if (battlePlayerData != null && battlePlayerData.CharacterInst?.skill?.skillId == 129)
		{
			if (battlePlayerData.CharacterInst.skill is Skill_129 skill_)
			{
				return !skill_.TriggerSkill;
			}
			return true;
		}
		return false;
	}
}
