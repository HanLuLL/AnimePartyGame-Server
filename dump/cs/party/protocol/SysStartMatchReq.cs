using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SysStartMatchReq : IMessage<SysStartMatchReq>, IMessage, IEquatable<SysStartMatchReq>, IDeepCloneable<SysStartMatchReq>, IBufferMessage
{
	private static readonly MessageParser<SysStartMatchReq> _parser = new MessageParser<SysStartMatchReq>(() => new SysStartMatchReq());

	private UnknownFieldSet _unknownFields;

	public const int TeamIdFieldNumber = 1;

	private long teamId_;

	public const int ModeFieldNumber = 2;

	private MatchMode mode_;

	public const int MapIdFieldNumber = 3;

	private int mapId_;

	public const int DifficultyFieldNumber = 4;

	private int difficulty_;

	public const int PlayersFieldNumber = 5;

	private static readonly FieldCodec<Player> _repeated_players_codec = FieldCodec.ForMessage(42u, Player.Parser);

	private readonly RepeatedField<Player> players_ = new RepeatedField<Player>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysStartMatchReq> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => InteriorReflection.Descriptor.MessageTypes[8];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long TeamId
	{
		get
		{
			return teamId_;
		}
		set
		{
			teamId_ = value;
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
	public RepeatedField<Player> Players => players_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysStartMatchReq()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysStartMatchReq(SysStartMatchReq other)
		: this()
	{
		teamId_ = other.teamId_;
		mode_ = other.mode_;
		mapId_ = other.mapId_;
		difficulty_ = other.difficulty_;
		players_ = other.players_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysStartMatchReq Clone()
	{
		return new SysStartMatchReq(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysStartMatchReq);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysStartMatchReq other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (TeamId != other.TeamId)
		{
			return false;
		}
		if (Mode != other.Mode)
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
		if (!players_.Equals(other.players_))
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
		if (TeamId != 0L)
		{
			num ^= TeamId.GetHashCode();
		}
		if (Mode != MatchMode.None)
		{
			num ^= Mode.GetHashCode();
		}
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		if (Difficulty != 0)
		{
			num ^= Difficulty.GetHashCode();
		}
		num ^= players_.GetHashCode();
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
		if (TeamId != 0L)
		{
			output.WriteRawTag(8);
			output.WriteInt64(TeamId);
		}
		if (Mode != MatchMode.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)Mode);
		}
		if (MapId != 0)
		{
			output.WriteRawTag(24);
			output.WriteInt32(MapId);
		}
		if (Difficulty != 0)
		{
			output.WriteRawTag(32);
			output.WriteInt32(Difficulty);
		}
		players_.WriteTo(ref output, _repeated_players_codec);
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
		if (TeamId != 0L)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(TeamId);
		}
		if (Mode != MatchMode.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Mode);
		}
		if (MapId != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(MapId);
		}
		if (Difficulty != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Difficulty);
		}
		num += players_.CalculateSize(_repeated_players_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SysStartMatchReq other)
	{
		if (other != null)
		{
			if (other.TeamId != 0L)
			{
				TeamId = other.TeamId;
			}
			if (other.Mode != MatchMode.None)
			{
				Mode = other.Mode;
			}
			if (other.MapId != 0)
			{
				MapId = other.MapId;
			}
			if (other.Difficulty != 0)
			{
				Difficulty = other.Difficulty;
			}
			players_.Add(other.players_);
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
				TeamId = input.ReadInt64();
				break;
			case 16u:
				Mode = (MatchMode)input.ReadEnum();
				break;
			case 24u:
				MapId = input.ReadInt32();
				break;
			case 32u:
				Difficulty = input.ReadInt32();
				break;
			case 42u:
				players_.AddEntriesFrom(ref input, _repeated_players_codec);
				break;
			}
		}
	}
}
