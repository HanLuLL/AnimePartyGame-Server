using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class PayResultS2C : IMessage<PayResultS2C>, IMessage, IEquatable<PayResultS2C>, IDeepCloneable<PayResultS2C>, IBufferMessage
{
	private static readonly MessageParser<PayResultS2C> _parser = new MessageParser<PayResultS2C>(() => new PayResultS2C());

	private UnknownFieldSet _unknownFields;

	public const int FirstBuyFieldNumber = 1;

	private static readonly MapField<int, RechargeBuyFirst>.Codec _map_firstBuy_codec = new MapField<int, RechargeBuyFirst>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RechargeBuyFirst.Parser), 10u);

	private readonly MapField<int, RechargeBuyFirst> firstBuy_ = new MapField<int, RechargeBuyFirst>();

	public const int MonthlyCardRemDaysFieldNumber = 2;

	private int monthlyCardRemDays_;

	public const int TypeFieldNumber = 3;

	private int type_;

	public const int GoodsIdFieldNumber = 4;

	private int goodsId_;

	public const int ItemIdFieldNumber = 5;

	private int itemId_;

	public const int AmountFieldNumber = 6;

	private int amount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PayResultS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[530];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RechargeBuyFirst> FirstBuy => firstBuy_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MonthlyCardRemDays
	{
		get
		{
			return monthlyCardRemDays_;
		}
		set
		{
			monthlyCardRemDays_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Type
	{
		get
		{
			return type_;
		}
		set
		{
			type_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoodsId
	{
		get
		{
			return goodsId_;
		}
		set
		{
			goodsId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemId
	{
		get
		{
			return itemId_;
		}
		set
		{
			itemId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Amount
	{
		get
		{
			return amount_;
		}
		set
		{
			amount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayResultS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayResultS2C(PayResultS2C other)
		: this()
	{
		firstBuy_ = other.firstBuy_.Clone();
		monthlyCardRemDays_ = other.monthlyCardRemDays_;
		type_ = other.type_;
		goodsId_ = other.goodsId_;
		itemId_ = other.itemId_;
		amount_ = other.amount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayResultS2C Clone()
	{
		return new PayResultS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PayResultS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PayResultS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!FirstBuy.Equals(other.FirstBuy))
		{
			return false;
		}
		if (MonthlyCardRemDays != other.MonthlyCardRemDays)
		{
			return false;
		}
		if (Type != other.Type)
		{
			return false;
		}
		if (GoodsId != other.GoodsId)
		{
			return false;
		}
		if (ItemId != other.ItemId)
		{
			return false;
		}
		if (Amount != other.Amount)
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
		num ^= FirstBuy.GetHashCode();
		if (MonthlyCardRemDays != 0)
		{
			num ^= MonthlyCardRemDays.GetHashCode();
		}
		if (Type != 0)
		{
			num ^= Type.GetHashCode();
		}
		if (GoodsId != 0)
		{
			num ^= GoodsId.GetHashCode();
		}
		if (ItemId != 0)
		{
			num ^= ItemId.GetHashCode();
		}
		if (Amount != 0)
		{
			num ^= Amount.GetHashCode();
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
		firstBuy_.WriteTo(ref output, _map_firstBuy_codec);
		if (MonthlyCardRemDays != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MonthlyCardRemDays);
		}
		if (Type != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Type);
		}
		if (GoodsId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(GoodsId);
		}
		if (ItemId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(ItemId);
		}
		if (Amount != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Amount);
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
		num += firstBuy_.CalculateSize(_map_firstBuy_codec);
		if (MonthlyCardRemDays != 0)
		{
			num += 5;
		}
		if (Type != 0)
		{
			num += 5;
		}
		if (GoodsId != 0)
		{
			num += 5;
		}
		if (ItemId != 0)
		{
			num += 5;
		}
		if (Amount != 0)
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
	public void MergeFrom(PayResultS2C other)
	{
		if (other != null)
		{
			firstBuy_.MergeFrom(other.firstBuy_);
			if (other.MonthlyCardRemDays != 0)
			{
				MonthlyCardRemDays = other.MonthlyCardRemDays;
			}
			if (other.Type != 0)
			{
				Type = other.Type;
			}
			if (other.GoodsId != 0)
			{
				GoodsId = other.GoodsId;
			}
			if (other.ItemId != 0)
			{
				ItemId = other.ItemId;
			}
			if (other.Amount != 0)
			{
				Amount = other.Amount;
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
				firstBuy_.AddEntriesFrom(ref input, _map_firstBuy_codec);
				break;
			case 21u:
				MonthlyCardRemDays = input.ReadSFixed32();
				break;
			case 29u:
				Type = input.ReadSFixed32();
				break;
			case 37u:
				GoodsId = input.ReadSFixed32();
				break;
			case 45u:
				ItemId = input.ReadSFixed32();
				break;
			case 53u:
				Amount = input.ReadSFixed32();
				break;
			}
		}
	}
}
