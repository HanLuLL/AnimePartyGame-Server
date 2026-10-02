using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using party.protocol;

namespace Core;

public class TrainManager : MonoBehaviour
{
	public static TrainManager inst;

	[SerializeField]
	private TimelineAsset firstPath;

	[SerializeField]
	private TimelineAsset secondPath;

	[SerializeField]
	private Transform trainHead;

	private PlayableDirector director;

	private readonly List<Effect> effectList = new List<Effect>();

	private readonly List<int> paths = new List<int>();

	private UniTaskCompletionSource trainTsc;

	private readonly Dictionary<long, BattlePlayerData> showPlayers = new Dictionary<long, BattlePlayerData>();

	private UpdateHeroAttrS2C HeroAttrs;

	private MapEventInfoConfigure EventData;

	protected void Awake()
	{
		inst = this;
		director = GetComponent<PlayableDirector>();
		director.stopped -= FinishTrainDisplay;
		director.stopped += FinishTrainDisplay;
		StaticConfigure.MapEvent.InfoDict.TryGetValue(31003, out EventData);
	}

	private void FinishTrainDisplay(PlayableDirector _director)
	{
		trainTsc?.TrySetResult();
		showPlayers.Clear();
	}

	private void Update()
	{
		if (director.state != PlayState.Playing || !trainHead.parent.gameObject.activeSelf || HeroAttrs == null || HeroAttrs.EffectDatas.Count == 0)
		{
			return;
		}
		foreach (HeroAttrEffect effectData in HeroAttrs.EffectDatas)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(effectData.PlayerId);
			if (Vector3.Dot((playerDataById.CharacterInst.transform.position - trainHead.position).normalized, trainHead.forward) < 0f)
			{
				ShowStrike(playerDataById);
			}
		}
	}

	private void ShowStrike(BattlePlayerData playerData)
	{
		if (showPlayers.TryAdd(playerData.player.Id, playerData))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(playerData.player.Id, EventData.Perform1, "火车撞击", ignoreDuration: true);
		}
	}

	public async UniTask ActiveTrain(UpdateHeroAttrS2C model)
	{
		if (paths.Count != 0)
		{
			HeroAttrs = model;
			trainTsc = new UniTaskCompletionSource();
			if (paths.Contains(6) && paths.Contains(30))
			{
				director.Play((PlayableAsset)(object)firstPath, DirectorWrapMode.None);
			}
			else
			{
				director.Play((PlayableAsset)(object)secondPath, DirectorWrapMode.None);
			}
			CtsInfo cts = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
			cts.Token.Register(delegate
			{
				trainTsc.TrySetResult();
			});
			await trainTsc.Task;
			SimpleSingletonProvider<DelaySignalManager>.inst.DisposeCts(cts);
		}
		DestroyRoad();
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

	protected void OnDestroy()
	{
		inst = null;
	}
}
