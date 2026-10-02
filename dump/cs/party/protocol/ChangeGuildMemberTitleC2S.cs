using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChangeGuildMemberTitleC2S : IMessage<ChangeGuildMemberTitleC2S>, IMessage, IEquatable<ChangeGuildMemberTitleC2S>, IDeepCloneable<ChangeGuildMemberTitleC2S>, IBufferMessage
{
	private static readonly MessageParser<ChangeGuildMemberTitleC2S> _parser = new MessageParser<ChangeGuildMemberTitleC2S>(() => new ChangeGuildMemberTitleC2S());

	private UnknownFieldSet _unknownFields;

	public const int TargetPlayerIdFieldNumber = 1;

	private long targetPlayerId_;

	public const int RoleFieldNumber = 2;

	private int role_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChangeGuildMemberTitleC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[581];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long TargetPlayerId
	{
		get
		{
			return targetPlayerId_;
		}
		set
		{
			targetPlayerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Role
	{
		get
		{
			return role_;
		}
		set
		{
			role_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeGuildMemberTitleC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeGuildMemberTitleC2S(ChangeGuildMemberTitleC2S other)
		: this()
	{
		targetPlayerId_ = other.targetPlayerId_;
		role_ = other.role_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeGuildMemberTitleC2S Clone()
	{
		return new ChangeGuildMemberTitleC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChangeGuildMemberTitleC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChangeGuildMemberTitleC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (TargetPlayerId != other.TargetPlayerId)
		{
			return false;
		}
		if (Role != other.Role)
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
		if (TargetPlayerId != 0L)
		{
			num ^= TargetPlayerId.GetHashCode();
		}
		if (Role != 0)
		{
			num ^= Role.GetHashCode();
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
		if (TargetPlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(TargetPlayerId);
		}
		if (Role != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Role);
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
		if (TargetPlayerId != 0L)
		{
			num += 9;
		}
		if (Role != 0)
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
	public void MergeFrom(ChangeGuildMemberTitleC2S other)
	{
		if (other != null)
		{
			if (other.TargetPlayerId != 0L)
			{
				TargetPlayerId = other.TargetPlayerId;
			}
			if (other.Role != 0)
			{
				Role = other.Role;
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
				TargetPlayerId = input.ReadSFixed64();
				break;
			case 21u:
				Role = input.ReadSFixed32();
				break;
			}
		}
	}
}
