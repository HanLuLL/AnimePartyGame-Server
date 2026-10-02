using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using UnityEngine;

public sealed class CampaignLevelConfigure : IMessage<CampaignLevelConfigure>, IMessage, IEquatable<CampaignLevelConfigure>, IDeepCloneable<CampaignLevelConfigure>, IBufferMessage
{
	private static readonly MessageParser<CampaignLevelConfigure> _parser = new MessageParser<CampaignLevelConfigure>(() => new CampaignLevelConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int FormerIDFieldNumber = 2;

	private int formerID_;

	public const int BannerFieldNumber = 3;

	private string banner_ = "";

	public const int SerialNumberFieldNumber = 4;

	private int serialNumber_;

	public const int NameFieldNumber = 5;

	private int name_;

	public const int MapIDFieldNumber = 6;

	private int mapID_;

	public const int MapModeTypeFieldNumber = 7;

	private MapModeType mapModeType_;

	public const int GameDifficultyTypeFieldNumber = 8;

	private GameDifficultyType gameDifficultyType_;

	public const int VictoryTypeFieldNumber = 9;

	private VictoryType victoryType_;

	public const int VictoryParamsFieldNumber = 10;

	private static readonly FieldCodec<int> _repeated_victoryParams_codec = FieldCodec.ForSFixed32(82u);

	private readonly RepeatedField<int> victoryParams_ = new RepeatedField<int>();

	public const int VictoryDescriptionFieldNumber = 11;

	private int victoryDescription_;

	public const int BannerVictoryDescriptionFieldNumber = 12;

	private int bannerVictoryDescription_;

	public const int FirstPassRewardFieldNumber = 13;

	private static readonly MapField<int, int>.Codec _map_firstPassReward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 106u);

	private readonly MapField<int, int> firstPassReward_ = new MapField<int, int>();

	public const int RepeatPassRewardFieldNumber = 14;

