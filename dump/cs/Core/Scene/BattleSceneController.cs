using Cinemachine;
using Core.Camera;
using Core.Mark;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Render.Runtime;
using Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core.Scene;

public class BattleSceneController : BaseSceneController
{
	public static BattleSceneController inst;

	public int MapSceneIndex;

	[SerializeField]
	public CinemachineVirtualCamera freeCamera;

	[SerializeField]
	public FreeCameraObject freeObject;

	[SerializeField]
	public GameObject SceneUI;

	[HideInInspector]
	public MapRangeManage mapRangeManage;

	[HideInInspector]
	public BattleShowDirector directorManager;

	public MarkManager MarkManager;

	public Volume EnvironmentVolume;

	private Bloom bloom;

	private float _initialIntensityValue;

	private bool _initialIntensityStatus;

	private float _initialThresholdValue;

	private bool _initialThresholdStatus;

	private float _initialScatterValue;

	private bool _initialScatterStatus;

	private Color _initialTintValue;

	private bool _initialTintStatus;

	public UnityEngine.Camera mainCamera { get; private set; }

	public CinemachineBrain cinemachineBrain { get; private set; }

	protected override SceneType GetSceneType()
	{
		return SceneType.Battle;
	}

	public async UniTask InitScene()
	{
		inst = this;
		mainCamera = UnityEngine.Camera.main;
		cinemachineBrain = mainCamera.GetComponent<CinemachineBrain>();
		mapRangeManage = Object.FindObjectOfType<MapRangeManage>();
		SimpleSingletonProvider<LandManager>.inst.InitLand();
		UnitLand firstBornLand = SimpleSingletonProvider<LandManager>.inst.GetFirstBornLand();
		if (firstBornLand != null)
		{
			freeObject.InitPosition(firstBornLand.transform);
		}
		GameObject original = (GameObject)SimpleSingletonProvider<InternalAssetManager>.inst.TryGetOperationHandle("BattleShow");
		directorManager = Object.Instantiate(original, Vector3.up * 1000f, Quaternion.identity).GetComponent<BattleShowDirector>();
		directorManager.UpdateFightConfig(CameraExtensions.GetUniversalAdditionalCameraData(mainCamera).volumeTrigger);
		CardPool.Initialize();
		OperationTimer.Dispose();
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		MapSceneIndex = curRoomInfo?.MapIndex ?? 0;
		if (curRoomInfo != null && curRoomInfo.MapId == 83003)
		{
			AddMap020Scripts();
		}
		else if (curRoomInfo != null && curRoomInfo.MapId == 82016)
		{
			SpecialHotScenePlanarReflectionController specialHotScenePlanarReflectionController = new GameObject("SpecialHotScenePlanarReflectionController").AddComponent<SpecialHotScenePlanarReflectionController>();
			if ((bool)specialHotScenePlanarReflectionController)
			{
				specialHotScenePlanarReflectionController.PlaneY = 1.3f;
				specialHotScenePlanarReflectionController.PlanarReflectionStrength = 2f;
				specialHotScenePlanarReflectionController.Width = 512;
				specialHotScenePlanarReflectionController.Height = 512;
				specialHotScenePlanarReflectionController.BlurRadius = 1f;
				specialHotScenePlanarReflectionController.AddOrSetPlanarReflectionRenderFeature();
			}
		}
		InitVolume();
		await LoadAudioBank();
		ReadyMarkManager();
	}

