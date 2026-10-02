using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class MatchCreditTierConfigure : IMessage<MatchCreditTierConfigure>, IMessage, IEquatable<MatchCreditTierConfigure>, IDeepCloneable<MatchCreditTierConfigure>, IBufferMessage
{
	private static readonly MessageParser<MatchCreditTierConfigure> _parser = new MessageParser<MatchCreditTierConfigure>(() => new MatchCreditTierConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int MinScoreFieldNumber = 2;

	private int minScore_;

	public const int MaxScoreFieldNumber = 3;

	private int maxScore_;

	public const int TierNameFieldNumber = 4;

	private int tierName_;

	public const int MatchCdSecondsFieldNumber = 5;

	private int matchCdSeconds_;

	public const int RewardRatioFieldNumber = 6;

	private int rewardRatio_;

	public const int TierDescFieldNumber = 7;

	private int tierDesc_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MatchCreditTierConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MatchReflection.Descriptor.MessageTypes[2];

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
	public int MinScore
	{
		get
		{
			return minScore_;
		}
		private set
		{
			minScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxScore
	{
		get
		{
			return maxScore_;
		}
		private set
		{
			maxScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TierName
	{
		get
		{
			return tierName_;
		}
		private set
		{
			tierName_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MatchCdSeconds
	{
		get
		{
			return matchCdSeconds_;
		}
		private set
		{
			matchCdSeconds_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RewardRatio
	{
		get
		{
			return rewardRatio_;
		}
		private set
		{
			rewardRatio_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TierDesc
	{
		get
		{
			return tierDesc_;
		}
		private set
		{
			tierDesc_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchCreditTierConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchCreditTierConfigure(MatchCreditTierConfigure other)
		: this()
	{
		id_ = other.id_;
		minScore_ = other.minScore_;
		maxScore_ = other.maxScore_;
		tierName_ = other.tierName_;
		matchCdSeconds_ = other.matchCdSeconds_;
		rewardRatio_ = other.rewardRatio_;
		tierDesc_ = other.tierDesc_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchCreditTierConfigure Clone()
	{
		return new MatchCreditTierConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MatchCreditTierConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MatchCreditTierConfigure other)
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
		if (MinScore != other.MinScore)
		{
			return false;
		}
		if (MaxScore != other.MaxScore)
		{
			return false;
		}
		if (TierName != other.TierName)
		{
			return false;
		}
		if (MatchCdSeconds != other.MatchCdSeconds)
		{
			return false;
		}
		if (RewardRatio != other.RewardRatio)
		{
			return false;
		}
		if (TierDesc != other.TierDesc)
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
		if (MinScore != 0)
		{
			num ^= MinScore.GetHashCode();
		}
		if (MaxScore != 0)
		{
			num ^= MaxScore.GetHashCode();
		}
		if (TierName != 0)
		{
			num ^= TierName.GetHashCode();
		}
		if (MatchCdSeconds != 0)
		{
			num ^= MatchCdSeconds.GetHashCode();
		}
		if (RewardRatio != 0)
		{
			num ^= RewardRatio.GetHashCode();
		}
		if (TierDesc != 0)
		{
			num ^= TierDesc.GetHashCode();
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
		if (MinScore != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MinScore);
		}
		if (MaxScore != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(MaxScore);
		}
		if (TierName != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(TierName);
		}
		if (MatchCdSeconds != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(MatchCdSeconds);
		}
		if (RewardRatio != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(RewardRatio);
		}
		if (TierDesc != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(TierDesc);
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
		if (MinScore != 0)
		{
			num += 5;
		}
		if (MaxScore != 0)
		{
			num += 5;
		}
		if (TierName != 0)
		{
			num += 5;
		}
		if (MatchCdSeconds != 0)
		{
			num += 5;
		}
		if (RewardRatio != 0)
		{
			num += 5;
		}
		if (TierDesc != 0)
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
	public void MergeFrom(MatchCreditTierConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.MinScore != 0)
			{
				MinScore = other.MinScore;
			}
			if (other.MaxScore != 0)
			{
				MaxScore = other.MaxScore;
			}
			if (other.TierName != 0)
			{
				TierName = other.TierName;
			}
			if (other.MatchCdSeconds != 0)
			{
				MatchCdSeconds = other.MatchCdSeconds;
			}
			if (other.RewardRatio != 0)
			{
				RewardRatio = other.RewardRatio;
			}
			if (other.TierDesc != 0)
			{
				TierDesc = other.TierDesc;
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
				MinScore = input.ReadSFixed32();
				break;
			case 29u:
				MaxScore = input.ReadSFixed32();
				break;
			case 37u:
				TierName = input.ReadSFixed32();
				break;
			case 45u:
				MatchCdSeconds = input.ReadSFixed32();
				break;
			case 53u:
				RewardRatio = input.ReadSFixed32();
				break;
			case 61u:
				TierDesc = input.ReadSFixed32();
				break;
			}
		}
	}
}
