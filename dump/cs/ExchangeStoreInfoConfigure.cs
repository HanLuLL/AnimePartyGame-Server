using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class ExchangeStoreInfoConfigure : IMessage<ExchangeStoreInfoConfigure>, IMessage, IEquatable<ExchangeStoreInfoConfigure>, IDeepCloneable<ExchangeStoreInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<ExchangeStoreInfoConfigure> _parser = new MessageParser<ExchangeStoreInfoConfigure>(() => new ExchangeStoreInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ShopTabTypeFieldNumber = 1;

	private ShopTabType shopTabType_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int CurrencyBarFieldNumber = 3;

	private int currencyBar_;

	public const int BeginTimeFieldNumber = 4;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 5;

	private Timestamp endTime_;

	public const int GoodsRefreshTypeFieldNumber = 6;

	private GoodsRefreshType goodsRefreshType_;

	public const int WorkdayGoodsFieldNumber = 7;

	private static readonly MapField<int, int>.Codec _map_workdayGoods_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 58u);

	private readonly MapField<int, int> workdayGoods_ = new MapField<int, int>();

	public const int WeekendGoodsFieldNumber = 8;

	private static readonly MapField<int, int>.Codec _map_weekendGoods_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 66u);

	private readonly MapField<int, int> weekendGoods_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExchangeStoreInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ExchangeStoreReflection.Descriptor.MessageTypes[1];

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
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CurrencyBar
	{
		get
		{
			return currencyBar_;
		}
		private set
		{
			currencyBar_ = value;
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
	public MapField<int, int> WorkdayGoods => workdayGoods_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> WeekendGoods => weekendGoods_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreInfoConfigure(ExchangeStoreInfoConfigure other)
		: this()
	{
		shopTabType_ = other.shopTabType_;
		nameID_ = other.nameID_;
		currencyBar_ = other.currencyBar_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		goodsRefreshType_ = other.goodsRefreshType_;
		workdayGoods_ = other.workdayGoods_.Clone();
		weekendGoods_ = other.weekendGoods_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreInfoConfigure Clone()
	{
		return new ExchangeStoreInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExchangeStoreInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExchangeStoreInfoConfigure other)
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
		if (NameID != other.NameID)
		{
			return false;
		}
		if (CurrencyBar != other.CurrencyBar)
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
		if (GoodsRefreshType != other.GoodsRefreshType)
		{
			return false;
		}
		if (!WorkdayGoods.Equals(other.WorkdayGoods))
		{
			return false;
		}
		if (!WeekendGoods.Equals(other.WeekendGoods))
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
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (CurrencyBar != 0)
		{
			num ^= CurrencyBar.GetHashCode();
		}
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (GoodsRefreshType != GoodsRefreshType.None)
		{
			num ^= GoodsRefreshType.GetHashCode();
		}
		num ^= WorkdayGoods.GetHashCode();
		num ^= WeekendGoods.GetHashCode();
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
		if (NameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NameID);
		}
		if (CurrencyBar != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(CurrencyBar);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(34);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(42);
			output.WriteMessage(EndTime);
		}
		if (GoodsRefreshType != GoodsRefreshType.None)
		{
			output.WriteRawTag(48);
			output.WriteEnum((int)GoodsRefreshType);
		}
		workdayGoods_.WriteTo(ref output, _map_workdayGoods_codec);
		weekendGoods_.WriteTo(ref output, _map_weekendGoods_codec);
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
		if (NameID != 0)
		{
			num += 5;
		}
		if (CurrencyBar != 0)
		{
			num += 5;
		}
		if (beginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (GoodsRefreshType != GoodsRefreshType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GoodsRefreshType);
		}
		num += workdayGoods_.CalculateSize(_map_workdayGoods_codec);
		num += weekendGoods_.CalculateSize(_map_weekendGoods_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ExchangeStoreInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.ShopTabType != ShopTabType.None)
		{
			ShopTabType = other.ShopTabType;
		}
		if (other.NameID != 0)
		{
			NameID = other.NameID;
		}
		if (other.CurrencyBar != 0)
		{
			CurrencyBar = other.CurrencyBar;
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
		if (other.GoodsRefreshType != GoodsRefreshType.None)
		{
			GoodsRefreshType = other.GoodsRefreshType;
		}
		workdayGoods_.MergeFrom(other.workdayGoods_);
		weekendGoods_.MergeFrom(other.weekendGoods_);
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
			case 8u:
				ShopTabType = (ShopTabType)input.ReadEnum();
				break;
			case 21u:
				NameID = input.ReadSFixed32();
				break;
			case 29u:
				CurrencyBar = input.ReadSFixed32();
				break;
			case 34u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 42u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 48u:
				GoodsRefreshType = (GoodsRefreshType)input.ReadEnum();
				break;
			case 58u:
				workdayGoods_.AddEntriesFrom(ref input, _map_workdayGoods_codec);
				break;
			case 66u:
				weekendGoods_.AddEntriesFrom(ref input, _map_weekendGoods_codec);
				break;
			}
		}
	}
}