	private static readonly MapField<int, int>.Codec _map_repeatPassReward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 114u);

	private readonly MapField<int, int> repeatPassReward_ = new MapField<int, int>();

	public const int BotHeroIdsFieldNumber = 15;

	private static readonly FieldCodec<int> _repeated_botHeroIds_codec = FieldCodec.ForSFixed32(122u);

	private readonly RepeatedField<int> botHeroIds_ = new RepeatedField<int>();

	public const int ProtagonistInitialStarFieldNumber = 16;

	private int protagonistInitialStar_;

	public const int ProtagonistInitialGoldFieldNumber = 17;

	private int protagonistInitialGold_;

	public const int InitialStarFieldNumber = 18;

	private static readonly FieldCodec<int> _repeated_initialStar_codec = FieldCodec.ForSFixed32(146u);

	private readonly RepeatedField<int> initialStar_ = new RepeatedField<int>();

	public const int InitialGoldFieldNumber = 19;

	private static readonly FieldCodec<int> _repeated_initialGold_codec = FieldCodec.ForSFixed32(154u);

	private readonly RepeatedField<int> initialGold_ = new RepeatedField<int>();

	public const int TriggerTipsFieldNumber = 20;

	private static readonly FieldCodec<int> _repeated_triggerTips_codec = FieldCodec.ForSFixed32(162u);

	private readonly RepeatedField<int> triggerTips_ = new RepeatedField<int>();

	public const int ChoosingTimeTypeFieldNumber = 21;

	private ChoosingTimeType choosingTimeType_;

	public const int HeroesLimitedFieldNumber = 22;

	private static readonly FieldCodec<int> _repeated_heroesLimited_codec = FieldCodec.ForSFixed32(178u);

	private readonly RepeatedField<int> heroesLimited_ = new RepeatedField<int>();

	public const int ProtagonistInitialCardFieldNumber = 23;

	private static readonly FieldCodec<int> _repeated_protagonistInitialCard_codec = FieldCodec.ForSFixed32(186u);

	private readonly RepeatedField<int> protagonistInitialCard_ = new RepeatedField<int>();

	public const int InitialCardFieldNumber = 24;

	private static readonly MapField<int, CampaignLevelConfigureInitialCards>.Codec _map_initialCard_codec = new MapField<int, CampaignLevelConfigureInitialCards>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CampaignLevelConfigureInitialCards.Parser), 194u);

	private readonly MapField<int, CampaignLevelConfigureInitialCards> initialCard_ = new MapField<int, CampaignLevelConfigureInitialCards>();

	public const int BuffIdsFieldNumber = 25;

	private static readonly FieldCodec<int> _repeated_buffIds_codec = FieldCodec.ForSFixed32(202u);

	private readonly RepeatedField<int> buffIds_ = new RepeatedField<int>();

	private List<CampaignTriggerConfigure> _CampaignTriggerConfigures;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CampaignLevelConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CampaignReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FormerID
	{
		get
		{
			return formerID_;
		}
		private set
		{
			formerID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Banner
	{
		get
		{
			return banner_;
		}
		private set
		{
			banner_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SerialNumber
	{
		get
		{
			return serialNumber_;
		}
		private set
		{
			serialNumber_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Name
	{
		get
		{
			return name_;
		}
		private set
		{
			name_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapID
	{
		get
		{
			return mapID_;
		}
		private set
		{
			mapID_ = value;
		}
	}

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
	public GameDifficultyType GameDifficultyType
	{
		get
		{
			return gameDifficultyType_;
		}
		private set
		{
			gameDifficultyType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VictoryType VictoryType
	{
		get
		{
			return victoryType_;
		}
		private set
		{
			victoryType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> VictoryParams => victoryParams_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int VictoryDescription
	{
		get
		{
			return victoryDescription_;
		}
		private set
		{
			victoryDescription_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BannerVictoryDescription
	{
		get
		{
			return bannerVictoryDescription_;
		}
		private set
		{
			bannerVictoryDescription_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> FirstPassReward => firstPassReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> RepeatPassReward => repeatPassReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BotHeroIds => botHeroIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ProtagonistInitialStar
	{
		get
		{
			return protagonistInitialStar_;
		}
		private set
		{
			protagonistInitialStar_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ProtagonistInitialGold
	{
		get
		{
			return protagonistInitialGold_;
		}
		private set
		{
			protagonistInitialGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> InitialStar => initialStar_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> InitialGold => initialGold_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TriggerTips => triggerTips_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeType ChoosingTimeType
	{
		get
		{
			return choosingTimeType_;
		}
		private set
		{
			choosingTimeType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> HeroesLimited => heroesLimited_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> ProtagonistInitialCard => protagonistInitialCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CampaignLevelConfigureInitialCards> InitialCard => initialCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BuffIds => buffIds_;

	public List<CampaignTriggerConfigure> CampaignTriggerConfigures
	{
		get
		{
			if (_CampaignTriggerConfigures == null)
			{
				_CampaignTriggerConfigures = new List<CampaignTriggerConfigure>(TriggerTips.Count);
				for (int i = 0; i < TriggerTips.Count; i++)
				{
					if (!StaticConfigure.Campaign.TriggerDict.TryGetValue(TriggerTips[i], out var value))
					{
						Debug.LogError($"无法在Campaign.TriggerDict中找到TriggerTipsId:{TriggerTips[i]}的数据");
						return null;
					}
					_CampaignTriggerConfigures.Add(value);
				}
			}
			return _CampaignTriggerConfigures;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignLevelConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignLevelConfigure(CampaignLevelConfigure other)
		: this()
	{
		id_ = other.id_;
		formerID_ = other.formerID_;
		banner_ = other.banner_;
		serialNumber_ = other.serialNumber_;
		name_ = other.name_;
		mapID_ = other.mapID_;
		mapModeType_ = other.mapModeType_;
		gameDifficultyType_ = other.gameDifficultyType_;
		victoryType_ = other.victoryType_;
		victoryParams_ = other.victoryParams_.Clone();
		victoryDescription_ = other.victoryDescription_;
		bannerVictoryDescription_ = other.bannerVictoryDescription_;
		firstPassReward_ = other.firstPassReward_.Clone();
		repeatPassReward_ = other.repeatPassReward_.Clone();
		botHeroIds_ = other.botHeroIds_.Clone();
		protagonistInitialStar_ = other.protagonistInitialStar_;
		protagonistInitialGold_ = other.protagonistInitialGold_;
		initialStar_ = other.initialStar_.Clone();
		initialGold_ = other.initialGold_.Clone();
		triggerTips_ = other.triggerTips_.Clone();
		choosingTimeType_ = other.choosingTimeType_;
		heroesLimited_ = other.heroesLimited_.Clone();
		protagonistInitialCard_ = other.protagonistInitialCard_.Clone();
		initialCard_ = other.initialCard_.Clone();
		buffIds_ = other.buffIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignLevelConfigure Clone()
	{
		return new CampaignLevelConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CampaignLevelConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CampaignLevelConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (FormerID != other.FormerID)
		{
			return false;
		}
		if (Banner != other.Banner)
		{
			return false;
		}
		if (SerialNumber != other.SerialNumber)
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (MapID != other.MapID)
		{
			return false;
		}
		if (MapModeType != other.MapModeType)
		{
			return false;
		}
		if (GameDifficultyType != other.GameDifficultyType)
		{
			return false;
		}
		if (VictoryType != other.VictoryType)
		{
			return false;
		}
		if (!victoryParams_.Equals(other.victoryParams_))
		{
			return false;
		}
		if (VictoryDescription != other.VictoryDescription)
		{
			return false;
		}
		if (BannerVictoryDescription != other.BannerVictoryDescription)
		{
			return false;
		}
		if (!FirstPassReward.Equals(other.FirstPassReward))
		{
			return false;
		}
		if (!RepeatPassReward.Equals(other.RepeatPassReward))
		{
			return false;
		}
		if (!botHeroIds_.Equals(other.botHeroIds_))
		{
			return false;
		}
		if (ProtagonistInitialStar != other.ProtagonistInitialStar)
		{
			return false;
		}
		if (ProtagonistInitialGold != other.ProtagonistInitialGold)
		{
			return false;
		}
		if (!initialStar_.Equals(other.initialStar_))
		{
			return false;
		}
		if (!initialGold_.Equals(other.initialGold_))
		{
			return false;
		}
		if (!triggerTips_.Equals(other.triggerTips_))
		{
			return false;
		}
		if (ChoosingTimeType != other.ChoosingTimeType)
		{
			return false;
		}
		if (!heroesLimited_.Equals(other.heroesLimited_))
		{
			return false;
		}
		if (!protagonistInitialCard_.Equals(other.protagonistInitialCard_))
		{
			return false;
		}
		if (!InitialCard.Equals(other.InitialCard))
		{
			return false;
		}
		if (!buffIds_.Equals(other.buffIds_))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (FormerID != 0)
		{
			num ^= FormerID.GetHashCode();
		}
		if (Banner.Length != 0)
		{
			num ^= Banner.GetHashCode();
		}
		if (SerialNumber != 0)
		{
			num ^= SerialNumber.GetHashCode();
		}
		if (Name != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (MapID != 0)
		{
			num ^= MapID.GetHashCode();
		}
		if (MapModeType != MapModeType.None)
		{
			num ^= MapModeType.GetHashCode();
		}
		if (GameDifficultyType != GameDifficultyType.Easy)
		{
			num ^= GameDifficultyType.GetHashCode();
		}
		if (VictoryType != VictoryType.None)
		{
			num ^= VictoryType.GetHashCode();
		}
		num ^= victoryParams_.GetHashCode();
		if (VictoryDescription != 0)
		{
			num ^= VictoryDescription.GetHashCode();
		}
		if (BannerVictoryDescription != 0)
		{
			num ^= BannerVictoryDescription.GetHashCode();
		}
		num ^= FirstPassReward.GetHashCode();
		num ^= RepeatPassReward.GetHashCode();
		num ^= botHeroIds_.GetHashCode();
		if (ProtagonistInitialStar != 0)
		{
			num ^= ProtagonistInitialStar.GetHashCode();
		}
		if (ProtagonistInitialGold != 0)
		{
			num ^= ProtagonistInitialGold.GetHashCode();
		}
		num ^= initialStar_.GetHashCode();
		num ^= initialGold_.GetHashCode();
		num ^= triggerTips_.GetHashCode();
		if (ChoosingTimeType != ChoosingTimeType.None)
		{
			num ^= ChoosingTimeType.GetHashCode();
		}
		num ^= heroesLimited_.GetHashCode();
		num ^= protagonistInitialCard_.GetHashCode();
		num ^= InitialCard.GetHashCode();
		num ^= buffIds_.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (FormerID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(FormerID);
		}
		if (Banner.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Banner);
		}
		if (SerialNumber != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(SerialNumber);
		}
		if (Name != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Name);
		}
		if (MapID != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(MapID);
		}
		if (MapModeType != MapModeType.None)
		{
			output.WriteRawTag(56);
			output.WriteEnum((int)MapModeType);
		}
		if (GameDifficultyType != GameDifficultyType.Easy)
		{
			output.WriteRawTag(64);
			output.WriteEnum((int)GameDifficultyType);
		}
		if (VictoryType != VictoryType.None)
		{
			output.WriteRawTag(72);
			output.WriteEnum((int)VictoryType);
		}
		victoryParams_.WriteTo(ref output, _repeated_victoryParams_codec);
		if (VictoryDescription != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(VictoryDescription);
		}
		if (BannerVictoryDescription != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(BannerVictoryDescription);
		}
		firstPassReward_.WriteTo(ref output, _map_firstPassReward_codec);
		repeatPassReward_.WriteTo(ref output, _map_repeatPassReward_codec);
		botHeroIds_.WriteTo(ref output, _repeated_botHeroIds_codec);
		if (ProtagonistInitialStar != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(ProtagonistInitialStar);
		}
		if (ProtagonistInitialGold != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(ProtagonistInitialGold);
		}
		initialStar_.WriteTo(ref output, _repeated_initialStar_codec);
		initialGold_.WriteTo(ref output, _repeated_initialGold_codec);
		triggerTips_.WriteTo(ref output, _repeated_triggerTips_codec);
		if (ChoosingTimeType != ChoosingTimeType.None)
		{
			output.WriteRawTag(168, 1);
			output.WriteEnum((int)ChoosingTimeType);
		}
		heroesLimited_.WriteTo(ref output, _repeated_heroesLimited_codec);
		protagonistInitialCard_.WriteTo(ref output, _repeated_protagonistInitialCard_codec);
		initialCard_.WriteTo(ref output, _map_initialCard_codec);
		buffIds_.WriteTo(ref output, _repeated_buffIds_codec);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (FormerID != 0)
		{
			num += 5;
		}
		if (Banner.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Banner);
		}
		if (SerialNumber != 0)
		{
			num += 5;
		}
		if (Name != 0)
		{
			num += 5;
		}
		if (MapID != 0)
		{
			num += 5;
		}
		if (MapModeType != MapModeType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MapModeType);
		}
		if (GameDifficultyType != GameDifficultyType.Easy)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GameDifficultyType);
		}
		if (VictoryType != VictoryType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)VictoryType);
		}
		num += victoryParams_.CalculateSize(_repeated_victoryParams_codec);
		if (VictoryDescription != 0)
		{
			num += 5;
		}
		if (BannerVictoryDescription != 0)
		{
			num += 5;
		}
		num += firstPassReward_.CalculateSize(_map_firstPassReward_codec);
		num += repeatPassReward_.CalculateSize(_map_repeatPassReward_codec);
		num += botHeroIds_.CalculateSize(_repeated_botHeroIds_codec);
		if (ProtagonistInitialStar != 0)
		{
			num += 6;
		}
		if (ProtagonistInitialGold != 0)
		{
			num += 6;
		}
		num += initialStar_.CalculateSize(_repeated_initialStar_codec);
		num += initialGold_.CalculateSize(_repeated_initialGold_codec);
		num += triggerTips_.CalculateSize(_repeated_triggerTips_codec);
		if (ChoosingTimeType != ChoosingTimeType.None)
		{
			num += 2 + CodedOutputStream.ComputeEnumSize((int)ChoosingTimeType);
		}
		num += heroesLimited_.CalculateSize(_repeated_heroesLimited_codec);
		num += protagonistInitialCard_.CalculateSize(_repeated_protagonistInitialCard_codec);
		num += initialCard_.CalculateSize(_map_initialCard_codec);
		num += buffIds_.CalculateSize(_repeated_buffIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CampaignLevelConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.FormerID != 0)
			{
				FormerID = other.FormerID;
			}
			if (other.Banner.Length != 0)
			{
				Banner = other.Banner;
			}
			if (other.SerialNumber != 0)
			{
				SerialNumber = other.SerialNumber;
			}
			if (other.Name != 0)
			{
				Name = other.Name;
			}
			if (other.MapID != 0)
			{
				MapID = other.MapID;
			}
			if (other.MapModeType != MapModeType.None)
			{
				MapModeType = other.MapModeType;
			}
			if (other.GameDifficultyType != GameDifficultyType.Easy)
			{
				GameDifficultyType = other.GameDifficultyType;
			}
			if (other.VictoryType != VictoryType.None)
			{
				VictoryType = other.VictoryType;
			}
			victoryParams_.Add(other.victoryParams_);
			if (other.VictoryDescription != 0)
			{
				VictoryDescription = other.VictoryDescription;
			}
			if (other.BannerVictoryDescription != 0)
			{
				BannerVictoryDescription = other.BannerVictoryDescription;
			}
			firstPassReward_.MergeFrom(other.firstPassReward_);
			repeatPassReward_.MergeFrom(other.repeatPassReward_);
			botHeroIds_.Add(other.botHeroIds_);
			if (other.ProtagonistInitialStar != 0)
			{
				ProtagonistInitialStar = other.ProtagonistInitialStar;
			}
			if (other.ProtagonistInitialGold != 0)
			{
				ProtagonistInitialGold = other.ProtagonistInitialGold;
			}
			initialStar_.Add(other.initialStar_);
			initialGold_.Add(other.initialGold_);
			triggerTips_.Add(other.triggerTips_);
			if (other.ChoosingTimeType != ChoosingTimeType.None)
			{
				ChoosingTimeType = other.ChoosingTimeType;
			}
			heroesLimited_.Add(other.heroesLimited_);
			protagonistInitialCard_.Add(other.protagonistInitialCard_);
			initialCard_.MergeFrom(other.initialCard_);
			buffIds_.Add(other.buffIds_);
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
				Id = input.ReadSFixed32();
				break;
			case 21u:
				FormerID = input.ReadSFixed32();
				break;
			case 26u:
				Banner = input.ReadString();
				break;
			case 37u:
				SerialNumber = input.ReadSFixed32();
				break;
			case 45u:
				Name = input.ReadSFixed32();
				break;
			case 53u:
				MapID = input.ReadSFixed32();
				break;
			case 56u:
				MapModeType = (MapModeType)input.ReadEnum();
				break;
			case 64u:
				GameDifficultyType = (GameDifficultyType)input.ReadEnum();
				break;
			case 72u:
				VictoryType = (VictoryType)input.ReadEnum();
				break;
			case 82u:
			case 85u:
				victoryParams_.AddEntriesFrom(ref input, _repeated_victoryParams_codec);
				break;
			case 93u:
				VictoryDescription = input.ReadSFixed32();
				break;
			case 101u:
				BannerVictoryDescription = input.ReadSFixed32();
				break;
			case 106u:
				firstPassReward_.AddEntriesFrom(ref input, _map_firstPassReward_codec);
				break;
			case 114u:
				repeatPassReward_.AddEntriesFrom(ref input, _map_repeatPassReward_codec);
				break;
			case 122u:
			case 125u:
				botHeroIds_.AddEntriesFrom(ref input, _repeated_botHeroIds_codec);
				break;
			case 133u:
				ProtagonistInitialStar = input.ReadSFixed32();
				break;
			case 141u:
				ProtagonistInitialGold = input.ReadSFixed32();
				break;
			case 146u:
			case 149u:
				initialStar_.AddEntriesFrom(ref input, _repeated_initialStar_codec);
				break;
			case 154u:
			case 157u:
				initialGold_.AddEntriesFrom(ref input, _repeated_initialGold_codec);
				break;
			case 162u:
			case 165u:
				triggerTips_.AddEntriesFrom(ref input, _repeated_triggerTips_codec);
				break;
			case 168u:
				ChoosingTimeType = (ChoosingTimeType)input.ReadEnum();
				break;
			case 178u:
			case 181u:
				heroesLimited_.AddEntriesFrom(ref input, _repeated_heroesLimited_codec);
				break;
			case 186u:
			case 189u:
				protagonistInitialCard_.AddEntriesFrom(ref input, _repeated_protagonistInitialCard_codec);
				break;
			case 194u:
				initialCard_.AddEntriesFrom(ref input, _map_initialCard_codec);
				break;
			case 202u:
			case 205u:
				buffIds_.AddEntriesFrom(ref input, _repeated_buffIds_codec);
				break;
			}
		}
	}
}
