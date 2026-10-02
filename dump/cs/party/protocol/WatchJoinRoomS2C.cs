using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class WatchJoinRoomS2C : IMessage<WatchJoinRoomS2C>, IMessage, IEquatable<WatchJoinRoomS2C>, IDeepCloneable<WatchJoinRoomS2C>, IBufferMessage
{
	private static readonly MessageParser<WatchJoinRoomS2C> _parser = new MessageParser<WatchJoinRoomS2C>(() => new WatchJoinRoomS2C());

	private UnknownFieldSet _unknownFields;

	public const int RoomIdFieldNumber = 1;

	private long roomId_;

	public const int MapIdFieldNumber = 2;

	private int mapId_;

	public const int PlayersFieldNumber = 3;

	private static readonly FieldCodec<Player> _repeated_players_codec = FieldCodec.ForMessage(26u, Player.Parser);

	private readonly RepeatedField<Player> players_ = new RepeatedField<Player>();

	public const int WatchCountFieldNumber = 4;

	private int watchCount_;

	public const int StartTimeFieldNumber = 5;

	private long startTime_;

	public const int MapTypeFieldNumber = 6;

	private int mapType_;

	public const int BoxFieldNumber = 7;

	private HeroBarBox box_;

	public const int MapIndexFieldNumber = 8;

	private int mapIndex_;

	public const int DifficultyFieldNumber = 9;

	private int difficulty_;

	public const int MapDifficultyIdFieldNumber = 10;

	private int mapDifficultyId_;

	public const int RoomServerIdFieldNumber = 11;

	private int roomServerId_;

	public const int RoomTermIdsFieldNumber = 12;

	private static readonly FieldCodec<int> _repeated_roomTermIds_codec = FieldCodec.ForSFixed32(98u);

	private readonly RepeatedField<int> roomTermIds_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<WatchJoinRoomS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[42];

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
	public int MapId
	{
		get
		{
			return mapId_;
		}
		set
		{
			mapId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Player> Players => players_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WatchCount
	{
		get
		{
			return watchCount_;
		}
		set
		{
			watchCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long StartTime
	{
		get
		{
			return startTime_;
		}
		set
		{
			startTime_ = value;
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
	public HeroBarBox Box
	{
		get
		{
			return box_;
		}
		set
		{
			box_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapIndex
	{
		get
		{
			return mapIndex_;
		}
		set
		{
			mapIndex_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Difficulty
	{
		get
		{
			return difficulty_;
		}
		set
		{
			difficulty_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapDifficultyId
	{
		get
		{
			return mapDifficultyId_;
		}
		set
		{
			mapDifficultyId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoomServerId
	{
		get
		{
			return roomServerId_;
		}
		set
		{
			roomServerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RoomTermIds => roomTermIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WatchJoinRoomS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WatchJoinRoomS2C(WatchJoinRoomS2C other)
		: this()
	{
		roomId_ = other.roomId_;
		mapId_ = other.mapId_;
		players_ = other.players_.Clone();
		watchCount_ = other.watchCount_;
		startTime_ = other.startTime_;
		mapType_ = other.mapType_;
		box_ = ((other.box_ != null) ? other.box_.Clone() : null);
		mapIndex_ = other.mapIndex_;
		difficulty_ = other.difficulty_;
		mapDifficultyId_ = other.mapDifficultyId_;
		roomServerId_ = other.roomServerId_;
		roomTermIds_ = other.roomTermIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WatchJoinRoomS2C Clone()
	{
		return new WatchJoinRoomS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WatchJoinRoomS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(WatchJoinRoomS2C other)
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
		if (MapId != other.MapId)
		{
			return false;
		}
		if (!players_.Equals(other.players_))
		{
			return false;
		}
		if (WatchCount != other.WatchCount)
		{
			return false;
		}
		if (StartTime != other.StartTime)
		{
			return false;
		}
		if (MapType != other.MapType)
		{
			return false;
		}
		if (!object.Equals(Box, other.Box))
		{
			return false;
		}
		if (MapIndex != other.MapIndex)
		{
			return false;
		}
		if (Difficulty != other.Difficulty)
		{
			return false;
		}
		if (MapDifficultyId != other.MapDifficultyId)
		{
			return false;
		}
		if (RoomServerId != other.RoomServerId)
		{
			return false;
		}
		if (!roomTermIds_.Equals(other.roomTermIds_))
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
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		num ^= players_.GetHashCode();
		if (WatchCount != 0)
		{
			num ^= WatchCount.GetHashCode();
		}
		if (StartTime != 0L)
		{
			num ^= StartTime.GetHashCode();
		}
		if (MapType != 0)
		{
			num ^= MapType.GetHashCode();
		}
		if (box_ != null)
		{
			num ^= Box.GetHashCode();
		}
		if (MapIndex != 0)
		{
			num ^= MapIndex.GetHashCode();
		}
		if (Difficulty != 0)
		{
			num ^= Difficulty.GetHashCode();
		}
		if (MapDifficultyId != 0)
		{
			num ^= MapDifficultyId.GetHashCode();
		}
		if (RoomServerId != 0)
		{
			num ^= RoomServerId.GetHashCode();
		}
		num ^= roomTermIds_.GetHashCode();
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
		if (MapId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MapId);
		}
		players_.WriteTo(ref output, _repeated_players_codec);
		if (WatchCount != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(WatchCount);
		}
		if (StartTime != 0L)
		{
			output.WriteRawTag(41);
			output.WriteSFixed64(StartTime);
		}
		if (MapType != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(MapType);
		}
		if (box_ != null)
		{
			output.WriteRawTag(58);
			output.WriteMessage(Box);
		}
		if (MapIndex != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(MapIndex);
		}
		if (Difficulty != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(Difficulty);
		}
		if (MapDifficultyId != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(MapDifficultyId);
		}
		if (RoomServerId != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(RoomServerId);
		}
		roomTermIds_.WriteTo(ref output, _repeated_roomTermIds_codec);
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
		if (MapId != 0)
		{
			num += 5;
		}
		num += players_.CalculateSize(_repeated_players_codec);
		if (WatchCount != 0)
		{
			num += 5;
		}
		if (StartTime != 0L)
		{
			num += 9;
		}
		if (MapType != 0)
		{
			num += 5;
		}
		if (box_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Box);
		}
		if (MapIndex != 0)
		{
			num += 5;
		}
		if (Difficulty != 0)
		{
			num += 5;
		}
		if (MapDifficultyId != 0)
		{
			num += 5;
		}
		if (RoomServerId != 0)
		{
			num += 5;
		}
		num += roomTermIds_.CalculateSize(_repeated_roomTermIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(WatchJoinRoomS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.RoomId != 0L)
		{
			RoomId = other.RoomId;
		}
		if (other.MapId != 0)
		{
			MapId = other.MapId;
		}
		players_.Add(other.players_);
		if (other.WatchCount != 0)
		{
			WatchCount = other.WatchCount;
		}
		if (other.StartTime != 0L)
		{
			StartTime = other.StartTime;
		}
		if (other.MapType != 0)
		{
			MapType = other.MapType;
		}
		if (other.box_ != null)
		{
			if (box_ == null)
			{
				Box = new HeroBarBox();
			}
			Box.MergeFrom(other.Box);
		}
		if (other.MapIndex != 0)
		{
			MapIndex = other.MapIndex;
		}
		if (other.Difficulty != 0)
		{
			Difficulty = other.Difficulty;
		}
		if (other.MapDifficultyId != 0)
		{
			MapDifficultyId = other.MapDifficultyId;
		}
		if (other.RoomServerId != 0)
		{
			RoomServerId = other.RoomServerId;
		}
		roomTermIds_.Add(other.roomTermIds_);
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
			case 21u:
				MapId = input.ReadSFixed32();
				break;
			case 26u:
				players_.AddEntriesFrom(ref input, _repeated_players_codec);
				break;
			case 37u:
				WatchCount = input.ReadSFixed32();
				break;
			case 41u:
				StartTime = input.ReadSFixed64();
				break;
			case 53u:
				MapType = input.ReadSFixed32();
				break;
			case 58u:
				if (box_ == null)
				{
					Box = new HeroBarBox();
				}
				input.ReadMessage(Box);
				break;
			case 69u:
				MapIndex = input.ReadSFixed32();
				break;
			case 77u:
				Difficulty = input.ReadSFixed32();
				break;
			case 85u:
				MapDifficultyId = input.ReadSFixed32();
				break;
			case 93u:
				RoomServerId = input.ReadSFixed32();
				break;
			case 98u:
			case 101u:
				roomTermIds_.AddEntriesFrom(ref input, _repeated_roomTermIds_codec);
				break;
			}
		}
	}
}
