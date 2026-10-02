using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixINTJPExchangeStoreGoodsConfigure : IMessage<FixINTJPExchangeStoreGoodsConfigure>, IMessage, IEquatable<FixINTJPExchangeStoreGoodsConfigure>, IDeepCloneable<FixINTJPExchangeStoreGoodsConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixINTJPExchangeStoreGoodsConfigure> _parser = new MessageParser<FixINTJPExchangeStoreGoodsConfigure>(() => new FixINTJPExchangeStoreGoodsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ShopTabTypeFieldNumber = 1;

	private ShopTabType shopTabType_;

	public const int FixINTJPExchangeStoreGoodsConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<FixINTJPExchangeStoreGoodsConfigureItem> _repeated_fixINTJPExchangeStoreGoodsConfigureItems_codec = FieldCodec.ForMessage(18u, FixINTJPExchangeStoreGoodsConfigureItem.Parser);

	private readonly RepeatedField<FixINTJPExchangeStoreGoodsConfigureItem> fixINTJPExchangeStoreGoodsConfigureItems_ = new RepeatedField<FixINTJPExchangeStoreGoodsConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixINTJPExchangeStoreGoodsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixINTJPReflection.Descriptor.MessageTypes[9];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopTabType ShopTabType
	{
		get
		{
			return shopTabType_;
		}
		private set
		{
			shopTabType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTJPExchangeStoreGoodsConfigureItem> FixINTJPExchangeStoreGoodsConfigureItems => fixINTJPExchangeStoreGoodsConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPExchangeStoreGoodsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPExchangeStoreGoodsConfigure(FixINTJPExchangeStoreGoodsConfigure other)
		: this()
	{
		shopTabType_ = other.shopTabType_;
		fixINTJPExchangeStoreGoodsConfigureItems_ = other.fixINTJPExchangeStoreGoodsConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPExchangeStoreGoodsConfigure Clone()
	{
		return new FixINTJPExchangeStoreGoodsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixINTJPExchangeStoreGoodsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixINTJPExchangeStoreGoodsConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ShopTabType != other.ShopTabType)
		{
			return false;
		}
		if (!fixINTJPExchangeStoreGoodsConfigureItems_.Equals(other.fixINTJPExchangeStoreGoodsConfigureItems_))
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
		if (ShopTabType != ShopTabType.None)
		{
			num ^= ShopTabType.GetHashCode();
		}
		num ^= fixINTJPExchangeStoreGoodsConfigureItems_.GetHashCode();
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
		if (ShopTabType != ShopTabType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)ShopTabType);
		}
		fixINTJPExchangeStoreGoodsConfigureItems_.WriteTo(ref output, _repeated_fixINTJPExchangeStoreGoodsConfigureItems_codec);
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
		if (ShopTabType != ShopTabType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ShopTabType);
		}
		num += fixINTJPExchangeStoreGoodsConfigureItems_.CalculateSize(_repeated_fixINTJPExchangeStoreGoodsConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixINTJPExchangeStoreGoodsConfigure other)
	{
		if (other != null)
		{
			if (other.ShopTabType != ShopTabType.None)
			{
				ShopTabType = other.ShopTabType;
			}
			fixINTJPExchangeStoreGoodsConfigureItems_.Add(other.fixINTJPExchangeStoreGoodsConfigureItems_);
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
				ShopTabType = (ShopTabType)input.ReadEnum();
				break;
			case 18u:
				fixINTJPExchangeStoreGoodsConfigureItems_.AddEntriesFrom(ref input, _repeated_fixINTJPExchangeStoreGoodsConfigureItems_codec);
				break;
			}
		}
	}
}
