using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class GameScoreChangeS2C : IMessage<GameScoreChangeS2C>, IMessage, IEquatable<GameScoreChangeS2C>, IDeepCloneable<GameScoreChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<GameScoreChangeS2C> _parser = new MessageParser<GameScoreChangeS2C>(() => new GameScoreChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int SpecialScoreFieldNumber = 1;

	private int specialScore_;

	public const int TeamIdFieldNumber = 2;

	private int teamId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GameScoreChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[461];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SpecialScore
	{
		get
		{
			return specialScore_;
		}
		set
		{
			specialScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TeamId
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
	public GameScoreChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameScoreChangeS2C(GameScoreChangeS2C other)
		: this()
	{
		specialScore_ = other.specialScore_;
		teamId_ = other.teamId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameScoreChangeS2C Clone()
	{
		return new GameScoreChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GameScoreChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GameScoreChangeS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (SpecialScore != other.SpecialScore)
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
		if (SpecialScore != 0)
		{
			num ^= SpecialScore.GetHashCode();
		}
		if (TeamId != 0)
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
		if (SpecialScore != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(SpecialScore);
		}
		if (TeamId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(TeamId);
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
		if (SpecialScore != 0)
		{
			num += 5;
		}
		if (TeamId != 0)
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
	public void MergeFrom(GameScoreChangeS2C other)
	{
		if (other != null)
		{
			if (other.SpecialScore != 0)
			{
				SpecialScore = other.SpecialScore;
			}
			if (other.TeamId != 0)
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
			case 13u:
				SpecialScore = input.ReadSFixed32();
				break;
			case 21u:
				TeamId = input.ReadSFixed32();
				break;
			}
		}
	}
}
