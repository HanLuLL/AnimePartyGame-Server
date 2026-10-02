using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class DivinationTargetConfigure : IMessage<DivinationTargetConfigure>, IMessage, IEquatable<DivinationTargetConfigure>, IDeepCloneable<DivinationTargetConfigure>, IBufferMessage
{
	private static readonly MessageParser<DivinationTargetConfigure> _parser = new MessageParser<DivinationTargetConfigure>(() => new DivinationTargetConfigure());

	private UnknownFieldSet _unknownFields;

	public const int DivinationTargetTypeFieldNumber = 1;

	private DivinationTargetType divinationTargetType_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int FunctionNameFieldNumber = 3;

	private string functionName_ = "";

	public const int CardNumbFieldNumber = 4;

	private int cardNumb_;

	public const int DescIdFieldNumber = 5;

	private int descId_;

	public const int CommentIdFieldNumber = 6;

	private int commentId_;

	public const int ImageFieldNumber = 7;

	private string image_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<DivinationTargetConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DivinationReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DivinationTargetType DivinationTargetType
	{
		get
		{
			return divinationTargetType_;
		}
		private set
		{
			divinationTargetType_ = value;
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
	public DivinationTargetConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DivinationTargetConfigure(DivinationTargetConfigure other)
		: this()
	{
		divinationTargetType_ = other.divinationTargetType_;
		nameID_ = other.nameID_;
		functionName_ = other.functionName_;
		cardNumb_ = other.cardNumb_;
		descId_ = other.descId_;
		commentId_ = other.commentId_;
		image_ = other.image_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DivinationTargetConfigure Clone()
	{
		return new DivinationTargetConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DivinationTargetConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(DivinationTargetConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DivinationTargetType != other.DivinationTargetType)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (FunctionName != other.FunctionName)
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
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (DivinationTargetType != DivinationTargetType.None)
		{
			num ^= DivinationTargetType.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (FunctionName.Length != 0)
		{
			num ^= FunctionName.GetHashCode();
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
		if (DivinationTargetType != DivinationTargetType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)DivinationTargetType);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NameID);
		}
		if (FunctionName.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(FunctionName);
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
		if (DivinationTargetType != DivinationTargetType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)DivinationTargetType);
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (FunctionName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(FunctionName);
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
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(DivinationTargetConfigure other)
	{
		if (other != null)
		{
			if (other.DivinationTargetType != DivinationTargetType.None)
			{
				DivinationTargetType = other.DivinationTargetType;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.FunctionName.Length != 0)
			{
				FunctionName = other.FunctionName;
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
			case 8u:
				DivinationTargetType = (DivinationTargetType)input.ReadEnum();
				break;
			case 21u:
				NameID = input.ReadSFixed32();
				break;
			case 26u:
				FunctionName = input.ReadString();
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
			}
		}
	}
}
