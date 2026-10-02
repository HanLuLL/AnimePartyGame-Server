using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class RemoteShopC2S : IMessage<RemoteShopC2S>, IMessage, IEquatable<RemoteShopC2S>, IDeepCloneable<RemoteShopC2S>, IBufferMessage
{
	private static readonly MessageParser<RemoteShopC2S> _parser = new MessageParser<RemoteShopC2S>(() => new RemoteShopC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int CardsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_cards_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> cards_ = new RepeatedField<int>();

	public const int GoldFieldNumber = 3;

	private int gold_;

	public const int BuyIdxFieldNumber = 4;

	private int buyIdx_;

	public const int SaleCardIdFieldNumber = 5;

	private int saleCardId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RemoteShopC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[251];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionInfo Info
	{
		get
		{
			return info_;
		}
		set
		{
			info_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Cards => cards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Gold
	{
		get
		{
			return gold_;
		}
		set
		{
			gold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BuyIdx
	{
		get
		{
			return buyIdx_;
		}
		set
		{
			buyIdx_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SaleCardId
	{
		get
		{
			return saleCardId_;
		}
		set
		{
			saleCardId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RemoteShopC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RemoteShopC2S(RemoteShopC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		cards_ = other.cards_.Clone();
		gold_ = other.gold_;
		buyIdx_ = other.buyIdx_;
		saleCardId_ = other.saleCardId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RemoteShopC2S Clone()
	{
		return new RemoteShopC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RemoteShopC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RemoteShopC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Info, other.Info))
		{
			return false;
		}
		if (!cards_.Equals(other.cards_))
		{
			return false;
		}
		if (Gold != other.Gold)
		{
			return false;
		}
		if (BuyIdx != other.BuyIdx)
		{
			return false;
		}
		if (SaleCardId != other.SaleCardId)
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
		if (info_ != null)
		{
			num ^= Info.GetHashCode();
		}
		num ^= cards_.GetHashCode();
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
		}
		if (BuyIdx != 0)
		{
			num ^= BuyIdx.GetHashCode();
		}
		if (SaleCardId != 0)
		{
			num ^= SaleCardId.GetHashCode();
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
		if (info_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Info);
		}
		cards_.WriteTo(ref output, _repeated_cards_codec);
		if (Gold != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Gold);
		}
		if (BuyIdx != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(BuyIdx);
		}
		if (SaleCardId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(SaleCardId);
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
		if (info_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Info);
		}
		num += cards_.CalculateSize(_repeated_cards_codec);
		if (Gold != 0)
		{
			num += 5;
		}
		if (BuyIdx != 0)
		{
			num += 5;
		}
		if (SaleCardId != 0)
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
	public void MergeFrom(RemoteShopC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.info_ != null)
		{
			if (info_ == null)
			{
				Info = new ActionInfo();
			}
			Info.MergeFrom(other.Info);
		}
		cards_.Add(other.cards_);
		if (other.Gold != 0)
		{
			Gold = other.Gold;
		}
		if (other.BuyIdx != 0)
		{
			BuyIdx = other.BuyIdx;
		}
		if (other.SaleCardId != 0)
		{
			SaleCardId = other.SaleCardId;
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
			case 10u:
				if (info_ == null)
				{
					Info = new ActionInfo();
				}
				input.ReadMessage(Info);
				break;
			case 18u:
			case 21u:
				cards_.AddEntriesFrom(ref input, _repeated_cards_codec);
				break;
			case 29u:
				Gold = input.ReadSFixed32();
				break;
			case 37u:
				BuyIdx = input.ReadSFixed32();
				break;
			case 45u:
				SaleCardId = input.ReadSFixed32();
				break;
			}
		}
	}
}
