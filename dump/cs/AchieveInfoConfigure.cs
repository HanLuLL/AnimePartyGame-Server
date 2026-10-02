using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class AchieveInfoConfigure : IMessage<AchieveInfoConfigure>, IMessage, IEquatable<AchieveInfoConfigure>, IDeepCloneable<AchieveInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<AchieveInfoConfigure> _parser = new MessageParser<AchieveInfoConfigure>(() => new AchieveInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int FinishAchieveTypeFieldNumber = 1;

	private FinishAchieveType finishAchieveType_;

	public const int IspositiveFieldNumber = 2;

	private bool ispositive_;

	public const int AchievePicSettlementFieldNumber = 3;

	private string achievePicSettlement_ = "";

	public const int AchievedescIdFieldNumber = 4;

	private int achievedescId_;

	public const int DescIdFieldNumber = 5;

	private int descId_;

	public const int WeightFieldNumber = 6;

	private int weight_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AchieveInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AchieveReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FinishAchieveType FinishAchieveType
	{
		get
		{
			return finishAchieveType_;
		}
		private set
		{
			finishAchieveType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Ispositive
	{
		get
		{
			return ispositive_;
		}
		private set
		{
			ispositive_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AchievePicSettlement
	{
		get
		{
			return achievePicSettlement_;
		}
		private set
		{
			achievePicSettlement_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AchievedescId
	{
		get
		{
			return achievedescId_;
		}
		private set
		{
			achievedescId_ = value;
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
	public int Weight
	{
		get
		{
			return weight_;
		}
		private set
		{
			weight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AchieveInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AchieveInfoConfigure(AchieveInfoConfigure other)
		: this()
	{
		finishAchieveType_ = other.finishAchieveType_;
		ispositive_ = other.ispositive_;
		achievePicSettlement_ = other.achievePicSettlement_;
		achievedescId_ = other.achievedescId_;
		descId_ = other.descId_;
		weight_ = other.weight_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AchieveInfoConfigure Clone()
	{
		return new AchieveInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AchieveInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AchieveInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (FinishAchieveType != other.FinishAchieveType)
		{
			return false;
		}
		if (Ispositive != other.Ispositive)
		{
			return false;
		}
		if (AchievePicSettlement != other.AchievePicSettlement)
		{
			return false;
		}
		if (AchievedescId != other.AchievedescId)
		{
			return false;
		}
		if (DescId != other.DescId)
		{
			return false;
		}
		if (Weight != other.Weight)
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
		if (FinishAchieveType != FinishAchieveType.None)
		{
			num ^= FinishAchieveType.GetHashCode();
		}
		if (Ispositive)
		{
			num ^= Ispositive.GetHashCode();
		}
		if (AchievePicSettlement.Length != 0)
		{
			num ^= AchievePicSettlement.GetHashCode();
		}
		if (AchievedescId != 0)
		{
			num ^= AchievedescId.GetHashCode();
		}
		if (DescId != 0)
		{
			num ^= DescId.GetHashCode();
		}
		if (Weight != 0)
		{
			num ^= Weight.GetHashCode();
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
		if (FinishAchieveType != FinishAchieveType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)FinishAchieveType);
		}
		if (Ispositive)
		{
			output.WriteRawTag(16);
			output.WriteBool(Ispositive);
		}
		if (AchievePicSettlement.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(AchievePicSettlement);
		}
		if (AchievedescId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(AchievedescId);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DescId);
		}
		if (Weight != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Weight);
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
		if (FinishAchieveType != FinishAchieveType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)FinishAchieveType);
		}
		if (Ispositive)
		{
			num += 2;
		}
		if (AchievePicSettlement.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AchievePicSettlement);
		}
		if (AchievedescId != 0)
		{
			num += 5;
		}
		if (DescId != 0)
		{
			num += 5;
		}
		if (Weight != 0)
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
	public void MergeFrom(AchieveInfoConfigure other)
	{
		if (other != null)
		{
			if (other.FinishAchieveType != FinishAchieveType.None)
			{
				FinishAchieveType = other.FinishAchieveType;
			}
			if (other.Ispositive)
			{
				Ispositive = other.Ispositive;
			}
			if (other.AchievePicSettlement.Length != 0)
			{
				AchievePicSettlement = other.AchievePicSettlement;
			}
			if (other.AchievedescId != 0)
			{
				AchievedescId = other.AchievedescId;
			}
			if (other.DescId != 0)
			{
				DescId = other.DescId;
			}
			if (other.Weight != 0)
			{
				Weight = other.Weight;
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
				FinishAchieveType = (FinishAchieveType)input.ReadEnum();
				break;
			case 16u:
				Ispositive = input.ReadBool();
				break;
			case 26u:
				AchievePicSettlement = input.ReadString();
				break;
			case 37u:
				AchievedescId = input.ReadSFixed32();
				break;
			case 45u:
				DescId = input.ReadSFixed32();
				break;
			case 53u:
				Weight = input.ReadSFixed32();
				break;
			}
		}
	}
}
