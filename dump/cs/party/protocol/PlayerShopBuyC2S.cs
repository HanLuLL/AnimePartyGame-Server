using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class PlayerShopBuyC2S : IMessage<PlayerShopBuyC2S>, IMessage, IEquatable<PlayerShopBuyC2S>, IDeepCloneable<PlayerShopBuyC2S>, IBufferMessage
{
	private static readonly MessageParser<PlayerShopBuyC2S> _parser = new MessageParser<PlayerShopBuyC2S>(() => new PlayerShopBuyC2S());

	private UnknownFieldSet _unknownFields;

	public const int TypeFieldNumber = 1;

	private int type_;

	public const int GoodsIdFieldNumber = 2;

	private int goodsId_;

	public const int BuyCountFieldNumber = 3;

	private int buyCount_;

	public const int ChainIdFieldNumber = 4;

	private int chainId_;

	public const int DiscountCardIdFieldNumber = 5;

	private int discountCardId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PlayerShopBuyC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[182];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public int BuyCount
	{
		get
		{
			return buyCount_;
		}
		set
		{
			buyCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ChainId
	{
		get
		{
			return chainId_;
		}
		set
		{
			chainId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiscountCardId
	{
		get
		{
			return discountCardId_;
		}
		set
		{
			discountCardId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerShopBuyC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerShopBuyC2S(PlayerShopBuyC2S other)
		: this()
	{
		type_ = other.type_;
		goodsId_ = other.goodsId_;
		buyCount_ = other.buyCount_;
		chainId_ = other.chainId_;
		discountCardId_ = other.discountCardId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerShopBuyC2S Clone()
	{
		return new PlayerShopBuyC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PlayerShopBuyC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PlayerShopBuyC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Type != other.Type)
		{
			return false;
		}
		if (GoodsId != other.GoodsId)
		{
			return false;
		}
		if (BuyCount != other.BuyCount)
		{
			return false;
		}
		if (ChainId != other.ChainId)
		{
			return false;
		}
		if (DiscountCardId != other.DiscountCardId)
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
		if (Type != 0)
		{
			num ^= Type.GetHashCode();
		}
		if (GoodsId != 0)
		{
			num ^= GoodsId.GetHashCode();
		}
		if (BuyCount != 0)
		{
			num ^= BuyCount.GetHashCode();
		}
		if (ChainId != 0)
		{
			num ^= ChainId.GetHashCode();
		}
		if (DiscountCardId != 0)
		{
			num ^= DiscountCardId.GetHashCode();
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
		if (Type != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Type);
		}
		if (GoodsId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(GoodsId);
		}
		if (BuyCount != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(BuyCount);
		}
		if (ChainId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ChainId);
		}
		if (DiscountCardId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DiscountCardId);
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
		if (Type != 0)
		{
			num += 5;
		}
		if (GoodsId != 0)
		{
			num += 5;
		}
		if (BuyCount != 0)
		{
			num += 5;
		}
		if (ChainId != 0)
		{
			num += 5;
		}
		if (DiscountCardId != 0)
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
	public void MergeFrom(PlayerShopBuyC2S other)
	{
		if (other != null)
		{
			if (other.Type != 0)
			{
				Type = other.Type;
			}
			if (other.GoodsId != 0)
			{
				GoodsId = other.GoodsId;
			}
			if (other.BuyCount != 0)
			{
				BuyCount = other.BuyCount;
			}
			if (other.ChainId != 0)
			{
				ChainId = other.ChainId;
			}
			if (other.DiscountCardId != 0)
			{
				DiscountCardId = other.DiscountCardId;
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
				Type = input.ReadSFixed32();
				break;
			case 21u:
				GoodsId = input.ReadSFixed32();
				break;
			case 29u:
				BuyCount = input.ReadSFixed32();
				break;
			case 37u:
				ChainId = input.ReadSFixed32();
				break;
			case 45u:
				DiscountCardId = input.ReadSFixed32();
				break;
			}
		}
	}
}
