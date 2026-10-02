using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class GmC2S : IMessage<GmC2S>, IMessage, IEquatable<GmC2S>, IDeepCloneable<GmC2S>, IBufferMessage
{
	private static readonly MessageParser<GmC2S> _parser = new MessageParser<GmC2S>(() => new GmC2S());

	private UnknownFieldSet _unknownFields;

	public const int HpFieldNumber = 1;

	private int hp_;

	public const int GoldFieldNumber = 2;

	private int gold_;

	public const int EventIdFieldNumber = 3;

	private int eventId_;

	public const int DestinyIdFieldNumber = 4;

	private int destinyId_;

	public const int DivinationIdFieldNumber = 5;

	private int divinationId_;

	public const int AddCardIdsFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_addCardIds_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> addCardIds_ = new RepeatedField<int>();

	public const int RemoveCardIdsFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_removeCardIds_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> removeCardIds_ = new RepeatedField<int>();

	public const int IsOverFieldNumber = 8;

	private bool isOver_;

	public const int RelicIdFieldNumber = 9;

	private int relicId_;

	public const int GameProgressFieldNumber = 10;

	private int gameProgress_;

	public const int SkillIdFieldNumber = 11;

	private int skillId_;

	public const int MonsterIdFieldNumber = 12;

	private int monsterId_;

	public const int TargetIdFieldNumber = 13;

	private long targetId_;

	public const int TargetNodeIdFieldNumber = 14;

	private int targetNodeId_;

	public const int MovePointFieldNumber = 15;

	private int movePoint_;

	public const int HealthValFieldNumber = 16;

	private int healthVal_;

	public const int AttackValFieldNumber = 17;

	private int attackVal_;

	public const int DefenseValFieldNumber = 18;

	private int defenseVal_;

	public const int StarLvFieldNumber = 19;

	private int starLv_;

	public const int GoldCountFieldNumber = 20;

	private int goldCount_;

	public const int DiceAttackPointFieldNumber = 21;

	private int diceAttackPoint_;

	public const int DiceDefensePointFieldNumber = 22;

	private int diceDefensePoint_;

	public const int CreateMonsterNodeIdFieldNumber = 23;

	private int createMonsterNodeId_;

	public const int ReRollNumFieldNumber = 24;

	private int reRollNum_;

	public const int BuffIdFieldNumber = 25;

	private int buffId_;

	public const int SourceSkillIdFieldNumber = 26;

	private int sourceSkillId_;

	public const int SourceCardIdFieldNumber = 27;

	private int sourceCardId_;

	public const int UseCardNumFieldNumber = 28;

	private int useCardNum_;

	public const int TermIdsFieldNumber = 29;

	private static readonly FieldCodec<int> _repeated_termIds_codec = FieldCodec.ForSFixed32(234u);

	private readonly RepeatedField<int> termIds_ = new RepeatedField<int>();

	public const int MapDifficultyIdFieldNumber = 30;

	private int mapDifficultyId_;

	public const int GameRoundFieldNumber = 31;

	private int gameRound_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GmC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[213];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Hp
	{
		get
		{
			return hp_;
		}
		set
		{
			hp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Gold
	{
		get
		{
			return gold_;
		}
		set
		{
			gold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EventId
	{
		get
		{
			return eventId_;
		}
		set
		{
			eventId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DestinyId
	{
		get
		{
			return destinyId_;
		}
		set
		{
			destinyId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DivinationId
	{
		get
		{
			return divinationId_;
		}
		set
		{
			divinationId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> AddCardIds => addCardIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RemoveCardIds => removeCardIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsOver
	{
		get
		{
			return isOver_;
		}
		set
		{
			isOver_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicId
	{
		get
		{
			return relicId_;
		}
		set
		{
			relicId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GameProgress
	{
		get
		{
			return gameProgress_;
		}
		set
		{
			gameProgress_ = value;
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
	public int MonsterId
	{
		get
		{
			return monsterId_;
		}
		set
		{
			monsterId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long TargetId
	{
		get
		{
			return targetId_;
		}
		set
		{
			targetId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TargetNodeId
	{
		get
		{
			return targetNodeId_;
		}
		set
		{
			targetNodeId_ = value;
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
	public int HealthVal
	{
		get
		{
			return healthVal_;
		}
		set
		{
			healthVal_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AttackVal
	{
		get
		{
			return attackVal_;
		}
		set
		{
			attackVal_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DefenseVal
	{
		get
		{
			return defenseVal_;
		}
		set
		{
			defenseVal_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int StarLv
	{
		get
		{
			return starLv_;
		}
		set
		{
			starLv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoldCount
	{
		get
		{
			return goldCount_;
		}
		set
		{
			goldCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiceAttackPoint
	{
		get
		{
			return diceAttackPoint_;
		}
		set
		{
			diceAttackPoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiceDefensePoint
	{
		get
		{
			return diceDefensePoint_;
		}
		set
		{
			diceDefensePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CreateMonsterNodeId
	{
		get
		{
			return createMonsterNodeId_;
		}
		set
		{
			createMonsterNodeId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ReRollNum
	{
		get
		{
			return reRollNum_;
		}
		set
		{
			reRollNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BuffId
	{
		get
		{
			return buffId_;
		}
		set
		{
			buffId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SourceSkillId
	{
		get
		{
			return sourceSkillId_;
		}
		set
		{
			sourceSkillId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SourceCardId
	{
		get
		{
			return sourceCardId_;
		}
		set
		{
			sourceCardId_ = value;
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
	public RepeatedField<int> TermIds => termIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapDifficultyId
	{
		get
		{
			return mapDifficultyId_;
		}
		set
		{
			mapDifficultyId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GameRound
	{
		get
		{
			return gameRound_;
		}
		set
		{
			gameRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmC2S(GmC2S other)
		: this()
	{
		hp_ = other.hp_;
		gold_ = other.gold_;
		eventId_ = other.eventId_;
		destinyId_ = other.destinyId_;
		divinationId_ = other.divinationId_;
		addCardIds_ = other.addCardIds_.Clone();
		removeCardIds_ = other.removeCardIds_.Clone();
		isOver_ = other.isOver_;
		relicId_ = other.relicId_;
		gameProgress_ = other.gameProgress_;
		skillId_ = other.skillId_;
		monsterId_ = other.monsterId_;
		targetId_ = other.targetId_;
		targetNodeId_ = other.targetNodeId_;
		movePoint_ = other.movePoint_;
		healthVal_ = other.healthVal_;
		attackVal_ = other.attackVal_;
		defenseVal_ = other.defenseVal_;
		starLv_ = other.starLv_;
		goldCount_ = other.goldCount_;
		diceAttackPoint_ = other.diceAttackPoint_;
		diceDefensePoint_ = other.diceDefensePoint_;
		createMonsterNodeId_ = other.createMonsterNodeId_;
		reRollNum_ = other.reRollNum_;
		buffId_ = other.buffId_;
		sourceSkillId_ = other.sourceSkillId_;
		sourceCardId_ = other.sourceCardId_;
		useCardNum_ = other.useCardNum_;
		termIds_ = other.termIds_.Clone();
		mapDifficultyId_ = other.mapDifficultyId_;
		gameRound_ = other.gameRound_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmC2S Clone()
	{
		return new GmC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GmC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GmC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Hp != other.Hp)
		{
			return false;
		}
		if (Gold != other.Gold)
		{
			return false;
		}
		if (EventId != other.EventId)
		{
			return false;
		}
		if (DestinyId != other.DestinyId)
		{
			return false;
		}
		if (DivinationId != other.DivinationId)
		{
			return false;
		}
		if (!addCardIds_.Equals(other.addCardIds_))
		{
			return false;
		}
		if (!removeCardIds_.Equals(other.removeCardIds_))
		{
			return false;
		}
		if (IsOver != other.IsOver)
		{
			return false;
		}
		if (RelicId != other.RelicId)
		{
			return false;
		}
		if (GameProgress != other.GameProgress)
		{
			return false;
		}
		if (SkillId != other.SkillId)
		{
			return false;
		}
		if (MonsterId != other.MonsterId)
		{
			return false;
		}
		if (TargetId != other.TargetId)
		{
			return false;
		}
		if (TargetNodeId != other.TargetNodeId)
		{
			return false;
		}
		if (MovePoint != other.MovePoint)
		{
			return false;
		}
		if (HealthVal != other.HealthVal)
		{
			return false;
		}
		if (AttackVal != other.AttackVal)
		{
			return false;
		}
		if (DefenseVal != other.DefenseVal)
		{
			return false;
		}
		if (StarLv != other.StarLv)
		{
			return false;
		}
		if (GoldCount != other.GoldCount)
		{
			return false;
		}
		if (DiceAttackPoint != other.DiceAttackPoint)
		{
			return false;
		}
		if (DiceDefensePoint != other.DiceDefensePoint)
		{
			return false;
		}
		if (CreateMonsterNodeId != other.CreateMonsterNodeId)
		{
			return false;
		}
		if (ReRollNum != other.ReRollNum)
		{
			return false;
		}
		if (BuffId != other.BuffId)
		{
			return false;
		}
		if (SourceSkillId != other.SourceSkillId)
		{
			return false;
		}
		if (SourceCardId != other.SourceCardId)
		{
			return false;
		}
		if (UseCardNum != other.UseCardNum)
		{
			return false;
		}
		if (!termIds_.Equals(other.termIds_))
		{
			return false;
		}
		if (MapDifficultyId != other.MapDifficultyId)
		{
			return false;
		}
		if (GameRound != other.GameRound)
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
		if (Hp != 0)
		{
			num ^= Hp.GetHashCode();
		}
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
		}
		if (EventId != 0)
		{
			num ^= EventId.GetHashCode();
		}
		if (DestinyId != 0)
		{
			num ^= DestinyId.GetHashCode();
		}
		if (DivinationId != 0)
		{
			num ^= DivinationId.GetHashCode();
		}
		num ^= addCardIds_.GetHashCode();
		num ^= removeCardIds_.GetHashCode();
		if (IsOver)
		{
			num ^= IsOver.GetHashCode();
		}
		if (RelicId != 0)
		{
			num ^= RelicId.GetHashCode();
		}
		if (GameProgress != 0)
		{
			num ^= GameProgress.GetHashCode();
		}
		if (SkillId != 0)
		{
			num ^= SkillId.GetHashCode();
		}
		if (MonsterId != 0)
		{
			num ^= MonsterId.GetHashCode();
		}
		if (TargetId != 0L)
		{
			num ^= TargetId.GetHashCode();
		}
		if (TargetNodeId != 0)
		{
			num ^= TargetNodeId.GetHashCode();
		}
		if (MovePoint != 0)
		{
			num ^= MovePoint.GetHashCode();
		}
		if (HealthVal != 0)
		{
			num ^= HealthVal.GetHashCode();
		}
		if (AttackVal != 0)
		{
			num ^= AttackVal.GetHashCode();
		}
		if (DefenseVal != 0)
		{
			num ^= DefenseVal.GetHashCode();
		}
		if (StarLv != 0)
		{
			num ^= StarLv.GetHashCode();
		}
		if (GoldCount != 0)
		{
			num ^= GoldCount.GetHashCode();
		}
		if (DiceAttackPoint != 0)
		{
			num ^= DiceAttackPoint.GetHashCode();
		}
		if (DiceDefensePoint != 0)
		{
			num ^= DiceDefensePoint.GetHashCode();
		}
		if (CreateMonsterNodeId != 0)
		{
			num ^= CreateMonsterNodeId.GetHashCode();
		}
		if (ReRollNum != 0)
		{
			num ^= ReRollNum.GetHashCode();
		}
		if (BuffId != 0)
		{
			num ^= BuffId.GetHashCode();
		}
		if (SourceSkillId != 0)
		{
			num ^= SourceSkillId.GetHashCode();
		}
		if (SourceCardId != 0)
		{
			num ^= SourceCardId.GetHashCode();
		}
		if (UseCardNum != 0)
		{
			num ^= UseCardNum.GetHashCode();
		}
		num ^= termIds_.GetHashCode();
		if (MapDifficultyId != 0)
		{
			num ^= MapDifficultyId.GetHashCode();
		}
		if (GameRound != 0)
		{
			num ^= GameRound.GetHashCode();
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
		if (Hp != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Hp);
		}
		if (Gold != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Gold);
		}
		if (EventId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(EventId);
		}
		if (DestinyId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(DestinyId);
		}
		if (DivinationId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DivinationId);
		}
		addCardIds_.WriteTo(ref output, _repeated_addCardIds_codec);
		removeCardIds_.WriteTo(ref output, _repeated_removeCardIds_codec);
		if (IsOver)
		{
			output.WriteRawTag(64);
			output.WriteBool(IsOver);
		}
		if (RelicId != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(RelicId);
		}
		if (GameProgress != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(GameProgress);
		}
		if (SkillId != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(SkillId);
		}
		if (MonsterId != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(MonsterId);
		}
		if (TargetId != 0L)
		{
			output.WriteRawTag(105);
			output.WriteSFixed64(TargetId);
		}
		if (TargetNodeId != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(TargetNodeId);
		}
		if (MovePoint != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(MovePoint);
		}
		if (HealthVal != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(HealthVal);
		}
		if (AttackVal != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(AttackVal);
		}
		if (DefenseVal != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(DefenseVal);
		}
		if (StarLv != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(StarLv);
		}
		if (GoldCount != 0)
		{
			output.WriteRawTag(165, 1);
			output.WriteSFixed32(GoldCount);
		}
		if (DiceAttackPoint != 0)
		{
			output.WriteRawTag(173, 1);
			output.WriteSFixed32(DiceAttackPoint);
		}
		if (DiceDefensePoint != 0)
		{
			output.WriteRawTag(181, 1);
			output.WriteSFixed32(DiceDefensePoint);
		}
		if (CreateMonsterNodeId != 0)
		{
			output.WriteRawTag(189, 1);
			output.WriteSFixed32(CreateMonsterNodeId);
		}
		if (ReRollNum != 0)
		{
			output.WriteRawTag(197, 1);
			output.WriteSFixed32(ReRollNum);
		}
		if (BuffId != 0)
		{
			output.WriteRawTag(205, 1);
			output.WriteSFixed32(BuffId);
		}
		if (SourceSkillId != 0)
		{
			output.WriteRawTag(213, 1);
			output.WriteSFixed32(SourceSkillId);
		}
		if (SourceCardId != 0)
		{
			output.WriteRawTag(221, 1);
			output.WriteSFixed32(SourceCardId);
		}
		if (UseCardNum != 0)
		{
			output.WriteRawTag(229, 1);
			output.WriteSFixed32(UseCardNum);
		}
		termIds_.WriteTo(ref output, _repeated_termIds_codec);
		if (MapDifficultyId != 0)
		{
			output.WriteRawTag(245, 1);
			output.WriteSFixed32(MapDifficultyId);
		}
		if (GameRound != 0)
		{
			output.WriteRawTag(253, 1);
			output.WriteSFixed32(GameRound);
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
		if (Hp != 0)
		{
			num += 5;
		}
		if (Gold != 0)
		{
			num += 5;
		}
		if (EventId != 0)
		{
			num += 5;
		}
		if (DestinyId != 0)
		{
			num += 5;
		}
		if (DivinationId != 0)
		{
			num += 5;
		}
		num += addCardIds_.CalculateSize(_repeated_addCardIds_codec);
		num += removeCardIds_.CalculateSize(_repeated_removeCardIds_codec);
		if (IsOver)
		{
			num += 2;
		}
		if (RelicId != 0)
		{
			num += 5;
		}
		if (GameProgress != 0)
		{
			num += 5;
		}
		if (SkillId != 0)
		{
			num += 5;
		}
		if (MonsterId != 0)
		{
			num += 5;
		}
		if (TargetId != 0L)
		{
			num += 9;
		}
		if (TargetNodeId != 0)
		{
			num += 5;
		}
		if (MovePoint != 0)
		{
			num += 5;
		}
		if (HealthVal != 0)
		{
			num += 6;
		}
		if (AttackVal != 0)
		{
			num += 6;
		}
		if (DefenseVal != 0)
		{
			num += 6;
		}
		if (StarLv != 0)
		{
			num += 6;
		}
		if (GoldCount != 0)
		{
			num += 6;
		}
		if (DiceAttackPoint != 0)
		{
			num += 6;
		}
		if (DiceDefensePoint != 0)
		{
			num += 6;
		}
		if (CreateMonsterNodeId != 0)
		{
			num += 6;
		}
		if (ReRollNum != 0)
		{
			num += 6;
		}
		if (BuffId != 0)
		{
			num += 6;
		}
		if (SourceSkillId != 0)
		{
			num += 6;
		}
		if (SourceCardId != 0)
		{
			num += 6;
		}
		if (UseCardNum != 0)
		{
			num += 6;
		}
		num += termIds_.CalculateSize(_repeated_termIds_codec);
		if (MapDifficultyId != 0)
		{
			num += 6;
		}
		if (GameRound != 0)
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
	public void MergeFrom(GmC2S other)
	{
		if (other != null)
		{
			if (other.Hp != 0)
			{
				Hp = other.Hp;
			}
			if (other.Gold != 0)
			{
				Gold = other.Gold;
			}
			if (other.EventId != 0)
			{
				EventId = other.EventId;
			}
			if (other.DestinyId != 0)
			{
				DestinyId = other.DestinyId;
			}
			if (other.DivinationId != 0)
			{
				DivinationId = other.DivinationId;
			}
			addCardIds_.Add(other.addCardIds_);
			removeCardIds_.Add(other.removeCardIds_);
			if (other.IsOver)
			{
				IsOver = other.IsOver;
			}
			if (other.RelicId != 0)
			{
				RelicId = other.RelicId;
			}
			if (other.GameProgress != 0)
			{
				GameProgress = other.GameProgress;
			}
			if (other.SkillId != 0)
			{
				SkillId = other.SkillId;
			}
			if (other.MonsterId != 0)
			{
				MonsterId = other.MonsterId;
			}
			if (other.TargetId != 0L)
			{
				TargetId = other.TargetId;
			}
			if (other.TargetNodeId != 0)
			{
				TargetNodeId = other.TargetNodeId;
			}
			if (other.MovePoint != 0)
			{
				MovePoint = other.MovePoint;
			}
			if (other.HealthVal != 0)
			{
				HealthVal = other.HealthVal;
			}
			if (other.AttackVal != 0)
			{
				AttackVal = other.AttackVal;
			}
			if (other.DefenseVal != 0)
			{
				DefenseVal = other.DefenseVal;
			}
			if (other.StarLv != 0)
			{
				StarLv = other.StarLv;
			}
			if (other.GoldCount != 0)
			{
				GoldCount = other.GoldCount;
			}
			if (other.DiceAttackPoint != 0)
			{
				DiceAttackPoint = other.DiceAttackPoint;
			}
			if (other.DiceDefensePoint != 0)
			{
				DiceDefensePoint = other.DiceDefensePoint;
			}
			if (other.CreateMonsterNodeId != 0)
			{
				CreateMonsterNodeId = other.CreateMonsterNodeId;
			}
			if (other.ReRollNum != 0)
			{
				ReRollNum = other.ReRollNum;
			}
			if (other.BuffId != 0)
			{
				BuffId = other.BuffId;
			}
			if (other.SourceSkillId != 0)
			{
				SourceSkillId = other.SourceSkillId;
			}
			if (other.SourceCardId != 0)
			{
				SourceCardId = other.SourceCardId;
			}
			if (other.UseCardNum != 0)
			{
				UseCardNum = other.UseCardNum;
			}
			termIds_.Add(other.termIds_);
			if (other.MapDifficultyId != 0)
			{
				MapDifficultyId = other.MapDifficultyId;
			}
			if (other.GameRound != 0)
			{
				GameRound = other.GameRound;
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
				Hp = input.ReadSFixed32();
				break;
			case 21u:
				Gold = input.ReadSFixed32();
				break;
			case 29u:
				EventId = input.ReadSFixed32();
				break;
			case 37u:
				DestinyId = input.ReadSFixed32();
				break;
			case 45u:
				DivinationId = input.ReadSFixed32();
				break;
			case 50u:
			case 53u:
				addCardIds_.AddEntriesFrom(ref input, _repeated_addCardIds_codec);
				break;
			case 58u:
			case 61u:
				removeCardIds_.AddEntriesFrom(ref input, _repeated_removeCardIds_codec);
				break;
			case 64u:
				IsOver = input.ReadBool();
				break;
			case 77u:
				RelicId = input.ReadSFixed32();
				break;
			case 85u:
				GameProgress = input.ReadSFixed32();
				break;
			case 93u:
				SkillId = input.ReadSFixed32();
				break;
			case 101u:
				MonsterId = input.ReadSFixed32();
				break;
			case 105u:
				TargetId = input.ReadSFixed64();
				break;
			case 117u:
				TargetNodeId = input.ReadSFixed32();
				break;
			case 125u:
				MovePoint = input.ReadSFixed32();
				break;
			case 133u:
				HealthVal = input.ReadSFixed32();
				break;
			case 141u:
				AttackVal = input.ReadSFixed32();
				break;
			case 149u:
				DefenseVal = input.ReadSFixed32();
				break;
			case 157u:
				StarLv = input.ReadSFixed32();
				break;
			case 165u:
				GoldCount = input.ReadSFixed32();
				break;
			case 173u:
				DiceAttackPoint = input.ReadSFixed32();
				break;
			case 181u:
				DiceDefensePoint = input.ReadSFixed32();
				break;
			case 189u:
				CreateMonsterNodeId = input.ReadSFixed32();
				break;
			case 197u:
				ReRollNum = input.ReadSFixed32();
				break;
			case 205u:
				BuffId = input.ReadSFixed32();
				break;
			case 213u:
				SourceSkillId = input.ReadSFixed32();
				break;
			case 221u:
				SourceCardId = input.ReadSFixed32();
				break;
			case 229u:
				UseCardNum = input.ReadSFixed32();
				break;
			case 234u:
			case 237u:
				termIds_.AddEntriesFrom(ref input, _repeated_termIds_codec);
				break;
			case 245u:
				MapDifficultyId = input.ReadSFixed32();
				break;
			case 253u:
				GameRound = input.ReadSFixed32();
				break;
			}
		}
	}
}
