using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SkinSellInfoConfigure : IMessage<SkinSellInfoConfigure>, IMessage, IEquatable<SkinSellInfoConfigure>, IDeepCloneable<SkinSellInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<SkinSellInfoConfigure> _parser = new MessageParser<SkinSellInfoConfigure>(() => new SkinSellInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int EntranceFieldNumber = 2;

	private string entrance_ = "";

	public const int BackgroundFieldNumber = 3;

	private string background_ = "";

	public const int EntranceTitleFieldNumber = 4;

	private int entranceTitle_;

	public const int SplitSaleFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_splitSale_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> splitSale_ = new RepeatedField<int>();

	public const int SkinSellInfoConfigureItemsFieldNumber = 6;

	private static readonly FieldCodec<SkinSellInfoConfigureItem> _repeated_skinSellInfoConfigureItems_codec = FieldCodec.ForMessage(50u, SkinSellInfoConfigureItem.Parser);

	private readonly RepeatedField<SkinSellInfoConfigureItem> skinSellInfoConfigureItems_ = new RepeatedField<SkinSellInfoConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SkinSellInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SkinSellReflection.Descriptor.MessageTypes[0];

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
	public string Entrance
	{
		get
		{
			return entrance_;
		}
		private set
		{
			entrance_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Background
	{
		get
		{
			return background_;
		}
		private set
		{
			background_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EntranceTitle
	{
		get
		{
			return entranceTitle_;
		}
		private set
		{
			entranceTitle_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> SplitSale => splitSale_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SkinSellInfoConfigureItem> SkinSellInfoConfigureItems => skinSellInfoConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinSellInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinSellInfoConfigure(SkinSellInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		entrance_ = other.entrance_;
		background_ = other.background_;
		entranceTitle_ = other.entranceTitle_;
		splitSale_ = other.splitSale_.Clone();
		skinSellInfoConfigureItems_ = other.skinSellInfoConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinSellInfoConfigure Clone()
	{
		return new SkinSellInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SkinSellInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SkinSellInfoConfigure other)
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
		if (Entrance != other.Entrance)
		{
			return false;
		}
		if (Background != other.Background)
		{
			return false;
		}
		if (EntranceTitle != other.EntranceTitle)
		{
			return false;
		}
		if (!splitSale_.Equals(other.splitSale_))
		{
			return false;
		}
		if (!skinSellInfoConfigureItems_.Equals(other.skinSellInfoConfigureItems_))
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
		if (Entrance.Length != 0)
		{
			num ^= Entrance.GetHashCode();
		}
		if (Background.Length != 0)
		{
			num ^= Background.GetHashCode();
		}
		if (EntranceTitle != 0)
		{
			num ^= EntranceTitle.GetHashCode();
		}
		num ^= splitSale_.GetHashCode();
		num ^= skinSellInfoConfigureItems_.GetHashCode();
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
		if (Entrance.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Entrance);
		}
		if (Background.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Background);
		}
		if (EntranceTitle != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(EntranceTitle);
		}
		splitSale_.WriteTo(ref output, _repeated_splitSale_codec);
		skinSellInfoConfigureItems_.WriteTo(ref output, _repeated_skinSellInfoConfigureItems_codec);
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
		if (Entrance.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Entrance);
		}
		if (Background.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Background);
		}
		if (EntranceTitle != 0)
		{
			num += 5;
		}
		num += splitSale_.CalculateSize(_repeated_splitSale_codec);
		num += skinSellInfoConfigureItems_.CalculateSize(_repeated_skinSellInfoConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SkinSellInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.Entrance.Length != 0)
			{
				Entrance = other.Entrance;
			}
			if (other.Background.Length != 0)
			{
				Background = other.Background;
			}
			if (other.EntranceTitle != 0)
			{
				EntranceTitle = other.EntranceTitle;
			}
			splitSale_.Add(other.splitSale_);
			skinSellInfoConfigureItems_.Add(other.skinSellInfoConfigureItems_);
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
				Entrance = input.ReadString();
				break;
			case 26u:
				Background = input.ReadString();
				break;
			case 37u:
				EntranceTitle = input.ReadSFixed32();
				break;
			case 42u:
			case 45u:
				splitSale_.AddEntriesFrom(ref input, _repeated_splitSale_codec);
				break;
			case 50u:
				skinSellInfoConfigureItems_.AddEntriesFrom(ref input, _repeated_skinSellInfoConfigureItems_codec);
				break;
			}
		}
	}
}
