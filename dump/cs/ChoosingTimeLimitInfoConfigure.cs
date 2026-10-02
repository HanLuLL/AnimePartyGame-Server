using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ChoosingTimeLimitInfoConfigure : IMessage<ChoosingTimeLimitInfoConfigure>, IMessage, IEquatable<ChoosingTimeLimitInfoConfigure>, IDeepCloneable<ChoosingTimeLimitInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<ChoosingTimeLimitInfoConfigure> _parser = new MessageParser<ChoosingTimeLimitInfoConfigure>(() => new ChoosingTimeLimitInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ChoosingTimeTypeFieldNumber = 1;

	private ChoosingTimeType choosingTimeType_;

	public const int IsDefaultFieldNumber = 2;

	private bool isDefault_;

	public const int DescriptionIDFieldNumber = 3;

	private int descriptionID_;

	public const int ChoosingCardTimeLimitFieldNumber = 4;

	private int choosingCardTimeLimit_;

	public const int OtherTimeLimitFieldNumber = 5;

	private int otherTimeLimit_;

	public const int ExtraTimeFieldNumber = 6;

	private int extraTime_;

	public const int PunishmentTimeLimitFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_punishmentTimeLimit_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> punishmentTimeLimit_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChoosingTimeLimitInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChoosingTimeLimitReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeType ChoosingTimeType
	{
		get
		{
			return choosingTimeType_;
		}
		private set
		{
			choosingTimeType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDefault
	{
		get
		{
			return isDefault_;
		}
		private set
		{
			isDefault_ = value;
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
	public int ChoosingCardTimeLimit
	{
		get
		{
			return choosingCardTimeLimit_;
		}
		private set
		{
			choosingCardTimeLimit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OtherTimeLimit
	{
		get
		{
			return otherTimeLimit_;
		}
		private set
		{
			otherTimeLimit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ExtraTime
	{
		get
		{
			return extraTime_;
		}
		private set
		{
			extraTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PunishmentTimeLimit => punishmentTimeLimit_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitInfoConfigure(ChoosingTimeLimitInfoConfigure other)
		: this()
	{
		choosingTimeType_ = other.choosingTimeType_;
		isDefault_ = other.isDefault_;
		descriptionID_ = other.descriptionID_;
		choosingCardTimeLimit_ = other.choosingCardTimeLimit_;
		otherTimeLimit_ = other.otherTimeLimit_;
		extraTime_ = other.extraTime_;
		punishmentTimeLimit_ = other.punishmentTimeLimit_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitInfoConfigure Clone()
	{
		return new ChoosingTimeLimitInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChoosingTimeLimitInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChoosingTimeLimitInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ChoosingTimeType != other.ChoosingTimeType)
		{
			return false;
		}
		if (IsDefault != other.IsDefault)
		{
			return false;
		}
		if (DescriptionID != other.DescriptionID)
		{
			return false;
		}
		if (ChoosingCardTimeLimit != other.ChoosingCardTimeLimit)
		{
			return false;
		}
		if (OtherTimeLimit != other.OtherTimeLimit)
		{
			return false;
		}
		if (ExtraTime != other.ExtraTime)
		{
			return false;
		}
		if (!punishmentTimeLimit_.Equals(other.punishmentTimeLimit_))
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
		if (ChoosingTimeType != ChoosingTimeType.None)
		{
			num ^= ChoosingTimeType.GetHashCode();
		}
		if (IsDefault)
		{
			num ^= IsDefault.GetHashCode();
		}
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
		}
		if (ChoosingCardTimeLimit != 0)
		{
			num ^= ChoosingCardTimeLimit.GetHashCode();
		}
		if (OtherTimeLimit != 0)
		{
			num ^= OtherTimeLimit.GetHashCode();
		}
		if (ExtraTime != 0)
		{
			num ^= ExtraTime.GetHashCode();
		}
		num ^= punishmentTimeLimit_.GetHashCode();
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
		if (ChoosingTimeType != ChoosingTimeType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)ChoosingTimeType);
		}
		if (IsDefault)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsDefault);
		}
		if (DescriptionID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DescriptionID);
		}
		if (ChoosingCardTimeLimit != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ChoosingCardTimeLimit);
		}
		if (OtherTimeLimit != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(OtherTimeLimit);
		}
		if (ExtraTime != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(ExtraTime);
		}
		punishmentTimeLimit_.WriteTo(ref output, _repeated_punishmentTimeLimit_codec);
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
		if (ChoosingTimeType != ChoosingTimeType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ChoosingTimeType);
		}
		if (IsDefault)
		{
			num += 2;
		}
		if (DescriptionID != 0)
		{
			num += 5;
		}
		if (ChoosingCardTimeLimit != 0)
		{
			num += 5;
		}
		if (OtherTimeLimit != 0)
		{
			num += 5;
		}
		if (ExtraTime != 0)
		{
			num += 5;
		}
		num += punishmentTimeLimit_.CalculateSize(_repeated_punishmentTimeLimit_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChoosingTimeLimitInfoConfigure other)
	{
		if (other != null)
		{
			if (other.ChoosingTimeType != ChoosingTimeType.None)
			{
				ChoosingTimeType = other.ChoosingTimeType;
			}
			if (other.IsDefault)
			{
				IsDefault = other.IsDefault;
			}
			if (other.DescriptionID != 0)
			{
				DescriptionID = other.DescriptionID;
			}
			if (other.ChoosingCardTimeLimit != 0)
			{
				ChoosingCardTimeLimit = other.ChoosingCardTimeLimit;
			}
			if (other.OtherTimeLimit != 0)
			{
				OtherTimeLimit = other.OtherTimeLimit;
			}
			if (other.ExtraTime != 0)
			{
				ExtraTime = other.ExtraTime;
			}
			punishmentTimeLimit_.Add(other.punishmentTimeLimit_);
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
				ChoosingTimeType = (ChoosingTimeType)input.ReadEnum();
				break;
			case 16u:
				IsDefault = input.ReadBool();
				break;
			case 29u:
				DescriptionID = input.ReadSFixed32();
				break;
			case 37u:
				ChoosingCardTimeLimit = input.ReadSFixed32();
				break;
			case 45u:
				OtherTimeLimit = input.ReadSFixed32();
				break;
			case 53u:
				ExtraTime = input.ReadSFixed32();
				break;
			case 58u:
			case 61u:
				punishmentTimeLimit_.AddEntriesFrom(ref input, _repeated_punishmentTimeLimit_codec);
				break;
			}
		}
	}
}
