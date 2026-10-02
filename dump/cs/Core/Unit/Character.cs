using System;
using System.Collections.Generic;
using System.Linq;
using Core.Camera;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;

namespace Core.Unit;

[Serializable]
public class Character : Unit
{
	public enum ScaleSourceType
	{
		None,
		KongQueKaiPing,
		ShiLaiMu
	}

	public enum AlphaSourceType
	{
		None,
		BangNi,
		XiuNv
	}

	[Header("依赖对象")]
	public Transform characterObject;

	public Transform EffectContainer;

	public Transform CharacterEffectParent;

	public CharacterSignal signal = new CharacterSignal();

	[Space(8f)]
	[Header("角色参数")]
	public RoomPlayer player;

	[Space(8f)]
	[Header("组件")]
	public CharacterAnimator characterAnimator;

	private CharacterMove move;

	public CharacterCamera vCamera;

	public CharacterWindow window;

	public CharacterShowComponent showComponent;

	[Space(8f)]
	[Header("移动")]
	[SerializeField]
	private const float _movementSpeed = 100f;

	[SerializeField]
	private float _rotateSpeed = 3f;

	private UnitLand _standLand;

	private int _fromLandId;

	private int _canStep;

	[Space(8f)]
	[Header("技能")]
	public Skill skill;

	public bool ServerSkillNotVailStatus;

	private Effect ThinkEffect;

	private float _curScale = 1f;

	private Dictionary<ScaleSourceType, float> scaleSources = new Dictionary<ScaleSourceType, float>();

	private Tweener _scaleTwener;

	public readonly Signal<float> scaleChange = new Signal<float>();

	private float curSpriteAlpha = 1f;

	private Dictionary<AlphaSourceType, float> alphaSources = new Dictionary<AlphaSourceType, float>();

	public float scaleConfigCoefficient => player.standingPainting.SkinScale[0];

	public float AnimationDefaultScale => scaleConfigCoefficient * 4f;

	public float movementSpeed
	{
		get
		{
			float num = 100f * BattleConfig.RoleAnimatorSpeed;
			if (skill != null && player != null)
			{
				num *= skill.MoveEffectSpeed(player.Id);
			}
			return num;
		}
	}

	public float rotateSpeed
	{
		get
		{
			float num = _rotateSpeed * BattleConfig.RoleAnimatorSpeed;
			if (skill != null && player != null)
			{
				num *= skill.MoveEffectSpeed(player.Id);
			}
			return num;
		}
	}

	public UnitLand standLand => _standLand;

	public int fromLandId => _fromLandId;

	public int canStep => _canStep;

