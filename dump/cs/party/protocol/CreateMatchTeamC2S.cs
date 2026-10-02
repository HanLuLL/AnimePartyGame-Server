using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class CreateMatchTeamC2S : IMessage<CreateMatchTeamC2S>, IMessage, IEquatable<CreateMatchTeamC2S>, IDeepCloneable<CreateMatchTeamC2S>, IBufferMessage
{
	private static readonly MessageParser<CreateMatchTeamC2S> _parser = new MessageParser<CreateMatchTeamC2S>(() => new CreateMatchTeamC2S());

	private UnknownFieldSet _unknownFields;

	public const int ModeFieldNumber = 1;

	private MatchMode mode_;

	public const int MapIdFieldNumber = 2;

	private int mapId_;

	public const int DifficultyFieldNumber = 3;

	private int difficulty_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CreateMatchTeamC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[9];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public CreateMatchTeamC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateMatchTeamC2S(CreateMatchTeamC2S other)
		: this()
	{
		mode_ = other.mode_;
		mapId_ = other.mapId_;
		difficulty_ = other.difficulty_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateMatchTeamC2S Clone()
	{
		return new CreateMatchTeamC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CreateMatchTeamC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CreateMatchTeamC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
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
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
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
		if (Mode != MatchMode.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)Mode);
		}
		if (MapId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MapId);
		}
		if (Difficulty != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Difficulty);
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
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CreateMatchTeamC2S other)
	{
		if (other != null)
		{
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
				Mode = (MatchMode)input.ReadEnum();
				break;
			case 21u:
				MapId = input.ReadSFixed32();
				break;
			case 29u:
				Difficulty = input.ReadSFixed32();
				break;
			}
		}
	}
}
