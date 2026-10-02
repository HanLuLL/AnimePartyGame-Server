using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SysChangeMatchTeamState : IMessage<SysChangeMatchTeamState>, IMessage, IEquatable<SysChangeMatchTeamState>, IDeepCloneable<SysChangeMatchTeamState>, IBufferMessage
{
	private static readonly MessageParser<SysChangeMatchTeamState> _parser = new MessageParser<SysChangeMatchTeamState>(() => new SysChangeMatchTeamState());

	private UnknownFieldSet _unknownFields;

	public const int TeamIdsFieldNumber = 1;

	private static readonly FieldCodec<long> _repeated_teamIds_codec = FieldCodec.ForSFixed64(10u);

	private readonly RepeatedField<long> teamIds_ = new RepeatedField<long>();

	public const int StateFieldNumber = 2;

	private MatchTeamInfo.Types.State state_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysChangeMatchTeamState> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[158];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> TeamIds => teamIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamInfo.Types.State State
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
	public SysChangeMatchTeamState()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysChangeMatchTeamState(SysChangeMatchTeamState other)
		: this()
	{
		teamIds_ = other.teamIds_.Clone();
		state_ = other.state_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysChangeMatchTeamState Clone()
	{
		return new SysChangeMatchTeamState(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysChangeMatchTeamState);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysChangeMatchTeamState other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!teamIds_.Equals(other.teamIds_))
		{
			return false;
		}
		if (State != other.State)
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
		num ^= teamIds_.GetHashCode();
		if (State != MatchTeamInfo.Types.State.None)
		{
			num ^= State.GetHashCode();
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
		teamIds_.WriteTo(ref output, _repeated_teamIds_codec);
		if (State != MatchTeamInfo.Types.State.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)State);
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
		num += teamIds_.CalculateSize(_repeated_teamIds_codec);
		if (State != MatchTeamInfo.Types.State.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)State);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SysChangeMatchTeamState other)
	{
		if (other != null)
		{
			teamIds_.Add(other.teamIds_);
			if (other.State != MatchTeamInfo.Types.State.None)
			{
				State = other.State;
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
			case 9u:
			case 10u:
				teamIds_.AddEntriesFrom(ref input, _repeated_teamIds_codec);
				break;
			case 16u:
				State = (MatchTeamInfo.Types.State)input.ReadEnum();
				break;
			}
		}
	}
}
