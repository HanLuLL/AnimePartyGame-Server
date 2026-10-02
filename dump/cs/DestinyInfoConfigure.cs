using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class DestinyInfoConfigure : IMessage<DestinyInfoConfigure>, IMessage, IEquatable<DestinyInfoConfigure>, IDeepCloneable<DestinyInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<DestinyInfoConfigure> _parser = new MessageParser<DestinyInfoConfigure>(() => new DestinyInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int FunctionNameFieldNumber = 2;

	private string functionName_ = "";

	public const int CardTypeFieldNumber = 3;

	private CardType cardType_;

	public const int NameIDFieldNumber = 4;

	private int nameID_;

	public const int CardNumbFieldNumber = 5;

	private int cardNumb_;

	public const int DescIdFieldNumber = 6;

	private int descId_;

	public const int CommentIdFieldNumber = 7;

	private int commentId_;

	public const int ImageFieldNumber = 8;

	private string image_ = "";

	public const int ParamsFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSInt32(74u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	public const int BuffIdsFieldNumber = 10;

	private static readonly FieldCodec<int> _repeated_buffIds_codec = FieldCodec.ForSFixed32(82u);

	private readonly RepeatedField<int> buffIds_ = new RepeatedField<int>();

	public const int SummonIdFieldNumber = 11;

	private int summonId_;

	public const int PerformFieldNumber = 12;

	private int perform_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<DestinyInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DestinyReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<int> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BuffIds => buffIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SummonId
	{
		get
		{
			return summonId_;
		}
		private set
		{
			summonId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Perform
	{
		get
		{
			return perform_;
		}
		private set
		{
			perform_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DestinyInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DestinyInfoConfigure(DestinyInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		functionName_ = other.functionName_;
		cardType_ = other.cardType_;
		nameID_ = other.nameID_;
		cardNumb_ = other.cardNumb_;
		descId_ = other.descId_;
		commentId_ = other.commentId_;
		image_ = other.image_;
		params_ = other.params_.Clone();
		buffIds_ = other.buffIds_.Clone();
		summonId_ = other.summonId_;
		perform_ = other.perform_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DestinyInfoConfigure Clone()
	{
		return new DestinyInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DestinyInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(DestinyInfoConfigure other)
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
		if (!params_.Equals(other.params_))
		{
			return false;
		}
		if (!buffIds_.Equals(other.buffIds_))
		{
			return false;
		}
		if (SummonId != other.SummonId)
		{
			return false;
		}
		if (Perform != other.Perform)
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
		num ^= params_.GetHashCode();
		num ^= buffIds_.GetHashCode();
		if (SummonId != 0)
		{
			num ^= SummonId.GetHashCode();
		}
		if (Perform != 0)
		{
			num ^= Perform.GetHashCode();
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
		if (NameID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(NameID);
		}
		if (CardNumb != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(CardNumb);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(DescId);
		}
		if (CommentId != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(CommentId);
		}
		if (Image.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(Image);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
		buffIds_.WriteTo(ref output, _repeated_buffIds_codec);
		if (SummonId != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(SummonId);
		}
		if (Perform != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(Perform);
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
		num += params_.CalculateSize(_repeated_params_codec);
		num += buffIds_.CalculateSize(_repeated_buffIds_codec);
		if (SummonId != 0)
		{
			num += 5;
		}
		if (Perform != 0)
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
	public void MergeFrom(DestinyInfoConfigure other)
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
			params_.Add(other.params_);
			buffIds_.Add(other.buffIds_);
			if (other.SummonId != 0)
			{
				SummonId = other.SummonId;
			}
			if (other.Perform != 0)
			{
				Perform = other.Perform;
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
			case 37u:
				NameID = input.ReadSFixed32();
				break;
			case 45u:
				CardNumb = input.ReadSFixed32();
				break;
			case 53u:
				DescId = input.ReadSFixed32();
				break;
			case 61u:
				CommentId = input.ReadSFixed32();
				break;
			case 66u:
				Image = input.ReadString();
				break;
			case 72u:
			case 74u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 82u:
			case 85u:
				buffIds_.AddEntriesFrom(ref input, _repeated_buffIds_codec);
				break;
			case 93u:
				SummonId = input.ReadSFixed32();
				break;
			case 101u:
				Perform = input.ReadSFixed32();
				break;
			}
		}
	}
}
