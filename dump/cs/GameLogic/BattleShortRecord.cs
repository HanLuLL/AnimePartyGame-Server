using System;
using Core;
using Google.Protobuf.Collections;
using Tools;
using party.model;
using party.protocol;

namespace GameLogic;

public class BattleShortRecord
{
	public readonly BattleShortRecordType Type;

	public readonly long Time;

	public readonly string FormatTime;

	public readonly int Rank;

	public readonly int HeroId;

	public readonly int Index;

	public readonly int MapType;

	public readonly string ReplayId;

	public readonly string Version;

	private GameFinishS2C _gf;

	private ReplaySnapshotS2C _lastSnapshot;

	public BattleShortRecord(ShowPlayerShortFight record, BattleShortRecordType type)
	{
		Index = record.Index;
		Rank = record.Rank;
		HeroId = record.HeroId;
		Time = record.Time;
		MapType = record.MapType;
		DateTime time = (Time * 1000).StampMillisecondsToDateTime();
		FormatTime = time.ToUIDateTime_YMDHM();
		Type = type;
		ReplayId = record.ReplayId;
		Version = record.Version;
	}

	public BattleShortRecord(string replayId, GameFinishS2C gf, ReplaySnapshotS2C lastSnapshot, BattleShortRecordType type)
	{
		Type = type;
		ReplayId = replayId;
		Index = -1;
		_gf = gf;
		_lastSnapshot = lastSnapshot;
		if (gf == null)
		{
			return;
		}
		Time = gf.FinishTime;
		DateTime time = (Time * 1000).StampMillisecondsToDateTime();
		FormatTime = time.ToUIDateTime_YMDHM();
		MapType = gf.MapType;
		Version = gf.Version;
		long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		gf.Rank.TryGetValue(playerID, out Rank);
		RepeatedField<Player> repeatedField = lastSnapshot?.Room?.Players;
		if (repeatedField == null)
		{
			return;
		}
		for (int i = 0; i < repeatedField.Count; i++)
		{
			if (repeatedField[i].Id == playerID)
			{
				HeroId = repeatedField[i].Hero.HeroId;
			}
		}
	}

	public FightRecordDetail GetRecordDetail()
	{
		if (_gf == null || _lastSnapshot == null)
		{
			return null;
		}
		RepeatedField<Player> repeatedField = _lastSnapshot?.Room?.Players;
		if (repeatedField == null)
		{
			return null;
		}
		RepeatedField<PlayerFightData> repeatedField2 = new RepeatedField<PlayerFightData>();
		for (int i = 0; i < repeatedField.Count; i++)
		{
			_gf.Rank.TryGetValue(repeatedField[i].Id, out var value);
			PlayerFightData item = new PlayerFightData
			{
				PlayerId = repeatedField[i].Id,
				HeroId = (repeatedField[i].Hero?.HeroId ?? 0),
				Rank = value,
				Name = repeatedField[i].Nick,
				IsGiveUp = repeatedField[i].OffLine,
				Lv = repeatedField[i].Level
			};
			repeatedField2.Add(item);
		}
		return new FightRecordDetail(repeatedField2);
	}

	public bool IsValidReplay()
	{
		return !string.IsNullOrEmpty(ReplayId);
	}

	public bool IsValidVersion()
	{
		return (GameSettings.APP_VERSION + "-" + GameSettings.RES_VERSION).Equals(Version);
	}
}
