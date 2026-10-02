using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class CardAltArtConfigureItem : IMessage<CardAltArtConfigureItem>, IMessage, IEquatable<CardAltArtConfigureItem>, IDeepCloneable<CardAltArtConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<CardAltArtConfigureItem> _parser = new MessageParser<CardAltArtConfigureItem>(() => new CardAltArtConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int TypeFieldNumber = 1;

	private int type_;

	public const int AltArtIdFieldNumber = 2;

	private int altArtId_;

	public const int AccountBackgroundFieldNumber = 3;

	private string accountBackground_ = "";

	public const int AccountBackgroundSfwFieldNumber = 4;

	private string accountBackgroundSfw_ = "";

	public const int AccountBackgroundVideoFieldNumber = 5;

	private string accountBackgroundVideo_ = "";

	public const int AccountBackgroundVideoSfwFieldNumber = 6;

	private string accountBackgroundVideoSfw_ = "";

	public const int CardFrontFieldNumber = 7;

	private string cardFront_ = "";

	public const int CardFrontMaterialFieldNumber = 8;

	private string cardFrontMaterial_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CardAltArtConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CardReflection.Descriptor.MessageTypes[6];

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
		private set
		{
			type_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AltArtId
	{
		get
		{
			return altArtId_;
		}
		private set
		{
			altArtId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AccountBackground
	{
		get
		{
			return accountBackground_;
		}
		private set
		{
			accountBackground_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AccountBackgroundSfw
	{
		get
		{
			return accountBackgroundSfw_;
		}
		private set
		{
			accountBackgroundSfw_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AccountBackgroundVideo
	{
		get
		{
			return accountBackgroundVideo_;
		}
		private set
		{
			accountBackgroundVideo_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AccountBackgroundVideoSfw
	{
		get
		{
			return accountBackgroundVideoSfw_;
		}
		private set
		{
			accountBackgroundVideoSfw_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CardFront
	{
		get
		{
			return cardFront_;
		}
		private set
		{
			cardFront_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CardFrontMaterial
	{
		get
		{
			return cardFrontMaterial_;
		}
		private set
		{
			cardFrontMaterial_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardAltArtConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardAltArtConfigureItem(CardAltArtConfigureItem other)
		: this()
	{
		type_ = other.type_;
		altArtId_ = other.altArtId_;
		accountBackground_ = other.accountBackground_;
		accountBackgroundSfw_ = other.accountBackgroundSfw_;
		accountBackgroundVideo_ = other.accountBackgroundVideo_;
		accountBackgroundVideoSfw_ = other.accountBackgroundVideoSfw_;
		cardFront_ = other.cardFront_;
		cardFrontMaterial_ = other.cardFrontMaterial_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardAltArtConfigureItem Clone()
	{
		return new CardAltArtConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CardAltArtConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CardAltArtConfigureItem other)
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
		if (AltArtId != other.AltArtId)
		{
			return false;
		}
		if (AccountBackground != other.AccountBackground)
		{
			return false;
		}
		if (AccountBackgroundSfw != other.AccountBackgroundSfw)
		{
			return false;
		}
		if (AccountBackgroundVideo != other.AccountBackgroundVideo)
		{
			return false;
		}
		if (AccountBackgroundVideoSfw != other.AccountBackgroundVideoSfw)
		{
			return false;
		}
		if (CardFront != other.CardFront)
		{
			return false;
		}
		if (CardFrontMaterial != other.CardFrontMaterial)
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
		if (AltArtId != 0)
		{
			num ^= AltArtId.GetHashCode();
		}
		if (AccountBackground.Length != 0)
		{
			num ^= AccountBackground.GetHashCode();
		}
		if (AccountBackgroundSfw.Length != 0)
		{
			num ^= AccountBackgroundSfw.GetHashCode();
		}
		if (AccountBackgroundVideo.Length != 0)
		{
			num ^= AccountBackgroundVideo.GetHashCode();
		}
		if (AccountBackgroundVideoSfw.Length != 0)
		{
			num ^= AccountBackgroundVideoSfw.GetHashCode();
		}
		if (CardFront.Length != 0)
		{
			num ^= CardFront.GetHashCode();
		}
		if (CardFrontMaterial.Length != 0)
		{
			num ^= CardFrontMaterial.GetHashCode();
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
		if (AltArtId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(AltArtId);
		}
		if (AccountBackground.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(AccountBackground);
		}
		if (AccountBackgroundSfw.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(AccountBackgroundSfw);
		}
		if (AccountBackgroundVideo.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(AccountBackgroundVideo);
		}
		if (AccountBackgroundVideoSfw.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(AccountBackgroundVideoSfw);
		}
		if (CardFront.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(CardFront);
		}
		if (CardFrontMaterial.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(CardFrontMaterial);
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
		if (AltArtId != 0)
		{
			num += 5;
		}
		if (AccountBackground.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccountBackground);
		}
		if (AccountBackgroundSfw.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccountBackgroundSfw);
		}
		if (AccountBackgroundVideo.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccountBackgroundVideo);
		}
		if (AccountBackgroundVideoSfw.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccountBackgroundVideoSfw);
		}
		if (CardFront.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CardFront);
		}
		if (CardFrontMaterial.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CardFrontMaterial);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CardAltArtConfigureItem other)
	{
		if (other != null)
		{
			if (other.Type != 0)
			{
				Type = other.Type;
			}
			if (other.AltArtId != 0)
			{
				AltArtId = other.AltArtId;
			}
			if (other.AccountBackground.Length != 0)
			{
				AccountBackground = other.AccountBackground;
			}
			if (other.AccountBackgroundSfw.Length != 0)
			{
				AccountBackgroundSfw = other.AccountBackgroundSfw;
			}
			if (other.AccountBackgroundVideo.Length != 0)
			{
				AccountBackgroundVideo = other.AccountBackgroundVideo;
			}
			if (other.AccountBackgroundVideoSfw.Length != 0)
			{
				AccountBackgroundVideoSfw = other.AccountBackgroundVideoSfw;
			}
			if (other.CardFront.Length != 0)
			{
				CardFront = other.CardFront;
			}
			if (other.CardFrontMaterial.Length != 0)
			{
				CardFrontMaterial = other.CardFrontMaterial;
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
				AltArtId = input.ReadSFixed32();
				break;
			case 26u:
				AccountBackground = input.ReadString();
				break;
			case 34u:
				AccountBackgroundSfw = input.ReadString();
				break;
			case 42u:
				AccountBackgroundVideo = input.ReadString();
				break;
			case 50u:
				AccountBackgroundVideoSfw = input.ReadString();
				break;
			case 58u:
				CardFront = input.ReadString();
				break;
			case 66u:
				CardFrontMaterial = input.ReadString();
				break;
			}
		}
	}
}
