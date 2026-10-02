using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ChoosingTimeLimitroomsettingConfigure : IMessage<ChoosingTimeLimitroomsettingConfigure>, IMessage, IEquatable<ChoosingTimeLimitroomsettingConfigure>, IDeepCloneable<ChoosingTimeLimitroomsettingConfigure>, IBufferMessage
{
	private static readonly MessageParser<ChoosingTimeLimitroomsettingConfigure> _parser = new MessageParser<ChoosingTimeLimitroomsettingConfigure>(() => new ChoosingTimeLimitroomsettingConfigure());

	private UnknownFieldSet _unknownFields;

	public const int MapModeTypeFieldNumber = 1;

	private MapModeType mapModeType_;

	public const int HasMapFieldNumber = 2;

	private bool hasMap_;

	public const int HasChoosingTimeFieldNumber = 3;

	private bool hasChoosingTime_;

	public const int HasGameSpeedFieldNumber = 4;

	private bool hasGameSpeed_;

	public const int HasDifficultyFieldNumber = 5;

	private bool hasDifficulty_;

	public const int HasUpgradeFieldNumber = 6;

	private bool hasUpgrade_;

	public const int HasLabelFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_hasLabel_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> hasLabel_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChoosingTimeLimitroomsettingConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChoosingTimeLimitReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapModeType MapModeType
	{
		get
		{
			return mapModeType_;
		}
		private set
		{
			mapModeType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool HasMap
	{
		get
		{
			return hasMap_;
		}
		private set
		{
			hasMap_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool HasChoosingTime
	{
		get
		{
			return hasChoosingTime_;
		}
		private set
		{
			hasChoosingTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool HasGameSpeed
	{
		get
		{
			return hasGameSpeed_;
		}
		private set
		{
			hasGameSpeed_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool HasDifficulty
	{
		get
		{
			return hasDifficulty_;
		}
		private set
		{
			hasDifficulty_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool HasUpgrade
	{
		get
		{
			return hasUpgrade_;
		}
		private set
		{
			hasUpgrade_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> HasLabel => hasLabel_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitroomsettingConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitroomsettingConfigure(ChoosingTimeLimitroomsettingConfigure other)
		: this()
	{
		mapModeType_ = other.mapModeType_;
		hasMap_ = other.hasMap_;
		hasChoosingTime_ = other.hasChoosingTime_;
		hasGameSpeed_ = other.hasGameSpeed_;
		hasDifficulty_ = other.hasDifficulty_;
		hasUpgrade_ = other.hasUpgrade_;
		hasLabel_ = other.hasLabel_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitroomsettingConfigure Clone()
	{
		return new ChoosingTimeLimitroomsettingConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChoosingTimeLimitroomsettingConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChoosingTimeLimitroomsettingConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MapModeType != other.MapModeType)
		{
			return false;
		}
		if (HasMap != other.HasMap)
		{
			return false;
		}
		if (HasChoosingTime != other.HasChoosingTime)
		{
			return false;
		}
		if (HasGameSpeed != other.HasGameSpeed)
		{
			return false;
		}
		if (HasDifficulty != other.HasDifficulty)
		{
			return false;
		}
		if (HasUpgrade != other.HasUpgrade)
		{
			return false;
		}
		if (!hasLabel_.Equals(other.hasLabel_))
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
		if (MapModeType != MapModeType.None)
		{
			num ^= MapModeType.GetHashCode();
		}
		if (HasMap)
		{
			num ^= HasMap.GetHashCode();
		}
		if (HasChoosingTime)
		{
			num ^= HasChoosingTime.GetHashCode();
		}
		if (HasGameSpeed)
		{
			num ^= HasGameSpeed.GetHashCode();
		}
		if (HasDifficulty)
		{
			num ^= HasDifficulty.GetHashCode();
		}
		if (HasUpgrade)
		{
			num ^= HasUpgrade.GetHashCode();
		}
		num ^= hasLabel_.GetHashCode();
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
		if (MapModeType != MapModeType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)MapModeType);
		}
		if (HasMap)
		{
			output.WriteRawTag(16);
			output.WriteBool(HasMap);
		}
		if (HasChoosingTime)
		{
			output.WriteRawTag(24);
			output.WriteBool(HasChoosingTime);
		}
		if (HasGameSpeed)
		{
			output.WriteRawTag(32);
			output.WriteBool(HasGameSpeed);
		}
		if (HasDifficulty)
		{
			output.WriteRawTag(40);
			output.WriteBool(HasDifficulty);
		}
		if (HasUpgrade)
		{
			output.WriteRawTag(48);
			output.WriteBool(HasUpgrade);
		}
		hasLabel_.WriteTo(ref output, _repeated_hasLabel_codec);
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
		if (MapModeType != MapModeType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MapModeType);
		}
		if (HasMap)
		{
			num += 2;
		}
		if (HasChoosingTime)
		{
			num += 2;
		}
		if (HasGameSpeed)
		{
			num += 2;
		}
		if (HasDifficulty)
		{
			num += 2;
		}
		if (HasUpgrade)
		{
			num += 2;
		}
		num += hasLabel_.CalculateSize(_repeated_hasLabel_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChoosingTimeLimitroomsettingConfigure other)
	{
		if (other != null)
		{
			if (other.MapModeType != MapModeType.None)
			{
				MapModeType = other.MapModeType;
			}
			if (other.HasMap)
			{
				HasMap = other.HasMap;
			}
			if (other.HasChoosingTime)
			{
				HasChoosingTime = other.HasChoosingTime;
			}
			if (other.HasGameSpeed)
			{
				HasGameSpeed = other.HasGameSpeed;
			}
			if (other.HasDifficulty)
			{
				HasDifficulty = other.HasDifficulty;
			}
			if (other.HasUpgrade)
			{
				HasUpgrade = other.HasUpgrade;
			}
			hasLabel_.Add(other.hasLabel_);
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
				MapModeType = (MapModeType)input.ReadEnum();
				break;
			case 16u:
				HasMap = input.ReadBool();
				break;
			case 24u:
				HasChoosingTime = input.ReadBool();
				break;
			case 32u:
				HasGameSpeed = input.ReadBool();
				break;
			case 40u:
				HasDifficulty = input.ReadBool();
				break;
			case 48u:
				HasUpgrade = input.ReadBool();
				break;
			case 58u:
			case 61u:
				hasLabel_.AddEntriesFrom(ref input, _repeated_hasLabel_codec);
				break;
			}
		}
	}
}
