using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Core;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class EventInfoConfigure : IMessage<EventInfoConfigure>, IMessage, IEquatable<EventInfoConfigure>, IDeepCloneable<EventInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<EventInfoConfigure> _parser = new MessageParser<EventInfoConfigure>(() => new EventInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int FunctionNameFieldNumber = 2;

	private string functionName_ = "";

	public const int CardTypeFieldNumber = 3;

	private CardType cardType_;

	public const int IsGalleryShowFieldNumber = 4;

	private bool isGalleryShow_;

	public const int NameIDFieldNumber = 5;

	private int nameID_;

	public const int CardNumbFieldNumber = 6;

	private int cardNumb_;

	public const int DescIdFieldNumber = 7;

	private int descId_;

	public const int CommentIdFieldNumber = 8;

	private int commentId_;

	public const int ImageFieldNumber = 9;

	private string image_ = "";

	public const int ImageSfwFieldNumber = 10;

	private string imageSfw_ = "";

	public const int ParamsFieldNumber = 11;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSInt32(90u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	public const int BuffIdsFieldNumber = 12;

	private static readonly FieldCodec<int> _repeated_buffIds_codec = FieldCodec.ForSFixed32(98u);

	private readonly RepeatedField<int> buffIds_ = new RepeatedField<int>();

	public const int Perform1FieldNumber = 13;

	private int perform1_;

	public const int Perform2FieldNumber = 14;

	private int perform2_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<EventInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => EventReflection.Descriptor.MessageTypes[0];

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
	public string FunctionName
	{
		get
		{
			return functionName_;
		}
		private set
		{
			functionName_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public bool IsGalleryShow
	{
		get
		{
			return isGalleryShow_;
		}
		private set
		{
			isGalleryShow_ = value;
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
	public RepeatedField<int> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BuffIds => buffIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Perform1
	{
		get
		{
			return perform1_;
		}
		private set
		{
			perform1_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Perform2
	{
		get
		{
			return perform2_;
		}
		private set
		{
			perform2_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventInfoConfigure(EventInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		functionName_ = other.functionName_;
		cardType_ = other.cardType_;
		isGalleryShow_ = other.isGalleryShow_;
		nameID_ = other.nameID_;
		cardNumb_ = other.cardNumb_;
		descId_ = other.descId_;
		commentId_ = other.commentId_;
		image_ = other.image_;
		imageSfw_ = other.imageSfw_;
		params_ = other.params_.Clone();
		buffIds_ = other.buffIds_.Clone();
		perform1_ = other.perform1_;
		perform2_ = other.perform2_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventInfoConfigure Clone()
	{
		return new EventInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as EventInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(EventInfoConfigure other)
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
		if (FunctionName != other.FunctionName)
		{
			return false;
		}
		if (CardType != other.CardType)
		{
			return false;
		}
		if (IsGalleryShow != other.IsGalleryShow)
		{
			return false;
		}
		if (NameID != other.NameID)
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
		if (!params_.Equals(other.params_))
		{
			return false;
		}
		if (!buffIds_.Equals(other.buffIds_))
		{
			return false;
		}
		if (Perform1 != other.Perform1)
		{
			return false;
		}
		if (Perform2 != other.Perform2)
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
		if (FunctionName.Length != 0)
		{
			num ^= FunctionName.GetHashCode();
		}
		if (CardType != CardType.None)
		{
			num ^= CardType.GetHashCode();
		}
		if (IsGalleryShow)
		{
			num ^= IsGalleryShow.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
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
		num ^= params_.GetHashCode();
		num ^= buffIds_.GetHashCode();
		if (Perform1 != 0)
		{
			num ^= Perform1.GetHashCode();
		}
		if (Perform2 != 0)
		{
			num ^= Perform2.GetHashCode();
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
		if (FunctionName.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(FunctionName);
		}
		if (CardType != CardType.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)CardType);
		}
		if (IsGalleryShow)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsGalleryShow);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(NameID);
		}
		if (CardNumb != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(CardNumb);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(DescId);
		}
		if (CommentId != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(CommentId);
		}
		if (Image.Length != 0)
		{
			output.WriteRawTag(74);
			output.WriteString(Image);
		}
		if (ImageSfw.Length != 0)
		{
			output.WriteRawTag(82);
			output.WriteString(ImageSfw);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
		buffIds_.WriteTo(ref output, _repeated_buffIds_codec);
		if (Perform1 != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(Perform1);
		}
		if (Perform2 != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(Perform2);
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
		if (FunctionName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(FunctionName);
		}
		if (CardType != CardType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)CardType);
		}
		if (IsGalleryShow)
		{
			num += 2;
		}
		if (NameID != 0)
		{
			num += 5;
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
		num += params_.CalculateSize(_repeated_params_codec);
		num += buffIds_.CalculateSize(_repeated_buffIds_codec);
		if (Perform1 != 0)
		{
			num += 5;
		}
		if (Perform2 != 0)
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
	public void MergeFrom(EventInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.FunctionName.Length != 0)
			{
				FunctionName = other.FunctionName;
			}
			if (other.CardType != CardType.None)
			{
				CardType = other.CardType;
			}
			if (other.IsGalleryShow)
			{
				IsGalleryShow = other.IsGalleryShow;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
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
			params_.Add(other.params_);
			buffIds_.Add(other.buffIds_);
			if (other.Perform1 != 0)
			{
				Perform1 = other.Perform1;
			}
			if (other.Perform2 != 0)
			{
				Perform2 = other.Perform2;
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
				FunctionName = input.ReadString();
				break;
			case 24u:
				CardType = (CardType)input.ReadEnum();
				break;
			case 32u:
				IsGalleryShow = input.ReadBool();
				break;
			case 45u:
				NameID = input.ReadSFixed32();
				break;
			case 53u:
				CardNumb = input.ReadSFixed32();
				break;
			case 61u:
				DescId = input.ReadSFixed32();
				break;
			case 69u:
				CommentId = input.ReadSFixed32();
				break;
			case 74u:
				Image = input.ReadString();
				break;
			case 82u:
				ImageSfw = input.ReadString();
				break;
			case 88u:
			case 90u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 98u:
			case 101u:
				buffIds_.AddEntriesFrom(ref input, _repeated_buffIds_codec);
				break;
			case 109u:
				Perform1 = input.ReadSFixed32();
				break;
			case 117u:
				Perform2 = input.ReadSFixed32();
				break;
			}
		}
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
