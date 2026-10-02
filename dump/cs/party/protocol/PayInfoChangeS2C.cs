using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class PayInfoChangeS2C : IMessage<PayInfoChangeS2C>, IMessage, IEquatable<PayInfoChangeS2C>, IDeepCloneable<PayInfoChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<PayInfoChangeS2C> _parser = new MessageParser<PayInfoChangeS2C>(() => new PayInfoChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int AgePositionFieldNumber = 1;

	private int agePosition_;

	public const int AmountFieldNumber = 2;

	private int amount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PayInfoChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[531];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AgePosition
	{
		get
		{
			return agePosition_;
		}
		set
		{
			agePosition_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Amount
	{
		get
		{
			return amount_;
		}
		set
		{
			amount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayInfoChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayInfoChangeS2C(PayInfoChangeS2C other)
		: this()
	{
		agePosition_ = other.agePosition_;
		amount_ = other.amount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayInfoChangeS2C Clone()
	{
		return new PayInfoChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PayInfoChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PayInfoChangeS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (AgePosition != other.AgePosition)
		{
			return false;
		}
		if (Amount != other.Amount)
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
		if (AgePosition != 0)
		{
			num ^= AgePosition.GetHashCode();
		}
		if (Amount != 0)
		{
			num ^= Amount.GetHashCode();
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
		if (AgePosition != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(AgePosition);
		}
		if (Amount != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Amount);
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
		if (AgePosition != 0)
		{
			num += 5;
		}
		if (Amount != 0)
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
	public void MergeFrom(PayInfoChangeS2C other)
	{
		if (other != null)
		{
			if (other.AgePosition != 0)
			{
				AgePosition = other.AgePosition;
			}
			if (other.Amount != 0)
			{
				Amount = other.Amount;
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
				AgePosition = input.ReadSFixed32();
				break;
			case 21u:
				Amount = input.ReadSFixed32();
				break;
			}
		}
	}
}