	private async UniTask LoadAudioBank()
	{
		if (SimpleSingletonProvider<SceneManager>.inst.lastSceneType != SceneType.Battle)
		{
			await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_BGM_BATTLE);
			await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_BGM_ROLE_COMMON);
			await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_Npc_01);
		}
	}

	private void UnloadAudioBank()
	{
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(StaticGlobalData.AUDIO_BANK_BGM_BATTLE);
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(StaticGlobalData.AUDIO_BANK_BGM_ROLE_COMMON);
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(StaticGlobalData.AUDIO_BANK_Npc_01);
	}

	public void UnLoadScene()
	{
		if (directorManager != null)
		{
			Object.Destroy(directorManager.gameObject);
		}
		CardPool.Clear();
		OperationTimer.Dispose();
		if (SimpleSingletonProvider<DiceManager>.hasInstance)
		{
			SimpleSingletonProvider<DiceManager>.inst.DestroyPool();
		}
		if (SimpleSingletonProvider<EffectManager>.hasInstance)
		{
			SimpleSingletonProvider<EffectManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<BuffEffectManager>.hasInstance)
		{
			SimpleSingletonProvider<BuffEffectManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<CameraManager>.hasInstance)
		{
			SimpleSingletonProvider<CameraManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<LandManager>.hasInstance)
		{
			SimpleSingletonProvider<LandManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<RoadLineManager>.hasInstance)
		{
			SimpleSingletonProvider<RoadLineManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<GameLogicManager>.hasInstance)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<RegisterBoardManager>.hasInstance)
		{
			SimpleSingletonProvider<RegisterBoardManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<SummonManager>.hasInstance)
		{
			SimpleSingletonProvider<SummonManager>.inst.Dispose();
		}
	}

	public void UnLoadPreLoadCharacterResources()
	{
		SimpleSingletonProvider<InternalAssetManager>.inst.TryDestroyBattleAssets();
		UnloadAudioBank();
	}

	protected override void OnDestroy()
	{
		inst = null;
		UnLoadScene();
	}

	private void InitVolume()
	{
		ChangeMainCameraPostProcessing();
		SetUICameraPostProcessing();
	}

	private void ChangeMainCameraPostProcessing()
	{
		EnvironmentVolume = mainCamera.GetComponentInChildren<Volume>();
		if (EnvironmentVolume != null)
		{
			if (EnvironmentVolume.profile.TryGet<Bloom>(out bloom))
			{
				_initialIntensityValue = bloom.intensity.value;
				_initialIntensityStatus = bloom.intensity.overrideState;
				_initialThresholdValue = bloom.threshold.value;
				_initialThresholdStatus = bloom.threshold.overrideState;
				_initialScatterValue = bloom.scatter.value;
				_initialScatterStatus = bloom.scatter.overrideState;
				_initialTintValue = bloom.tint.value;
				_initialTintStatus = bloom.tint.overrideState;
			}
			else
			{
				Debug.LogError("Bloom 未在 Volume Profile 中启用！");
			}
		}
	}

	public void ResetVolumeBloom()
	{
		if (!((Object)(object)bloom == null))
		{
			if (!bloom.intensity.value.Equals(_initialIntensityValue))
			{
				bloom.intensity.value = _initialIntensityValue;
				bloom.intensity.overrideState = _initialIntensityStatus;
			}
			if (!bloom.threshold.value.Equals(_initialThresholdValue))
			{
				bloom.threshold.value = _initialThresholdValue;
				bloom.threshold.overrideState = _initialThresholdStatus;
			}
			if (!bloom.scatter.value.Equals(_initialScatterValue))
			{
				bloom.scatter.value = _initialScatterValue;
				bloom.scatter.overrideState = _initialScatterStatus;
			}
			if (!bloom.tint.value.Equals(_initialTintValue))
			{
				bloom.tint.value = _initialTintValue;
				bloom.tint.overrideState = _initialTintStatus;
			}
		}
	}

	public void SetVolumeBloom(BattleResourcePostProcessConfigure postProcessConfig)
	{
		if (!((Object)(object)bloom == null) && postProcessConfig != null)
		{
			bloom.intensity.value = (float)postProcessConfig.Intensity * 0.01f;
			bloom.intensity.overrideState = true;
			bloom.threshold.value = (float)postProcessConfig.Threshold * 0.01f;
			bloom.threshold.overrideState = true;
			bloom.scatter.value = (float)postProcessConfig.Scatter * 0.01f;
			bloom.scatter.overrideState = true;
			if (ColorUtility.TryParseHtmlString(postProcessConfig.Tint, out var color))
			{
				bloom.tint.value = color;
				bloom.tint.overrideState = true;
			}
		}
	}

	private void SetUICameraPostProcessing()
	{
		if (!(StageCamera.main == null))
		{
			UniversalAdditionalCameraData universalAdditionalCameraData = CameraExtensions.GetUniversalAdditionalCameraData(StageCamera.main);
			GameObject uIVolumeInstance = SimpleSingletonProvider<InternalAssetManager>.inst.GetUIVolumeInstance();
			if (uIVolumeInstance != null)
			{
				universalAdditionalCameraData.renderPostProcessing = true;
				universalAdditionalCameraData.volumeLayerMask = LayerMask.GetMask("UI");
				universalAdditionalCameraData.volumeTrigger = uIVolumeInstance.transform;
				uIVolumeInstance.transform.parent = ((Component)(object)universalAdditionalCameraData).transform;
			}
		}
	}

	private void ReadyMarkManager()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		bool num = curRoomInfo?.IsNovice() ?? false;
		bool flag = curRoomInfo?.IsPVP() ?? false;
		bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.watch?.PlayerIsWatcher() ?? false;
		if (!num && !flag2 && !flag)
		{
			MarkManager = base.gameObject.AddComponent<MarkManager>();
		}
	}

	private void AddMap020Scripts()
	{
		GameObject gameObject = GameObject.Find("Map");
		if (gameObject == null || !(gameObject.GetComponentInChildren<MapGimmickManager020>() == null))
		{
			return;
		}
		MapGimmickManager020 mapGimmickManager = gameObject.AddComponent<MapGimmickManager020>();
		if (GameObject.Find("=====MapParent=====") == null)
		{
			Debug.LogError("场景中没有=====MapParent=====节点");
			return;
		}
		Transform child = gameObject.transform.GetChild(0);
		mapGimmickManager.DreamEffects = new GameObject[3];
		Transform transform = child.transform.Find("P1");
		if (transform == null)
		{
			Debug.LogError("场景中没有p1节点");
			return;
		}
		mapGimmickManager.DreamEffects[0] = transform.gameObject;
		Transform transform2 = child.transform.Find("P2");
		if (transform2 == null)
		{
			Debug.LogError("场景中没有p2节点");
			return;
		}
		mapGimmickManager.DreamEffects[1] = transform2.gameObject;
		Transform transform3 = child.transform.Find("P3");
		if (transform3 == null)
		{
			Debug.LogError("场景中没有p3节点");
			return;
		}
		Transform transform4 = child.transform.Find("Viewer");
		mapGimmickManager.DreamEffects[2] = transform3.gameObject;
		Transform transform5 = transform4.transform.Find("Humen");
		if (transform5 == null)
		{
			Debug.LogError("场景中没有Humen节点");
			return;
		}
		mapGimmickManager.HumanNpcRoot = transform5;
		Transform transform6 = transform4.transform.Find("AI");
		if (transform6 == null)
		{
			Debug.LogError("场景中没有AI节点");
			return;
		}
		mapGimmickManager.RobotNpcRoot = transform6;
		SimpleSingletonProvider<LandManager>.inst.MapGimmickManager = mapGimmickManager;
		mapGimmickManager.Initialize();
	}
}
