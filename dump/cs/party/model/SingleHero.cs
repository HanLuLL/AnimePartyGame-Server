using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SingleHero : IMessage<SingleHero>, IMessage, IEquatable<SingleHero>, IDeepCloneable<SingleHero>, IBufferMessage
{
	private static readonly MessageParser<SingleHero> _parser = new MessageParser<SingleHero>(() => new SingleHero());

	private UnknownFieldSet _unknownFields;

	public const int HeroIdFieldNumber = 1;

	private int heroId_;

	public const int GoldFieldNumber = 2;

	private int gold_;

	public const int DevelopLandCountFieldNumber = 3;

	private int developLandCount_;

	public const int ForceFirstDicePointFieldNumber = 4;

	private int forceFirstDicePoint_;

	public const int DoubleDiceTimeFieldNumber = 5;

	private int doubleDiceTime_;

	public const int CardCostReductionBonusFieldNumber = 6;

	private int cardCostReductionBonus_;

	public const int CardRevenueBonusFieldNumber = 7;

	private int cardRevenueBonus_;

	public const int RelicFieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_relic_codec = FieldCodec.ForSFixed32(66u);

	private readonly RepeatedField<int> relic_ = new RepeatedField<int>();

	public const int StandLandIdFieldNumber = 9;

	private int standLandId_;

	public const int NextLandIdsFieldNumber = 10;

	private static readonly FieldCodec<int> _repeated_nextLandIds_codec = FieldCodec.ForSFixed32(82u);

	private readonly RepeatedField<int> nextLandIds_ = new RepeatedField<int>();

	public const int TotalGoldFieldNumber = 11;

	private int totalGold_;

	public const int RelicCurrPointFieldNumber = 12;

	private static readonly FieldCodec<int> _repeated_relicCurrPoint_codec = FieldCodec.ForSFixed32(98u);

	private readonly RepeatedField<int> relicCurrPoint_ = new RepeatedField<int>();

	public const int BuildingStarCoinFieldNumber = 13;

	private static readonly MapField<int, int>.Codec _map_buildingStarCoin_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 106u);

	private readonly MapField<int, int> buildingStarCoin_ = new MapField<int, int>();

	public const int RelicStarCoinFieldNumber = 14;

	private static readonly MapField<int, int>.Codec _map_relicStarCoin_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 114u);

	private readonly MapField<int, int> relicStarCoin_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SingleHero> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[105];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public int DevelopLandCount
	{
		get
		{
			return developLandCount_;
		}
		set
		{
			developLandCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ForceFirstDicePoint
	{
		get
		{
			return forceFirstDicePoint_;
		}
		set
		{
			forceFirstDicePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DoubleDiceTime
	{
		get
		{
			return doubleDiceTime_;
		}
		set
		{
			doubleDiceTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardCostReductionBonus
	{
		get
		{
			return cardCostReductionBonus_;
		}
		set
		{
			cardCostReductionBonus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardRevenueBonus
	{
		get
		{
			return cardRevenueBonus_;
		}
		set
		{
			cardRevenueBonus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Relic => relic_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int StandLandId
	{
		get
		{
			return standLandId_;
		}
		set
		{
			standLandId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> NextLandIds => nextLandIds_;

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
	public RepeatedField<int> RelicCurrPoint => relicCurrPoint_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> BuildingStarCoin => buildingStarCoin_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> RelicStarCoin => relicStarCoin_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleHero()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleHero(SingleHero other)
		: this()
	{
		heroId_ = other.heroId_;
		gold_ = other.gold_;
		developLandCount_ = other.developLandCount_;
		forceFirstDicePoint_ = other.forceFirstDicePoint_;
		doubleDiceTime_ = other.doubleDiceTime_;
		cardCostReductionBonus_ = other.cardCostReductionBonus_;
		cardRevenueBonus_ = other.cardRevenueBonus_;
		relic_ = other.relic_.Clone();
		standLandId_ = other.standLandId_;
		nextLandIds_ = other.nextLandIds_.Clone();
		totalGold_ = other.totalGold_;
		relicCurrPoint_ = other.relicCurrPoint_.Clone();
		buildingStarCoin_ = other.buildingStarCoin_.Clone();
		relicStarCoin_ = other.relicStarCoin_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleHero Clone()
	{
		return new SingleHero(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SingleHero);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SingleHero other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (HeroId != other.HeroId)
		{
			return false;
		}
		if (Gold != other.Gold)
		{
			return false;
		}
		if (DevelopLandCount != other.DevelopLandCount)
		{
			return false;
		}
		if (ForceFirstDicePoint != other.ForceFirstDicePoint)
		{
			return false;
		}
		if (DoubleDiceTime != other.DoubleDiceTime)
		{
			return false;
		}
		if (CardCostReductionBonus != other.CardCostReductionBonus)
		{
			return false;
		}
		if (CardRevenueBonus != other.CardRevenueBonus)
		{
			return false;
		}
		if (!relic_.Equals(other.relic_))
		{
			return false;
		}
		if (StandLandId != other.StandLandId)
		{
			return false;
		}
		if (!nextLandIds_.Equals(other.nextLandIds_))
		{
			return false;
		}
		if (TotalGold != other.TotalGold)
		{
			return false;
		}
		if (!relicCurrPoint_.Equals(other.relicCurrPoint_))
		{
			return false;
		}
		if (!BuildingStarCoin.Equals(other.BuildingStarCoin))
		{
			return false;
		}
		if (!RelicStarCoin.Equals(other.RelicStarCoin))
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
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
		}
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
		}
		if (DevelopLandCount != 0)
		{
			num ^= DevelopLandCount.GetHashCode();
		}
		if (ForceFirstDicePoint != 0)
		{
			num ^= ForceFirstDicePoint.GetHashCode();
		}
		if (DoubleDiceTime != 0)
		{
			num ^= DoubleDiceTime.GetHashCode();
		}
		if (CardCostReductionBonus != 0)
		{
			num ^= CardCostReductionBonus.GetHashCode();
		}
		if (CardRevenueBonus != 0)
		{
			num ^= CardRevenueBonus.GetHashCode();
		}
		num ^= relic_.GetHashCode();
		if (StandLandId != 0)
		{
			num ^= StandLandId.GetHashCode();
		}
		num ^= nextLandIds_.GetHashCode();
		if (TotalGold != 0)
		{
			num ^= TotalGold.GetHashCode();
		}
		num ^= relicCurrPoint_.GetHashCode();
		num ^= BuildingStarCoin.GetHashCode();
		num ^= RelicStarCoin.GetHashCode();
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
		if (HeroId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(HeroId);
		}
		if (Gold != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Gold);
		}
		if (DevelopLandCount != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DevelopLandCount);
		}
		if (ForceFirstDicePoint != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ForceFirstDicePoint);
		}
		if (DoubleDiceTime != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DoubleDiceTime);
		}
		if (CardCostReductionBonus != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(CardCostReductionBonus);
		}
		if (CardRevenueBonus != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(CardRevenueBonus);
		}
		relic_.WriteTo(ref output, _repeated_relic_codec);
		if (StandLandId != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(StandLandId);
		}
		nextLandIds_.WriteTo(ref output, _repeated_nextLandIds_codec);
		if (TotalGold != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(TotalGold);
		}
		relicCurrPoint_.WriteTo(ref output, _repeated_relicCurrPoint_codec);
		buildingStarCoin_.WriteTo(ref output, _map_buildingStarCoin_codec);
		relicStarCoin_.WriteTo(ref output, _map_relicStarCoin_codec);
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
		if (HeroId != 0)
		{
			num += 5;
		}
		if (Gold != 0)
		{
			num += 5;
		}
		if (DevelopLandCount != 0)
		{
			num += 5;
		}
		if (ForceFirstDicePoint != 0)
		{
			num += 5;
		}
		if (DoubleDiceTime != 0)
		{
			num += 5;
		}
		if (CardCostReductionBonus != 0)
		{
			num += 5;
		}
		if (CardRevenueBonus != 0)
		{
			num += 5;
		}
		num += relic_.CalculateSize(_repeated_relic_codec);
		if (StandLandId != 0)
		{
			num += 5;
		}
		num += nextLandIds_.CalculateSize(_repeated_nextLandIds_codec);
		if (TotalGold != 0)
		{
			num += 5;
		}
		num += relicCurrPoint_.CalculateSize(_repeated_relicCurrPoint_codec);
		num += buildingStarCoin_.CalculateSize(_map_buildingStarCoin_codec);
		num += relicStarCoin_.CalculateSize(_map_relicStarCoin_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SingleHero other)
	{
		if (other != null)
		{
			if (other.HeroId != 0)
			{
				HeroId = other.HeroId;
			}
			if (other.Gold != 0)
			{
				Gold = other.Gold;
			}
			if (other.DevelopLandCount != 0)
			{
				DevelopLandCount = other.DevelopLandCount;
			}
			if (other.ForceFirstDicePoint != 0)
			{
				ForceFirstDicePoint = other.ForceFirstDicePoint;
			}
			if (other.DoubleDiceTime != 0)
			{
				DoubleDiceTime = other.DoubleDiceTime;
			}
			if (other.CardCostReductionBonus != 0)
			{
				CardCostReductionBonus = other.CardCostReductionBonus;
			}
			if (other.CardRevenueBonus != 0)
			{
				CardRevenueBonus = other.CardRevenueBonus;
			}
			relic_.Add(other.relic_);
			if (other.StandLandId != 0)
			{
				StandLandId = other.StandLandId;
			}
			nextLandIds_.Add(other.nextLandIds_);
			if (other.TotalGold != 0)
			{
				TotalGold = other.TotalGold;
			}
			relicCurrPoint_.Add(other.relicCurrPoint_);
			buildingStarCoin_.MergeFrom(other.buildingStarCoin_);
			relicStarCoin_.MergeFrom(other.relicStarCoin_);
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
				HeroId = input.ReadSFixed32();
				break;
			case 21u:
				Gold = input.ReadSFixed32();
				break;
			case 29u:
				DevelopLandCount = input.ReadSFixed32();
				break;
			case 37u:
				ForceFirstDicePoint = input.ReadSFixed32();
				break;
			case 45u:
				DoubleDiceTime = input.ReadSFixed32();
				break;
			case 53u:
				CardCostReductionBonus = input.ReadSFixed32();
				break;
			case 61u:
				CardRevenueBonus = input.ReadSFixed32();
				break;
			case 66u:
			case 69u:
				relic_.AddEntriesFrom(ref input, _repeated_relic_codec);
				break;
			case 77u:
				StandLandId = input.ReadSFixed32();
				break;
			case 82u:
			case 85u:
				nextLandIds_.AddEntriesFrom(ref input, _repeated_nextLandIds_codec);
				break;
			case 93u:
				TotalGold = input.ReadSFixed32();
				break;
			case 98u:
			case 101u:
				relicCurrPoint_.AddEntriesFrom(ref input, _repeated_relicCurrPoint_codec);
				break;
			case 106u:
				buildingStarCoin_.AddEntriesFrom(ref input, _map_buildingStarCoin_codec);
				break;
			case 114u:
				relicStarCoin_.AddEntriesFrom(ref input, _map_relicStarCoin_codec);
				break;
			}
		}
	}
}
