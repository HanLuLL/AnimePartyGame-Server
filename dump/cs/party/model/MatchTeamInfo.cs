using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class MatchTeamInfo : IMessage<MatchTeamInfo>, IMessage, IEquatable<MatchTeamInfo>, IDeepCloneable<MatchTeamInfo>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum State
		{
			[OriginalName("none")]
			None,
			[OriginalName("waiting")]
			Waiting,
			[OriginalName("matching")]
			Matching,
			[OriginalName("playing")]
			Playing
		}
	}

	private static readonly MessageParser<MatchTeamInfo> _parser = new MessageParser<MatchTeamInfo>(() => new MatchTeamInfo());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private long id_;

	public const int ModeFieldNumber = 2;

	private MatchMode mode_;

	public const int StateFieldNumber = 3;

	private Types.State state_;

	public const int MapIdFieldNumber = 4;

	private int mapId_;

	public const int DifficultyFieldNumber = 5;

	private int difficulty_;

	public const int LeaderIdFieldNumber = 6;

	private long leaderId_;

	public const int PlayersFieldNumber = 7;

	private static readonly FieldCodec<Player> _repeated_players_codec = FieldCodec.ForMessage(58u, Player.Parser);

	private readonly RepeatedField<Player> players_ = new RepeatedField<Player>();

	public const int MatchTimeFieldNumber = 8;

	private long matchTime_;

	public const int PlayerReadyFieldNumber = 9;

	private static readonly MapField<long, bool>.Codec _map_playerReady_codec = new MapField<long, bool>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForBool(16u, defaultValue: false), 74u);

	private readonly MapField<long, bool> playerReady_ = new MapField<long, bool>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MatchTeamInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[69];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchMode Mode
	{
		get
		{
			return mode_;
		}
		set
		{
			mode_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.State State
	{
		get
		{
			return state_;
		}
		set
		{
			state_ = value;
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
	public long LeaderId
	{
		get
		{
			return leaderId_;
		}
		set
		{
			leaderId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Player> Players => players_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long MatchTime
	{
		get
		{
			return matchTime_;
		}
		set
		{
			matchTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, bool> PlayerReady => playerReady_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamInfo(MatchTeamInfo other)
		: this()
	{
		id_ = other.id_;
		mode_ = other.mode_;
		state_ = other.state_;
		mapId_ = other.mapId_;
		difficulty_ = other.difficulty_;
		leaderId_ = other.leaderId_;
		players_ = other.players_.Clone();
		matchTime_ = other.matchTime_;
		playerReady_ = other.playerReady_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamInfo Clone()
	{
		return new MatchTeamInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MatchTeamInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MatchTeamInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (Mode != other.Mode)
		{
			return false;
		}
		if (State != other.State)
		{
			return false;
		}
		if (MapId != other.MapId)
		{
			return false;
		}
		if (Difficulty != other.Difficulty)
		{
			return false;
		}
		if (LeaderId != other.LeaderId)
		{
			return false;
		}
		if (!players_.Equals(other.players_))
		{
			return false;
		}
		if (MatchTime != other.MatchTime)
		{
			return false;
		}
		if (!PlayerReady.Equals(other.PlayerReady))
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
		if (Id != 0L)
		{
			num ^= Id.GetHashCode();
		}
		if (Mode != MatchMode.None)
		{
			num ^= Mode.GetHashCode();
		}
		if (State != Types.State.None)
		{
			num ^= State.GetHashCode();
		}
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		if (Difficulty != 0)
		{
			num ^= Difficulty.GetHashCode();
		}
		if (LeaderId != 0L)
		{
			num ^= LeaderId.GetHashCode();
		}
		num ^= players_.GetHashCode();
		if (MatchTime != 0L)
		{
			num ^= MatchTime.GetHashCode();
		}
		num ^= PlayerReady.GetHashCode();
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
		if (Id != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(Id);
		}
		if (Mode != MatchMode.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)Mode);
		}
		if (State != Types.State.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)State);
		}
		if (MapId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(MapId);
		}
		if (Difficulty != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Difficulty);
		}
		if (LeaderId != 0L)
		{
			output.WriteRawTag(49);
			output.WriteSFixed64(LeaderId);
		}
		players_.WriteTo(ref output, _repeated_players_codec);
		if (MatchTime != 0L)
		{
			output.WriteRawTag(65);
			output.WriteSFixed64(MatchTime);
		}
		playerReady_.WriteTo(ref output, _map_playerReady_codec);
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
		if (Id != 0L)
		{
			num += 9;
		}
		if (Mode != MatchMode.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Mode);
		}
		if (State != Types.State.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)State);
		}
		if (MapId != 0)
		{
			num += 5;
		}
		if (Difficulty != 0)
		{
			num += 5;
		}
		if (LeaderId != 0L)
		{
			num += 9;
		}
		num += players_.CalculateSize(_repeated_players_codec);
		if (MatchTime != 0L)
		{
			num += 9;
		}
		num += playerReady_.CalculateSize(_map_playerReady_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MatchTeamInfo other)
	{
		if (other != null)
		{
			if (other.Id != 0L)
			{
				Id = other.Id;
			}
			if (other.Mode != MatchMode.None)
			{
				Mode = other.Mode;
			}
			if (other.State != Types.State.None)
			{
				State = other.State;
			}
			if (other.MapId != 0)
			{
				MapId = other.MapId;
			}
			if (other.Difficulty != 0)
			{
				Difficulty = other.Difficulty;
			}
			if (other.LeaderId != 0L)
			{
				LeaderId = other.LeaderId;
			}
			players_.Add(other.players_);
			if (other.MatchTime != 0L)
			{
				MatchTime = other.MatchTime;
			}
			playerReady_.MergeFrom(other.playerReady_);
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
			case 9u:
				Id = input.ReadSFixed64();
				break;
			case 16u:
				Mode = (MatchMode)input.ReadEnum();
				break;
			case 24u:
				State = (Types.State)input.ReadEnum();
				break;
			case 37u:
				MapId = input.ReadSFixed32();
				break;
			case 45u:
				Difficulty = input.ReadSFixed32();
				break;
			case 49u:
				LeaderId = input.ReadSFixed64();
				break;
			case 58u:
				players_.AddEntriesFrom(ref input, _repeated_players_codec);
				break;
			case 65u:
				MatchTime = input.ReadSFixed64();
				break;
			case 74u:
				playerReady_.AddEntriesFrom(ref input, _map_playerReady_codec);
				break;
			}
		}
	}
}
