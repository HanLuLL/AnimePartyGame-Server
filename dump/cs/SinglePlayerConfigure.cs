using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerConfigure : IMessage<SinglePlayerConfigure>, IMessage, IEquatable<SinglePlayerConfigure>, IDeepCloneable<SinglePlayerConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerConfigure> _parser = new MessageParser<SinglePlayerConfigure>(() => new SinglePlayerConfigure());

	private UnknownFieldSet _unknownFields;

	public const int UpgradesFieldNumber = 1;

	private static readonly FieldCodec<SinglePlayerUpgradeConfigure> _repeated_upgrades_codec = FieldCodec.ForMessage(10u, SinglePlayerUpgradeConfigure.Parser);

	private readonly RepeatedField<SinglePlayerUpgradeConfigure> upgrades_ = new RepeatedField<SinglePlayerUpgradeConfigure>();

	public const int UpgradeDictFieldNumber = 2;

	private static readonly MapField<int, SinglePlayerUpgradeConfigure>.Codec _map_upgradeDict_codec = new MapField<int, SinglePlayerUpgradeConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerUpgradeConfigure.Parser), 18u);

	private readonly MapField<int, SinglePlayerUpgradeConfigure> upgradeDict_ = new MapField<int, SinglePlayerUpgradeConfigure>();

	public const int CardUpgradesFieldNumber = 3;

	private static readonly FieldCodec<SinglePlayerCardUpgradeConfigure> _repeated_cardUpgrades_codec = FieldCodec.ForMessage(26u, SinglePlayerCardUpgradeConfigure.Parser);

	private readonly RepeatedField<SinglePlayerCardUpgradeConfigure> cardUpgrades_ = new RepeatedField<SinglePlayerCardUpgradeConfigure>();

	public const int CardUpgradeDictFieldNumber = 4;

	private static readonly MapField<int, SinglePlayerCardUpgradeConfigure>.Codec _map_cardUpgradeDict_codec = new MapField<int, SinglePlayerCardUpgradeConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerCardUpgradeConfigure.Parser), 34u);

	private readonly MapField<int, SinglePlayerCardUpgradeConfigure> cardUpgradeDict_ = new MapField<int, SinglePlayerCardUpgradeConfigure>();

	public const int ParamsFieldNumber = 5;

	private static readonly FieldCodec<SinglePlayerParamConfigure> _repeated_params_codec = FieldCodec.ForMessage(42u, SinglePlayerParamConfigure.Parser);

	private readonly RepeatedField<SinglePlayerParamConfigure> params_ = new RepeatedField<SinglePlayerParamConfigure>();

	public const int ParamDictFieldNumber = 6;

	private static readonly MapField<int, SinglePlayerParamConfigure>.Codec _map_paramDict_codec = new MapField<int, SinglePlayerParamConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerParamConfigure.Parser), 50u);

	private readonly MapField<int, SinglePlayerParamConfigure> paramDict_ = new MapField<int, SinglePlayerParamConfigure>();

	public const int RelicsFieldNumber = 7;

	private static readonly FieldCodec<SinglePlayerRelicConfigure> _repeated_relics_codec = FieldCodec.ForMessage(58u, SinglePlayerRelicConfigure.Parser);

	private readonly RepeatedField<SinglePlayerRelicConfigure> relics_ = new RepeatedField<SinglePlayerRelicConfigure>();

	public const int RelicDictFieldNumber = 8;

	private static readonly MapField<int, SinglePlayerRelicConfigure>.Codec _map_relicDict_codec = new MapField<int, SinglePlayerRelicConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerRelicConfigure.Parser), 66u);

	private readonly MapField<int, SinglePlayerRelicConfigure> relicDict_ = new MapField<int, SinglePlayerRelicConfigure>();

	public const int MonstersFieldNumber = 9;

	private static readonly FieldCodec<SinglePlayerMonsterConfigure> _repeated_monsters_codec = FieldCodec.ForMessage(74u, SinglePlayerMonsterConfigure.Parser);

	private readonly RepeatedField<SinglePlayerMonsterConfigure> monsters_ = new RepeatedField<SinglePlayerMonsterConfigure>();

	public const int MonsterDictFieldNumber = 10;

	private static readonly MapField<int, SinglePlayerMonsterConfigure>.Codec _map_monsterDict_codec = new MapField<int, SinglePlayerMonsterConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerMonsterConfigure.Parser), 82u);

	private readonly MapField<int, SinglePlayerMonsterConfigure> monsterDict_ = new MapField<int, SinglePlayerMonsterConfigure>();

	public const int LevelsFieldNumber = 11;

	private static readonly FieldCodec<SinglePlayerLevelConfigure> _repeated_levels_codec = FieldCodec.ForMessage(90u, SinglePlayerLevelConfigure.Parser);

	private readonly RepeatedField<SinglePlayerLevelConfigure> levels_ = new RepeatedField<SinglePlayerLevelConfigure>();

	public const int LevelDictFieldNumber = 12;

	private static readonly MapField<int, SinglePlayerLevelConfigure>.Codec _map_levelDict_codec = new MapField<int, SinglePlayerLevelConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerLevelConfigure.Parser), 98u);

	private readonly MapField<int, SinglePlayerLevelConfigure> levelDict_ = new MapField<int, SinglePlayerLevelConfigure>();

	public const int ChaptersFieldNumber = 13;

	private static readonly FieldCodec<SinglePlayerChapterConfigure> _repeated_chapters_codec = FieldCodec.ForMessage(106u, SinglePlayerChapterConfigure.Parser);

	private readonly RepeatedField<SinglePlayerChapterConfigure> chapters_ = new RepeatedField<SinglePlayerChapterConfigure>();

	public const int ChapterDictFieldNumber = 14;

	private static readonly MapField<int, SinglePlayerChapterConfigure>.Codec _map_chapterDict_codec = new MapField<int, SinglePlayerChapterConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerChapterConfigure.Parser), 114u);

	private readonly MapField<int, SinglePlayerChapterConfigure> chapterDict_ = new MapField<int, SinglePlayerChapterConfigure>();

	public const int LevelProgresssFieldNumber = 15;

	private static readonly FieldCodec<SinglePlayerLevelProgressConfigure> _repeated_levelProgresss_codec = FieldCodec.ForMessage(122u, SinglePlayerLevelProgressConfigure.Parser);

	private readonly RepeatedField<SinglePlayerLevelProgressConfigure> levelProgresss_ = new RepeatedField<SinglePlayerLevelProgressConfigure>();

	public const int LevelProgressDictFieldNumber = 16;

	private static readonly MapField<int, SinglePlayerLevelProgressConfigure>.Codec _map_levelProgressDict_codec = new MapField<int, SinglePlayerLevelProgressConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerLevelProgressConfigure.Parser), 130u);

	private readonly MapField<int, SinglePlayerLevelProgressConfigure> levelProgressDict_ = new MapField<int, SinglePlayerLevelProgressConfigure>();

	public const int CardsFieldNumber = 17;

	private static readonly FieldCodec<SinglePlayerCardConfigure> _repeated_cards_codec = FieldCodec.ForMessage(138u, SinglePlayerCardConfigure.Parser);

	private readonly RepeatedField<SinglePlayerCardConfigure> cards_ = new RepeatedField<SinglePlayerCardConfigure>();

	public const int CardDictFieldNumber = 18;

	private static readonly MapField<int, SinglePlayerCardConfigure>.Codec _map_cardDict_codec = new MapField<int, SinglePlayerCardConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerCardConfigure.Parser), 146u);

	private readonly MapField<int, SinglePlayerCardConfigure> cardDict_ = new MapField<int, SinglePlayerCardConfigure>();

	public const int CardPacksFieldNumber = 19;

	private static readonly FieldCodec<SinglePlayerCardPackConfigure> _repeated_cardPacks_codec = FieldCodec.ForMessage(154u, SinglePlayerCardPackConfigure.Parser);

	private readonly RepeatedField<SinglePlayerCardPackConfigure> cardPacks_ = new RepeatedField<SinglePlayerCardPackConfigure>();

	public const int CardPackDictFieldNumber = 20;

	private static readonly MapField<int, SinglePlayerCardPackConfigure>.Codec _map_cardPackDict_codec = new MapField<int, SinglePlayerCardPackConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerCardPackConfigure.Parser), 162u);

	private readonly MapField<int, SinglePlayerCardPackConfigure> cardPackDict_ = new MapField<int, SinglePlayerCardPackConfigure>();

	public const int BulletsFieldNumber = 21;

	private static readonly FieldCodec<SinglePlayerBulletConfigure> _repeated_bullets_codec = FieldCodec.ForMessage(170u, SinglePlayerBulletConfigure.Parser);

	private readonly RepeatedField<SinglePlayerBulletConfigure> bullets_ = new RepeatedField<SinglePlayerBulletConfigure>();

	public const int BulletDictFieldNumber = 22;

	private static readonly MapField<int, SinglePlayerBulletConfigure>.Codec _map_bulletDict_codec = new MapField<int, SinglePlayerBulletConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerBulletConfigure.Parser), 178u);

	private readonly MapField<int, SinglePlayerBulletConfigure> bulletDict_ = new MapField<int, SinglePlayerBulletConfigure>();

	public const int PerformsFieldNumber = 23;

	private static readonly FieldCodec<SinglePlayerPerformConfigure> _repeated_performs_codec = FieldCodec.ForMessage(186u, SinglePlayerPerformConfigure.Parser);

	private readonly RepeatedField<SinglePlayerPerformConfigure> performs_ = new RepeatedField<SinglePlayerPerformConfigure>();

	public const int PerformDictFieldNumber = 24;

	private static readonly MapField<int, SinglePlayerPerformConfigure>.Codec _map_performDict_codec = new MapField<int, SinglePlayerPerformConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerPerformConfigure.Parser), 194u);

	private readonly MapField<int, SinglePlayerPerformConfigure> performDict_ = new MapField<int, SinglePlayerPerformConfigure>();

	public const int LandsFieldNumber = 25;

	private static readonly FieldCodec<SinglePlayerLandConfigure> _repeated_lands_codec = FieldCodec.ForMessage(202u, SinglePlayerLandConfigure.Parser);

	private readonly RepeatedField<SinglePlayerLandConfigure> lands_ = new RepeatedField<SinglePlayerLandConfigure>();

	public const int LandDictFieldNumber = 26;

	private static readonly MapField<int, SinglePlayerLandConfigure>.Codec _map_landDict_codec = new MapField<int, SinglePlayerLandConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerLandConfigure.Parser), 210u);

	private readonly MapField<int, SinglePlayerLandConfigure> landDict_ = new MapField<int, SinglePlayerLandConfigure>();

	public const int CardTagsFieldNumber = 27;

	private static readonly FieldCodec<SinglePlayerCardTagConfigure> _repeated_cardTags_codec = FieldCodec.ForMessage(218u, SinglePlayerCardTagConfigure.Parser);

	private readonly RepeatedField<SinglePlayerCardTagConfigure> cardTags_ = new RepeatedField<SinglePlayerCardTagConfigure>();

	public const int CardTagDictFieldNumber = 28;

	private static readonly MapField<int, SinglePlayerCardTagConfigure>.Codec _map_cardTagDict_codec = new MapField<int, SinglePlayerCardTagConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SinglePlayerCardTagConfigure.Parser), 226u);

	private readonly MapField<int, SinglePlayerCardTagConfigure> cardTagDict_ = new MapField<int, SinglePlayerCardTagConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[18];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerUpgradeConfigure> Upgrades => upgrades_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerUpgradeConfigure> UpgradeDict => upgradeDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerCardUpgradeConfigure> CardUpgrades => cardUpgrades_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerCardUpgradeConfigure> CardUpgradeDict => cardUpgradeDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerParamConfigure> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerParamConfigure> ParamDict => paramDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerRelicConfigure> Relics => relics_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerRelicConfigure> RelicDict => relicDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerMonsterConfigure> Monsters => monsters_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerMonsterConfigure> MonsterDict => monsterDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerLevelConfigure> Levels => levels_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerLevelConfigure> LevelDict => levelDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerChapterConfigure> Chapters => chapters_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerChapterConfigure> ChapterDict => chapterDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerLevelProgressConfigure> LevelProgresss => levelProgresss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerLevelProgressConfigure> LevelProgressDict => levelProgressDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerCardConfigure> Cards => cards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerCardConfigure> CardDict => cardDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerCardPackConfigure> CardPacks => cardPacks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerCardPackConfigure> CardPackDict => cardPackDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerBulletConfigure> Bullets => bullets_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerBulletConfigure> BulletDict => bulletDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerPerformConfigure> Performs => performs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerPerformConfigure> PerformDict => performDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerLandConfigure> Lands => lands_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerLandConfigure> LandDict => landDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerCardTagConfigure> CardTags => cardTags_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SinglePlayerCardTagConfigure> CardTagDict => cardTagDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerConfigure(SinglePlayerConfigure other)
		: this()
	{
		upgrades_ = other.upgrades_.Clone();
		upgradeDict_ = other.upgradeDict_.Clone();
		cardUpgrades_ = other.cardUpgrades_.Clone();
		cardUpgradeDict_ = other.cardUpgradeDict_.Clone();
		params_ = other.params_.Clone();
		paramDict_ = other.paramDict_.Clone();
		relics_ = other.relics_.Clone();
		relicDict_ = other.relicDict_.Clone();
		monsters_ = other.monsters_.Clone();
		monsterDict_ = other.monsterDict_.Clone();
		levels_ = other.levels_.Clone();
		levelDict_ = other.levelDict_.Clone();
		chapters_ = other.chapters_.Clone();
		chapterDict_ = other.chapterDict_.Clone();
		levelProgresss_ = other.levelProgresss_.Clone();
		levelProgressDict_ = other.levelProgressDict_.Clone();
		cards_ = other.cards_.Clone();
		cardDict_ = other.cardDict_.Clone();
		cardPacks_ = other.cardPacks_.Clone();
		cardPackDict_ = other.cardPackDict_.Clone();
		bullets_ = other.bullets_.Clone();
		bulletDict_ = other.bulletDict_.Clone();
		performs_ = other.performs_.Clone();
		performDict_ = other.performDict_.Clone();
		lands_ = other.lands_.Clone();
		landDict_ = other.landDict_.Clone();
		cardTags_ = other.cardTags_.Clone();
		cardTagDict_ = other.cardTagDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerConfigure Clone()
	{
		return new SinglePlayerConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!upgrades_.Equals(other.upgrades_))
		{
			return false;
		}
		if (!UpgradeDict.Equals(other.UpgradeDict))
		{
			return false;
		}
		if (!cardUpgrades_.Equals(other.cardUpgrades_))
		{
			return false;
		}
		if (!CardUpgradeDict.Equals(other.CardUpgradeDict))
		{
			return false;
		}
		if (!params_.Equals(other.params_))
		{
			return false;
		}
		if (!ParamDict.Equals(other.ParamDict))
		{
			return false;
		}
		if (!relics_.Equals(other.relics_))
		{
			return false;
		}
		if (!RelicDict.Equals(other.RelicDict))
		{
			return false;
		}
		if (!monsters_.Equals(other.monsters_))
		{
			return false;
		}
		if (!MonsterDict.Equals(other.MonsterDict))
		{
			return false;
		}
		if (!levels_.Equals(other.levels_))
		{
			return false;
		}
		if (!LevelDict.Equals(other.LevelDict))
		{
			return false;
		}
		if (!chapters_.Equals(other.chapters_))
		{
			return false;
		}
		if (!ChapterDict.Equals(other.ChapterDict))
		{
			return false;
		}
		if (!levelProgresss_.Equals(other.levelProgresss_))
		{
			return false;
		}
		if (!LevelProgressDict.Equals(other.LevelProgressDict))
		{
			return false;
		}
		if (!cards_.Equals(other.cards_))
		{
			return false;
		}
		if (!CardDict.Equals(other.CardDict))
		{
			return false;
		}
		if (!cardPacks_.Equals(other.cardPacks_))
		{
			return false;
		}
		if (!CardPackDict.Equals(other.CardPackDict))
		{
			return false;
		}
		if (!bullets_.Equals(other.bullets_))
		{
			return false;
		}
		if (!BulletDict.Equals(other.BulletDict))
		{
			return false;
		}
		if (!performs_.Equals(other.performs_))
		{
			return false;
		}
		if (!PerformDict.Equals(other.PerformDict))
		{
			return false;
		}
		if (!lands_.Equals(other.lands_))
		{
			return false;
		}
		if (!LandDict.Equals(other.LandDict))
		{
			return false;
		}
		if (!cardTags_.Equals(other.cardTags_))
		{
			return false;
		}
		if (!CardTagDict.Equals(other.CardTagDict))
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
		num ^= upgrades_.GetHashCode();
		num ^= UpgradeDict.GetHashCode();
		num ^= cardUpgrades_.GetHashCode();
		num ^= CardUpgradeDict.GetHashCode();
		num ^= params_.GetHashCode();
		num ^= ParamDict.GetHashCode();
		num ^= relics_.GetHashCode();
		num ^= RelicDict.GetHashCode();
		num ^= monsters_.GetHashCode();
		num ^= MonsterDict.GetHashCode();
		num ^= levels_.GetHashCode();
		num ^= LevelDict.GetHashCode();
		num ^= chapters_.GetHashCode();
		num ^= ChapterDict.GetHashCode();
		num ^= levelProgresss_.GetHashCode();
		num ^= LevelProgressDict.GetHashCode();
		num ^= cards_.GetHashCode();
		num ^= CardDict.GetHashCode();
		num ^= cardPacks_.GetHashCode();
		num ^= CardPackDict.GetHashCode();
		num ^= bullets_.GetHashCode();
		num ^= BulletDict.GetHashCode();
		num ^= performs_.GetHashCode();
		num ^= PerformDict.GetHashCode();
		num ^= lands_.GetHashCode();
		num ^= LandDict.GetHashCode();
		num ^= cardTags_.GetHashCode();
		num ^= CardTagDict.GetHashCode();
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
		upgrades_.WriteTo(ref output, _repeated_upgrades_codec);
		upgradeDict_.WriteTo(ref output, _map_upgradeDict_codec);
		cardUpgrades_.WriteTo(ref output, _repeated_cardUpgrades_codec);
		cardUpgradeDict_.WriteTo(ref output, _map_cardUpgradeDict_codec);
		params_.WriteTo(ref output, _repeated_params_codec);
		paramDict_.WriteTo(ref output, _map_paramDict_codec);
		relics_.WriteTo(ref output, _repeated_relics_codec);
		relicDict_.WriteTo(ref output, _map_relicDict_codec);
		monsters_.WriteTo(ref output, _repeated_monsters_codec);
		monsterDict_.WriteTo(ref output, _map_monsterDict_codec);
		levels_.WriteTo(ref output, _repeated_levels_codec);
		levelDict_.WriteTo(ref output, _map_levelDict_codec);
		chapters_.WriteTo(ref output, _repeated_chapters_codec);
		chapterDict_.WriteTo(ref output, _map_chapterDict_codec);
		levelProgresss_.WriteTo(ref output, _repeated_levelProgresss_codec);
		levelProgressDict_.WriteTo(ref output, _map_levelProgressDict_codec);
		cards_.WriteTo(ref output, _repeated_cards_codec);
		cardDict_.WriteTo(ref output, _map_cardDict_codec);
		cardPacks_.WriteTo(ref output, _repeated_cardPacks_codec);
		cardPackDict_.WriteTo(ref output, _map_cardPackDict_codec);
		bullets_.WriteTo(ref output, _repeated_bullets_codec);
		bulletDict_.WriteTo(ref output, _map_bulletDict_codec);
		performs_.WriteTo(ref output, _repeated_performs_codec);
		performDict_.WriteTo(ref output, _map_performDict_codec);
		lands_.WriteTo(ref output, _repeated_lands_codec);
		landDict_.WriteTo(ref output, _map_landDict_codec);
		cardTags_.WriteTo(ref output, _repeated_cardTags_codec);
		cardTagDict_.WriteTo(ref output, _map_cardTagDict_codec);
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
		num += upgrades_.CalculateSize(_repeated_upgrades_codec);
		num += upgradeDict_.CalculateSize(_map_upgradeDict_codec);
		num += cardUpgrades_.CalculateSize(_repeated_cardUpgrades_codec);
		num += cardUpgradeDict_.CalculateSize(_map_cardUpgradeDict_codec);
		num += params_.CalculateSize(_repeated_params_codec);
		num += paramDict_.CalculateSize(_map_paramDict_codec);
		num += relics_.CalculateSize(_repeated_relics_codec);
		num += relicDict_.CalculateSize(_map_relicDict_codec);
		num += monsters_.CalculateSize(_repeated_monsters_codec);
		num += monsterDict_.CalculateSize(_map_monsterDict_codec);
		num += levels_.CalculateSize(_repeated_levels_codec);
		num += levelDict_.CalculateSize(_map_levelDict_codec);
		num += chapters_.CalculateSize(_repeated_chapters_codec);
		num += chapterDict_.CalculateSize(_map_chapterDict_codec);
		num += levelProgresss_.CalculateSize(_repeated_levelProgresss_codec);
		num += levelProgressDict_.CalculateSize(_map_levelProgressDict_codec);
		num += cards_.CalculateSize(_repeated_cards_codec);
		num += cardDict_.CalculateSize(_map_cardDict_codec);
		num += cardPacks_.CalculateSize(_repeated_cardPacks_codec);
		num += cardPackDict_.CalculateSize(_map_cardPackDict_codec);
		num += bullets_.CalculateSize(_repeated_bullets_codec);
		num += bulletDict_.CalculateSize(_map_bulletDict_codec);
		num += performs_.CalculateSize(_repeated_performs_codec);
		num += performDict_.CalculateSize(_map_performDict_codec);
		num += lands_.CalculateSize(_repeated_lands_codec);
		num += landDict_.CalculateSize(_map_landDict_codec);
		num += cardTags_.CalculateSize(_repeated_cardTags_codec);
		num += cardTagDict_.CalculateSize(_map_cardTagDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerConfigure other)
	{
		if (other != null)
		{
			upgrades_.Add(other.upgrades_);
			upgradeDict_.MergeFrom(other.upgradeDict_);
			cardUpgrades_.Add(other.cardUpgrades_);
			cardUpgradeDict_.MergeFrom(other.cardUpgradeDict_);
			params_.Add(other.params_);
			paramDict_.MergeFrom(other.paramDict_);
			relics_.Add(other.relics_);
			relicDict_.MergeFrom(other.relicDict_);
			monsters_.Add(other.monsters_);
			monsterDict_.MergeFrom(other.monsterDict_);
			levels_.Add(other.levels_);
			levelDict_.MergeFrom(other.levelDict_);
			chapters_.Add(other.chapters_);
			chapterDict_.MergeFrom(other.chapterDict_);
			levelProgresss_.Add(other.levelProgresss_);
			levelProgressDict_.MergeFrom(other.levelProgressDict_);
			cards_.Add(other.cards_);
			cardDict_.MergeFrom(other.cardDict_);
			cardPacks_.Add(other.cardPacks_);
			cardPackDict_.MergeFrom(other.cardPackDict_);
			bullets_.Add(other.bullets_);
			bulletDict_.MergeFrom(other.bulletDict_);
			performs_.Add(other.performs_);
			performDict_.MergeFrom(other.performDict_);
			lands_.Add(other.lands_);
			landDict_.MergeFrom(other.landDict_);
			cardTags_.Add(other.cardTags_);
			cardTagDict_.MergeFrom(other.cardTagDict_);
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
				upgrades_.AddEntriesFrom(ref input, _repeated_upgrades_codec);
				break;
			case 18u:
				upgradeDict_.AddEntriesFrom(ref input, _map_upgradeDict_codec);
				break;
			case 26u:
				cardUpgrades_.AddEntriesFrom(ref input, _repeated_cardUpgrades_codec);
				break;
			case 34u:
				cardUpgradeDict_.AddEntriesFrom(ref input, _map_cardUpgradeDict_codec);
				break;
			case 42u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 50u:
				paramDict_.AddEntriesFrom(ref input, _map_paramDict_codec);
				break;
			case 58u:
				relics_.AddEntriesFrom(ref input, _repeated_relics_codec);
				break;
			case 66u:
				relicDict_.AddEntriesFrom(ref input, _map_relicDict_codec);
				break;
			case 74u:
				monsters_.AddEntriesFrom(ref input, _repeated_monsters_codec);
				break;
			case 82u:
				monsterDict_.AddEntriesFrom(ref input, _map_monsterDict_codec);
				break;
			case 90u:
				levels_.AddEntriesFrom(ref input, _repeated_levels_codec);
				break;
			case 98u:
				levelDict_.AddEntriesFrom(ref input, _map_levelDict_codec);
				break;
			case 106u:
				chapters_.AddEntriesFrom(ref input, _repeated_chapters_codec);
				break;
			case 114u:
				chapterDict_.AddEntriesFrom(ref input, _map_chapterDict_codec);
				break;
			case 122u:
				levelProgresss_.AddEntriesFrom(ref input, _repeated_levelProgresss_codec);
				break;
			case 130u:
				levelProgressDict_.AddEntriesFrom(ref input, _map_levelProgressDict_codec);
				break;
			case 138u:
				cards_.AddEntriesFrom(ref input, _repeated_cards_codec);
				break;
			case 146u:
				cardDict_.AddEntriesFrom(ref input, _map_cardDict_codec);
				break;
			case 154u:
				cardPacks_.AddEntriesFrom(ref input, _repeated_cardPacks_codec);
				break;
			case 162u:
				cardPackDict_.AddEntriesFrom(ref input, _map_cardPackDict_codec);
				break;
			case 170u:
				bullets_.AddEntriesFrom(ref input, _repeated_bullets_codec);
				break;
			case 178u:
				bulletDict_.AddEntriesFrom(ref input, _map_bulletDict_codec);
				break;
			case 186u:
				performs_.AddEntriesFrom(ref input, _repeated_performs_codec);
				break;
			case 194u:
				performDict_.AddEntriesFrom(ref input, _map_performDict_codec);
				break;
			case 202u:
				lands_.AddEntriesFrom(ref input, _repeated_lands_codec);
				break;
			case 210u:
				landDict_.AddEntriesFrom(ref input, _map_landDict_codec);
				break;
			case 218u:
				cardTags_.AddEntriesFrom(ref input, _repeated_cardTags_codec);
				break;
			case 226u:
				cardTagDict_.AddEntriesFrom(ref input, _map_cardTagDict_codec);
				break;
			}
		}
	}
}
