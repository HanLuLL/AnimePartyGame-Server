using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ChoosingTimeLimitgamespeedConfigure : IMessage<ChoosingTimeLimitgamespeedConfigure>, IMessage, IEquatable<ChoosingTimeLimitgamespeedConfigure>, IDeepCloneable<ChoosingTimeLimitgamespeedConfigure>, IBufferMessage
{
	private static readonly MessageParser<ChoosingTimeLimitgamespeedConfigure> _parser = new MessageParser<ChoosingTimeLimitgamespeedConfigure>(() => new ChoosingTimeLimitgamespeedConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GameSpeedTypeFieldNumber = 1;

	private GameSpeedType gameSpeedType_;

	public const int IsDefaultFieldNumber = 2;

	private bool isDefault_;

	public const int DescriptionIDFieldNumber = 3;

	private int descriptionID_;

	public const int AnimSpeedFieldNumber = 4;

	private float animSpeed_;

	public const int DiceSpeedFieldNumber = 5;

	private float diceSpeed_;

	public const int PerformSpeedFieldNumber = 6;

	private float performSpeed_;

	public const int VfxSpeedFieldNumber = 7;

	private float vfxSpeed_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChoosingTimeLimitgamespeedConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChoosingTimeLimitReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameSpeedType GameSpeedType
	{
		get
		{
			return gameSpeedType_;
		}
		private set
		{
			gameSpeedType_ = value;
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
	public float AnimSpeed
	{
		get
		{
			return animSpeed_;
		}
		private set
		{
			animSpeed_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float DiceSpeed
	{
		get
		{
			return diceSpeed_;
		}
		private set
		{
			diceSpeed_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float PerformSpeed
	{
		get
		{
			return performSpeed_;
		}
		private set
		{
			performSpeed_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float VfxSpeed
	{
		get
		{
			return vfxSpeed_;
		}
		private set
		{
			vfxSpeed_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitgamespeedConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitgamespeedConfigure(ChoosingTimeLimitgamespeedConfigure other)
		: this()
	{
		gameSpeedType_ = other.gameSpeedType_;
		isDefault_ = other.isDefault_;
		descriptionID_ = other.descriptionID_;
		animSpeed_ = other.animSpeed_;
		diceSpeed_ = other.diceSpeed_;
		performSpeed_ = other.performSpeed_;
		vfxSpeed_ = other.vfxSpeed_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitgamespeedConfigure Clone()
	{
		return new ChoosingTimeLimitgamespeedConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChoosingTimeLimitgamespeedConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChoosingTimeLimitgamespeedConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GameSpeedType != other.GameSpeedType)
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
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(AnimSpeed, other.AnimSpeed))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(DiceSpeed, other.DiceSpeed))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(PerformSpeed, other.PerformSpeed))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(VfxSpeed, other.VfxSpeed))
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
		if (GameSpeedType != GameSpeedType.None)
		{
			num ^= GameSpeedType.GetHashCode();
		}
		if (IsDefault)
		{
			num ^= IsDefault.GetHashCode();
		}
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
		}
		if (AnimSpeed != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(AnimSpeed);
		}
		if (DiceSpeed != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(DiceSpeed);
		}
		if (PerformSpeed != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(PerformSpeed);
		}
		if (VfxSpeed != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(VfxSpeed);
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
		if (GameSpeedType != GameSpeedType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)GameSpeedType);
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
		if (AnimSpeed != 0f)
		{
			output.WriteRawTag(37);
			output.WriteFloat(AnimSpeed);
		}
		if (DiceSpeed != 0f)
		{
			output.WriteRawTag(45);
			output.WriteFloat(DiceSpeed);
		}
		if (PerformSpeed != 0f)
		{
			output.WriteRawTag(53);
			output.WriteFloat(PerformSpeed);
		}
		if (VfxSpeed != 0f)
		{
			output.WriteRawTag(61);
			output.WriteFloat(VfxSpeed);
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
		if (GameSpeedType != GameSpeedType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GameSpeedType);
		}
		if (IsDefault)
		{
			num += 2;
		}
		if (DescriptionID != 0)
		{
			num += 5;
		}
		if (AnimSpeed != 0f)
		{
			num += 5;
		}
		if (DiceSpeed != 0f)
		{
			num += 5;
		}
		if (PerformSpeed != 0f)
		{
			num += 5;
		}
		if (VfxSpeed != 0f)
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
	public void MergeFrom(ChoosingTimeLimitgamespeedConfigure other)
	{
		if (other != null)
		{
			if (other.GameSpeedType != GameSpeedType.None)
			{
				GameSpeedType = other.GameSpeedType;
			}
			if (other.IsDefault)
			{
				IsDefault = other.IsDefault;
			}
			if (other.DescriptionID != 0)
			{
				DescriptionID = other.DescriptionID;
			}
			if (other.AnimSpeed != 0f)
			{
				AnimSpeed = other.AnimSpeed;
			}
			if (other.DiceSpeed != 0f)
			{
				DiceSpeed = other.DiceSpeed;
			}
			if (other.PerformSpeed != 0f)
			{
				PerformSpeed = other.PerformSpeed;
			}
			if (other.VfxSpeed != 0f)
			{
				VfxSpeed = other.VfxSpeed;
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
				GameSpeedType = (GameSpeedType)input.ReadEnum();
				break;
			case 16u:
				IsDefault = input.ReadBool();
				break;
			case 29u:
				DescriptionID = input.ReadSFixed32();
				break;
			case 37u:
				AnimSpeed = input.ReadFloat();
				break;
			case 45u:
				DiceSpeed = input.ReadFloat();
				break;
			case 53u:
				PerformSpeed = input.ReadFloat();
				break;
			case 61u:
				VfxSpeed = input.ReadFloat();
				break;
			}
		}
	}
}
