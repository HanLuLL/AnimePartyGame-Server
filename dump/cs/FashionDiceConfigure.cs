using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class FashionDiceConfigure : IMessage<FashionDiceConfigure>, IMessage, IEquatable<FashionDiceConfigure>, IDeepCloneable<FashionDiceConfigure>, IBufferMessage
{
	private static readonly MessageParser<FashionDiceConfigure> _parser = new MessageParser<FashionDiceConfigure>(() => new FashionDiceConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int DiceModelFieldNumber = 2;

	private string diceModel_ = "";

	public const int DiceAnimFieldNumber = 3;

	private string diceAnim_ = "";

	public const int DiceVfxFieldNumber = 4;

	private string diceVfx_ = "";

	public const int DiceSFXFieldNumber = 5;

	private int diceSFX_;

	public const int PreviewVideoFieldNumber = 6;

	private string previewVideo_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FashionDiceConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FashionReflection.Descriptor.MessageTypes[4];

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
	public string DiceModel
	{
		get
		{
			return diceModel_;
		}
		private set
		{
			diceModel_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string DiceAnim
	{
		get
		{
			return diceAnim_;
		}
		private set
		{
			diceAnim_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string DiceVfx
	{
		get
		{
			return diceVfx_;
		}
		private set
		{
			diceVfx_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiceSFX
	{
		get
		{
			return diceSFX_;
		}
		private set
		{
			diceSFX_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PreviewVideo
	{
		get
		{
			return previewVideo_;
		}
		private set
		{
			previewVideo_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionDiceConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionDiceConfigure(FashionDiceConfigure other)
		: this()
	{
		id_ = other.id_;
		diceModel_ = other.diceModel_;
		diceAnim_ = other.diceAnim_;
		diceVfx_ = other.diceVfx_;
		diceSFX_ = other.diceSFX_;
		previewVideo_ = other.previewVideo_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionDiceConfigure Clone()
	{
		return new FashionDiceConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FashionDiceConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FashionDiceConfigure other)
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
		if (DiceModel != other.DiceModel)
		{
			return false;
		}
		if (DiceAnim != other.DiceAnim)
		{
			return false;
		}
		if (DiceVfx != other.DiceVfx)
		{
			return false;
		}
		if (DiceSFX != other.DiceSFX)
		{
			return false;
		}
		if (PreviewVideo != other.PreviewVideo)
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
		if (DiceModel.Length != 0)
		{
			num ^= DiceModel.GetHashCode();
		}
		if (DiceAnim.Length != 0)
		{
			num ^= DiceAnim.GetHashCode();
		}
		if (DiceVfx.Length != 0)
		{
			num ^= DiceVfx.GetHashCode();
		}
		if (DiceSFX != 0)
		{
			num ^= DiceSFX.GetHashCode();
		}
		if (PreviewVideo.Length != 0)
		{
			num ^= PreviewVideo.GetHashCode();
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
		if (DiceModel.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(DiceModel);
		}
		if (DiceAnim.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(DiceAnim);
		}
		if (DiceVfx.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(DiceVfx);
		}
		if (DiceSFX != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DiceSFX);
		}
		if (PreviewVideo.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(PreviewVideo);
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
		if (DiceModel.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DiceModel);
		}
		if (DiceAnim.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DiceAnim);
		}
		if (DiceVfx.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DiceVfx);
		}
		if (DiceSFX != 0)
		{
			num += 5;
		}
		if (PreviewVideo.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PreviewVideo);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FashionDiceConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.DiceModel.Length != 0)
			{
				DiceModel = other.DiceModel;
			}
			if (other.DiceAnim.Length != 0)
			{
				DiceAnim = other.DiceAnim;
			}
			if (other.DiceVfx.Length != 0)
			{
				DiceVfx = other.DiceVfx;
			}
			if (other.DiceSFX != 0)
			{
				DiceSFX = other.DiceSFX;
			}
			if (other.PreviewVideo.Length != 0)
			{
				PreviewVideo = other.PreviewVideo;
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
				DiceModel = input.ReadString();
				break;
			case 26u:
				DiceAnim = input.ReadString();
				break;
			case 34u:
				DiceVfx = input.ReadString();
				break;
			case 45u:
				DiceSFX = input.ReadSFixed32();
				break;
			case 50u:
				PreviewVideo = input.ReadString();
				break;
			}
		}
	}
}
