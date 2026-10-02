using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class AdditionAttribute : IMessage<AdditionAttribute>, IMessage, IEquatable<AdditionAttribute>, IDeepCloneable<AdditionAttribute>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum AttrType
		{
			[OriginalName("none")]
			None,
			[OriginalName("atk")]
			Atk,
			[OriginalName("def")]
			Def,
			[OriginalName("max_hp")]
			MaxHp,
			[OriginalName("move")]
			Move,
			[OriginalName("cd")]
			Cd,
			[OriginalName("card_num")]
			CardNum,
			[OriginalName("gold")]
			Gold,
			[OriginalName("reroll_num")]
			RerollNum
		}
	}

	private static readonly MessageParser<AdditionAttribute> _parser = new MessageParser<AdditionAttribute>(() => new AdditionAttribute());

	private UnknownFieldSet _unknownFields;

	public const int AttackFieldNumber = 1;

	private int attack_;

	public const int DefenseFieldNumber = 2;

	private int defense_;

	public const int HarmFieldNumber = 3;

	private int harm_;

	public const int MovePointFieldNumber = 4;

	private int movePoint_;

	public const int NextMovePointFieldNumber = 5;

	private int nextMovePoint_;

	public const int UniqueIdFieldNumber = 6;

	private long uniqueId_;

	public const int Count1FieldNumber = 7;

	private int count1_;

	public const int AbandonCardCountFieldNumber = 9;

	private int abandonCardCount_;

	public const int MarkFieldNumber = 10;

	private AttrMarkInfo mark_;

	public const int MaxHpFieldNumber = 11;

	private int maxHp_;

	public const int SkillCdFieldNumber = 12;

	private int skillCd_;

	public const int UseCardNumFieldNumber = 13;

	private int useCardNum_;

	public const int ExtraParamFieldNumber = 14;

	private long extraParam_;

	public const int CardDistanceFieldNumber = 15;

	private int cardDistance_;

	public const int CanCounterFieldNumber = 16;

	private int canCounter_;

	public const int NoMoveFieldNumber = 17;

	private bool noMove_;

	public const int IsLaughFieldNumber = 18;

	private bool isLaugh_;

	public const int ExtraParam1FieldNumber = 19;

	private long extraParam1_;

	public const int CardAtkFieldNumber = 20;

	private int cardAtk_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AdditionAttribute> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[63];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Attack
	{
		get
		{
			return attack_;
		}
		set
		{
			attack_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Defense
	{
		get
		{
			return defense_;
		}
		set
		{
			defense_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Harm
	{
		get
		{
			return harm_;
		}
		set
		{
			harm_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MovePoint
	{
		get
		{
			return movePoint_;
		}
		set
		{
			movePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NextMovePoint
	{
		get
		{
			return nextMovePoint_;
		}
		set
		{
			nextMovePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long UniqueId
	{
		get
		{
			return uniqueId_;
		}
		set
		{
			uniqueId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Count1
	{
		get
		{
			return count1_;
		}
		set
		{
			count1_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AbandonCardCount
	{
		get
		{
			return abandonCardCount_;
		}
		set
		{
			abandonCardCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AttrMarkInfo Mark
	{
		get
		{
			return mark_;
		}
		set
		{
			mark_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxHp
	{
		get
		{
			return maxHp_;
		}
		set
		{
			maxHp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkillCd
	{
		get
		{
			return skillCd_;
		}
		set
		{
			skillCd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseCardNum
	{
		get
		{
			return useCardNum_;
		}
		set
		{
			useCardNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ExtraParam
	{
		get
		{
			return extraParam_;
		}
		set
		{
			extraParam_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardDistance
	{
		get
		{
			return cardDistance_;
		}
		set
		{
			cardDistance_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CanCounter
	{
		get
		{
			return canCounter_;
		}
		set
		{
			canCounter_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NoMove
	{
		get
		{
			return noMove_;
		}
		set
		{
			noMove_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsLaugh
	{
		get
		{
			return isLaugh_;
		}
		set
		{
			isLaugh_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ExtraParam1
	{
		get
		{
			return extraParam1_;
		}
		set
		{
			extraParam1_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardAtk
	{
		get
		{
			return cardAtk_;
		}
		set
		{
			cardAtk_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AdditionAttribute()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AdditionAttribute(AdditionAttribute other)
		: this()
	{
		attack_ = other.attack_;
		defense_ = other.defense_;
		harm_ = other.harm_;
		movePoint_ = other.movePoint_;
		nextMovePoint_ = other.nextMovePoint_;
		uniqueId_ = other.uniqueId_;
		count1_ = other.count1_;
		abandonCardCount_ = other.abandonCardCount_;
		mark_ = ((other.mark_ != null) ? other.mark_.Clone() : null);
		maxHp_ = other.maxHp_;
		skillCd_ = other.skillCd_;
		useCardNum_ = other.useCardNum_;
		extraParam_ = other.extraParam_;
		cardDistance_ = other.cardDistance_;
		canCounter_ = other.canCounter_;
		noMove_ = other.noMove_;
		isLaugh_ = other.isLaugh_;
		extraParam1_ = other.extraParam1_;
		cardAtk_ = other.cardAtk_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AdditionAttribute Clone()
	{
		return new AdditionAttribute(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AdditionAttribute);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AdditionAttribute other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Attack != other.Attack)
		{
			return false;
		}
		if (Defense != other.Defense)
		{
			return false;
		}
		if (Harm != other.Harm)
		{
			return false;
		}
		if (MovePoint != other.MovePoint)
		{
			return false;
		}
		if (NextMovePoint != other.NextMovePoint)
		{
			return false;
		}
		if (UniqueId != other.UniqueId)
		{
			return false;
		}
		if (Count1 != other.Count1)
		{
			return false;
		}
		if (AbandonCardCount != other.AbandonCardCount)
		{
			return false;
		}
		if (!object.Equals(Mark, other.Mark))
		{
			return false;
		}
		if (MaxHp != other.MaxHp)
		{
			return false;
		}
		if (SkillCd != other.SkillCd)
		{
			return false;
		}
		if (UseCardNum != other.UseCardNum)
		{
			return false;
		}
		if (ExtraParam != other.ExtraParam)
		{
			return false;
		}
		if (CardDistance != other.CardDistance)
		{
			return false;
		}
		if (CanCounter != other.CanCounter)
		{
			return false;
		}
		if (NoMove != other.NoMove)
		{
			return false;
		}
		if (IsLaugh != other.IsLaugh)
		{
			return false;
		}
		if (ExtraParam1 != other.ExtraParam1)
		{
			return false;
		}
		if (CardAtk != other.CardAtk)
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
		if (Attack != 0)
		{
			num ^= Attack.GetHashCode();
		}
		if (Defense != 0)
		{
			num ^= Defense.GetHashCode();
		}
		if (Harm != 0)
		{
			num ^= Harm.GetHashCode();
		}
		if (MovePoint != 0)
		{
			num ^= MovePoint.GetHashCode();
		}
		if (NextMovePoint != 0)
		{
			num ^= NextMovePoint.GetHashCode();
		}
		if (UniqueId != 0L)
		{
			num ^= UniqueId.GetHashCode();
		}
		if (Count1 != 0)
		{
			num ^= Count1.GetHashCode();
		}
		if (AbandonCardCount != 0)
		{
			num ^= AbandonCardCount.GetHashCode();
		}
		if (mark_ != null)
		{
			num ^= Mark.GetHashCode();
		}
		if (MaxHp != 0)
		{
			num ^= MaxHp.GetHashCode();
		}
		if (SkillCd != 0)
		{
			num ^= SkillCd.GetHashCode();
		}
		if (UseCardNum != 0)
		{
			num ^= UseCardNum.GetHashCode();
		}
		if (ExtraParam != 0L)
		{
			num ^= ExtraParam.GetHashCode();
		}
		if (CardDistance != 0)
		{
			num ^= CardDistance.GetHashCode();
		}
		if (CanCounter != 0)
		{
			num ^= CanCounter.GetHashCode();
		}
		if (NoMove)
		{
			num ^= NoMove.GetHashCode();
		}
		if (IsLaugh)
		{
			num ^= IsLaugh.GetHashCode();
		}
		if (ExtraParam1 != 0L)
		{
			num ^= ExtraParam1.GetHashCode();
		}
		if (CardAtk != 0)
		{
			num ^= CardAtk.GetHashCode();
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
		if (Attack != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Attack);
		}
		if (Defense != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Defense);
		}
		if (Harm != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Harm);
		}
		if (MovePoint != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(MovePoint);
		}
		if (NextMovePoint != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(NextMovePoint);
		}
		if (UniqueId != 0L)
		{
			output.WriteRawTag(49);
			output.WriteSFixed64(UniqueId);
		}
		if (Count1 != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Count1);
		}
		if (AbandonCardCount != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(AbandonCardCount);
		}
		if (mark_ != null)
		{
			output.WriteRawTag(82);
			output.WriteMessage(Mark);
		}
		if (MaxHp != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(MaxHp);
		}
		if (SkillCd != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(SkillCd);
		}
		if (UseCardNum != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(UseCardNum);
		}
		if (ExtraParam != 0L)
		{
			output.WriteRawTag(113);
			output.WriteSFixed64(ExtraParam);
		}
		if (CardDistance != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(CardDistance);
		}
		if (CanCounter != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(CanCounter);
		}
		if (NoMove)
		{
			output.WriteRawTag(136, 1);
			output.WriteBool(NoMove);
		}
		if (IsLaugh)
		{
			output.WriteRawTag(144, 1);
			output.WriteBool(IsLaugh);
		}
		if (ExtraParam1 != 0L)
		{
			output.WriteRawTag(153, 1);
			output.WriteSFixed64(ExtraParam1);
		}
		if (CardAtk != 0)
		{
			output.WriteRawTag(165, 1);
			output.WriteSFixed32(CardAtk);
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
		if (Attack != 0)
		{
			num += 5;
		}
		if (Defense != 0)
		{
			num += 5;
		}
		if (Harm != 0)
		{
			num += 5;
		}
		if (MovePoint != 0)
		{
			num += 5;
		}
		if (NextMovePoint != 0)
		{
			num += 5;
		}
		if (UniqueId != 0L)
		{
			num += 9;
		}
		if (Count1 != 0)
		{
			num += 5;
		}
		if (AbandonCardCount != 0)
		{
			num += 5;
		}
		if (mark_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Mark);
		}
		if (MaxHp != 0)
		{
			num += 5;
		}
		if (SkillCd != 0)
		{
			num += 5;
		}
		if (UseCardNum != 0)
		{
			num += 5;
		}
		if (ExtraParam != 0L)
		{
			num += 9;
		}
		if (CardDistance != 0)
		{
			num += 5;
		}
		if (CanCounter != 0)
		{
			num += 6;
		}
		if (NoMove)
		{
			num += 3;
		}
		if (IsLaugh)
		{
			num += 3;
		}
		if (ExtraParam1 != 0L)
		{
			num += 10;
		}
		if (CardAtk != 0)
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
	public void MergeFrom(AdditionAttribute other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Attack != 0)
		{
			Attack = other.Attack;
		}
		if (other.Defense != 0)
		{
			Defense = other.Defense;
		}
		if (other.Harm != 0)
		{
			Harm = other.Harm;
		}
		if (other.MovePoint != 0)
		{
			MovePoint = other.MovePoint;
		}
		if (other.NextMovePoint != 0)
		{
			NextMovePoint = other.NextMovePoint;
		}
		if (other.UniqueId != 0L)
		{
			UniqueId = other.UniqueId;
		}
		if (other.Count1 != 0)
		{
			Count1 = other.Count1;
		}
		if (other.AbandonCardCount != 0)
		{
			AbandonCardCount = other.AbandonCardCount;
		}
		if (other.mark_ != null)
		{
			if (mark_ == null)
			{
				Mark = new AttrMarkInfo();
			}
			Mark.MergeFrom(other.Mark);
		}
		if (other.MaxHp != 0)
		{
			MaxHp = other.MaxHp;
		}
		if (other.SkillCd != 0)
		{
			SkillCd = other.SkillCd;
		}
		if (other.UseCardNum != 0)
		{
			UseCardNum = other.UseCardNum;
		}
		if (other.ExtraParam != 0L)
		{
			ExtraParam = other.ExtraParam;
		}
		if (other.CardDistance != 0)
		{
			CardDistance = other.CardDistance;
		}
		if (other.CanCounter != 0)
		{
			CanCounter = other.CanCounter;
		}
		if (other.NoMove)
		{
			NoMove = other.NoMove;
		}
		if (other.IsLaugh)
		{
			IsLaugh = other.IsLaugh;
		}
		if (other.ExtraParam1 != 0L)
		{
			ExtraParam1 = other.ExtraParam1;
		}
		if (other.CardAtk != 0)
		{
			CardAtk = other.CardAtk;
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
			case 13u:
				Attack = input.ReadSFixed32();
				break;
			case 21u:
				Defense = input.ReadSFixed32();
				break;
			case 29u:
				Harm = input.ReadSFixed32();
				break;
			case 37u:
				MovePoint = input.ReadSFixed32();
				break;
			case 45u:
				NextMovePoint = input.ReadSFixed32();
				break;
			case 49u:
				UniqueId = input.ReadSFixed64();
				break;
			case 61u:
				Count1 = input.ReadSFixed32();
				break;
			case 77u:
				AbandonCardCount = input.ReadSFixed32();
				break;
			case 82u:
				if (mark_ == null)
				{
					Mark = new AttrMarkInfo();
				}
				input.ReadMessage(Mark);
				break;
			case 93u:
				MaxHp = input.ReadSFixed32();
				break;
			case 101u:
				SkillCd = input.ReadSFixed32();
				break;
			case 109u:
				UseCardNum = input.ReadSFixed32();
				break;
			case 113u:
				ExtraParam = input.ReadSFixed64();
				break;
			case 125u:
				CardDistance = input.ReadSFixed32();
				break;
			case 133u:
				CanCounter = input.ReadSFixed32();
				break;
			case 136u:
				NoMove = input.ReadBool();
				break;
			case 144u:
				IsLaugh = input.ReadBool();
				break;
			case 153u:
				ExtraParam1 = input.ReadSFixed64();
				break;
			case 165u:
				CardAtk = input.ReadSFixed32();
				break;
			}
		}
	}
}
