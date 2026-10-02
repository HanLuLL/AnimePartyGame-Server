using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using UnityEngine;

public sealed class PlayerConfigure : IMessage<PlayerConfigure>, IMessage, IEquatable<PlayerConfigure>, IDeepCloneable<PlayerConfigure>, IBufferMessage
{
	private static readonly MessageParser<PlayerConfigure> _parser = new MessageParser<PlayerConfigure>(() => new PlayerConfigure());

	private UnknownFieldSet _unknownFields;

	public const int LevelsFieldNumber = 1;

	private static readonly FieldCodec<PlayerLevelConfigure> _repeated_levels_codec = FieldCodec.ForMessage(10u, PlayerLevelConfigure.Parser);

	private readonly RepeatedField<PlayerLevelConfigure> levels_ = new RepeatedField<PlayerLevelConfigure>();

	public const int LevelDictFieldNumber = 2;

	private static readonly MapField<int, PlayerLevelConfigure>.Codec _map_levelDict_codec = new MapField<int, PlayerLevelConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PlayerLevelConfigure.Parser), 18u);

	private readonly MapField<int, PlayerLevelConfigure> levelDict_ = new MapField<int, PlayerLevelConfigure>();

	public const int RewardsFieldNumber = 3;

	private static readonly FieldCodec<PlayerRewardConfigure> _repeated_rewards_codec = FieldCodec.ForMessage(26u, PlayerRewardConfigure.Parser);

	private readonly RepeatedField<PlayerRewardConfigure> rewards_ = new RepeatedField<PlayerRewardConfigure>();

	public const int RewardDictFieldNumber = 4;

	private static readonly MapField<int, PlayerRewardConfigure>.Codec _map_rewardDict_codec = new MapField<int, PlayerRewardConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PlayerRewardConfigure.Parser), 34u);

	private readonly MapField<int, PlayerRewardConfigure> rewardDict_ = new MapField<int, PlayerRewardConfigure>();

	public const int CoverNamesFieldNumber = 5;

	private static readonly FieldCodec<PlayerCoverNameConfigure> _repeated_coverNames_codec = FieldCodec.ForMessage(42u, PlayerCoverNameConfigure.Parser);

	private readonly RepeatedField<PlayerCoverNameConfigure> coverNames_ = new RepeatedField<PlayerCoverNameConfigure>();

	public const int CoverNameDictFieldNumber = 6;

	private static readonly MapField<int, PlayerCoverNameConfigure>.Codec _map_coverNameDict_codec = new MapField<int, PlayerCoverNameConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PlayerCoverNameConfigure.Parser), 50u);

	private readonly MapField<int, PlayerCoverNameConfigure> coverNameDict_ = new MapField<int, PlayerCoverNameConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PlayerConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PlayerReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PlayerLevelConfigure> Levels => levels_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PlayerLevelConfigure> LevelDict => levelDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PlayerRewardConfigure> Rewards => rewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PlayerRewardConfigure> RewardDict => rewardDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PlayerCoverNameConfigure> CoverNames => coverNames_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PlayerCoverNameConfigure> CoverNameDict => coverNameDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerConfigure(PlayerConfigure other)
		: this()
	{
		levels_ = other.levels_.Clone();
		levelDict_ = other.levelDict_.Clone();
		rewards_ = other.rewards_.Clone();
		rewardDict_ = other.rewardDict_.Clone();
		coverNames_ = other.coverNames_.Clone();
		coverNameDict_ = other.coverNameDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerConfigure Clone()
	{
		return new PlayerConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PlayerConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PlayerConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!levels_.Equals(other.levels_))
		{
			return false;
		}
		if (!LevelDict.Equals(other.LevelDict))
		{
			return false;
		}
		if (!rewards_.Equals(other.rewards_))
		{
			return false;
		}
		if (!RewardDict.Equals(other.RewardDict))
		{
			return false;
		}
		if (!coverNames_.Equals(other.coverNames_))
		{
			return false;
		}
		if (!CoverNameDict.Equals(other.CoverNameDict))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= levels_.GetHashCode();
		num ^= LevelDict.GetHashCode();
		num ^= rewards_.GetHashCode();
		num ^= RewardDict.GetHashCode();
		num ^= coverNames_.GetHashCode();
		num ^= CoverNameDict.GetHashCode();
		if (_unknownFields != null)
		{
			num ^= _unknownFields.GetHashCode();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override string ToString()
	{
		return JsonFormatter.ToDiagnosticString(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void WriteTo(CodedOutputStream output)
	{
		output.WriteRawMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalWriteTo(ref WriteContext output)
	{
		levels_.WriteTo(ref output, _repeated_levels_codec);
		levelDict_.WriteTo(ref output, _map_levelDict_codec);
		rewards_.WriteTo(ref output, _repeated_rewards_codec);
		rewardDict_.WriteTo(ref output, _map_rewardDict_codec);
		coverNames_.WriteTo(ref output, _repeated_coverNames_codec);
		coverNameDict_.WriteTo(ref output, _map_coverNameDict_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref output);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += levels_.CalculateSize(_repeated_levels_codec);
		num += levelDict_.CalculateSize(_map_levelDict_codec);
		num += rewards_.CalculateSize(_repeated_rewards_codec);
		num += rewardDict_.CalculateSize(_map_rewardDict_codec);
		num += coverNames_.CalculateSize(_repeated_coverNames_codec);
		num += coverNameDict_.CalculateSize(_map_coverNameDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PlayerConfigure other)
	{
		if (other != null)
		{
			levels_.Add(other.levels_);
			levelDict_.MergeFrom(other.levelDict_);
			rewards_.Add(other.rewards_);
			rewardDict_.MergeFrom(other.rewardDict_);
			coverNames_.Add(other.coverNames_);
			coverNameDict_.MergeFrom(other.coverNameDict_);
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CodedInputStream input)
	{
		input.ReadRawMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalMergeFrom(ref ParseContext input)
	{
		uint num;
		while ((num = input.ReadTag()) != 0)
		{
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				break;
			case 10u:
				levels_.AddEntriesFrom(ref input, _repeated_levels_codec);
				break;
			case 18u:
				levelDict_.AddEntriesFrom(ref input, _map_levelDict_codec);
				break;
			case 26u:
				rewards_.AddEntriesFrom(ref input, _repeated_rewards_codec);
				break;
			case 34u:
				rewardDict_.AddEntriesFrom(ref input, _map_rewardDict_codec);
				break;
			case 42u:
				coverNames_.AddEntriesFrom(ref input, _repeated_coverNames_codec);
				break;
			case 50u:
				coverNameDict_.AddEntriesFrom(ref input, _map_coverNameDict_codec);
				break;
			}
		}
	}

	public static int GetConverId(List<int> existedIds)
	{
		if (existedIds == null || existedIds.Count == 0)
		{
			return GetRandomId();
		}
		int num = GetRandomId();
		while (existedIds.Contains(num))
		{
			num = GetRandomId();
		}
		return num;
		static int GetRandomId()
		{
			int index = UnityEngine.Random.Range(0, StaticConfigure.Player.CoverNames.Count);
			return StaticConfigure.Player.CoverNames[index].Id;
		}
	}
}
