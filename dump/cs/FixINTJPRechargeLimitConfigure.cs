using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class FixINTJPRechargeLimitConfigure : IMessage<FixINTJPRechargeLimitConfigure>, IMessage, IEquatable<FixINTJPRechargeLimitConfigure>, IDeepCloneable<FixINTJPRechargeLimitConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixINTJPRechargeLimitConfigure> _parser = new MessageParser<FixINTJPRechargeLimitConfigure>(() => new FixINTJPRechargeLimitConfigure());

	private UnknownFieldSet _unknownFields;

	public const int RechargeGearTypeFieldNumber = 1;

	private RechargeGearType rechargeGearType_;

	public const int RechargeLimitFieldNumber = 2;

	private int rechargeLimit_;

	public const int AgeFieldNumber = 3;

	private string age_ = "";

	public const int MsgFieldNumber = 4;

	private string msg_ = "";

	public const int TipsFieldNumber = 5;

	private string tips_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixINTJPRechargeLimitConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixINTJPReflection.Descriptor.MessageTypes[8];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeGearType RechargeGearType
	{
		get
		{
			return rechargeGearType_;
		}
		private set
		{
			rechargeGearType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RechargeLimit
	{
		get
		{
			return rechargeLimit_;
		}
		private set
		{
			rechargeLimit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Age
	{
		get
		{
			return age_;
		}
		private set
		{
			age_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Msg
	{
		get
		{
			return msg_;
		}
		private set
		{
			msg_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Tips
	{
		get
		{
			return tips_;
		}
		private set
		{
			tips_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPRechargeLimitConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPRechargeLimitConfigure(FixINTJPRechargeLimitConfigure other)
		: this()
	{
		rechargeGearType_ = other.rechargeGearType_;
		rechargeLimit_ = other.rechargeLimit_;
		age_ = other.age_;
		msg_ = other.msg_;
		tips_ = other.tips_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPRechargeLimitConfigure Clone()
	{
		return new FixINTJPRechargeLimitConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixINTJPRechargeLimitConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixINTJPRechargeLimitConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (RechargeGearType != other.RechargeGearType)
		{
			return false;
		}
		if (RechargeLimit != other.RechargeLimit)
		{
			return false;
		}
		if (Age != other.Age)
		{
			return false;
		}
		if (Msg != other.Msg)
		{
			return false;
		}
		if (Tips != other.Tips)
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
		if (RechargeGearType != RechargeGearType.None)
		{
			num ^= RechargeGearType.GetHashCode();
		}
		if (RechargeLimit != 0)
		{
			num ^= RechargeLimit.GetHashCode();
		}
		if (Age.Length != 0)
		{
			num ^= Age.GetHashCode();
		}
		if (Msg.Length != 0)
		{
			num ^= Msg.GetHashCode();
		}
		if (Tips.Length != 0)
		{
			num ^= Tips.GetHashCode();
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
		if (RechargeGearType != RechargeGearType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)RechargeGearType);
		}
		if (RechargeLimit != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(RechargeLimit);
		}
		if (Age.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Age);
		}
		if (Msg.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Msg);
		}
		if (Tips.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Tips);
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
		if (RechargeGearType != RechargeGearType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)RechargeGearType);
		}
		if (RechargeLimit != 0)
		{
			num += 5;
		}
		if (Age.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Age);
		}
		if (Msg.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Msg);
		}
		if (Tips.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Tips);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixINTJPRechargeLimitConfigure other)
	{
		if (other != null)
		{
			if (other.RechargeGearType != RechargeGearType.None)
			{
				RechargeGearType = other.RechargeGearType;
			}
			if (other.RechargeLimit != 0)
			{
				RechargeLimit = other.RechargeLimit;
			}
			if (other.Age.Length != 0)
			{
				Age = other.Age;
			}
			if (other.Msg.Length != 0)
			{
				Msg = other.Msg;
			}
			if (other.Tips.Length != 0)
			{
				Tips = other.Tips;
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
				RechargeGearType = (RechargeGearType)input.ReadEnum();
				break;
			case 21u:
				RechargeLimit = input.ReadSFixed32();
				break;
			case 26u:
				Age = input.ReadString();
				break;
			case 34u:
				Msg = input.ReadString();
				break;
			case 42u:
				Tips = input.ReadString();
				break;
			}
		}
	}
}
