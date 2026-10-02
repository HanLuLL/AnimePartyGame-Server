using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RechargeStoreConfigure : IMessage<RechargeStoreConfigure>, IMessage, IEquatable<RechargeStoreConfigure>, IDeepCloneable<RechargeStoreConfigure>, IBufferMessage
{
	private static readonly MessageParser<RechargeStoreConfigure> _parser = new MessageParser<RechargeStoreConfigure>(() => new RechargeStoreConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ShelfsFieldNumber = 1;

	private static readonly FieldCodec<RechargeStoreShelfConfigure> _repeated_shelfs_codec = FieldCodec.ForMessage(10u, RechargeStoreShelfConfigure.Parser);

	private readonly RepeatedField<RechargeStoreShelfConfigure> shelfs_ = new RepeatedField<RechargeStoreShelfConfigure>();

	public const int ShelfDictFieldNumber = 2;

	private static readonly MapField<int, RechargeStoreShelfConfigure>.Codec _map_shelfDict_codec = new MapField<int, RechargeStoreShelfConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RechargeStoreShelfConfigure.Parser), 18u);

	private readonly MapField<int, RechargeStoreShelfConfigure> shelfDict_ = new MapField<int, RechargeStoreShelfConfigure>();

	public const int InfosFieldNumber = 3;

	private static readonly FieldCodec<RechargeStoreInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(26u, RechargeStoreInfoConfigure.Parser);

	private readonly RepeatedField<RechargeStoreInfoConfigure> infos_ = new RepeatedField<RechargeStoreInfoConfigure>();

	public const int InfoDictFieldNumber = 4;

	private static readonly MapField<int, RechargeStoreInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, RechargeStoreInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RechargeStoreInfoConfigure.Parser), 34u);

	private readonly MapField<int, RechargeStoreInfoConfigure> infoDict_ = new MapField<int, RechargeStoreInfoConfigure>();

	public const int GoodssFieldNumber = 5;

	private static readonly FieldCodec<RechargeStoreGoodsConfigure> _repeated_goodss_codec = FieldCodec.ForMessage(42u, RechargeStoreGoodsConfigure.Parser);

	private readonly RepeatedField<RechargeStoreGoodsConfigure> goodss_ = new RepeatedField<RechargeStoreGoodsConfigure>();

	public const int GoodsDictFieldNumber = 6;

	private static readonly MapField<int, RechargeStoreGoodsConfigure>.Codec _map_goodsDict_codec = new MapField<int, RechargeStoreGoodsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RechargeStoreGoodsConfigure.Parser), 50u);

	private readonly MapField<int, RechargeStoreGoodsConfigure> goodsDict_ = new MapField<int, RechargeStoreGoodsConfigure>();

	public const int TransferTypesFieldNumber = 7;

	private static readonly FieldCodec<RechargeStoreTransferTypeConfigure> _repeated_transferTypes_codec = FieldCodec.ForMessage(58u, RechargeStoreTransferTypeConfigure.Parser);

	private readonly RepeatedField<RechargeStoreTransferTypeConfigure> transferTypes_ = new RepeatedField<RechargeStoreTransferTypeConfigure>();

	public const int TransferTypeDictFieldNumber = 8;

	private static readonly MapField<int, RechargeStoreTransferTypeConfigure>.Codec _map_transferTypeDict_codec = new MapField<int, RechargeStoreTransferTypeConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RechargeStoreTransferTypeConfigure.Parser), 66u);

	private readonly MapField<int, RechargeStoreTransferTypeConfigure> transferTypeDict_ = new MapField<int, RechargeStoreTransferTypeConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeStoreConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeStoreReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RechargeStoreShelfConfigure> Shelfs => shelfs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RechargeStoreShelfConfigure> ShelfDict => shelfDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RechargeStoreInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RechargeStoreInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RechargeStoreGoodsConfigure> Goodss => goodss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RechargeStoreGoodsConfigure> GoodsDict => goodsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RechargeStoreTransferTypeConfigure> TransferTypes => transferTypes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RechargeStoreTransferTypeConfigure> TransferTypeDict => transferTypeDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreConfigure(RechargeStoreConfigure other)
		: this()
	{
		shelfs_ = other.shelfs_.Clone();
		shelfDict_ = other.shelfDict_.Clone();
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		goodss_ = other.goodss_.Clone();
		goodsDict_ = other.goodsDict_.Clone();
		transferTypes_ = other.transferTypes_.Clone();
		transferTypeDict_ = other.transferTypeDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreConfigure Clone()
	{
		return new RechargeStoreConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeStoreConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeStoreConfigure other)
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
		if (!goodss_.Equals(other.goodss_))
		{
			return false;
		}
		if (!GoodsDict.Equals(other.GoodsDict))
		{
			return false;
		}
		if (!transferTypes_.Equals(other.transferTypes_))
		{
			return false;
		}
		if (!TransferTypeDict.Equals(other.TransferTypeDict))
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
		num ^= goodss_.GetHashCode();
		num ^= GoodsDict.GetHashCode();
		num ^= transferTypes_.GetHashCode();
		num ^= TransferTypeDict.GetHashCode();
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
		goodss_.WriteTo(ref output, _repeated_goodss_codec);
		goodsDict_.WriteTo(ref output, _map_goodsDict_codec);
		transferTypes_.WriteTo(ref output, _repeated_transferTypes_codec);
		transferTypeDict_.WriteTo(ref output, _map_transferTypeDict_codec);
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
		num += goodss_.CalculateSize(_repeated_goodss_codec);
		num += goodsDict_.CalculateSize(_map_goodsDict_codec);
		num += transferTypes_.CalculateSize(_repeated_transferTypes_codec);
		num += transferTypeDict_.CalculateSize(_map_transferTypeDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RechargeStoreConfigure other)
	{
		if (other != null)
		{
			shelfs_.Add(other.shelfs_);
			shelfDict_.MergeFrom(other.shelfDict_);
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			goodss_.Add(other.goodss_);
			goodsDict_.MergeFrom(other.goodsDict_);
			transferTypes_.Add(other.transferTypes_);
			transferTypeDict_.MergeFrom(other.transferTypeDict_);
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
				goodss_.AddEntriesFrom(ref input, _repeated_goodss_codec);
				break;
			case 50u:
				goodsDict_.AddEntriesFrom(ref input, _map_goodsDict_codec);
				break;
			case 58u:
				transferTypes_.AddEntriesFrom(ref input, _repeated_transferTypes_codec);
				break;
			case 66u:
				transferTypeDict_.AddEntriesFrom(ref input, _map_transferTypeDict_codec);
				break;
			}
		}
	}
}
