using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class ShopBuyC2S : IMessage<ShopBuyC2S>, IMessage, IEquatable<ShopBuyC2S>, IDeepCloneable<ShopBuyC2S>, IBufferMessage
{
	private static readonly MessageParser<ShopBuyC2S> _parser = new MessageParser<ShopBuyC2S>(() => new ShopBuyC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int CardsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_cards_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> cards_ = new RepeatedField<int>();

	public const int FreeCardFieldNumber = 3;

	private int freeCard_;

	public const int GoldFieldNumber = 4;

	private int gold_;

	public const int BuyCardsFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_buyCards_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> buyCards_ = new RepeatedField<int>();

	public const int FreeCardNumFieldNumber = 6;

	private int freeCardNum_;

	public const int IsRemoteFieldNumber = 7;

	private bool isRemote_;

	public const int AlreadysFieldNumber = 8;

	private static readonly FieldCodec<bool> _repeated_alreadys_codec = FieldCodec.ForBool(66u);

	private readonly RepeatedField<bool> alreadys_ = new RepeatedField<bool>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ShopBuyC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[253];

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
	public int FreeCard
	{
		get
		{
			return freeCard_;
		}
		set
		{
			freeCard_ = value;
		}
	}

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
	public RepeatedField<int> BuyCards => buyCards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FreeCardNum
	{
		get
		{
			return freeCardNum_;
		}
		set
		{
			freeCardNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsRemote
	{
		get
		{
			return isRemote_;
		}
		set
		{
			isRemote_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<bool> Alreadys => alreadys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopBuyC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopBuyC2S(ShopBuyC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		cards_ = other.cards_.Clone();
		freeCard_ = other.freeCard_;
		gold_ = other.gold_;
		buyCards_ = other.buyCards_.Clone();
		freeCardNum_ = other.freeCardNum_;
		isRemote_ = other.isRemote_;
		alreadys_ = other.alreadys_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopBuyC2S Clone()
	{
		return new ShopBuyC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ShopBuyC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ShopBuyC2S other)
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
		if (FreeCard != other.FreeCard)
		{
			return false;
		}
		if (Gold != other.Gold)
		{
			return false;
		}
		if (!buyCards_.Equals(other.buyCards_))
		{
			return false;
		}
		if (FreeCardNum != other.FreeCardNum)
		{
			return false;
		}
		if (IsRemote != other.IsRemote)
		{
			return false;
		}
		if (!alreadys_.Equals(other.alreadys_))
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
		if (FreeCard != 0)
		{
			num ^= FreeCard.GetHashCode();
		}
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
		}
		num ^= buyCards_.GetHashCode();
		if (FreeCardNum != 0)
		{
			num ^= FreeCardNum.GetHashCode();
		}
		if (IsRemote)
		{
			num ^= IsRemote.GetHashCode();
		}
		num ^= alreadys_.GetHashCode();
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
		if (FreeCard != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(FreeCard);
		}
		if (Gold != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Gold);
		}
		buyCards_.WriteTo(ref output, _repeated_buyCards_codec);
		if (FreeCardNum != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(FreeCardNum);
		}
		if (IsRemote)
		{
			output.WriteRawTag(56);
			output.WriteBool(IsRemote);
		}
		alreadys_.WriteTo(ref output, _repeated_alreadys_codec);
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
		if (FreeCard != 0)
		{
			num += 5;
		}
		if (Gold != 0)
		{
			num += 5;
		}
		num += buyCards_.CalculateSize(_repeated_buyCards_codec);
		if (FreeCardNum != 0)
		{
			num += 5;
		}
		if (IsRemote)
		{
			num += 2;
		}
		num += alreadys_.CalculateSize(_repeated_alreadys_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ShopBuyC2S other)
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
		if (other.FreeCard != 0)
		{
			FreeCard = other.FreeCard;
		}
		if (other.Gold != 0)
		{
			Gold = other.Gold;
		}
		buyCards_.Add(other.buyCards_);
		if (other.FreeCardNum != 0)
		{
			FreeCardNum = other.FreeCardNum;
		}
		if (other.IsRemote)
		{
			IsRemote = other.IsRemote;
		}
		alreadys_.Add(other.alreadys_);
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
				FreeCard = input.ReadSFixed32();
				break;
			case 37u:
				Gold = input.ReadSFixed32();
				break;
			case 42u:
			case 45u:
				buyCards_.AddEntriesFrom(ref input, _repeated_buyCards_codec);
				break;
			case 53u:
				FreeCardNum = input.ReadSFixed32();
				break;
			case 56u:
				IsRemote = input.ReadBool();
				break;
			case 64u:
			case 66u:
				alreadys_.AddEntriesFrom(ref input, _repeated_alreadys_codec);
				break;
			}
		}
	}
}
