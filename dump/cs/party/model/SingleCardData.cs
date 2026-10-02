using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SingleCardData : IMessage<SingleCardData>, IMessage, IEquatable<SingleCardData>, IDeepCloneable<SingleCardData>, IBufferMessage
{
	private static readonly MessageParser<SingleCardData> _parser = new MessageParser<SingleCardData>(() => new SingleCardData());

	private UnknownFieldSet _unknownFields;

	public const int BagCardUIDsFieldNumber = 1;

	private static readonly FieldCodec<int> _repeated_bagCardUIDs_codec = FieldCodec.ForSFixed32(10u);

	private readonly RepeatedField<int> bagCardUIDs_ = new RepeatedField<int>();

	public const int ShopCardUIDsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_shopCardUIDs_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> shopCardUIDs_ = new RepeatedField<int>();

	public const int CardDepotFieldNumber = 3;

	private static readonly MapField<int, SingleCard>.Codec _map_cardDepot_codec = new MapField<int, SingleCard>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SingleCard.Parser), 26u);

	private readonly MapField<int, SingleCard> cardDepot_ = new MapField<int, SingleCard>();

	public const int ShopRefreshCountFieldNumber = 4;

	private int shopRefreshCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SingleCardData> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[106];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BagCardUIDs => bagCardUIDs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> ShopCardUIDs => shopCardUIDs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SingleCard> CardDepot => cardDepot_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ShopRefreshCount
	{
		get
		{
			return shopRefreshCount_;
		}
		set
		{
			shopRefreshCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleCardData()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleCardData(SingleCardData other)
		: this()
	{
		bagCardUIDs_ = other.bagCardUIDs_.Clone();
		shopCardUIDs_ = other.shopCardUIDs_.Clone();
		cardDepot_ = other.cardDepot_.Clone();
		shopRefreshCount_ = other.shopRefreshCount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleCardData Clone()
	{
		return new SingleCardData(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SingleCardData);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SingleCardData other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!bagCardUIDs_.Equals(other.bagCardUIDs_))
		{
			return false;
		}
		if (!shopCardUIDs_.Equals(other.shopCardUIDs_))
		{
			return false;
		}
		if (!CardDepot.Equals(other.CardDepot))
		{
			return false;
		}
		if (ShopRefreshCount != other.ShopRefreshCount)
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
		num ^= bagCardUIDs_.GetHashCode();
		num ^= shopCardUIDs_.GetHashCode();
		num ^= CardDepot.GetHashCode();
		if (ShopRefreshCount != 0)
		{
			num ^= ShopRefreshCount.GetHashCode();
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
		bagCardUIDs_.WriteTo(ref output, _repeated_bagCardUIDs_codec);
		shopCardUIDs_.WriteTo(ref output, _repeated_shopCardUIDs_codec);
		cardDepot_.WriteTo(ref output, _map_cardDepot_codec);
		if (ShopRefreshCount != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ShopRefreshCount);
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
		num += bagCardUIDs_.CalculateSize(_repeated_bagCardUIDs_codec);
		num += shopCardUIDs_.CalculateSize(_repeated_shopCardUIDs_codec);
		num += cardDepot_.CalculateSize(_map_cardDepot_codec);
		if (ShopRefreshCount != 0)
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
	public void MergeFrom(SingleCardData other)
	{
		if (other != null)
		{
			bagCardUIDs_.Add(other.bagCardUIDs_);
			shopCardUIDs_.Add(other.shopCardUIDs_);
			cardDepot_.MergeFrom(other.cardDepot_);
			if (other.ShopRefreshCount != 0)
			{
				ShopRefreshCount = other.ShopRefreshCount;
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
			case 13u:
				bagCardUIDs_.AddEntriesFrom(ref input, _repeated_bagCardUIDs_codec);
				break;
			case 18u:
			case 21u:
				shopCardUIDs_.AddEntriesFrom(ref input, _repeated_shopCardUIDs_codec);
				break;
			case 26u:
				cardDepot_.AddEntriesFrom(ref input, _map_cardDepot_codec);
				break;
			case 37u:
				ShopRefreshCount = input.ReadSFixed32();
				break;
			}
		}
	}
}
