using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Core;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class MapEventMapEventCardConfigure : IMessage<MapEventMapEventCardConfigure>, IMessage, IEquatable<MapEventMapEventCardConfigure>, IDeepCloneable<MapEventMapEventCardConfigure>, IBufferMessage
{
	private static readonly MessageParser<MapEventMapEventCardConfigure> _parser = new MessageParser<MapEventMapEventCardConfigure>(() => new MapEventMapEventCardConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int CardTypeFieldNumber = 3;

	private CardType cardType_;

	public const int CardNumbFieldNumber = 4;

	private int cardNumb_;

	public const int DescIdFieldNumber = 5;

	private int descId_;

	public const int CommentIdFieldNumber = 6;

	private int commentId_;

	public const int ImageFieldNumber = 7;

	private string image_ = "";

	public const int ImageSfwFieldNumber = 8;

	private string imageSfw_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapEventMapEventCardConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MapEventReflection.Descriptor.MessageTypes[2];

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
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardType CardType
	{
		get
		{
			return cardType_;
		}
		private set
		{
			cardType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardNumb
	{
		get
		{
			return cardNumb_;
		}
		private set
		{
			cardNumb_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescId
	{
		get
		{
			return descId_;
		}
		private set
		{
			descId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CommentId
	{
		get
		{
			return commentId_;
		}
		private set
		{
			commentId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Image
	{
		get
		{
			return image_;
		}
		private set
		{
			image_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ImageSfw
	{
		get
		{
			return imageSfw_;
		}
		private set
		{
			imageSfw_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventMapEventCardConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventMapEventCardConfigure(MapEventMapEventCardConfigure other)
		: this()
	{
		id_ = other.id_;
		nameID_ = other.nameID_;
		cardType_ = other.cardType_;
		cardNumb_ = other.cardNumb_;
		descId_ = other.descId_;
		commentId_ = other.commentId_;
		image_ = other.image_;
		imageSfw_ = other.imageSfw_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventMapEventCardConfigure Clone()
	{
		return new MapEventMapEventCardConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapEventMapEventCardConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapEventMapEventCardConfigure other)
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
		if (NameID != other.NameID)
		{
			return false;
		}
		if (CardType != other.CardType)
		{
			return false;
		}
		if (CardNumb != other.CardNumb)
		{
			return false;
		}
		if (DescId != other.DescId)
		{
			return false;
		}
		if (CommentId != other.CommentId)
		{
			return false;
		}
		if (Image != other.Image)
		{
			return false;
		}
		if (ImageSfw != other.ImageSfw)
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
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (CardType != CardType.None)
		{
			num ^= CardType.GetHashCode();
		}
		if (CardNumb != 0)
		{
			num ^= CardNumb.GetHashCode();
		}
		if (DescId != 0)
		{
			num ^= DescId.GetHashCode();
		}
		if (CommentId != 0)
		{
			num ^= CommentId.GetHashCode();
		}
		if (Image.Length != 0)
		{
			num ^= Image.GetHashCode();
		}
		if (ImageSfw.Length != 0)
		{
			num ^= ImageSfw.GetHashCode();
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
		if (NameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NameID);
		}
		if (CardType != CardType.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)CardType);
		}
		if (CardNumb != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CardNumb);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DescId);
		}
		if (CommentId != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(CommentId);
		}
		if (Image.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(Image);
		}
		if (ImageSfw.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(ImageSfw);
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
		if (NameID != 0)
		{
			num += 5;
		}
		if (CardType != CardType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)CardType);
		}
		if (CardNumb != 0)
		{
			num += 5;
		}
		if (DescId != 0)
		{
			num += 5;
		}
		if (CommentId != 0)
		{
			num += 5;
		}
		if (Image.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Image);
		}
		if (ImageSfw.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ImageSfw);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MapEventMapEventCardConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.CardType != CardType.None)
			{
				CardType = other.CardType;
			}
			if (other.CardNumb != 0)
			{
				CardNumb = other.CardNumb;
			}
			if (other.DescId != 0)
			{
				DescId = other.DescId;
			}
			if (other.CommentId != 0)
			{
				CommentId = other.CommentId;
			}
			if (other.Image.Length != 0)
			{
				Image = other.Image;
			}
			if (other.ImageSfw.Length != 0)
			{
				ImageSfw = other.ImageSfw;
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
			case 21u:
				NameID = input.ReadSFixed32();
				break;
			case 24u:
				CardType = (CardType)input.ReadEnum();
				break;
			case 37u:
				CardNumb = input.ReadSFixed32();
				break;
			case 45u:
				DescId = input.ReadSFixed32();
				break;
			case 53u:
				CommentId = input.ReadSFixed32();
				break;
			case 58u:
				Image = input.ReadString();
				break;
			case 66u:
				ImageSfw = input.ReadString();
				break;
			}
		}
	}

	public void FixImage(string image)
	{
		image_ = image;
	}

	public string GetImage()
	{
		if (GameSettings.angelMode && !string.IsNullOrEmpty(imageSfw_))
		{
			return imageSfw_;
		}
		return image_;
	}
}
