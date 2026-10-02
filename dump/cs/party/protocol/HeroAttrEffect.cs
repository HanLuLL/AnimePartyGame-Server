using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class HeroAttrEffect : IMessage<HeroAttrEffect>, IMessage, IEquatable<HeroAttrEffect>, IDeepCloneable<HeroAttrEffect>, IBufferMessage
{
	public enum DataOneofCase
	{
		None = 0,
		Gold = 2,
		Hp = 3,
		Atk = 4,
		Def = 5,
		Buff = 6,
		Place = 7,
		Lottery = 8,
		Card = 9,
		Bomb = 10,
		Lv = 11,
		Cd = 12,
		Reroll = 13,
		SpecialScore = 14,
		CureNum = 15,
		SalaryNum = 16,
		MarkNum = 17,
		UseCardNum = 18,
		CardDistance = 19,
		CanCounter = 20,
		CounterNum = 21,
		ModifyNum = 22,
		NotSelect = 23,
		ConvertCard = 24,
		Disappear = 25,
		Combine = 26,
		AddMove = 27,
		UniqueNum = 28,
		AddTerm = 29,
		CardAtk = 30,
		EnergyNum = 31,
		CrimeNum = 32
	}

	private static readonly MessageParser<HeroAttrEffect> _parser = new MessageParser<HeroAttrEffect>(() => new HeroAttrEffect());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int GoldFieldNumber = 2;

	public const int HpFieldNumber = 3;

	public const int AtkFieldNumber = 4;

	public const int DefFieldNumber = 5;

	public const int BuffFieldNumber = 6;

	public const int PlaceFieldNumber = 7;

	public const int LotteryFieldNumber = 8;

	public const int CardFieldNumber = 9;

	public const int BombFieldNumber = 10;

	public const int LvFieldNumber = 11;

	public const int CdFieldNumber = 12;

	public const int RerollFieldNumber = 13;

	public const int SpecialScoreFieldNumber = 14;

	public const int CureNumFieldNumber = 15;

	public const int SalaryNumFieldNumber = 16;

	public const int MarkNumFieldNumber = 17;

	public const int UseCardNumFieldNumber = 18;

	public const int CardDistanceFieldNumber = 19;

	public const int CanCounterFieldNumber = 20;

	public const int CounterNumFieldNumber = 21;

	public const int ModifyNumFieldNumber = 22;

	public const int NotSelectFieldNumber = 23;

	public const int ConvertCardFieldNumber = 24;

	public const int DisappearFieldNumber = 25;

	public const int CombineFieldNumber = 26;

	public const int AddMoveFieldNumber = 27;

	public const int UniqueNumFieldNumber = 28;

	public const int AddTermFieldNumber = 29;

	public const int CardAtkFieldNumber = 30;

	public const int EnergyNumFieldNumber = 31;

	public const int CrimeNumFieldNumber = 32;

	private object data_;

	private DataOneofCase dataCase_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<HeroAttrEffect> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[444];

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
	public HeroGoldChangeS2C Gold
	{
		get
		{
			if (dataCase_ != DataOneofCase.Gold)
			{
				return null;
			}
			return (HeroGoldChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Gold : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroHpChangeS2C Hp
	{
		get
		{
			if (dataCase_ != DataOneofCase.Hp)
			{
				return null;
			}
			return (HeroHpChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Hp : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroAtkChangeS2C Atk
	{
		get
		{
			if (dataCase_ != DataOneofCase.Atk)
			{
				return null;
			}
			return (HeroAtkChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Atk : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroDefChangeS2C Def
	{
		get
		{
			if (dataCase_ != DataOneofCase.Def)
			{
				return null;
			}
			return (HeroDefChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Def : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroBuffChangeS2C Buff
	{
		get
		{
			if (dataCase_ != DataOneofCase.Buff)
			{
				return null;
			}
			return (HeroBuffChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Buff : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroPlaceChangeS2C Place
	{
		get
		{
			if (dataCase_ != DataOneofCase.Place)
			{
				return null;
			}
			return (HeroPlaceChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Place : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroLotteryChangeS2C Lottery
	{
		get
		{
			if (dataCase_ != DataOneofCase.Lottery)
			{
				return null;
			}
			return (HeroLotteryChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Lottery : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroCardChangeS2C Card
	{
		get
		{
			if (dataCase_ != DataOneofCase.Card)
			{
				return null;
			}
			return (HeroCardChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Card : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroBombChangeS2C Bomb
	{
		get
		{
			if (dataCase_ != DataOneofCase.Bomb)
			{
				return null;
			}
			return (HeroBombChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Bomb : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroUpLvS2C Lv
	{
		get
		{
			if (dataCase_ != DataOneofCase.Lv)
			{
				return null;
			}
			return (HeroUpLvS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Lv : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroCdChangeS2C Cd
	{
		get
		{
			if (dataCase_ != DataOneofCase.Cd)
			{
				return null;
			}
			return (HeroCdChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Cd : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroReRollChangeS2C Reroll
	{
		get
		{
			if (dataCase_ != DataOneofCase.Reroll)
			{
				return null;
			}
			return (HeroReRollChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Reroll : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroSpecialScoreChangeS2C SpecialScore
	{
		get
		{
			if (dataCase_ != DataOneofCase.SpecialScore)
			{
				return null;
			}
			return (HeroSpecialScoreChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.SpecialScore : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroCureNumChangeS2C CureNum
	{
		get
		{
			if (dataCase_ != DataOneofCase.CureNum)
			{
				return null;
			}
			return (HeroCureNumChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.CureNum : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroSalaryNumChangeS2C SalaryNum
	{
		get
		{
			if (dataCase_ != DataOneofCase.SalaryNum)
			{
				return null;
			}
			return (HeroSalaryNumChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.SalaryNum : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroMarkNumChangeS2C MarkNum
	{
		get
		{
			if (dataCase_ != DataOneofCase.MarkNum)
			{
				return null;
			}
			return (HeroMarkNumChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.MarkNum : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroUseCardNumChangeS2C UseCardNum
	{
		get
		{
			if (dataCase_ != DataOneofCase.UseCardNum)
			{
				return null;
			}
			return (HeroUseCardNumChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.UseCardNum : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardDistanceChangeS2C CardDistance
	{
		get
		{
			if (dataCase_ != DataOneofCase.CardDistance)
			{
				return null;
			}
			return (CardDistanceChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.CardDistance : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroCounterChangeS2C CanCounter
	{
		get
		{
			if (dataCase_ != DataOneofCase.CanCounter)
			{
				return null;
			}
			return (HeroCounterChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.CanCounter : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroCounterNumChangeS2C CounterNum
	{
		get
		{
			if (dataCase_ != DataOneofCase.CounterNum)
			{
				return null;
			}
			return (HeroCounterNumChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.CounterNum : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroModifyNumChangeS2C ModifyNum
	{
		get
		{
			if (dataCase_ != DataOneofCase.ModifyNum)
			{
				return null;
			}
			return (HeroModifyNumChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.ModifyNum : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroNotSelectS2C NotSelect
	{
		get
		{
			if (dataCase_ != DataOneofCase.NotSelect)
			{
				return null;
			}
			return (HeroNotSelectS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.NotSelect : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroCardConvertS2C ConvertCard
	{
		get
		{
			if (dataCase_ != DataOneofCase.ConvertCard)
			{
				return null;
			}
			return (HeroCardConvertS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.ConvertCard : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroDisappearS2C Disappear
	{
		get
		{
			if (dataCase_ != DataOneofCase.Disappear)
			{
				return null;
			}
			return (HeroDisappearS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Disappear : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveBossCombineS2C Combine
	{
		get
		{
			if (dataCase_ != DataOneofCase.Combine)
			{
				return null;
			}
			return (PveBossCombineS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.Combine : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroAddMoveChangeS2C AddMove
	{
		get
		{
			if (dataCase_ != DataOneofCase.AddMove)
			{
				return null;
			}
			return (HeroAddMoveChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.AddMove : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroUniqueNumChangeS2C UniqueNum
	{
		get
		{
			if (dataCase_ != DataOneofCase.UniqueNum)
			{
				return null;
			}
			return (HeroUniqueNumChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.UniqueNum : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomHeroTriggerAddTermS2C AddTerm
	{
		get
		{
			if (dataCase_ != DataOneofCase.AddTerm)
			{
				return null;
			}
			return (RoomHeroTriggerAddTermS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.AddTerm : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardAtkChangeS2C CardAtk
	{
		get
		{
			if (dataCase_ != DataOneofCase.CardAtk)
			{
				return null;
			}
			return (CardAtkChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.CardAtk : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroEnergyNumChangeS2C EnergyNum
	{
		get
		{
			if (dataCase_ != DataOneofCase.EnergyNum)
			{
				return null;
			}
			return (HeroEnergyNumChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.EnergyNum : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroCrimeNumChangeS2C CrimeNum
	{
		get
		{
			if (dataCase_ != DataOneofCase.CrimeNum)
			{
				return null;
			}
			return (HeroCrimeNumChangeS2C)data_;
		}
		set
		{
			data_ = value;
			dataCase_ = ((value != null) ? DataOneofCase.CrimeNum : DataOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DataOneofCase DataCase => dataCase_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroAttrEffect()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroAttrEffect(HeroAttrEffect other)
		: this()
	{
		playerId_ = other.playerId_;
		switch (other.DataCase)
		{
		case DataOneofCase.Gold:
			Gold = other.Gold.Clone();
			break;
		case DataOneofCase.Hp:
			Hp = other.Hp.Clone();
			break;
		case DataOneofCase.Atk:
			Atk = other.Atk.Clone();
			break;
		case DataOneofCase.Def:
			Def = other.Def.Clone();
			break;
		case DataOneofCase.Buff:
			Buff = other.Buff.Clone();
			break;
		case DataOneofCase.Place:
			Place = other.Place.Clone();
			break;
		case DataOneofCase.Lottery:
			Lottery = other.Lottery.Clone();
			break;
		case DataOneofCase.Card:
			Card = other.Card.Clone();
			break;
		case DataOneofCase.Bomb:
			Bomb = other.Bomb.Clone();
			break;
		case DataOneofCase.Lv:
			Lv = other.Lv.Clone();
			break;
		case DataOneofCase.Cd:
			Cd = other.Cd.Clone();
			break;
		case DataOneofCase.Reroll:
			Reroll = other.Reroll.Clone();
			break;
		case DataOneofCase.SpecialScore:
			SpecialScore = other.SpecialScore.Clone();
			break;
		case DataOneofCase.CureNum:
			CureNum = other.CureNum.Clone();
			break;
		case DataOneofCase.SalaryNum:
			SalaryNum = other.SalaryNum.Clone();
			break;
		case DataOneofCase.MarkNum:
			MarkNum = other.MarkNum.Clone();
			break;
		case DataOneofCase.UseCardNum:
			UseCardNum = other.UseCardNum.Clone();
			break;
		case DataOneofCase.CardDistance:
			CardDistance = other.CardDistance.Clone();
			break;
		case DataOneofCase.CanCounter:
			CanCounter = other.CanCounter.Clone();
			break;
		case DataOneofCase.CounterNum:
			CounterNum = other.CounterNum.Clone();
			break;
		case DataOneofCase.ModifyNum:
			ModifyNum = other.ModifyNum.Clone();
			break;
		case DataOneofCase.NotSelect:
			NotSelect = other.NotSelect.Clone();
			break;
		case DataOneofCase.ConvertCard:
			ConvertCard = other.ConvertCard.Clone();
			break;
		case DataOneofCase.Disappear:
			Disappear = other.Disappear.Clone();
			break;
		case DataOneofCase.Combine:
			Combine = other.Combine.Clone();
			break;
		case DataOneofCase.AddMove:
			AddMove = other.AddMove.Clone();
			break;
		case DataOneofCase.UniqueNum:
			UniqueNum = other.UniqueNum.Clone();
			break;
		case DataOneofCase.AddTerm:
			AddTerm = other.AddTerm.Clone();
			break;
		case DataOneofCase.CardAtk:
			CardAtk = other.CardAtk.Clone();
			break;
		case DataOneofCase.EnergyNum:
			EnergyNum = other.EnergyNum.Clone();
			break;
		case DataOneofCase.CrimeNum:
			CrimeNum = other.CrimeNum.Clone();
			break;
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroAttrEffect Clone()
	{
		return new HeroAttrEffect(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void ClearData()
	{
		dataCase_ = DataOneofCase.None;
		data_ = null;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HeroAttrEffect);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(HeroAttrEffect other)
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
		if (!object.Equals(Gold, other.Gold))
		{
			return false;
		}
		if (!object.Equals(Hp, other.Hp))
		{
			return false;
		}
		if (!object.Equals(Atk, other.Atk))
		{
			return false;
		}
		if (!object.Equals(Def, other.Def))
		{
			return false;
		}
		if (!object.Equals(Buff, other.Buff))
		{
			return false;
		}
		if (!object.Equals(Place, other.Place))
		{
			return false;
		}
		if (!object.Equals(Lottery, other.Lottery))
		{
			return false;
		}
		if (!object.Equals(Card, other.Card))
		{
			return false;
		}
		if (!object.Equals(Bomb, other.Bomb))
		{
			return false;
		}
		if (!object.Equals(Lv, other.Lv))
		{
			return false;
		}
		if (!object.Equals(Cd, other.Cd))
		{
			return false;
		}
		if (!object.Equals(Reroll, other.Reroll))
		{
			return false;
		}
		if (!object.Equals(SpecialScore, other.SpecialScore))
		{
			return false;
		}
		if (!object.Equals(CureNum, other.CureNum))
		{
			return false;
		}
		if (!object.Equals(SalaryNum, other.SalaryNum))
		{
			return false;
		}
		if (!object.Equals(MarkNum, other.MarkNum))
		{
			return false;
		}
		if (!object.Equals(UseCardNum, other.UseCardNum))
		{
			return false;
		}
		if (!object.Equals(CardDistance, other.CardDistance))
		{
			return false;
		}
		if (!object.Equals(CanCounter, other.CanCounter))
		{
			return false;
		}
		if (!object.Equals(CounterNum, other.CounterNum))
		{
			return false;
		}
		if (!object.Equals(ModifyNum, other.ModifyNum))
		{
			return false;
		}
		if (!object.Equals(NotSelect, other.NotSelect))
		{
			return false;
		}
		if (!object.Equals(ConvertCard, other.ConvertCard))
		{
			return false;
		}
		if (!object.Equals(Disappear, other.Disappear))
		{
			return false;
		}
		if (!object.Equals(Combine, other.Combine))
		{
			return false;
		}
		if (!object.Equals(AddMove, other.AddMove))
		{
			return false;
		}
		if (!object.Equals(UniqueNum, other.UniqueNum))
		{
			return false;
		}
		if (!object.Equals(AddTerm, other.AddTerm))
		{
			return false;
		}
		if (!object.Equals(CardAtk, other.CardAtk))
		{
			return false;
		}
		if (!object.Equals(EnergyNum, other.EnergyNum))
		{
			return false;
		}
		if (!object.Equals(CrimeNum, other.CrimeNum))
		{
			return false;
		}
		if (DataCase != other.DataCase)
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
		if (dataCase_ == DataOneofCase.Gold)
		{
			num ^= Gold.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Hp)
		{
			num ^= Hp.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Atk)
		{
			num ^= Atk.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Def)
		{
			num ^= Def.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Buff)
		{
			num ^= Buff.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Place)
		{
			num ^= Place.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Lottery)
		{
			num ^= Lottery.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Card)
		{
			num ^= Card.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Bomb)
		{
			num ^= Bomb.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Lv)
		{
			num ^= Lv.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Cd)
		{
			num ^= Cd.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Reroll)
		{
			num ^= Reroll.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.SpecialScore)
		{
			num ^= SpecialScore.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.CureNum)
		{
			num ^= CureNum.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.SalaryNum)
		{
			num ^= SalaryNum.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.MarkNum)
		{
			num ^= MarkNum.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.UseCardNum)
		{
			num ^= UseCardNum.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.CardDistance)
		{
			num ^= CardDistance.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.CanCounter)
		{
			num ^= CanCounter.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.CounterNum)
		{
			num ^= CounterNum.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.ModifyNum)
		{
			num ^= ModifyNum.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.NotSelect)
		{
			num ^= NotSelect.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.ConvertCard)
		{
			num ^= ConvertCard.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Disappear)
		{
			num ^= Disappear.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.Combine)
		{
			num ^= Combine.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.AddMove)
		{
			num ^= AddMove.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.UniqueNum)
		{
			num ^= UniqueNum.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.AddTerm)
		{
			num ^= AddTerm.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.CardAtk)
		{
			num ^= CardAtk.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.EnergyNum)
		{
			num ^= EnergyNum.GetHashCode();
		}
		if (dataCase_ == DataOneofCase.CrimeNum)
		{
			num ^= CrimeNum.GetHashCode();
		}
		num ^= (int)dataCase_;
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
		if (dataCase_ == DataOneofCase.Gold)
		{
			output.WriteRawTag(18);
			output.WriteMessage(Gold);
		}
		if (dataCase_ == DataOneofCase.Hp)
		{
			output.WriteRawTag(26);
			output.WriteMessage(Hp);
		}
		if (dataCase_ == DataOneofCase.Atk)
		{
			output.WriteRawTag(34);
			output.WriteMessage(Atk);
		}
		if (dataCase_ == DataOneofCase.Def)
		{
			output.WriteRawTag(42);
			output.WriteMessage(Def);
		}
		if (dataCase_ == DataOneofCase.Buff)
		{
			output.WriteRawTag(50);
			output.WriteMessage(Buff);
		}
		if (dataCase_ == DataOneofCase.Place)
		{
			output.WriteRawTag(58);
			output.WriteMessage(Place);
		}
		if (dataCase_ == DataOneofCase.Lottery)
		{
			output.WriteRawTag(66);
			output.WriteMessage(Lottery);
		}
		if (dataCase_ == DataOneofCase.Card)
		{
			output.WriteRawTag(74);
			output.WriteMessage(Card);
		}
		if (dataCase_ == DataOneofCase.Bomb)
		{
			output.WriteRawTag(82);
			output.WriteMessage(Bomb);
		}
		if (dataCase_ == DataOneofCase.Lv)
		{
			output.WriteRawTag(90);
			output.WriteMessage(Lv);
		}
		if (dataCase_ == DataOneofCase.Cd)
		{
			output.WriteRawTag(98);
			output.WriteMessage(Cd);
		}
		if (dataCase_ == DataOneofCase.Reroll)
		{
			output.WriteRawTag(106);
			output.WriteMessage(Reroll);
		}
		if (dataCase_ == DataOneofCase.SpecialScore)
		{
			output.WriteRawTag(114);
			output.WriteMessage(SpecialScore);
		}
		if (dataCase_ == DataOneofCase.CureNum)
		{
			output.WriteRawTag(122);
			output.WriteMessage(CureNum);
		}
		if (dataCase_ == DataOneofCase.SalaryNum)
		{
			output.WriteRawTag(130, 1);
			output.WriteMessage(SalaryNum);
		}
		if (dataCase_ == DataOneofCase.MarkNum)
		{
			output.WriteRawTag(138, 1);
			output.WriteMessage(MarkNum);
		}
		if (dataCase_ == DataOneofCase.UseCardNum)
		{
			output.WriteRawTag(146, 1);
			output.WriteMessage(UseCardNum);
		}
		if (dataCase_ == DataOneofCase.CardDistance)
		{
			output.WriteRawTag(154, 1);
			output.WriteMessage(CardDistance);
		}
		if (dataCase_ == DataOneofCase.CanCounter)
		{
			output.WriteRawTag(162, 1);
			output.WriteMessage(CanCounter);
		}
		if (dataCase_ == DataOneofCase.CounterNum)
		{
			output.WriteRawTag(170, 1);
			output.WriteMessage(CounterNum);
		}
		if (dataCase_ == DataOneofCase.ModifyNum)
		{
			output.WriteRawTag(178, 1);
			output.WriteMessage(ModifyNum);
		}
		if (dataCase_ == DataOneofCase.NotSelect)
		{
			output.WriteRawTag(186, 1);
			output.WriteMessage(NotSelect);
		}
		if (dataCase_ == DataOneofCase.ConvertCard)
		{
			output.WriteRawTag(194, 1);
			output.WriteMessage(ConvertCard);
		}
		if (dataCase_ == DataOneofCase.Disappear)
		{
			output.WriteRawTag(202, 1);
			output.WriteMessage(Disappear);
		}
		if (dataCase_ == DataOneofCase.Combine)
		{
			output.WriteRawTag(210, 1);
			output.WriteMessage(Combine);
		}
		if (dataCase_ == DataOneofCase.AddMove)
		{
			output.WriteRawTag(218, 1);
			output.WriteMessage(AddMove);
		}
		if (dataCase_ == DataOneofCase.UniqueNum)
		{
			output.WriteRawTag(226, 1);
			output.WriteMessage(UniqueNum);
		}
		if (dataCase_ == DataOneofCase.AddTerm)
		{
			output.WriteRawTag(234, 1);
			output.WriteMessage(AddTerm);
		}
		if (dataCase_ == DataOneofCase.CardAtk)
		{
			output.WriteRawTag(242, 1);
			output.WriteMessage(CardAtk);
		}
		if (dataCase_ == DataOneofCase.EnergyNum)
		{
			output.WriteRawTag(250, 1);
			output.WriteMessage(EnergyNum);
		}
		if (dataCase_ == DataOneofCase.CrimeNum)
		{
			output.WriteRawTag(130, 2);
			output.WriteMessage(CrimeNum);
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
		if (dataCase_ == DataOneofCase.Gold)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Gold);
		}
		if (dataCase_ == DataOneofCase.Hp)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Hp);
		}
		if (dataCase_ == DataOneofCase.Atk)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Atk);
		}
		if (dataCase_ == DataOneofCase.Def)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Def);
		}
		if (dataCase_ == DataOneofCase.Buff)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Buff);
		}
		if (dataCase_ == DataOneofCase.Place)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Place);
		}
		if (dataCase_ == DataOneofCase.Lottery)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Lottery);
		}
		if (dataCase_ == DataOneofCase.Card)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Card);
		}
		if (dataCase_ == DataOneofCase.Bomb)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Bomb);
		}
		if (dataCase_ == DataOneofCase.Lv)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Lv);
		}
		if (dataCase_ == DataOneofCase.Cd)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Cd);
		}
		if (dataCase_ == DataOneofCase.Reroll)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Reroll);
		}
		if (dataCase_ == DataOneofCase.SpecialScore)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SpecialScore);
		}
		if (dataCase_ == DataOneofCase.CureNum)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CureNum);
		}
		if (dataCase_ == DataOneofCase.SalaryNum)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SalaryNum);
		}
		if (dataCase_ == DataOneofCase.MarkNum)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MarkNum);
		}
		if (dataCase_ == DataOneofCase.UseCardNum)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UseCardNum);
		}
		if (dataCase_ == DataOneofCase.CardDistance)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CardDistance);
		}
		if (dataCase_ == DataOneofCase.CanCounter)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CanCounter);
		}
		if (dataCase_ == DataOneofCase.CounterNum)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CounterNum);
		}
		if (dataCase_ == DataOneofCase.ModifyNum)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ModifyNum);
		}
		if (dataCase_ == DataOneofCase.NotSelect)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(NotSelect);
		}
		if (dataCase_ == DataOneofCase.ConvertCard)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ConvertCard);
		}
		if (dataCase_ == DataOneofCase.Disappear)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Disappear);
		}
		if (dataCase_ == DataOneofCase.Combine)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Combine);
		}
		if (dataCase_ == DataOneofCase.AddMove)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(AddMove);
		}
		if (dataCase_ == DataOneofCase.UniqueNum)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UniqueNum);
		}
		if (dataCase_ == DataOneofCase.AddTerm)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(AddTerm);
		}
		if (dataCase_ == DataOneofCase.CardAtk)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CardAtk);
		}
		if (dataCase_ == DataOneofCase.EnergyNum)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(EnergyNum);
		}
		if (dataCase_ == DataOneofCase.CrimeNum)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CrimeNum);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(HeroAttrEffect other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PlayerId != 0L)
		{
			PlayerId = other.PlayerId;
		}
		switch (other.DataCase)
		{
		case DataOneofCase.Gold:
			if (Gold == null)
			{
				Gold = new HeroGoldChangeS2C();
			}
			Gold.MergeFrom(other.Gold);
			break;
		case DataOneofCase.Hp:
			if (Hp == null)
			{
				Hp = new HeroHpChangeS2C();
			}
			Hp.MergeFrom(other.Hp);
			break;
		case DataOneofCase.Atk:
			if (Atk == null)
			{
				Atk = new HeroAtkChangeS2C();
			}
			Atk.MergeFrom(other.Atk);
			break;
		case DataOneofCase.Def:
			if (Def == null)
			{
				Def = new HeroDefChangeS2C();
			}
			Def.MergeFrom(other.Def);
			break;
		case DataOneofCase.Buff:
			if (Buff == null)
			{
				Buff = new HeroBuffChangeS2C();
			}
			Buff.MergeFrom(other.Buff);
			break;
		case DataOneofCase.Place:
			if (Place == null)
			{
				Place = new HeroPlaceChangeS2C();
			}
			Place.MergeFrom(other.Place);
			break;
		case DataOneofCase.Lottery:
			if (Lottery == null)
			{
				Lottery = new HeroLotteryChangeS2C();
			}
			Lottery.MergeFrom(other.Lottery);
			break;
		case DataOneofCase.Card:
			if (Card == null)
			{
				Card = new HeroCardChangeS2C();
			}
			Card.MergeFrom(other.Card);
			break;
		case DataOneofCase.Bomb:
			if (Bomb == null)
			{
				Bomb = new HeroBombChangeS2C();
			}
			Bomb.MergeFrom(other.Bomb);
			break;
		case DataOneofCase.Lv:
			if (Lv == null)
			{
				Lv = new HeroUpLvS2C();
			}
			Lv.MergeFrom(other.Lv);
			break;
		case DataOneofCase.Cd:
			if (Cd == null)
			{
				Cd = new HeroCdChangeS2C();
			}
			Cd.MergeFrom(other.Cd);
			break;
		case DataOneofCase.Reroll:
			if (Reroll == null)
			{
				Reroll = new HeroReRollChangeS2C();
			}
			Reroll.MergeFrom(other.Reroll);
			break;
		case DataOneofCase.SpecialScore:
			if (SpecialScore == null)
			{
				SpecialScore = new HeroSpecialScoreChangeS2C();
			}
			SpecialScore.MergeFrom(other.SpecialScore);
			break;
		case DataOneofCase.CureNum:
			if (CureNum == null)
			{
				CureNum = new HeroCureNumChangeS2C();
			}
			CureNum.MergeFrom(other.CureNum);
			break;
		case DataOneofCase.SalaryNum:
			if (SalaryNum == null)
			{
				SalaryNum = new HeroSalaryNumChangeS2C();
			}
			SalaryNum.MergeFrom(other.SalaryNum);
			break;
		case DataOneofCase.MarkNum:
			if (MarkNum == null)
			{
				MarkNum = new HeroMarkNumChangeS2C();
			}
			MarkNum.MergeFrom(other.MarkNum);
			break;
		case DataOneofCase.UseCardNum:
			if (UseCardNum == null)
			{
				UseCardNum = new HeroUseCardNumChangeS2C();
			}
			UseCardNum.MergeFrom(other.UseCardNum);
			break;
		case DataOneofCase.CardDistance:
			if (CardDistance == null)
			{
				CardDistance = new CardDistanceChangeS2C();
			}
			CardDistance.MergeFrom(other.CardDistance);
			break;
		case DataOneofCase.CanCounter:
			if (CanCounter == null)
			{
				CanCounter = new HeroCounterChangeS2C();
			}
			CanCounter.MergeFrom(other.CanCounter);
			break;
		case DataOneofCase.CounterNum:
			if (CounterNum == null)
			{
				CounterNum = new HeroCounterNumChangeS2C();
			}
			CounterNum.MergeFrom(other.CounterNum);
			break;
		case DataOneofCase.ModifyNum:
			if (ModifyNum == null)
			{
				ModifyNum = new HeroModifyNumChangeS2C();
			}
			ModifyNum.MergeFrom(other.ModifyNum);
			break;
		case DataOneofCase.NotSelect:
			if (NotSelect == null)
			{
				NotSelect = new HeroNotSelectS2C();
			}
			NotSelect.MergeFrom(other.NotSelect);
			break;
		case DataOneofCase.ConvertCard:
			if (ConvertCard == null)
			{
				ConvertCard = new HeroCardConvertS2C();
			}
			ConvertCard.MergeFrom(other.ConvertCard);
			break;
		case DataOneofCase.Disappear:
			if (Disappear == null)
			{
				Disappear = new HeroDisappearS2C();
			}
			Disappear.MergeFrom(other.Disappear);
			break;
		case DataOneofCase.Combine:
			if (Combine == null)
			{
				Combine = new PveBossCombineS2C();
			}
			Combine.MergeFrom(other.Combine);
			break;
		case DataOneofCase.AddMove:
			if (AddMove == null)
			{
				AddMove = new HeroAddMoveChangeS2C();
			}
			AddMove.MergeFrom(other.AddMove);
			break;
		case DataOneofCase.UniqueNum:
			if (UniqueNum == null)
			{
				UniqueNum = new HeroUniqueNumChangeS2C();
			}
			UniqueNum.MergeFrom(other.UniqueNum);
			break;
		case DataOneofCase.AddTerm:
			if (AddTerm == null)
			{
				AddTerm = new RoomHeroTriggerAddTermS2C();
			}
			AddTerm.MergeFrom(other.AddTerm);
			break;
		case DataOneofCase.CardAtk:
			if (CardAtk == null)
			{
				CardAtk = new CardAtkChangeS2C();
			}
			CardAtk.MergeFrom(other.CardAtk);
			break;
		case DataOneofCase.EnergyNum:
			if (EnergyNum == null)
			{
				EnergyNum = new HeroEnergyNumChangeS2C();
			}
			EnergyNum.MergeFrom(other.EnergyNum);
			break;
		case DataOneofCase.CrimeNum:
			if (CrimeNum == null)
			{
				CrimeNum = new HeroCrimeNumChangeS2C();
			}
			CrimeNum.MergeFrom(other.CrimeNum);
			break;
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 18u:
			{
				HeroGoldChangeS2C heroGoldChangeS2C = new HeroGoldChangeS2C();
				if (dataCase_ == DataOneofCase.Gold)
				{
					heroGoldChangeS2C.MergeFrom(Gold);
				}
				input.ReadMessage(heroGoldChangeS2C);
				Gold = heroGoldChangeS2C;
				break;
			}
			case 26u:
			{
				HeroHpChangeS2C heroHpChangeS2C = new HeroHpChangeS2C();
				if (dataCase_ == DataOneofCase.Hp)
				{
					heroHpChangeS2C.MergeFrom(Hp);
				}
				input.ReadMessage(heroHpChangeS2C);
				Hp = heroHpChangeS2C;
				break;
			}
			case 34u:
			{
				HeroAtkChangeS2C heroAtkChangeS2C = new HeroAtkChangeS2C();
				if (dataCase_ == DataOneofCase.Atk)
				{
					heroAtkChangeS2C.MergeFrom(Atk);
				}
				input.ReadMessage(heroAtkChangeS2C);
				Atk = heroAtkChangeS2C;
				break;
			}
			case 42u:
			{
				HeroDefChangeS2C heroDefChangeS2C = new HeroDefChangeS2C();
				if (dataCase_ == DataOneofCase.Def)
				{
					heroDefChangeS2C.MergeFrom(Def);
				}
				input.ReadMessage(heroDefChangeS2C);
				Def = heroDefChangeS2C;
				break;
			}
			case 50u:
			{
				HeroBuffChangeS2C heroBuffChangeS2C = new HeroBuffChangeS2C();
				if (dataCase_ == DataOneofCase.Buff)
				{
					heroBuffChangeS2C.MergeFrom(Buff);
				}
				input.ReadMessage(heroBuffChangeS2C);
				Buff = heroBuffChangeS2C;
				break;
			}
			case 58u:
			{
				HeroPlaceChangeS2C heroPlaceChangeS2C = new HeroPlaceChangeS2C();
				if (dataCase_ == DataOneofCase.Place)
				{
					heroPlaceChangeS2C.MergeFrom(Place);
				}
				input.ReadMessage(heroPlaceChangeS2C);
				Place = heroPlaceChangeS2C;
				break;
			}
			case 66u:
			{
				HeroLotteryChangeS2C heroLotteryChangeS2C = new HeroLotteryChangeS2C();
				if (dataCase_ == DataOneofCase.Lottery)
				{
					heroLotteryChangeS2C.MergeFrom(Lottery);
				}
				input.ReadMessage(heroLotteryChangeS2C);
				Lottery = heroLotteryChangeS2C;
				break;
			}
			case 74u:
			{
				HeroCardChangeS2C heroCardChangeS2C = new HeroCardChangeS2C();
				if (dataCase_ == DataOneofCase.Card)
				{
					heroCardChangeS2C.MergeFrom(Card);
				}
				input.ReadMessage(heroCardChangeS2C);
				Card = heroCardChangeS2C;
				break;
			}
			case 82u:
			{
				HeroBombChangeS2C heroBombChangeS2C = new HeroBombChangeS2C();
				if (dataCase_ == DataOneofCase.Bomb)
				{
					heroBombChangeS2C.MergeFrom(Bomb);
				}
				input.ReadMessage(heroBombChangeS2C);
				Bomb = heroBombChangeS2C;
				break;
			}
			case 90u:
			{
				HeroUpLvS2C heroUpLvS2C = new HeroUpLvS2C();
				if (dataCase_ == DataOneofCase.Lv)
				{
					heroUpLvS2C.MergeFrom(Lv);
				}
				input.ReadMessage(heroUpLvS2C);
				Lv = heroUpLvS2C;
				break;
			}
			case 98u:
			{
				HeroCdChangeS2C heroCdChangeS2C = new HeroCdChangeS2C();
				if (dataCase_ == DataOneofCase.Cd)
				{
					heroCdChangeS2C.MergeFrom(Cd);
				}
				input.ReadMessage(heroCdChangeS2C);
				Cd = heroCdChangeS2C;
				break;
			}
			case 106u:
			{
				HeroReRollChangeS2C heroReRollChangeS2C = new HeroReRollChangeS2C();
				if (dataCase_ == DataOneofCase.Reroll)
				{
					heroReRollChangeS2C.MergeFrom(Reroll);
				}
				input.ReadMessage(heroReRollChangeS2C);
				Reroll = heroReRollChangeS2C;
				break;
			}
			case 114u:
			{
				HeroSpecialScoreChangeS2C heroSpecialScoreChangeS2C = new HeroSpecialScoreChangeS2C();
				if (dataCase_ == DataOneofCase.SpecialScore)
				{
					heroSpecialScoreChangeS2C.MergeFrom(SpecialScore);
				}
				input.ReadMessage(heroSpecialScoreChangeS2C);
				SpecialScore = heroSpecialScoreChangeS2C;
				break;
			}
			case 122u:
			{
				HeroCureNumChangeS2C heroCureNumChangeS2C = new HeroCureNumChangeS2C();
				if (dataCase_ == DataOneofCase.CureNum)
				{
					heroCureNumChangeS2C.MergeFrom(CureNum);
				}
				input.ReadMessage(heroCureNumChangeS2C);
				CureNum = heroCureNumChangeS2C;
				break;
			}
			case 130u:
			{
				HeroSalaryNumChangeS2C heroSalaryNumChangeS2C = new HeroSalaryNumChangeS2C();
				if (dataCase_ == DataOneofCase.SalaryNum)
				{
					heroSalaryNumChangeS2C.MergeFrom(SalaryNum);
				}
				input.ReadMessage(heroSalaryNumChangeS2C);
				SalaryNum = heroSalaryNumChangeS2C;
				break;
			}
			case 138u:
			{
				HeroMarkNumChangeS2C heroMarkNumChangeS2C = new HeroMarkNumChangeS2C();
				if (dataCase_ == DataOneofCase.MarkNum)
				{
					heroMarkNumChangeS2C.MergeFrom(MarkNum);
				}
				input.ReadMessage(heroMarkNumChangeS2C);
				MarkNum = heroMarkNumChangeS2C;
				break;
			}
			case 146u:
			{
				HeroUseCardNumChangeS2C heroUseCardNumChangeS2C = new HeroUseCardNumChangeS2C();
				if (dataCase_ == DataOneofCase.UseCardNum)
				{
					heroUseCardNumChangeS2C.MergeFrom(UseCardNum);
				}
				input.ReadMessage(heroUseCardNumChangeS2C);
				UseCardNum = heroUseCardNumChangeS2C;
				break;
			}
			case 154u:
			{
				CardDistanceChangeS2C cardDistanceChangeS2C = new CardDistanceChangeS2C();
				if (dataCase_ == DataOneofCase.CardDistance)
				{
					cardDistanceChangeS2C.MergeFrom(CardDistance);
				}
				input.ReadMessage(cardDistanceChangeS2C);
				CardDistance = cardDistanceChangeS2C;
				break;
			}
			case 162u:
			{
				HeroCounterChangeS2C heroCounterChangeS2C = new HeroCounterChangeS2C();
				if (dataCase_ == DataOneofCase.CanCounter)
				{
					heroCounterChangeS2C.MergeFrom(CanCounter);
				}
				input.ReadMessage(heroCounterChangeS2C);
				CanCounter = heroCounterChangeS2C;
				break;
			}
			case 170u:
			{
				HeroCounterNumChangeS2C heroCounterNumChangeS2C = new HeroCounterNumChangeS2C();
				if (dataCase_ == DataOneofCase.CounterNum)
				{
					heroCounterNumChangeS2C.MergeFrom(CounterNum);
				}
				input.ReadMessage(heroCounterNumChangeS2C);
				CounterNum = heroCounterNumChangeS2C;
				break;
			}
			case 178u:
			{
				HeroModifyNumChangeS2C heroModifyNumChangeS2C = new HeroModifyNumChangeS2C();
				if (dataCase_ == DataOneofCase.ModifyNum)
				{
					heroModifyNumChangeS2C.MergeFrom(ModifyNum);
				}
				input.ReadMessage(heroModifyNumChangeS2C);
				ModifyNum = heroModifyNumChangeS2C;
				break;
			}
			case 186u:
			{
				HeroNotSelectS2C heroNotSelectS2C = new HeroNotSelectS2C();
				if (dataCase_ == DataOneofCase.NotSelect)
				{
					heroNotSelectS2C.MergeFrom(NotSelect);
				}
				input.ReadMessage(heroNotSelectS2C);
				NotSelect = heroNotSelectS2C;
				break;
			}
			case 194u:
			{
				HeroCardConvertS2C heroCardConvertS2C = new HeroCardConvertS2C();
				if (dataCase_ == DataOneofCase.ConvertCard)
				{
					heroCardConvertS2C.MergeFrom(ConvertCard);
				}
				input.ReadMessage(heroCardConvertS2C);
				ConvertCard = heroCardConvertS2C;
				break;
			}
			case 202u:
			{
				HeroDisappearS2C heroDisappearS2C = new HeroDisappearS2C();
				if (dataCase_ == DataOneofCase.Disappear)
				{
					heroDisappearS2C.MergeFrom(Disappear);
				}
				input.ReadMessage(heroDisappearS2C);
				Disappear = heroDisappearS2C;
				break;
			}
			case 210u:
			{
				PveBossCombineS2C pveBossCombineS2C = new PveBossCombineS2C();
				if (dataCase_ == DataOneofCase.Combine)
				{
					pveBossCombineS2C.MergeFrom(Combine);
				}
				input.ReadMessage(pveBossCombineS2C);
				Combine = pveBossCombineS2C;
				break;
			}
			case 218u:
			{
				HeroAddMoveChangeS2C heroAddMoveChangeS2C = new HeroAddMoveChangeS2C();
				if (dataCase_ == DataOneofCase.AddMove)
				{
					heroAddMoveChangeS2C.MergeFrom(AddMove);
				}
				input.ReadMessage(heroAddMoveChangeS2C);
				AddMove = heroAddMoveChangeS2C;
				break;
			}
			case 226u:
			{
				HeroUniqueNumChangeS2C heroUniqueNumChangeS2C = new HeroUniqueNumChangeS2C();
				if (dataCase_ == DataOneofCase.UniqueNum)
				{
					heroUniqueNumChangeS2C.MergeFrom(UniqueNum);
				}
				input.ReadMessage(heroUniqueNumChangeS2C);
				UniqueNum = heroUniqueNumChangeS2C;
				break;
			}
			case 234u:
			{
				RoomHeroTriggerAddTermS2C roomHeroTriggerAddTermS2C = new RoomHeroTriggerAddTermS2C();
				if (dataCase_ == DataOneofCase.AddTerm)
				{
					roomHeroTriggerAddTermS2C.MergeFrom(AddTerm);
				}
				input.ReadMessage(roomHeroTriggerAddTermS2C);
				AddTerm = roomHeroTriggerAddTermS2C;
				break;
			}
			case 242u:
			{
				CardAtkChangeS2C cardAtkChangeS2C = new CardAtkChangeS2C();
				if (dataCase_ == DataOneofCase.CardAtk)
				{
					cardAtkChangeS2C.MergeFrom(CardAtk);
				}
				input.ReadMessage(cardAtkChangeS2C);
				CardAtk = cardAtkChangeS2C;
				break;
			}
			case 250u:
			{
				HeroEnergyNumChangeS2C heroEnergyNumChangeS2C = new HeroEnergyNumChangeS2C();
				if (dataCase_ == DataOneofCase.EnergyNum)
				{
					heroEnergyNumChangeS2C.MergeFrom(EnergyNum);
				}
				input.ReadMessage(heroEnergyNumChangeS2C);
				EnergyNum = heroEnergyNumChangeS2C;
				break;
			}
			case 258u:
			{
				HeroCrimeNumChangeS2C heroCrimeNumChangeS2C = new HeroCrimeNumChangeS2C();
				if (dataCase_ == DataOneofCase.CrimeNum)
				{
					heroCrimeNumChangeS2C.MergeFrom(CrimeNum);
				}
				input.ReadMessage(heroCrimeNumChangeS2C);
				CrimeNum = heroCrimeNumChangeS2C;
				break;
			}
			}
		}
	}
}
