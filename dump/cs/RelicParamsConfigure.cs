using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class RelicParamsConfigure : IMessage<RelicParamsConfigure>, IMessage, IEquatable<RelicParamsConfigure>, IDeepCloneable<RelicParamsConfigure>, IBufferMessage
{
	private static readonly MessageParser<RelicParamsConfigure> _parser = new MessageParser<RelicParamsConfigure>(() => new RelicParamsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int RelicRecRoleNormalScoreFieldNumber = 2;

	private int relicRecRoleNormalScore_;

	public const int RelicRecRoleScoreFieldNumber = 3;

	private int relicRecRoleScore_;

	public const int RelicRecGrowthScoreFieldNumber = 4;

	private int relicRecGrowthScore_;

	public const int RelicRecTagTypeNdPtFieldNumber = 5;

	private int relicRecTagTypeNdPt_;

	public const int RelicRecTagTypeScoreFieldNumber = 6;

	private int relicRecTagTypeScore_;

	public const int RelicRecWordTypeScoreFieldNumber = 7;

	private int relicRecWordTypeScore_;

	public const int RelicRecBlueScoreFieldNumber = 8;

	private int relicRecBlueScore_;

	public const int RelicRecPurpleScoreFieldNumber = 9;

	private int relicRecPurpleScore_;

	public const int RelicRecOrangeScoreFieldNumber = 10;

	private int relicRecOrangeScore_;

	public const int RelicRecLimitScoreFieldNumber = 11;

	private int relicRecLimitScore_;

	public const int RelicRecSLimitScoreFieldNumber = 12;

	private int relicRecSLimitScore_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RelicParamsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RelicReflection.Descriptor.MessageTypes[1];

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
	public int RelicRecRoleNormalScore
	{
		get
		{
			return relicRecRoleNormalScore_;
		}
		private set
		{
			relicRecRoleNormalScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecRoleScore
	{
		get
		{
			return relicRecRoleScore_;
		}
		private set
		{
			relicRecRoleScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecGrowthScore
	{
		get
		{
			return relicRecGrowthScore_;
		}
		private set
		{
			relicRecGrowthScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecTagTypeNdPt
	{
		get
		{
			return relicRecTagTypeNdPt_;
		}
		private set
		{
			relicRecTagTypeNdPt_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecTagTypeScore
	{
		get
		{
			return relicRecTagTypeScore_;
		}
		private set
		{
			relicRecTagTypeScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecWordTypeScore
	{
		get
		{
			return relicRecWordTypeScore_;
		}
		private set
		{
			relicRecWordTypeScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecBlueScore
	{
		get
		{
			return relicRecBlueScore_;
		}
		private set
		{
			relicRecBlueScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecPurpleScore
	{
		get
		{
			return relicRecPurpleScore_;
		}
		private set
		{
			relicRecPurpleScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecOrangeScore
	{
		get
		{
			return relicRecOrangeScore_;
		}
		private set
		{
			relicRecOrangeScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecLimitScore
	{
		get
		{
			return relicRecLimitScore_;
		}
		private set
		{
			relicRecLimitScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecSLimitScore
	{
		get
		{
			return relicRecSLimitScore_;
		}
		private set
		{
			relicRecSLimitScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicParamsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicParamsConfigure(RelicParamsConfigure other)
		: this()
	{
		id_ = other.id_;
		relicRecRoleNormalScore_ = other.relicRecRoleNormalScore_;
		relicRecRoleScore_ = other.relicRecRoleScore_;
		relicRecGrowthScore_ = other.relicRecGrowthScore_;
		relicRecTagTypeNdPt_ = other.relicRecTagTypeNdPt_;
		relicRecTagTypeScore_ = other.relicRecTagTypeScore_;
		relicRecWordTypeScore_ = other.relicRecWordTypeScore_;
		relicRecBlueScore_ = other.relicRecBlueScore_;
		relicRecPurpleScore_ = other.relicRecPurpleScore_;
		relicRecOrangeScore_ = other.relicRecOrangeScore_;
		relicRecLimitScore_ = other.relicRecLimitScore_;
		relicRecSLimitScore_ = other.relicRecSLimitScore_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicParamsConfigure Clone()
	{
		return new RelicParamsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RelicParamsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RelicParamsConfigure other)
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
		if (RelicRecRoleNormalScore != other.RelicRecRoleNormalScore)
		{
			return false;
		}
		if (RelicRecRoleScore != other.RelicRecRoleScore)
		{
			return false;
		}
		if (RelicRecGrowthScore != other.RelicRecGrowthScore)
		{
			return false;
		}
		if (RelicRecTagTypeNdPt != other.RelicRecTagTypeNdPt)
		{
			return false;
		}
		if (RelicRecTagTypeScore != other.RelicRecTagTypeScore)
		{
			return false;
		}
		if (RelicRecWordTypeScore != other.RelicRecWordTypeScore)
		{
			return false;
		}
		if (RelicRecBlueScore != other.RelicRecBlueScore)
		{
			return false;
		}
		if (RelicRecPurpleScore != other.RelicRecPurpleScore)
		{
			return false;
		}
		if (RelicRecOrangeScore != other.RelicRecOrangeScore)
		{
			return false;
		}
		if (RelicRecLimitScore != other.RelicRecLimitScore)
		{
			return false;
		}
		if (RelicRecSLimitScore != other.RelicRecSLimitScore)
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
		if (RelicRecRoleNormalScore != 0)
		{
			num ^= RelicRecRoleNormalScore.GetHashCode();
		}
		if (RelicRecRoleScore != 0)
		{
			num ^= RelicRecRoleScore.GetHashCode();
		}
		if (RelicRecGrowthScore != 0)
		{
			num ^= RelicRecGrowthScore.GetHashCode();
		}
		if (RelicRecTagTypeNdPt != 0)
		{
			num ^= RelicRecTagTypeNdPt.GetHashCode();
		}
		if (RelicRecTagTypeScore != 0)
		{
			num ^= RelicRecTagTypeScore.GetHashCode();
		}
		if (RelicRecWordTypeScore != 0)
		{
			num ^= RelicRecWordTypeScore.GetHashCode();
		}
		if (RelicRecBlueScore != 0)
		{
			num ^= RelicRecBlueScore.GetHashCode();
		}
		if (RelicRecPurpleScore != 0)
		{
			num ^= RelicRecPurpleScore.GetHashCode();
		}
		if (RelicRecOrangeScore != 0)
		{
			num ^= RelicRecOrangeScore.GetHashCode();
		}
		if (RelicRecLimitScore != 0)
		{
			num ^= RelicRecLimitScore.GetHashCode();
		}
		if (RelicRecSLimitScore != 0)
		{
			num ^= RelicRecSLimitScore.GetHashCode();
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
		if (RelicRecRoleNormalScore != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(RelicRecRoleNormalScore);
		}
		if (RelicRecRoleScore != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(RelicRecRoleScore);
		}
		if (RelicRecGrowthScore != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(RelicRecGrowthScore);
		}
		if (RelicRecTagTypeNdPt != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(RelicRecTagTypeNdPt);
		}
		if (RelicRecTagTypeScore != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(RelicRecTagTypeScore);
		}
		if (RelicRecWordTypeScore != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(RelicRecWordTypeScore);
		}
		if (RelicRecBlueScore != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(RelicRecBlueScore);
		}
		if (RelicRecPurpleScore != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(RelicRecPurpleScore);
		}
		if (RelicRecOrangeScore != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(RelicRecOrangeScore);
		}
		if (RelicRecLimitScore != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(RelicRecLimitScore);
		}
		if (RelicRecSLimitScore != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(RelicRecSLimitScore);
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
		if (RelicRecRoleNormalScore != 0)
		{
			num += 5;
		}
		if (RelicRecRoleScore != 0)
		{
			num += 5;
		}
		if (RelicRecGrowthScore != 0)
		{
			num += 5;
		}
		if (RelicRecTagTypeNdPt != 0)
		{
			num += 5;
		}
		if (RelicRecTagTypeScore != 0)
		{
			num += 5;
		}
		if (RelicRecWordTypeScore != 0)
		{
			num += 5;
		}
		if (RelicRecBlueScore != 0)
		{
			num += 5;
		}
		if (RelicRecPurpleScore != 0)
		{
			num += 5;
		}
		if (RelicRecOrangeScore != 0)
		{
			num += 5;
		}
		if (RelicRecLimitScore != 0)
		{
			num += 5;
		}
		if (RelicRecSLimitScore != 0)
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
	public void MergeFrom(RelicParamsConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.RelicRecRoleNormalScore != 0)
			{
				RelicRecRoleNormalScore = other.RelicRecRoleNormalScore;
			}
			if (other.RelicRecRoleScore != 0)
			{
				RelicRecRoleScore = other.RelicRecRoleScore;
			}
			if (other.RelicRecGrowthScore != 0)
			{
				RelicRecGrowthScore = other.RelicRecGrowthScore;
			}
			if (other.RelicRecTagTypeNdPt != 0)
			{
				RelicRecTagTypeNdPt = other.RelicRecTagTypeNdPt;
			}
			if (other.RelicRecTagTypeScore != 0)
			{
				RelicRecTagTypeScore = other.RelicRecTagTypeScore;
			}
			if (other.RelicRecWordTypeScore != 0)
			{
				RelicRecWordTypeScore = other.RelicRecWordTypeScore;
			}
			if (other.RelicRecBlueScore != 0)
			{
				RelicRecBlueScore = other.RelicRecBlueScore;
			}
			if (other.RelicRecPurpleScore != 0)
			{
				RelicRecPurpleScore = other.RelicRecPurpleScore;
			}
			if (other.RelicRecOrangeScore != 0)
			{
				RelicRecOrangeScore = other.RelicRecOrangeScore;
			}
			if (other.RelicRecLimitScore != 0)
			{
				RelicRecLimitScore = other.RelicRecLimitScore;
			}
			if (other.RelicRecSLimitScore != 0)
			{
				RelicRecSLimitScore = other.RelicRecSLimitScore;
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
			case 21u:
				RelicRecRoleNormalScore = input.ReadSFixed32();
				break;
			case 29u:
				RelicRecRoleScore = input.ReadSFixed32();
				break;
			case 37u:
				RelicRecGrowthScore = input.ReadSFixed32();
				break;
			case 45u:
				RelicRecTagTypeNdPt = input.ReadSFixed32();
				break;
			case 53u:
				RelicRecTagTypeScore = input.ReadSFixed32();
				break;
			case 61u:
				RelicRecWordTypeScore = input.ReadSFixed32();
				break;
			case 69u:
				RelicRecBlueScore = input.ReadSFixed32();
				break;
			case 77u:
				RelicRecPurpleScore = input.ReadSFixed32();
				break;
			case 85u:
				RelicRecOrangeScore = input.ReadSFixed32();
				break;
			case 93u:
				RelicRecLimitScore = input.ReadSFixed32();
				break;
			case 101u:
				RelicRecSLimitScore = input.ReadSFixed32();
				break;
			}
		}
	}
}
