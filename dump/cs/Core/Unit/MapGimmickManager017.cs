using System.Collections.Generic;
using Core.Camera;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core.Unit;

public class MapGimmickManager017 : MapGimmickManager
{
	public GameObject FirstStepEnvironment;

	public GameObject SecondStepEnvironment;

	public const int CampEnvironmentBGMId_0 = 169;

	public const int CampEnvironmentBGMId_1 = 170;

	public const int CampEnvironmentBGMId_2 = 171;

	public const int CampEnvironmentBGMId_3 = 172;

	[SerializeField]
	private Transform _spawnPoint;

	private GameObject _CheongsamGirl;

	private const int _CheongsamGirlId = 1050;

	[SerializeField]
	private Transform _effectRoot;

	private List<Transform> _inactiveEffectPool = new List<Transform>(64);

	private Dictionary<Transform, float> _activeEffectPool = new Dictionary<Transform, float>(16);

	private bool _showP2Effect;

	private float ShowTimeCount = 7.7f;

	[SerializeField]
	private int ShowEffectObjCount = 16;

	[SerializeField]
	private float ShowEffectIntervalLongTime = 2f;

	[SerializeField]
	private float ShowEffectIntervalShortTime = 0.2f;

	private float _lastShowEffectTime_Long;

	private float _lastShowEffectTime_Short;

	protected override void Awake()
	{
		Initialize();
		base.Awake();
		InitEffectPool();
	}

	private void Update()
	{
		UpdateP2Effect();
	}

