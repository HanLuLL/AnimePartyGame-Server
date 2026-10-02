using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class PlayerFinishAchieve : IMessage<PlayerFinishAchieve>, IMessage, IEquatable<PlayerFinishAchieve>, IDeepCloneable<PlayerFinishAchieve>, IBufferMessage
{
	private static readonly MessageParser<PlayerFinishAchieve> _parser = new MessageParser<PlayerFinishAchieve>(() => new PlayerFinishAchieve());

	private UnknownFieldSet _unknownFields;

	public const int RelicsFieldNumber = 1;

	private static readonly FieldCodec<int> _repeated_relics_codec = FieldCodec.ForSFixed32(10u);

	private readonly RepeatedField<int> relics_ = new RepeatedField<int>();

	public const int KillCountFieldNumber = 2;

	private int killCount_;

	public const int TotalDamageFieldNumber = 3;

	private int totalDamage_;

	public const int TotalDieFieldNumber = 5;

	private int totalDie_;

	public const int TotalInjuredFieldNumber = 6;

	private int totalInjured_;

	public const int PvpResultGoldFieldNumber = 7;

	private int pvpResultGold_;

	public const int PvpResultLvFieldNumber = 8;

	private int pvpResultLv_;

	public const int TotalGoldFieldNumber = 9;

	private int totalGold_;

	public const int PveTransferGoldFieldNumber = 10;

	private int pveTransferGold_;

	public const int WinCountFieldNumber = 11;

	private int winCount_;

	public const int PkDamageMaxFieldNumber = 12;

	private int pkDamageMax_;

	public const int TreatmentScoreFieldNumber = 13;

	private int treatmentScore_;

	public const int MovePointFieldNumber = 14;

	private int movePoint_;

	public const int BattleDiceSixCountFieldNumber = 15;

	private int battleDiceSixCount_;

	public const int FinalKillBossFieldNumber = 16;

	private bool finalKillBoss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PlayerFinishAchieve> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[386];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Relics => relics_;

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
	public int PvpResultGold
	{
		get
		{
			return pvpResultGold_;
		}
		set
		{
			pvpResultGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PvpResultLv
	{
		get
		{
			return pvpResultLv_;
		}
		set
		{
			pvpResultLv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TotalGold
	{
		get
		{
			return totalGold_;
		}
		set
		{
			totalGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PveTransferGold
	{
		get
		{
			return pveTransferGold_;
		}
		set
		{
			pveTransferGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WinCount
	{
		get
		{
			return winCount_;
		}
		set
		{
			winCount_ = value;
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
	public PlayerFinishAchieve()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerFinishAchieve(PlayerFinishAchieve other)
		: this()
	{
		relics_ = other.relics_.Clone();
		killCount_ = other.killCount_;
		totalDamage_ = other.totalDamage_;
		totalDie_ = other.totalDie_;
		totalInjured_ = other.totalInjured_;
		pvpResultGold_ = other.pvpResultGold_;
		pvpResultLv_ = other.pvpResultLv_;
		totalGold_ = other.totalGold_;
		pveTransferGold_ = other.pveTransferGold_;
		winCount_ = other.winCount_;
		pkDamageMax_ = other.pkDamageMax_;
		treatmentScore_ = other.treatmentScore_;
		movePoint_ = other.movePoint_;
		battleDiceSixCount_ = other.battleDiceSixCount_;
		finalKillBoss_ = other.finalKillBoss_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerFinishAchieve Clone()
	{
		return new PlayerFinishAchieve(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PlayerFinishAchieve);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PlayerFinishAchieve other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!relics_.Equals(other.relics_))
		{
			return false;
		}
		if (KillCount != other.KillCount)
		{
			return false;
		}
		if (TotalDamage != other.TotalDamage)
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
		if (PvpResultGold != other.PvpResultGold)
		{
			return false;
		}
		if (PvpResultLv != other.PvpResultLv)
		{
			return false;
		}
		if (TotalGold != other.TotalGold)
		{
			return false;
		}
		if (PveTransferGold != other.PveTransferGold)
		{
			return false;
		}
		if (WinCount != other.WinCount)
		{
			return false;
		}
		if (PkDamageMax != other.PkDamageMax)
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
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= relics_.GetHashCode();
		if (KillCount != 0)
		{
			num ^= KillCount.GetHashCode();
		}
		if (TotalDamage != 0)
		{
			num ^= TotalDamage.GetHashCode();
		}
		if (TotalDie != 0)
		{
			num ^= TotalDie.GetHashCode();
		}
		if (TotalInjured != 0)
		{
			num ^= TotalInjured.GetHashCode();
		}
		if (PvpResultGold != 0)
		{
			num ^= PvpResultGold.GetHashCode();
		}
		if (PvpResultLv != 0)
		{
			num ^= PvpResultLv.GetHashCode();
		}
		if (TotalGold != 0)
		{
			num ^= TotalGold.GetHashCode();
		}
		if (PveTransferGold != 0)
		{
			num ^= PveTransferGold.GetHashCode();
		}
		if (WinCount != 0)
		{
			num ^= WinCount.GetHashCode();
		}
		if (PkDamageMax != 0)
		{
			num ^= PkDamageMax.GetHashCode();
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
		relics_.WriteTo(ref output, _repeated_relics_codec);
		if (KillCount != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(KillCount);
		}
		if (TotalDamage != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(TotalDamage);
		}
		if (TotalDie != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(TotalDie);
		}
		if (TotalInjured != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(TotalInjured);
		}
		if (PvpResultGold != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(PvpResultGold);
		}
		if (PvpResultLv != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(PvpResultLv);
		}
		if (TotalGold != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(TotalGold);
		}
		if (PveTransferGold != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(PveTransferGold);
		}
		if (WinCount != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(WinCount);
		}
		if (PkDamageMax != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(PkDamageMax);
		}
		if (TreatmentScore != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(TreatmentScore);
		}
		if (MovePoint != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(MovePoint);
		}
		if (BattleDiceSixCount != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(BattleDiceSixCount);
		}
		if (FinalKillBoss)
		{
			output.WriteRawTag(128, 1);
			output.WriteBool(FinalKillBoss);
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
		num += relics_.CalculateSize(_repeated_relics_codec);
		if (KillCount != 0)
		{
			num += 5;
		}
		if (TotalDamage != 0)
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
		if (PvpResultGold != 0)
		{
			num += 5;
		}
		if (PvpResultLv != 0)
		{
			num += 5;
		}
		if (TotalGold != 0)
		{
			num += 5;
		}
		if (PveTransferGold != 0)
		{
			num += 5;
		}
		if (WinCount != 0)
		{
			num += 5;
		}
		if (PkDamageMax != 0)
		{
			num += 5;
		}
		if (TreatmentScore != 0)
		{
			num += 5;
		}
		if (MovePoint != 0)
		{
			num += 5;
		}
		if (BattleDiceSixCount != 0)
		{
			num += 5;
		}
		if (FinalKillBoss)
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
	public void MergeFrom(PlayerFinishAchieve other)
	{
		if (other != null)
		{
			relics_.Add(other.relics_);
			if (other.KillCount != 0)
			{
				KillCount = other.KillCount;
			}
			if (other.TotalDamage != 0)
			{
				TotalDamage = other.TotalDamage;
			}
			if (other.TotalDie != 0)
			{
				TotalDie = other.TotalDie;
			}
			if (other.TotalInjured != 0)
			{
				TotalInjured = other.TotalInjured;
			}
			if (other.PvpResultGold != 0)
			{
				PvpResultGold = other.PvpResultGold;
			}
			if (other.PvpResultLv != 0)
			{
				PvpResultLv = other.PvpResultLv;
			}
			if (other.TotalGold != 0)
			{
				TotalGold = other.TotalGold;
			}
			if (other.PveTransferGold != 0)
			{
				PveTransferGold = other.PveTransferGold;
			}
			if (other.WinCount != 0)
			{
				WinCount = other.WinCount;
			}
			if (other.PkDamageMax != 0)
			{
				PkDamageMax = other.PkDamageMax;
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
			case 10u:
			case 13u:
				relics_.AddEntriesFrom(ref input, _repeated_relics_codec);
				break;
			case 21u:
				KillCount = input.ReadSFixed32();
				break;
			case 29u:
				TotalDamage = input.ReadSFixed32();
				break;
			case 45u:
				TotalDie = input.ReadSFixed32();
				break;
			case 53u:
				TotalInjured = input.ReadSFixed32();
				break;
			case 61u:
				PvpResultGold = input.ReadSFixed32();
				break;
			case 69u:
				PvpResultLv = input.ReadSFixed32();
				break;
			case 77u:
				TotalGold = input.ReadSFixed32();
				break;
			case 85u:
				PveTransferGold = input.ReadSFixed32();
				break;
			case 93u:
				WinCount = input.ReadSFixed32();
				break;
			case 101u:
				PkDamageMax = input.ReadSFixed32();
				break;
			case 109u:
				TreatmentScore = input.ReadSFixed32();
				break;
			case 117u:
				MovePoint = input.ReadSFixed32();
				break;
			case 125u:
				BattleDiceSixCount = input.ReadSFixed32();
				break;
			case 128u:
				FinalKillBoss = input.ReadBool();
				break;
			}
		}
	}
}
