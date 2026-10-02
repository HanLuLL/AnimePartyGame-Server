using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class BattleRole : IMessage<BattleRole>, IMessage, IEquatable<BattleRole>, IDeepCloneable<BattleRole>, IBufferMessage
{
	private static readonly MessageParser<BattleRole> _parser = new MessageParser<BattleRole>(() => new BattleRole());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int AtkFieldNumber = 2;

	private int atk_;

	public const int DefFieldNumber = 3;

	private int def_;

	public const int UseCardsFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_useCards_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> useCards_ = new RepeatedField<int>();

	public const int PointFieldNumber = 6;

	private int point_;

	public const int DodgeFieldNumber = 7;

	private bool dodge_;

	public const int IncHpFieldNumber = 8;

	private int incHp_;

	public const int CostFieldNumber = 9;

	private int cost_;

	public const int DropGoldFieldNumber = 10;

	private int dropGold_;

	public const int MaxCostFieldNumber = 11;

	private int maxCost_;

	public const int CanNotFightBackFieldNumber = 12;

	private bool canNotFightBack_;

	public const int HeroIdFieldNumber = 13;

	private int heroId_;

	public const int AttackBonusFieldNumber = 14;

	private int attackBonus_;

	public const int ChainAttackerFieldNumber = 15;

	private long chainAttacker_;

	public const int ChainAttackDamageFieldNumber = 16;

	private int chainAttackDamage_;

	public const int MaxAtkFieldNumber = 17;

	private int maxAtk_;

	public const int MaxDefFieldNumber = 18;

	private int maxDef_;

	public const int MinAtkFieldNumber = 19;

	private int minAtk_;

	public const int MinDefFieldNumber = 20;

	private int minDef_;

	public const int InitAtkFieldNumber = 21;

	private int initAtk_;

	public const int InitDefFieldNumber = 22;

	private int initDef_;

	public const int CardCombatBonusFieldNumber = 23;

	private static readonly FieldCodec<cardCombat> _repeated_cardCombatBonus_codec = FieldCodec.ForMessage(186u, cardCombat.Parser);

	private readonly RepeatedField<cardCombat> cardCombatBonus_ = new RepeatedField<cardCombat>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattleRole> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[91];

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
	public int Atk
	{
		get
		{
			return atk_;
		}
		set
		{
			atk_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Def
	{
		get
		{
			return def_;
		}
		set
		{
			def_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> UseCards => useCards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Point
	{
		get
		{
			return point_;
		}
		set
		{
			point_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Dodge
	{
		get
		{
			return dodge_;
		}
		set
		{
			dodge_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int IncHp
	{
		get
		{
			return incHp_;
		}
		set
		{
			incHp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Cost
	{
		get
		{
			return cost_;
		}
		set
		{
			cost_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DropGold
	{
		get
		{
			return dropGold_;
		}
		set
		{
			dropGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxCost
	{
		get
		{
			return maxCost_;
		}
		set
		{
			maxCost_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanNotFightBack
	{
		get
		{
			return canNotFightBack_;
		}
		set
		{
			canNotFightBack_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HeroId
	{
		get
		{
			return heroId_;
		}
		set
		{
			heroId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AttackBonus
	{
		get
		{
			return attackBonus_;
		}
		set
		{
			attackBonus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ChainAttacker
	{
		get
		{
			return chainAttacker_;
		}
		set
		{
			chainAttacker_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ChainAttackDamage
	{
		get
		{
			return chainAttackDamage_;
		}
		set
		{
			chainAttackDamage_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxAtk
	{
		get
		{
			return maxAtk_;
		}
		set
		{
			maxAtk_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxDef
	{
		get
		{
			return maxDef_;
		}
		set
		{
			maxDef_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MinAtk
	{
		get
		{
			return minAtk_;
		}
		set
		{
			minAtk_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MinDef
	{
		get
		{
			return minDef_;
		}
		set
		{
			minDef_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int InitAtk
	{
		get
		{
			return initAtk_;
		}
		set
		{
			initAtk_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int InitDef
	{
		get
		{
			return initDef_;
		}
		set
		{
			initDef_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<cardCombat> CardCombatBonus => cardCombatBonus_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleRole()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleRole(BattleRole other)
		: this()
	{
		playerId_ = other.playerId_;
		atk_ = other.atk_;
		def_ = other.def_;
		useCards_ = other.useCards_.Clone();
		point_ = other.point_;
		dodge_ = other.dodge_;
		incHp_ = other.incHp_;
		cost_ = other.cost_;
		dropGold_ = other.dropGold_;
		maxCost_ = other.maxCost_;
		canNotFightBack_ = other.canNotFightBack_;
		heroId_ = other.heroId_;
		attackBonus_ = other.attackBonus_;
		chainAttacker_ = other.chainAttacker_;
		chainAttackDamage_ = other.chainAttackDamage_;
		maxAtk_ = other.maxAtk_;
		maxDef_ = other.maxDef_;
		minAtk_ = other.minAtk_;
		minDef_ = other.minDef_;
		initAtk_ = other.initAtk_;
		initDef_ = other.initDef_;
		cardCombatBonus_ = other.cardCombatBonus_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleRole Clone()
	{
		return new BattleRole(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattleRole);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattleRole other)
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
		if (Atk != other.Atk)
		{
			return false;
		}
		if (Def != other.Def)
		{
			return false;
		}
		if (!useCards_.Equals(other.useCards_))
		{
			return false;
		}
		if (Point != other.Point)
		{
			return false;
		}
		if (Dodge != other.Dodge)
		{
			return false;
		}
		if (IncHp != other.IncHp)
		{
			return false;
		}
		if (Cost != other.Cost)
		{
			return false;
		}
		if (DropGold != other.DropGold)
		{
			return false;
		}
		if (MaxCost != other.MaxCost)
		{
			return false;
		}
		if (CanNotFightBack != other.CanNotFightBack)
		{
			return false;
		}
		if (HeroId != other.HeroId)
		{
			return false;
		}
		if (AttackBonus != other.AttackBonus)
		{
			return false;
		}
		if (ChainAttacker != other.ChainAttacker)
		{
			return false;
		}
		if (ChainAttackDamage != other.ChainAttackDamage)
		{
			return false;
		}
		if (MaxAtk != other.MaxAtk)
		{
			return false;
		}
		if (MaxDef != other.MaxDef)
		{
			return false;
		}
		if (MinAtk != other.MinAtk)
		{
			return false;
		}
		if (MinDef != other.MinDef)
		{
			return false;
		}
		if (InitAtk != other.InitAtk)
		{
			return false;
		}
		if (InitDef != other.InitDef)
		{
			return false;
		}
		if (!cardCombatBonus_.Equals(other.cardCombatBonus_))
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
		if (Atk != 0)
		{
			num ^= Atk.GetHashCode();
		}
		if (Def != 0)
		{
			num ^= Def.GetHashCode();
		}
		num ^= useCards_.GetHashCode();
		if (Point != 0)
		{
			num ^= Point.GetHashCode();
		}
		if (Dodge)
		{
			num ^= Dodge.GetHashCode();
		}
		if (IncHp != 0)
		{
			num ^= IncHp.GetHashCode();
		}
		if (Cost != 0)
		{
			num ^= Cost.GetHashCode();
		}
		if (DropGold != 0)
		{
			num ^= DropGold.GetHashCode();
		}
		if (MaxCost != 0)
		{
			num ^= MaxCost.GetHashCode();
		}
		if (CanNotFightBack)
		{
			num ^= CanNotFightBack.GetHashCode();
		}
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
		}
		if (AttackBonus != 0)
		{
			num ^= AttackBonus.GetHashCode();
		}
		if (ChainAttacker != 0L)
		{
			num ^= ChainAttacker.GetHashCode();
		}
		if (ChainAttackDamage != 0)
		{
			num ^= ChainAttackDamage.GetHashCode();
		}
		if (MaxAtk != 0)
		{
			num ^= MaxAtk.GetHashCode();
		}
		if (MaxDef != 0)
		{
			num ^= MaxDef.GetHashCode();
		}
		if (MinAtk != 0)
		{
			num ^= MinAtk.GetHashCode();
		}
		if (MinDef != 0)
		{
			num ^= MinDef.GetHashCode();
		}
		if (InitAtk != 0)
		{
			num ^= InitAtk.GetHashCode();
		}
		if (InitDef != 0)
		{
			num ^= InitDef.GetHashCode();
		}
		num ^= cardCombatBonus_.GetHashCode();
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
		if (Atk != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Atk);
		}
		if (Def != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Def);
		}
		useCards_.WriteTo(ref output, _repeated_useCards_codec);
		if (Point != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Point);
		}
		if (Dodge)
		{
			output.WriteRawTag(56);
			output.WriteBool(Dodge);
		}
		if (IncHp != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(IncHp);
		}
		if (Cost != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(Cost);
		}
		if (DropGold != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(DropGold);
		}
		if (MaxCost != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(MaxCost);
		}
		if (CanNotFightBack)
		{
			output.WriteRawTag(96);
			output.WriteBool(CanNotFightBack);
		}
		if (HeroId != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(HeroId);
		}
		if (AttackBonus != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(AttackBonus);
		}
		if (ChainAttacker != 0L)
		{
			output.WriteRawTag(121);
			output.WriteSFixed64(ChainAttacker);
		}
		if (ChainAttackDamage != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(ChainAttackDamage);
		}
		if (MaxAtk != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(MaxAtk);
		}
		if (MaxDef != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(MaxDef);
		}
		if (MinAtk != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(MinAtk);
		}
		if (MinDef != 0)
		{
			output.WriteRawTag(165, 1);
			output.WriteSFixed32(MinDef);
		}
		if (InitAtk != 0)
		{
			output.WriteRawTag(173, 1);
			output.WriteSFixed32(InitAtk);
		}
		if (InitDef != 0)
		{
			output.WriteRawTag(181, 1);
			output.WriteSFixed32(InitDef);
		}
		cardCombatBonus_.WriteTo(ref output, _repeated_cardCombatBonus_codec);
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
		if (Atk != 0)
		{
			num += 5;
		}
		if (Def != 0)
		{
			num += 5;
		}
		num += useCards_.CalculateSize(_repeated_useCards_codec);
		if (Point != 0)
		{
			num += 5;
		}
		if (Dodge)
		{
			num += 2;
		}
		if (IncHp != 0)
		{
			num += 5;
		}
		if (Cost != 0)
		{
			num += 5;
		}
		if (DropGold != 0)
		{
			num += 5;
		}
		if (MaxCost != 0)
		{
			num += 5;
		}
		if (CanNotFightBack)
		{
			num += 2;
		}
		if (HeroId != 0)
		{
			num += 5;
		}
		if (AttackBonus != 0)
		{
			num += 5;
		}
		if (ChainAttacker != 0L)
		{
			num += 9;
		}
		if (ChainAttackDamage != 0)
		{
			num += 6;
		}
		if (MaxAtk != 0)
		{
			num += 6;
		}
		if (MaxDef != 0)
		{
			num += 6;
		}
		if (MinAtk != 0)
		{
			num += 6;
		}
		if (MinDef != 0)
		{
			num += 6;
		}
		if (InitAtk != 0)
		{
			num += 6;
		}
		if (InitDef != 0)
		{
			num += 6;
		}
		num += cardCombatBonus_.CalculateSize(_repeated_cardCombatBonus_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattleRole other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.Atk != 0)
			{
				Atk = other.Atk;
			}
			if (other.Def != 0)
			{
				Def = other.Def;
			}
			useCards_.Add(other.useCards_);
			if (other.Point != 0)
			{
				Point = other.Point;
			}
			if (other.Dodge)
			{
				Dodge = other.Dodge;
			}
			if (other.IncHp != 0)
			{
				IncHp = other.IncHp;
			}
			if (other.Cost != 0)
			{
				Cost = other.Cost;
			}
			if (other.DropGold != 0)
			{
				DropGold = other.DropGold;
			}
			if (other.MaxCost != 0)
			{
				MaxCost = other.MaxCost;
			}
			if (other.CanNotFightBack)
			{
				CanNotFightBack = other.CanNotFightBack;
			}
			if (other.HeroId != 0)
			{
				HeroId = other.HeroId;
			}
			if (other.AttackBonus != 0)
			{
				AttackBonus = other.AttackBonus;
			}
			if (other.ChainAttacker != 0L)
			{
				ChainAttacker = other.ChainAttacker;
			}
			if (other.ChainAttackDamage != 0)
			{
				ChainAttackDamage = other.ChainAttackDamage;
			}
			if (other.MaxAtk != 0)
			{
				MaxAtk = other.MaxAtk;
			}
			if (other.MaxDef != 0)
			{
				MaxDef = other.MaxDef;
			}
			if (other.MinAtk != 0)
			{
				MinAtk = other.MinAtk;
			}
			if (other.MinDef != 0)
			{
				MinDef = other.MinDef;
			}
			if (other.InitAtk != 0)
			{
				InitAtk = other.InitAtk;
			}
			if (other.InitDef != 0)
			{
				InitDef = other.InitDef;
			}
			cardCombatBonus_.Add(other.cardCombatBonus_);
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
				Atk = input.ReadSFixed32();
				break;
			case 29u:
				Def = input.ReadSFixed32();
				break;
			case 42u:
			case 45u:
				useCards_.AddEntriesFrom(ref input, _repeated_useCards_codec);
				break;
			case 53u:
				Point = input.ReadSFixed32();
				break;
			case 56u:
				Dodge = input.ReadBool();
				break;
			case 69u:
				IncHp = input.ReadSFixed32();
				break;
			case 77u:
				Cost = input.ReadSFixed32();
				break;
			case 85u:
				DropGold = input.ReadSFixed32();
				break;
			case 93u:
				MaxCost = input.ReadSFixed32();
				break;
			case 96u:
				CanNotFightBack = input.ReadBool();
				break;
			case 109u:
				HeroId = input.ReadSFixed32();
				break;
			case 117u:
				AttackBonus = input.ReadSFixed32();
				break;
			case 121u:
				ChainAttacker = input.ReadSFixed64();
				break;
			case 133u:
				ChainAttackDamage = input.ReadSFixed32();
				break;
			case 141u:
				MaxAtk = input.ReadSFixed32();
				break;
			case 149u:
				MaxDef = input.ReadSFixed32();
				break;
			case 157u:
				MinAtk = input.ReadSFixed32();
				break;
			case 165u:
				MinDef = input.ReadSFixed32();
				break;
			case 173u:
				InitAtk = input.ReadSFixed32();
				break;
			case 181u:
				InitDef = input.ReadSFixed32();
				break;
			case 186u:
				cardCombatBonus_.AddEntriesFrom(ref input, _repeated_cardCombatBonus_codec);
				break;
			}
		}
	}
}
