using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class UseEffectCardC2S : IMessage<UseEffectCardC2S>, IMessage, IEquatable<UseEffectCardC2S>, IDeepCloneable<UseEffectCardC2S>, IBufferMessage
{
	private static readonly MessageParser<UseEffectCardC2S> _parser = new MessageParser<UseEffectCardC2S>(() => new UseEffectCardC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int CanUseCardIdsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_canUseCardIds_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> canUseCardIds_ = new RepeatedField<int>();

	public const int CardIdFieldNumber = 3;

	private int cardId_;

	public const int TargetIdsFieldNumber = 5;

	private static readonly FieldCodec<long> _repeated_targetIds_codec = FieldCodec.ForSFixed64(42u);

	private readonly RepeatedField<long> targetIds_ = new RepeatedField<long>();

	public const int TargetNodeIdsFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_targetNodeIds_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> targetNodeIds_ = new RepeatedField<int>();

	public const int DevPointFieldNumber = 7;

	private int devPoint_;

	public const int UseSkillFieldNumber = 8;

	private bool useSkill_;

	public const int SkillIdFieldNumber = 9;

	private int skillId_;

	public const int NotUseSkillFieldNumber = 10;

	private bool notUseSkill_;

	public const int CounterPlayerFieldNumber = 11;

	private long counterPlayer_;

	public const int CardUniqueIdsFieldNumber = 12;

	private static readonly FieldCodec<int> _repeated_cardUniqueIds_codec = FieldCodec.ForSFixed32(98u);

	private readonly RepeatedField<int> cardUniqueIds_ = new RepeatedField<int>();

	public const int UseSelectCardIndexFieldNumber = 13;

	private int useSelectCardIndex_;

	public const int LandBuffUniqueIdsFieldNumber = 14;

	private static readonly FieldCodec<long> _repeated_landBuffUniqueIds_codec = FieldCodec.ForSFixed64(114u);

	private readonly RepeatedField<long> landBuffUniqueIds_ = new RepeatedField<long>();

	public const int NotMoveFieldNumber = 15;

	private bool notMove_;

	public const int DicePointFieldNumber = 16;

	private int dicePoint_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UseEffectCardC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[323];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionInfo Info
	{
		get
		{
			return info_;
		}
		set
		{
			info_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CanUseCardIds => canUseCardIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardId
	{
		get
		{
			return cardId_;
		}
		set
		{
			cardId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> TargetIds => targetIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TargetNodeIds => targetNodeIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DevPoint
	{
		get
		{
			return devPoint_;
		}
		set
		{
			devPoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool UseSkill
	{
		get
		{
			return useSkill_;
		}
		set
		{
			useSkill_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkillId
	{
		get
		{
			return skillId_;
		}
		set
		{
			skillId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NotUseSkill
	{
		get
		{
			return notUseSkill_;
		}
		set
		{
			notUseSkill_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long CounterPlayer
	{
		get
		{
			return counterPlayer_;
		}
		set
		{
			counterPlayer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CardUniqueIds => cardUniqueIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseSelectCardIndex
	{
		get
		{
			return useSelectCardIndex_;
		}
		set
		{
			useSelectCardIndex_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> LandBuffUniqueIds => landBuffUniqueIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NotMove
	{
		get
		{
			return notMove_;
		}
		set
		{
			notMove_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DicePoint
	{
		get
		{
			return dicePoint_;
		}
		set
		{
			dicePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseEffectCardC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseEffectCardC2S(UseEffectCardC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		canUseCardIds_ = other.canUseCardIds_.Clone();
		cardId_ = other.cardId_;
		targetIds_ = other.targetIds_.Clone();
		targetNodeIds_ = other.targetNodeIds_.Clone();
		devPoint_ = other.devPoint_;
		useSkill_ = other.useSkill_;
		skillId_ = other.skillId_;
		notUseSkill_ = other.notUseSkill_;
		counterPlayer_ = other.counterPlayer_;
		cardUniqueIds_ = other.cardUniqueIds_.Clone();
		useSelectCardIndex_ = other.useSelectCardIndex_;
		landBuffUniqueIds_ = other.landBuffUniqueIds_.Clone();
		notMove_ = other.notMove_;
		dicePoint_ = other.dicePoint_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseEffectCardC2S Clone()
	{
		return new UseEffectCardC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UseEffectCardC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UseEffectCardC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Info, other.Info))
		{
			return false;
		}
		if (!canUseCardIds_.Equals(other.canUseCardIds_))
		{
			return false;
		}
		if (CardId != other.CardId)
		{
			return false;
		}
		if (!targetIds_.Equals(other.targetIds_))
		{
			return false;
		}
		if (!targetNodeIds_.Equals(other.targetNodeIds_))
		{
			return false;
		}
		if (DevPoint != other.DevPoint)
		{
			return false;
		}
		if (UseSkill != other.UseSkill)
		{
			return false;
		}
		if (SkillId != other.SkillId)
		{
			return false;
		}
		if (NotUseSkill != other.NotUseSkill)
		{
			return false;
		}
		if (CounterPlayer != other.CounterPlayer)
		{
			return false;
		}
		if (!cardUniqueIds_.Equals(other.cardUniqueIds_))
		{
			return false;
		}
		if (UseSelectCardIndex != other.UseSelectCardIndex)
		{
			return false;
		}
		if (!landBuffUniqueIds_.Equals(other.landBuffUniqueIds_))
		{
			return false;
		}
		if (NotMove != other.NotMove)
		{
			return false;
		}
		if (DicePoint != other.DicePoint)
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
		if (info_ != null)
		{
			num ^= Info.GetHashCode();
		}
		num ^= canUseCardIds_.GetHashCode();
		if (CardId != 0)
		{
			num ^= CardId.GetHashCode();
		}
		num ^= targetIds_.GetHashCode();
		num ^= targetNodeIds_.GetHashCode();
		if (DevPoint != 0)
		{
			num ^= DevPoint.GetHashCode();
		}
		if (UseSkill)
		{
			num ^= UseSkill.GetHashCode();
		}
		if (SkillId != 0)
		{
			num ^= SkillId.GetHashCode();
		}
		if (NotUseSkill)
		{
			num ^= NotUseSkill.GetHashCode();
		}
		if (CounterPlayer != 0L)
		{
			num ^= CounterPlayer.GetHashCode();
		}
		num ^= cardUniqueIds_.GetHashCode();
		if (UseSelectCardIndex != 0)
		{
			num ^= UseSelectCardIndex.GetHashCode();
		}
		num ^= landBuffUniqueIds_.GetHashCode();
		if (NotMove)
		{
			num ^= NotMove.GetHashCode();
		}
		if (DicePoint != 0)
		{
			num ^= DicePoint.GetHashCode();
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
		if (info_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Info);
		}
		canUseCardIds_.WriteTo(ref output, _repeated_canUseCardIds_codec);
		if (CardId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(CardId);
		}
		targetIds_.WriteTo(ref output, _repeated_targetIds_codec);
		targetNodeIds_.WriteTo(ref output, _repeated_targetNodeIds_codec);
		if (DevPoint != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(DevPoint);
		}
		if (UseSkill)
		{
			output.WriteRawTag(64);
			output.WriteBool(UseSkill);
		}
		if (SkillId != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(SkillId);
		}
		if (NotUseSkill)
		{
			output.WriteRawTag(80);
			output.WriteBool(NotUseSkill);
		}
		if (CounterPlayer != 0L)
		{
			output.WriteRawTag(89);
			output.WriteSFixed64(CounterPlayer);
		}
		cardUniqueIds_.WriteTo(ref output, _repeated_cardUniqueIds_codec);
		if (UseSelectCardIndex != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(UseSelectCardIndex);
		}
		landBuffUniqueIds_.WriteTo(ref output, _repeated_landBuffUniqueIds_codec);
		if (NotMove)
		{
			output.WriteRawTag(120);
			output.WriteBool(NotMove);
		}
		if (DicePoint != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(DicePoint);
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
		if (info_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Info);
		}
		num += canUseCardIds_.CalculateSize(_repeated_canUseCardIds_codec);
		if (CardId != 0)
		{
			num += 5;
		}
		num += targetIds_.CalculateSize(_repeated_targetIds_codec);
		num += targetNodeIds_.CalculateSize(_repeated_targetNodeIds_codec);
		if (DevPoint != 0)
		{
			num += 5;
		}
		if (UseSkill)
		{
			num += 2;
		}
		if (SkillId != 0)
		{
			num += 5;
		}
		if (NotUseSkill)
		{
			num += 2;
		}
		if (CounterPlayer != 0L)
		{
			num += 9;
		}
		num += cardUniqueIds_.CalculateSize(_repeated_cardUniqueIds_codec);
		if (UseSelectCardIndex != 0)
		{
			num += 5;
		}
		num += landBuffUniqueIds_.CalculateSize(_repeated_landBuffUniqueIds_codec);
		if (NotMove)
		{
			num += 2;
		}
		if (DicePoint != 0)
		{
			num += 6;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(UseEffectCardC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.info_ != null)
		{
			if (info_ == null)
			{
				Info = new ActionInfo();
			}
			Info.MergeFrom(other.Info);
		}
		canUseCardIds_.Add(other.canUseCardIds_);
		if (other.CardId != 0)
		{
			CardId = other.CardId;
		}
		targetIds_.Add(other.targetIds_);
		targetNodeIds_.Add(other.targetNodeIds_);
		if (other.DevPoint != 0)
		{
			DevPoint = other.DevPoint;
		}
		if (other.UseSkill)
		{
			UseSkill = other.UseSkill;
		}
		if (other.SkillId != 0)
		{
			SkillId = other.SkillId;
		}
		if (other.NotUseSkill)
		{
			NotUseSkill = other.NotUseSkill;
		}
		if (other.CounterPlayer != 0L)
		{
			CounterPlayer = other.CounterPlayer;
		}
		cardUniqueIds_.Add(other.cardUniqueIds_);
		if (other.UseSelectCardIndex != 0)
		{
			UseSelectCardIndex = other.UseSelectCardIndex;
		}
		landBuffUniqueIds_.Add(other.landBuffUniqueIds_);
		if (other.NotMove)
		{
			NotMove = other.NotMove;
		}
		if (other.DicePoint != 0)
		{
			DicePoint = other.DicePoint;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			case 10u:
				if (info_ == null)
				{
					Info = new ActionInfo();
				}
				input.ReadMessage(Info);
				break;
			case 18u:
			case 21u:
				canUseCardIds_.AddEntriesFrom(ref input, _repeated_canUseCardIds_codec);
				break;
			case 29u:
				CardId = input.ReadSFixed32();
				break;
			case 41u:
			case 42u:
				targetIds_.AddEntriesFrom(ref input, _repeated_targetIds_codec);
				break;
			case 50u:
			case 53u:
				targetNodeIds_.AddEntriesFrom(ref input, _repeated_targetNodeIds_codec);
				break;
			case 61u:
				DevPoint = input.ReadSFixed32();
				break;
			case 64u:
				UseSkill = input.ReadBool();
				break;
			case 77u:
				SkillId = input.ReadSFixed32();
				break;
			case 80u:
				NotUseSkill = input.ReadBool();
				break;
			case 89u:
				CounterPlayer = input.ReadSFixed64();
				break;
			case 98u:
			case 101u:
				cardUniqueIds_.AddEntriesFrom(ref input, _repeated_cardUniqueIds_codec);
				break;
			case 109u:
				UseSelectCardIndex = input.ReadSFixed32();
				break;
			case 113u:
			case 114u:
				landBuffUniqueIds_.AddEntriesFrom(ref input, _repeated_landBuffUniqueIds_codec);
				break;
			case 120u:
				NotMove = input.ReadBool();
				break;
			case 133u:
				DicePoint = input.ReadSFixed32();
				break;
			}
		}
	}
}
