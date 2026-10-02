using System.Collections.Generic;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

public class MapGimmickManager016 : MapGimmickManager
{
	private const int VocalConcert = 168;

	[SerializeField]
	private PlayableDirector PlayableDirector;

	protected override void Awake()
	{
		Initialize();
		base.Awake();
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
		List<RoomPlayer> monsters = roomController.localRoom.Monsters;
		if (monsters.Count <= 0)
		{
			return;
		}
		foreach (RoomPlayer item in monsters)
		{
			if (item.Hero.HeroId == 1037 && item.Hero.Hp > 0)
			{
				return;
			}
		}
		TryPlayBGM().Forget();
	}

	public override void RefreshGimmickData(int groupId, int statusId)
	{
	}

	public override UniTask SwitchGimmick(int groupId, int statusId, bool wait = true)
	{
		return UniTask.CompletedTask;
	}

	public void PlayEnvironmentShowByMapEvent()
	{
		if (!((Object)(object)PlayableDirector == null))
		{
			PlayableDirector.Play();
		}
	}

	public async UniTask TryPlayBGM()
	{
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntil(() => SimpleSingletonProvider<SceneManager>.inst.loadingScene.Value.stateType != SceneStateType.Begin);
		RoomBattleBGM battleBGM = SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleBGM;
		if (battleBGM != null && battleBGM.Map_BGMId != 168)
		{
			battleBGM.Map_BGMId = 168;
			battleBGM.PlayBattleBGM(0L);
		}
	}
}
