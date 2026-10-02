using System.Collections.Generic;
using System.Linq;
using Core.Audio;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Card;
using SinglePlayer.GamePlay.Character;
using SinglePlayer.GamePlay.Map;
using SinglePlayer.Tools;
using Tools;
using UnityEngine;
using party.model;

namespace SinglePlayer.GamePlay;

public class GameData : IModel, IInitialize, IDispose
{
	public ReactiveEncryptorProperty<int, Int32Encryptor> Round;

	public ReactiveEncryptorProperty<int, Int32Encryptor> GameProgress;

	public readonly MapData MapData = new MapData();

	public readonly CardData CardData = new CardData();

	public readonly BuildingData BuildingData = new BuildingData();

	public int LevelId = 10000;

	public int ChapterId = 10000;

	private Dictionary<int, int> maxIdPassedDict = new Dictionary<int, int>();

	private int UpgradePlanKey = 1;

	private int[] defaultCardPacks = new int[4] { 100, 101, 102, 103 };

	private const int defaultLevelId = 10000;

	private const int defaultChapterId = 10000;

	private const string key_selectedCardPacks = "keys_selectedCardPacks";

	public RoundTiming RoundTiming { get; private set; }

	public HeroProperty heroProperty { get; private set; }

	public MonsterProperty MonsterProperty { get; private set; }

	public SinglePlayerLevelConfigure CurrentLevelConfig { get; private set; }

	public int MaxLevelIdPassed { get; private set; }

	public HashSet<int> SelectedCardPacks { get; private set; } = new HashSet<int>();

