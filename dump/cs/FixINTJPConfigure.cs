using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixINTJPConfigure : IMessage<FixINTJPConfigure>, IMessage, IEquatable<FixINTJPConfigure>, IDeepCloneable<FixINTJPConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixINTJPConfigure> _parser = new MessageParser<FixINTJPConfigure>(() => new FixINTJPConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ItemInfosFieldNumber = 1;

	private static readonly FieldCodec<FixINTJPItemInfoConfigure> _repeated_itemInfos_codec = FieldCodec.ForMessage(10u, FixINTJPItemInfoConfigure.Parser);

	private readonly RepeatedField<FixINTJPItemInfoConfigure> itemInfos_ = new RepeatedField<FixINTJPItemInfoConfigure>();

	public const int ItemInfoDictFieldNumber = 2;

	private static readonly MapField<int, FixINTJPItemInfoConfigure>.Codec _map_itemInfoDict_codec = new MapField<int, FixINTJPItemInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixINTJPItemInfoConfigure.Parser), 18u);

	private readonly MapField<int, FixINTJPItemInfoConfigure> itemInfoDict_ = new MapField<int, FixINTJPItemInfoConfigure>();

	public const int FashionAccountBackgroundsFieldNumber = 3;

	private static readonly FieldCodec<FixINTJPFashionAccountBackgroundConfigure> _repeated_fashionAccountBackgrounds_codec = FieldCodec.ForMessage(26u, FixINTJPFashionAccountBackgroundConfigure.Parser);

	private readonly RepeatedField<FixINTJPFashionAccountBackgroundConfigure> fashionAccountBackgrounds_ = new RepeatedField<FixINTJPFashionAccountBackgroundConfigure>();

	public const int FashionAccountBackgroundDictFieldNumber = 4;

	private static readonly MapField<int, FixINTJPFashionAccountBackgroundConfigure>.Codec _map_fashionAccountBackgroundDict_codec = new MapField<int, FixINTJPFashionAccountBackgroundConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixINTJPFashionAccountBackgroundConfigure.Parser), 34u);

	private readonly MapField<int, FixINTJPFashionAccountBackgroundConfigure> fashionAccountBackgroundDict_ = new MapField<int, FixINTJPFashionAccountBackgroundConfigure>();

	public const int MapEventInfosFieldNumber = 5;

	private static readonly FieldCodec<FixINTJPMapEventInfoConfigure> _repeated_mapEventInfos_codec = FieldCodec.ForMessage(42u, FixINTJPMapEventInfoConfigure.Parser);

	private readonly RepeatedField<FixINTJPMapEventInfoConfigure> mapEventInfos_ = new RepeatedField<FixINTJPMapEventInfoConfigure>();

	public const int MapEventInfoDictFieldNumber = 6;

	private static readonly MapField<int, FixINTJPMapEventInfoConfigure>.Codec _map_mapEventInfoDict_codec = new MapField<int, FixINTJPMapEventInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixINTJPMapEventInfoConfigure.Parser), 50u);

	private readonly MapField<int, FixINTJPMapEventInfoConfigure> mapEventInfoDict_ = new MapField<int, FixINTJPMapEventInfoConfigure>();

	public const int SkinStandingPaintingsFieldNumber = 7;

	private static readonly FieldCodec<FixINTJPSkinStandingPaintingConfigure> _repeated_skinStandingPaintings_codec = FieldCodec.ForMessage(58u, FixINTJPSkinStandingPaintingConfigure.Parser);

	private readonly RepeatedField<FixINTJPSkinStandingPaintingConfigure> skinStandingPaintings_ = new RepeatedField<FixINTJPSkinStandingPaintingConfigure>();

	public const int SkinStandingPaintingDictFieldNumber = 8;

	private static readonly MapField<int, FixINTJPSkinStandingPaintingConfigure>.Codec _map_skinStandingPaintingDict_codec = new MapField<int, FixINTJPSkinStandingPaintingConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixINTJPSkinStandingPaintingConfigure.Parser), 66u);

	private readonly MapField<int, FixINTJPSkinStandingPaintingConfigure> skinStandingPaintingDict_ = new MapField<int, FixINTJPSkinStandingPaintingConfigure>();

	public const int BannersFieldNumber = 9;

	private static readonly FieldCodec<FixINTJPBannerConfigure> _repeated_banners_codec = FieldCodec.ForMessage(74u, FixINTJPBannerConfigure.Parser);

	private readonly RepeatedField<FixINTJPBannerConfigure> banners_ = new RepeatedField<FixINTJPBannerConfigure>();

	public const int BannerDictFieldNumber = 10;

	private static readonly MapField<int, FixINTJPBannerConfigure>.Codec _map_bannerDict_codec = new MapField<int, FixINTJPBannerConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixINTJPBannerConfigure.Parser), 82u);

	private readonly MapField<int, FixINTJPBannerConfigure> bannerDict_ = new MapField<int, FixINTJPBannerConfigure>();

	public const int SpecifiedCommercialTransactionssFieldNumber = 11;

	private static readonly FieldCodec<FixINTJPSpecifiedCommercialTransactionsConfigure> _repeated_specifiedCommercialTransactionss_codec = FieldCodec.ForMessage(90u, FixINTJPSpecifiedCommercialTransactionsConfigure.Parser);

	private readonly RepeatedField<FixINTJPSpecifiedCommercialTransactionsConfigure> specifiedCommercialTransactionss_ = new RepeatedField<FixINTJPSpecifiedCommercialTransactionsConfigure>();

	public const int SpecifiedCommercialTransactionsDictFieldNumber = 12;

	private static readonly MapField<int, FixINTJPSpecifiedCommercialTransactionsConfigure>.Codec _map_specifiedCommercialTransactionsDict_codec = new MapField<int, FixINTJPSpecifiedCommercialTransactionsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixINTJPSpecifiedCommercialTransactionsConfigure.Parser), 98u);

	private readonly MapField<int, FixINTJPSpecifiedCommercialTransactionsConfigure> specifiedCommercialTransactionsDict_ = new MapField<int, FixINTJPSpecifiedCommercialTransactionsConfigure>();

	public const int PaymentServicesActsFieldNumber = 13;

	private static readonly FieldCodec<FixINTJPPaymentServicesActConfigure> _repeated_paymentServicesActs_codec = FieldCodec.ForMessage(106u, FixINTJPPaymentServicesActConfigure.Parser);

	private readonly RepeatedField<FixINTJPPaymentServicesActConfigure> paymentServicesActs_ = new RepeatedField<FixINTJPPaymentServicesActConfigure>();

	public const int PaymentServicesActDictFieldNumber = 14;

	private static readonly MapField<int, FixINTJPPaymentServicesActConfigure>.Codec _map_paymentServicesActDict_codec = new MapField<int, FixINTJPPaymentServicesActConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixINTJPPaymentServicesActConfigure.Parser), 114u);

	private readonly MapField<int, FixINTJPPaymentServicesActConfigure> paymentServicesActDict_ = new MapField<int, FixINTJPPaymentServicesActConfigure>();

	public const int RechargeLimitsFieldNumber = 15;

	private static readonly FieldCodec<FixINTJPRechargeLimitConfigure> _repeated_rechargeLimits_codec = FieldCodec.ForMessage(122u, FixINTJPRechargeLimitConfigure.Parser);

	private readonly RepeatedField<FixINTJPRechargeLimitConfigure> rechargeLimits_ = new RepeatedField<FixINTJPRechargeLimitConfigure>();

	public const int RechargeLimitDictFieldNumber = 16;

	private static readonly MapField<int, FixINTJPRechargeLimitConfigure>.Codec _map_rechargeLimitDict_codec = new MapField<int, FixINTJPRechargeLimitConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixINTJPRechargeLimitConfigure.Parser), 130u);

	private readonly MapField<int, FixINTJPRechargeLimitConfigure> rechargeLimitDict_ = new MapField<int, FixINTJPRechargeLimitConfigure>();

	public const int ExchangeStoreGoodssFieldNumber = 17;

	private static readonly FieldCodec<FixINTJPExchangeStoreGoodsConfigure> _repeated_exchangeStoreGoodss_codec = FieldCodec.ForMessage(138u, FixINTJPExchangeStoreGoodsConfigure.Parser);

	private readonly RepeatedField<FixINTJPExchangeStoreGoodsConfigure> exchangeStoreGoodss_ = new RepeatedField<FixINTJPExchangeStoreGoodsConfigure>();

	public const int ExchangeStoreGoodsDictFieldNumber = 18;

	private static readonly MapField<int, FixINTJPExchangeStoreGoodsConfigure>.Codec _map_exchangeStoreGoodsDict_codec = new MapField<int, FixINTJPExchangeStoreGoodsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixINTJPExchangeStoreGoodsConfigure.Parser), 146u);

	private readonly MapField<int, FixINTJPExchangeStoreGoodsConfigure> exchangeStoreGoodsDict_ = new MapField<int, FixINTJPExchangeStoreGoodsConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixINTJPConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixINTJPReflection.Descriptor.MessageTypes[11];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTJPItemInfoConfigure> ItemInfos => itemInfos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixINTJPItemInfoConfigure> ItemInfoDict => itemInfoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTJPFashionAccountBackgroundConfigure> FashionAccountBackgrounds => fashionAccountBackgrounds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixINTJPFashionAccountBackgroundConfigure> FashionAccountBackgroundDict => fashionAccountBackgroundDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTJPMapEventInfoConfigure> MapEventInfos => mapEventInfos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixINTJPMapEventInfoConfigure> MapEventInfoDict => mapEventInfoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTJPSkinStandingPaintingConfigure> SkinStandingPaintings => skinStandingPaintings_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixINTJPSkinStandingPaintingConfigure> SkinStandingPaintingDict => skinStandingPaintingDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTJPBannerConfigure> Banners => banners_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixINTJPBannerConfigure> BannerDict => bannerDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTJPSpecifiedCommercialTransactionsConfigure> SpecifiedCommercialTransactionss => specifiedCommercialTransactionss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixINTJPSpecifiedCommercialTransactionsConfigure> SpecifiedCommercialTransactionsDict => specifiedCommercialTransactionsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTJPPaymentServicesActConfigure> PaymentServicesActs => paymentServicesActs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixINTJPPaymentServicesActConfigure> PaymentServicesActDict => paymentServicesActDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTJPRechargeLimitConfigure> RechargeLimits => rechargeLimits_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixINTJPRechargeLimitConfigure> RechargeLimitDict => rechargeLimitDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTJPExchangeStoreGoodsConfigure> ExchangeStoreGoodss => exchangeStoreGoodss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixINTJPExchangeStoreGoodsConfigure> ExchangeStoreGoodsDict => exchangeStoreGoodsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPConfigure(FixINTJPConfigure other)
		: this()
	{
		itemInfos_ = other.itemInfos_.Clone();
		itemInfoDict_ = other.itemInfoDict_.Clone();
		fashionAccountBackgrounds_ = other.fashionAccountBackgrounds_.Clone();
		fashionAccountBackgroundDict_ = other.fashionAccountBackgroundDict_.Clone();
		mapEventInfos_ = other.mapEventInfos_.Clone();
		mapEventInfoDict_ = other.mapEventInfoDict_.Clone();
		skinStandingPaintings_ = other.skinStandingPaintings_.Clone();
		skinStandingPaintingDict_ = other.skinStandingPaintingDict_.Clone();
		banners_ = other.banners_.Clone();
		bannerDict_ = other.bannerDict_.Clone();
		specifiedCommercialTransactionss_ = other.specifiedCommercialTransactionss_.Clone();
		specifiedCommercialTransactionsDict_ = other.specifiedCommercialTransactionsDict_.Clone();
		paymentServicesActs_ = other.paymentServicesActs_.Clone();
		paymentServicesActDict_ = other.paymentServicesActDict_.Clone();
		rechargeLimits_ = other.rechargeLimits_.Clone();
		rechargeLimitDict_ = other.rechargeLimitDict_.Clone();
		exchangeStoreGoodss_ = other.exchangeStoreGoodss_.Clone();
		exchangeStoreGoodsDict_ = other.exchangeStoreGoodsDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPConfigure Clone()
	{
		return new FixINTJPConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixINTJPConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixINTJPConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!itemInfos_.Equals(other.itemInfos_))
		{
			return false;
		}
		if (!ItemInfoDict.Equals(other.ItemInfoDict))
		{
			return false;
		}
		if (!fashionAccountBackgrounds_.Equals(other.fashionAccountBackgrounds_))
		{
			return false;
		}
		if (!FashionAccountBackgroundDict.Equals(other.FashionAccountBackgroundDict))
		{
			return false;
		}
		if (!mapEventInfos_.Equals(other.mapEventInfos_))
		{
			return false;
		}
		if (!MapEventInfoDict.Equals(other.MapEventInfoDict))
		{
			return false;
		}
		if (!skinStandingPaintings_.Equals(other.skinStandingPaintings_))
		{
			return false;
		}
		if (!SkinStandingPaintingDict.Equals(other.SkinStandingPaintingDict))
		{
			return false;
		}
		if (!banners_.Equals(other.banners_))
		{
			return false;
		}
		if (!BannerDict.Equals(other.BannerDict))
		{
			return false;
		}
		if (!specifiedCommercialTransactionss_.Equals(other.specifiedCommercialTransactionss_))
		{
			return false;
		}
		if (!SpecifiedCommercialTransactionsDict.Equals(other.SpecifiedCommercialTransactionsDict))
		{
			return false;
		}
		if (!paymentServicesActs_.Equals(other.paymentServicesActs_))
		{
			return false;
		}
		if (!PaymentServicesActDict.Equals(other.PaymentServicesActDict))
		{
			return false;
		}
		if (!rechargeLimits_.Equals(other.rechargeLimits_))
		{
			return false;
		}
		if (!RechargeLimitDict.Equals(other.RechargeLimitDict))
		{
			return false;
		}
		if (!exchangeStoreGoodss_.Equals(other.exchangeStoreGoodss_))
		{
			return false;
		}
		if (!ExchangeStoreGoodsDict.Equals(other.ExchangeStoreGoodsDict))
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
		num ^= itemInfos_.GetHashCode();
		num ^= ItemInfoDict.GetHashCode();
		num ^= fashionAccountBackgrounds_.GetHashCode();
		num ^= FashionAccountBackgroundDict.GetHashCode();
		num ^= mapEventInfos_.GetHashCode();
		num ^= MapEventInfoDict.GetHashCode();
		num ^= skinStandingPaintings_.GetHashCode();
		num ^= SkinStandingPaintingDict.GetHashCode();
		num ^= banners_.GetHashCode();
		num ^= BannerDict.GetHashCode();
		num ^= specifiedCommercialTransactionss_.GetHashCode();
		num ^= SpecifiedCommercialTransactionsDict.GetHashCode();
		num ^= paymentServicesActs_.GetHashCode();
		num ^= PaymentServicesActDict.GetHashCode();
		num ^= rechargeLimits_.GetHashCode();
		num ^= RechargeLimitDict.GetHashCode();
		num ^= exchangeStoreGoodss_.GetHashCode();
		num ^= ExchangeStoreGoodsDict.GetHashCode();
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
		itemInfos_.WriteTo(ref output, _repeated_itemInfos_codec);
		itemInfoDict_.WriteTo(ref output, _map_itemInfoDict_codec);
		fashionAccountBackgrounds_.WriteTo(ref output, _repeated_fashionAccountBackgrounds_codec);
		fashionAccountBackgroundDict_.WriteTo(ref output, _map_fashionAccountBackgroundDict_codec);
		mapEventInfos_.WriteTo(ref output, _repeated_mapEventInfos_codec);
		mapEventInfoDict_.WriteTo(ref output, _map_mapEventInfoDict_codec);
		skinStandingPaintings_.WriteTo(ref output, _repeated_skinStandingPaintings_codec);
		skinStandingPaintingDict_.WriteTo(ref output, _map_skinStandingPaintingDict_codec);
		banners_.WriteTo(ref output, _repeated_banners_codec);
		bannerDict_.WriteTo(ref output, _map_bannerDict_codec);
		specifiedCommercialTransactionss_.WriteTo(ref output, _repeated_specifiedCommercialTransactionss_codec);
		specifiedCommercialTransactionsDict_.WriteTo(ref output, _map_specifiedCommercialTransactionsDict_codec);
		paymentServicesActs_.WriteTo(ref output, _repeated_paymentServicesActs_codec);
		paymentServicesActDict_.WriteTo(ref output, _map_paymentServicesActDict_codec);
		rechargeLimits_.WriteTo(ref output, _repeated_rechargeLimits_codec);
		rechargeLimitDict_.WriteTo(ref output, _map_rechargeLimitDict_codec);
		exchangeStoreGoodss_.WriteTo(ref output, _repeated_exchangeStoreGoodss_codec);
		exchangeStoreGoodsDict_.WriteTo(ref output, _map_exchangeStoreGoodsDict_codec);
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
		num += itemInfos_.CalculateSize(_repeated_itemInfos_codec);
		num += itemInfoDict_.CalculateSize(_map_itemInfoDict_codec);
		num += fashionAccountBackgrounds_.CalculateSize(_repeated_fashionAccountBackgrounds_codec);
		num += fashionAccountBackgroundDict_.CalculateSize(_map_fashionAccountBackgroundDict_codec);
		num += mapEventInfos_.CalculateSize(_repeated_mapEventInfos_codec);
		num += mapEventInfoDict_.CalculateSize(_map_mapEventInfoDict_codec);
		num += skinStandingPaintings_.CalculateSize(_repeated_skinStandingPaintings_codec);
		num += skinStandingPaintingDict_.CalculateSize(_map_skinStandingPaintingDict_codec);
		num += banners_.CalculateSize(_repeated_banners_codec);
		num += bannerDict_.CalculateSize(_map_bannerDict_codec);
		num += specifiedCommercialTransactionss_.CalculateSize(_repeated_specifiedCommercialTransactionss_codec);
		num += specifiedCommercialTransactionsDict_.CalculateSize(_map_specifiedCommercialTransactionsDict_codec);
		num += paymentServicesActs_.CalculateSize(_repeated_paymentServicesActs_codec);
		num += paymentServicesActDict_.CalculateSize(_map_paymentServicesActDict_codec);
		num += rechargeLimits_.CalculateSize(_repeated_rechargeLimits_codec);
		num += rechargeLimitDict_.CalculateSize(_map_rechargeLimitDict_codec);
		num += exchangeStoreGoodss_.CalculateSize(_repeated_exchangeStoreGoodss_codec);
		num += exchangeStoreGoodsDict_.CalculateSize(_map_exchangeStoreGoodsDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixINTJPConfigure other)
	{
		if (other != null)
		{
			itemInfos_.Add(other.itemInfos_);
			itemInfoDict_.MergeFrom(other.itemInfoDict_);
			fashionAccountBackgrounds_.Add(other.fashionAccountBackgrounds_);
			fashionAccountBackgroundDict_.MergeFrom(other.fashionAccountBackgroundDict_);
			mapEventInfos_.Add(other.mapEventInfos_);
			mapEventInfoDict_.MergeFrom(other.mapEventInfoDict_);
			skinStandingPaintings_.Add(other.skinStandingPaintings_);
			skinStandingPaintingDict_.MergeFrom(other.skinStandingPaintingDict_);
			banners_.Add(other.banners_);
			bannerDict_.MergeFrom(other.bannerDict_);
			specifiedCommercialTransactionss_.Add(other.specifiedCommercialTransactionss_);
			specifiedCommercialTransactionsDict_.MergeFrom(other.specifiedCommercialTransactionsDict_);
			paymentServicesActs_.Add(other.paymentServicesActs_);
			paymentServicesActDict_.MergeFrom(other.paymentServicesActDict_);
			rechargeLimits_.Add(other.rechargeLimits_);
			rechargeLimitDict_.MergeFrom(other.rechargeLimitDict_);
			exchangeStoreGoodss_.Add(other.exchangeStoreGoodss_);
			exchangeStoreGoodsDict_.MergeFrom(other.exchangeStoreGoodsDict_);
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
			case 10u:
				itemInfos_.AddEntriesFrom(ref input, _repeated_itemInfos_codec);
				break;
			case 18u:
				itemInfoDict_.AddEntriesFrom(ref input, _map_itemInfoDict_codec);
				break;
			case 26u:
				fashionAccountBackgrounds_.AddEntriesFrom(ref input, _repeated_fashionAccountBackgrounds_codec);
				break;
			case 34u:
				fashionAccountBackgroundDict_.AddEntriesFrom(ref input, _map_fashionAccountBackgroundDict_codec);
				break;
			case 42u:
				mapEventInfos_.AddEntriesFrom(ref input, _repeated_mapEventInfos_codec);
				break;
			case 50u:
				mapEventInfoDict_.AddEntriesFrom(ref input, _map_mapEventInfoDict_codec);
				break;
			case 58u:
				skinStandingPaintings_.AddEntriesFrom(ref input, _repeated_skinStandingPaintings_codec);
				break;
			case 66u:
				skinStandingPaintingDict_.AddEntriesFrom(ref input, _map_skinStandingPaintingDict_codec);
				break;
			case 74u:
				banners_.AddEntriesFrom(ref input, _repeated_banners_codec);
				break;
			case 82u:
				bannerDict_.AddEntriesFrom(ref input, _map_bannerDict_codec);
				break;
			case 90u:
				specifiedCommercialTransactionss_.AddEntriesFrom(ref input, _repeated_specifiedCommercialTransactionss_codec);
				break;
			case 98u:
				specifiedCommercialTransactionsDict_.AddEntriesFrom(ref input, _map_specifiedCommercialTransactionsDict_codec);
				break;
			case 106u:
				paymentServicesActs_.AddEntriesFrom(ref input, _repeated_paymentServicesActs_codec);
				break;
			case 114u:
				paymentServicesActDict_.AddEntriesFrom(ref input, _map_paymentServicesActDict_codec);
				break;
			case 122u:
				rechargeLimits_.AddEntriesFrom(ref input, _repeated_rechargeLimits_codec);
				break;
			case 130u:
				rechargeLimitDict_.AddEntriesFrom(ref input, _map_rechargeLimitDict_codec);
				break;
			case 138u:
				exchangeStoreGoodss_.AddEntriesFrom(ref input, _repeated_exchangeStoreGoodss_codec);
				break;
			case 146u:
				exchangeStoreGoodsDict_.AddEntriesFrom(ref input, _map_exchangeStoreGoodsDict_codec);
				break;
			}
		}
	}
}
