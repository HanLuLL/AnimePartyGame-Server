using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class CreditInfo : IMessage<CreditInfo>, IMessage, IEquatable<CreditInfo>, IDeepCloneable<CreditInfo>, IBufferMessage
{
	private static readonly MessageParser<CreditInfo> _parser = new MessageParser<CreditInfo>(() => new CreditInfo());

	private UnknownFieldSet _unknownFields;

	public const int DeductScoreFieldNumber = 1;

	private int deductScore_;

	public const int LastViolationTimeFieldNumber = 2;

	private long lastViolationTime_;

	public const int MatchPunishmentTimeFieldNumber = 3;

	private long matchPunishmentTime_;

	public const int LastExemptedTimeFieldNumber = 4;

	private long lastExemptedTime_;

	public const int LastExemptedActionTypeFieldNumber = 5;

	private int lastExemptedActionType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CreditInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[33];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DeductScore
	{
		get
		{
			return deductScore_;
		}
		set
		{
			deductScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastViolationTime
	{
		get
		{
			return lastViolationTime_;
		}
		set
		{
			lastViolationTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long MatchPunishmentTime
	{
		get
		{
			return matchPunishmentTime_;
		}
		set
		{
			matchPunishmentTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastExemptedTime
	{
		get
		{
			return lastExemptedTime_;
		}
		set
		{
			lastExemptedTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LastExemptedActionType
	{
		get
		{
			return lastExemptedActionType_;
		}
		set
		{
			lastExemptedActionType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreditInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreditInfo(CreditInfo other)
		: this()
	{
		deductScore_ = other.deductScore_;
		lastViolationTime_ = other.lastViolationTime_;
		matchPunishmentTime_ = other.matchPunishmentTime_;
		lastExemptedTime_ = other.lastExemptedTime_;
		lastExemptedActionType_ = other.lastExemptedActionType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreditInfo Clone()
	{
		return new CreditInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CreditInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CreditInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DeductScore != other.DeductScore)
		{
			return false;
		}
		if (LastViolationTime != other.LastViolationTime)
		{
			return false;
		}
		if (MatchPunishmentTime != other.MatchPunishmentTime)
		{
			return false;
		}
		if (LastExemptedTime != other.LastExemptedTime)
		{
			return false;
		}
		if (LastExemptedActionType != other.LastExemptedActionType)
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
		if (DeductScore != 0)
		{
			num ^= DeductScore.GetHashCode();
		}
		if (LastViolationTime != 0L)
		{
			num ^= LastViolationTime.GetHashCode();
		}
		if (MatchPunishmentTime != 0L)
		{
			num ^= MatchPunishmentTime.GetHashCode();
		}
		if (LastExemptedTime != 0L)
		{
			num ^= LastExemptedTime.GetHashCode();
		}
		if (LastExemptedActionType != 0)
		{
			num ^= LastExemptedActionType.GetHashCode();
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
		if (DeductScore != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(DeductScore);
		}
		if (LastViolationTime != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(LastViolationTime);
		}
		if (MatchPunishmentTime != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(MatchPunishmentTime);
		}
		if (LastExemptedTime != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(LastExemptedTime);
		}
		if (LastExemptedActionType != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(LastExemptedActionType);
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
		if (DeductScore != 0)
		{
			num += 5;
		}
		if (LastViolationTime != 0L)
		{
			num += 9;
		}
		if (MatchPunishmentTime != 0L)
		{
			num += 9;
		}
		if (LastExemptedTime != 0L)
		{
			num += 9;
		}
		if (LastExemptedActionType != 0)
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
	public void MergeFrom(CreditInfo other)
	{
		if (other != null)
		{
			if (other.DeductScore != 0)
			{
				DeductScore = other.DeductScore;
			}
			if (other.LastViolationTime != 0L)
			{
				LastViolationTime = other.LastViolationTime;
			}
			if (other.MatchPunishmentTime != 0L)
			{
				MatchPunishmentTime = other.MatchPunishmentTime;
			}
			if (other.LastExemptedTime != 0L)
			{
				LastExemptedTime = other.LastExemptedTime;
			}
			if (other.LastExemptedActionType != 0)
			{
				LastExemptedActionType = other.LastExemptedActionType;
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
				DeductScore = input.ReadSFixed32();
				break;
			case 17u:
				LastViolationTime = input.ReadSFixed64();
				break;
			case 25u:
				MatchPunishmentTime = input.ReadSFixed64();
				break;
			case 33u:
				LastExemptedTime = input.ReadSFixed64();
				break;
			case 45u:
				LastExemptedActionType = input.ReadSFixed32();
				break;
			}
		}
	}
}
