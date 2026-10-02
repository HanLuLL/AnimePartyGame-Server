using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class ExchangeStoreGoodsConfigureItem : IMessage<ExchangeStoreGoodsConfigureItem>, IMessage, IEquatable<ExchangeStoreGoodsConfigureItem>, IDeepCloneable<ExchangeStoreGoodsConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<ExchangeStoreGoodsConfigureItem> _parser = new MessageParser<ExchangeStoreGoodsConfigureItem>(() => new ExchangeStoreGoodsConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int GoodsIDFieldNumber = 1;

	private int goodsID_;

	public const int GoodsOrderFieldNumber = 2;

	private int goodsOrder_;

	public const int RechargeIconFieldNumber = 3;

	private string rechargeIcon_ = "";

	public const int ItemTypeFieldNumber = 4;

	private string itemType_ = "";

	public const int ItemIDFieldNumber = 5;

	private int itemID_;

	public const int ItemNumFieldNumber = 6;

	private int itemNum_;

	public const int CurrencyIDFieldNumber = 7;

	private int currencyID_;

	public const int OriginalPriceFieldNumber = 8;

	private int originalPrice_;

	public const int DiscountPriceFieldNumber = 9;

	private int discountPrice_;

	public const int BeginTimeLimitedFieldNumber = 10;

	private Timestamp beginTimeLimited_;

	public const int EndTimeLimitedFieldNumber = 11;

	private Timestamp endTimeLimited_;

	public const int DiscountPriceLimitedFieldNumber = 12;

	private int discountPriceLimited_;

	public const int GoodsRefreshTypeFieldNumber = 13;

	private GoodsRefreshType goodsRefreshType_;

	public const int NumLimitFieldNumber = 14;

	private int numLimit_;

	public const int GoodsLabelTypeFieldNumber = 15;

	private GoodsLabelType goodsLabelType_;

	public const int BeginTimeFieldNumber = 16;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 17;

	private Timestamp endTime_;

	public const int ParamFieldNumber = 18;

	private int param_;

	public const int GreatFieldNumber = 19;

	private int great_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExchangeStoreGoodsConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ExchangeStoreReflection.Descriptor.MessageTypes[5];

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
	public int GoodsOrder
	{
		get
		{
			return goodsOrder_;
		}
		private set
		{
			goodsOrder_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string RechargeIcon
	{
		get
		{
			return rechargeIcon_;
		}
		private set
		{
			rechargeIcon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ItemType
	{
		get
		{
			return itemType_;
		}
		private set
		{
			itemType_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public Timestamp BeginTimeLimited
	{
		get
		{
			return beginTimeLimited_;
		}
		private set
		{
			beginTimeLimited_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTimeLimited
	{
		get
		{
			return endTimeLimited_;
		}
		private set
		{
			endTimeLimited_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiscountPriceLimited
	{
		get
		{
			return discountPriceLimited_;
		}
		private set
		{
			discountPriceLimited_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GoodsRefreshType GoodsRefreshType
	{
		get
		{
			return goodsRefreshType_;
		}
		private set
		{
			goodsRefreshType_ = value;
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
	public GoodsLabelType GoodsLabelType
	{
		get
		{
			return goodsLabelType_;
		}
		private set
		{
			goodsLabelType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp BeginTime
	{
		get
		{
			return beginTime_;
		}
		private set
		{
			beginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTime
	{
		get
		{
			return endTime_;
		}
		private set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Param
	{
		get
		{
			return param_;
		}
		private set
		{
			param_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Great
	{
		get
		{
			return great_;
		}
		private set
		{
			great_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGoodsConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGoodsConfigureItem(ExchangeStoreGoodsConfigureItem other)
		: this()
	{
		goodsID_ = other.goodsID_;
		goodsOrder_ = other.goodsOrder_;
		rechargeIcon_ = other.rechargeIcon_;
		itemType_ = other.itemType_;
		itemID_ = other.itemID_;
		itemNum_ = other.itemNum_;
		currencyID_ = other.currencyID_;
		originalPrice_ = other.originalPrice_;
		discountPrice_ = other.discountPrice_;
		beginTimeLimited_ = ((other.beginTimeLimited_ != null) ? other.beginTimeLimited_.Clone() : null);
		endTimeLimited_ = ((other.endTimeLimited_ != null) ? other.endTimeLimited_.Clone() : null);
		discountPriceLimited_ = other.discountPriceLimited_;
		goodsRefreshType_ = other.goodsRefreshType_;
		numLimit_ = other.numLimit_;
		goodsLabelType_ = other.goodsLabelType_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		param_ = other.param_;
		great_ = other.great_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGoodsConfigureItem Clone()
	{
		return new ExchangeStoreGoodsConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExchangeStoreGoodsConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExchangeStoreGoodsConfigureItem other)
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
		if (GoodsOrder != other.GoodsOrder)
		{
			return false;
		}
		if (RechargeIcon != other.RechargeIcon)
		{
			return false;
		}
		if (ItemType != other.ItemType)
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
		if (!object.Equals(BeginTimeLimited, other.BeginTimeLimited))
		{
			return false;
		}
		if (!object.Equals(EndTimeLimited, other.EndTimeLimited))
		{
			return false;
		}
		if (DiscountPriceLimited != other.DiscountPriceLimited)
		{
			return false;
		}
		if (GoodsRefreshType != other.GoodsRefreshType)
		{
			return false;
		}
		if (NumLimit != other.NumLimit)
		{
			return false;
		}
		if (GoodsLabelType != other.GoodsLabelType)
		{
			return false;
		}
		if (!object.Equals(BeginTime, other.BeginTime))
		{
			return false;
		}
		if (!object.Equals(EndTime, other.EndTime))
		{
			return false;
		}
		if (Param != other.Param)
		{
			return false;
		}
		if (Great != other.Great)
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
		if (GoodsOrder != 0)
		{
			num ^= GoodsOrder.GetHashCode();
		}
		if (RechargeIcon.Length != 0)
		{
			num ^= RechargeIcon.GetHashCode();
		}
		if (ItemType.Length != 0)
		{
			num ^= ItemType.GetHashCode();
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
		if (beginTimeLimited_ != null)
		{
			num ^= BeginTimeLimited.GetHashCode();
		}
		if (endTimeLimited_ != null)
		{
			num ^= EndTimeLimited.GetHashCode();
		}
		if (DiscountPriceLimited != 0)
		{
			num ^= DiscountPriceLimited.GetHashCode();
		}
		if (GoodsRefreshType != GoodsRefreshType.None)
		{
			num ^= GoodsRefreshType.GetHashCode();
		}
		if (NumLimit != 0)
		{
			num ^= NumLimit.GetHashCode();
		}
		if (GoodsLabelType != GoodsLabelType.None)
		{
			num ^= GoodsLabelType.GetHashCode();
		}
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (Param != 0)
		{
			num ^= Param.GetHashCode();
		}
		if (Great != 0)
		{
			num ^= Great.GetHashCode();
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
		if (GoodsOrder != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(GoodsOrder);
		}
		if (RechargeIcon.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(RechargeIcon);
		}
		if (ItemType.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(ItemType);
		}
		if (ItemID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(ItemID);
		}
		if (ItemNum != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(ItemNum);
		}
		if (CurrencyID != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(CurrencyID);
		}
		if (OriginalPrice != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(OriginalPrice);
		}
		if (DiscountPrice != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(DiscountPrice);
		}
		if (beginTimeLimited_ != null)
		{
			output.WriteRawTag(82);
			output.WriteMessage(BeginTimeLimited);
		}
		if (endTimeLimited_ != null)
		{
			output.WriteRawTag(90);
			output.WriteMessage(EndTimeLimited);
		}
		if (DiscountPriceLimited != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(DiscountPriceLimited);
		}
		if (GoodsRefreshType != GoodsRefreshType.None)
		{
			output.WriteRawTag(104);
			output.WriteEnum((int)GoodsRefreshType);
		}
		if (NumLimit != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(NumLimit);
		}
		if (GoodsLabelType != GoodsLabelType.None)
		{
			output.WriteRawTag(120);
			output.WriteEnum((int)GoodsLabelType);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(130, 1);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(138, 1);
			output.WriteMessage(EndTime);
		}
		if (Param != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(Param);
		}
		if (Great != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(Great);
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
		if (GoodsOrder != 0)
		{
			num += 5;
		}
		if (RechargeIcon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(RechargeIcon);
		}
		if (ItemType.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ItemType);
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
		if (beginTimeLimited_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTimeLimited);
		}
		if (endTimeLimited_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTimeLimited);
		}
		if (DiscountPriceLimited != 0)
		{
			num += 5;
		}
		if (GoodsRefreshType != GoodsRefreshType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GoodsRefreshType);
		}
		if (NumLimit != 0)
		{
			num += 5;
		}
		if (GoodsLabelType != GoodsLabelType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GoodsLabelType);
		}
		if (beginTime_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (Param != 0)
		{
			num += 6;
		}
		if (Great != 0)
		{
			num += 6;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ExchangeStoreGoodsConfigureItem other)
	{
		if (other == null)
		{
			return;
		}
		if (other.GoodsID != 0)
		{
			GoodsID = other.GoodsID;
		}
		if (other.GoodsOrder != 0)
		{
			GoodsOrder = other.GoodsOrder;
		}
		if (other.RechargeIcon.Length != 0)
		{
			RechargeIcon = other.RechargeIcon;
		}
		if (other.ItemType.Length != 0)
		{
			ItemType = other.ItemType;
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
		if (other.beginTimeLimited_ != null)
		{
			if (beginTimeLimited_ == null)
			{
				BeginTimeLimited = new Timestamp();
			}
			BeginTimeLimited.MergeFrom(other.BeginTimeLimited);
		}
		if (other.endTimeLimited_ != null)
		{
			if (endTimeLimited_ == null)
			{
				EndTimeLimited = new Timestamp();
			}
			EndTimeLimited.MergeFrom(other.EndTimeLimited);
		}
		if (other.DiscountPriceLimited != 0)
		{
			DiscountPriceLimited = other.DiscountPriceLimited;
		}
		if (other.GoodsRefreshType != GoodsRefreshType.None)
		{
			GoodsRefreshType = other.GoodsRefreshType;
		}
		if (other.NumLimit != 0)
		{
			NumLimit = other.NumLimit;
		}
		if (other.GoodsLabelType != GoodsLabelType.None)
		{
			GoodsLabelType = other.GoodsLabelType;
		}
		if (other.beginTime_ != null)
		{
			if (beginTime_ == null)
			{
				BeginTime = new Timestamp();
			}
			BeginTime.MergeFrom(other.BeginTime);
		}
		if (other.endTime_ != null)
		{
			if (endTime_ == null)
			{
				EndTime = new Timestamp();
			}
			EndTime.MergeFrom(other.EndTime);
		}
		if (other.Param != 0)
		{
			Param = other.Param;
		}
		if (other.Great != 0)
		{
			Great = other.Great;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				GoodsOrder = input.ReadSFixed32();
				break;
			case 26u:
				RechargeIcon = input.ReadString();
				break;
			case 34u:
				ItemType = input.ReadString();
				break;
			case 45u:
				ItemID = input.ReadSFixed32();
				break;
			case 53u:
				ItemNum = input.ReadSFixed32();
				break;
			case 61u:
				CurrencyID = input.ReadSFixed32();
				break;
			case 69u:
				OriginalPrice = input.ReadSFixed32();
				break;
			case 77u:
				DiscountPrice = input.ReadSFixed32();
				break;
			case 82u:
				if (beginTimeLimited_ == null)
				{
					BeginTimeLimited = new Timestamp();
				}
				input.ReadMessage(BeginTimeLimited);
				break;
			case 90u:
				if (endTimeLimited_ == null)
				{
					EndTimeLimited = new Timestamp();
				}
				input.ReadMessage(EndTimeLimited);
				break;
			case 101u:
				DiscountPriceLimited = input.ReadSFixed32();
				break;
			case 104u:
				GoodsRefreshType = (GoodsRefreshType)input.ReadEnum();
				break;
			case 117u:
				NumLimit = input.ReadSFixed32();
				break;
			case 120u:
				GoodsLabelType = (GoodsLabelType)input.ReadEnum();
				break;
			case 130u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 138u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 149u:
				Param = input.ReadSFixed32();
				break;
			case 157u:
				Great = input.ReadSFixed32();
				break;
			}
		}
	}
}
