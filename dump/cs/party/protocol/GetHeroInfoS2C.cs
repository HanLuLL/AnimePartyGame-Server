using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class GetHeroInfoS2C : IMessage<GetHeroInfoS2C>, IMessage, IEquatable<GetHeroInfoS2C>, IDeepCloneable<GetHeroInfoS2C>, IBufferMessage
{
	private static readonly MessageParser<GetHeroInfoS2C> _parser = new MessageParser<GetHeroInfoS2C>(() => new GetHeroInfoS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int KillCountFieldNumber = 2;

	private int killCount_;

	public const int TotalDieFieldNumber = 3;

	private int totalDie_;

	public const int TotalDamageFieldNumber = 4;

	private int totalDamage_;

	public const int TotalInjuredFieldNumber = 5;

	private int totalInjured_;

	public const int TreatmentScoreFieldNumber = 6;

	private int treatmentScore_;

	public const int CdFieldNumber = 7;

	private int cd_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GetHeroInfoS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[322];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int KillCount
	{
		get
		{
			return killCount_;
		}
		set
		{
			killCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TotalDie
	{
		get
		{
			return totalDie_;
		}
		set
		{
			totalDie_ = value;
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
	public int TotalInjured
	{
		get
		{
			return totalInjured_;
		}
		set
		{
			totalInjured_ = value;
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
	public int Cd
	{
		get
		{
			return cd_;
		}
		set
		{
			cd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetHeroInfoS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetHeroInfoS2C(GetHeroInfoS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		killCount_ = other.killCount_;
		totalDie_ = other.totalDie_;
		totalDamage_ = other.totalDamage_;
		totalInjured_ = other.totalInjured_;
		treatmentScore_ = other.treatmentScore_;
		cd_ = other.cd_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetHeroInfoS2C Clone()
	{
		return new GetHeroInfoS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GetHeroInfoS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GetHeroInfoS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (KillCount != other.KillCount)
		{
			return false;
		}
		if (TotalDie != other.TotalDie)
		{
			return false;
		}
		if (TotalDamage != other.TotalDamage)
		{
			return false;
		}
		if (TotalInjured != other.TotalInjured)
		{
			return false;
		}
		if (TreatmentScore != other.TreatmentScore)
		{
			return false;
		}
		if (Cd != other.Cd)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (KillCount != 0)
		{
			num ^= KillCount.GetHashCode();
		}
		if (TotalDie != 0)
		{
			num ^= TotalDie.GetHashCode();
		}
		if (TotalDamage != 0)
		{
			num ^= TotalDamage.GetHashCode();
		}
		if (TotalInjured != 0)
		{
			num ^= TotalInjured.GetHashCode();
		}
		if (TreatmentScore != 0)
		{
			num ^= TreatmentScore.GetHashCode();
		}
		if (Cd != 0)
		{
			num ^= Cd.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (KillCount != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(KillCount);
		}
		if (TotalDie != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(TotalDie);
		}
		if (TotalDamage != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(TotalDamage);
		}
		if (TotalInjured != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(TotalInjured);
		}
		if (TreatmentScore != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(TreatmentScore);
		}
		if (Cd != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Cd);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (KillCount != 0)
		{
			num += 5;
		}
		if (TotalDie != 0)
		{
			num += 5;
		}
		if (TotalDamage != 0)
		{
			num += 5;
		}
		if (TotalInjured != 0)
		{
			num += 5;
		}
		if (TreatmentScore != 0)
		{
			num += 5;
		}
		if (Cd != 0)
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
	public void MergeFrom(GetHeroInfoS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.KillCount != 0)
			{
				KillCount = other.KillCount;
			}
			if (other.TotalDie != 0)
			{
				TotalDie = other.TotalDie;
			}
			if (other.TotalDamage != 0)
			{
				TotalDamage = other.TotalDamage;
			}
			if (other.TotalInjured != 0)
			{
				TotalInjured = other.TotalInjured;
			}
			if (other.TreatmentScore != 0)
			{
				TreatmentScore = other.TreatmentScore;
			}
			if (other.Cd != 0)
			{
				Cd = other.Cd;
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 21u:
				KillCount = input.ReadSFixed32();
				break;
			case 29u:
				TotalDie = input.ReadSFixed32();
				break;
			case 37u:
				TotalDamage = input.ReadSFixed32();
				break;
			case 45u:
				TotalInjured = input.ReadSFixed32();
				break;
			case 53u:
				TreatmentScore = input.ReadSFixed32();
				break;
			case 61u:
				Cd = input.ReadSFixed32();
				break;
			}
		}
	}
}
