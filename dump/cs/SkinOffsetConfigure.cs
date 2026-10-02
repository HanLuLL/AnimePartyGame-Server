using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SkinOffsetConfigure : IMessage<SkinOffsetConfigure>, IMessage, IEquatable<SkinOffsetConfigure>, IDeepCloneable<SkinOffsetConfigure>, IBufferMessage
{
	private static readonly MessageParser<SkinOffsetConfigure> _parser = new MessageParser<SkinOffsetConfigure>(() => new SkinOffsetConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ImageNameFieldNumber = 2;

	private string imageName_ = "";

	public const int OffsetXFieldNumber = 3;

	private float offsetX_;

	public const int OffsetYFieldNumber = 4;

	private float offsetY_;

	public const int CharacterStateSkinOffsetXFieldNumber = 5;

	private float characterStateSkinOffsetX_;

	public const int CharacterStateSkinOffsetYFieldNumber = 6;

	private float characterStateSkinOffsetY_;

	public const int CharacterStateSkinScaleFieldNumber = 7;

	private float characterStateSkinScale_;

	public const int IsCharacterStateSkinFlipXFieldNumber = 8;

	private bool isCharacterStateSkinFlipX_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SkinOffsetConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SkinReflection.Descriptor.MessageTypes[2];

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
	public string ImageName
	{
		get
		{
			return imageName_;
		}
		private set
		{
			imageName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float OffsetX
	{
		get
		{
			return offsetX_;
		}
		private set
		{
			offsetX_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float OffsetY
	{
		get
		{
			return offsetY_;
		}
		private set
		{
			offsetY_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float CharacterStateSkinOffsetX
	{
		get
		{
			return characterStateSkinOffsetX_;
		}
		private set
		{
			characterStateSkinOffsetX_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float CharacterStateSkinOffsetY
	{
		get
		{
			return characterStateSkinOffsetY_;
		}
		private set
		{
			characterStateSkinOffsetY_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float CharacterStateSkinScale
	{
		get
		{
			return characterStateSkinScale_;
		}
		private set
		{
			characterStateSkinScale_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsCharacterStateSkinFlipX
	{
		get
		{
			return isCharacterStateSkinFlipX_;
		}
		private set
		{
			isCharacterStateSkinFlipX_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinOffsetConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinOffsetConfigure(SkinOffsetConfigure other)
		: this()
	{
		id_ = other.id_;
		imageName_ = other.imageName_;
		offsetX_ = other.offsetX_;
		offsetY_ = other.offsetY_;
		characterStateSkinOffsetX_ = other.characterStateSkinOffsetX_;
		characterStateSkinOffsetY_ = other.characterStateSkinOffsetY_;
		characterStateSkinScale_ = other.characterStateSkinScale_;
		isCharacterStateSkinFlipX_ = other.isCharacterStateSkinFlipX_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinOffsetConfigure Clone()
	{
		return new SkinOffsetConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SkinOffsetConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SkinOffsetConfigure other)
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
		if (ImageName != other.ImageName)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(OffsetX, other.OffsetX))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(OffsetY, other.OffsetY))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(CharacterStateSkinOffsetX, other.CharacterStateSkinOffsetX))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(CharacterStateSkinOffsetY, other.CharacterStateSkinOffsetY))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(CharacterStateSkinScale, other.CharacterStateSkinScale))
		{
			return false;
		}
		if (IsCharacterStateSkinFlipX != other.IsCharacterStateSkinFlipX)
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
		if (ImageName.Length != 0)
		{
			num ^= ImageName.GetHashCode();
		}
		if (OffsetX != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(OffsetX);
		}
		if (OffsetY != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(OffsetY);
		}
		if (CharacterStateSkinOffsetX != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(CharacterStateSkinOffsetX);
		}
		if (CharacterStateSkinOffsetY != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(CharacterStateSkinOffsetY);
		}
		if (CharacterStateSkinScale != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(CharacterStateSkinScale);
		}
		if (IsCharacterStateSkinFlipX)
		{
			num ^= IsCharacterStateSkinFlipX.GetHashCode();
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
		if (ImageName.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(ImageName);
		}
		if (OffsetX != 0f)
		{
			output.WriteRawTag(29);
			output.WriteFloat(OffsetX);
		}
		if (OffsetY != 0f)
		{
			output.WriteRawTag(37);
			output.WriteFloat(OffsetY);
		}
		if (CharacterStateSkinOffsetX != 0f)
		{
			output.WriteRawTag(45);
			output.WriteFloat(CharacterStateSkinOffsetX);
		}
		if (CharacterStateSkinOffsetY != 0f)
		{
			output.WriteRawTag(53);
			output.WriteFloat(CharacterStateSkinOffsetY);
		}
		if (CharacterStateSkinScale != 0f)
		{
			output.WriteRawTag(61);
			output.WriteFloat(CharacterStateSkinScale);
		}
		if (IsCharacterStateSkinFlipX)
		{
			output.WriteRawTag(64);
			output.WriteBool(IsCharacterStateSkinFlipX);
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
		if (ImageName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ImageName);
		}
		if (OffsetX != 0f)
		{
			num += 5;
		}
		if (OffsetY != 0f)
		{
			num += 5;
		}
		if (CharacterStateSkinOffsetX != 0f)
		{
			num += 5;
		}
		if (CharacterStateSkinOffsetY != 0f)
		{
			num += 5;
		}
		if (CharacterStateSkinScale != 0f)
		{
			num += 5;
		}
		if (IsCharacterStateSkinFlipX)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SkinOffsetConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.ImageName.Length != 0)
			{
				ImageName = other.ImageName;
			}
			if (other.OffsetX != 0f)
			{
				OffsetX = other.OffsetX;
			}
			if (other.OffsetY != 0f)
			{
				OffsetY = other.OffsetY;
			}
			if (other.CharacterStateSkinOffsetX != 0f)
			{
				CharacterStateSkinOffsetX = other.CharacterStateSkinOffsetX;
			}
			if (other.CharacterStateSkinOffsetY != 0f)
			{
				CharacterStateSkinOffsetY = other.CharacterStateSkinOffsetY;
			}
			if (other.CharacterStateSkinScale != 0f)
			{
				CharacterStateSkinScale = other.CharacterStateSkinScale;
			}
			if (other.IsCharacterStateSkinFlipX)
			{
				IsCharacterStateSkinFlipX = other.IsCharacterStateSkinFlipX;
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
				ImageName = input.ReadString();
				break;
			case 29u:
				OffsetX = input.ReadFloat();
				break;
			case 37u:
				OffsetY = input.ReadFloat();
				break;
			case 45u:
				CharacterStateSkinOffsetX = input.ReadFloat();
				break;
			case 53u:
				CharacterStateSkinOffsetY = input.ReadFloat();
				break;
			case 61u:
				CharacterStateSkinScale = input.ReadFloat();
				break;
			case 64u:
				IsCharacterStateSkinFlipX = input.ReadBool();
				break;
			}
		}
	}
}
