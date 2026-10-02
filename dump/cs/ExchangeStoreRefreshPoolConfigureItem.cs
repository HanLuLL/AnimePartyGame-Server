using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class ExchangeStoreRefreshPoolConfigureItem : IMessage<ExchangeStoreRefreshPoolConfigureItem>, IMessage, IEquatable<ExchangeStoreRefreshPoolConfigureItem>, IDeepCloneable<ExchangeStoreRefreshPoolConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<ExchangeStoreRefreshPoolConfigureItem> _parser = new MessageParser<ExchangeStoreRefreshPoolConfigureItem>(() => new ExchangeStoreRefreshPoolConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int GoodsIDFieldNumber = 1;

	private int goodsID_;

	public const int ItemIDFieldNumber = 2;

	private int itemID_;

	public const int ItemNumFieldNumber = 3;

	private int itemNum_;

	public const int CurrencyIDFieldNumber = 4;

	private int currencyID_;

	public const int OriginalPriceFieldNumber = 5;

	private int originalPrice_;

	public const int DiscountPriceFieldNumber = 6;

	private int discountPrice_;

	public const int NumLimitFieldNumber = 7;

	private int numLimit_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExchangeStoreRefreshPoolConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ExchangeStoreReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoodsID
	{
		get
		{
			return goodsID_;
		}
		private set
		{
			goodsID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemID
	{
		get
		{
			return itemID_;
		}
		private set
		{
			itemID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemNum
	{
		get
		{
			return itemNum_;
		}
		private set
		{
			itemNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CurrencyID
	{
		get
		{
			return currencyID_;
		}
		private set
		{
			currencyID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OriginalPrice
	{
		get
		{
			return originalPrice_;
		}
		private set
		{
			originalPrice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiscountPrice
	{
		get
		{
			return discountPrice_;
		}
		private set
		{
			discountPrice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NumLimit
	{
		get
		{
			return numLimit_;
		}
		private set
		{
			numLimit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreRefreshPoolConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreRefreshPoolConfigureItem(ExchangeStoreRefreshPoolConfigureItem other)
		: this()
	{
		goodsID_ = other.goodsID_;
		itemID_ = other.itemID_;
		itemNum_ = other.itemNum_;
		currencyID_ = other.currencyID_;
		originalPrice_ = other.originalPrice_;
		discountPrice_ = other.discountPrice_;
		numLimit_ = other.numLimit_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreRefreshPoolConfigureItem Clone()
	{
		return new ExchangeStoreRefreshPoolConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExchangeStoreRefreshPoolConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExchangeStoreRefreshPoolConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GoodsID != other.GoodsID)
		{
			return false;
		}
		if (ItemID != other.ItemID)
		{
			return false;
		}
		if (ItemNum != other.ItemNum)
		{
			return false;
		}
		if (CurrencyID != other.CurrencyID)
		{
			return false;
		}
		if (OriginalPrice != other.OriginalPrice)
		{
			return false;
		}
		if (DiscountPrice != other.DiscountPrice)
		{
			return false;
		}
		if (NumLimit != other.NumLimit)
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
		if (GoodsID != 0)
		{
			num ^= GoodsID.GetHashCode();
		}
		if (ItemID != 0)
		{
			num ^= ItemID.GetHashCode();
		}
		if (ItemNum != 0)
		{
			num ^= ItemNum.GetHashCode();
		}
		if (CurrencyID != 0)
		{
			num ^= CurrencyID.GetHashCode();
		}
		if (OriginalPrice != 0)
		{
			num ^= OriginalPrice.GetHashCode();
		}
		if (DiscountPrice != 0)
		{
			num ^= DiscountPrice.GetHashCode();
		}
		if (NumLimit != 0)
		{
			num ^= NumLimit.GetHashCode();
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
		if (GoodsID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(GoodsID);
		}
		if (ItemID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ItemID);
		}
		if (ItemNum != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ItemNum);
		}
		if (CurrencyID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CurrencyID);
		}
		if (OriginalPrice != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(OriginalPrice);
		}
		if (DiscountPrice != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(DiscountPrice);
		}
		if (NumLimit != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(NumLimit);
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
		if (GoodsID != 0)
		{
			num += 5;
		}
		if (ItemID != 0)
		{
			num += 5;
		}
		if (ItemNum != 0)
		{
			num += 5;
		}
		if (CurrencyID != 0)
		{
			num += 5;
		}
		if (OriginalPrice != 0)
		{
			num += 5;
		}
		if (DiscountPrice != 0)
		{
			num += 5;
		}
		if (NumLimit != 0)
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
	public void MergeFrom(ExchangeStoreRefreshPoolConfigureItem other)
	{
		if (other != null)
		{
			if (other.GoodsID != 0)
			{
				GoodsID = other.GoodsID;
			}
			if (other.ItemID != 0)
			{
				ItemID = other.ItemID;
			}
			if (other.ItemNum != 0)
			{
				ItemNum = other.ItemNum;
			}
			if (other.CurrencyID != 0)
			{
				CurrencyID = other.CurrencyID;
			}
			if (other.OriginalPrice != 0)
			{
				OriginalPrice = other.OriginalPrice;
			}
			if (other.DiscountPrice != 0)
			{
				DiscountPrice = other.DiscountPrice;
			}
			if (other.NumLimit != 0)
			{
				NumLimit = other.NumLimit;
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
				GoodsID = input.ReadSFixed32();
				break;
			case 21u:
				ItemID = input.ReadSFixed32();
				break;
			case 29u:
				ItemNum = input.ReadSFixed32();
				break;
			case 37u:
				CurrencyID = input.ReadSFixed32();
				break;
			case 45u:
				OriginalPrice = input.ReadSFixed32();
				break;
			case 53u:
				DiscountPrice = input.ReadSFixed32();
				break;
			case 61u:
				NumLimit = input.ReadSFixed32();
				break;
			}
		}
	}
}
