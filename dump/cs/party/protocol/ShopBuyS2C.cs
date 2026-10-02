using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ShopBuyS2C : IMessage<ShopBuyS2C>, IMessage, IEquatable<ShopBuyS2C>, IDeepCloneable<ShopBuyS2C>, IBufferMessage
{
	private static readonly MessageParser<ShopBuyS2C> _parser = new MessageParser<ShopBuyS2C>(() => new ShopBuyS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int BuyCardsFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_buyCards_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> buyCards_ = new RepeatedField<int>();

	public const int CardsFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_cards_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> cards_ = new RepeatedField<int>();

	public const int AlreadysFieldNumber = 5;

	private static readonly FieldCodec<bool> _repeated_alreadys_codec = FieldCodec.ForBool(42u);

	private readonly RepeatedField<bool> alreadys_ = new RepeatedField<bool>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ShopBuyS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[254];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BuyCards => buyCards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Cards => cards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<bool> Alreadys => alreadys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopBuyS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopBuyS2C(ShopBuyS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		buyCards_ = other.buyCards_.Clone();
		cards_ = other.cards_.Clone();
		alreadys_ = other.alreadys_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopBuyS2C Clone()
	{
		return new ShopBuyS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ShopBuyS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ShopBuyS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (!buyCards_.Equals(other.buyCards_))
		{
			return false;
		}
		if (!cards_.Equals(other.cards_))
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		num ^= buyCards_.GetHashCode();
		num ^= cards_.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		buyCards_.WriteTo(ref output, _repeated_buyCards_codec);
		cards_.WriteTo(ref output, _repeated_cards_codec);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		num += buyCards_.CalculateSize(_repeated_buyCards_codec);
		num += cards_.CalculateSize(_repeated_cards_codec);
		num += alreadys_.CalculateSize(_repeated_alreadys_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ShopBuyS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			buyCards_.Add(other.buyCards_);
			cards_.Add(other.cards_);
			alreadys_.Add(other.alreadys_);
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 26u:
			case 29u:
				buyCards_.AddEntriesFrom(ref input, _repeated_buyCards_codec);
				break;
			case 34u:
			case 37u:
				cards_.AddEntriesFrom(ref input, _repeated_cards_codec);
				break;
			case 40u:
			case 42u:
				alreadys_.AddEntriesFrom(ref input, _repeated_alreadys_codec);
				break;
			}
		}
	}
}
