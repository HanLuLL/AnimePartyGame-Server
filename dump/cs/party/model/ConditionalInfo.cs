using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ConditionalInfo : IMessage<ConditionalInfo>, IMessage, IEquatable<ConditionalInfo>, IDeepCloneable<ConditionalInfo>, IBufferMessage
{
	private static readonly MessageParser<ConditionalInfo> _parser = new MessageParser<ConditionalInfo>(() => new ConditionalInfo());

	private UnknownFieldSet _unknownFields;

	public const int KillCountFieldNumber = 1;

	private int killCount_;

	public const int TotalDamageFieldNumber = 2;

	private int totalDamage_;

	public const int PkDamageMaxFieldNumber = 3;

	private int pkDamageMax_;

	public const int TotalDieFieldNumber = 4;

	private int totalDie_;

	public const int TotalInjuredFieldNumber = 5;

	private int totalInjured_;

	public const int TotalTrapFieldNumber = 6;

	private int totalTrap_;

	public const int UseTrapFieldNumber = 7;

	private int useTrap_;

	public const int UseRoadblockFieldNumber = 8;

	private int useRoadblock_;

	public const int ThunderFieldNumber = 9;

	private int thunder_;

	public const int HospitalFieldNumber = 10;

	private int hospital_;

	public const int LotteryFieldNumber = 11;

	private int lottery_;

	public const int KillPvpMonsterFieldNumber = 12;

	private int killPvpMonster_;

	public const int GoldFieldNumber = 13;

	private int gold_;

	public const int KillThiefFieldNumber = 14;

	private int killThief_;

	public const int SakuraCountFieldNumber = 15;

	private int sakuraCount_;

	public const int TrainCountFieldNumber = 16;

	private int trainCount_;

	public const int HospitalSakuraCountFieldNumber = 17;

	private int hospitalSakuraCount_;

	public const int GoldRelicNumFieldNumber = 18;

	private int goldRelicNum_;

	public const int SinkAllOpponentsFieldNumber = 19;

	private int sinkAllOpponents_;

	public const int TransferGoldFieldNumber = 20;

	private int transferGold_;

	public const int InitGoldCountFieldNumber = 21;

	private int initGoldCount_;

	public const int TraitorCardCountFieldNumber = 22;

	private int traitorCardCount_;

	public const int KillPveMonsterFieldNumber = 23;

	private int killPveMonster_;

	public const int TreatmentScoreFieldNumber = 24;

	private int treatmentScore_;

	public const int MovePointFieldNumber = 25;

	private int movePoint_;

	public const int BattleDiceSixCountFieldNumber = 26;

	private int battleDiceSixCount_;

	public const int FinalKillBossFieldNumber = 27;

	private bool finalKillBoss_;

	public const int SelfDieFieldNumber = 28;

	private int selfDie_;

	public const int KillMonstersFieldNumber = 29;

	private static readonly MapField<int, int>.Codec _map_killMonsters_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 234u);

	private readonly MapField<int, int> killMonsters_ = new MapField<int, int>();

	public const int DragonBallSynthesisFieldNumber = 30;

	private int dragonBallSynthesis_;

	public const int SnakeBossMissionFieldNumber = 31;

	private int snakeBossMission_;

	public const int FishBossMissionFieldNumber = 32;

	private int fishBossMission_;

	public const int UseCandyCountFieldNumber = 33;

	private static readonly MapField<int, int>.Codec _map_useCandyCount_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 266u);

	private readonly MapField<int, int> useCandyCount_ = new MapField<int, int>();

	public const int TeammateKillMonsterFieldNumber = 34;

	private static readonly MapField<int, int>.Codec _map_teammateKillMonster_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 274u);

	private readonly MapField<int, int> teammateKillMonster_ = new MapField<int, int>();

	public const int UseCardFieldNumber = 35;

	private int useCard_;

	public const int UseSkillFieldNumber = 36;

	private int useSkill_;

	public const int ActionOverTimeFieldNumber = 37;

	private static readonly MapField<int, int>.Codec _map_actionOverTime_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 298u);

	private readonly MapField<int, int> actionOverTime_ = new MapField<int, int>();

	public const int TeammateStarCoinFieldNumber = 38;

	private static readonly MapField<int, int>.Codec _map_teammateStarCoin_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 306u);

	private readonly MapField<int, int> teammateStarCoin_ = new MapField<int, int>();

	public const int KillPlayersFieldNumber = 39;

	private static readonly MapField<long, int>.Codec _map_killPlayers_codec = new MapField<long, int>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForSFixed32(21u, 0), 314u);

	private readonly MapField<long, int> killPlayers_ = new MapField<long, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ConditionalInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[59];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public int PkDamageMax
	{
		get
		{
			return pkDamageMax_;
		}
		set
		{
			pkDamageMax_ = value;
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
	public int TotalTrap
	{
		get
		{
			return totalTrap_;
		}
		set
		{
			totalTrap_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseTrap
	{
		get
		{
			return useTrap_;
		}
		set
		{
			useTrap_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseRoadblock
	{
		get
		{
			return useRoadblock_;
		}
		set
		{
			useRoadblock_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Thunder
	{
		get
		{
			return thunder_;
		}
		set
		{
			thunder_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Hospital
	{
		get
		{
			return hospital_;
		}
		set
		{
			hospital_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Lottery
	{
		get
		{
			return lottery_;
		}
		set
		{
			lottery_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int KillPvpMonster
	{
		get
		{
			return killPvpMonster_;
		}
		set
		{
			killPvpMonster_ = value;
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
	public int KillThief
	{
		get
		{
			return killThief_;
		}
		set
		{
			killThief_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SakuraCount
	{
		get
		{
			return sakuraCount_;
		}
		set
		{
			sakuraCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TrainCount
	{
		get
		{
			return trainCount_;
		}
		set
		{
			trainCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HospitalSakuraCount
	{
		get
		{
			return hospitalSakuraCount_;
		}
		set
		{
			hospitalSakuraCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoldRelicNum
	{
		get
		{
			return goldRelicNum_;
		}
		set
		{
			goldRelicNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SinkAllOpponents
	{
		get
		{
			return sinkAllOpponents_;
		}
		set
		{
			sinkAllOpponents_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TransferGold
	{
		get
		{
			return transferGold_;
		}
		set
		{
			transferGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int InitGoldCount
	{
		get
		{
			return initGoldCount_;
		}
		set
		{
			initGoldCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TraitorCardCount
	{
		get
		{
			return traitorCardCount_;
		}
		set
		{
			traitorCardCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int KillPveMonster
	{
		get
		{
			return killPveMonster_;
		}
		set
		{
			killPveMonster_ = value;
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
	public int BattleDiceSixCount
	{
		get
		{
			return battleDiceSixCount_;
		}
		set
		{
			battleDiceSixCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool FinalKillBoss
	{
		get
		{
			return finalKillBoss_;
		}
		set
		{
			finalKillBoss_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SelfDie
	{
		get
		{
			return selfDie_;
		}
		set
		{
			selfDie_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> KillMonsters => killMonsters_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DragonBallSynthesis
	{
		get
		{
			return dragonBallSynthesis_;
		}
		set
		{
			dragonBallSynthesis_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SnakeBossMission
	{
		get
		{
			return snakeBossMission_;
		}
		set
		{
			snakeBossMission_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FishBossMission
	{
		get
		{
			return fishBossMission_;
		}
		set
		{
			fishBossMission_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> UseCandyCount => useCandyCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> TeammateKillMonster => teammateKillMonster_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseCard
	{
		get
		{
			return useCard_;
		}
		set
		{
			useCard_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseSkill
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
	public MapField<int, int> ActionOverTime => actionOverTime_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> TeammateStarCoin => teammateStarCoin_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, int> KillPlayers => killPlayers_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConditionalInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConditionalInfo(ConditionalInfo other)
		: this()
	{
		killCount_ = other.killCount_;
		totalDamage_ = other.totalDamage_;
		pkDamageMax_ = other.pkDamageMax_;
		totalDie_ = other.totalDie_;
		totalInjured_ = other.totalInjured_;
		totalTrap_ = other.totalTrap_;
		useTrap_ = other.useTrap_;
		useRoadblock_ = other.useRoadblock_;
		thunder_ = other.thunder_;
		hospital_ = other.hospital_;
		lottery_ = other.lottery_;
		killPvpMonster_ = other.killPvpMonster_;
		gold_ = other.gold_;
		killThief_ = other.killThief_;
		sakuraCount_ = other.sakuraCount_;
		trainCount_ = other.trainCount_;
		hospitalSakuraCount_ = other.hospitalSakuraCount_;
		goldRelicNum_ = other.goldRelicNum_;
		sinkAllOpponents_ = other.sinkAllOpponents_;
		transferGold_ = other.transferGold_;
		initGoldCount_ = other.initGoldCount_;
		traitorCardCount_ = other.traitorCardCount_;
		killPveMonster_ = other.killPveMonster_;
		treatmentScore_ = other.treatmentScore_;
		movePoint_ = other.movePoint_;
		battleDiceSixCount_ = other.battleDiceSixCount_;
		finalKillBoss_ = other.finalKillBoss_;
		selfDie_ = other.selfDie_;
		killMonsters_ = other.killMonsters_.Clone();
		dragonBallSynthesis_ = other.dragonBallSynthesis_;
		snakeBossMission_ = other.snakeBossMission_;
		fishBossMission_ = other.fishBossMission_;
		useCandyCount_ = other.useCandyCount_.Clone();
		teammateKillMonster_ = other.teammateKillMonster_.Clone();
		useCard_ = other.useCard_;
		useSkill_ = other.useSkill_;
		actionOverTime_ = other.actionOverTime_.Clone();
		teammateStarCoin_ = other.teammateStarCoin_.Clone();
		killPlayers_ = other.killPlayers_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConditionalInfo Clone()
	{
		return new ConditionalInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ConditionalInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ConditionalInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (KillCount != other.KillCount)
		{
			return false;
		}
		if (TotalDamage != other.TotalDamage)
		{
			return false;
		}
		if (PkDamageMax != other.PkDamageMax)
		{
			return false;
		}
		if (TotalDie != other.TotalDie)
		{
			return false;
		}
		if (TotalInjured != other.TotalInjured)
		{
			return false;
		}
		if (TotalTrap != other.TotalTrap)
		{
			return false;
		}
		if (UseTrap != other.UseTrap)
		{
			return false;
		}
		if (UseRoadblock != other.UseRoadblock)
		{
			return false;
		}
		if (Thunder != other.Thunder)
		{
			return false;
		}
		if (Hospital != other.Hospital)
		{
			return false;
		}
		if (Lottery != other.Lottery)
		{
			return false;
		}
		if (KillPvpMonster != other.KillPvpMonster)
		{
			return false;
		}
		if (Gold != other.Gold)
		{
			return false;
		}
		if (KillThief != other.KillThief)
		{
			return false;
		}
		if (SakuraCount != other.SakuraCount)
		{
			return false;
		}
		if (TrainCount != other.TrainCount)
		{
			return false;
		}
		if (HospitalSakuraCount != other.HospitalSakuraCount)
		{
			return false;
		}
		if (GoldRelicNum != other.GoldRelicNum)
		{
			return false;
		}
		if (SinkAllOpponents != other.SinkAllOpponents)
		{
			return false;
		}
		if (TransferGold != other.TransferGold)
		{
			return false;
		}
		if (InitGoldCount != other.InitGoldCount)
		{
			return false;
		}
		if (TraitorCardCount != other.TraitorCardCount)
		{
			return false;
		}
		if (KillPveMonster != other.KillPveMonster)
		{
			return false;
		}
		if (TreatmentScore != other.TreatmentScore)
		{
			return false;
		}
		if (MovePoint != other.MovePoint)
		{
			return false;
		}
		if (BattleDiceSixCount != other.BattleDiceSixCount)
		{
			return false;
		}
		if (FinalKillBoss != other.FinalKillBoss)
		{
			return false;
		}
		if (SelfDie != other.SelfDie)
		{
			return false;
		}
		if (!KillMonsters.Equals(other.KillMonsters))
		{
			return false;
		}
		if (DragonBallSynthesis != other.DragonBallSynthesis)
		{
			return false;
		}
		if (SnakeBossMission != other.SnakeBossMission)
		{
			return false;
		}
		if (FishBossMission != other.FishBossMission)
		{
			return false;
		}
		if (!UseCandyCount.Equals(other.UseCandyCount))
		{
			return false;
		}
		if (!TeammateKillMonster.Equals(other.TeammateKillMonster))
		{
			return false;
		}
		if (UseCard != other.UseCard)
		{
			return false;
		}
		if (UseSkill != other.UseSkill)
		{
			return false;
		}
		if (!ActionOverTime.Equals(other.ActionOverTime))
		{
			return false;
		}
		if (!TeammateStarCoin.Equals(other.TeammateStarCoin))
		{
			return false;
		}
		if (!KillPlayers.Equals(other.KillPlayers))
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
		if (KillCount != 0)
		{
			num ^= KillCount.GetHashCode();
		}
		if (TotalDamage != 0)
		{
			num ^= TotalDamage.GetHashCode();
		}
		if (PkDamageMax != 0)
		{
			num ^= PkDamageMax.GetHashCode();
		}
		if (TotalDie != 0)
		{
			num ^= TotalDie.GetHashCode();
		}
		if (TotalInjured != 0)
		{
			num ^= TotalInjured.GetHashCode();
		}
		if (TotalTrap != 0)
		{
			num ^= TotalTrap.GetHashCode();
		}
		if (UseTrap != 0)
		{
			num ^= UseTrap.GetHashCode();
		}
		if (UseRoadblock != 0)
		{
			num ^= UseRoadblock.GetHashCode();
		}
		if (Thunder != 0)
		{
			num ^= Thunder.GetHashCode();
		}
		if (Hospital != 0)
		{
			num ^= Hospital.GetHashCode();
		}
		if (Lottery != 0)
		{
			num ^= Lottery.GetHashCode();
		}
		if (KillPvpMonster != 0)
		{
			num ^= KillPvpMonster.GetHashCode();
		}
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
		}
		if (KillThief != 0)
		{
			num ^= KillThief.GetHashCode();
		}
		if (SakuraCount != 0)
		{
			num ^= SakuraCount.GetHashCode();
		}
		if (TrainCount != 0)
		{
			num ^= TrainCount.GetHashCode();
		}
		if (HospitalSakuraCount != 0)
		{
			num ^= HospitalSakuraCount.GetHashCode();
		}
		if (GoldRelicNum != 0)
		{
			num ^= GoldRelicNum.GetHashCode();
		}
		if (SinkAllOpponents != 0)
		{
			num ^= SinkAllOpponents.GetHashCode();
		}
		if (TransferGold != 0)
		{
			num ^= TransferGold.GetHashCode();
		}
		if (InitGoldCount != 0)
		{
			num ^= InitGoldCount.GetHashCode();
		}
		if (TraitorCardCount != 0)
		{
			num ^= TraitorCardCount.GetHashCode();
		}
		if (KillPveMonster != 0)
		{
			num ^= KillPveMonster.GetHashCode();
		}
		if (TreatmentScore != 0)
		{
			num ^= TreatmentScore.GetHashCode();
		}
		if (MovePoint != 0)
		{
			num ^= MovePoint.GetHashCode();
		}
		if (BattleDiceSixCount != 0)
		{
			num ^= BattleDiceSixCount.GetHashCode();
		}
		if (FinalKillBoss)
		{
			num ^= FinalKillBoss.GetHashCode();
		}
		if (SelfDie != 0)
		{
			num ^= SelfDie.GetHashCode();
		}
		num ^= KillMonsters.GetHashCode();
		if (DragonBallSynthesis != 0)
		{
			num ^= DragonBallSynthesis.GetHashCode();
		}
		if (SnakeBossMission != 0)
		{
			num ^= SnakeBossMission.GetHashCode();
		}
		if (FishBossMission != 0)
		{
			num ^= FishBossMission.GetHashCode();
		}
		num ^= UseCandyCount.GetHashCode();
		num ^= TeammateKillMonster.GetHashCode();
		if (UseCard != 0)
		{
			num ^= UseCard.GetHashCode();
		}
		if (UseSkill != 0)
		{
			num ^= UseSkill.GetHashCode();
		}
		num ^= ActionOverTime.GetHashCode();
		num ^= TeammateStarCoin.GetHashCode();
		num ^= KillPlayers.GetHashCode();
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
		if (KillCount != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(KillCount);
		}
		if (TotalDamage != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(TotalDamage);
		}
		if (PkDamageMax != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(PkDamageMax);
		}
		if (TotalDie != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(TotalDie);
		}
		if (TotalInjured != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(TotalInjured);
		}
		if (TotalTrap != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(TotalTrap);
		}
		if (UseTrap != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(UseTrap);
		}
		if (UseRoadblock != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(UseRoadblock);
		}
		if (Thunder != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(Thunder);
		}
		if (Hospital != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(Hospital);
		}
		if (Lottery != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(Lottery);
		}
		if (KillPvpMonster != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(KillPvpMonster);
		}
		if (Gold != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(Gold);
		}
		if (KillThief != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(KillThief);
		}
		if (SakuraCount != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(SakuraCount);
		}
		if (TrainCount != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(TrainCount);
		}
		if (HospitalSakuraCount != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(HospitalSakuraCount);
		}
		if (GoldRelicNum != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(GoldRelicNum);
		}
		if (SinkAllOpponents != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(SinkAllOpponents);
		}
		if (TransferGold != 0)
		{
			output.WriteRawTag(165, 1);
			output.WriteSFixed32(TransferGold);
		}
		if (InitGoldCount != 0)
		{
			output.WriteRawTag(173, 1);
			output.WriteSFixed32(InitGoldCount);
		}
		if (TraitorCardCount != 0)
		{
			output.WriteRawTag(181, 1);
			output.WriteSFixed32(TraitorCardCount);
		}
		if (KillPveMonster != 0)
		{
			output.WriteRawTag(189, 1);
			output.WriteSFixed32(KillPveMonster);
		}
		if (TreatmentScore != 0)
		{
			output.WriteRawTag(197, 1);
			output.WriteSFixed32(TreatmentScore);
		}
		if (MovePoint != 0)
		{
			output.WriteRawTag(205, 1);
			output.WriteSFixed32(MovePoint);
		}
		if (BattleDiceSixCount != 0)
		{
			output.WriteRawTag(213, 1);
			output.WriteSFixed32(BattleDiceSixCount);
		}
		if (FinalKillBoss)
		{
			output.WriteRawTag(216, 1);
			output.WriteBool(FinalKillBoss);
		}
		if (SelfDie != 0)
		{
			output.WriteRawTag(229, 1);
			output.WriteSFixed32(SelfDie);
		}
		killMonsters_.WriteTo(ref output, _map_killMonsters_codec);
		if (DragonBallSynthesis != 0)
		{
			output.WriteRawTag(245, 1);
			output.WriteSFixed32(DragonBallSynthesis);
		}
		if (SnakeBossMission != 0)
		{
			output.WriteRawTag(253, 1);
			output.WriteSFixed32(SnakeBossMission);
		}
		if (FishBossMission != 0)
		{
			output.WriteRawTag(133, 2);
			output.WriteSFixed32(FishBossMission);
		}
		useCandyCount_.WriteTo(ref output, _map_useCandyCount_codec);
		teammateKillMonster_.WriteTo(ref output, _map_teammateKillMonster_codec);
		if (UseCard != 0)
		{
			output.WriteRawTag(157, 2);
			output.WriteSFixed32(UseCard);
		}
		if (UseSkill != 0)
		{
			output.WriteRawTag(165, 2);
			output.WriteSFixed32(UseSkill);
		}
		actionOverTime_.WriteTo(ref output, _map_actionOverTime_codec);
		teammateStarCoin_.WriteTo(ref output, _map_teammateStarCoin_codec);
		killPlayers_.WriteTo(ref output, _map_killPlayers_codec);
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
		if (KillCount != 0)
		{
			num += 5;
		}
		if (TotalDamage != 0)
		{
			num += 5;
		}
		if (PkDamageMax != 0)
		{
			num += 5;
		}
		if (TotalDie != 0)
		{
			num += 5;
		}
		if (TotalInjured != 0)
		{
			num += 5;
		}
		if (TotalTrap != 0)
		{
			num += 5;
		}
		if (UseTrap != 0)
		{
			num += 5;
		}
		if (UseRoadblock != 0)
		{
			num += 5;
		}
		if (Thunder != 0)
		{
			num += 5;
		}
		if (Hospital != 0)
		{
			num += 5;
		}
		if (Lottery != 0)
		{
			num += 5;
		}
		if (KillPvpMonster != 0)
		{
			num += 5;
		}
		if (Gold != 0)
		{
			num += 5;
		}
		if (KillThief != 0)
		{
			num += 5;
		}
		if (SakuraCount != 0)
		{
			num += 5;
		}
		if (TrainCount != 0)
		{
			num += 6;
		}
		if (HospitalSakuraCount != 0)
		{
			num += 6;
		}
		if (GoldRelicNum != 0)
		{
			num += 6;
		}
		if (SinkAllOpponents != 0)
		{
			num += 6;
		}
		if (TransferGold != 0)
		{
			num += 6;
		}
		if (InitGoldCount != 0)
		{
			num += 6;
		}
		if (TraitorCardCount != 0)
		{
			num += 6;
		}
		if (KillPveMonster != 0)
		{
			num += 6;
		}
		if (TreatmentScore != 0)
		{
			num += 6;
		}
		if (MovePoint != 0)
		{
			num += 6;
		}
		if (BattleDiceSixCount != 0)
		{
			num += 6;
		}
		if (FinalKillBoss)
		{
			num += 3;
		}
		if (SelfDie != 0)
		{
			num += 6;
		}
		num += killMonsters_.CalculateSize(_map_killMonsters_codec);
		if (DragonBallSynthesis != 0)
		{
			num += 6;
		}
		if (SnakeBossMission != 0)
		{
			num += 6;
		}
		if (FishBossMission != 0)
		{
			num += 6;
		}
		num += useCandyCount_.CalculateSize(_map_useCandyCount_codec);
		num += teammateKillMonster_.CalculateSize(_map_teammateKillMonster_codec);
		if (UseCard != 0)
		{
			num += 6;
		}
		if (UseSkill != 0)
		{
			num += 6;
		}
		num += actionOverTime_.CalculateSize(_map_actionOverTime_codec);
		num += teammateStarCoin_.CalculateSize(_map_teammateStarCoin_codec);
		num += killPlayers_.CalculateSize(_map_killPlayers_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ConditionalInfo other)
	{
		if (other != null)
		{
			if (other.KillCount != 0)
			{
				KillCount = other.KillCount;
			}
			if (other.TotalDamage != 0)
			{
				TotalDamage = other.TotalDamage;
			}
			if (other.PkDamageMax != 0)
			{
				PkDamageMax = other.PkDamageMax;
			}
			if (other.TotalDie != 0)
			{
				TotalDie = other.TotalDie;
			}
			if (other.TotalInjured != 0)
			{
				TotalInjured = other.TotalInjured;
			}
			if (other.TotalTrap != 0)
			{
				TotalTrap = other.TotalTrap;
			}
			if (other.UseTrap != 0)
			{
				UseTrap = other.UseTrap;
			}
			if (other.UseRoadblock != 0)
			{
				UseRoadblock = other.UseRoadblock;
			}
			if (other.Thunder != 0)
			{
				Thunder = other.Thunder;
			}
			if (other.Hospital != 0)
			{
				Hospital = other.Hospital;
			}
			if (other.Lottery != 0)
			{
				Lottery = other.Lottery;
			}
			if (other.KillPvpMonster != 0)
			{
				KillPvpMonster = other.KillPvpMonster;
			}
			if (other.Gold != 0)
			{
				Gold = other.Gold;
			}
			if (other.KillThief != 0)
			{
				KillThief = other.KillThief;
			}
			if (other.SakuraCount != 0)
			{
				SakuraCount = other.SakuraCount;
			}
			if (other.TrainCount != 0)
			{
				TrainCount = other.TrainCount;
			}
			if (other.HospitalSakuraCount != 0)
			{
				HospitalSakuraCount = other.HospitalSakuraCount;
			}
			if (other.GoldRelicNum != 0)
			{
				GoldRelicNum = other.GoldRelicNum;
			}
			if (other.SinkAllOpponents != 0)
			{
				SinkAllOpponents = other.SinkAllOpponents;
			}
			if (other.TransferGold != 0)
			{
				TransferGold = other.TransferGold;
			}
			if (other.InitGoldCount != 0)
			{
				InitGoldCount = other.InitGoldCount;
			}
			if (other.TraitorCardCount != 0)
			{
				TraitorCardCount = other.TraitorCardCount;
			}
			if (other.KillPveMonster != 0)
			{
				KillPveMonster = other.KillPveMonster;
			}
			if (other.TreatmentScore != 0)
			{
				TreatmentScore = other.TreatmentScore;
			}
			if (other.MovePoint != 0)
			{
				MovePoint = other.MovePoint;
			}
			if (other.BattleDiceSixCount != 0)
			{
				BattleDiceSixCount = other.BattleDiceSixCount;
			}
			if (other.FinalKillBoss)
			{
				FinalKillBoss = other.FinalKillBoss;
			}
			if (other.SelfDie != 0)
			{
				SelfDie = other.SelfDie;
			}
			killMonsters_.MergeFrom(other.killMonsters_);
			if (other.DragonBallSynthesis != 0)
			{
				DragonBallSynthesis = other.DragonBallSynthesis;
			}
			if (other.SnakeBossMission != 0)
			{
				SnakeBossMission = other.SnakeBossMission;
			}
			if (other.FishBossMission != 0)
			{
				FishBossMission = other.FishBossMission;
			}
			useCandyCount_.MergeFrom(other.useCandyCount_);
			teammateKillMonster_.MergeFrom(other.teammateKillMonster_);
			if (other.UseCard != 0)
			{
				UseCard = other.UseCard;
			}
			if (other.UseSkill != 0)
			{
				UseSkill = other.UseSkill;
			}
			actionOverTime_.MergeFrom(other.actionOverTime_);
			teammateStarCoin_.MergeFrom(other.teammateStarCoin_);
			killPlayers_.MergeFrom(other.killPlayers_);
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
				KillCount = input.ReadSFixed32();
				break;
			case 21u:
				TotalDamage = input.ReadSFixed32();
				break;
			case 29u:
				PkDamageMax = input.ReadSFixed32();
				break;
			case 37u:
				TotalDie = input.ReadSFixed32();
				break;
			case 45u:
				TotalInjured = input.ReadSFixed32();
				break;
			case 53u:
				TotalTrap = input.ReadSFixed32();
				break;
			case 61u:
				UseTrap = input.ReadSFixed32();
				break;
			case 69u:
				UseRoadblock = input.ReadSFixed32();
				break;
			case 77u:
				Thunder = input.ReadSFixed32();
				break;
			case 85u:
				Hospital = input.ReadSFixed32();
				break;
			case 93u:
				Lottery = input.ReadSFixed32();
				break;
			case 101u:
				KillPvpMonster = input.ReadSFixed32();
				break;
			case 109u:
				Gold = input.ReadSFixed32();
				break;
			case 117u:
				KillThief = input.ReadSFixed32();
				break;
			case 125u:
				SakuraCount = input.ReadSFixed32();
				break;
			case 133u:
				TrainCount = input.ReadSFixed32();
				break;
			case 141u:
				HospitalSakuraCount = input.ReadSFixed32();
				break;
			case 149u:
				GoldRelicNum = input.ReadSFixed32();
				break;
			case 157u:
				SinkAllOpponents = input.ReadSFixed32();
				break;
			case 165u:
				TransferGold = input.ReadSFixed32();
				break;
			case 173u:
				InitGoldCount = input.ReadSFixed32();
				break;
			case 181u:
				TraitorCardCount = input.ReadSFixed32();
				break;
			case 189u:
				KillPveMonster = input.ReadSFixed32();
				break;
			case 197u:
				TreatmentScore = input.ReadSFixed32();
				break;
			case 205u:
				MovePoint = input.ReadSFixed32();
				break;
			case 213u:
				BattleDiceSixCount = input.ReadSFixed32();
				break;
			case 216u:
				FinalKillBoss = input.ReadBool();
				break;
			case 229u:
				SelfDie = input.ReadSFixed32();
				break;
			case 234u:
				killMonsters_.AddEntriesFrom(ref input, _map_killMonsters_codec);
				break;
			case 245u:
				DragonBallSynthesis = input.ReadSFixed32();
				break;
			case 253u:
				SnakeBossMission = input.ReadSFixed32();
				break;
			case 261u:
				FishBossMission = input.ReadSFixed32();
				break;
			case 266u:
				useCandyCount_.AddEntriesFrom(ref input, _map_useCandyCount_codec);
				break;
			case 274u:
				teammateKillMonster_.AddEntriesFrom(ref input, _map_teammateKillMonster_codec);
				break;
			case 285u:
				UseCard = input.ReadSFixed32();
				break;
			case 293u:
				UseSkill = input.ReadSFixed32();
				break;
			case 298u:
				actionOverTime_.AddEntriesFrom(ref input, _map_actionOverTime_codec);
				break;
			case 306u:
				teammateStarCoin_.AddEntriesFrom(ref input, _map_teammateStarCoin_codec);
				break;
			case 314u:
				killPlayers_.AddEntriesFrom(ref input, _map_killPlayers_codec);
				break;
			}
		}
	}
}
