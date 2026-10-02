using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ExchangeStoreGoodsConfigure : IMessage<ExchangeStoreGoodsConfigure>, IMessage, IEquatable<ExchangeStoreGoodsConfigure>, IDeepCloneable<ExchangeStoreGoodsConfigure>, IBufferMessage
{
	private static readonly MessageParser<ExchangeStoreGoodsConfigure> _parser = new MessageParser<ExchangeStoreGoodsConfigure>(() => new ExchangeStoreGoodsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ShopTabTypeFieldNumber = 1;

	private ShopTabType shopTabType_;

	public const int ExchangeStoreGoodsConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<ExchangeStoreGoodsConfigureItem> _repeated_exchangeStoreGoodsConfigureItems_codec = FieldCodec.ForMessage(18u, ExchangeStoreGoodsConfigureItem.Parser);

	private readonly RepeatedField<ExchangeStoreGoodsConfigureItem> exchangeStoreGoodsConfigureItems_ = new RepeatedField<ExchangeStoreGoodsConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExchangeStoreGoodsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ExchangeStoreReflection.Descriptor.MessageTypes[4];

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
	public RepeatedField<ExchangeStoreGoodsConfigureItem> ExchangeStoreGoodsConfigureItems => exchangeStoreGoodsConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGoodsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGoodsConfigure(ExchangeStoreGoodsConfigure other)
		: this()
	{
		shopTabType_ = other.shopTabType_;
		exchangeStoreGoodsConfigureItems_ = other.exchangeStoreGoodsConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGoodsConfigure Clone()
	{
		return new ExchangeStoreGoodsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExchangeStoreGoodsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExchangeStoreGoodsConfigure other)
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
		if (!exchangeStoreGoodsConfigureItems_.Equals(other.exchangeStoreGoodsConfigureItems_))
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
		num ^= exchangeStoreGoodsConfigureItems_.GetHashCode();
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
		exchangeStoreGoodsConfigureItems_.WriteTo(ref output, _repeated_exchangeStoreGoodsConfigureItems_codec);
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
		num += exchangeStoreGoodsConfigureItems_.CalculateSize(_repeated_exchangeStoreGoodsConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ExchangeStoreGoodsConfigure other)
	{
		if (other != null)
		{
			if (other.ShopTabType != ShopTabType.None)
			{
				ShopTabType = other.ShopTabType;
			}
			exchangeStoreGoodsConfigureItems_.Add(other.exchangeStoreGoodsConfigureItems_);
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
				exchangeStoreGoodsConfigureItems_.AddEntriesFrom(ref input, _repeated_exchangeStoreGoodsConfigureItems_codec);
				break;
			}
		}
	}

	private ExchangeStoreGoodsConfigureItem GetExchangeStoreGoodsItem(int goodsId)
	{
		foreach (ExchangeStoreGoodsConfigureItem exchangeStoreGoodsConfigureItem in ExchangeStoreGoodsConfigureItems)
		{
			if (exchangeStoreGoodsConfigureItem.GoodsID == goodsId)
			{
				return exchangeStoreGoodsConfigureItem;
			}
		}
		return null;
	}
}
