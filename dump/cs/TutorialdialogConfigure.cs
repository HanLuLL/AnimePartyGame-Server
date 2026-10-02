using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class TutorialdialogConfigure : IMessage<TutorialdialogConfigure>, IMessage, IEquatable<TutorialdialogConfigure>, IDeepCloneable<TutorialdialogConfigure>, IBufferMessage
{
	private static readonly MessageParser<TutorialdialogConfigure> _parser = new MessageParser<TutorialdialogConfigure>(() => new TutorialdialogConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int StandingPaintingFieldNumber = 2;

	private string standingPainting_ = "";

	public const int SfwStandingPaintingFieldNumber = 3;

	private string sfwStandingPainting_ = "";

	public const int ProfilePhotoFieldNumber = 4;

	private string profilePhoto_ = "";

	public const int TutorialdialogConfigureItemsFieldNumber = 5;

	private static readonly FieldCodec<TutorialdialogConfigureItem> _repeated_tutorialdialogConfigureItems_codec = FieldCodec.ForMessage(42u, TutorialdialogConfigureItem.Parser);

	private readonly RepeatedField<TutorialdialogConfigureItem> tutorialdialogConfigureItems_ = new RepeatedField<TutorialdialogConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TutorialdialogConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => TutorialReflection.Descriptor.MessageTypes[2];

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
	public string StandingPainting
	{
		get
		{
			return standingPainting_;
		}
		private set
		{
			standingPainting_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SfwStandingPainting
	{
		get
		{
			return sfwStandingPainting_;
		}
		private set
		{
			sfwStandingPainting_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ProfilePhoto
	{
		get
		{
			return profilePhoto_;
		}
		private set
		{
			profilePhoto_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TutorialdialogConfigureItem> TutorialdialogConfigureItems => tutorialdialogConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialdialogConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialdialogConfigure(TutorialdialogConfigure other)
		: this()
	{
		id_ = other.id_;
		standingPainting_ = other.standingPainting_;
		sfwStandingPainting_ = other.sfwStandingPainting_;
		profilePhoto_ = other.profilePhoto_;
		tutorialdialogConfigureItems_ = other.tutorialdialogConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialdialogConfigure Clone()
	{
		return new TutorialdialogConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TutorialdialogConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TutorialdialogConfigure other)
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
		if (StandingPainting != other.StandingPainting)
		{
			return false;
		}
		if (SfwStandingPainting != other.SfwStandingPainting)
		{
			return false;
		}
		if (ProfilePhoto != other.ProfilePhoto)
		{
			return false;
		}
		if (!tutorialdialogConfigureItems_.Equals(other.tutorialdialogConfigureItems_))
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
		if (StandingPainting.Length != 0)
		{
			num ^= StandingPainting.GetHashCode();
		}
		if (SfwStandingPainting.Length != 0)
		{
			num ^= SfwStandingPainting.GetHashCode();
		}
		if (ProfilePhoto.Length != 0)
		{
			num ^= ProfilePhoto.GetHashCode();
		}
		num ^= tutorialdialogConfigureItems_.GetHashCode();
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
		if (StandingPainting.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(StandingPainting);
		}
		if (SfwStandingPainting.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(SfwStandingPainting);
		}
		if (ProfilePhoto.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(ProfilePhoto);
		}
		tutorialdialogConfigureItems_.WriteTo(ref output, _repeated_tutorialdialogConfigureItems_codec);
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
		if (StandingPainting.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(StandingPainting);
		}
		if (SfwStandingPainting.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SfwStandingPainting);
		}
		if (ProfilePhoto.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ProfilePhoto);
		}
		num += tutorialdialogConfigureItems_.CalculateSize(_repeated_tutorialdialogConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(TutorialdialogConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.StandingPainting.Length != 0)
			{
				StandingPainting = other.StandingPainting;
			}
			if (other.SfwStandingPainting.Length != 0)
			{
				SfwStandingPainting = other.SfwStandingPainting;
			}
			if (other.ProfilePhoto.Length != 0)
			{
				ProfilePhoto = other.ProfilePhoto;
			}
			tutorialdialogConfigureItems_.Add(other.tutorialdialogConfigureItems_);
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
				StandingPainting = input.ReadString();
				break;
			case 26u:
				SfwStandingPainting = input.ReadString();
				break;
			case 34u:
				ProfilePhoto = input.ReadString();
				break;
			case 42u:
				tutorialdialogConfigureItems_.AddEntriesFrom(ref input, _repeated_tutorialdialogConfigureItems_codec);
				break;
			}
		}
	}
}
