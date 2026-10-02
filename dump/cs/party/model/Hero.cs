using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Hero : IMessage<Hero>, IMessage, IEquatable<Hero>, IDeepCloneable<Hero>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum MonsterType
		{
			[OriginalName("NONE")]
			None = 0,
			[OriginalName("Thief")]
			Thief = 1,
			[OriginalName("BOSS")]
			Boss = 2,
			[OriginalName("PVE_BOSS")]
			PveBoss = 10,
			[OriginalName("PVE_MONSTER")]
			PveMonster = 11,
			[OriginalName("PVE_ELITE_MONSTER")]
			PveEliteMonster = 12,
			[OriginalName("PVE_ALLY_MONSTER")]
			PveAllyMonster = 30
		}

		public enum OfflineType
		{
			[OriginalName("OfflineType_None")]
			None,
			[OriginalName("OfflineType_WithoutReconnect")]
			WithoutReconnect,
			[OriginalName("OfflineType_WithReconnect")]
			WithReconnect,
			[OriginalName("OfflineType_OverThreeMinute")]
			OverThreeMinute
		}
	}

	private static readonly MessageParser<Hero> _parser = new MessageParser<Hero>(() => new Hero());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int HeroIdFieldNumber = 2;

	private int heroId_;

	public const int NodeIdFieldNumber = 3;

	private int nodeId_;

	public const int BuffsFieldNumber = 4;

	private static readonly MapField<long, Buff>.Codec _map_buffs_codec = new MapField<long, Buff>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, Buff.Parser), 34u);

	private readonly MapField<long, Buff> buffs_ = new MapField<long, Buff>();

	public const int RoundFieldNumber = 5;

	private int round_;

	public const int BeBornNodeIdFieldNumber = 6;

	private int beBornNodeId_;

	public const int GoldFieldNumber = 7;

	private int gold_;

	public const int CardsFieldNumber = 8;

	private static readonly FieldCodec<CardInfo> _repeated_cards_codec = FieldCodec.ForMessage(66u, CardInfo.Parser);

	private readonly RepeatedField<CardInfo> cards_ = new RepeatedField<CardInfo>();

	public const int MovePointFieldNumber = 10;

	private int movePoint_;

	public const int FrontNodeIdsFieldNumber = 11;

	private static readonly FieldCodec<int> _repeated_frontNodeIds_codec = FieldCodec.ForSFixed32(90u);

	private readonly RepeatedField<int> frontNodeIds_ = new RepeatedField<int>();

	public const int BackNodeIdFieldNumber = 12;

	private int backNodeId_;

	public const int UseTimeFieldNumber = 13;

	private int useTime_;

	public const int HpFieldNumber = 14;

	private int hp_;

	public const int DefenseFieldNumber = 15;

	private int defense_;

	public const int AttackFieldNumber = 16;

	private int attack_;

	public const int BattleIdFieldNumber = 17;

	private long battleId_;

	public const int LotterysFieldNumber = 18;

	private static readonly MapField<int, bool>.Codec _map_lotterys_codec = new MapField<int, bool>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForBool(16u, defaultValue: false), 146u);

	private readonly MapField<int, bool> lotterys_ = new MapField<int, bool>();

	public const int AskPlayerIdsFieldNumber = 19;

	private static readonly FieldCodec<long> _repeated_askPlayerIds_codec = FieldCodec.ForSFixed64(154u);

	private readonly RepeatedField<long> askPlayerIds_ = new RepeatedField<long>();

	public const int AppendMovePoint1FieldNumber = 20;

	private int appendMovePoint1_;

	public const int AppendMovePoint2FieldNumber = 21;

	private int appendMovePoint2_;

	public const int BombsFieldNumber = 22;

	private static readonly FieldCodec<Bomb> _repeated_bombs_codec = FieldCodec.ForMessage(178u, Bomb.Parser);

	private readonly RepeatedField<Bomb> bombs_ = new RepeatedField<Bomb>();

	public const int MaxHpFieldNumber = 23;

	private int maxHp_;

	public const int IsDoubleThrowDiceFieldNumber = 24;

	private bool isDoubleThrowDice_;

	public const int IsStopRoundFieldNumber = 25;

	private bool isStopRound_;

	public const int RealMoveFieldNumber = 26;

	private int realMove_;

	public const int IsBigShopFieldNumber = 27;

	private bool isBigShop_;

	public const int BigShopEffectCardNumFieldNumber = 28;

	private int bigShopEffectCardNum_;

	public const int BigShopCombatCardNumFieldNumber = 29;

	private int bigShopCombatCardNum_;

	public const int UserCardNumFieldNumber = 30;

	private int userCardNum_;

	public const int IsControlMovePointFieldNumber = 31;

	private bool isControlMovePoint_;

	public const int AdditionAttrsFieldNumber = 32;

	private static readonly MapField<long, AdditionAttribute>.Codec _map_additionAttrs_codec = new MapField<long, AdditionAttribute>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, AdditionAttribute.Parser), 258u);

	private readonly MapField<long, AdditionAttribute> additionAttrs_ = new MapField<long, AdditionAttribute>();

	public const int LvFieldNumber = 33;

	private int lv_;

	public const int ForceDirFieldNumber = 34;

	private bool forceDir_;

	public const int LandNoHandleFieldNumber = 35;

	private bool landNoHandle_;

	public const int EventTriggerNumFieldNumber = 36;

	private int eventTriggerNum_;

	public const int EventIdsFieldNumber = 37;

	private static readonly MapField<int, bool>.Codec _map_eventIds_codec = new MapField<int, bool>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForBool(16u, defaultValue: false), 298u);

	private readonly MapField<int, bool> eventIds_ = new MapField<int, bool>();

	public const int SkillCdsFieldNumber = 38;

	private static readonly MapField<int, int>.Codec _map_skillCds_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 306u);

	private readonly MapField<int, int> skillCds_ = new MapField<int, int>();

	public const int UseSkillNumFieldNumber = 39;

	private int useSkillNum_;

	public const int AffirmFieldNumber = 40;

	private bool affirm_;

	public const int DevNextEventIdFieldNumber = 41;

	private int devNextEventId_;

	public const int DevNextDestinyIdFieldNumber = 42;

	private int devNextDestinyId_;

	public const int DevNextDivinationIdFieldNumber = 43;

	private int devNextDivinationId_;

	public const int BossTargetIdFieldNumber = 44;

	private long bossTargetId_;

	public const int LastChatTimeFieldNumber = 45;

	private long lastChatTime_;

	public const int StandingPaintingFieldNumber = 46;

	private int standingPainting_;

	public const int CondFieldNumber = 47;

	private ConditionalInfo cond_;

	public const int IsLaunchFightFieldNumber = 48;

	private bool isLaunchFight_;

	public const int MonsterTypeFieldNumber = 49;

	private Types.MonsterType monsterType_;

	public const int ActionWeightFieldNumber = 50;

	private int actionWeight_;

	public const int SelectRelicsFieldNumber = 52;

	private static readonly MapField<int, bool>.Codec _map_selectRelics_codec = new MapField<int, bool>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForBool(16u, defaultValue: false), 418u);

	private readonly MapField<int, bool> selectRelics_ = new MapField<int, bool>();

	public const int MonsterIndexFieldNumber = 53;

	private int monsterIndex_;

	public const int DoubleThrowDiceSkillFieldNumber = 54;

	private int doubleThrowDiceSkill_;

	public const int PveHeroStrengthenFieldNumber = 55;

	private PveHeroStrengthen pveHeroStrengthen_;

	public const int BattleCountFieldNumber = 56;

	private int battleCount_;

	public const int ReRollNumFieldNumber = 57;

	private int reRollNum_;

	public const int DevMovePointFieldNumber = 58;

	private int devMovePoint_;

	public const int DevAttackPointFieldNumber = 59;

	private int devAttackPoint_;

	public const int DevDefensePointFieldNumber = 60;

	private int devDefensePoint_;

	public const int TeamIdFieldNumber = 61;

	private int teamId_;

	public const int SpecialScoreFieldNumber = 62;

	private int specialScore_;

	public const int ReviveRoundNumFieldNumber = 63;

	private int reviveRoundNum_;

	public const int CureNumFieldNumber = 64;

	private int cureNum_;

	public const int SalaryNumFieldNumber = 65;

	private int salaryNum_;

	public const int MarkNumFieldNumber = 66;

	private int markNum_;

	public const int IsTrialFieldNumber = 67;

	private bool isTrial_;

	public const int BuyRelicNumFieldNumber = 68;

	private int buyRelicNum_;

	public const int UseCardMaxNumFieldNumber = 69;

	private int useCardMaxNum_;

	public const int SkinPendantFieldNumber = 70;

	private int skinPendant_;

	public const int CardDistanceFieldNumber = 71;

	private int cardDistance_;

	public const int CanCounterFieldNumber = 72;

	private bool canCounter_;

	public const int CounterNumFieldNumber = 73;

	private int counterNum_;

	public const int ModityNumFieldNumber = 74;

	private int modityNum_;

	public const int NotSelectFieldNumber = 75;

	private bool notSelect_;

	public const int MoveEffectSkillIdFieldNumber = 76;

	private int moveEffectSkillId_;

	public const int TimeOutNumFieldNumber = 77;

	private int timeOutNum_;

	public const int CardDiscountFieldNumber = 78;

	private int cardDiscount_;

	public const int DisCardPriceFieldNumber = 79;

	private int disCardPrice_;

	public const int CardIdxFieldNumber = 80;

	private int cardIdx_;

	public const int DiceWeightIndexFieldNumber = 81;

	private int diceWeightIndex_;

	public const int LandBuffHandleFieldNumber = 82;

	private bool landBuffHandle_;

	public const int HasSendChatFieldNumber = 84;

	private bool hasSendChat_;

	public const int HasSendPhraseFieldNumber = 85;

	private bool hasSendPhrase_;

	public const int OfflineTimeFieldNumber = 86;

	private long offlineTime_;

	public const int OfflineTypeFieldNumber = 87;

	private Types.OfflineType offlineType_;

	public const int UniqueNumFieldNumber = 88;

	private int uniqueNum_;

	public const int CardAtkFieldNumber = 89;

	private int cardAtk_;

	public const int EnergyNumFieldNumber = 90;

	private int energyNum_;

	public const int IsSkipRoundFieldNumber = 91;

	private bool isSkipRound_;

	public const int CrimeNumFieldNumber = 92;

	private int crimeNum_;

	public const int DoneActionsFieldNumber = 1000;

	private static readonly MapField<long, bool>.Codec _map_doneActions_codec = new MapField<long, bool>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForBool(16u, defaultValue: false), 8002u);

	private readonly MapField<long, bool> doneActions_ = new MapField<long, bool>();

	public const int ActionEndFieldNumber = 1001;

	private bool actionEnd_;

	public const int OnlyUseCardFieldNumber = 1002;

	private bool onlyUseCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Hero> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[58];

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
	public int NodeId
	{
		get
		{
			return nodeId_;
		}
		set
		{
			nodeId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, Buff> Buffs => buffs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Round
	{
		get
		{
			return round_;
		}
		set
		{
			round_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BeBornNodeId
	{
		get
		{
			return beBornNodeId_;
		}
		set
		{
			beBornNodeId_ = value;
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
	public RepeatedField<CardInfo> Cards => cards_;

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
	public RepeatedField<int> FrontNodeIds => frontNodeIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BackNodeId
	{
		get
		{
			return backNodeId_;
		}
		set
		{
			backNodeId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseTime
	{
		get
		{
			return useTime_;
		}
		set
		{
			useTime_ = value;
		}
	}

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
	public long BattleId
	{
		get
		{
			return battleId_;
		}
		set
		{
			battleId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, bool> Lotterys => lotterys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> AskPlayerIds => askPlayerIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AppendMovePoint1
	{
		get
		{
			return appendMovePoint1_;
		}
		set
		{
			appendMovePoint1_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AppendMovePoint2
	{
		get
		{
			return appendMovePoint2_;
		}
		set
		{
			appendMovePoint2_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Bomb> Bombs => bombs_;

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
	public bool IsDoubleThrowDice
	{
		get
		{
			return isDoubleThrowDice_;
		}
		set
		{
			isDoubleThrowDice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsStopRound
	{
		get
		{
			return isStopRound_;
		}
		set
		{
			isStopRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RealMove
	{
		get
		{
			return realMove_;
		}
		set
		{
			realMove_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsBigShop
	{
		get
		{
			return isBigShop_;
		}
		set
		{
			isBigShop_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BigShopEffectCardNum
	{
		get
		{
			return bigShopEffectCardNum_;
		}
		set
		{
			bigShopEffectCardNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BigShopCombatCardNum
	{
		get
		{
			return bigShopCombatCardNum_;
		}
		set
		{
			bigShopCombatCardNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UserCardNum
	{
		get
		{
			return userCardNum_;
		}
		set
		{
			userCardNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsControlMovePoint
	{
		get
		{
			return isControlMovePoint_;
		}
		set
		{
			isControlMovePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, AdditionAttribute> AdditionAttrs => additionAttrs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Lv
	{
		get
		{
			return lv_;
		}
		set
		{
			lv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool ForceDir
	{
		get
		{
			return forceDir_;
		}
		set
		{
			forceDir_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool LandNoHandle
	{
		get
		{
			return landNoHandle_;
		}
		set
		{
			landNoHandle_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EventTriggerNum
	{
		get
		{
			return eventTriggerNum_;
		}
		set
		{
			eventTriggerNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, bool> EventIds => eventIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> SkillCds => skillCds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseSkillNum
	{
		get
		{
			return useSkillNum_;
		}
		set
		{
			useSkillNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Affirm
	{
		get
		{
			return affirm_;
		}
		set
		{
			affirm_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DevNextEventId
	{
		get
		{
			return devNextEventId_;
		}
		set
		{
			devNextEventId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DevNextDestinyId
	{
		get
		{
			return devNextDestinyId_;
		}
		set
		{
			devNextDestinyId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DevNextDivinationId
	{
		get
		{
			return devNextDivinationId_;
		}
		set
		{
			devNextDivinationId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long BossTargetId
	{
		get
		{
			return bossTargetId_;
		}
		set
		{
			bossTargetId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastChatTime
	{
		get
		{
			return lastChatTime_;
		}
		set
		{
			lastChatTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int StandingPainting
	{
		get
		{
			return standingPainting_;
		}
		set
		{
			standingPainting_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConditionalInfo Cond
	{
		get
		{
			return cond_;
		}
		set
		{
			cond_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsLaunchFight
	{
		get
		{
			return isLaunchFight_;
		}
		set
		{
			isLaunchFight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.MonsterType MonsterType
	{
		get
		{
			return monsterType_;
		}
		set
		{
			monsterType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActionWeight
	{
		get
		{
			return actionWeight_;
		}
		set
		{
			actionWeight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, bool> SelectRelics => selectRelics_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MonsterIndex
	{
		get
		{
			return monsterIndex_;
		}
		set
		{
			monsterIndex_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DoubleThrowDiceSkill
	{
		get
		{
			return doubleThrowDiceSkill_;
		}
		set
		{
			doubleThrowDiceSkill_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroStrengthen PveHeroStrengthen
	{
		get
		{
			return pveHeroStrengthen_;
		}
		set
		{
			pveHeroStrengthen_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BattleCount
	{
		get
		{
			return battleCount_;
		}
		set
		{
			battleCount_ = value;
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
	public int DevMovePoint
	{
		get
		{
			return devMovePoint_;
		}
		set
		{
			devMovePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DevAttackPoint
	{
		get
		{
			return devAttackPoint_;
		}
		set
		{
			devAttackPoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DevDefensePoint
	{
		get
		{
			return devDefensePoint_;
		}
		set
		{
			devDefensePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TeamId
	{
		get
		{
			return teamId_;
		}
		set
		{
			teamId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SpecialScore
	{
		get
		{
			return specialScore_;
		}
		set
		{
			specialScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ReviveRoundNum
	{
		get
		{
			return reviveRoundNum_;
		}
		set
		{
			reviveRoundNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CureNum
	{
		get
		{
			return cureNum_;
		}
		set
		{
			cureNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SalaryNum
	{
		get
		{
			return salaryNum_;
		}
		set
		{
			salaryNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MarkNum
	{
		get
		{
			return markNum_;
		}
		set
		{
			markNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsTrial
	{
		get
		{
			return isTrial_;
		}
		set
		{
			isTrial_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BuyRelicNum
	{
		get
		{
			return buyRelicNum_;
		}
		set
		{
			buyRelicNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseCardMaxNum
	{
		get
		{
			return useCardMaxNum_;
		}
		set
		{
			useCardMaxNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkinPendant
	{
		get
		{
			return skinPendant_;
		}
		set
		{
			skinPendant_ = value;
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
	public bool CanCounter
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
	public int CounterNum
	{
		get
		{
			return counterNum_;
		}
		set
		{
			counterNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ModityNum
	{
		get
		{
			return modityNum_;
		}
		set
		{
			modityNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NotSelect
	{
		get
		{
			return notSelect_;
		}
		set
		{
			notSelect_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MoveEffectSkillId
	{
		get
		{
			return moveEffectSkillId_;
		}
		set
		{
			moveEffectSkillId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TimeOutNum
	{
		get
		{
			return timeOutNum_;
		}
		set
		{
			timeOutNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardDiscount
	{
		get
		{
			return cardDiscount_;
		}
		set
		{
			cardDiscount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DisCardPrice
	{
		get
		{
			return disCardPrice_;
		}
		set
		{
			disCardPrice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardIdx
	{
		get
		{
			return cardIdx_;
		}
		set
		{
			cardIdx_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiceWeightIndex
	{
		get
		{
			return diceWeightIndex_;
		}
		set
		{
			diceWeightIndex_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool LandBuffHandle
	{
		get
		{
			return landBuffHandle_;
		}
		set
		{
			landBuffHandle_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool HasSendChat
	{
		get
		{
			return hasSendChat_;
		}
		set
		{
			hasSendChat_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool HasSendPhrase
	{
		get
		{
			return hasSendPhrase_;
		}
		set
		{
			hasSendPhrase_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long OfflineTime
	{
		get
		{
			return offlineTime_;
		}
		set
		{
			offlineTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.OfflineType OfflineType
	{
		get
		{
			return offlineType_;
		}
		set
		{
			offlineType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UniqueNum
	{
		get
		{
			return uniqueNum_;
		}
		set
		{
			uniqueNum_ = value;
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
	public int EnergyNum
	{
		get
		{
			return energyNum_;
		}
		set
		{
			energyNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsSkipRound
	{
		get
		{
			return isSkipRound_;
		}
		set
		{
			isSkipRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CrimeNum
	{
		get
		{
			return crimeNum_;
		}
		set
		{
			crimeNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, bool> DoneActions => doneActions_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool ActionEnd
	{
		get
		{
			return actionEnd_;
		}
		set
		{
			actionEnd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool OnlyUseCard
	{
		get
		{
			return onlyUseCard_;
		}
		set
		{
			onlyUseCard_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Hero()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Hero(Hero other)
		: this()
	{
		playerId_ = other.playerId_;
		heroId_ = other.heroId_;
		nodeId_ = other.nodeId_;
		buffs_ = other.buffs_.Clone();
		round_ = other.round_;
		beBornNodeId_ = other.beBornNodeId_;
		gold_ = other.gold_;
		cards_ = other.cards_.Clone();
		movePoint_ = other.movePoint_;
		frontNodeIds_ = other.frontNodeIds_.Clone();
		backNodeId_ = other.backNodeId_;
		useTime_ = other.useTime_;
		hp_ = other.hp_;
		defense_ = other.defense_;
		attack_ = other.attack_;
		battleId_ = other.battleId_;
		lotterys_ = other.lotterys_.Clone();
		askPlayerIds_ = other.askPlayerIds_.Clone();
		appendMovePoint1_ = other.appendMovePoint1_;
		appendMovePoint2_ = other.appendMovePoint2_;
		bombs_ = other.bombs_.Clone();
		maxHp_ = other.maxHp_;
		isDoubleThrowDice_ = other.isDoubleThrowDice_;
		isStopRound_ = other.isStopRound_;
		realMove_ = other.realMove_;
		isBigShop_ = other.isBigShop_;
		bigShopEffectCardNum_ = other.bigShopEffectCardNum_;
		bigShopCombatCardNum_ = other.bigShopCombatCardNum_;
		userCardNum_ = other.userCardNum_;
		isControlMovePoint_ = other.isControlMovePoint_;
		additionAttrs_ = other.additionAttrs_.Clone();
		lv_ = other.lv_;
		forceDir_ = other.forceDir_;
		landNoHandle_ = other.landNoHandle_;
		eventTriggerNum_ = other.eventTriggerNum_;
		eventIds_ = other.eventIds_.Clone();
		skillCds_ = other.skillCds_.Clone();
		useSkillNum_ = other.useSkillNum_;
		affirm_ = other.affirm_;
		devNextEventId_ = other.devNextEventId_;
		devNextDestinyId_ = other.devNextDestinyId_;
		devNextDivinationId_ = other.devNextDivinationId_;
		bossTargetId_ = other.bossTargetId_;
		lastChatTime_ = other.lastChatTime_;
		standingPainting_ = other.standingPainting_;
		cond_ = ((other.cond_ != null) ? other.cond_.Clone() : null);
		isLaunchFight_ = other.isLaunchFight_;
		monsterType_ = other.monsterType_;
		actionWeight_ = other.actionWeight_;
		selectRelics_ = other.selectRelics_.Clone();
		monsterIndex_ = other.monsterIndex_;
		doubleThrowDiceSkill_ = other.doubleThrowDiceSkill_;
		pveHeroStrengthen_ = ((other.pveHeroStrengthen_ != null) ? other.pveHeroStrengthen_.Clone() : null);
		battleCount_ = other.battleCount_;
		reRollNum_ = other.reRollNum_;
		devMovePoint_ = other.devMovePoint_;
		devAttackPoint_ = other.devAttackPoint_;
		devDefensePoint_ = other.devDefensePoint_;
		teamId_ = other.teamId_;
		specialScore_ = other.specialScore_;
		reviveRoundNum_ = other.reviveRoundNum_;
		cureNum_ = other.cureNum_;
		salaryNum_ = other.salaryNum_;
		markNum_ = other.markNum_;
		isTrial_ = other.isTrial_;
		buyRelicNum_ = other.buyRelicNum_;
		useCardMaxNum_ = other.useCardMaxNum_;
		skinPendant_ = other.skinPendant_;
		cardDistance_ = other.cardDistance_;
		canCounter_ = other.canCounter_;
		counterNum_ = other.counterNum_;
		modityNum_ = other.modityNum_;
		notSelect_ = other.notSelect_;
		moveEffectSkillId_ = other.moveEffectSkillId_;
		timeOutNum_ = other.timeOutNum_;
		cardDiscount_ = other.cardDiscount_;
		disCardPrice_ = other.disCardPrice_;
		cardIdx_ = other.cardIdx_;
		diceWeightIndex_ = other.diceWeightIndex_;
		landBuffHandle_ = other.landBuffHandle_;
		hasSendChat_ = other.hasSendChat_;
		hasSendPhrase_ = other.hasSendPhrase_;
		offlineTime_ = other.offlineTime_;
		offlineType_ = other.offlineType_;
		uniqueNum_ = other.uniqueNum_;
		cardAtk_ = other.cardAtk_;
		energyNum_ = other.energyNum_;
		isSkipRound_ = other.isSkipRound_;
		crimeNum_ = other.crimeNum_;
		doneActions_ = other.doneActions_.Clone();
		actionEnd_ = other.actionEnd_;
		onlyUseCard_ = other.onlyUseCard_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Hero Clone()
	{
		return new Hero(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Hero);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Hero other)
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
		if (HeroId != other.HeroId)
		{
			return false;
		}
		if (NodeId != other.NodeId)
		{
			return false;
		}
		if (!Buffs.Equals(other.Buffs))
		{
			return false;
		}
		if (Round != other.Round)
		{
			return false;
		}
		if (BeBornNodeId != other.BeBornNodeId)
		{
			return false;
		}
		if (Gold != other.Gold)
		{
			return false;
		}
		if (!cards_.Equals(other.cards_))
		{
			return false;
		}
		if (MovePoint != other.MovePoint)
		{
			return false;
		}
		if (!frontNodeIds_.Equals(other.frontNodeIds_))
		{
			return false;
		}
		if (BackNodeId != other.BackNodeId)
		{
			return false;
		}
		if (UseTime != other.UseTime)
		{
			return false;
		}
		if (Hp != other.Hp)
		{
			return false;
		}
		if (Defense != other.Defense)
		{
			return false;
		}
		if (Attack != other.Attack)
		{
			return false;
		}
		if (BattleId != other.BattleId)
		{
			return false;
		}
		if (!Lotterys.Equals(other.Lotterys))
		{
			return false;
		}
		if (!askPlayerIds_.Equals(other.askPlayerIds_))
		{
			return false;
		}
		if (AppendMovePoint1 != other.AppendMovePoint1)
		{
			return false;
		}
		if (AppendMovePoint2 != other.AppendMovePoint2)
		{
			return false;
		}
		if (!bombs_.Equals(other.bombs_))
		{
			return false;
		}
		if (MaxHp != other.MaxHp)
		{
			return false;
		}
		if (IsDoubleThrowDice != other.IsDoubleThrowDice)
		{
			return false;
		}
		if (IsStopRound != other.IsStopRound)
		{
			return false;
		}
		if (RealMove != other.RealMove)
		{
			return false;
		}
		if (IsBigShop != other.IsBigShop)
		{
			return false;
		}
		if (BigShopEffectCardNum != other.BigShopEffectCardNum)
		{
			return false;
		}
		if (BigShopCombatCardNum != other.BigShopCombatCardNum)
		{
			return false;
		}
		if (UserCardNum != other.UserCardNum)
		{
			return false;
		}
		if (IsControlMovePoint != other.IsControlMovePoint)
		{
			return false;
		}
		if (!AdditionAttrs.Equals(other.AdditionAttrs))
		{
			return false;
		}
		if (Lv != other.Lv)
		{
			return false;
		}
		if (ForceDir != other.ForceDir)
		{
			return false;
		}
		if (LandNoHandle != other.LandNoHandle)
		{
			return false;
		}
		if (EventTriggerNum != other.EventTriggerNum)
		{
			return false;
		}
		if (!EventIds.Equals(other.EventIds))
		{
			return false;
		}
		if (!SkillCds.Equals(other.SkillCds))
		{
			return false;
		}
		if (UseSkillNum != other.UseSkillNum)
		{
			return false;
		}
		if (Affirm != other.Affirm)
		{
			return false;
		}
		if (DevNextEventId != other.DevNextEventId)
		{
			return false;
		}
		if (DevNextDestinyId != other.DevNextDestinyId)
		{
			return false;
		}
		if (DevNextDivinationId != other.DevNextDivinationId)
		{
			return false;
		}
		if (BossTargetId != other.BossTargetId)
		{
			return false;
		}
		if (LastChatTime != other.LastChatTime)
		{
			return false;
		}
		if (StandingPainting != other.StandingPainting)
		{
			return false;
		}
		if (!object.Equals(Cond, other.Cond))
		{
			return false;
		}
		if (IsLaunchFight != other.IsLaunchFight)
		{
			return false;
		}
		if (MonsterType != other.MonsterType)
		{
			return false;
		}
		if (ActionWeight != other.ActionWeight)
		{
			return false;
		}
		if (!SelectRelics.Equals(other.SelectRelics))
		{
			return false;
		}
		if (MonsterIndex != other.MonsterIndex)
		{
			return false;
		}
		if (DoubleThrowDiceSkill != other.DoubleThrowDiceSkill)
		{
			return false;
		}
		if (!object.Equals(PveHeroStrengthen, other.PveHeroStrengthen))
		{
			return false;
		}
		if (BattleCount != other.BattleCount)
		{
			return false;
		}
		if (ReRollNum != other.ReRollNum)
		{
			return false;
		}
		if (DevMovePoint != other.DevMovePoint)
		{
			return false;
		}
		if (DevAttackPoint != other.DevAttackPoint)
		{
			return false;
		}
		if (DevDefensePoint != other.DevDefensePoint)
		{
			return false;
		}
		if (TeamId != other.TeamId)
		{
			return false;
		}
		if (SpecialScore != other.SpecialScore)
		{
			return false;
		}
		if (ReviveRoundNum != other.ReviveRoundNum)
		{
			return false;
		}
		if (CureNum != other.CureNum)
		{
			return false;
		}
		if (SalaryNum != other.SalaryNum)
		{
			return false;
		}
		if (MarkNum != other.MarkNum)
		{
			return false;
		}
		if (IsTrial != other.IsTrial)
		{
			return false;
		}
		if (BuyRelicNum != other.BuyRelicNum)
		{
			return false;
		}
		if (UseCardMaxNum != other.UseCardMaxNum)
		{
			return false;
		}
		if (SkinPendant != other.SkinPendant)
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
		if (CounterNum != other.CounterNum)
		{
			return false;
		}
		if (ModityNum != other.ModityNum)
		{
			return false;
		}
		if (NotSelect != other.NotSelect)
		{
			return false;
		}
		if (MoveEffectSkillId != other.MoveEffectSkillId)
		{
			return false;
		}
		if (TimeOutNum != other.TimeOutNum)
		{
			return false;
		}
		if (CardDiscount != other.CardDiscount)
		{
			return false;
		}
		if (DisCardPrice != other.DisCardPrice)
		{
			return false;
		}
		if (CardIdx != other.CardIdx)
		{
			return false;
		}
		if (DiceWeightIndex != other.DiceWeightIndex)
		{
			return false;
		}
		if (LandBuffHandle != other.LandBuffHandle)
		{
			return false;
		}
		if (HasSendChat != other.HasSendChat)
		{
			return false;
		}
		if (HasSendPhrase != other.HasSendPhrase)
		{
			return false;
		}
		if (OfflineTime != other.OfflineTime)
		{
			return false;
		}
		if (OfflineType != other.OfflineType)
		{
			return false;
		}
		if (UniqueNum != other.UniqueNum)
		{
			return false;
		}
		if (CardAtk != other.CardAtk)
		{
			return false;
		}
		if (EnergyNum != other.EnergyNum)
		{
			return false;
		}
		if (IsSkipRound != other.IsSkipRound)
		{
			return false;
		}
		if (CrimeNum != other.CrimeNum)
		{
			return false;
		}
		if (!DoneActions.Equals(other.DoneActions))
		{
			return false;
		}
		if (ActionEnd != other.ActionEnd)
		{
			return false;
		}
		if (OnlyUseCard != other.OnlyUseCard)
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
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
		}
		if (NodeId != 0)
		{
			num ^= NodeId.GetHashCode();
		}
		num ^= Buffs.GetHashCode();
		if (Round != 0)
		{
			num ^= Round.GetHashCode();
		}
		if (BeBornNodeId != 0)
		{
			num ^= BeBornNodeId.GetHashCode();
		}
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
		}
		num ^= cards_.GetHashCode();
		if (MovePoint != 0)
		{
			num ^= MovePoint.GetHashCode();
		}
		num ^= frontNodeIds_.GetHashCode();
		if (BackNodeId != 0)
		{
			num ^= BackNodeId.GetHashCode();
		}
		if (UseTime != 0)
		{
			num ^= UseTime.GetHashCode();
		}
		if (Hp != 0)
		{
			num ^= Hp.GetHashCode();
		}
		if (Defense != 0)
		{
			num ^= Defense.GetHashCode();
		}
		if (Attack != 0)
		{
			num ^= Attack.GetHashCode();
		}
		if (BattleId != 0L)
		{
			num ^= BattleId.GetHashCode();
		}
		num ^= Lotterys.GetHashCode();
		num ^= askPlayerIds_.GetHashCode();
		if (AppendMovePoint1 != 0)
		{
			num ^= AppendMovePoint1.GetHashCode();
		}
		if (AppendMovePoint2 != 0)
		{
			num ^= AppendMovePoint2.GetHashCode();
		}
		num ^= bombs_.GetHashCode();
		if (MaxHp != 0)
		{
			num ^= MaxHp.GetHashCode();
		}
		if (IsDoubleThrowDice)
		{
			num ^= IsDoubleThrowDice.GetHashCode();
		}
		if (IsStopRound)
		{
			num ^= IsStopRound.GetHashCode();
		}
		if (RealMove != 0)
		{
			num ^= RealMove.GetHashCode();
		}
		if (IsBigShop)
		{
			num ^= IsBigShop.GetHashCode();
		}
		if (BigShopEffectCardNum != 0)
		{
			num ^= BigShopEffectCardNum.GetHashCode();
		}
		if (BigShopCombatCardNum != 0)
		{
			num ^= BigShopCombatCardNum.GetHashCode();
		}
		if (UserCardNum != 0)
		{
			num ^= UserCardNum.GetHashCode();
		}
		if (IsControlMovePoint)
		{
			num ^= IsControlMovePoint.GetHashCode();
		}
		num ^= AdditionAttrs.GetHashCode();
		if (Lv != 0)
		{
			num ^= Lv.GetHashCode();
		}
		if (ForceDir)
		{
			num ^= ForceDir.GetHashCode();
		}
		if (LandNoHandle)
		{
			num ^= LandNoHandle.GetHashCode();
		}
		if (EventTriggerNum != 0)
		{
			num ^= EventTriggerNum.GetHashCode();
		}
		num ^= EventIds.GetHashCode();
		num ^= SkillCds.GetHashCode();
		if (UseSkillNum != 0)
		{
			num ^= UseSkillNum.GetHashCode();
		}
		if (Affirm)
		{
			num ^= Affirm.GetHashCode();
		}
		if (DevNextEventId != 0)
		{
			num ^= DevNextEventId.GetHashCode();
		}
		if (DevNextDestinyId != 0)
		{
			num ^= DevNextDestinyId.GetHashCode();
		}
		if (DevNextDivinationId != 0)
		{
			num ^= DevNextDivinationId.GetHashCode();
		}
		if (BossTargetId != 0L)
		{
			num ^= BossTargetId.GetHashCode();
		}
		if (LastChatTime != 0L)
		{
			num ^= LastChatTime.GetHashCode();
		}
		if (StandingPainting != 0)
		{
			num ^= StandingPainting.GetHashCode();
		}
		if (cond_ != null)
		{
			num ^= Cond.GetHashCode();
		}
		if (IsLaunchFight)
		{
			num ^= IsLaunchFight.GetHashCode();
		}
		if (MonsterType != Types.MonsterType.None)
		{
			num ^= MonsterType.GetHashCode();
		}
		if (ActionWeight != 0)
		{
			num ^= ActionWeight.GetHashCode();
		}
		num ^= SelectRelics.GetHashCode();
		if (MonsterIndex != 0)
		{
			num ^= MonsterIndex.GetHashCode();
		}
		if (DoubleThrowDiceSkill != 0)
		{
			num ^= DoubleThrowDiceSkill.GetHashCode();
		}
		if (pveHeroStrengthen_ != null)
		{
			num ^= PveHeroStrengthen.GetHashCode();
		}
		if (BattleCount != 0)
		{
			num ^= BattleCount.GetHashCode();
		}
		if (ReRollNum != 0)
		{
			num ^= ReRollNum.GetHashCode();
		}
		if (DevMovePoint != 0)
		{
			num ^= DevMovePoint.GetHashCode();
		}
		if (DevAttackPoint != 0)
		{
			num ^= DevAttackPoint.GetHashCode();
		}
		if (DevDefensePoint != 0)
		{
			num ^= DevDefensePoint.GetHashCode();
		}
		if (TeamId != 0)
		{
			num ^= TeamId.GetHashCode();
		}
		if (SpecialScore != 0)
		{
			num ^= SpecialScore.GetHashCode();
		}
		if (ReviveRoundNum != 0)
		{
			num ^= ReviveRoundNum.GetHashCode();
		}
		if (CureNum != 0)
		{
			num ^= CureNum.GetHashCode();
		}
		if (SalaryNum != 0)
		{
			num ^= SalaryNum.GetHashCode();
		}
		if (MarkNum != 0)
		{
			num ^= MarkNum.GetHashCode();
		}
		if (IsTrial)
		{
			num ^= IsTrial.GetHashCode();
		}
		if (BuyRelicNum != 0)
		{
			num ^= BuyRelicNum.GetHashCode();
		}
		if (UseCardMaxNum != 0)
		{
			num ^= UseCardMaxNum.GetHashCode();
		}
		if (SkinPendant != 0)
		{
			num ^= SkinPendant.GetHashCode();
		}
		if (CardDistance != 0)
		{
			num ^= CardDistance.GetHashCode();
		}
		if (CanCounter)
		{
			num ^= CanCounter.GetHashCode();
		}
		if (CounterNum != 0)
		{
			num ^= CounterNum.GetHashCode();
		}
		if (ModityNum != 0)
		{
			num ^= ModityNum.GetHashCode();
		}
		if (NotSelect)
		{
			num ^= NotSelect.GetHashCode();
		}
		if (MoveEffectSkillId != 0)
		{
			num ^= MoveEffectSkillId.GetHashCode();
		}
		if (TimeOutNum != 0)
		{
			num ^= TimeOutNum.GetHashCode();
		}
		if (CardDiscount != 0)
		{
			num ^= CardDiscount.GetHashCode();
		}
		if (DisCardPrice != 0)
		{
			num ^= DisCardPrice.GetHashCode();
		}
		if (CardIdx != 0)
		{
			num ^= CardIdx.GetHashCode();
		}
		if (DiceWeightIndex != 0)
		{
			num ^= DiceWeightIndex.GetHashCode();
		}
		if (LandBuffHandle)
		{
			num ^= LandBuffHandle.GetHashCode();
		}
		if (HasSendChat)
		{
			num ^= HasSendChat.GetHashCode();
		}
		if (HasSendPhrase)
		{
			num ^= HasSendPhrase.GetHashCode();
		}
		if (OfflineTime != 0L)
		{
			num ^= OfflineTime.GetHashCode();
		}
		if (OfflineType != Types.OfflineType.None)
		{
			num ^= OfflineType.GetHashCode();
		}
		if (UniqueNum != 0)
		{
			num ^= UniqueNum.GetHashCode();
		}
		if (CardAtk != 0)
		{
			num ^= CardAtk.GetHashCode();
		}
		if (EnergyNum != 0)
		{
			num ^= EnergyNum.GetHashCode();
		}
		if (IsSkipRound)
		{
			num ^= IsSkipRound.GetHashCode();
		}
		if (CrimeNum != 0)
		{
			num ^= CrimeNum.GetHashCode();
		}
		num ^= DoneActions.GetHashCode();
		if (ActionEnd)
		{
			num ^= ActionEnd.GetHashCode();
		}
		if (OnlyUseCard)
		{
			num ^= OnlyUseCard.GetHashCode();
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
		if (HeroId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(HeroId);
		}
		if (NodeId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(NodeId);
		}
		buffs_.WriteTo(ref output, _map_buffs_codec);
		if (Round != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Round);
		}
		if (BeBornNodeId != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(BeBornNodeId);
		}
		if (Gold != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Gold);
		}
		cards_.WriteTo(ref output, _repeated_cards_codec);
		if (MovePoint != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(MovePoint);
		}
		frontNodeIds_.WriteTo(ref output, _repeated_frontNodeIds_codec);
		if (BackNodeId != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(BackNodeId);
		}
		if (UseTime != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(UseTime);
		}
		if (Hp != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(Hp);
		}
		if (Defense != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(Defense);
		}
		if (Attack != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(Attack);
		}
		if (BattleId != 0L)
		{
			output.WriteRawTag(137, 1);
			output.WriteSFixed64(BattleId);
		}
		lotterys_.WriteTo(ref output, _map_lotterys_codec);
		askPlayerIds_.WriteTo(ref output, _repeated_askPlayerIds_codec);
		if (AppendMovePoint1 != 0)
		{
			output.WriteRawTag(165, 1);
			output.WriteSFixed32(AppendMovePoint1);
		}
		if (AppendMovePoint2 != 0)
		{
			output.WriteRawTag(173, 1);
			output.WriteSFixed32(AppendMovePoint2);
		}
		bombs_.WriteTo(ref output, _repeated_bombs_codec);
		if (MaxHp != 0)
		{
			output.WriteRawTag(189, 1);
			output.WriteSFixed32(MaxHp);
		}
		if (IsDoubleThrowDice)
		{
			output.WriteRawTag(192, 1);
			output.WriteBool(IsDoubleThrowDice);
		}
		if (IsStopRound)
		{
			output.WriteRawTag(200, 1);
			output.WriteBool(IsStopRound);
		}
		if (RealMove != 0)
		{
			output.WriteRawTag(213, 1);
			output.WriteSFixed32(RealMove);
		}
		if (IsBigShop)
		{
			output.WriteRawTag(216, 1);
			output.WriteBool(IsBigShop);
		}
		if (BigShopEffectCardNum != 0)
		{
			output.WriteRawTag(229, 1);
			output.WriteSFixed32(BigShopEffectCardNum);
		}
		if (BigShopCombatCardNum != 0)
		{
			output.WriteRawTag(237, 1);
			output.WriteSFixed32(BigShopCombatCardNum);
		}
		if (UserCardNum != 0)
		{
			output.WriteRawTag(245, 1);
			output.WriteSFixed32(UserCardNum);
		}
		if (IsControlMovePoint)
		{
			output.WriteRawTag(248, 1);
			output.WriteBool(IsControlMovePoint);
		}
		additionAttrs_.WriteTo(ref output, _map_additionAttrs_codec);
		if (Lv != 0)
		{
			output.WriteRawTag(141, 2);
			output.WriteSFixed32(Lv);
		}
		if (ForceDir)
		{
			output.WriteRawTag(144, 2);
			output.WriteBool(ForceDir);
		}
		if (LandNoHandle)
		{
			output.WriteRawTag(152, 2);
			output.WriteBool(LandNoHandle);
		}
		if (EventTriggerNum != 0)
		{
			output.WriteRawTag(165, 2);
			output.WriteSFixed32(EventTriggerNum);
		}
		eventIds_.WriteTo(ref output, _map_eventIds_codec);
		skillCds_.WriteTo(ref output, _map_skillCds_codec);
		if (UseSkillNum != 0)
		{
			output.WriteRawTag(189, 2);
			output.WriteSFixed32(UseSkillNum);
		}
		if (Affirm)
		{
			output.WriteRawTag(192, 2);
			output.WriteBool(Affirm);
		}
		if (DevNextEventId != 0)
		{
			output.WriteRawTag(205, 2);
			output.WriteSFixed32(DevNextEventId);
		}
		if (DevNextDestinyId != 0)
		{
			output.WriteRawTag(213, 2);
			output.WriteSFixed32(DevNextDestinyId);
		}
		if (DevNextDivinationId != 0)
		{
			output.WriteRawTag(221, 2);
			output.WriteSFixed32(DevNextDivinationId);
		}
		if (BossTargetId != 0L)
		{
			output.WriteRawTag(225, 2);
			output.WriteSFixed64(BossTargetId);
		}
		if (LastChatTime != 0L)
		{
			output.WriteRawTag(233, 2);
			output.WriteSFixed64(LastChatTime);
		}
		if (StandingPainting != 0)
		{
			output.WriteRawTag(245, 2);
			output.WriteSFixed32(StandingPainting);
		}
		if (cond_ != null)
		{
			output.WriteRawTag(250, 2);
			output.WriteMessage(Cond);
		}
		if (IsLaunchFight)
		{
			output.WriteRawTag(128, 3);
			output.WriteBool(IsLaunchFight);
		}
		if (MonsterType != Types.MonsterType.None)
		{
			output.WriteRawTag(136, 3);
			output.WriteEnum((int)MonsterType);
		}
		if (ActionWeight != 0)
		{
			output.WriteRawTag(149, 3);
			output.WriteSFixed32(ActionWeight);
		}
		selectRelics_.WriteTo(ref output, _map_selectRelics_codec);
		if (MonsterIndex != 0)
		{
			output.WriteRawTag(173, 3);
			output.WriteSFixed32(MonsterIndex);
		}
		if (DoubleThrowDiceSkill != 0)
		{
			output.WriteRawTag(181, 3);
			output.WriteSFixed32(DoubleThrowDiceSkill);
		}
		if (pveHeroStrengthen_ != null)
		{
			output.WriteRawTag(186, 3);
			output.WriteMessage(PveHeroStrengthen);
		}
		if (BattleCount != 0)
		{
			output.WriteRawTag(197, 3);
			output.WriteSFixed32(BattleCount);
		}
		if (ReRollNum != 0)
		{
			output.WriteRawTag(205, 3);
			output.WriteSFixed32(ReRollNum);
		}
		if (DevMovePoint != 0)
		{
			output.WriteRawTag(213, 3);
			output.WriteSFixed32(DevMovePoint);
		}
		if (DevAttackPoint != 0)
		{
			output.WriteRawTag(221, 3);
			output.WriteSFixed32(DevAttackPoint);
		}
		if (DevDefensePoint != 0)
		{
			output.WriteRawTag(229, 3);
			output.WriteSFixed32(DevDefensePoint);
		}
		if (TeamId != 0)
		{
			output.WriteRawTag(237, 3);
			output.WriteSFixed32(TeamId);
		}
		if (SpecialScore != 0)
		{
			output.WriteRawTag(245, 3);
			output.WriteSFixed32(SpecialScore);
		}
		if (ReviveRoundNum != 0)
		{
			output.WriteRawTag(253, 3);
			output.WriteSFixed32(ReviveRoundNum);
		}
		if (CureNum != 0)
		{
			output.WriteRawTag(133, 4);
			output.WriteSFixed32(CureNum);
		}
		if (SalaryNum != 0)
		{
			output.WriteRawTag(141, 4);
			output.WriteSFixed32(SalaryNum);
		}
		if (MarkNum != 0)
		{
			output.WriteRawTag(149, 4);
			output.WriteSFixed32(MarkNum);
		}
		if (IsTrial)
		{
			output.WriteRawTag(152, 4);
			output.WriteBool(IsTrial);
		}
		if (BuyRelicNum != 0)
		{
			output.WriteRawTag(165, 4);
			output.WriteSFixed32(BuyRelicNum);
		}
		if (UseCardMaxNum != 0)
		{
			output.WriteRawTag(173, 4);
			output.WriteSFixed32(UseCardMaxNum);
		}
		if (SkinPendant != 0)
		{
			output.WriteRawTag(181, 4);
			output.WriteSFixed32(SkinPendant);
		}
		if (CardDistance != 0)
		{
			output.WriteRawTag(189, 4);
			output.WriteSFixed32(CardDistance);
		}
		if (CanCounter)
		{
			output.WriteRawTag(192, 4);
			output.WriteBool(CanCounter);
		}
		if (CounterNum != 0)
		{
			output.WriteRawTag(205, 4);
			output.WriteSFixed32(CounterNum);
		}
		if (ModityNum != 0)
		{
			output.WriteRawTag(213, 4);
			output.WriteSFixed32(ModityNum);
		}
		if (NotSelect)
		{
			output.WriteRawTag(216, 4);
			output.WriteBool(NotSelect);
		}
		if (MoveEffectSkillId != 0)
		{
			output.WriteRawTag(229, 4);
			output.WriteSFixed32(MoveEffectSkillId);
		}
		if (TimeOutNum != 0)
		{
			output.WriteRawTag(237, 4);
			output.WriteSFixed32(TimeOutNum);
		}
		if (CardDiscount != 0)
		{
			output.WriteRawTag(245, 4);
			output.WriteSFixed32(CardDiscount);
		}
		if (DisCardPrice != 0)
		{
			output.WriteRawTag(253, 4);
			output.WriteSFixed32(DisCardPrice);
		}
		if (CardIdx != 0)
		{
			output.WriteRawTag(133, 5);
			output.WriteSFixed32(CardIdx);
		}
		if (DiceWeightIndex != 0)
		{
			output.WriteRawTag(141, 5);
			output.WriteSFixed32(DiceWeightIndex);
		}
		if (LandBuffHandle)
		{
			output.WriteRawTag(144, 5);
			output.WriteBool(LandBuffHandle);
		}
		if (HasSendChat)
		{
			output.WriteRawTag(160, 5);
			output.WriteBool(HasSendChat);
		}
		if (HasSendPhrase)
		{
			output.WriteRawTag(168, 5);
			output.WriteBool(HasSendPhrase);
		}
		if (OfflineTime != 0L)
		{
			output.WriteRawTag(177, 5);
			output.WriteSFixed64(OfflineTime);
		}
		if (OfflineType != Types.OfflineType.None)
		{
			output.WriteRawTag(184, 5);
			output.WriteEnum((int)OfflineType);
		}
		if (UniqueNum != 0)
		{
			output.WriteRawTag(197, 5);
			output.WriteSFixed32(UniqueNum);
		}
		if (CardAtk != 0)
		{
			output.WriteRawTag(205, 5);
			output.WriteSFixed32(CardAtk);
		}
		if (EnergyNum != 0)
		{
			output.WriteRawTag(213, 5);
			output.WriteSFixed32(EnergyNum);
		}
		if (IsSkipRound)
		{
			output.WriteRawTag(216, 5);
			output.WriteBool(IsSkipRound);
		}
		if (CrimeNum != 0)
		{
			output.WriteRawTag(229, 5);
			output.WriteSFixed32(CrimeNum);
		}
		doneActions_.WriteTo(ref output, _map_doneActions_codec);
		if (ActionEnd)
		{
			output.WriteRawTag(200, 62);
			output.WriteBool(ActionEnd);
		}
		if (OnlyUseCard)
		{
			output.WriteRawTag(208, 62);
			output.WriteBool(OnlyUseCard);
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
		if (HeroId != 0)
		{
			num += 5;
		}
		if (NodeId != 0)
		{
			num += 5;
		}
		num += buffs_.CalculateSize(_map_buffs_codec);
		if (Round != 0)
		{
			num += 5;
		}
		if (BeBornNodeId != 0)
		{
			num += 5;
		}
		if (Gold != 0)
		{
			num += 5;
		}
		num += cards_.CalculateSize(_repeated_cards_codec);
		if (MovePoint != 0)
		{
			num += 5;
		}
		num += frontNodeIds_.CalculateSize(_repeated_frontNodeIds_codec);
		if (BackNodeId != 0)
		{
			num += 5;
		}
		if (UseTime != 0)
		{
			num += 5;
		}
		if (Hp != 0)
		{
			num += 5;
		}
		if (Defense != 0)
		{
			num += 5;
		}
		if (Attack != 0)
		{
			num += 6;
		}
		if (BattleId != 0L)
		{
			num += 10;
		}
		num += lotterys_.CalculateSize(_map_lotterys_codec);
		num += askPlayerIds_.CalculateSize(_repeated_askPlayerIds_codec);
		if (AppendMovePoint1 != 0)
		{
			num += 6;
		}
		if (AppendMovePoint2 != 0)
		{
			num += 6;
		}
		num += bombs_.CalculateSize(_repeated_bombs_codec);
		if (MaxHp != 0)
		{
			num += 6;
		}
		if (IsDoubleThrowDice)
		{
			num += 3;
		}
		if (IsStopRound)
		{
			num += 3;
		}
		if (RealMove != 0)
		{
			num += 6;
		}
		if (IsBigShop)
		{
			num += 3;
		}
		if (BigShopEffectCardNum != 0)
		{
			num += 6;
		}
		if (BigShopCombatCardNum != 0)
		{
			num += 6;
		}
		if (UserCardNum != 0)
		{
			num += 6;
		}
		if (IsControlMovePoint)
		{
			num += 3;
		}
		num += additionAttrs_.CalculateSize(_map_additionAttrs_codec);
		if (Lv != 0)
		{
			num += 6;
		}
		if (ForceDir)
		{
			num += 3;
		}
		if (LandNoHandle)
		{
			num += 3;
		}
		if (EventTriggerNum != 0)
		{
			num += 6;
		}
		num += eventIds_.CalculateSize(_map_eventIds_codec);
		num += skillCds_.CalculateSize(_map_skillCds_codec);
		if (UseSkillNum != 0)
		{
			num += 6;
		}
		if (Affirm)
		{
			num += 3;
		}
		if (DevNextEventId != 0)
		{
			num += 6;
		}
		if (DevNextDestinyId != 0)
		{
			num += 6;
		}
		if (DevNextDivinationId != 0)
		{
			num += 6;
		}
		if (BossTargetId != 0L)
		{
			num += 10;
		}
		if (LastChatTime != 0L)
		{
			num += 10;
		}
		if (StandingPainting != 0)
		{
			num += 6;
		}
		if (cond_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Cond);
		}
		if (IsLaunchFight)
		{
			num += 3;
		}
		if (MonsterType != Types.MonsterType.None)
		{
			num += 2 + CodedOutputStream.ComputeEnumSize((int)MonsterType);
		}
		if (ActionWeight != 0)
		{
			num += 6;
		}
		num += selectRelics_.CalculateSize(_map_selectRelics_codec);
		if (MonsterIndex != 0)
		{
			num += 6;
		}
		if (DoubleThrowDiceSkill != 0)
		{
			num += 6;
		}
		if (pveHeroStrengthen_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(PveHeroStrengthen);
		}
		if (BattleCount != 0)
		{
			num += 6;
		}
		if (ReRollNum != 0)
		{
			num += 6;
		}
		if (DevMovePoint != 0)
		{
			num += 6;
		}
		if (DevAttackPoint != 0)
		{
			num += 6;
		}
		if (DevDefensePoint != 0)
		{
			num += 6;
		}
		if (TeamId != 0)
		{
			num += 6;
		}
		if (SpecialScore != 0)
		{
			num += 6;
		}
		if (ReviveRoundNum != 0)
		{
			num += 6;
		}
		if (CureNum != 0)
		{
			num += 6;
		}
		if (SalaryNum != 0)
		{
			num += 6;
		}
		if (MarkNum != 0)
		{
			num += 6;
		}
		if (IsTrial)
		{
			num += 3;
		}
		if (BuyRelicNum != 0)
		{
			num += 6;
		}
		if (UseCardMaxNum != 0)
		{
			num += 6;
		}
		if (SkinPendant != 0)
		{
			num += 6;
		}
		if (CardDistance != 0)
		{
			num += 6;
		}
		if (CanCounter)
		{
			num += 3;
		}
		if (CounterNum != 0)
		{
			num += 6;
		}
		if (ModityNum != 0)
		{
			num += 6;
		}
		if (NotSelect)
		{
			num += 3;
		}
		if (MoveEffectSkillId != 0)
		{
			num += 6;
		}
		if (TimeOutNum != 0)
		{
			num += 6;
		}
		if (CardDiscount != 0)
		{
			num += 6;
		}
		if (DisCardPrice != 0)
		{
			num += 6;
		}
		if (CardIdx != 0)
		{
			num += 6;
		}
		if (DiceWeightIndex != 0)
		{
			num += 6;
		}
		if (LandBuffHandle)
		{
			num += 3;
		}
		if (HasSendChat)
		{
			num += 3;
		}
		if (HasSendPhrase)
		{
			num += 3;
		}
		if (OfflineTime != 0L)
		{
			num += 10;
		}
		if (OfflineType != Types.OfflineType.None)
		{
			num += 2 + CodedOutputStream.ComputeEnumSize((int)OfflineType);
		}
		if (UniqueNum != 0)
		{
			num += 6;
		}
		if (CardAtk != 0)
		{
			num += 6;
		}
		if (EnergyNum != 0)
		{
			num += 6;
		}
		if (IsSkipRound)
		{
			num += 3;
		}
		if (CrimeNum != 0)
		{
			num += 6;
		}
		num += doneActions_.CalculateSize(_map_doneActions_codec);
		if (ActionEnd)
		{
			num += 3;
		}
		if (OnlyUseCard)
		{
			num += 3;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(Hero other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PlayerId != 0L)
		{
			PlayerId = other.PlayerId;
		}
		if (other.HeroId != 0)
		{
			HeroId = other.HeroId;
		}
		if (other.NodeId != 0)
		{
			NodeId = other.NodeId;
		}
		buffs_.MergeFrom(other.buffs_);
		if (other.Round != 0)
		{
			Round = other.Round;
		}
		if (other.BeBornNodeId != 0)
		{
			BeBornNodeId = other.BeBornNodeId;
		}
		if (other.Gold != 0)
		{
			Gold = other.Gold;
		}
		cards_.Add(other.cards_);
		if (other.MovePoint != 0)
		{
			MovePoint = other.MovePoint;
		}
		frontNodeIds_.Add(other.frontNodeIds_);
		if (other.BackNodeId != 0)
		{
			BackNodeId = other.BackNodeId;
		}
		if (other.UseTime != 0)
		{
			UseTime = other.UseTime;
		}
		if (other.Hp != 0)
		{
			Hp = other.Hp;
		}
		if (other.Defense != 0)
		{
			Defense = other.Defense;
		}
		if (other.Attack != 0)
		{
			Attack = other.Attack;
		}
		if (other.BattleId != 0L)
		{
			BattleId = other.BattleId;
		}
		lotterys_.MergeFrom(other.lotterys_);
		askPlayerIds_.Add(other.askPlayerIds_);
		if (other.AppendMovePoint1 != 0)
		{
			AppendMovePoint1 = other.AppendMovePoint1;
		}
		if (other.AppendMovePoint2 != 0)
		{
			AppendMovePoint2 = other.AppendMovePoint2;
		}
		bombs_.Add(other.bombs_);
		if (other.MaxHp != 0)
		{
			MaxHp = other.MaxHp;
		}
		if (other.IsDoubleThrowDice)
		{
			IsDoubleThrowDice = other.IsDoubleThrowDice;
		}
		if (other.IsStopRound)
		{
			IsStopRound = other.IsStopRound;
		}
		if (other.RealMove != 0)
		{
			RealMove = other.RealMove;
		}
		if (other.IsBigShop)
		{
			IsBigShop = other.IsBigShop;
		}
		if (other.BigShopEffectCardNum != 0)
		{
			BigShopEffectCardNum = other.BigShopEffectCardNum;
		}
		if (other.BigShopCombatCardNum != 0)
		{
			BigShopCombatCardNum = other.BigShopCombatCardNum;
		}
		if (other.UserCardNum != 0)
		{
			UserCardNum = other.UserCardNum;
		}
		if (other.IsControlMovePoint)
		{
			IsControlMovePoint = other.IsControlMovePoint;
		}
		additionAttrs_.MergeFrom(other.additionAttrs_);
		if (other.Lv != 0)
		{
			Lv = other.Lv;
		}
		if (other.ForceDir)
		{
			ForceDir = other.ForceDir;
		}
		if (other.LandNoHandle)
		{
			LandNoHandle = other.LandNoHandle;
		}
		if (other.EventTriggerNum != 0)
		{
			EventTriggerNum = other.EventTriggerNum;
		}
		eventIds_.MergeFrom(other.eventIds_);
		skillCds_.MergeFrom(other.skillCds_);
		if (other.UseSkillNum != 0)
		{
			UseSkillNum = other.UseSkillNum;
		}
		if (other.Affirm)
		{
			Affirm = other.Affirm;
		}
		if (other.DevNextEventId != 0)
		{
			DevNextEventId = other.DevNextEventId;
		}
		if (other.DevNextDestinyId != 0)
		{
			DevNextDestinyId = other.DevNextDestinyId;
		}
		if (other.DevNextDivinationId != 0)
		{
			DevNextDivinationId = other.DevNextDivinationId;
		}
		if (other.BossTargetId != 0L)
		{
			BossTargetId = other.BossTargetId;
		}
		if (other.LastChatTime != 0L)
		{
			LastChatTime = other.LastChatTime;
		}
		if (other.StandingPainting != 0)
		{
			StandingPainting = other.StandingPainting;
		}
		if (other.cond_ != null)
		{
			if (cond_ == null)
			{
				Cond = new ConditionalInfo();
			}
			Cond.MergeFrom(other.Cond);
		}
		if (other.IsLaunchFight)
		{
			IsLaunchFight = other.IsLaunchFight;
		}
		if (other.MonsterType != Types.MonsterType.None)
		{
			MonsterType = other.MonsterType;
		}
		if (other.ActionWeight != 0)
		{
			ActionWeight = other.ActionWeight;
		}
		selectRelics_.MergeFrom(other.selectRelics_);
		if (other.MonsterIndex != 0)
		{
			MonsterIndex = other.MonsterIndex;
		}
		if (other.DoubleThrowDiceSkill != 0)
		{
			DoubleThrowDiceSkill = other.DoubleThrowDiceSkill;
		}
		if (other.pveHeroStrengthen_ != null)
		{
			if (pveHeroStrengthen_ == null)
			{
				PveHeroStrengthen = new PveHeroStrengthen();
			}
			PveHeroStrengthen.MergeFrom(other.PveHeroStrengthen);
		}
		if (other.BattleCount != 0)
		{
			BattleCount = other.BattleCount;
		}
		if (other.ReRollNum != 0)
		{
			ReRollNum = other.ReRollNum;
		}
		if (other.DevMovePoint != 0)
		{
			DevMovePoint = other.DevMovePoint;
		}
		if (other.DevAttackPoint != 0)
		{
			DevAttackPoint = other.DevAttackPoint;
		}
		if (other.DevDefensePoint != 0)
		{
			DevDefensePoint = other.DevDefensePoint;
		}
		if (other.TeamId != 0)
		{
			TeamId = other.TeamId;
		}
		if (other.SpecialScore != 0)
		{
			SpecialScore = other.SpecialScore;
		}
		if (other.ReviveRoundNum != 0)
		{
			ReviveRoundNum = other.ReviveRoundNum;
		}
		if (other.CureNum != 0)
		{
			CureNum = other.CureNum;
		}
		if (other.SalaryNum != 0)
		{
			SalaryNum = other.SalaryNum;
		}
		if (other.MarkNum != 0)
		{
			MarkNum = other.MarkNum;
		}
		if (other.IsTrial)
		{
			IsTrial = other.IsTrial;
		}
		if (other.BuyRelicNum != 0)
		{
			BuyRelicNum = other.BuyRelicNum;
		}
		if (other.UseCardMaxNum != 0)
		{
			UseCardMaxNum = other.UseCardMaxNum;
		}
		if (other.SkinPendant != 0)
		{
			SkinPendant = other.SkinPendant;
		}
		if (other.CardDistance != 0)
		{
			CardDistance = other.CardDistance;
		}
		if (other.CanCounter)
		{
			CanCounter = other.CanCounter;
		}
		if (other.CounterNum != 0)
		{
			CounterNum = other.CounterNum;
		}
		if (other.ModityNum != 0)
		{
			ModityNum = other.ModityNum;
		}
		if (other.NotSelect)
		{
			NotSelect = other.NotSelect;
		}
		if (other.MoveEffectSkillId != 0)
		{
			MoveEffectSkillId = other.MoveEffectSkillId;
		}
		if (other.TimeOutNum != 0)
		{
			TimeOutNum = other.TimeOutNum;
		}
		if (other.CardDiscount != 0)
		{
			CardDiscount = other.CardDiscount;
		}
		if (other.DisCardPrice != 0)
		{
			DisCardPrice = other.DisCardPrice;
		}
		if (other.CardIdx != 0)
		{
			CardIdx = other.CardIdx;
		}
		if (other.DiceWeightIndex != 0)
		{
			DiceWeightIndex = other.DiceWeightIndex;
		}
		if (other.LandBuffHandle)
		{
			LandBuffHandle = other.LandBuffHandle;
		}
		if (other.HasSendChat)
		{
			HasSendChat = other.HasSendChat;
		}
		if (other.HasSendPhrase)
		{
			HasSendPhrase = other.HasSendPhrase;
		}
		if (other.OfflineTime != 0L)
		{
			OfflineTime = other.OfflineTime;
		}
		if (other.OfflineType != Types.OfflineType.None)
		{
			OfflineType = other.OfflineType;
		}
		if (other.UniqueNum != 0)
		{
			UniqueNum = other.UniqueNum;
		}
		if (other.CardAtk != 0)
		{
			CardAtk = other.CardAtk;
		}
		if (other.EnergyNum != 0)
		{
			EnergyNum = other.EnergyNum;
		}
		if (other.IsSkipRound)
		{
			IsSkipRound = other.IsSkipRound;
		}
		if (other.CrimeNum != 0)
		{
			CrimeNum = other.CrimeNum;
		}
		doneActions_.MergeFrom(other.doneActions_);
		if (other.ActionEnd)
		{
			ActionEnd = other.ActionEnd;
		}
		if (other.OnlyUseCard)
		{
			OnlyUseCard = other.OnlyUseCard;
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
			case 21u:
				HeroId = input.ReadSFixed32();
				break;
			case 29u:
				NodeId = input.ReadSFixed32();
				break;
			case 34u:
				buffs_.AddEntriesFrom(ref input, _map_buffs_codec);
				break;
			case 45u:
				Round = input.ReadSFixed32();
				break;
			case 53u:
				BeBornNodeId = input.ReadSFixed32();
				break;
			case 61u:
				Gold = input.ReadSFixed32();
				break;
			case 66u:
				cards_.AddEntriesFrom(ref input, _repeated_cards_codec);
				break;
			case 85u:
				MovePoint = input.ReadSFixed32();
				break;
			case 90u:
			case 93u:
				frontNodeIds_.AddEntriesFrom(ref input, _repeated_frontNodeIds_codec);
				break;
			case 101u:
				BackNodeId = input.ReadSFixed32();
				break;
			case 109u:
				UseTime = input.ReadSFixed32();
				break;
			case 117u:
				Hp = input.ReadSFixed32();
				break;
			case 125u:
				Defense = input.ReadSFixed32();
				break;
			case 133u:
				Attack = input.ReadSFixed32();
				break;
			case 137u:
				BattleId = input.ReadSFixed64();
				break;
			case 146u:
				lotterys_.AddEntriesFrom(ref input, _map_lotterys_codec);
				break;
			case 153u:
			case 154u:
				askPlayerIds_.AddEntriesFrom(ref input, _repeated_askPlayerIds_codec);
				break;
			case 165u:
				AppendMovePoint1 = input.ReadSFixed32();
				break;
			case 173u:
				AppendMovePoint2 = input.ReadSFixed32();
				break;
			case 178u:
				bombs_.AddEntriesFrom(ref input, _repeated_bombs_codec);
				break;
			case 189u:
				MaxHp = input.ReadSFixed32();
				break;
			case 192u:
				IsDoubleThrowDice = input.ReadBool();
				break;
			case 200u:
				IsStopRound = input.ReadBool();
				break;
			case 213u:
				RealMove = input.ReadSFixed32();
				break;
			case 216u:
				IsBigShop = input.ReadBool();
				break;
			case 229u:
				BigShopEffectCardNum = input.ReadSFixed32();
				break;
			case 237u:
				BigShopCombatCardNum = input.ReadSFixed32();
				break;
			case 245u:
				UserCardNum = input.ReadSFixed32();
				break;
			case 248u:
				IsControlMovePoint = input.ReadBool();
				break;
			case 258u:
				additionAttrs_.AddEntriesFrom(ref input, _map_additionAttrs_codec);
				break;
			case 269u:
				Lv = input.ReadSFixed32();
				break;
			case 272u:
				ForceDir = input.ReadBool();
				break;
			case 280u:
				LandNoHandle = input.ReadBool();
				break;
			case 293u:
				EventTriggerNum = input.ReadSFixed32();
				break;
			case 298u:
				eventIds_.AddEntriesFrom(ref input, _map_eventIds_codec);
				break;
			case 306u:
				skillCds_.AddEntriesFrom(ref input, _map_skillCds_codec);
				break;
			case 317u:
				UseSkillNum = input.ReadSFixed32();
				break;
			case 320u:
				Affirm = input.ReadBool();
				break;
			case 333u:
				DevNextEventId = input.ReadSFixed32();
				break;
			case 341u:
				DevNextDestinyId = input.ReadSFixed32();
				break;
			case 349u:
				DevNextDivinationId = input.ReadSFixed32();
				break;
			case 353u:
				BossTargetId = input.ReadSFixed64();
				break;
			case 361u:
				LastChatTime = input.ReadSFixed64();
				break;
			case 373u:
				StandingPainting = input.ReadSFixed32();
				break;
			case 378u:
				if (cond_ == null)
				{
					Cond = new ConditionalInfo();
				}
				input.ReadMessage(Cond);
				break;
			case 384u:
				IsLaunchFight = input.ReadBool();
				break;
			case 392u:
				MonsterType = (Types.MonsterType)input.ReadEnum();
				break;
			case 405u:
				ActionWeight = input.ReadSFixed32();
				break;
			case 418u:
				selectRelics_.AddEntriesFrom(ref input, _map_selectRelics_codec);
				break;
			case 429u:
				MonsterIndex = input.ReadSFixed32();
				break;
			case 437u:
				DoubleThrowDiceSkill = input.ReadSFixed32();
				break;
			case 442u:
				if (pveHeroStrengthen_ == null)
				{
					PveHeroStrengthen = new PveHeroStrengthen();
				}
				input.ReadMessage(PveHeroStrengthen);
				break;
			case 453u:
				BattleCount = input.ReadSFixed32();
				break;
			case 461u:
				ReRollNum = input.ReadSFixed32();
				break;
			case 469u:
				DevMovePoint = input.ReadSFixed32();
				break;
			case 477u:
				DevAttackPoint = input.ReadSFixed32();
				break;
			case 485u:
				DevDefensePoint = input.ReadSFixed32();
				break;
			case 493u:
				TeamId = input.ReadSFixed32();
				break;
			case 501u:
				SpecialScore = input.ReadSFixed32();
				break;
			case 509u:
				ReviveRoundNum = input.ReadSFixed32();
				break;
			case 517u:
				CureNum = input.ReadSFixed32();
				break;
			case 525u:
				SalaryNum = input.ReadSFixed32();
				break;
			case 533u:
				MarkNum = input.ReadSFixed32();
				break;
			case 536u:
				IsTrial = input.ReadBool();
				break;
			case 549u:
				BuyRelicNum = input.ReadSFixed32();
				break;
			case 557u:
				UseCardMaxNum = input.ReadSFixed32();
				break;
			case 565u:
				SkinPendant = input.ReadSFixed32();
				break;
			case 573u:
				CardDistance = input.ReadSFixed32();
				break;
			case 576u:
				CanCounter = input.ReadBool();
				break;
			case 589u:
				CounterNum = input.ReadSFixed32();
				break;
			case 597u:
				ModityNum = input.ReadSFixed32();
				break;
			case 600u:
				NotSelect = input.ReadBool();
				break;
			case 613u:
				MoveEffectSkillId = input.ReadSFixed32();
				break;
			case 621u:
				TimeOutNum = input.ReadSFixed32();
				break;
			case 629u:
				CardDiscount = input.ReadSFixed32();
				break;
			case 637u:
				DisCardPrice = input.ReadSFixed32();
				break;
			case 645u:
				CardIdx = input.ReadSFixed32();
				break;
			case 653u:
				DiceWeightIndex = input.ReadSFixed32();
				break;
			case 656u:
				LandBuffHandle = input.ReadBool();
				break;
			case 672u:
				HasSendChat = input.ReadBool();
				break;
			case 680u:
				HasSendPhrase = input.ReadBool();
				break;
			case 689u:
				OfflineTime = input.ReadSFixed64();
				break;
			case 696u:
				OfflineType = (Types.OfflineType)input.ReadEnum();
				break;
			case 709u:
				UniqueNum = input.ReadSFixed32();
				break;
			case 717u:
				CardAtk = input.ReadSFixed32();
				break;
			case 725u:
				EnergyNum = input.ReadSFixed32();
				break;
			case 728u:
				IsSkipRound = input.ReadBool();
				break;
			case 741u:
				CrimeNum = input.ReadSFixed32();
				break;
			case 8002u:
				doneActions_.AddEntriesFrom(ref input, _map_doneActions_codec);
				break;
			case 8008u:
				ActionEnd = input.ReadBool();
				break;
			case 8016u:
				OnlyUseCard = input.ReadBool();
				break;
			}
		}
	}
}
