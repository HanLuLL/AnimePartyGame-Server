using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ExchangeStoreConfigure : IMessage<ExchangeStoreConfigure>, IMessage, IEquatable<ExchangeStoreConfigure>, IDeepCloneable<ExchangeStoreConfigure>, IBufferMessage
{
	private static readonly MessageParser<ExchangeStoreConfigure> _parser = new MessageParser<ExchangeStoreConfigure>(() => new ExchangeStoreConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ShelfsFieldNumber = 1;

	private static readonly FieldCodec<ExchangeStoreShelfConfigure> _repeated_shelfs_codec = FieldCodec.ForMessage(10u, ExchangeStoreShelfConfigure.Parser);

	private readonly RepeatedField<ExchangeStoreShelfConfigure> shelfs_ = new RepeatedField<ExchangeStoreShelfConfigure>();

	public const int ShelfDictFieldNumber = 2;

	private static readonly MapField<int, ExchangeStoreShelfConfigure>.Codec _map_shelfDict_codec = new MapField<int, ExchangeStoreShelfConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ExchangeStoreShelfConfigure.Parser), 18u);

	private readonly MapField<int, ExchangeStoreShelfConfigure> shelfDict_ = new MapField<int, ExchangeStoreShelfConfigure>();

	public const int InfosFieldNumber = 3;

	private static readonly FieldCodec<ExchangeStoreInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(26u, ExchangeStoreInfoConfigure.Parser);

	private readonly RepeatedField<ExchangeStoreInfoConfigure> infos_ = new RepeatedField<ExchangeStoreInfoConfigure>();

	public const int InfoDictFieldNumber = 4;

	private static readonly MapField<int, ExchangeStoreInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, ExchangeStoreInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ExchangeStoreInfoConfigure.Parser), 34u);

	private readonly MapField<int, ExchangeStoreInfoConfigure> infoDict_ = new MapField<int, ExchangeStoreInfoConfigure>();

	public const int RefreshPoolsFieldNumber = 5;

	private static readonly FieldCodec<ExchangeStoreRefreshPoolConfigure> _repeated_refreshPools_codec = FieldCodec.ForMessage(42u, ExchangeStoreRefreshPoolConfigure.Parser);

	private readonly RepeatedField<ExchangeStoreRefreshPoolConfigure> refreshPools_ = new RepeatedField<ExchangeStoreRefreshPoolConfigure>();

	public const int RefreshPoolDictFieldNumber = 6;

	private static readonly MapField<int, ExchangeStoreRefreshPoolConfigure>.Codec _map_refreshPoolDict_codec = new MapField<int, ExchangeStoreRefreshPoolConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ExchangeStoreRefreshPoolConfigure.Parser), 50u);

	private readonly MapField<int, ExchangeStoreRefreshPoolConfigure> refreshPoolDict_ = new MapField<int, ExchangeStoreRefreshPoolConfigure>();

	public const int GoodssFieldNumber = 7;

	private static readonly FieldCodec<ExchangeStoreGoodsConfigure> _repeated_goodss_codec = FieldCodec.ForMessage(58u, ExchangeStoreGoodsConfigure.Parser);

	private readonly RepeatedField<ExchangeStoreGoodsConfigure> goodss_ = new RepeatedField<ExchangeStoreGoodsConfigure>();

	public const int GoodsDictFieldNumber = 8;

	private static readonly MapField<int, ExchangeStoreGoodsConfigure>.Codec _map_goodsDict_codec = new MapField<int, ExchangeStoreGoodsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ExchangeStoreGoodsConfigure.Parser), 66u);

	private readonly MapField<int, ExchangeStoreGoodsConfigure> goodsDict_ = new MapField<int, ExchangeStoreGoodsConfigure>();

	public const int GiftChainsFieldNumber = 9;

	private static readonly FieldCodec<ExchangeStoreGiftChainConfigure> _repeated_giftChains_codec = FieldCodec.ForMessage(74u, ExchangeStoreGiftChainConfigure.Parser);

	private readonly RepeatedField<ExchangeStoreGiftChainConfigure> giftChains_ = new RepeatedField<ExchangeStoreGiftChainConfigure>();

	public const int GiftChainDictFieldNumber = 10;

	private static readonly MapField<int, ExchangeStoreGiftChainConfigure>.Codec _map_giftChainDict_codec = new MapField<int, ExchangeStoreGiftChainConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ExchangeStoreGiftChainConfigure.Parser), 82u);

	private readonly MapField<int, ExchangeStoreGiftChainConfigure> giftChainDict_ = new MapField<int, ExchangeStoreGiftChainConfigure>();

	public const int SkinGroupsFieldNumber = 11;

	private static readonly FieldCodec<ExchangeStoreSkinGroupConfigure> _repeated_skinGroups_codec = FieldCodec.ForMessage(90u, ExchangeStoreSkinGroupConfigure.Parser);

	private readonly RepeatedField<ExchangeStoreSkinGroupConfigure> skinGroups_ = new RepeatedField<ExchangeStoreSkinGroupConfigure>();

	public const int SkinGroupDictFieldNumber = 12;

	private static readonly MapField<int, ExchangeStoreSkinGroupConfigure>.Codec _map_skinGroupDict_codec = new MapField<int, ExchangeStoreSkinGroupConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ExchangeStoreSkinGroupConfigure.Parser), 98u);

	private readonly MapField<int, ExchangeStoreSkinGroupConfigure> skinGroupDict_ = new MapField<int, ExchangeStoreSkinGroupConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExchangeStoreConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ExchangeStoreReflection.Descriptor.MessageTypes[9];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ExchangeStoreShelfConfigure> Shelfs => shelfs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ExchangeStoreShelfConfigure> ShelfDict => shelfDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ExchangeStoreInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ExchangeStoreInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ExchangeStoreRefreshPoolConfigure> RefreshPools => refreshPools_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ExchangeStoreRefreshPoolConfigure> RefreshPoolDict => refreshPoolDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ExchangeStoreGoodsConfigure> Goodss => goodss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ExchangeStoreGoodsConfigure> GoodsDict => goodsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ExchangeStoreGiftChainConfigure> GiftChains => giftChains_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ExchangeStoreGiftChainConfigure> GiftChainDict => giftChainDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ExchangeStoreSkinGroupConfigure> SkinGroups => skinGroups_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ExchangeStoreSkinGroupConfigure> SkinGroupDict => skinGroupDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreConfigure(ExchangeStoreConfigure other)
		: this()
	{
		shelfs_ = other.shelfs_.Clone();
		shelfDict_ = other.shelfDict_.Clone();
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		refreshPools_ = other.refreshPools_.Clone();
		refreshPoolDict_ = other.refreshPoolDict_.Clone();
		goodss_ = other.goodss_.Clone();
		goodsDict_ = other.goodsDict_.Clone();
		giftChains_ = other.giftChains_.Clone();
		giftChainDict_ = other.giftChainDict_.Clone();
		skinGroups_ = other.skinGroups_.Clone();
		skinGroupDict_ = other.skinGroupDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreConfigure Clone()
	{
		return new ExchangeStoreConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExchangeStoreConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExchangeStoreConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!shelfs_.Equals(other.shelfs_))
		{
			return false;
		}
		if (!ShelfDict.Equals(other.ShelfDict))
		{
			return false;
		}
		if (!infos_.Equals(other.infos_))
		{
			return false;
		}
		if (!InfoDict.Equals(other.InfoDict))
		{
			return false;
		}
		if (!refreshPools_.Equals(other.refreshPools_))
		{
			return false;
		}
		if (!RefreshPoolDict.Equals(other.RefreshPoolDict))
		{
			return false;
		}
		if (!goodss_.Equals(other.goodss_))
		{
			return false;
		}
		if (!GoodsDict.Equals(other.GoodsDict))
		{
			return false;
		}
		if (!giftChains_.Equals(other.giftChains_))
		{
			return false;
		}
		if (!GiftChainDict.Equals(other.GiftChainDict))
		{
			return false;
		}
		if (!skinGroups_.Equals(other.skinGroups_))
		{
			return false;
		}
		if (!SkinGroupDict.Equals(other.SkinGroupDict))
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
		num ^= shelfs_.GetHashCode();
		num ^= ShelfDict.GetHashCode();
		num ^= infos_.GetHashCode();
		num ^= InfoDict.GetHashCode();
		num ^= refreshPools_.GetHashCode();
		num ^= RefreshPoolDict.GetHashCode();
		num ^= goodss_.GetHashCode();
		num ^= GoodsDict.GetHashCode();
		num ^= giftChains_.GetHashCode();
		num ^= GiftChainDict.GetHashCode();
		num ^= skinGroups_.GetHashCode();
		num ^= SkinGroupDict.GetHashCode();
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
		shelfs_.WriteTo(ref output, _repeated_shelfs_codec);
		shelfDict_.WriteTo(ref output, _map_shelfDict_codec);
		infos_.WriteTo(ref output, _repeated_infos_codec);
		infoDict_.WriteTo(ref output, _map_infoDict_codec);
		refreshPools_.WriteTo(ref output, _repeated_refreshPools_codec);
		refreshPoolDict_.WriteTo(ref output, _map_refreshPoolDict_codec);
		goodss_.WriteTo(ref output, _repeated_goodss_codec);
		goodsDict_.WriteTo(ref output, _map_goodsDict_codec);
		giftChains_.WriteTo(ref output, _repeated_giftChains_codec);
		giftChainDict_.WriteTo(ref output, _map_giftChainDict_codec);
		skinGroups_.WriteTo(ref output, _repeated_skinGroups_codec);
		skinGroupDict_.WriteTo(ref output, _map_skinGroupDict_codec);
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
		num += shelfs_.CalculateSize(_repeated_shelfs_codec);
		num += shelfDict_.CalculateSize(_map_shelfDict_codec);
		num += infos_.CalculateSize(_repeated_infos_codec);
		num += infoDict_.CalculateSize(_map_infoDict_codec);
		num += refreshPools_.CalculateSize(_repeated_refreshPools_codec);
		num += refreshPoolDict_.CalculateSize(_map_refreshPoolDict_codec);
		num += goodss_.CalculateSize(_repeated_goodss_codec);
		num += goodsDict_.CalculateSize(_map_goodsDict_codec);
		num += giftChains_.CalculateSize(_repeated_giftChains_codec);
		num += giftChainDict_.CalculateSize(_map_giftChainDict_codec);
		num += skinGroups_.CalculateSize(_repeated_skinGroups_codec);
		num += skinGroupDict_.CalculateSize(_map_skinGroupDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ExchangeStoreConfigure other)
	{
		if (other != null)
		{
			shelfs_.Add(other.shelfs_);
			shelfDict_.MergeFrom(other.shelfDict_);
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			refreshPools_.Add(other.refreshPools_);
			refreshPoolDict_.MergeFrom(other.refreshPoolDict_);
			goodss_.Add(other.goodss_);
			goodsDict_.MergeFrom(other.goodsDict_);
			giftChains_.Add(other.giftChains_);
			giftChainDict_.MergeFrom(other.giftChainDict_);
			skinGroups_.Add(other.skinGroups_);
			skinGroupDict_.MergeFrom(other.skinGroupDict_);
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
				shelfs_.AddEntriesFrom(ref input, _repeated_shelfs_codec);
				break;
			case 18u:
				shelfDict_.AddEntriesFrom(ref input, _map_shelfDict_codec);
				break;
			case 26u:
				infos_.AddEntriesFrom(ref input, _repeated_infos_codec);
				break;
			case 34u:
				infoDict_.AddEntriesFrom(ref input, _map_infoDict_codec);
				break;
			case 42u:
				refreshPools_.AddEntriesFrom(ref input, _repeated_refreshPools_codec);
				break;
			case 50u:
				refreshPoolDict_.AddEntriesFrom(ref input, _map_refreshPoolDict_codec);
				break;
			case 58u:
				goodss_.AddEntriesFrom(ref input, _repeated_goodss_codec);
				break;
			case 66u:
				goodsDict_.AddEntriesFrom(ref input, _map_goodsDict_codec);
				break;
			case 74u:
				giftChains_.AddEntriesFrom(ref input, _repeated_giftChains_codec);
				break;
			case 82u:
				giftChainDict_.AddEntriesFrom(ref input, _map_giftChainDict_codec);
				break;
			case 90u:
				skinGroups_.AddEntriesFrom(ref input, _repeated_skinGroups_codec);
				break;
			case 98u:
				skinGroupDict_.AddEntriesFrom(ref input, _map_skinGroupDict_codec);
				break;
			}
		}
	}

	public void Fix()
	{
	}
}
