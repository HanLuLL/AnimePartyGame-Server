using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class KillMessageS2C : IMessage<KillMessageS2C>, IMessage, IEquatable<KillMessageS2C>, IDeepCloneable<KillMessageS2C>, IBufferMessage
{
	private static readonly MessageParser<KillMessageS2C> _parser = new MessageParser<KillMessageS2C>(() => new KillMessageS2C());

	private UnknownFieldSet _unknownFields;

	public const int AttackerIdFieldNumber = 1;

	private long attackerId_;

	public const int DefenderIdFieldNumber = 2;

	private long defenderId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<KillMessageS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[398];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long AttackerId
	{
		get
		{
			return attackerId_;
		}
		set
		{
			attackerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long DefenderId
	{
		get
		{
			return defenderId_;
		}
		set
		{
			defenderId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public KillMessageS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public KillMessageS2C(KillMessageS2C other)
		: this()
	{
		attackerId_ = other.attackerId_;
		defenderId_ = other.defenderId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public KillMessageS2C Clone()
	{
		return new KillMessageS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as KillMessageS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(KillMessageS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (AttackerId != other.AttackerId)
		{
			return false;
		}
		if (DefenderId != other.DefenderId)
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
		if (AttackerId != 0L)
		{
			num ^= AttackerId.GetHashCode();
		}
		if (DefenderId != 0L)
		{
			num ^= DefenderId.GetHashCode();
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
		if (AttackerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(AttackerId);
		}
		if (DefenderId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(DefenderId);
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
		if (AttackerId != 0L)
		{
			num += 9;
		}
		if (DefenderId != 0L)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(KillMessageS2C other)
	{
		if (other != null)
		{
			if (other.AttackerId != 0L)
			{
				AttackerId = other.AttackerId;
			}
			if (other.DefenderId != 0L)
			{
				DefenderId = other.DefenderId;
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
				AttackerId = input.ReadSFixed64();
				break;
			case 17u:
				DefenderId = input.ReadSFixed64();
				break;
			}
		}
	}
}