	public bool activeSkillVail
	{
		get
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(player.Id);
			if (playerDataById != null && playerDataById.Property.activeSkillCD.Value == 0)
			{
				return !ServerSkillNotVailStatus;
			}
			return false;
		}
	}

	public float currentScale
	{
		get
		{
			return _curScale;
		}
		set
		{
			_curScale = value;
			scaleChange.Dispatch(_curScale);
		}
	}

	public void InitData(BattlePlayerData playerData)
	{
		UpdateDataByPlayer(playerData);
		characterAnimator = GetComponentInChildren<CharacterAnimator>();
		characterAnimator.InitBaseComponent(this, AnimationDefaultScale);
		move = new CharacterMove(this);
		vCamera = new CharacterCamera(this);
		window = new CharacterWindow(this);
		InitShowComponent();
		UpdateTransform();
		UpdateSkillData();
	}

	public void UpdateData(BattlePlayerData playerData)
	{
		UpdateDataByPlayer(playerData);
		UpdateTransform();
		UpdateSkillData();
		window.UpdateMonsterAttrInfo();
	}

	private void UpdateDataByPlayer(BattlePlayerData playerData)
	{
		player = playerData.player;
		ResetStep(player.Hero.MovePoint);
		UnitLand standNode = GetStandNode();
		List<int> frontNodeIds = GetFrontNodeIds();
		UpdateLand(standNode, frontNodeIds);
	}

	private UnitLand GetStandNode()
	{
		if (!SimpleSingletonProvider<LandManager>.inst.NodeDict.TryGetValue(player.NodeId, out var value))
		{
			Debug.LogError($"地块id_{player.NodeId}没能获取到");
			return null;
		}
		return value;
	}

	private List<int> GetFrontNodeIds()
	{
		List<int> frontIds = player.FrontIds;
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo != null && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
		{
			frontIds.AddRange(SimpleSingletonProvider<LandManager>.inst.GetLandById(player.NodeId).CanSelectedLandId(player.BackNodeId));
		}
		return frontIds;
	}

	protected void UpdateTransform()
	{
		float mapCharacterOffsetHeight = SimpleSingletonProvider<GameLogicManager>.inst.battle.mapCharacterOffsetHeight;
		base.transform.position = new Vector3(_standLand.transform.position.x, mapCharacterOffsetHeight, _standLand.transform.position.z);
		Transform child = base.transform.GetChild(0);
		if (child != null)
		{
			child.LookAt(base.transform.position + vCamera.vCamera.transform.rotation * Vector3.forward, vCamera.vCamera.transform.rotation * Vector3.up);
			child.localPosition = new Vector3((float)player.characterConfig.OffsetInMap[0] * 0.01f, (float)player.characterConfig.OffsetInMap[1] * 0.01f, (float)player.characterConfig.OffsetInMap[2] * 0.01f);
		}
		if (SimpleSingletonProvider<LandManager>.inst.NodeDict.TryGetValue(standLand.GetNextLandId(_fromLandId), out var value))
		{
			float direction = move.GetDirection(value.transform.position - characterObject.position, characterObject.localScale);
			characterObject.localScale = new Vector3(direction, characterObject.localScale.y, characterObject.localScale.z);
		}
		CharacterEffectParent.position = characterObject.position;
		CharacterEffectParent.rotation = characterObject.rotation;
		CharacterEffectParent.localScale = characterObject.localScale;
		ShowWalkDirections(player.FrontIds);
	}

	private void UpdateLand(UnitLand land, List<int> _frontNodeIds)
	{
		_standLand = land;
		_fromLandId = standLand.GetNextLandId(_frontNodeIds);
	}

	private void UpdateSkillData()
	{
		int battleActiveSkillId = player.GetBattleActiveSkillId();
		if (battleActiveSkillId == 0)
		{
			return;
		}
		Type type = Type.GetType($"GameLogic.Skill_{battleActiveSkillId}");
		if (type == null)
		{
			Debug.LogError($"无法获取技能：GameLogic.Skill_{battleActiveSkillId} 的类，请检查");
			return;
		}
		skill = Activator.CreateInstance(type) as Skill;
		if (skill == null)
		{
			Debug.LogError($"无法创建技能：GameLogic.Skill_{battleActiveSkillId}的实例，请检查");
			return;
		}
		skill.InitSkill(this);
		UpdateActiveSkillCD(player.Hero.SkillCds);
	}

	public void UpdateActiveSkillCD(MapField<int, int> SkillCD)
	{
		int battleActiveSkillId = player.GetBattleActiveSkillId();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(player.Id)?.Property.UpdateSkillCD(SkillCD.GetValueOrDefault(battleActiveSkillId, 0));
	}

	public void ResetStep(int step)
	{
		_canStep = step;
		if (_canStep < 0)
		{
			_canStep = 0;
		}
	}

	public void ResetStandLand(UnitLand land)
	{
		_standLand = land;
	}

	public void ResetFromLandId(int landId)
	{
		_fromLandId = landId;
	}

	public async UniTask ExecuteMove(Queue<int> _queue, bool _End)
	{
		SimpleSingletonProvider<LandManager>.inst.GetLandById(standLand.Id).LandCloseFlash();
		await SwitchCamera();
		await move.ExecuteMove(_queue, _End);
	}

	public void SendCharacter(int nodeId, RepeatedField<int> FrontIds)
	{
		move.SendCharacter(nodeId, FrontIds);
	}

	public float GetWalkDir()
	{
		return move.GetWalkDirection();
	}

	public void SetCharacterPos(float pos_X, float pos_Y)
	{
		base.transform.localPosition = new Vector3(pos_X, base.transform.localPosition.y, pos_Y);
	}

	public void ShowWalkDirections(List<int> frontIds)
	{
		move.ShowWalkDirections(frontIds).Forget();
	}

	public async UniTask StopMove(bool moveAttr = true)
	{
		characterAnimator.Move(walk: false);
		ResetStep(0);
		if (player.characterType == CharacterType.Hero)
		{
			SimpleSingletonProvider<RoadLineManager>.inst.DestroyRoad();
		}
		if (moveAttr)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.action.HandleOtherMoveSkillAttr(player.Id);
		}
		showComponent.UpdateStopStatus();
	}

	public UniTask<bool> SwitchCamera()
	{
		return vCamera.SwitchCamera();
	}

	public CharacterCamera GetCharacterCamera()
	{
		return vCamera;
	}

	public async UniTask PlayThinkingEffect()
	{
		if (player.characterType != CharacterType.Monster)
		{
			if (ThinkEffect == null)
			{
				ThinkEffect = await PlayCharacterEffect(20);
			}
			ThinkEffect.transform.localScale = Vector3.one;
		}
	}

	public void StopThinkEffect()
	{
		if (!(ThinkEffect == null))
		{
			ThinkEffect.transform.localScale = Vector3.zero;
		}
	}

	public async UniTask<Effect> PlayCharacterEffect(int effectId)
	{
		EffectInfoConfigure effectDataConfigure = effectId.GetEffectDataConfigure();
		if (effectDataConfigure != null)
		{
			return effectDataConfigure.IsCharacterObj ? (await SimpleSingletonProvider<EffectManager>.inst.PlayByName(effectDataConfigure.EffectName, Vector3.zero, Quaternion.identity, CharacterEffectParent)) : ((!effectDataConfigure.IsHeroSupport) ? (await SimpleSingletonProvider<EffectManager>.inst.PlayByName(effectDataConfigure.EffectName, base.transform.position, Quaternion.identity)) : (await SimpleSingletonProvider<EffectManager>.inst.PlayByName(effectDataConfigure.EffectName, Vector3.zero, Quaternion.identity, EffectContainer)));
		}
		return null;
	}

	private void InitShowComponent()
	{
		string text = "Core.Unit.CharacterShowComponent";
		Type type = Type.GetType($"{text}_{player.Hero.HeroId}") ?? Type.GetType(text);
		if (type != null)
		{
			showComponent = Activator.CreateInstance(type) as CharacterShowComponent;
			if (showComponent != null)
			{
				showComponent.InitComponent(this);
			}
			else
			{
				Debug.LogError($"角色{player.Hero.HeroId}无法生成演出组件");
			}
		}
		else
		{
			Debug.LogError($"角色{player.Hero.HeroId}无法获取组件类型");
		}
	}

	public float UpdateScale()
	{
		float b = scaleSources.Values.Aggregate(1f, (float a, float num) => a * num);
		b = Mathf.Min(StaticGlobalData.GAME_HERO_SCALE_LIMIT_MAX, b);
		if (Mathf.Approximately(b, currentScale))
		{
			return currentScale;
		}
		currentScale = b;
		Vector3 vector = new Vector3(currentScale, currentScale, currentScale);
		_scaleTwener?.Kill();
		if (scaleSources.ContainsKey(ScaleSourceType.ShiLaiMu))
		{
			_scaleTwener = DOTween.To(() => base.transform.localScale, delegate(Vector3 scale)
			{
				base.transform.localScale = scale;
			}, vector, 1.2f).SetEase(Ease.OutElastic).SetAutoKill();
		}
		else
		{
			base.transform.localScale = vector;
		}
		return currentScale;
	}

	public float SetScale(ScaleSourceType scaleType, float scale)
	{
		scaleSources[scaleType] = scale;
		UpdateScale();
		return currentScale;
	}

	public float RemoveScale(ScaleSourceType scaleType)
	{
		if (scaleSources.Remove(scaleType))
		{
			UpdateScale();
		}
		return currentScale;
	}

	public float UpdateSpriteAlpha()
	{
		curSpriteAlpha = 1f;
		foreach (AlphaSourceType key in alphaSources.Keys)
		{
			curSpriteAlpha = Mathf.Min(curSpriteAlpha, alphaSources[key]);
		}
		SpriteRenderer componentInChildren = base.transform.GetComponentInChildren<SpriteRenderer>();
		if (componentInChildren != null)
		{
			Color color = componentInChildren.color;
			color.a = curSpriteAlpha;
			componentInChildren.color = color;
		}
		return curSpriteAlpha;
	}

	public float SetSpriteAlpha(AlphaSourceType alphaType, float alpha)
	{
		alphaSources[alphaType] = alpha;
		return UpdateSpriteAlpha();
	}

	public float RemoveSpriteAlpha(AlphaSourceType alphaType)
	{
		alphaSources.Remove(alphaType);
		return UpdateSpriteAlpha();
	}

	public void Dispose()
	{
		if (EffectContainer != null)
		{
			Effect[] componentsInChildren = EffectContainer.GetComponentsInChildren<Effect>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ReleaseEffect();
			}
		}
		move?.Dispose();
		window?.Dispose();
		vCamera?.Dispose();
		skill?.DisposeSkill();
		showComponent?.Dispose();
	}
}
