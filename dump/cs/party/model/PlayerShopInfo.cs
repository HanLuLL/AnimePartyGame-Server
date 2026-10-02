using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class PlayerShopInfo : IMessage<PlayerShopInfo>, IMessage, IEquatable<PlayerShopInfo>, IDeepCloneable<PlayerShopInfo>, IBufferMessage
{
	private static readonly MessageParser<PlayerShopInfo> _parser = new MessageParser<PlayerShopInfo>(() => new PlayerShopInfo());

	private UnknownFieldSet _unknownFields;

	public const int ShopRandomItemFieldNumber = 1;

	private static readonly MapField<int, ShopRandomItem>.Codec _map_shopRandomItem_codec = new MapField<int, ShopRandomItem>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.ShopRandomItem.Parser), 10u);

	private readonly MapField<int, ShopRandomItem> shopRandomItem_ = new MapField<int, ShopRandomItem>();

	public const int RecordFieldNumber = 2;

	private static readonly MapField<int, ShopBuyRecord>.Codec _map_record_codec = new MapField<int, ShopBuyRecord>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ShopBuyRecord.Parser), 18u);

	private readonly MapField<int, ShopBuyRecord> record_ = new MapField<int, ShopBuyRecord>();

	public const int RechargeRecordFieldNumber = 3;

	private static readonly MapField<int, ShopBuyRecord>.Codec _map_rechargeRecord_codec = new MapField<int, ShopBuyRecord>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ShopBuyRecord.Parser), 26u);

	private readonly MapField<int, ShopBuyRecord> rechargeRecord_ = new MapField<int, ShopBuyRecord>();

	public const int FirstBuyFieldNumber = 4;

	private static readonly MapField<int, RechargeBuyFirst>.Codec _map_firstBuy_codec = new MapField<int, RechargeBuyFirst>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RechargeBuyFirst.Parser), 34u);

	private readonly MapField<int, RechargeBuyFirst> firstBuy_ = new MapField<int, RechargeBuyFirst>();

	public const int LastResetTimeFieldNumber = 5;

	private long lastResetTime_;

	public const int LastTransferTimeFieldNumber = 6;

	private long lastTransferTime_;

	public const int ShopRookieGiftFieldNumber = 7;

	private static readonly MapField<int, int>.Codec _map_shopRookieGift_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 58u);

	private readonly MapField<int, int> shopRookieGift_ = new MapField<int, int>();

	public const int RechargeGoodsValueFieldNumber = 8;

	private long rechargeGoodsValue_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PlayerShopInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[49];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ShopRandomItem> ShopRandomItem => shopRandomItem_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ShopBuyRecord> Record => record_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ShopBuyRecord> RechargeRecord => rechargeRecord_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RechargeBuyFirst> FirstBuy => firstBuy_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastResetTime
	{
		get
		{
			return lastResetTime_;
		}
		set
		{
			lastResetTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastTransferTime
	{
		get
		{
			return lastTransferTime_;
		}
		set
		{
			lastTransferTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> ShopRookieGift => shopRookieGift_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long RechargeGoodsValue
	{
		get
		{
			return rechargeGoodsValue_;
		}
		set
		{
			rechargeGoodsValue_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerShopInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerShopInfo(PlayerShopInfo other)
		: this()
	{
		shopRandomItem_ = other.shopRandomItem_.Clone();
		record_ = other.record_.Clone();
		rechargeRecord_ = other.rechargeRecord_.Clone();
		firstBuy_ = other.firstBuy_.Clone();
		lastResetTime_ = other.lastResetTime_;
		lastTransferTime_ = other.lastTransferTime_;
		shopRookieGift_ = other.shopRookieGift_.Clone();
		rechargeGoodsValue_ = other.rechargeGoodsValue_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerShopInfo Clone()
	{
		return new PlayerShopInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PlayerShopInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PlayerShopInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!ShopRandomItem.Equals(other.ShopRandomItem))
		{
			return false;
		}
		if (!Record.Equals(other.Record))
		{
			return false;
		}
		if (!RechargeRecord.Equals(other.RechargeRecord))
		{
			return false;
		}
		if (!FirstBuy.Equals(other.FirstBuy))
		{
			return false;
		}
		if (LastResetTime != other.LastResetTime)
		{
			return false;
		}
		if (LastTransferTime != other.LastTransferTime)
		{
			return false;
		}
		if (!ShopRookieGift.Equals(other.ShopRookieGift))
		{
			return false;
		}
		if (RechargeGoodsValue != other.RechargeGoodsValue)
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
		num ^= ShopRandomItem.GetHashCode();
		num ^= Record.GetHashCode();
		num ^= RechargeRecord.GetHashCode();
		num ^= FirstBuy.GetHashCode();
		if (LastResetTime != 0L)
		{
			num ^= LastResetTime.GetHashCode();
		}
		if (LastTransferTime != 0L)
		{
			num ^= LastTransferTime.GetHashCode();
		}
		num ^= ShopRookieGift.GetHashCode();
		if (RechargeGoodsValue != 0L)
		{
			num ^= RechargeGoodsValue.GetHashCode();
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
		shopRandomItem_.WriteTo(ref output, _map_shopRandomItem_codec);
		record_.WriteTo(ref output, _map_record_codec);
		rechargeRecord_.WriteTo(ref output, _map_rechargeRecord_codec);
		firstBuy_.WriteTo(ref output, _map_firstBuy_codec);
		if (LastResetTime != 0L)
		{
			output.WriteRawTag(41);
			output.WriteSFixed64(LastResetTime);
		}
		if (LastTransferTime != 0L)
		{
			output.WriteRawTag(49);
			output.WriteSFixed64(LastTransferTime);
		}
		shopRookieGift_.WriteTo(ref output, _map_shopRookieGift_codec);
		if (RechargeGoodsValue != 0L)
		{
			output.WriteRawTag(65);
			output.WriteSFixed64(RechargeGoodsValue);
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
		num += shopRandomItem_.CalculateSize(_map_shopRandomItem_codec);
		num += record_.CalculateSize(_map_record_codec);
		num += rechargeRecord_.CalculateSize(_map_rechargeRecord_codec);
		num += firstBuy_.CalculateSize(_map_firstBuy_codec);
		if (LastResetTime != 0L)
		{
			num += 9;
		}
		if (LastTransferTime != 0L)
		{
			num += 9;
		}
		num += shopRookieGift_.CalculateSize(_map_shopRookieGift_codec);
		if (RechargeGoodsValue != 0L)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PlayerShopInfo other)
	{
		if (other != null)
		{
			shopRandomItem_.MergeFrom(other.shopRandomItem_);
			record_.MergeFrom(other.record_);
			rechargeRecord_.MergeFrom(other.rechargeRecord_);
			firstBuy_.MergeFrom(other.firstBuy_);
			if (other.LastResetTime != 0L)
			{
				LastResetTime = other.LastResetTime;
			}
			if (other.LastTransferTime != 0L)
			{
				LastTransferTime = other.LastTransferTime;
			}
			shopRookieGift_.MergeFrom(other.shopRookieGift_);
			if (other.RechargeGoodsValue != 0L)
			{
				RechargeGoodsValue = other.RechargeGoodsValue;
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
			case 10u:
				shopRandomItem_.AddEntriesFrom(ref input, _map_shopRandomItem_codec);
				break;
			case 18u:
				record_.AddEntriesFrom(ref input, _map_record_codec);
				break;
			case 26u:
				rechargeRecord_.AddEntriesFrom(ref input, _map_rechargeRecord_codec);
				break;
			case 34u:
				firstBuy_.AddEntriesFrom(ref input, _map_firstBuy_codec);
				break;
			case 41u:
				LastResetTime = input.ReadSFixed64();
				break;
			case 49u:
				LastTransferTime = input.ReadSFixed64();
				break;
			case 58u:
				shopRookieGift_.AddEntriesFrom(ref input, _map_shopRookieGift_codec);
				break;
			case 65u:
				RechargeGoodsValue = input.ReadSFixed64();
				break;
			}
		}
	}
}
