using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class StorySkinConfigure : IMessage<StorySkinConfigure>, IMessage, IEquatable<StorySkinConfigure>, IDeepCloneable<StorySkinConfigure>, IBufferMessage
{
	private static readonly MessageParser<StorySkinConfigure> _parser = new MessageParser<StorySkinConfigure>(() => new StorySkinConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IDFieldNumber = 1;

	private int iD_;

	public const int BasePaintingFieldNumber = 2;

	private string basePainting_ = "";

	public const int SfwPaintingFieldNumber = 3;

	private string sfwPainting_ = "";

	public const int PivotFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_pivot_codec = FieldCodec.ForSInt32(34u);

	private readonly RepeatedField<int> pivot_ = new RepeatedField<int>();

	public const int Emoji1FieldNumber = 5;

	private string emoji1_ = "";

	public const int Emoji2FieldNumber = 6;

	private string emoji2_ = "";

	public const int Emoji3FieldNumber = 7;

	private string emoji3_ = "";

	public const int Emoji4FieldNumber = 8;

	private string emoji4_ = "";

	public const int Emoji5FieldNumber = 9;

	private string emoji5_ = "";

	public const int Emoji6FieldNumber = 10;

	private string emoji6_ = "";

	public const int Emoji7FieldNumber = 11;

	private string emoji7_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<StorySkinConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => StoryReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ID
	{
		get
		{
			return iD_;
		}
		private set
		{
			iD_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BasePainting
	{
		get
		{
			return basePainting_;
		}
		private set
		{
			basePainting_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SfwPainting
	{
		get
		{
			return sfwPainting_;
		}
		private set
		{
			sfwPainting_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Pivot => pivot_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Emoji1
	{
		get
		{
			return emoji1_;
		}
		private set
		{
			emoji1_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Emoji2
	{
		get
		{
			return emoji2_;
		}
		private set
		{
			emoji2_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Emoji3
	{
		get
		{
			return emoji3_;
		}
		private set
		{
			emoji3_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Emoji4
	{
		get
		{
			return emoji4_;
		}
		private set
		{
			emoji4_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Emoji5
	{
		get
		{
			return emoji5_;
		}
		private set
		{
			emoji5_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Emoji6
	{
		get
		{
			return emoji6_;
		}
		private set
		{
			emoji6_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Emoji7
	{
		get
		{
			return emoji7_;
		}
		private set
		{
			emoji7_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StorySkinConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StorySkinConfigure(StorySkinConfigure other)
		: this()
	{
		iD_ = other.iD_;
		basePainting_ = other.basePainting_;
		sfwPainting_ = other.sfwPainting_;
		pivot_ = other.pivot_.Clone();
		emoji1_ = other.emoji1_;
		emoji2_ = other.emoji2_;
		emoji3_ = other.emoji3_;
		emoji4_ = other.emoji4_;
		emoji5_ = other.emoji5_;
		emoji6_ = other.emoji6_;
		emoji7_ = other.emoji7_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StorySkinConfigure Clone()
	{
		return new StorySkinConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as StorySkinConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(StorySkinConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ID != other.ID)
		{
			return false;
		}
		if (BasePainting != other.BasePainting)
		{
			return false;
		}
		if (SfwPainting != other.SfwPainting)
		{
			return false;
		}
		if (!pivot_.Equals(other.pivot_))
		{
			return false;
		}
		if (Emoji1 != other.Emoji1)
		{
			return false;
		}
		if (Emoji2 != other.Emoji2)
		{
			return false;
		}
		if (Emoji3 != other.Emoji3)
		{
			return false;
		}
		if (Emoji4 != other.Emoji4)
		{
			return false;
		}
		if (Emoji5 != other.Emoji5)
		{
			return false;
		}
		if (Emoji6 != other.Emoji6)
		{
			return false;
		}
		if (Emoji7 != other.Emoji7)
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
		if (ID != 0)
		{
			num ^= ID.GetHashCode();
		}
		if (BasePainting.Length != 0)
		{
			num ^= BasePainting.GetHashCode();
		}
		if (SfwPainting.Length != 0)
		{
			num ^= SfwPainting.GetHashCode();
		}
		num ^= pivot_.GetHashCode();
		if (Emoji1.Length != 0)
		{
			num ^= Emoji1.GetHashCode();
		}
		if (Emoji2.Length != 0)
		{
			num ^= Emoji2.GetHashCode();
		}
		if (Emoji3.Length != 0)
		{
			num ^= Emoji3.GetHashCode();
		}
		if (Emoji4.Length != 0)
		{
			num ^= Emoji4.GetHashCode();
		}
		if (Emoji5.Length != 0)
		{
			num ^= Emoji5.GetHashCode();
		}
		if (Emoji6.Length != 0)
		{
			num ^= Emoji6.GetHashCode();
		}
		if (Emoji7.Length != 0)
		{
			num ^= Emoji7.GetHashCode();
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
		if (ID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ID);
		}
		if (BasePainting.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(BasePainting);
		}
		if (SfwPainting.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(SfwPainting);
		}
		pivot_.WriteTo(ref output, _repeated_pivot_codec);
		if (Emoji1.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Emoji1);
		}
		if (Emoji2.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(Emoji2);
		}
		if (Emoji3.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(Emoji3);
		}
		if (Emoji4.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(Emoji4);
		}
		if (Emoji5.Length != 0)
		{
			output.WriteRawTag(74);
			output.WriteString(Emoji5);
		}
		if (Emoji6.Length != 0)
		{
			output.WriteRawTag(82);
			output.WriteString(Emoji6);
		}
		if (Emoji7.Length != 0)
		{
			output.WriteRawTag(90);
			output.WriteString(Emoji7);
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
		if (ID != 0)
		{
			num += 5;
		}
		if (BasePainting.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BasePainting);
		}
		if (SfwPainting.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SfwPainting);
		}
		num += pivot_.CalculateSize(_repeated_pivot_codec);
		if (Emoji1.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Emoji1);
		}
		if (Emoji2.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Emoji2);
		}
		if (Emoji3.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Emoji3);
		}
		if (Emoji4.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Emoji4);
		}
		if (Emoji5.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Emoji5);
		}
		if (Emoji6.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Emoji6);
		}
		if (Emoji7.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Emoji7);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(StorySkinConfigure other)
	{
		if (other != null)
		{
			if (other.ID != 0)
			{
				ID = other.ID;
			}
			if (other.BasePainting.Length != 0)
			{
				BasePainting = other.BasePainting;
			}
			if (other.SfwPainting.Length != 0)
			{
				SfwPainting = other.SfwPainting;
			}
			pivot_.Add(other.pivot_);
			if (other.Emoji1.Length != 0)
			{
				Emoji1 = other.Emoji1;
			}
			if (other.Emoji2.Length != 0)
			{
				Emoji2 = other.Emoji2;
			}
			if (other.Emoji3.Length != 0)
			{
				Emoji3 = other.Emoji3;
			}
			if (other.Emoji4.Length != 0)
			{
				Emoji4 = other.Emoji4;
			}
			if (other.Emoji5.Length != 0)
			{
				Emoji5 = other.Emoji5;
			}
			if (other.Emoji6.Length != 0)
			{
				Emoji6 = other.Emoji6;
			}
			if (other.Emoji7.Length != 0)
			{
				Emoji7 = other.Emoji7;
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
				ID = input.ReadSFixed32();
				break;
			case 18u:
				BasePainting = input.ReadString();
				break;
			case 26u:
				SfwPainting = input.ReadString();
				break;
			case 32u:
			case 34u:
				pivot_.AddEntriesFrom(ref input, _repeated_pivot_codec);
				break;
			case 42u:
				Emoji1 = input.ReadString();
				break;
			case 50u:
				Emoji2 = input.ReadString();
				break;
			case 58u:
				Emoji3 = input.ReadString();
				break;
			case 66u:
				Emoji4 = input.ReadString();
				break;
			case 74u:
				Emoji5 = input.ReadString();
				break;
			case 82u:
				Emoji6 = input.ReadString();
				break;
			case 90u:
				Emoji7 = input.ReadString();
				break;
			}
		}
	}
}
