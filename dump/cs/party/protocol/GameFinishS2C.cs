using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class GameFinishS2C : IMessage<GameFinishS2C>, IMessage, IEquatable<GameFinishS2C>, IDeepCloneable<GameFinishS2C>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public sealed class FinishAchieve : IMessage<FinishAchieve>, IMessage, IEquatable<FinishAchieve>, IDeepCloneable<FinishAchieve>, IBufferMessage
		{
			private static readonly MessageParser<FinishAchieve> _parser = new MessageParser<FinishAchieve>(() => new FinishAchieve());

			private UnknownFieldSet _unknownFields;

			public const int TypeFieldNumber = 1;

			private FinishAchieveType type_;

			public const int PlayerIdFieldNumber = 2;

			private long playerId_;

			public const int ValueFieldNumber = 3;

			private int value_;

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public static MessageParser<FinishAchieve> Parser => _parser;

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public static MessageDescriptor Descriptor => GameFinishS2C.Descriptor.NestedTypes[0];

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			MessageDescriptor IMessage.Descriptor => Descriptor;

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public FinishAchieveType Type
			{
				get
				{
					return type_;
				}
				set
				{
					type_ = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public long PlayerId
			{
				get
				{
					return playerId_;
				}
				set
				{
					playerId_ = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public int Value
			{
				get
				{
					return value_;
				}
				set
				{
					value_ = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public FinishAchieve()
			{
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public FinishAchieve(FinishAchieve other)
				: this()
			{
				type_ = other.type_;
				playerId_ = other.playerId_;
				value_ = other.value_;
				_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public FinishAchieve Clone()
			{
				return new FinishAchieve(this);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public override bool Equals(object other)
			{
				return Equals(other as FinishAchieve);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public bool Equals(FinishAchieve other)
			{
				if (other == null)
				{
					return false;
				}
				if (other == this)
				{
					return true;
				}
				if (Type != other.Type)
				{
					return false;
				}
				if (PlayerId != other.PlayerId)
				{
					return false;
				}
				if (Value != other.Value)
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
				if (Type != FinishAchieveType.None)
				{
					num ^= Type.GetHashCode();
				}
				if (PlayerId != 0L)
				{
					num ^= PlayerId.GetHashCode();
				}
				if (Value != 0)
				{
					num ^= Value.GetHashCode();
				}
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
				if (Type != FinishAchieveType.None)
				{
					output.WriteRawTag(8);
					output.WriteEnum((int)Type);
				}
				if (PlayerId != 0L)
				{
					output.WriteRawTag(17);
					output.WriteSFixed64(PlayerId);
				}
				if (Value != 0)
				{
					output.WriteRawTag(29);
					output.WriteSFixed32(Value);
				}
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
				if (Type != FinishAchieveType.None)
				{
					num += 1 + CodedOutputStream.ComputeEnumSize((int)Type);
				}
				if (PlayerId != 0L)
				{
					num += 9;
				}
				if (Value != 0)
				{
					num += 5;
				}
				if (_unknownFields != null)
				{
					num += _unknownFields.CalculateSize();
				}
				return num;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public void MergeFrom(FinishAchieve other)
			{
				if (other != null)
				{
					if (other.Type != FinishAchieveType.None)
					{
						Type = other.Type;
					}
					if (other.PlayerId != 0L)
					{
						PlayerId = other.PlayerId;
					}
					if (other.Value != 0)
					{
						Value = other.Value;
					}
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
					case 8u:
						Type = (FinishAchieveType)input.ReadEnum();
						break;
					case 17u:
						PlayerId = input.ReadSFixed64();
						break;
					case 29u:
						Value = input.ReadSFixed32();
						break;
					}
				}
			}
		}
	}

	private static readonly MessageParser<GameFinishS2C> _parser = new MessageParser<GameFinishS2C>(() => new GameFinishS2C());

	private UnknownFieldSet _unknownFields;

	public const int RoomIdFieldNumber = 1;

	private long roomId_;

	public const int WinerFieldNumber = 2;

	private long winer_;

	public const int FinishTimeFieldNumber = 3;

	private long finishTime_;

	public const int AchieveFieldNumber = 4;

	private static readonly FieldCodec<Types.FinishAchieve> _repeated_achieve_codec = FieldCodec.ForMessage(34u, Types.FinishAchieve.Parser);

	private readonly RepeatedField<Types.FinishAchieve> achieve_ = new RepeatedField<Types.FinishAchieve>();

	public const int ActivityIdFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_activityId_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> activityId_ = new RepeatedField<int>();

	public const int AwardsFieldNumber = 6;

	private static readonly MapField<int, int>.Codec _map_awards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 50u);

	private readonly MapField<int, int> awards_ = new MapField<int, int>();

	public const int NewAchieveFieldNumber = 7;

	private static readonly MapField<long, PlayerFinishAchieve>.Codec _map_newAchieve_codec = new MapField<long, PlayerFinishAchieve>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, PlayerFinishAchieve.Parser), 58u);

	private readonly MapField<long, PlayerFinishAchieve> newAchieve_ = new MapField<long, PlayerFinishAchieve>();

	public const int CampScoresFieldNumber = 8;

	private Int32KvPair campScores_;

	public const int RookieBonusAwardsFieldNumber = 9;

	private static readonly MapField<int, int>.Codec _map_rookieBonusAwards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 74u);

	private readonly MapField<int, int> rookieBonusAwards_ = new MapField<int, int>();

	public const int ReturnBonusAwardsFieldNumber = 10;

	private static readonly MapField<int, int>.Codec _map_returnBonusAwards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 82u);

	private readonly MapField<int, int> returnBonusAwards_ = new MapField<int, int>();

	public const int ReplayIdFieldNumber = 11;

	private string replayId_ = "";

	public const int VersionFieldNumber = 12;

	private string version_ = "";

	public const int MapTypeFieldNumber = 13;

	private int mapType_;

	public const int RankFieldNumber = 14;

	private static readonly MapField<long, int>.Codec _map_rank_codec = new MapField<long, int>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForSFixed32(21u, 0), 114u);

	private readonly MapField<long, int> rank_ = new MapField<long, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GameFinishS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[384];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long RoomId
	{
		get
		{
			return roomId_;
		}
		set
		{
			roomId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Winer
	{
		get
		{
			return winer_;
		}
		set
		{
			winer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long FinishTime
	{
		get
		{
			return finishTime_;
		}
		set
		{
			finishTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Types.FinishAchieve> Achieve => achieve_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> ActivityId => activityId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Awards => awards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, PlayerFinishAchieve> NewAchieve => newAchieve_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Int32KvPair CampScores
	{
		get
		{
			return campScores_;
		}
		set
		{
			campScores_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> RookieBonusAwards => rookieBonusAwards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> ReturnBonusAwards => returnBonusAwards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ReplayId
	{
		get
		{
			return replayId_;
		}
		set
		{
			replayId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Version
	{
		get
		{
			return version_;
		}
		set
		{
			version_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapType
	{
		get
		{
			return mapType_;
		}
		set
		{
			mapType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, int> Rank => rank_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameFinishS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameFinishS2C(GameFinishS2C other)
		: this()
	{
		roomId_ = other.roomId_;
		winer_ = other.winer_;
		finishTime_ = other.finishTime_;
		achieve_ = other.achieve_.Clone();
		activityId_ = other.activityId_.Clone();
		awards_ = other.awards_.Clone();
		newAchieve_ = other.newAchieve_.Clone();
		campScores_ = ((other.campScores_ != null) ? other.campScores_.Clone() : null);
		rookieBonusAwards_ = other.rookieBonusAwards_.Clone();
		returnBonusAwards_ = other.returnBonusAwards_.Clone();
		replayId_ = other.replayId_;
		version_ = other.version_;
		mapType_ = other.mapType_;
		rank_ = other.rank_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameFinishS2C Clone()
	{
		return new GameFinishS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GameFinishS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GameFinishS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (RoomId != other.RoomId)
		{
			return false;
		}
		if (Winer != other.Winer)
		{
			return false;
		}
		if (FinishTime != other.FinishTime)
		{
			return false;
		}
		if (!achieve_.Equals(other.achieve_))
		{
			return false;
		}
		if (!activityId_.Equals(other.activityId_))
		{
			return false;
		}
		if (!Awards.Equals(other.Awards))
		{
			return false;
		}
		if (!NewAchieve.Equals(other.NewAchieve))
		{
			return false;
		}
		if (!object.Equals(CampScores, other.CampScores))
		{
			return false;
		}
		if (!RookieBonusAwards.Equals(other.RookieBonusAwards))
		{
			return false;
		}
		if (!ReturnBonusAwards.Equals(other.ReturnBonusAwards))
		{
			return false;
		}
		if (ReplayId != other.ReplayId)
		{
			return false;
		}
		if (Version != other.Version)
		{
			return false;
		}
		if (MapType != other.MapType)
		{
			return false;
		}
		if (!Rank.Equals(other.Rank))
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
		if (RoomId != 0L)
		{
			num ^= RoomId.GetHashCode();
		}
		if (Winer != 0L)
		{
			num ^= Winer.GetHashCode();
		}
		if (FinishTime != 0L)
		{
			num ^= FinishTime.GetHashCode();
		}
		num ^= achieve_.GetHashCode();
		num ^= activityId_.GetHashCode();
		num ^= Awards.GetHashCode();
		num ^= NewAchieve.GetHashCode();
		if (campScores_ != null)
		{
			num ^= CampScores.GetHashCode();
		}
		num ^= RookieBonusAwards.GetHashCode();
		num ^= ReturnBonusAwards.GetHashCode();
		if (ReplayId.Length != 0)
		{
			num ^= ReplayId.GetHashCode();
		}
		if (Version.Length != 0)
		{
			num ^= Version.GetHashCode();
		}
		if (MapType != 0)
		{
			num ^= MapType.GetHashCode();
		}
		num ^= Rank.GetHashCode();
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
		if (RoomId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(RoomId);
		}
		if (Winer != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(Winer);
		}
		if (FinishTime != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(FinishTime);
		}
		achieve_.WriteTo(ref output, _repeated_achieve_codec);
		activityId_.WriteTo(ref output, _repeated_activityId_codec);
		awards_.WriteTo(ref output, _map_awards_codec);
		newAchieve_.WriteTo(ref output, _map_newAchieve_codec);
		if (campScores_ != null)
		{
			output.WriteRawTag(66);
			output.WriteMessage(CampScores);
		}
		rookieBonusAwards_.WriteTo(ref output, _map_rookieBonusAwards_codec);
		returnBonusAwards_.WriteTo(ref output, _map_returnBonusAwards_codec);
		if (ReplayId.Length != 0)
		{
			output.WriteRawTag(90);
			output.WriteString(ReplayId);
		}
		if (Version.Length != 0)
		{
			output.WriteRawTag(98);
			output.WriteString(Version);
		}
		if (MapType != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(MapType);
		}
		rank_.WriteTo(ref output, _map_rank_codec);
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
		if (RoomId != 0L)
		{
			num += 9;
		}
		if (Winer != 0L)
		{
			num += 9;
		}
		if (FinishTime != 0L)
		{
			num += 9;
		}
		num += achieve_.CalculateSize(_repeated_achieve_codec);
		num += activityId_.CalculateSize(_repeated_activityId_codec);
		num += awards_.CalculateSize(_map_awards_codec);
		num += newAchieve_.CalculateSize(_map_newAchieve_codec);
		if (campScores_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CampScores);
		}
		num += rookieBonusAwards_.CalculateSize(_map_rookieBonusAwards_codec);
		num += returnBonusAwards_.CalculateSize(_map_returnBonusAwards_codec);
		if (ReplayId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ReplayId);
		}
		if (Version.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Version);
		}
		if (MapType != 0)
		{
			num += 5;
		}
		num += rank_.CalculateSize(_map_rank_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GameFinishS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.RoomId != 0L)
		{
			RoomId = other.RoomId;
		}
		if (other.Winer != 0L)
		{
			Winer = other.Winer;
		}
		if (other.FinishTime != 0L)
		{
			FinishTime = other.FinishTime;
		}
		achieve_.Add(other.achieve_);
		activityId_.Add(other.activityId_);
		awards_.MergeFrom(other.awards_);
		newAchieve_.MergeFrom(other.newAchieve_);
		if (other.campScores_ != null)
		{
			if (campScores_ == null)
			{
				CampScores = new Int32KvPair();
			}
			CampScores.MergeFrom(other.CampScores);
		}
		rookieBonusAwards_.MergeFrom(other.rookieBonusAwards_);
		returnBonusAwards_.MergeFrom(other.returnBonusAwards_);
		if (other.ReplayId.Length != 0)
		{
			ReplayId = other.ReplayId;
		}
		if (other.Version.Length != 0)
		{
			Version = other.Version;
		}
		if (other.MapType != 0)
		{
			MapType = other.MapType;
		}
		rank_.MergeFrom(other.rank_);
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			case 9u:
				RoomId = input.ReadSFixed64();
				break;
			case 17u:
				Winer = input.ReadSFixed64();
				break;
			case 25u:
				FinishTime = input.ReadSFixed64();
				break;
			case 34u:
				achieve_.AddEntriesFrom(ref input, _repeated_achieve_codec);
				break;
			case 42u:
			case 45u:
				activityId_.AddEntriesFrom(ref input, _repeated_activityId_codec);
				break;
			case 50u:
				awards_.AddEntriesFrom(ref input, _map_awards_codec);
				break;
			case 58u:
				newAchieve_.AddEntriesFrom(ref input, _map_newAchieve_codec);
				break;
			case 66u:
				if (campScores_ == null)
				{
					CampScores = new Int32KvPair();
				}
				input.ReadMessage(CampScores);
				break;
			case 74u:
				rookieBonusAwards_.AddEntriesFrom(ref input, _map_rookieBonusAwards_codec);
				break;
			case 82u:
				returnBonusAwards_.AddEntriesFrom(ref input, _map_returnBonusAwards_codec);
				break;
			case 90u:
				ReplayId = input.ReadString();
				break;
			case 98u:
				Version = input.ReadString();
				break;
			case 109u:
				MapType = input.ReadSFixed32();
				break;
			case 114u:
				rank_.AddEntriesFrom(ref input, _map_rank_codec);
				break;
			}
		}
	}
}
