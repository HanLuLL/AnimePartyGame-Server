using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class InviteInfoNotifyS2C : IMessage<InviteInfoNotifyS2C>, IMessage, IEquatable<InviteInfoNotifyS2C>, IDeepCloneable<InviteInfoNotifyS2C>, IBufferMessage
{
	private static readonly MessageParser<InviteInfoNotifyS2C> _parser = new MessageParser<InviteInfoNotifyS2C>(() => new InviteInfoNotifyS2C());

	private UnknownFieldSet _unknownFields;

	public const int InviteCodeFieldNumber = 1;

	private string inviteCode_ = "";

	public const int InviterFieldNumber = 2;

	private string inviter_ = "";

	public const int NumFieldNumber = 3;

	private int num_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<InviteInfoNotifyS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[518];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string InviteCode
	{
		get
		{
			return inviteCode_;
		}
		set
		{
			inviteCode_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Inviter
	{
		get
		{
			return inviter_;
		}
		set
		{
			inviter_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Num
	{
		get
		{
			return num_;
		}
		set
		{
			num_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public InviteInfoNotifyS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public InviteInfoNotifyS2C(InviteInfoNotifyS2C other)
		: this()
	{
		inviteCode_ = other.inviteCode_;
		inviter_ = other.inviter_;
		num_ = other.num_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public InviteInfoNotifyS2C Clone()
	{
		return new InviteInfoNotifyS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as InviteInfoNotifyS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(InviteInfoNotifyS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (InviteCode != other.InviteCode)
		{
			return false;
		}
		if (Inviter != other.Inviter)
		{
			return false;
		}
		if (Num != other.Num)
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
		if (InviteCode.Length != 0)
		{
			num ^= InviteCode.GetHashCode();
		}
		if (Inviter.Length != 0)
		{
			num ^= Inviter.GetHashCode();
		}
		if (Num != 0)
		{
			num ^= Num.GetHashCode();
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
		if (InviteCode.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(InviteCode);
		}
		if (Inviter.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Inviter);
		}
		if (Num != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Num);
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
		if (InviteCode.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(InviteCode);
		}
		if (Inviter.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Inviter);
		}
		if (Num != 0)
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
	public void MergeFrom(InviteInfoNotifyS2C other)
	{
		if (other != null)
		{
			if (other.InviteCode.Length != 0)
			{
				InviteCode = other.InviteCode;
			}
			if (other.Inviter.Length != 0)
			{
				Inviter = other.Inviter;
			}
			if (other.Num != 0)
			{
				Num = other.Num;
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
			case 10u:
				InviteCode = input.ReadString();
				break;
			case 18u:
				Inviter = input.ReadString();
				break;
			case 29u:
				Num = input.ReadSFixed32();
				break;
			}
		}
	}
}
