using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class FashionCardBackConfigure : IMessage<FashionCardBackConfigure>, IMessage, IEquatable<FashionCardBackConfigure>, IDeepCloneable<FashionCardBackConfigure>, IBufferMessage
{
	private static readonly MessageParser<FashionCardBackConfigure> _parser = new MessageParser<FashionCardBackConfigure>(() => new FashionCardBackConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int CardFrontFieldNumber = 2;

	private string cardFront_ = "";

	public const int CardFrontColorFieldNumber = 3;

	private string cardFrontColor_ = "";

	public const int CardBackFieldNumber = 4;

	private string cardBack_ = "";

	public const int CardBackMaterialFieldNumber = 5;

	private string cardBackMaterial_ = "";

	public const int CardPerviewFieldNumber = 6;

	private string cardPerview_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FashionCardBackConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FashionReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
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
	public string CardFrontColor
	{
		get
		{
			return cardFrontColor_;
		}
		private set
		{
			cardFrontColor_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CardBack
	{
		get
		{
			return cardBack_;
		}
		private set
		{
			cardBack_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CardBackMaterial
	{
		get
		{
			return cardBackMaterial_;
		}
		private set
		{
			cardBackMaterial_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CardPerview
	{
		get
		{
			return cardPerview_;
		}
		private set
		{
			cardPerview_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionCardBackConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionCardBackConfigure(FashionCardBackConfigure other)
		: this()
	{
		id_ = other.id_;
		cardFront_ = other.cardFront_;
		cardFrontColor_ = other.cardFrontColor_;
		cardBack_ = other.cardBack_;
		cardBackMaterial_ = other.cardBackMaterial_;
		cardPerview_ = other.cardPerview_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionCardBackConfigure Clone()
	{
		return new FashionCardBackConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FashionCardBackConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FashionCardBackConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (CardFront != other.CardFront)
		{
			return false;
		}
		if (CardFrontColor != other.CardFrontColor)
		{
			return false;
		}
		if (CardBack != other.CardBack)
		{
			return false;
		}
		if (CardBackMaterial != other.CardBackMaterial)
		{
			return false;
		}
		if (CardPerview != other.CardPerview)
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (CardFront.Length != 0)
		{
			num ^= CardFront.GetHashCode();
		}
		if (CardFrontColor.Length != 0)
		{
			num ^= CardFrontColor.GetHashCode();
		}
		if (CardBack.Length != 0)
		{
			num ^= CardBack.GetHashCode();
		}
		if (CardBackMaterial.Length != 0)
		{
			num ^= CardBackMaterial.GetHashCode();
		}
		if (CardPerview.Length != 0)
		{
			num ^= CardPerview.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (CardFront.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(CardFront);
		}
		if (CardFrontColor.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(CardFrontColor);
		}
		if (CardBack.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(CardBack);
		}
		if (CardBackMaterial.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(CardBackMaterial);
		}
		if (CardPerview.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(CardPerview);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (CardFront.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CardFront);
		}
		if (CardFrontColor.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CardFrontColor);
		}
		if (CardBack.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CardBack);
		}
		if (CardBackMaterial.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CardBackMaterial);
		}
		if (CardPerview.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CardPerview);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FashionCardBackConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.CardFront.Length != 0)
			{
				CardFront = other.CardFront;
			}
			if (other.CardFrontColor.Length != 0)
			{
				CardFrontColor = other.CardFrontColor;
			}
			if (other.CardBack.Length != 0)
			{
				CardBack = other.CardBack;
			}
			if (other.CardBackMaterial.Length != 0)
			{
				CardBackMaterial = other.CardBackMaterial;
			}
			if (other.CardPerview.Length != 0)
			{
				CardPerview = other.CardPerview;
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
				Id = input.ReadSFixed32();
				break;
			case 18u:
				CardFront = input.ReadString();
				break;
			case 26u:
				CardFrontColor = input.ReadString();
				break;
			case 34u:
				CardBack = input.ReadString();
				break;
			case 42u:
				CardBackMaterial = input.ReadString();
				break;
			case 50u:
				CardPerview = input.ReadString();
				break;
			}
		}
	}
}
