using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RechargeRebateRebateConfigureItem : IMessage<RechargeRebateRebateConfigureItem>, IMessage, IEquatable<RechargeRebateRebateConfigureItem>, IDeepCloneable<RechargeRebateRebateConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<RechargeRebateRebateConfigureItem> _parser = new MessageParser<RechargeRebateRebateConfigureItem>(() => new RechargeRebateRebateConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int AmountStartFieldNumber = 1;

	private int amountStart_;

	public const int AmountEndFieldNumber = 2;

	private int amountEnd_;

	public const int MultipleFieldNumber = 3;

	private float multiple_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeRebateRebateConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeRebateReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AmountStart
	{
		get
		{
			return amountStart_;
		}
		private set
		{
			amountStart_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AmountEnd
	{
		get
		{
			return amountEnd_;
		}
		private set
		{
			amountEnd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float Multiple
	{
		get
		{
			return multiple_;
		}
		private set
		{
			multiple_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeRebateRebateConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeRebateRebateConfigureItem(RechargeRebateRebateConfigureItem other)
		: this()
	{
		amountStart_ = other.amountStart_;
		amountEnd_ = other.amountEnd_;
		multiple_ = other.multiple_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeRebateRebateConfigureItem Clone()
	{
		return new RechargeRebateRebateConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeRebateRebateConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeRebateRebateConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (AmountStart != other.AmountStart)
		{
			return false;
		}
		if (AmountEnd != other.AmountEnd)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Multiple, other.Multiple))
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
		if (AmountStart != 0)
		{
			num ^= AmountStart.GetHashCode();
		}
		if (AmountEnd != 0)
		{
			num ^= AmountEnd.GetHashCode();
		}
		if (Multiple != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Multiple);
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
		if (AmountStart != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(AmountStart);
		}
		if (AmountEnd != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(AmountEnd);
		}
		if (Multiple != 0f)
		{
			output.WriteRawTag(29);
			output.WriteFloat(Multiple);
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
		if (AmountStart != 0)
		{
			num += 5;
		}
		if (AmountEnd != 0)
		{
			num += 5;
		}
		if (Multiple != 0f)
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
	public void MergeFrom(RechargeRebateRebateConfigureItem other)
	{
		if (other != null)
		{
			if (other.AmountStart != 0)
			{
				AmountStart = other.AmountStart;
			}
			if (other.AmountEnd != 0)
			{
				AmountEnd = other.AmountEnd;
			}
			if (other.Multiple != 0f)
			{
				Multiple = other.Multiple;
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
				AmountStart = input.ReadSFixed32();
				break;
			case 21u:
				AmountEnd = input.ReadSFixed32();
				break;
			case 29u:
				Multiple = input.ReadFloat();
				break;
			}
		}
	}
}
