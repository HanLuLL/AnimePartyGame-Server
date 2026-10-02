using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using GameLogic;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using Tools;

public sealed class GameModeInfoConfigure : IMessage<GameModeInfoConfigure>, IMessage, IEquatable<GameModeInfoConfigure>, IDeepCloneable<GameModeInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<GameModeInfoConfigure> _parser = new MessageParser<GameModeInfoConfigure>(() => new GameModeInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int MapModeTypeFieldNumber = 1;

	private MapModeType mapModeType_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int IsShowFieldNumber = 3;

	private bool isShow_;

	public const int AccountExpFieldNumber = 4;

	private int accountExp_;

	public const int GoldFirstFieldNumber = 5;

	private int goldFirst_;

	public const int GoldOtherFieldNumber = 6;

	private int goldOther_;

	public const int MapIDFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_mapID_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> mapID_ = new RepeatedField<int>();

	public const int UpgradeFieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_upgrade_codec = FieldCodec.ForSFixed32(66u);

	private readonly RepeatedField<int> upgrade_ = new RepeatedField<int>();

	public const int VictoryParamFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_victoryParam_codec = FieldCodec.ForSFixed32(74u);

	private readonly RepeatedField<int> victoryParam_ = new RepeatedField<int>();

	public const int CardInHandLimitFieldNumber = 10;

	private int cardInHandLimit_;

	public const int BattlecostFieldNumber = 11;

	private int battlecost_;

	public const int DistributeResourcesFieldNumber = 12;

	private static readonly FieldCodec<int> _repeated_distributeResources_codec = FieldCodec.ForSInt32(98u);

	private readonly RepeatedField<int> distributeResources_ = new RepeatedField<int>();

	public const int BountyParamsFieldNumber = 13;

	private static readonly FieldCodec<int> _repeated_bountyParams_codec = FieldCodec.ForSInt32(106u);

	private readonly RepeatedField<int> bountyParams_ = new RepeatedField<int>();

	public const int ChristmasParamsFieldNumber = 14;

	private static readonly FieldCodec<int> _repeated_christmasParams_codec = FieldCodec.ForSInt32(114u);

	private readonly RepeatedField<int> christmasParams_ = new RepeatedField<int>();

	public const int CardNumFixFieldNumber = 15;

	private int cardNumFix_;

	public const int CoolDownFixFieldNumber = 16;

	private int coolDownFix_;

	public const int BuffsFieldNumber = 17;

	private static readonly FieldCodec<int> _repeated_buffs_codec = FieldCodec.ForSInt32(138u);

	private readonly RepeatedField<int> buffs_ = new RepeatedField<int>();

	public const int BanCharactersFieldNumber = 18;

	private static readonly FieldCodec<int> _repeated_banCharacters_codec = FieldCodec.ForSInt32(146u);

	private readonly RepeatedField<int> banCharacters_ = new RepeatedField<int>();

	public const int OrderWeightFieldNumber = 19;

	private int orderWeight_;

	public const int RulesIDFieldNumber = 20;

	private int rulesID_;

	public const int IsMatchFieldNumber = 21;

	private bool isMatch_;

	public const int MatchDesIDFieldNumber = 22;

	private int matchDesID_;

	public const int ModeDesIDFieldNumber = 23;

	private int modeDesID_;

	public const int BeginTimeFieldNumber = 24;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 25;

	private Timestamp endTime_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GameModeInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GameModeReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapModeType MapModeType
	{
		get
		{
			return mapModeType_;
		}
		private set
		{
			mapModeType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsShow
	{
		get
		{
			return isShow_;
		}
		private set
		{
			isShow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AccountExp
	{
		get
		{
			return accountExp_;
		}
		private set
		{
			accountExp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoldFirst
	{
		get
		{
			return goldFirst_;
		}
		private set
		{
			goldFirst_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoldOther
	{
		get
		{
			return goldOther_;
		}
		private set
		{
			goldOther_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MapID => mapID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Upgrade => upgrade_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> VictoryParam => victoryParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardInHandLimit
	{
		get
		{
			return cardInHandLimit_;
		}
		private set
		{
			cardInHandLimit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Battlecost
	{
		get
		{
			return battlecost_;
		}
		private set
		{
			battlecost_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> DistributeResources => distributeResources_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BountyParams => bountyParams_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> ChristmasParams => christmasParams_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardNumFix
	{
		get
		{
			return cardNumFix_;
		}
		private set
		{
			cardNumFix_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CoolDownFix
	{
		get
		{
			return coolDownFix_;
		}
		private set
		{
			coolDownFix_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Buffs => buffs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BanCharacters => banCharacters_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OrderWeight
	{
		get
		{
			return orderWeight_;
		}
		private set
		{
			orderWeight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RulesID
	{
		get
		{
			return rulesID_;
		}
		private set
		{
			rulesID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsMatch
	{
		get
		{
			return isMatch_;
		}
		private set
		{
			isMatch_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MatchDesID
	{
		get
		{
			return matchDesID_;
		}
		private set
		{
			matchDesID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ModeDesID
	{
		get
		{
			return modeDesID_;
		}
		private set
		{
			modeDesID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp BeginTime
	{
		get
		{
			return beginTime_;
		}
		private set
		{
			beginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTime
	{
		get
		{
			return endTime_;
		}
		private set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeInfoConfigure(GameModeInfoConfigure other)
		: this()
	{
		mapModeType_ = other.mapModeType_;
		nameID_ = other.nameID_;
		isShow_ = other.isShow_;
		accountExp_ = other.accountExp_;
		goldFirst_ = other.goldFirst_;
		goldOther_ = other.goldOther_;
		mapID_ = other.mapID_.Clone();
		upgrade_ = other.upgrade_.Clone();
		victoryParam_ = other.victoryParam_.Clone();
		cardInHandLimit_ = other.cardInHandLimit_;
		battlecost_ = other.battlecost_;
		distributeResources_ = other.distributeResources_.Clone();
		bountyParams_ = other.bountyParams_.Clone();
		christmasParams_ = other.christmasParams_.Clone();
		cardNumFix_ = other.cardNumFix_;
		coolDownFix_ = other.coolDownFix_;
		buffs_ = other.buffs_.Clone();
		banCharacters_ = other.banCharacters_.Clone();
		orderWeight_ = other.orderWeight_;
		rulesID_ = other.rulesID_;
		isMatch_ = other.isMatch_;
		matchDesID_ = other.matchDesID_;
		modeDesID_ = other.modeDesID_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeInfoConfigure Clone()
	{
		return new GameModeInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GameModeInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GameModeInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MapModeType != other.MapModeType)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (IsShow != other.IsShow)
		{
			return false;
		}
		if (AccountExp != other.AccountExp)
		{
			return false;
		}
		if (GoldFirst != other.GoldFirst)
		{
			return false;
		}
		if (GoldOther != other.GoldOther)
		{
			return false;
		}
		if (!mapID_.Equals(other.mapID_))
		{
			return false;
		}
		if (!upgrade_.Equals(other.upgrade_))
		{
			return false;
		}
		if (!victoryParam_.Equals(other.victoryParam_))
		{
			return false;
		}
		if (CardInHandLimit != other.CardInHandLimit)
		{
			return false;
		}
		if (Battlecost != other.Battlecost)
		{
			return false;
		}
		if (!distributeResources_.Equals(other.distributeResources_))
		{
			return false;
		}
		if (!bountyParams_.Equals(other.bountyParams_))
		{
			return false;
		}
		if (!christmasParams_.Equals(other.christmasParams_))
		{
			return false;
		}
		if (CardNumFix != other.CardNumFix)
		{
			return false;
		}
		if (CoolDownFix != other.CoolDownFix)
		{
			return false;
		}
		if (!buffs_.Equals(other.buffs_))
		{
			return false;
		}
		if (!banCharacters_.Equals(other.banCharacters_))
		{
			return false;
		}
		if (OrderWeight != other.OrderWeight)
		{
			return false;
		}
		if (RulesID != other.RulesID)
		{
			return false;
		}
		if (IsMatch != other.IsMatch)
		{
			return false;
		}
		if (MatchDesID != other.MatchDesID)
		{
			return false;
		}
		if (ModeDesID != other.ModeDesID)
		{
			return false;
		}
		if (!object.Equals(BeginTime, other.BeginTime))
		{
			return false;
		}
		if (!object.Equals(EndTime, other.EndTime))
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
		if (MapModeType != MapModeType.None)
		{
			num ^= MapModeType.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (IsShow)
		{
			num ^= IsShow.GetHashCode();
		}
		if (AccountExp != 0)
		{
			num ^= AccountExp.GetHashCode();
		}
		if (GoldFirst != 0)
		{
			num ^= GoldFirst.GetHashCode();
		}
		if (GoldOther != 0)
		{
			num ^= GoldOther.GetHashCode();
		}
		num ^= mapID_.GetHashCode();
		num ^= upgrade_.GetHashCode();
		num ^= victoryParam_.GetHashCode();
		if (CardInHandLimit != 0)
		{
			num ^= CardInHandLimit.GetHashCode();
		}
		if (Battlecost != 0)
		{
			num ^= Battlecost.GetHashCode();
		}
		num ^= distributeResources_.GetHashCode();
		num ^= bountyParams_.GetHashCode();
		num ^= christmasParams_.GetHashCode();
		if (CardNumFix != 0)
		{
			num ^= CardNumFix.GetHashCode();
		}
		if (CoolDownFix != 0)
		{
			num ^= CoolDownFix.GetHashCode();
		}
		num ^= buffs_.GetHashCode();
		num ^= banCharacters_.GetHashCode();
		if (OrderWeight != 0)
		{
			num ^= OrderWeight.GetHashCode();
		}
		if (RulesID != 0)
		{
			num ^= RulesID.GetHashCode();
		}
		if (IsMatch)
		{
			num ^= IsMatch.GetHashCode();
		}
		if (MatchDesID != 0)
		{
			num ^= MatchDesID.GetHashCode();
		}
		if (ModeDesID != 0)
		{
			num ^= ModeDesID.GetHashCode();
		}
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
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
		if (MapModeType != MapModeType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)MapModeType);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NameID);
		}
		if (IsShow)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsShow);
		}
		if (AccountExp != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(AccountExp);
		}
		if (GoldFirst != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(GoldFirst);
		}
		if (GoldOther != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(GoldOther);
		}
		mapID_.WriteTo(ref output, _repeated_mapID_codec);
		upgrade_.WriteTo(ref output, _repeated_upgrade_codec);
		victoryParam_.WriteTo(ref output, _repeated_victoryParam_codec);
		if (CardInHandLimit != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(CardInHandLimit);
		}
		if (Battlecost != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(Battlecost);
		}
		distributeResources_.WriteTo(ref output, _repeated_distributeResources_codec);
		bountyParams_.WriteTo(ref output, _repeated_bountyParams_codec);
		christmasParams_.WriteTo(ref output, _repeated_christmasParams_codec);
		if (CardNumFix != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(CardNumFix);
		}
		if (CoolDownFix != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(CoolDownFix);
		}
		buffs_.WriteTo(ref output, _repeated_buffs_codec);
		banCharacters_.WriteTo(ref output, _repeated_banCharacters_codec);
		if (OrderWeight != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(OrderWeight);
		}
		if (RulesID != 0)
		{
			output.WriteRawTag(165, 1);
			output.WriteSFixed32(RulesID);
		}
		if (IsMatch)
		{
			output.WriteRawTag(168, 1);
			output.WriteBool(IsMatch);
		}
		if (MatchDesID != 0)
		{
			output.WriteRawTag(181, 1);
			output.WriteSFixed32(MatchDesID);
		}
		if (ModeDesID != 0)
		{
			output.WriteRawTag(189, 1);
			output.WriteSFixed32(ModeDesID);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(194, 1);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(202, 1);
			output.WriteMessage(EndTime);
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
		if (MapModeType != MapModeType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MapModeType);
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (IsShow)
		{
			num += 2;
		}
		if (AccountExp != 0)
		{
			num += 5;
		}
		if (GoldFirst != 0)
		{
			num += 5;
		}
		if (GoldOther != 0)
		{
			num += 5;
		}
		num += mapID_.CalculateSize(_repeated_mapID_codec);
		num += upgrade_.CalculateSize(_repeated_upgrade_codec);
		num += victoryParam_.CalculateSize(_repeated_victoryParam_codec);
		if (CardInHandLimit != 0)
		{
			num += 5;
		}
		if (Battlecost != 0)
		{
			num += 5;
		}
		num += distributeResources_.CalculateSize(_repeated_distributeResources_codec);
		num += bountyParams_.CalculateSize(_repeated_bountyParams_codec);
		num += christmasParams_.CalculateSize(_repeated_christmasParams_codec);
		if (CardNumFix != 0)
		{
			num += 5;
		}
		if (CoolDownFix != 0)
		{
			num += 6;
		}
		num += buffs_.CalculateSize(_repeated_buffs_codec);
		num += banCharacters_.CalculateSize(_repeated_banCharacters_codec);
		if (OrderWeight != 0)
		{
			num += 6;
		}
		if (RulesID != 0)
		{
			num += 6;
		}
		if (IsMatch)
		{
			num += 3;
		}
		if (MatchDesID != 0)
		{
			num += 6;
		}
		if (ModeDesID != 0)
		{
			num += 6;
		}
		if (beginTime_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GameModeInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.MapModeType != MapModeType.None)
		{
			MapModeType = other.MapModeType;
		}
		if (other.NameID != 0)
		{
			NameID = other.NameID;
		}
		if (other.IsShow)
		{
			IsShow = other.IsShow;
		}
		if (other.AccountExp != 0)
		{
			AccountExp = other.AccountExp;
		}
		if (other.GoldFirst != 0)
		{
			GoldFirst = other.GoldFirst;
		}
		if (other.GoldOther != 0)
		{
			GoldOther = other.GoldOther;
		}
		mapID_.Add(other.mapID_);
		upgrade_.Add(other.upgrade_);
		victoryParam_.Add(other.victoryParam_);
		if (other.CardInHandLimit != 0)
		{
			CardInHandLimit = other.CardInHandLimit;
		}
		if (other.Battlecost != 0)
		{
			Battlecost = other.Battlecost;
		}
		distributeResources_.Add(other.distributeResources_);
		bountyParams_.Add(other.bountyParams_);
		christmasParams_.Add(other.christmasParams_);
		if (other.CardNumFix != 0)
		{
			CardNumFix = other.CardNumFix;
		}
		if (other.CoolDownFix != 0)
		{
			CoolDownFix = other.CoolDownFix;
		}
		buffs_.Add(other.buffs_);
		banCharacters_.Add(other.banCharacters_);
		if (other.OrderWeight != 0)
		{
			OrderWeight = other.OrderWeight;
		}
		if (other.RulesID != 0)
		{
			RulesID = other.RulesID;
		}
		if (other.IsMatch)
		{
			IsMatch = other.IsMatch;
		}
		if (other.MatchDesID != 0)
		{
			MatchDesID = other.MatchDesID;
		}
		if (other.ModeDesID != 0)
		{
			ModeDesID = other.ModeDesID;
		}
		if (other.beginTime_ != null)
		{
			if (beginTime_ == null)
			{
				BeginTime = new Timestamp();
			}
			BeginTime.MergeFrom(other.BeginTime);
		}
		if (other.endTime_ != null)
		{
			if (endTime_ == null)
			{
				EndTime = new Timestamp();
			}
			EndTime.MergeFrom(other.EndTime);
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
			case 8u:
				MapModeType = (MapModeType)input.ReadEnum();
				break;
			case 21u:
				NameID = input.ReadSFixed32();
				break;
			case 24u:
				IsShow = input.ReadBool();
				break;
			case 37u:
				AccountExp = input.ReadSFixed32();
				break;
			case 45u:
				GoldFirst = input.ReadSFixed32();
				break;
			case 53u:
				GoldOther = input.ReadSFixed32();
				break;
			case 58u:
			case 61u:
				mapID_.AddEntriesFrom(ref input, _repeated_mapID_codec);
				break;
			case 66u:
			case 69u:
				upgrade_.AddEntriesFrom(ref input, _repeated_upgrade_codec);
				break;
			case 74u:
			case 77u:
				victoryParam_.AddEntriesFrom(ref input, _repeated_victoryParam_codec);
				break;
			case 85u:
				CardInHandLimit = input.ReadSFixed32();
				break;
			case 93u:
				Battlecost = input.ReadSFixed32();
				break;
			case 96u:
			case 98u:
				distributeResources_.AddEntriesFrom(ref input, _repeated_distributeResources_codec);
				break;
			case 104u:
			case 106u:
				bountyParams_.AddEntriesFrom(ref input, _repeated_bountyParams_codec);
				break;
			case 112u:
			case 114u:
				christmasParams_.AddEntriesFrom(ref input, _repeated_christmasParams_codec);
				break;
			case 125u:
				CardNumFix = input.ReadSFixed32();
				break;
			case 133u:
				CoolDownFix = input.ReadSFixed32();
				break;
			case 136u:
			case 138u:
				buffs_.AddEntriesFrom(ref input, _repeated_buffs_codec);
				break;
			case 144u:
			case 146u:
				banCharacters_.AddEntriesFrom(ref input, _repeated_banCharacters_codec);
				break;
			case 157u:
				OrderWeight = input.ReadSFixed32();
				break;
			case 165u:
				RulesID = input.ReadSFixed32();
				break;
			case 168u:
				IsMatch = input.ReadBool();
				break;
			case 181u:
				MatchDesID = input.ReadSFixed32();
				break;
			case 189u:
				ModeDesID = input.ReadSFixed32();
				break;
			case 194u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 202u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			}
		}
	}

	public void FixTime(FixGameModeInfoConfigure data)
	{
		beginTime_ = data.BeginTime;
		endTime_ = data.EndTime;
	}

	public GameModeDifficultyDataConfigureItem GetDifficultyData(int mapModeType, int difficulty)
	{
		if (BattleConfig.IsPVE(mapModeType))
		{
			if (!StaticConfigure.GameMode.DifficultyDataDict.TryGetValue(mapModeType, out var value))
			{
				return null;
			}
			return value.GameModeDifficultyDataConfigureItems.GetSafeByIndex(difficulty);
		}
		return null;
	}
}
