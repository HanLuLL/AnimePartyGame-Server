using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class RechargeStoreGoodsConfigureItem : IMessage<RechargeStoreGoodsConfigureItem>, IMessage, IEquatable<RechargeStoreGoodsConfigureItem>, IDeepCloneable<RechargeStoreGoodsConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<RechargeStoreGoodsConfigureItem> _parser = new MessageParser<RechargeStoreGoodsConfigureItem>(() => new RechargeStoreGoodsConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int GoodsIDFieldNumber = 1;

	private int goodsID_;

	public const int OperationGoodsIDFieldNumber = 2;

	private string operationGoodsID_ = "";

	public const int GoodsOrderFieldNumber = 3;

	private int goodsOrder_;

	public const int MerchandiselTypeFieldNumber = 4;

	private MerchandiselType merchandiselType_;

	public const int NameFieldNumber = 5;

	private int name_;

	public const int RechargeIconFieldNumber = 6;

	private string rechargeIcon_ = "";

	public const int ItemTypeFieldNumber = 7;

	private string itemType_ = "";

	public const int ItemIDFieldNumber = 8;

	private int itemID_;

	public const int ItemNumFieldNumber = 9;

	private int itemNum_;

	public const int BonuseItemIDFieldNumber = 10;

	private int bonuseItemID_;

	public const int GoodsBonusesFieldNumber = 11;

	private int goodsBonuses_;

	public const int BonusesDescriptionFieldNumber = 12;

	private int bonusesDescription_;

	public const int OriginalPriceCNFieldNumber = 13;

	private int originalPriceCN_;

	public const int DiscountPriceCNFieldNumber = 14;

	private int discountPriceCN_;

	public const int OriginalPriceUSFieldNumber = 15;

	private int originalPriceUS_;

	public const int DiscountPriceUSFieldNumber = 16;

	private int discountPriceUS_;

	public const int OriginalPriceJPFieldNumber = 17;

	private int originalPriceJP_;

	public const int DiscountPriceJPFieldNumber = 18;

	private int discountPriceJP_;

	public const int BeginTimeLimitedFieldNumber = 19;

	private Timestamp beginTimeLimited_;

	public const int EndTimeLimitedFieldNumber = 20;

	private Timestamp endTimeLimited_;

	public const int DiscountPriceLimitedCNFieldNumber = 21;

	private int discountPriceLimitedCN_;

	public const int DiscountPriceLimitedUSFieldNumber = 22;

	private int discountPriceLimitedUS_;

	public const int DiscountPriceLimitedJPFieldNumber = 23;

	private int discountPriceLimitedJP_;

	public const int GoodsRefreshTypeFieldNumber = 24;

	private GoodsRefreshType goodsRefreshType_;

	public const int NumLimitFieldNumber = 25;

	private int numLimit_;

	public const int GoodsLabelTypeFieldNumber = 26;

	private GoodsLabelType goodsLabelType_;

	public const int BeginTimeFieldNumber = 27;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 28;

	private Timestamp endTime_;

	public const int ParamFieldNumber = 29;

	private int param_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeStoreGoodsConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeStoreReflection.Descriptor.MessageTypes[3];

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
	public string OperationGoodsID
	{
		get
		{
			return operationGoodsID_;
		}
		private set
		{
			operationGoodsID_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public MerchandiselType MerchandiselType
	{
		get
		{
			return merchandiselType_;
		}
		private set
		{
			merchandiselType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Name
	{
		get
		{
			return name_;
		}
		private set
		{
			name_ = value;
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
	public int BonuseItemID
	{
		get
		{
			return bonuseItemID_;
		}
		private set
		{
			bonuseItemID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoodsBonuses
	{
		get
		{
			return goodsBonuses_;
		}
		private set
		{
			goodsBonuses_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BonusesDescription
	{
		get
		{
			return bonusesDescription_;
		}
		private set
		{
			bonusesDescription_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OriginalPriceCN
	{
		get
		{
			return originalPriceCN_;
		}
		private set
		{
			originalPriceCN_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiscountPriceCN
	{
		get
		{
			return discountPriceCN_;
		}
		private set
		{
			discountPriceCN_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OriginalPriceUS
	{
		get
		{
			return originalPriceUS_;
		}
		private set
		{
			originalPriceUS_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiscountPriceUS
	{
		get
		{
			return discountPriceUS_;
		}
		private set
		{
			discountPriceUS_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OriginalPriceJP
	{
		get
		{
			return originalPriceJP_;
		}
		private set
		{
			originalPriceJP_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiscountPriceJP
	{
		get
		{
			return discountPriceJP_;
		}
		private set
		{
			discountPriceJP_ = value;
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
	public int DiscountPriceLimitedCN
	{
		get
		{
			return discountPriceLimitedCN_;
		}
		private set
		{
			discountPriceLimitedCN_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiscountPriceLimitedUS
	{
		get
		{
			return discountPriceLimitedUS_;
		}
		private set
		{
			discountPriceLimitedUS_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiscountPriceLimitedJP
	{
		get
		{
			return discountPriceLimitedJP_;
		}
		private set
		{
			discountPriceLimitedJP_ = value;
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
	public RechargeStoreGoodsConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreGoodsConfigureItem(RechargeStoreGoodsConfigureItem other)
		: this()
	{
		goodsID_ = other.goodsID_;
		operationGoodsID_ = other.operationGoodsID_;
		goodsOrder_ = other.goodsOrder_;
		merchandiselType_ = other.merchandiselType_;
		name_ = other.name_;
		rechargeIcon_ = other.rechargeIcon_;
		itemType_ = other.itemType_;
		itemID_ = other.itemID_;
		itemNum_ = other.itemNum_;
		bonuseItemID_ = other.bonuseItemID_;
		goodsBonuses_ = other.goodsBonuses_;
		bonusesDescription_ = other.bonusesDescription_;
		originalPriceCN_ = other.originalPriceCN_;
		discountPriceCN_ = other.discountPriceCN_;
		originalPriceUS_ = other.originalPriceUS_;
		discountPriceUS_ = other.discountPriceUS_;
		originalPriceJP_ = other.originalPriceJP_;
		discountPriceJP_ = other.discountPriceJP_;
		beginTimeLimited_ = ((other.beginTimeLimited_ != null) ? other.beginTimeLimited_.Clone() : null);
		endTimeLimited_ = ((other.endTimeLimited_ != null) ? other.endTimeLimited_.Clone() : null);
		discountPriceLimitedCN_ = other.discountPriceLimitedCN_;
		discountPriceLimitedUS_ = other.discountPriceLimitedUS_;
		discountPriceLimitedJP_ = other.discountPriceLimitedJP_;
		goodsRefreshType_ = other.goodsRefreshType_;
		numLimit_ = other.numLimit_;
		goodsLabelType_ = other.goodsLabelType_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		param_ = other.param_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreGoodsConfigureItem Clone()
	{
		return new RechargeStoreGoodsConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeStoreGoodsConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeStoreGoodsConfigureItem other)
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
		if (OperationGoodsID != other.OperationGoodsID)
		{
			return false;
		}
		if (GoodsOrder != other.GoodsOrder)
		{
			return false;
		}
		if (MerchandiselType != other.MerchandiselType)
		{
			return false;
		}
		if (Name != other.Name)
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
		if (BonuseItemID != other.BonuseItemID)
		{
			return false;
		}
		if (GoodsBonuses != other.GoodsBonuses)
		{
			return false;
		}
		if (BonusesDescription != other.BonusesDescription)
		{
			return false;
		}
		if (OriginalPriceCN != other.OriginalPriceCN)
		{
			return false;
		}
		if (DiscountPriceCN != other.DiscountPriceCN)
		{
			return false;
		}
		if (OriginalPriceUS != other.OriginalPriceUS)
		{
			return false;
		}
		if (DiscountPriceUS != other.DiscountPriceUS)
		{
			return false;
		}
		if (OriginalPriceJP != other.OriginalPriceJP)
		{
			return false;
		}
		if (DiscountPriceJP != other.DiscountPriceJP)
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
		if (DiscountPriceLimitedCN != other.DiscountPriceLimitedCN)
		{
			return false;
		}
		if (DiscountPriceLimitedUS != other.DiscountPriceLimitedUS)
		{
			return false;
		}
		if (DiscountPriceLimitedJP != other.DiscountPriceLimitedJP)
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
		if (OperationGoodsID.Length != 0)
		{
			num ^= OperationGoodsID.GetHashCode();
		}
		if (GoodsOrder != 0)
		{
			num ^= GoodsOrder.GetHashCode();
		}
		if (MerchandiselType != MerchandiselType.None)
		{
			num ^= MerchandiselType.GetHashCode();
		}
		if (Name != 0)
		{
			num ^= Name.GetHashCode();
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
		if (BonuseItemID != 0)
		{
			num ^= BonuseItemID.GetHashCode();
		}
		if (GoodsBonuses != 0)
		{
			num ^= GoodsBonuses.GetHashCode();
		}
		if (BonusesDescription != 0)
		{
			num ^= BonusesDescription.GetHashCode();
		}
		if (OriginalPriceCN != 0)
		{
			num ^= OriginalPriceCN.GetHashCode();
		}
		if (DiscountPriceCN != 0)
		{
			num ^= DiscountPriceCN.GetHashCode();
		}
		if (OriginalPriceUS != 0)
		{
			num ^= OriginalPriceUS.GetHashCode();
		}
		if (DiscountPriceUS != 0)
		{
			num ^= DiscountPriceUS.GetHashCode();
		}
		if (OriginalPriceJP != 0)
		{
			num ^= OriginalPriceJP.GetHashCode();
		}
		if (DiscountPriceJP != 0)
		{
			num ^= DiscountPriceJP.GetHashCode();
		}
		if (beginTimeLimited_ != null)
		{
			num ^= BeginTimeLimited.GetHashCode();
		}
		if (endTimeLimited_ != null)
		{
			num ^= EndTimeLimited.GetHashCode();
		}
		if (DiscountPriceLimitedCN != 0)
		{
			num ^= DiscountPriceLimitedCN.GetHashCode();
		}
		if (DiscountPriceLimitedUS != 0)
		{
			num ^= DiscountPriceLimitedUS.GetHashCode();
		}
		if (DiscountPriceLimitedJP != 0)
		{
			num ^= DiscountPriceLimitedJP.GetHashCode();
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
		if (OperationGoodsID.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(OperationGoodsID);
		}
		if (GoodsOrder != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(GoodsOrder);
		}
		if (MerchandiselType != MerchandiselType.None)
		{
			output.WriteRawTag(32);
			output.WriteEnum((int)MerchandiselType);
		}
		if (Name != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Name);
		}
		if (RechargeIcon.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(RechargeIcon);
		}
		if (ItemType.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(ItemType);
		}
		if (ItemID != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(ItemID);
		}
		if (ItemNum != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(ItemNum);
		}
		if (BonuseItemID != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(BonuseItemID);
		}
		if (GoodsBonuses != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(GoodsBonuses);
		}
		if (BonusesDescription != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(BonusesDescription);
		}
		if (OriginalPriceCN != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(OriginalPriceCN);
		}
		if (DiscountPriceCN != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(DiscountPriceCN);
		}
		if (OriginalPriceUS != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(OriginalPriceUS);
		}
		if (DiscountPriceUS != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(DiscountPriceUS);
		}
		if (OriginalPriceJP != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(OriginalPriceJP);
		}
		if (DiscountPriceJP != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(DiscountPriceJP);
		}
		if (beginTimeLimited_ != null)
		{
			output.WriteRawTag(154, 1);
			output.WriteMessage(BeginTimeLimited);
		}
		if (endTimeLimited_ != null)
		{
			output.WriteRawTag(162, 1);
			output.WriteMessage(EndTimeLimited);
		}
		if (DiscountPriceLimitedCN != 0)
		{
			output.WriteRawTag(173, 1);
			output.WriteSFixed32(DiscountPriceLimitedCN);
		}
		if (DiscountPriceLimitedUS != 0)
		{
			output.WriteRawTag(181, 1);
			output.WriteSFixed32(DiscountPriceLimitedUS);
		}
		if (DiscountPriceLimitedJP != 0)
		{
			output.WriteRawTag(189, 1);
			output.WriteSFixed32(DiscountPriceLimitedJP);
		}
		if (GoodsRefreshType != GoodsRefreshType.None)
		{
			output.WriteRawTag(192, 1);
			output.WriteEnum((int)GoodsRefreshType);
		}
		if (NumLimit != 0)
		{
			output.WriteRawTag(205, 1);
			output.WriteSFixed32(NumLimit);
		}
		if (GoodsLabelType != GoodsLabelType.None)
		{
			output.WriteRawTag(208, 1);
			output.WriteEnum((int)GoodsLabelType);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(218, 1);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(226, 1);
			output.WriteMessage(EndTime);
		}
		if (Param != 0)
		{
			output.WriteRawTag(237, 1);
			output.WriteSFixed32(Param);
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
		if (OperationGoodsID.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(OperationGoodsID);
		}
		if (GoodsOrder != 0)
		{
			num += 5;
		}
		if (MerchandiselType != MerchandiselType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MerchandiselType);
		}
		if (Name != 0)
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
		if (BonuseItemID != 0)
		{
			num += 5;
		}
		if (GoodsBonuses != 0)
		{
			num += 5;
		}
		if (BonusesDescription != 0)
		{
			num += 5;
		}
		if (OriginalPriceCN != 0)
		{
			num += 5;
		}
		if (DiscountPriceCN != 0)
		{
			num += 5;
		}
		if (OriginalPriceUS != 0)
		{
			num += 5;
		}
		if (DiscountPriceUS != 0)
		{
			num += 6;
		}
		if (OriginalPriceJP != 0)
		{
			num += 6;
		}
		if (DiscountPriceJP != 0)
		{
			num += 6;
		}
		if (beginTimeLimited_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BeginTimeLimited);
		}
		if (endTimeLimited_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(EndTimeLimited);
		}
		if (DiscountPriceLimitedCN != 0)
		{
			num += 6;
		}
		if (DiscountPriceLimitedUS != 0)
		{
			num += 6;
		}
		if (DiscountPriceLimitedJP != 0)
		{
			num += 6;
		}
		if (GoodsRefreshType != GoodsRefreshType.None)
		{
			num += 2 + CodedOutputStream.ComputeEnumSize((int)GoodsRefreshType);
		}
		if (NumLimit != 0)
		{
			num += 6;
		}
		if (GoodsLabelType != GoodsLabelType.None)
		{
			num += 2 + CodedOutputStream.ComputeEnumSize((int)GoodsLabelType);
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
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RechargeStoreGoodsConfigureItem other)
	{
		if (other == null)
		{
			return;
		}
		if (other.GoodsID != 0)
		{
			GoodsID = other.GoodsID;
		}
		if (other.OperationGoodsID.Length != 0)
		{
			OperationGoodsID = other.OperationGoodsID;
		}
		if (other.GoodsOrder != 0)
		{
			GoodsOrder = other.GoodsOrder;
		}
		if (other.MerchandiselType != MerchandiselType.None)
		{
			MerchandiselType = other.MerchandiselType;
		}
		if (other.Name != 0)
		{
			Name = other.Name;
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
		if (other.BonuseItemID != 0)
		{
			BonuseItemID = other.BonuseItemID;
		}
		if (other.GoodsBonuses != 0)
		{
			GoodsBonuses = other.GoodsBonuses;
		}
		if (other.BonusesDescription != 0)
		{
			BonusesDescription = other.BonusesDescription;
		}
		if (other.OriginalPriceCN != 0)
		{
			OriginalPriceCN = other.OriginalPriceCN;
		}
		if (other.DiscountPriceCN != 0)
		{
			DiscountPriceCN = other.DiscountPriceCN;
		}
		if (other.OriginalPriceUS != 0)
		{
			OriginalPriceUS = other.OriginalPriceUS;
		}
		if (other.DiscountPriceUS != 0)
		{
			DiscountPriceUS = other.DiscountPriceUS;
		}
		if (other.OriginalPriceJP != 0)
		{
			OriginalPriceJP = other.OriginalPriceJP;
		}
		if (other.DiscountPriceJP != 0)
		{
			DiscountPriceJP = other.DiscountPriceJP;
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
		if (other.DiscountPriceLimitedCN != 0)
		{
			DiscountPriceLimitedCN = other.DiscountPriceLimitedCN;
		}
		if (other.DiscountPriceLimitedUS != 0)
		{
			DiscountPriceLimitedUS = other.DiscountPriceLimitedUS;
		}
		if (other.DiscountPriceLimitedJP != 0)
		{
			DiscountPriceLimitedJP = other.DiscountPriceLimitedJP;
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
			case 18u:
				OperationGoodsID = input.ReadString();
				break;
			case 29u:
				GoodsOrder = input.ReadSFixed32();
				break;
			case 32u:
				MerchandiselType = (MerchandiselType)input.ReadEnum();
				break;
			case 45u:
				Name = input.ReadSFixed32();
				break;
			case 50u:
				RechargeIcon = input.ReadString();
				break;
			case 58u:
				ItemType = input.ReadString();
				break;
			case 69u:
				ItemID = input.ReadSFixed32();
				break;
			case 77u:
				ItemNum = input.ReadSFixed32();
				break;
			case 85u:
				BonuseItemID = input.ReadSFixed32();
				break;
			case 93u:
				GoodsBonuses = input.ReadSFixed32();
				break;
			case 101u:
				BonusesDescription = input.ReadSFixed32();
				break;
			case 109u:
				OriginalPriceCN = input.ReadSFixed32();
				break;
			case 117u:
				DiscountPriceCN = input.ReadSFixed32();
				break;
			case 125u:
				OriginalPriceUS = input.ReadSFixed32();
				break;
			case 133u:
				DiscountPriceUS = input.ReadSFixed32();
				break;
			case 141u:
				OriginalPriceJP = input.ReadSFixed32();
				break;
			case 149u:
				DiscountPriceJP = input.ReadSFixed32();
				break;
			case 154u:
				if (beginTimeLimited_ == null)
				{
					BeginTimeLimited = new Timestamp();
				}
				input.ReadMessage(BeginTimeLimited);
				break;
			case 162u:
				if (endTimeLimited_ == null)
				{
					EndTimeLimited = new Timestamp();
				}
				input.ReadMessage(EndTimeLimited);
				break;
			case 173u:
				DiscountPriceLimitedCN = input.ReadSFixed32();
				break;
			case 181u:
				DiscountPriceLimitedUS = input.ReadSFixed32();
				break;
			case 189u:
				DiscountPriceLimitedJP = input.ReadSFixed32();
				break;
			case 192u:
				GoodsRefreshType = (GoodsRefreshType)input.ReadEnum();
				break;
			case 205u:
				NumLimit = input.ReadSFixed32();
				break;
			case 208u:
				GoodsLabelType = (GoodsLabelType)input.ReadEnum();
				break;
			case 218u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 226u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 237u:
				Param = input.ReadSFixed32();
				break;
			}
		}
	}
}