	public override void Initialize()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room == null)
		{
			return;
		}
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		RoomInfo localRoom = roomController.localRoom;
		if (localRoom == null || localRoom.info == null)
		{
			return;
		}
		Dictionary<int, MapMissionData> mapMissionDict = roomController.localRoom.MapMissionDict;
		foreach (MapMissionData value in mapMissionDict.Values)
		{
			if (value.MissionId == 320880 && value.state == 2)
			{
				HideCheongsam();
				TryPlayBGM(169).Forget();
				FirstStepEnvironment.SetActiveEx(active: true);
				SecondStepEnvironment.SetActiveEx(active: false);
				return;
			}
		}
		foreach (MapMissionData value2 in mapMissionDict.Values)
		{
			int missionId = value2.MissionId;
			if ((missionId == 320830 || missionId == 320840) && value2.state == 2)
			{
				HideCheongsam();
				TryPlayBGM(171).Forget();
				FirstStepEnvironment.SetActiveEx(active: false);
				SecondStepEnvironment.SetActiveEx(active: true);
				return;
			}
			missionId = value2.MissionId;
			if ((missionId == 320890 || missionId == 320900) && value2.state != 0 && value2.state != 3)
			{
				HideCheongsam();
				TryPlayBGM(172).Forget();
				FirstStepEnvironment.SetActiveEx(active: false);
				SecondStepEnvironment.SetActiveEx(active: true);
				return;
			}
		}
		ShowCheongsam();
		FirstStepEnvironment.SetActiveEx(active: true);
		SecondStepEnvironment.SetActiveEx(active: false);
		TryPlayBGM(169).Forget();
	}

	public override void RefreshGimmickData(int groupId, int statusId)
	{
	}

	public override UniTask SwitchGimmick(int groupId, int statusId, bool wait = true)
	{
		return UniTask.CompletedTask;
	}

	private void LimitCamera(bool state)
	{
		if (SimpleSingletonProvider<CameraManager>.hasInstance)
		{
			SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.limitCameraControl.Dispatch(SimpleSingletonProvider<CameraManager>.inst.GetHostStatus());
		}
	}

	public async UniTask SwitchMap(bool headspace, bool chooseCenter = false)
	{
		SubMapSwitchController mapSwitchController = Object.FindObjectOfType<SubMapSwitchController>();
		if (mapSwitchController == null)
		{
			return;
		}
		LimitCamera(state: true);
		TryPlayBGM((!headspace) ? 169 : (chooseCenter ? 172 : 171)).Forget();
		SimpleSingletonProvider<CameraManager>.inst.ControlFreeCamera(Vector3.zero);
		if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500)))
		{
			if (headspace)
			{
				mapSwitchController.StartSubMapSwitch(0, 1);
			}
			else
			{
				mapSwitchController.StartSubMapSwitch(1, 0);
			}
			bool num = await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntil(() => mapSwitchController == null || !mapSwitchController.IsPlaying);
			_showP2Effect = headspace;
			if (!num)
			{
				LimitCamera(state: false);
			}
		}
	}

	public async UniTask TryPlayBGM(int bgmId)
	{
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntil(() => SimpleSingletonProvider<SceneManager>.inst.loadingScene.Value.stateType != SceneStateType.Begin);
		RoomBattleBGM battleBGM = SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleBGM;
		if (battleBGM != null && battleBGM.Map_BGMId != bgmId)
		{
			battleBGM.Map_BGMId = bgmId;
			battleBGM.PlayBattleBGM(0L);
		}
	}

	private void ShowCheongsam()
	{
		if (!(_CheongsamGirl != null))
		{
			_CheongsamGirl = SimpleSingletonProvider<CharacterAssetManager>.inst.InstantiateCharacter(-1050L, 1050);
			_CheongsamGirl.transform.SetParent(_spawnPoint);
			_CheongsamGirl.transform.localPosition = Vector3.zero;
			Transform obj = _CheongsamGirl.transform.GetChild(0).transform;
			Transform transform = UnityEngine.Camera.main.transform;
			obj.LookAt(_CheongsamGirl.transform.position + transform.rotation * Vector3.forward, transform.rotation * Vector3.up);
			obj.localScale = Vector3.one * 10f;
			_CheongsamGirl.GetComponentInChildren<Animator>().SetBool("IdleState", false);
		}
	}

	private void HideCheongsam()
	{
		SimpleSingletonProvider<CharacterAssetManager>.inst.DestroyRole(-1050L, 1050);
	}

	public void DestroyCheongsam()
	{
		HideCheongsam();
	}

	private void UpdateP2Effect()
	{
		if (!_showP2Effect || _inactiveEffectPool.Count == 0)
		{
			return;
		}
		float time = Time.time;
		if (time > _lastShowEffectTime_Long)
		{
			_lastShowEffectTime_Long = time + ShowEffectIntervalLongTime;
			_lastShowEffectTime_Short = time + ShowEffectIntervalShortTime;
			ShowEffect();
		}
		else if (_lastShowEffectTime_Short > 0f && time > _lastShowEffectTime_Short)
		{
			_lastShowEffectTime_Short = -1f;
			ShowEffect();
		}
		Transform transform = null;
		foreach (KeyValuePair<Transform, float> item in _activeEffectPool)
		{
			if (time > item.Value)
			{
				transform = item.Key;
				break;
			}
		}
		if (transform != null)
		{
			transform.gameObject.SetActive(value: false);
			_activeEffectPool.Remove(transform);
			_inactiveEffectPool.Add(transform);
		}
	}

	private void InitEffectPool()
	{
		if (_effectRoot == null || _effectRoot.childCount == 0)
		{
			Debug.LogError("Map 017 特效组节点 绑定异常");
			return;
		}
		_inactiveEffectPool.Clear();
		_activeEffectPool.Clear();
		foreach (Transform item in _effectRoot)
		{
			item.gameObject.SetActive(value: false);
			_inactiveEffectPool.Add(item);
		}
	}

	private void ShowEffect()
	{
		if (_activeEffectPool.Count < ShowEffectObjCount)
		{
			Transform randomEffect = GetRandomEffect();
			randomEffect.gameObject.SetActive(value: true);
			_activeEffectPool.Add(randomEffect, Time.time + ShowTimeCount);
		}
	}

	private Transform GetRandomEffect()
	{
		int count = _inactiveEffectPool.Count;
		int index = UnityEngine.Random.Range(0, count);
		Transform result = _inactiveEffectPool[index];
		_inactiveEffectPool[index] = _inactiveEffectPool[count - 1];
		_inactiveEffectPool.RemoveAt(count - 1);
		return result;
	}
}
