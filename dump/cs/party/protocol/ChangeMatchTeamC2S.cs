using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class ChangeMatchTeamC2S : IMessage<ChangeMatchTeamC2S>, IMessage, IEquatable<ChangeMatchTeamC2S>, IDeepCloneable<ChangeMatchTeamC2S>, IBufferMessage
{
	private static readonly MessageParser<ChangeMatchTeamC2S> _parser = new MessageParser<ChangeMatchTeamC2S>(() => new ChangeMatchTeamC2S());

	private UnknownFieldSet _unknownFields;

	public const int ModeFieldNumber = 1;

	private MatchMode mode_;

	public const int MapIdFieldNumber = 2;

	private int mapId_;

	public const int DifficultyFieldNumber = 3;

	private int difficulty_;

	public const int TeamIdFieldNumber = 100;

	private long teamId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChangeMatchTeamC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[11];

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
	public ChangeMatchTeamC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeMatchTeamC2S(ChangeMatchTeamC2S other)
		: this()
	{
		mode_ = other.mode_;
		mapId_ = other.mapId_;
		difficulty_ = other.difficulty_;
		teamId_ = other.teamId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeMatchTeamC2S Clone()
	{
		return new ChangeMatchTeamC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChangeMatchTeamC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChangeMatchTeamC2S other)
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
		if (TeamId != other.TeamId)
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
		if (TeamId != 0L)
		{
			num ^= TeamId.GetHashCode();
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
		if (TeamId != 0L)
		{
			output.WriteRawTag(161, 6);
			output.WriteSFixed64(TeamId);
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
		if (TeamId != 0L)
		{
			num += 10;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChangeMatchTeamC2S other)
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
			if (other.TeamId != 0L)
			{
				TeamId = other.TeamId;
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
			case 801u:
				TeamId = input.ReadSFixed64();
				break;
			}
		}
	}
}
