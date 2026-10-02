using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;
using party.protocol;

namespace GameLogic;

public class BattleSettlement
{
	public readonly long WinTeamId;

	public int MapMode;

	private readonly List<PlayerSettlement> PlaySettlements = new List<PlayerSettlement>();

	public Dictionary<int, int> BattleTotalData = new Dictionary<int, int>();

	public readonly int ExpItemId = 50;

	public readonly int PVECoinItemId = 6;

	public readonly List<KeyValuePair<int, int>> AwardItems = new List<KeyValuePair<int, int>>();

	public readonly Dictionary<int, int> maxValues = new Dictionary<int, int>();

	public readonly Int32KvPair CampScore;

	public readonly MapField<int, int> RookieBonusAwards;

	public readonly MapField<int, int> ReturnBonusAwards;

	private List<BattlePlayerData> _players;

	public bool IsPVP => BattleConfig.IsPVP(MapMode);

	public bool IsPVE => BattleConfig.IsPVE(MapMode);

	public BattleSettlement(GameFinishS2C data, List<BattlePlayerData> players)
	{
		MapMode = data.MapType;
		if (MapMode == 0)
		{
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			if (curRoomInfo != null)
			{
				MapMode = curRoomInfo.MapType;
			}
		}
		_players = players;
		WinTeamId = data.Winer;
		CalculateAwards(data.Awards);
		RookieBonusAwards = data.RookieBonusAwards;
		ReturnBonusAwards = data.ReturnBonusAwards;
		CalculateAchievements(data.NewAchieve);
		CampScore = data.CampScores;
	}

	public int GetRookieBonusAwardByItemId(int itemId)
	{
		return RookieBonusAwards.GetValueOrDefault(itemId);
	}

	public int GetRetrurnBonusAwardByItemId(int itemId)
	{
		return ReturnBonusAwards.GetValueOrDefault(itemId);
	}

	private void CalculateAwards(MapField<int, int> awards)
	{
		foreach (var (num3, value) in awards)
		{
			if (num3 != PVECoinItemId)
			{
				AwardItems.Add(new KeyValuePair<int, int>(num3, value));
			}
		}
		if (IsPVE && awards.TryGetValue(PVECoinItemId, out var value2))
		{
			AwardItems.Add(new KeyValuePair<int, int>(PVECoinItemId, value2));
		}
	}

	public BattlePlayerData GetPlayerData(long playerId)
	{
		if (_players == null)
		{
			return null;
		}
		return _players.Find((BattlePlayerData x) => x.player.Id == playerId);
	}

	private void CalculateAchievements(MapField<long, PlayerFinishAchieve> Achieve)
	{
		foreach (KeyValuePair<long, PlayerFinishAchieve> item2 in Achieve)
		{
			item2.Deconstruct(out var key, out var value);
			long playerId = key;
			PlayerFinishAchieve achievement = value;
			BattlePlayerData playerData = GetPlayerData(playerId);
			if (playerData != null)
			{
				PlayerSettlement item = new PlayerSettlement(WinTeamId, playerData, achievement);
				PlaySettlements.Add(item);
			}
		}
		if (PlaySettlements == null || PlaySettlements.Count == 0)
		{
			return;
		}
		foreach (PlayerSettlement playSettlement in PlaySettlements)
		{
			foreach (KeyValuePair<int, int> battleDatum in playSettlement.BattleData)
			{
				BattleTotalData[battleDatum.Key] = BattleTotalData.GetValueOrDefault(battleDatum.Key) + battleDatum.Value;
				if (!maxValues.ContainsKey(battleDatum.Key) || battleDatum.Value > maxValues[battleDatum.Key])
				{
					maxValues[battleDatum.Key] = battleDatum.Value;
				}
			}
		}
		foreach (PlayerSettlement playSettlement2 in PlaySettlements)
		{
			foreach (KeyValuePair<int, int> battleDatum2 in playSettlement2.BattleData)
			{
				if (battleDatum2.Value != 0 && battleDatum2.Value == maxValues[battleDatum2.Key])
				{
					playSettlement2.TryAddAchievements(battleDatum2.Key);
				}
			}
			playSettlement2.SortAchievements(IsPVE);
		}
		UpdateRankData();
		PlaySettlements.Sort((PlayerSettlement x, PlayerSettlement y) => x.PlayerData.player.Slot.CompareTo(y.PlayerData.player.Slot));
	}

	private void UpdateRankData()
	{
		List<BattlePlayerData> list = new List<BattlePlayerData>(4);
		for (int i = 0; i < PlaySettlements.Count; i++)
		{
			list.Add(PlaySettlements[i].PlayerData);
		}
		if (BattleConfig.IsAsymmetricalBattle(MapMode) || BattleConfig.IsLuckyStarBattle(MapMode))
		{
			foreach (BattlePlayerData item in list)
			{
				item.rank = ((item.player.TeamId != WinTeamId) ? 1 : 0);
			}
			return;
		}
		list.Sort(delegate(BattlePlayerData x, BattlePlayerData y)
		{
			int num3 = y.Property.level.Value.CompareTo(x.Property.level.Value);
			return (num3 != 0) ? num3 : y.Property.gold.Value.CompareTo(x.Property.gold.Value);
		});
		int num = 0;
		for (int num2 = 0; num2 < list.Count; num2++)
		{
			list[num2].rank = num;
			if (num2 >= list.Count - 1 || list[num2].Property.level.Value != list[num2 + 1].Property.level.Value || list[num2].Property.gold.Value != list[num2 + 1].Property.gold.Value)
			{
				num++;
			}
		}
	}

	public PlayerSettlement GetPlayerSettlement(int index)
	{
		if (PlaySettlements.Count <= index)
		{
			return null;
		}
		return PlaySettlements[index];
	}

	public PlayerSettlement GetPlayerSettlementById(long playerId)
	{
		foreach (PlayerSettlement playSettlement in PlaySettlements)
		{
			if (playSettlement.PlayerData.player.Id == playerId)
			{
				return playSettlement;
			}
		}
		return null;
	}

	public int GetExpCount()
	{
		foreach (var (num3, result) in AwardItems)
		{
			if (num3 == ExpItemId)
			{
				return result;
			}
		}
		return 0;
	}
}
