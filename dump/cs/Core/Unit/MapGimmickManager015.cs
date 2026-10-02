using System.Collections.Generic;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;

namespace Core.Unit;

public class MapGimmickManager015 : MapGimmickManager
{
	private readonly List<Effect> effectList = new List<Effect>();

	private readonly List<int> paths = new List<int>();

	private CrabManager _crabManager;

	public GameObject DefaultElementParent;

	public GameObject CampElementParent_01;

	public GameObject CampElementParent_02;

	private const int CampBossBGMId_Li = 166;

	private const int CampBossBGMId_Jiao = 165;

	protected override void Awake()
	{
		Initialize();
		base.Awake();
	}

	public override void Initialize()
	{
		_crabManager = Object.FindObjectOfType<CrabManager>();
		if (SimpleSingletonProvider<GameLogicManager>.inst.room != null)
		{
			RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
			RoomInfo localRoom = roomController.localRoom;
			if (localRoom != null && localRoom.info != null && roomController.localRoom.info.CampId != 0)
			{
				RefreshMapStatus(roomController.localRoom.info.CampId);
			}
		}
	}

	public override void RefreshGimmickData(int groupId, int statusId)
	{
	}

	public override async UniTask SwitchGimmick(int groupId, int statusId, bool wait = true)
	{
		if (_crabManager == null)
		{
			Debug.LogError("MapGimmickManager015: CrabManager is null!");
		}
		else if (statusId <= 1)
		{
			await _crabManager.PlayMove(statusId);
		}
	}

	public async UniTask ActiveCrab()
	{
		DestroyRoad();
		await _crabManager.PlayAttack(delegate
		{
			ActionEffectShow actionEffectShow = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
			List<long> changeAttrPlayerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
			for (int i = 0; i < changeAttrPlayerIds.Count; i++)
			{
				actionEffectShow.PlayPlayerShow(changeAttrPlayerIds[i], 8, "螃蟹 目标演出").Forget();
			}
		});
	}

	public async UniTask ActiveRoadLine(RepeatedField<int> _Paths)
	{
		paths.Clear();
		paths.AddRange(_Paths);
		for (int i = 0; i < paths.Count; i++)
		{
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(paths[i]);
			Effect item = await SimpleSingletonProvider<EffectManager>.inst.PlayById(49, Vector3.zero, Quaternion.identity, landById.transform);
			effectList.Add(item);
		}
	}

	private void DestroyRoad()
	{
		for (int i = 0; i < effectList.Count; i++)
		{
			if (effectList[i] != null)
			{
				effectList[i].ReleaseEffect();
			}
		}
		effectList.Clear();
	}

	public void AfterAssistVote(int campId)
	{
		RefreshMapStatus(campId);
		DestroyRoad();
		_crabManager.SetCrabStatus();
	}

	private void RefreshMapStatus(int campId)
	{
		if (campId == 0)
		{
			Debug.LogError("当前获取的地图阵营id为0!");
			return;
		}
		PVEMissionVoteConfigure leftMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster;
		if (CampElementParent_01 == null)
		{
			Debug.LogError("MapGimmickManager015: CampElementParent_01 is null!");
			return;
		}
		if (CampElementParent_02 == null)
		{
			Debug.LogError("MapGimmickManager015: CampElementParent_02 is null!");
			return;
		}
		bool flag = leftMonster.Id == campId;
		CampElementParent_01.SetActive(!flag);
		CampElementParent_02.SetActive(flag);
		DefaultElementParent.SetActive(value: false);
		TryPlayBGM(flag).Forget();
	}

	private async UniTask TryPlayBGM(bool isLeftCamp)
	{
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntil(() => SimpleSingletonProvider<SceneManager>.inst.loadingScene.Value.stateType != SceneStateType.Begin);
		RoomBattleBGM battleBGM = SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleBGM;
		if (battleBGM != null)
		{
			int num = (isLeftCamp ? 165 : 166);
			if (battleBGM.Map_BGMId != num)
			{
				battleBGM.Map_BGMId = num;
				battleBGM.PlayBattleBGM(0L);
			}
		}
	}

	public void PlayCrabDown()
	{
		_crabManager.PlayDown();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleBGM?.PlayBattleBGM(0L);
	}

	public async UniTask<bool> TryShowArtifactByStoryId(int storyId)
	{
		if (storyId == 8201302)
		{
			return await SimpleSingletonProvider<UIManager>.inst.battleHint.ShowDragonPlaceTreasure();
		}
		return false;
	}
}
