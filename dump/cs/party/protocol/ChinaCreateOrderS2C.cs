using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChinaCreateOrderS2C : IMessage<ChinaCreateOrderS2C>, IMessage, IEquatable<ChinaCreateOrderS2C>, IDeepCloneable<ChinaCreateOrderS2C>, IBufferMessage
{
	private static readonly MessageParser<ChinaCreateOrderS2C> _parser = new MessageParser<ChinaCreateOrderS2C>(() => new ChinaCreateOrderS2C());

	private UnknownFieldSet _unknownFields;

	public const int GoodsIdFieldNumber = 1;

	private int goodsId_;

	public const int OrderIdFieldNumber = 2;

	private string orderId_ = "";

	public const int AmountFieldNumber = 3;

	private int amount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChinaCreateOrderS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[522];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoodsId
	{
		get
		{
			return goodsId_;
		}
		set
		{
			goodsId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string OrderId
	{
		get
		{
			return orderId_;
		}
		set
		{
			orderId_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public ChinaCreateOrderS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaCreateOrderS2C(ChinaCreateOrderS2C other)
		: this()
	{
		goodsId_ = other.goodsId_;
		orderId_ = other.orderId_;
		amount_ = other.amount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaCreateOrderS2C Clone()
	{
		return new ChinaCreateOrderS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChinaCreateOrderS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChinaCreateOrderS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GoodsId != other.GoodsId)
		{
			return false;
		}
		if (OrderId != other.OrderId)
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
		if (GoodsId != 0)
		{
			num ^= GoodsId.GetHashCode();
		}
		if (OrderId.Length != 0)
		{
			num ^= OrderId.GetHashCode();
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
		if (GoodsId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(GoodsId);
		}
		if (OrderId.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(OrderId);
		}
		if (Amount != 0)
		{
			output.WriteRawTag(29);
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
		if (GoodsId != 0)
		{
			num += 5;
		}
		if (OrderId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(OrderId);
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
	public void MergeFrom(ChinaCreateOrderS2C other)
	{
		if (other != null)
		{
			if (other.GoodsId != 0)
			{
				GoodsId = other.GoodsId;
			}
			if (other.OrderId.Length != 0)
			{
				OrderId = other.OrderId;
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
				GoodsId = input.ReadSFixed32();
				break;
			case 18u:
				OrderId = input.ReadString();
				break;
			case 29u:
				Amount = input.ReadSFixed32();
				break;
			}
		}
	}
}
