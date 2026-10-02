using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ComebackParamsConfigure : IMessage<ComebackParamsConfigure>, IMessage, IEquatable<ComebackParamsConfigure>, IDeepCloneable<ComebackParamsConfigure>, IBufferMessage
{
	private static readonly MessageParser<ComebackParamsConfigure> _parser = new MessageParser<ComebackParamsConfigure>(() => new ComebackParamsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ActivityIdFieldNumber = 2;

	private int activityId_;

	public const int ChestIdFieldNumber = 3;

	private int chestId_;

	public const int PveLevelFieldNumber = 4;

	private int pveLevel_;

	public const int HeroIDsFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_heroIDs_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> heroIDs_ = new RepeatedField<int>();

	public const int SignInIdFieldNumber = 6;

	private int signInId_;

	public const int SignInRechargeIdFieldNumber = 7;

	private int signInRechargeId_;

	public const int QuestionnaireIdFieldNumber = 8;

	private static readonly FieldCodec<string> _repeated_questionnaireId_codec = FieldCodec.ForString(66u);

	private readonly RepeatedField<string> questionnaireId_ = new RepeatedField<string>();

	public const int QuestionnaireRewardFieldNumber = 9;

	private static readonly MapField<int, int>.Codec _map_questionnaireReward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 74u);

	private readonly MapField<int, int> questionnaireReward_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ComebackParamsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ComebackReflection.Descriptor.MessageTypes[0];

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
	public int ActivityId
	{
		get
		{
			return activityId_;
		}
		private set
		{
			activityId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ChestId
	{
		get
		{
			return chestId_;
		}
		private set
		{
			chestId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PveLevel
	{
		get
		{
			return pveLevel_;
		}
		private set
		{
			pveLevel_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> HeroIDs => heroIDs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SignInId
	{
		get
		{
			return signInId_;
		}
		private set
		{
			signInId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SignInRechargeId
	{
		get
		{
			return signInRechargeId_;
		}
		private set
		{
			signInRechargeId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> QuestionnaireId => questionnaireId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> QuestionnaireReward => questionnaireReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ComebackParamsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ComebackParamsConfigure(ComebackParamsConfigure other)
		: this()
	{
		id_ = other.id_;
		activityId_ = other.activityId_;
		chestId_ = other.chestId_;
		pveLevel_ = other.pveLevel_;
		heroIDs_ = other.heroIDs_.Clone();
		signInId_ = other.signInId_;
		signInRechargeId_ = other.signInRechargeId_;
		questionnaireId_ = other.questionnaireId_.Clone();
		questionnaireReward_ = other.questionnaireReward_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ComebackParamsConfigure Clone()
	{
		return new ComebackParamsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ComebackParamsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ComebackParamsConfigure other)
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
		if (ActivityId != other.ActivityId)
		{
			return false;
		}
		if (ChestId != other.ChestId)
		{
			return false;
		}
		if (PveLevel != other.PveLevel)
		{
			return false;
		}
		if (!heroIDs_.Equals(other.heroIDs_))
		{
			return false;
		}
		if (SignInId != other.SignInId)
		{
			return false;
		}
		if (SignInRechargeId != other.SignInRechargeId)
		{
			return false;
		}
		if (!questionnaireId_.Equals(other.questionnaireId_))
		{
			return false;
		}
		if (!QuestionnaireReward.Equals(other.QuestionnaireReward))
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
		if (ActivityId != 0)
		{
			num ^= ActivityId.GetHashCode();
		}
		if (ChestId != 0)
		{
			num ^= ChestId.GetHashCode();
		}
		if (PveLevel != 0)
		{
			num ^= PveLevel.GetHashCode();
		}
		num ^= heroIDs_.GetHashCode();
		if (SignInId != 0)
		{
			num ^= SignInId.GetHashCode();
		}
		if (SignInRechargeId != 0)
		{
			num ^= SignInRechargeId.GetHashCode();
		}
		num ^= questionnaireId_.GetHashCode();
		num ^= QuestionnaireReward.GetHashCode();
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
		if (ActivityId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ActivityId);
		}
		if (ChestId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ChestId);
		}
		if (PveLevel != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(PveLevel);
		}
		heroIDs_.WriteTo(ref output, _repeated_heroIDs_codec);
		if (SignInId != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(SignInId);
		}
		if (SignInRechargeId != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(SignInRechargeId);
		}
		questionnaireId_.WriteTo(ref output, _repeated_questionnaireId_codec);
		questionnaireReward_.WriteTo(ref output, _map_questionnaireReward_codec);
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
		if (ActivityId != 0)
		{
			num += 5;
		}
		if (ChestId != 0)
		{
			num += 5;
		}
		if (PveLevel != 0)
		{
			num += 5;
		}
		num += heroIDs_.CalculateSize(_repeated_heroIDs_codec);
		if (SignInId != 0)
		{
			num += 5;
		}
		if (SignInRechargeId != 0)
		{
			num += 5;
		}
		num += questionnaireId_.CalculateSize(_repeated_questionnaireId_codec);
		num += questionnaireReward_.CalculateSize(_map_questionnaireReward_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ComebackParamsConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.ActivityId != 0)
			{
				ActivityId = other.ActivityId;
			}
			if (other.ChestId != 0)
			{
				ChestId = other.ChestId;
			}
			if (other.PveLevel != 0)
			{
				PveLevel = other.PveLevel;
			}
			heroIDs_.Add(other.heroIDs_);
			if (other.SignInId != 0)
			{
				SignInId = other.SignInId;
			}
			if (other.SignInRechargeId != 0)
			{
				SignInRechargeId = other.SignInRechargeId;
			}
			questionnaireId_.Add(other.questionnaireId_);
			questionnaireReward_.MergeFrom(other.questionnaireReward_);
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
				ActivityId = input.ReadSFixed32();
				break;
			case 29u:
				ChestId = input.ReadSFixed32();
				break;
			case 37u:
				PveLevel = input.ReadSFixed32();
				break;
			case 42u:
			case 45u:
				heroIDs_.AddEntriesFrom(ref input, _repeated_heroIDs_codec);
				break;
			case 53u:
				SignInId = input.ReadSFixed32();
				break;
			case 61u:
				SignInRechargeId = input.ReadSFixed32();
				break;
			case 66u:
				questionnaireId_.AddEntriesFrom(ref input, _repeated_questionnaireId_codec);
				break;
			case 74u:
				questionnaireReward_.AddEntriesFrom(ref input, _map_questionnaireReward_codec);
				break;
			}
		}
	}
}
