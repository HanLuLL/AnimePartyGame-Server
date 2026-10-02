using System.Collections.Generic;
using Google.Protobuf.Collections;
using UI;
using party.model;

namespace GameLogic;

public class SportsMeetData
{
	public MapField<int, ChallengeData> heroChallengeData = new MapField<int, ChallengeData>();

	public readonly RepeatedField<int> passMapIds = new RepeatedField<int>();

	public bool isKnockoutMatch;

	public bool isFinalMatch;

	public void InitFromServer(SportsMeetInfo info)
	{
		heroChallengeData.Clear();
		passMapIds.Clear();
		if (info != null)
		{
			if (info.ChallengeData != null)
			{
				UpdateChallengeData(info.ChallengeData);
			}
			if (info.PassMapIds != null)
			{
				passMapIds.AddRange(info.PassMapIds);
			}
			isKnockoutMatch = info.IsKnockoutMatch;
			isFinalMatch = info.IsFinalMatch;
		}
	}

	public void UpdateChallengeData(MapField<int, ChallengeData> challengeDatas)
	{
		foreach (int key in challengeDatas.Keys)
		{
			heroChallengeData[key] = challengeDatas[key];
		}
	}

	public bool HeroMapIsUnlockByHeroId(int heroId, int mapId)
	{
		if (GetSeedHeroIds().Contains(heroId))
		{
			return true;
		}
		int mapFormerID = GetMapFormerID(mapId);
		switch (mapFormerID)
		{
		case -1:
			return false;
		case 0:
			return true;
		default:
		{
			if (GetSportsMeetRankByMapId(mapId) == 2 && isKnockoutMatch)
			{
				return true;
			}
			if (GetSportsMeetRankByMapId(mapId) == 1 && isFinalMatch)
			{
				return true;
			}
			if (heroChallengeData.TryGetValue(heroId, out var value))
			{
				return value.GameData.ContainsKey(mapFormerID);
			}
			return false;
		}
		}
	}

	public bool MapIsUnlock(int mapId)
	{
		int mapFormerID = GetMapFormerID(mapId);
		return mapFormerID switch
		{
			-1 => false, 
			0 => true, 
			_ => passMapIds.Contains(mapFormerID), 
		};
	}

	public RepeatedField<int> GetSportsMeetMapIds()
	{
		return 12.GetGameModeInfoConfigure()?.MapID;
	}

	public RepeatedField<int> GetSeedHeroIds()
	{
		if (StaticConfigure.GameMode.MutatorPVEDict.TryGetValue(1, out var value))
		{
			return value.EligibleHeroIds;
		}
		return null;
	}

	public int GetHeroMapRecordById(int heroId, int mapId)
	{
		if (heroChallengeData.TryGetValue(heroId, out var value))
		{
			value.GameData.TryGetValue(mapId, out var value2);
			return value2;
		}
		return 0;
	}

	public bool HeroHasStar(int heroId)
	{
		if (GetHeroMapRecordById(heroId, GetMapIdBySportsMeetRank(1)) <= 0 && GetHeroMapRecordById(heroId, GetMapIdBySportsMeetRank(2)) <= 0)
		{
			return GetHeroMapRecordById(heroId, GetMapIdBySportsMeetRank(3)) > 0;
		}
		return true;
	}

	public int GetHeroKillRecordById(int heroId, int monsterId)
	{
		if (heroChallengeData.TryGetValue(heroId, out var value))
		{
			value.KillData.TryGetValue(monsterId, out var value2);
			return value2;
		}
		return 0;
	}

	public bool MapIsUnlockAll()
	{
		foreach (int sportsMeetMapId in GetSportsMeetMapIds())
		{
			if (!MapIsUnlock(sportsMeetMapId))
			{
				return false;
			}
		}
		return true;
	}

	public static int GetMapFormerID(int mapId)
	{
		return mapId switch
		{
			83001 => 0, 
			83002 => 83001, 
			83003 => 83002, 
			_ => -1, 
		};
	}

	public static int GetSportsMeetRankByMapId(int mapId)
	{
		return mapId switch
		{
			83001 => 3, 
			83002 => 2, 
			83003 => 1, 
			_ => 0, 
		};
	}

	public static int GetMapIdBySportsMeetRank(int rank)
	{
		return rank switch
		{
			3 => 83001, 
			2 => 83002, 
			1 => 83003, 
			_ => 0, 
		};
	}

	public static int GetMapRecordById(MapField<int, int> mapData, int mapId)
	{
		return mapData?.GetValueOrDefault(mapId, 0) ?? 0;
	}
}
