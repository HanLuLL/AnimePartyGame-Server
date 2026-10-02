using System.Collections.Generic;
using Core.Scene;
using GameLogic.Replay;
using Tools;
using UnityEngine;

namespace GameLogic;

public static class BattleConfig
{
	public static readonly float CameraBlendTime = (float)StaticGlobalData.GAME_CAMERA_SWITCH_TIME / 1000f;

	public static int BaseTeamId = 10;

	public static int MonsterTeamId = 0;

	public static int AsymmetricalAttackerTeamId = 10;

	public static int AsymmetricalDefenderTeamId = 20;

	public static int AsymmetricalDefenderWinRound = StaticConfigure.GameMode.AsymmetricalBattles[0].AsymmetricalDefenderWinRound;

	public static int AsymmetricalAttackerWinScore = StaticConfigure.GameMode.AsymmetricalBattles[0].AsymmetricalAttackerWinScore;

	public static int AsymmetricalPkRobScore = 1;

	public static int AsymmetricalAttackerReviveRound = StaticConfigure.GameMode.AsymmetricalBattles[0].AsymmetricalAttackerReviveRound;

	public static int AsymmetricalReviveGolds = 10;

	public static int AsymmetricalSpeedRound = StaticConfigure.GameMode.AsymmetricalBattles[0].AsymmetricalSpeedRound;

	public static int AsymmetricalFirstStageGiftCount = 1;

	public static int AsymmetricalFinalStageGiftCount = 2;

	public static List<int> StoryMapIds = new List<int> { 82013, 82015 };

	public static readonly int LuckyStarRedTeamId = 100;

	public static readonly Color LuckyStarRedColor = new Color(1f, 0.07f, 0.5f, 1f);

	public static readonly int LuckyStarGreenTeamId = 110;

	public static readonly Color LuckyStarGreenColor = new Color(0f, 0.89f, 0.77f, 1f);

	public static float RoleAnimatorSpeed
	{
		get
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
			{
				return 1f;
			}
			ChoosingTimeLimitgamespeedConfigure activeGameSpeed = GetActiveGameSpeed();
			if (activeGameSpeed == null)
			{
				return 1f;
			}
			return activeGameSpeed.AnimSpeed * GetReplaySpeedMultiplier();
		}
	}

	public static float DiceAnimatorSpeed
	{
		get
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
			{
				return 1f;
			}
			ChoosingTimeLimitgamespeedConfigure activeGameSpeed = GetActiveGameSpeed();
			if (activeGameSpeed == null)
			{
				return 1f;
			}
			return activeGameSpeed.DiceSpeed * GetReplaySpeedMultiplier();
		}
	}

	public static float EffectSpeed
	{
		get
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
			{
				return 1f;
			}
			ChoosingTimeLimitgamespeedConfigure activeGameSpeed = GetActiveGameSpeed();
			if (activeGameSpeed == null)
			{
				return 1f;
			}
			return activeGameSpeed.VfxSpeed * GetReplaySpeedMultiplier();
		}
	}

	public static float OtherSpeed
	{
		get
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
			{
				return 1f;
			}
			ChoosingTimeLimitgamespeedConfigure activeGameSpeed = GetActiveGameSpeed();
			if (activeGameSpeed == null)
			{
				return 1f;
			}
			return activeGameSpeed.PerformSpeed * GetReplaySpeedMultiplier();
		}
	}

	public static bool IsPVE(int MapType)
	{
		if (MapType != 4 && MapType != 9 && MapType != 6 && MapType != 10)
		{
			return MapType == 12;
		}
		return true;
	}

	public static bool IsPVE(MapModeType MapType)
	{
		if (MapType != MapModeType.Pve && MapType != MapModeType.PracticePve && MapType != MapModeType.CampaignPve && MapType != MapModeType.Pvenovice)
		{
			return MapType == MapModeType.MutatorPve;
		}
		return true;
	}

	public static bool IsPVP(int MapType)
	{
		if (MapType != 1 && MapType != 8 && MapType != 5)
		{
			return MapType == 3;
		}
		return true;
	}

	public static bool IsCampaign(int MapType)
	{
		if (MapType != 5)
		{
			return MapType == 6;
		}
		return true;
	}

	public static bool IsPractice(int MapType)
	{
		if (MapType != 8)
		{
			return MapType == 9;
		}
		return true;
	}

	public static bool IsSingleGameModel(int MapType)
	{
		if (!IsCampaign(MapType) && !IsPractice(MapType))
		{
			return IsNovice(MapType);
		}
		return true;
	}

	public static bool IsAsymmetricalBattle(int MapType)
	{
		return MapType == 7;
	}

	public static bool IsNovice(int MapType)
	{
		if (MapType != 2)
		{
			return MapType == 10;
		}
		return true;
	}

	public static bool IsLuckyStarBattle(int MapType)
	{
		return MapType == 11;
	}

	public static bool IsMutatorPve(int MapType)
	{
		return MapType == 12;
	}

	private static float GetReplaySpeedMultiplier()
	{
		ReplaySession replaySession = SimpleSingletonProvider<GameLogicManager>.inst?.replay?.Session;
		if (replaySession != null && replaySession.IsReplay)
		{
			return replaySession.PlaySpeedMultiplier;
		}
		return 1f;
	}

	private static ChoosingTimeLimitgamespeedConfigure GetActiveGameSpeed()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst?.room?.curRoomInfo;
		if (roomInfo == null)
		{
			return null;
		}
		int key = roomInfo.speedType;
		ReplaySession replaySession = SimpleSingletonProvider<GameLogicManager>.inst?.replay?.Session;
		if (replaySession != null && replaySession.IsReplay)
		{
			key = 2;
		}
		return StaticConfigure.ChoosingTimeLimit.GamespeedDict.GetValueOrDefault(key);
	}
}