	public async UniTask Initialize()
	{
		SingleGameData singleGameData = SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer?.ServerGameData;
		Round = new ReactiveEncryptorProperty<int, Int32Encryptor>(singleGameData?.Round ?? 0);
		GameProgress = new ReactiveEncryptorProperty<int, Int32Encryptor>(singleGameData?.GameProgress ?? 0);
		if (singleGameData != null)
		{
			LevelId = ((singleGameData.LevelId != 0) ? singleGameData.LevelId : 10000);
			ChapterId = ((singleGameData.StageId != 0) ? singleGameData.StageId : 10000);
		}
		else
		{
			LevelId = 10000;
			ChapterId = 10000;
		}
		SelectedCardPacks.Clear();
		RepeatedField<int> repeatedField = singleGameData?.SelectCardIds;
		if (repeatedField != null && repeatedField.Count > 0)
		{
			foreach (int item2 in repeatedField)
			{
				SelectedCardPacks.Add(item2);
			}
		}
		else
		{
			int[] array = ES3.Load("keys_selectedCardPacks", defaultCardPacks);
			if (array == null || array.Length < 4)
			{
				array = defaultCardPacks;
			}
			int[] array2 = array;
			foreach (int item in array2)
			{
				SelectedCardPacks.Add(item);
			}
		}
		maxIdPassedDict.Clear();
		foreach (KeyValuePair<int, int> item3 in singleGameData?.StageLevelId ?? new MapField<int, int>())
		{
			maxIdPassedDict[item3.Key] = item3.Value;
		}
		foreach (KeyValuePair<int, int> item4 in SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer?.MaxIdPassedDict ?? new Dictionary<int, int>())
		{
			maxIdPassedDict[item4.Key] = item4.Value;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer?.SetMaxIdPassedDict(maxIdPassedDict);
		int value;
		bool flag = maxIdPassedDict.TryGetValue(ChapterId, out value);
		MaxLevelIdPassed = (flag ? value : 0);
		InitializeRoundTiming(singleGameData);
		UIDGenerator.SetCurrentUID(singleGameData?.ClientGenerateUID ?? 0);
		CardData.Initialize(singleGameData);
		MapData.Initialize(singleGameData?.MapData);
		InitHeroPropertyFromServer(singleGameData?.Hero);
		await UniTask.CompletedTask;
		if (StaticConfigure.SinglePlayer.LevelDict.TryGetValue(LevelId, out var value2))
		{
			CurrentLevelConfig = value2;
			BGMHelper.TryPlayBGM(value2.Bgm);
		}
	}

	public void SetSelectedCardPacks(IEnumerable<int> cardPackIds)
	{
		SelectedCardPacks.Clear();
		foreach (int cardPackId in cardPackIds)
		{
			SelectedCardPacks.Add(cardPackId);
		}
		ES3.Save("keys_selectedCardPacks", SelectedCardPacks.ToArray());
	}

	public void SetLeveId(int levelId)
	{
		LevelId = levelId;
	}

	public bool IsFinalLevel()
	{
		bool result = true;
		if (StaticConfigure.SinglePlayer.ChapterDict.TryGetValue(ChapterId, out var value))
		{
			result = value.Levels.Last() == LevelId;
		}
		return result;
	}

	public void UpdateLevelConfigBeforeGameStart()
	{
		MapData.ReInitMapMission();
		if (StaticConfigure.SinglePlayer.LevelDict.TryGetValue(LevelId, out var value))
		{
			CurrentLevelConfig = value;
			BGMHelper.TryPlayBGM(value.Bgm);
		}
		Game.GetSystem<BoardManager>().cardManager.RebuildCardDropTableInitialPool();
	}

	public void Dispose()
	{
		MapData.Dispose();
		CardData.Dispose();
		BuildingData.Dispose();
	}

	public void UploadDataToServer(GameStatus status)
	{
		SingleGameData singleGameData = new SingleGameData
		{
			LevelId = LevelId,
			StageId = ChapterId,
			GameProgress = GameProgress.Value,
			Round = Round.Value,
			ClientGenerateUID = UIDGenerator.NextUID(),
			CardData = CardData.GetUploadData(),
			MapData = MapData.GetUploadData(),
			Hero = heroProperty.GetUploadData(),
			BuildingData = BuildingData.GetUploadData(),
			GameStatus = (int)status,
			RoundTiming = (int)RoundTiming
		};
		singleGameData.SelectCardIds.AddRange(SelectedCardPacks);
		if (status == GameStatus.Victory)
		{
			int value = Mathf.Max(MaxLevelIdPassed, LevelId);
			maxIdPassedDict[ChapterId] = value;
			foreach (KeyValuePair<int, int> item in maxIdPassedDict)
			{
				singleGameData.StageLevelId[item.Key] = item.Value;
			}
			SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer?.SetMaxIdPassedDict(maxIdPassedDict);
		}
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer?.RequestSyncSingleGameDataC2S(status, singleGameData);
	}

	private void InitializeRoundTiming(SingleGameData singleGameData)
	{
		if (singleGameData == null)
		{
			RoundTiming = RoundTiming.None;
		}
		else
		{
			RoundTiming = (RoundTiming)singleGameData.RoundTiming;
		}
	}

	public void SetRoundTiming(RoundTiming timing)
	{
		if (timing > RoundTiming)
		{
			RoundTiming = timing;
		}
		else if (RoundTiming == RoundTiming.RoundEndAfter && timing == RoundTiming.RoundStartBefore)
		{
			RoundTiming = RoundTiming.RoundStartBefore;
		}
		else if (RoundTiming == RoundTiming.UserOperationEnd && timing == RoundTiming.RoundStartBefore)
		{
			RoundTiming = RoundTiming.RoundStartBefore;
		}
	}

	public UniTask ChangeRound()
	{
		return UniTask.CompletedTask;
	}

	private void InitHeroPropertyFromServer(SingleHero heroData)
	{
		Land fillingStationLand = MapData.GetFillingStationLand();
		List<int> nextIds = new List<int> { fillingStationLand.NeighborLandIds[0] };
		heroProperty = new HeroProperty(1, fillingStationLand.Id, nextIds);
		if (heroData != null)
		{
			heroProperty.InitDataFromServer(heroData);
		}
	}

	public MonsterProperty CreateMonsterProperty(int monsterId, int missionId)
	{
		if (MonsterProperty != null && MonsterProperty.Id == monsterId)
		{
			return MonsterProperty;
		}
		if (StaticConfigure.SinglePlayer.MonsterDict.ContainsKey(monsterId))
		{
			MonsterProperty = new MonsterProperty(monsterId, missionId);
			return MonsterProperty;
		}
		return null;
	}

	public void DisposeMonsterProperty()
	{
		MonsterProperty = null;
	}

	public SinglePlayerUpgradeConfigure GetUpgradeInfoGroup()
	{
		if (!StaticConfigure.SinglePlayer.UpgradeDict.TryGetValue(UpgradePlanKey, out var value))
		{
			Debug.LogError($"无法通过Key:{UpgradePlanKey}, 获取对应的升级方案");
			return null;
		}
		return value;
	}

	public SinglePlayerUpgradeConfigureItem GetHeroUpgradeInfo(int heroStar)
	{
		SinglePlayerUpgradeConfigure upgradeInfoGroup = GetUpgradeInfoGroup();
		if (upgradeInfoGroup == null)
		{
			return null;
		}
		foreach (SinglePlayerUpgradeConfigureItem singlePlayerUpgradeConfigureItem in upgradeInfoGroup.SinglePlayerUpgradeConfigureItems)
		{
			if (singlePlayerUpgradeConfigureItem.Star == heroStar)
			{
				return singlePlayerUpgradeConfigureItem;
			}
		}
		return null;
	}

	public GameStatus GetGameStatus()
	{
		SinglePlayer.GamePlay.Map.MapMission currentMissionData = Game.GetSystem<BoardManager>().missionManager.GetCurrentMissionData();
		if (currentMissionData == null)
		{
			if (MapData.MapMissionData.All((SinglePlayer.GamePlay.Map.MapMission m) => m.Status == MapMissionStatus.Success))
			{
				return GameStatus.Victory;
			}
		}
		else if (currentMissionData.Status == MapMissionStatus.Failure)
		{
			return GameStatus.GameOver;
		}
		return GameStatus.Running;
	}
}
