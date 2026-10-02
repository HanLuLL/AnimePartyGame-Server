using System.Collections.Generic;
using UI;
using party.protocol;

namespace GameLogic;

public class PlayerSettlement
{
	public bool IsWinner;

	public readonly BattlePlayerData PlayerData;

	public readonly Dictionary<int, int> BattleData;

	public readonly List<int> Achievements = new List<int>();

	public readonly int TotalWinCount;

	public PlayerSettlement(long WinnerTeamId, BattlePlayerData playerData, PlayerFinishAchieve achievement)
	{
		PlayerData = playerData;
		IsWinner = playerData.player.TeamId == WinnerTeamId;
		TotalWinCount = achievement.WinCount;
		BattleData = new Dictionary<int, int>
		{
			[1] = achievement.KillCount,
			[2] = achievement.TotalDamage,
			[4] = achievement.TotalDie,
			[5] = achievement.TotalInjured,
			[9] = achievement.PvpResultGold,
			[8] = achievement.PvpResultLv,
			[10] = achievement.TotalGold,
			[11] = achievement.PveTransferGold,
			[3] = achievement.PkDamageMax,
			[16] = achievement.TotalGold,
			[13] = achievement.TreatmentScore,
			[14] = achievement.MovePoint,
			[15] = -achievement.MovePoint,
			[17] = achievement.BattleDiceSixCount,
			[12] = (achievement.FinalKillBoss ? 1 : 0),
			[7] = (IsWinner ? 1 : 0)
		};
	}

	public (int, int) GetAchieve(int index)
	{
		int num = ((Achievements.Count > index) ? Achievements[index] : 0);
		if (BattleData.TryGetValue(num, out var value))
		{
			return (num, value);
		}
		return (0, 0);
	}

	public int GetBattleData(FinishAchieveType type)
	{
		return BattleData.GetValueOrDefault((int)type);
	}

	public void TryAddAchievements(int type)
	{
		if (!Achievements.Contains(type) && StaticConfigure.Achieve.InfoDict.ContainsKey(type))
		{
			Achievements.Add(type);
		}
	}

	public void SortAchievements(bool isPve)
	{
		if (isPve && Achievements.Contains(7))
		{
			Achievements.Remove(7);
		}
		Achievements.Sort(ToCompareByWight);
	}

	private int ToCompareByWight(int x, int y)
	{
		AchieveInfoConfigure achieveConfig = x.GetAchieveConfig();
		AchieveInfoConfigure achieveConfig2 = y.GetAchieveConfig();
		if (achieveConfig.Weight <= achieveConfig2.Weight)
		{
			return 1;
		}
		return -1;
	}
}
