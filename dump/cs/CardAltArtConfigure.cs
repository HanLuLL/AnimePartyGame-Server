using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class CardAltArtConfigure : IMessage<CardAltArtConfigure>, IMessage, IEquatable<CardAltArtConfigure>, IDeepCloneable<CardAltArtConfigure>, IBufferMessage
{
	private static readonly MessageParser<CardAltArtConfigure> _parser = new MessageParser<CardAltArtConfigure>(() => new CardAltArtConfigure());

	private UnknownFieldSet _unknownFields;

	public const int CardIdFieldNumber = 1;

	private int cardId_;

	public const int CardAltArtConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<CardAltArtConfigureItem> _repeated_cardAltArtConfigureItems_codec = FieldCodec.ForMessage(18u, CardAltArtConfigureItem.Parser);

	private readonly RepeatedField<CardAltArtConfigureItem> cardAltArtConfigureItems_ = new RepeatedField<CardAltArtConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CardAltArtConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CardReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardId
	{
		get
		{
			return cardId_;
		}
		private set
		{
			cardId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CardAltArtConfigureItem> CardAltArtConfigureItems => cardAltArtConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardAltArtConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardAltArtConfigure(CardAltArtConfigure other)
		: this()
	{
		cardId_ = other.cardId_;
		cardAltArtConfigureItems_ = other.cardAltArtConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardAltArtConfigure Clone()
	{
		return new CardAltArtConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CardAltArtConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CardAltArtConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (CardId != other.CardId)
		{
			return false;
		}
		if (!cardAltArtConfigureItems_.Equals(other.cardAltArtConfigureItems_))
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
		if (CardId != 0)
		{
			num ^= CardId.GetHashCode();
		}
		num ^= cardAltArtConfigureItems_.GetHashCode();
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
		if (CardId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(CardId);
		}
		cardAltArtConfigureItems_.WriteTo(ref output, _repeated_cardAltArtConfigureItems_codec);
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
		if (CardId != 0)
		{
			num += 5;
		}
		num += cardAltArtConfigureItems_.CalculateSize(_repeated_cardAltArtConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CardAltArtConfigure other)
	{
		if (other != null)
		{
			if (other.CardId != 0)
			{
				CardId = other.CardId;
			}
			cardAltArtConfigureItems_.Add(other.cardAltArtConfigureItems_);
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
				CardId = input.ReadSFixed32();
				break;
			case 18u:
				cardAltArtConfigureItems_.AddEntriesFrom(ref input, _repeated_cardAltArtConfigureItems_codec);
				break;
			}
		}
	}
}
