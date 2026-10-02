using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChinaCreateOrderC2S : IMessage<ChinaCreateOrderC2S>, IMessage, IEquatable<ChinaCreateOrderC2S>, IDeepCloneable<ChinaCreateOrderC2S>, IBufferMessage
{
	private static readonly MessageParser<ChinaCreateOrderC2S> _parser = new MessageParser<ChinaCreateOrderC2S>(() => new ChinaCreateOrderC2S());

	private UnknownFieldSet _unknownFields;

	public const int ProductIdFieldNumber = 1;

	private int productId_;

	public const int QuantityFieldNumber = 2;

	private int quantity_;

	public const int TypeFieldNumber = 3;

	private int type_;

	public const int CouponsIdFieldNumber = 4;

	private int couponsId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChinaCreateOrderC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[521];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ProductId
	{
		get
		{
			return productId_;
		}
		set
		{
			productId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Quantity
	{
		get
		{
			return quantity_;
		}
		set
		{
			quantity_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Type
	{
		get
		{
			return type_;
		}
		set
		{
			type_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CouponsId
	{
		get
		{
			return couponsId_;
		}
		set
		{
			couponsId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaCreateOrderC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaCreateOrderC2S(ChinaCreateOrderC2S other)
		: this()
	{
		productId_ = other.productId_;
		quantity_ = other.quantity_;
		type_ = other.type_;
		couponsId_ = other.couponsId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaCreateOrderC2S Clone()
	{
		return new ChinaCreateOrderC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChinaCreateOrderC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChinaCreateOrderC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ProductId != other.ProductId)
		{
			return false;
		}
		if (Quantity != other.Quantity)
		{
			return false;
		}
		if (Type != other.Type)
		{
			return false;
		}
		if (CouponsId != other.CouponsId)
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
		if (ProductId != 0)
		{
			num ^= ProductId.GetHashCode();
		}
		if (Quantity != 0)
		{
			num ^= Quantity.GetHashCode();
		}
		if (Type != 0)
		{
			num ^= Type.GetHashCode();
		}
		if (CouponsId != 0)
		{
			num ^= CouponsId.GetHashCode();
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
		if (ProductId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ProductId);
		}
		if (Quantity != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Quantity);
		}
		if (Type != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Type);
		}
		if (CouponsId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CouponsId);
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
		if (ProductId != 0)
		{
			num += 5;
		}
		if (Quantity != 0)
		{
			num += 5;
		}
		if (Type != 0)
		{
			num += 5;
		}
		if (CouponsId != 0)
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
	public void MergeFrom(ChinaCreateOrderC2S other)
	{
		if (other != null)
		{
			if (other.ProductId != 0)
			{
				ProductId = other.ProductId;
			}
			if (other.Quantity != 0)
			{
				Quantity = other.Quantity;
			}
			if (other.Type != 0)
			{
				Type = other.Type;
			}
			if (other.CouponsId != 0)
			{
				CouponsId = other.CouponsId;
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
				ProductId = input.ReadSFixed32();
				break;
			case 21u:
				Quantity = input.ReadSFixed32();
				break;
			case 29u:
				Type = input.ReadSFixed32();
				break;
			case 37u:
				CouponsId = input.ReadSFixed32();
				break;
			}
		}
	}
}
