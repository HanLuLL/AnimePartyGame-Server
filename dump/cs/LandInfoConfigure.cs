using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class LandInfoConfigure : IMessage<LandInfoConfigure>, IMessage, IEquatable<LandInfoConfigure>, IDeepCloneable<LandInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<LandInfoConfigure> _parser = new MessageParser<LandInfoConfigure>(() => new LandInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int LandTypeFieldNumber = 1;

	private LandType landType_;

	public const int PlatformMainTexOffsetFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_platformMainTexOffset_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> platformMainTexOffset_ = new RepeatedField<int>();

	public const int IsGalleryShowFieldNumber = 3;

	private bool isGalleryShow_;

	public const int IsSPLandFieldNumber = 4;

	private bool isSPLand_;

	public const int NameIDFieldNumber = 5;

	private int nameID_;

	public const int DescriptionIDFieldNumber = 6;

	private int descriptionID_;

	public const int UIElementFieldNumber = 7;

	private string uIElement_ = "";

	public const int SfwUIElementFieldNumber = 8;

	private string sfwUIElement_ = "";

	public const int LandIconFieldNumber = 9;

	private string landIcon_ = "";

	public const int LandSfxFieldNumber = 10;

	private int landSfx_;

	public const int ParamsFieldNumber = 11;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSInt32(90u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	public const int Perform1FieldNumber = 12;

	private int perform1_;

	public const int Perform2FieldNumber = 13;

	private int perform2_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LandInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => LandReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandType LandType
	{
		get
		{
			return landType_;
		}
		private set
		{
			landType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PlatformMainTexOffset => platformMainTexOffset_;

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
	public bool IsSPLand
	{
		get
		{
			return isSPLand_;
		}
		private set
		{
			isSPLand_ = value;
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
	public int DescriptionID
	{
		get
		{
			return descriptionID_;
		}
		private set
		{
			descriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string UIElement
	{
		get
		{
			return uIElement_;
		}
		private set
		{
			uIElement_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SfwUIElement
	{
		get
		{
			return sfwUIElement_;
		}
		private set
		{
			sfwUIElement_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LandIcon
	{
		get
		{
			return landIcon_;
		}
		private set
		{
			landIcon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LandSfx
	{
		get
		{
			return landSfx_;
		}
		private set
		{
			landSfx_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Params => params_;

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
	public LandInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandInfoConfigure(LandInfoConfigure other)
		: this()
	{
		landType_ = other.landType_;
		platformMainTexOffset_ = other.platformMainTexOffset_.Clone();
		isGalleryShow_ = other.isGalleryShow_;
		isSPLand_ = other.isSPLand_;
		nameID_ = other.nameID_;
		descriptionID_ = other.descriptionID_;
		uIElement_ = other.uIElement_;
		sfwUIElement_ = other.sfwUIElement_;
		landIcon_ = other.landIcon_;
		landSfx_ = other.landSfx_;
		params_ = other.params_.Clone();
		perform1_ = other.perform1_;
		perform2_ = other.perform2_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandInfoConfigure Clone()
	{
		return new LandInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LandInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LandInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (LandType != other.LandType)
		{
			return false;
		}
		if (!platformMainTexOffset_.Equals(other.platformMainTexOffset_))
		{
			return false;
		}
		if (IsGalleryShow != other.IsGalleryShow)
		{
			return false;
		}
		if (IsSPLand != other.IsSPLand)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (DescriptionID != other.DescriptionID)
		{
			return false;
		}
		if (UIElement != other.UIElement)
		{
			return false;
		}
		if (SfwUIElement != other.SfwUIElement)
		{
			return false;
		}
		if (LandIcon != other.LandIcon)
		{
			return false;
		}
		if (LandSfx != other.LandSfx)
		{
			return false;
		}
		if (!params_.Equals(other.params_))
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
		if (LandType != LandType.None)
		{
			num ^= LandType.GetHashCode();
		}
		num ^= platformMainTexOffset_.GetHashCode();
		if (IsGalleryShow)
		{
			num ^= IsGalleryShow.GetHashCode();
		}
		if (IsSPLand)
		{
			num ^= IsSPLand.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
		}
		if (UIElement.Length != 0)
		{
			num ^= UIElement.GetHashCode();
		}
		if (SfwUIElement.Length != 0)
		{
			num ^= SfwUIElement.GetHashCode();
		}
		if (LandIcon.Length != 0)
		{
			num ^= LandIcon.GetHashCode();
		}
		if (LandSfx != 0)
		{
			num ^= LandSfx.GetHashCode();
		}
		num ^= params_.GetHashCode();
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
		if (LandType != LandType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)LandType);
		}
		platformMainTexOffset_.WriteTo(ref output, _repeated_platformMainTexOffset_codec);
		if (IsGalleryShow)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsGalleryShow);
		}
		if (IsSPLand)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsSPLand);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(NameID);
		}
		if (DescriptionID != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(DescriptionID);
		}
		if (UIElement.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(UIElement);
		}
		if (SfwUIElement.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(SfwUIElement);
		}
		if (LandIcon.Length != 0)
		{
			output.WriteRawTag(74);
			output.WriteString(LandIcon);
		}
		if (LandSfx != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(LandSfx);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
		if (Perform1 != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(Perform1);
		}
		if (Perform2 != 0)
		{
			output.WriteRawTag(109);
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
		if (LandType != LandType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)LandType);
		}
		num += platformMainTexOffset_.CalculateSize(_repeated_platformMainTexOffset_codec);
		if (IsGalleryShow)
		{
			num += 2;
		}
		if (IsSPLand)
		{
			num += 2;
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (DescriptionID != 0)
		{
			num += 5;
		}
		if (UIElement.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(UIElement);
		}
		if (SfwUIElement.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SfwUIElement);
		}
		if (LandIcon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LandIcon);
		}
		if (LandSfx != 0)
		{
			num += 5;
		}
		num += params_.CalculateSize(_repeated_params_codec);
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
	public void MergeFrom(LandInfoConfigure other)
	{
		if (other != null)
		{
			if (other.LandType != LandType.None)
			{
				LandType = other.LandType;
			}
			platformMainTexOffset_.Add(other.platformMainTexOffset_);
			if (other.IsGalleryShow)
			{
				IsGalleryShow = other.IsGalleryShow;
			}
			if (other.IsSPLand)
			{
				IsSPLand = other.IsSPLand;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.DescriptionID != 0)
			{
				DescriptionID = other.DescriptionID;
			}
			if (other.UIElement.Length != 0)
			{
				UIElement = other.UIElement;
			}
			if (other.SfwUIElement.Length != 0)
			{
				SfwUIElement = other.SfwUIElement;
			}
			if (other.LandIcon.Length != 0)
			{
				LandIcon = other.LandIcon;
			}
			if (other.LandSfx != 0)
			{
				LandSfx = other.LandSfx;
			}
			params_.Add(other.params_);
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
			case 8u:
				LandType = (LandType)input.ReadEnum();
				break;
			case 18u:
			case 21u:
				platformMainTexOffset_.AddEntriesFrom(ref input, _repeated_platformMainTexOffset_codec);
				break;
			case 24u:
				IsGalleryShow = input.ReadBool();
				break;
			case 32u:
				IsSPLand = input.ReadBool();
				break;
			case 45u:
				NameID = input.ReadSFixed32();
				break;
			case 53u:
				DescriptionID = input.ReadSFixed32();
				break;
			case 58u:
				UIElement = input.ReadString();
				break;
			case 66u:
				SfwUIElement = input.ReadString();
				break;
			case 74u:
				LandIcon = input.ReadString();
				break;
			case 85u:
				LandSfx = input.ReadSFixed32();
				break;
			case 88u:
			case 90u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 101u:
				Perform1 = input.ReadSFixed32();
				break;
			case 109u:
				Perform2 = input.ReadSFixed32();
				break;
			}
		}
	}
}
