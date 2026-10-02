using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class MatchSuccessC2S : IMessage<MatchSuccessC2S>, IMessage, IEquatable<MatchSuccessC2S>, IDeepCloneable<MatchSuccessC2S>, IBufferMessage
{
	private static readonly MessageParser<MatchSuccessC2S> _parser = new MessageParser<MatchSuccessC2S>(() => new MatchSuccessC2S());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private long id_;

	public const int ModeFieldNumber = 2;

	private MatchMode mode_;

	public const int MapIdFieldNumber = 3;

	private int mapId_;

	public const int DifficultyFieldNumber = 4;

	private int difficulty_;

	public const int PlayersFieldNumber = 5;

	private static readonly FieldCodec<Player> _repeated_players_codec = FieldCodec.ForMessage(42u, Player.Parser);

	private readonly RepeatedField<Player> players_ = new RepeatedField<Player>();

	public const int TeamIdsFieldNumber = 6;

	private static readonly FieldCodec<long> _repeated_teamIds_codec = FieldCodec.ForSFixed64(50u);

	private readonly RepeatedField<long> teamIds_ = new RepeatedField<long>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MatchSuccessC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[33];

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
	public RepeatedField<long> TeamIds => teamIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchSuccessC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchSuccessC2S(MatchSuccessC2S other)
		: this()
	{
		id_ = other.id_;
		mode_ = other.mode_;
		mapId_ = other.mapId_;
		difficulty_ = other.difficulty_;
		players_ = other.players_.Clone();
		teamIds_ = other.teamIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchSuccessC2S Clone()
	{
		return new MatchSuccessC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MatchSuccessC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MatchSuccessC2S other)
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
		if (!teamIds_.Equals(other.teamIds_))
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
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		if (Difficulty != 0)
		{
			num ^= Difficulty.GetHashCode();
		}
		num ^= players_.GetHashCode();
		num ^= teamIds_.GetHashCode();
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
		if (MapId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(MapId);
		}
		if (Difficulty != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Difficulty);
		}
		players_.WriteTo(ref output, _repeated_players_codec);
		teamIds_.WriteTo(ref output, _repeated_teamIds_codec);
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
		if (MapId != 0)
		{
			num += 5;
		}
		if (Difficulty != 0)
		{
			num += 5;
		}
		num += players_.CalculateSize(_repeated_players_codec);
		num += teamIds_.CalculateSize(_repeated_teamIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MatchSuccessC2S other)
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
			if (other.MapId != 0)
			{
				MapId = other.MapId;
			}
			if (other.Difficulty != 0)
			{
				Difficulty = other.Difficulty;
			}
			players_.Add(other.players_);
			teamIds_.Add(other.teamIds_);
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
			case 29u:
				MapId = input.ReadSFixed32();
				break;
			case 37u:
				Difficulty = input.ReadSFixed32();
				break;
			case 42u:
				players_.AddEntriesFrom(ref input, _repeated_players_codec);
				break;
			case 49u:
			case 50u:
				teamIds_.AddEntriesFrom(ref input, _repeated_teamIds_codec);
				break;
			}
		}
	}
}
