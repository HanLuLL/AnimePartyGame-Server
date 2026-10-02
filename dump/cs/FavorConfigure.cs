using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FavorConfigure : IMessage<FavorConfigure>, IMessage, IEquatable<FavorConfigure>, IDeepCloneable<FavorConfigure>, IBufferMessage
{
	private static readonly MessageParser<FavorConfigure> _parser = new MessageParser<FavorConfigure>(() => new FavorConfigure());

	private UnknownFieldSet _unknownFields;

	public const int LevelsFieldNumber = 1;

	private static readonly FieldCodec<FavorLevelConfigure> _repeated_levels_codec = FieldCodec.ForMessage(10u, FavorLevelConfigure.Parser);

	private readonly RepeatedField<FavorLevelConfigure> levels_ = new RepeatedField<FavorLevelConfigure>();

	public const int LevelDictFieldNumber = 2;

	private static readonly MapField<int, FavorLevelConfigure>.Codec _map_levelDict_codec = new MapField<int, FavorLevelConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FavorLevelConfigure.Parser), 18u);

	private readonly MapField<int, FavorLevelConfigure> levelDict_ = new MapField<int, FavorLevelConfigure>();

	public const int LevelRewardsFieldNumber = 3;

	private static readonly FieldCodec<FavorLevelRewardConfigure> _repeated_levelRewards_codec = FieldCodec.ForMessage(26u, FavorLevelRewardConfigure.Parser);

	private readonly RepeatedField<FavorLevelRewardConfigure> levelRewards_ = new RepeatedField<FavorLevelRewardConfigure>();

	public const int LevelRewardDictFieldNumber = 4;

	private static readonly MapField<int, FavorLevelRewardConfigure>.Codec _map_levelRewardDict_codec = new MapField<int, FavorLevelRewardConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FavorLevelRewardConfigure.Parser), 34u);

	private readonly MapField<int, FavorLevelRewardConfigure> levelRewardDict_ = new MapField<int, FavorLevelRewardConfigure>();

	public const int BreakthroughsFieldNumber = 5;

	private static readonly FieldCodec<FavorBreakthroughConfigure> _repeated_breakthroughs_codec = FieldCodec.ForMessage(42u, FavorBreakthroughConfigure.Parser);

	private readonly RepeatedField<FavorBreakthroughConfigure> breakthroughs_ = new RepeatedField<FavorBreakthroughConfigure>();

	public const int BreakthroughDictFieldNumber = 6;

	private static readonly MapField<int, FavorBreakthroughConfigure>.Codec _map_breakthroughDict_codec = new MapField<int, FavorBreakthroughConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FavorBreakthroughConfigure.Parser), 50u);

	private readonly MapField<int, FavorBreakthroughConfigure> breakthroughDict_ = new MapField<int, FavorBreakthroughConfigure>();

	public const int GiftsFieldNumber = 7;

	private static readonly FieldCodec<FavorGiftConfigure> _repeated_gifts_codec = FieldCodec.ForMessage(58u, FavorGiftConfigure.Parser);

	private readonly RepeatedField<FavorGiftConfigure> gifts_ = new RepeatedField<FavorGiftConfigure>();

	public const int GiftDictFieldNumber = 8;

	private static readonly MapField<int, FavorGiftConfigure>.Codec _map_giftDict_codec = new MapField<int, FavorGiftConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FavorGiftConfigure.Parser), 66u);

	private readonly MapField<int, FavorGiftConfigure> giftDict_ = new MapField<int, FavorGiftConfigure>();

	public const int WaysFieldNumber = 9;

	private static readonly FieldCodec<FavorWayConfigure> _repeated_ways_codec = FieldCodec.ForMessage(74u, FavorWayConfigure.Parser);

	private readonly RepeatedField<FavorWayConfigure> ways_ = new RepeatedField<FavorWayConfigure>();

	public const int WayDictFieldNumber = 10;

	private static readonly MapField<int, FavorWayConfigure>.Codec _map_wayDict_codec = new MapField<int, FavorWayConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FavorWayConfigure.Parser), 82u);

	private readonly MapField<int, FavorWayConfigure> wayDict_ = new MapField<int, FavorWayConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FavorConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FavorReflection.Descriptor.MessageTypes[6];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FavorLevelConfigure> Levels => levels_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FavorLevelConfigure> LevelDict => levelDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FavorLevelRewardConfigure> LevelRewards => levelRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FavorLevelRewardConfigure> LevelRewardDict => levelRewardDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FavorBreakthroughConfigure> Breakthroughs => breakthroughs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FavorBreakthroughConfigure> BreakthroughDict => breakthroughDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FavorGiftConfigure> Gifts => gifts_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FavorGiftConfigure> GiftDict => giftDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FavorWayConfigure> Ways => ways_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FavorWayConfigure> WayDict => wayDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FavorConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FavorConfigure(FavorConfigure other)
		: this()
	{
		levels_ = other.levels_.Clone();
		levelDict_ = other.levelDict_.Clone();
		levelRewards_ = other.levelRewards_.Clone();
		levelRewardDict_ = other.levelRewardDict_.Clone();
		breakthroughs_ = other.breakthroughs_.Clone();
		breakthroughDict_ = other.breakthroughDict_.Clone();
		gifts_ = other.gifts_.Clone();
		giftDict_ = other.giftDict_.Clone();
		ways_ = other.ways_.Clone();
		wayDict_ = other.wayDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FavorConfigure Clone()
	{
		return new FavorConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FavorConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FavorConfigure other)
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
		if (!levelRewards_.Equals(other.levelRewards_))
		{
			return false;
		}
		if (!LevelRewardDict.Equals(other.LevelRewardDict))
		{
			return false;
		}
		if (!breakthroughs_.Equals(other.breakthroughs_))
		{
			return false;
		}
		if (!BreakthroughDict.Equals(other.BreakthroughDict))
		{
			return false;
		}
		if (!gifts_.Equals(other.gifts_))
		{
			return false;
		}
		if (!GiftDict.Equals(other.GiftDict))
		{
			return false;
		}
		if (!ways_.Equals(other.ways_))
		{
			return false;
		}
		if (!WayDict.Equals(other.WayDict))
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
		num ^= levelRewards_.GetHashCode();
		num ^= LevelRewardDict.GetHashCode();
		num ^= breakthroughs_.GetHashCode();
		num ^= BreakthroughDict.GetHashCode();
		num ^= gifts_.GetHashCode();
		num ^= GiftDict.GetHashCode();
		num ^= ways_.GetHashCode();
		num ^= WayDict.GetHashCode();
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
		levelRewards_.WriteTo(ref output, _repeated_levelRewards_codec);
		levelRewardDict_.WriteTo(ref output, _map_levelRewardDict_codec);
		breakthroughs_.WriteTo(ref output, _repeated_breakthroughs_codec);
		breakthroughDict_.WriteTo(ref output, _map_breakthroughDict_codec);
		gifts_.WriteTo(ref output, _repeated_gifts_codec);
		giftDict_.WriteTo(ref output, _map_giftDict_codec);
		ways_.WriteTo(ref output, _repeated_ways_codec);
		wayDict_.WriteTo(ref output, _map_wayDict_codec);
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
		num += levelRewards_.CalculateSize(_repeated_levelRewards_codec);
		num += levelRewardDict_.CalculateSize(_map_levelRewardDict_codec);
		num += breakthroughs_.CalculateSize(_repeated_breakthroughs_codec);
		num += breakthroughDict_.CalculateSize(_map_breakthroughDict_codec);
		num += gifts_.CalculateSize(_repeated_gifts_codec);
		num += giftDict_.CalculateSize(_map_giftDict_codec);
		num += ways_.CalculateSize(_repeated_ways_codec);
		num += wayDict_.CalculateSize(_map_wayDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FavorConfigure other)
	{
		if (other != null)
		{
			levels_.Add(other.levels_);
			levelDict_.MergeFrom(other.levelDict_);
			levelRewards_.Add(other.levelRewards_);
			levelRewardDict_.MergeFrom(other.levelRewardDict_);
			breakthroughs_.Add(other.breakthroughs_);
			breakthroughDict_.MergeFrom(other.breakthroughDict_);
			gifts_.Add(other.gifts_);
			giftDict_.MergeFrom(other.giftDict_);
			ways_.Add(other.ways_);
			wayDict_.MergeFrom(other.wayDict_);
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
				levelRewards_.AddEntriesFrom(ref input, _repeated_levelRewards_codec);
				break;
			case 34u:
				levelRewardDict_.AddEntriesFrom(ref input, _map_levelRewardDict_codec);
				break;
			case 42u:
				breakthroughs_.AddEntriesFrom(ref input, _repeated_breakthroughs_codec);
				break;
			case 50u:
				breakthroughDict_.AddEntriesFrom(ref input, _map_breakthroughDict_codec);
				break;
			case 58u:
				gifts_.AddEntriesFrom(ref input, _repeated_gifts_codec);
				break;
			case 66u:
				giftDict_.AddEntriesFrom(ref input, _map_giftDict_codec);
				break;
			case 74u:
				ways_.AddEntriesFrom(ref input, _repeated_ways_codec);
				break;
			case 82u:
				wayDict_.AddEntriesFrom(ref input, _map_wayDict_codec);
				break;
			}
		}
	}
}
