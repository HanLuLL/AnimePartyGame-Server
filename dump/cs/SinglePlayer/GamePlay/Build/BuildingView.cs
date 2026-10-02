using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using SinglePlayer.AssetsHelper;
using SinglePlayer.GamePlay.Character;
using SinglePlayer.GamePlay.Map;
using Tools;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityTimer;

namespace SinglePlayer.GamePlay.Build;

public class BuildingView : MonoBehaviour, IUnitView, IFireBullet, IPlayEffect
{
	[SerializeField]
	private Transform _plinthRoot;

	[SerializeField]
	private PlayableDirector _director;

	[SerializeField]
	private BuildingMaterial _buildingMaterial;

	private Animator _animator;

	private Animator _plinthRootAnimator;

	private Animator _buildingRootAnimator;

	private GameObject _plinth;

	private GameObject _building;

	private BuildingAssetManager _buildingAssetManager;

	private UniTaskCompletionSource _task;

	public GameObject Plinth => _plinth;

	public GameObject Building => _building;

	public SinglePlayerCardConfigureItem Config { get; private set; }

	public BuildingBase BuildingBase { get; set; }

	public Transform UIInfoRoot { get; private set; }

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_director = GetComponent<PlayableDirector>();
	}

	public void Init(SinglePlayerCardConfigureItem config, GameObject plinth, GameObject building)
	{
		_buildingAssetManager = Game.GetSystem<SinglePlayerAssetsHelper>().buildingAssetManager;
		Config = config;
		_plinth = plinth;
		CombineBuilding(config, plinth, building);
		_buildingMaterial.Init();
		Game.GetModel<GlobalSignal>().BuildingMoveStart.AddListener(OnBuildingMoveStart);
	}

	public bool IsVaild()
	{
		return Config != null;
	}

	public void Clear()
	{
		_buildingAssetManager = null;
		_plinth = null;
		_building = null;
		Config = null;
		if (BuildingBase != null)
		{
			Game.GetModel<GlobalSignal>().CardUpgrade.RemoveListener(OnUpgrade);
			Game.GetModel<GlobalSignal>().CardGainExp.RemoveListener(OnExpChange);
			Game.GetModel<GlobalSignal>().BuildingOperateBonus.RemoveListener(OnOperateBonusChange);
			BuildingBase.BuildingTriggerStayEffect.RemoveListener(OnPlayLandEffect);
			BuildingBase.BuildingTriggerPassEffect.RemoveListener(OnPlayLandEffect);
			BuildingBase = null;
		}
		_plinthRootAnimator = null;
		_buildingRootAnimator = null;
		Game.GetModel<GlobalSignal>().BuildingMoveStart.RemoveListener(OnBuildingMoveStart);
		_buildingMaterial.Clear();
	}

	private void CombineBuilding(SinglePlayerCardConfigureItem config, GameObject plinth, GameObject building)
	{
		if (!string.IsNullOrEmpty(config.Plinth))
		{
			plinth.transform.SetParent(_plinthRoot);
			plinth.transform.localPosition = Vector3.zero;
			plinth.transform.localRotation = Quaternion.identity;
			plinth.transform.localScale = Vector3.one;
			Transform transform = plinth.transform.DeepFind("SG_BDBase");
			if (transform == null)
			{
				transform = new GameObject("SG_BDBase").transform;
				transform.SetParent(transform);
			}
			_building = building;
			building.transform.SetParent(transform);
			building.transform.localPosition = Vector3.zero;
			building.transform.localRotation = Quaternion.identity;
			building.transform.localScale = Vector3.one;
			_plinthRootAnimator = _plinthRoot.GetComponent<Animator>();
			_buildingRootAnimator = transform.GetComponent<Animator>();
		}
		else
		{
			_building = building;
			building.transform.SetParent(_plinthRoot);
			building.transform.localPosition = Vector3.zero;
			building.transform.localRotation = Quaternion.identity;
			building.transform.localScale = Vector3.one;
			_plinthRootAnimator = _plinthRoot.GetComponent<Animator>();
			_buildingRootAnimator = _plinthRoot.GetComponent<Animator>();
		}
	}

	public void CombineBuilding(GameObject building)
	{
		_building = building;
		building.transform.SetParent(_plinthRoot);
		building.transform.localPosition = Vector3.zero;
		building.transform.localRotation = Quaternion.identity;
		building.transform.localScale = Vector3.one;
		_plinthRootAnimator = _plinthRoot.GetComponent<Animator>();
		_buildingRootAnimator = _plinthRoot.GetComponent<Animator>();
	}

	public void SetBuildingBase(BuildingBase buildingBase, bool revert = false)
	{
		BuildingBase = buildingBase;
		Game.GetModel<GlobalSignal>().CardUpgrade.AddListener(OnUpgrade);
		Game.GetModel<GlobalSignal>().CardGainExp.AddListener(OnExpChange);
		Game.GetModel<GlobalSignal>().BuildingOperateBonus.AddListener(OnOperateBonusChange);
		buildingBase.BuildingTriggerStayEffect.AddListener(OnPlayLandEffect);
		buildingBase.BuildingTriggerPassEffect.AddListener(OnPlayLandEffect);
		if (!revert)
		{
			Play("SG_BuildEnter").Forget();
		}
		else
		{
			Play("SG_BuildingTrigger").Forget();
		}
		BuildingFoundation buildingFoundationById = Game.GetModel<GameData>().MapData.GetBuildingFoundationById(buildingBase.BuildingFoundationId);
		if (buildingFoundationById != null)
		{
			UIInfoRoot = buildingFoundationById.UIInfoRoot;
		}
	}

	public async UniTask Play(string key)
	{
		if (!string.IsNullOrEmpty(key))
		{
			await Play(await Game.GetSystem<SinglePlayerAssetsHelper>().buildingAssetManager.GetTimelineAsset(key));
		}
	}

	private async UniTask Play(TimelineAsset timeline)
	{
		foreach (TrackAsset outputTrack in timeline.GetOutputTracks())
		{
			AnimationTrack val = (AnimationTrack)(object)((outputTrack is AnimationTrack) ? outputTrack : null);
			if (val != null)
			{
				switch (((UnityEngine.Object)(object)val).name)
				{
				case "SG_BDBase":
					_director.SetGenericBinding((UnityEngine.Object)(object)outputTrack, (UnityEngine.Object)(object)_buildingRootAnimator);
					break;
				case "SG_BuildBase":
					_director.SetGenericBinding((UnityEngine.Object)(object)outputTrack, (UnityEngine.Object)(object)_animator);
					break;
				case "SG_P":
					_director.SetGenericBinding((UnityEngine.Object)(object)outputTrack, (UnityEngine.Object)(object)_plinthRootAnimator);
					break;
				}
			}
		}
		_director.Play((PlayableAsset)(object)timeline);
		await UniTask.WaitForSeconds((float)((PlayableAsset)(object)timeline).duration, ignoreTimeScale: false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
		_director.playableAsset = null;
	}

	public async UniTask PlayThrowDiceShow(AttributeChangeInfo message)
	{
		if (Config.TriggerParam.Count != 0)
		{
			Game.GetModel<GlobalSignal>().BuildingAttributeChangeShow.Dispatch(this, message);
			await Play(Config.TriggerTL);
			if (_task != null)
			{
				await _task.Task;
			}
		}
	}

	public void OnPassShow(AttributeChangeInfo message)
	{
		Game.GetModel<GlobalSignal>().BuildingAttributeChangeShow.Dispatch(this, message);
		if (Config.WalkParam.Count != 0)
		{
			PlayPerform(Config.WalkPerformID);
			Play(Config.WalkTL).Forget();
		}
	}

	public void PlayStayShow(AttributeChangeInfo message)
	{
		Game.GetModel<GlobalSignal>().BuildingAttributeChangeShow.Dispatch(this, message);
		if (Config.StopParam.Count != 0)
		{
			Play(Config.StopTL).Forget();
		}
	}

	private void PlayPerform(int id)
	{
		if (id == 0)
		{
			return;
		}
		if (!StaticConfigure.SinglePlayer.PerformDict.TryGetValue(id, out var value))
		{
			Debug.LogError($"#建筑物模块# 不存在演出配置，演出ID：{id}");
			return;
		}
		int value2;
		int key;
		foreach (KeyValuePair<int, int> effect in value.Effects)
		{
			effect.Deconstruct(out value2, out key);
			int effectId = value2;
			int time = key;
			PlayEffectPerform(effectId, time).Forget();
		}
		foreach (KeyValuePair<int, int> item in value.Bullet)
		{
			item.Deconstruct(out key, out value2);
			int bulletId = key;
			int time2 = value2;
			PlayBulletPerform(bulletId, time2).Forget();
		}
	}

	private async UniTask PlayEffectPerform(int effectId, int time)
	{
		if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(time))))
		{
			SimpleSingletonProvider<EffectManager>.inst.PlayById(effectId, base.transform.position, Quaternion.identity).Forget();
		}
	}

	private async UniTask PlayBulletPerform(int bulletId, int time)
	{
		if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(time)))
		{
			FireBullet(bulletId).Forget();
		}
	}

	private void OnUpgrade(int cardUid)
	{
		if (BuildingBase != null && cardUid == BuildingBase.Card.UID)
		{
			Play("SG_BuildLevelUp").Forget();
		}
	}

	private void OnBuildingMoveStart(int foundationId, int buildingId, int cardUid)
	{
	}

	private void OnExpChange(int cardUid)
	{
		if (BuildingBase != null && cardUid == BuildingBase.Card.UID && (!(_director.playableAsset != null) || !(_director.playableAsset.name == "SG_BuildLevelUp")))
		{
			Play("SG_BuildingGainExp").Forget();
		}
	}

	private void OnOperateBonusChange(int buildingId, int type, int value)
	{
		if (buildingId == BuildingBase.Id && value != 0)
		{
			PlayEffect(3109);
		}
	}

	private void OnPlayLandEffect()
	{
		Land landByBuildingFoundationId = Game.GetModel<GameData>().MapData.GetLandByBuildingFoundationId(BuildingBase.BuildingFoundationId);
		if (!(landByBuildingFoundationId == null))
		{
			landByBuildingFoundationId.PlayPassStayEffect();
		}
	}

	public async void FireBullet(int initialSpeed = 10, int acceleration = 1, int initialTurnSpeed = 1, int turnAcceleration = 180)
	{
		Effect effect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(3108, base.transform.position, Quaternion.identity);
		Bullet bullet = effect.gameObject.AddComponent<Bullet>();
		bullet.initialSpeed = initialSpeed;
		bullet.acceleration = acceleration;
		bullet.initialTurnSpeed = initialTurnSpeed;
		bullet.turnAcceleration = turnAcceleration;
		HeroView view = Game.GetSystem<BoardManager>().characterManager.Hero.view;
		Vector3 normalized = (view.GetPosition() - base.transform.position).normalized;
		Vector3 normalized2 = Vector3.Cross(Vector3.up, normalized).normalized;
		normalized = Quaternion.AngleAxis(-45f, normalized2) * normalized;
		bullet.Init(base.transform.position, normalized, view.transform.position, 1f, delegate
		{
			UnityEngine.Object.Destroy(bullet);
			effect.ReleaseEffect();
		});
	}

	public async UniTask FireBullet(Vector3 targetPosition, System.Action complete)
	{
		int num = 5;
		Vector3 firePosition = base.transform.position;
		firePosition.y += num;
		targetPosition.y += num;
		Effect effect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(3108, firePosition, Quaternion.identity);
		Bullet bullet = effect.gameObject.AddComponent<Bullet>();
		bullet.initialSpeed = 20f;
		bullet.acceleration = 60f;
		bullet.maxSpeed = 150f;
		bullet.initialTurnSpeed = 180f;
		bullet.turnAcceleration = 720f;
		Vector3 normalized = (targetPosition - firePosition).normalized;
		Vector3 normalized2 = Vector3.Cross(Vector3.up, normalized).normalized;
		normalized = Quaternion.AngleAxis(-45f, normalized2) * normalized;
		bullet.Init(firePosition, normalized, targetPosition, 2f, delegate
		{
			UnityEngine.Object.Destroy(bullet);
			TrailRenderer[] componentsInChildren = effect.GetComponentsInChildren<TrailRenderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Clear();
			}
			effect.ReleaseEffect();
			complete?.Invoke();
		});
	}

	public async UniTask FireBullet(int bulletId)
	{
		if (!StaticConfigure.SinglePlayer.BulletDict.TryGetValue(bulletId, out var bulletConfig))
		{
			Debug.LogError($"#建筑物模块# 不存在子弹配置，子弹ID：{bulletId}");
			return;
		}
		if (!StaticConfigure.Effect.InfoDict.TryGetValue(bulletConfig.BulletEffectid, out var _))
		{
			Debug.LogError($"#建筑物模块# 不存在子弹特效配置，特效ID：{bulletConfig.BulletEffectid}");
			return;
		}
		Transform shootRoot = base.transform.DeepFind("ShootRoot");
		if (shootRoot == null)
		{
			Debug.LogError(string.Format("#建筑物模块# 建筑物{0}不存在子弹发射点{1}", BuildingBase.Id, "ShootRoot"));
			return;
		}
		MonsterView targetView = Game.GetController<MonsterController>().UnitView;
		Effect effect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(bulletConfig.BulletEffectid, shootRoot.position, Quaternion.identity);
		_task = new UniTaskCompletionSource();
		Bullet bullet = effect.gameObject.AddComponent<Bullet>();
		Timer t = Timer.Register(5f, (System.Action)delegate
		{
			UnityEngine.Object.Destroy(bullet);
			effect.ReleaseEffect();
			_task.TrySetResult();
		}, base.gameObject);
		bullet.initialSpeed = bulletConfig.InitialSpeed;
		bullet.acceleration = bulletConfig.Acceleration;
		bullet.initialTurnSpeed = bulletConfig.InitialTurnSpeed;
		bullet.turnAcceleration = bulletConfig.TurnAcceleration;
		bullet.maxSpeed = bulletConfig.MaxSpeed;
		bullet.Init(shootRoot.position, shootRoot.forward, targetView.HitRoot.position, targetView.Radius, delegate(Vector3 hitPosition)
		{
			SimpleSingletonProvider<EffectManager>.inst.PlayById(bulletConfig.HitEffectId, hitPosition, Quaternion.identity).Forget();
			SimpleSingletonProvider<AudioManager>.inst.SendEvent(bulletConfig.HitSoundctId, base.gameObject);
			if (!t.isDone)
			{
				t.Cancel();
				UnityEngine.Object.Destroy(bullet);
				effect.ReleaseEffect();
			}
			_task.TrySetResult();
		});
		await _task.Task;
	}

	public void PlayEffect(int effectId)
	{
		Transform transform = base.transform.DeepFind("SG_VFX");
		if (transform == null)
		{
			Debug.LogError(string.Format("#建筑物模块# 建筑物{0}不存在特效根节点{1}", BuildingBase.Id, "SG_VFX"));
		}
		else
		{
			SimpleSingletonProvider<EffectManager>.inst.PlayById(effectId, Vector3.zero, Quaternion.identity, transform).Forget();
		}
	}

	public Vector3 GetPosition()
	{
		return base.transform.position;
	}

	private void AddExp(int exp)
	{
		BuildingBase.AddExp(exp);
	}

	private void GetNeighborBuildings()
	{
		if (BuildingBase == null)
		{
			return;
		}
		Debug.LogError("开始检测相邻建筑");
		foreach (BuildingBase neighborBuilding in BuildingBase.GetNeighborBuildings())
		{
			Debug.LogError(neighborBuilding.Id);
		}
	}
}
