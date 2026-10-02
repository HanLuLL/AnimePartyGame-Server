using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class GameStats : IMessage<GameStats>, IMessage, IEquatable<GameStats>, IDeepCloneable<GameStats>, IBufferMessage
{
	private static readonly MessageParser<GameStats> _parser = new MessageParser<GameStats>(() => new GameStats());

	private UnknownFieldSet _unknownFields;

	public const int MaxSingleDamageFieldNumber = 1;

	private int maxSingleDamage_;

	public const int MaxTotalDamageFieldNumber = 2;

	private int maxTotalDamage_;

	public const int MaxGetGoldFieldNumber = 3;

	private int maxGetGold_;

	public const int TreatmentScoreFieldNumber = 4;

	private int treatmentScore_;

	public const int TotalTransferGoldFieldNumber = 5;

	private int totalTransferGold_;

	public const int TotalDamageFieldNumber = 6;

	private int totalDamage_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GameStats> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[7];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxSingleDamage
	{
		get
		{
			return maxSingleDamage_;
		}
		set
		{
			maxSingleDamage_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxTotalDamage
	{
		get
		{
			return maxTotalDamage_;
		}
		set
		{
			maxTotalDamage_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxGetGold
	{
		get
		{
			return maxGetGold_;
		}
		set
		{
			maxGetGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TreatmentScore
	{
		get
		{
			return treatmentScore_;
		}
		set
		{
			treatmentScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TotalTransferGold
	{
		get
		{
			return totalTransferGold_;
		}
		set
		{
			totalTransferGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TotalDamage
	{
		get
		{
			return totalDamage_;
		}
		set
		{
			totalDamage_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameStats()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameStats(GameStats other)
		: this()
	{
		maxSingleDamage_ = other.maxSingleDamage_;
		maxTotalDamage_ = other.maxTotalDamage_;
		maxGetGold_ = other.maxGetGold_;
		treatmentScore_ = other.treatmentScore_;
		totalTransferGold_ = other.totalTransferGold_;
		totalDamage_ = other.totalDamage_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameStats Clone()
	{
		return new GameStats(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GameStats);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GameStats other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MaxSingleDamage != other.MaxSingleDamage)
		{
			return false;
		}
		if (MaxTotalDamage != other.MaxTotalDamage)
		{
			return false;
		}
		if (MaxGetGold != other.MaxGetGold)
		{
			return false;
		}
		if (TreatmentScore != other.TreatmentScore)
		{
			return false;
		}
		if (TotalTransferGold != other.TotalTransferGold)
		{
			return false;
		}
		if (TotalDamage != other.TotalDamage)
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
		if (MaxSingleDamage != 0)
		{
			num ^= MaxSingleDamage.GetHashCode();
		}
		if (MaxTotalDamage != 0)
		{
			num ^= MaxTotalDamage.GetHashCode();
		}
		if (MaxGetGold != 0)
		{
			num ^= MaxGetGold.GetHashCode();
		}
		if (TreatmentScore != 0)
		{
			num ^= TreatmentScore.GetHashCode();
		}
		if (TotalTransferGold != 0)
		{
			num ^= TotalTransferGold.GetHashCode();
		}
		if (TotalDamage != 0)
		{
			num ^= TotalDamage.GetHashCode();
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
		if (MaxSingleDamage != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(MaxSingleDamage);
		}
		if (MaxTotalDamage != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MaxTotalDamage);
		}
		if (MaxGetGold != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(MaxGetGold);
		}
		if (TreatmentScore != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(TreatmentScore);
		}
		if (TotalTransferGold != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(TotalTransferGold);
		}
		if (TotalDamage != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(TotalDamage);
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
		if (MaxSingleDamage != 0)
		{
			num += 5;
		}
		if (MaxTotalDamage != 0)
		{
			num += 5;
		}
		if (MaxGetGold != 0)
		{
			num += 5;
		}
		if (TreatmentScore != 0)
		{
			num += 5;
		}
		if (TotalTransferGold != 0)
		{
			num += 5;
		}
		if (TotalDamage != 0)
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
	public void MergeFrom(GameStats other)
	{
		if (other != null)
		{
			if (other.MaxSingleDamage != 0)
			{
				MaxSingleDamage = other.MaxSingleDamage;
			}
			if (other.MaxTotalDamage != 0)
			{
				MaxTotalDamage = other.MaxTotalDamage;
			}
			if (other.MaxGetGold != 0)
			{
				MaxGetGold = other.MaxGetGold;
			}
			if (other.TreatmentScore != 0)
			{
				TreatmentScore = other.TreatmentScore;
			}
			if (other.TotalTransferGold != 0)
			{
				TotalTransferGold = other.TotalTransferGold;
			}
			if (other.TotalDamage != 0)
			{
				TotalDamage = other.TotalDamage;
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
				MaxSingleDamage = input.ReadSFixed32();
				break;
			case 21u:
				MaxTotalDamage = input.ReadSFixed32();
				break;
			case 29u:
				MaxGetGold = input.ReadSFixed32();
				break;
			case 37u:
				TreatmentScore = input.ReadSFixed32();
				break;
			case 45u:
				TotalTransferGold = input.ReadSFixed32();
				break;
			case 53u:
				TotalDamage = input.ReadSFixed32();
				break;
			}
		}
	}
}
