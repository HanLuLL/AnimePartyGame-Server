using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using GameLogic;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Tools;
using UnityEngine;

public sealed class CharacterInfoConfigure : IMessage<CharacterInfoConfigure>, IMessage, IEquatable<CharacterInfoConfigure>, IDeepCloneable<CharacterInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<CharacterInfoConfigure> _parser = new MessageParser<CharacterInfoConfigure>(() => new CharacterInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int OrderWeightFieldNumber = 2;

	private int orderWeight_;

	public const int IsLinkageFieldNumber = 3;

	private bool isLinkage_;

	public const int NameIDFieldNumber = 4;

	private int nameID_;

	public const int NickIDFieldNumber = 5;

	private int nickID_;

	public const int CommentIdFieldNumber = 6;

	private int commentId_;

	public const int BiographyIDFieldNumber = 7;

	private int biographyID_;

	public const int IsGalleryShowFieldNumber = 8;

	private bool isGalleryShow_;

	public const int MechanismIDFieldNumber = 9;

	private int mechanismID_;

	public const int LinesIDFieldNumber = 10;

	private int linesID_;

	public const int NameBGColorFieldNumber = 11;

	private string nameBGColor_ = "";

	public const int HeroTypeFieldNumber = 12;

	private CharacterType heroType_;

	public const int TagTypeFieldNumber = 13;

	private CharacterTagType tagType_;

	public const int IsDefaultFieldNumber = 14;

	private bool isDefault_;

	public const int BloodFieldNumber = 15;

	private int blood_;

	public const int AttackFieldNumber = 16;

	private int attack_;

	public const int DefenseFieldNumber = 17;

	private int defense_;

	public const int ActiveSkillFieldNumber = 18;

	private int activeSkill_;

	public const int PveActiveSkillFieldNumber = 19;

	private int pveActiveSkill_;

	public const int PassiveSkillsFieldNumber = 20;

	private static readonly FieldCodec<int> _repeated_passiveSkills_codec = FieldCodec.ForSFixed32(162u);

	private readonly RepeatedField<int> passiveSkills_ = new RepeatedField<int>();

	public const int PvePassiveSkillsFieldNumber = 21;

	private static readonly FieldCodec<int> _repeated_pvePassiveSkills_codec = FieldCodec.ForSFixed32(170u);

	private readonly RepeatedField<int> pvePassiveSkills_ = new RepeatedField<int>();

	public const int PveBreakFieldNumber = 22;

	private static readonly FieldCodec<int> _repeated_pveBreak_codec = FieldCodec.ForSFixed32(178u);

	private readonly RepeatedField<int> pveBreak_ = new RepeatedField<int>();

	public const int CharacterMapFieldNumber = 23;

	private string characterMap_ = "";

	public const int OffsetInMapFieldNumber = 24;

	private static readonly FieldCodec<int> _repeated_offsetInMap_codec = FieldCodec.ForSFixed32(194u);

	private readonly RepeatedField<int> offsetInMap_ = new RepeatedField<int>();

	public const int LandTexFieldNumber = 25;

	private string landTex_ = "";

	public const int ExpressionPackIDFieldNumber = 26;

	private int expressionPackID_;

	public const int StandingPaintingFieldNumber = 27;

	private int standingPainting_;

	public const int HasKizunaFieldNumber = 28;

	private bool hasKizuna_;

	public const int HeroFavorGiftFieldNumber = 29;

	private int heroFavorGift_;

	public const int FavorLevelRewardFieldNumber = 30;

	private int favorLevelReward_;

	public const int FavorBreakthroughRewardFieldNumber = 31;

	private int favorBreakthroughReward_;

	public const int IntenseFixSkillFieldNumber = 32;

	private int intenseFixSkill_;

	private SkillInfoConfigure _PVPSkillInfoConfig;

	private SkillInfoConfigure _PVESkillInfoConfig;

	private FavorBreakthroughConfigure _FavorBreakthroughConfigure;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CharacterInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CharacterReflection.Descriptor.MessageTypes[0];

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
	public bool IsLinkage
	{
		get
		{
			return isLinkage_;
		}
		private set
		{
			isLinkage_ = value;
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
	public int NickID
	{
		get
		{
			return nickID_;
		}
		private set
		{
			nickID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CommentId
	{
		get
		{
			return commentId_;
		}
		private set
		{
			commentId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BiographyID
	{
		get
		{
			return biographyID_;
		}
		private set
		{
			biographyID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsGalleryShow
	{
		get
		{
			return isGalleryShow_;
		}
		private set
		{
			isGalleryShow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MechanismID
	{
		get
		{
			return mechanismID_;
		}
		private set
		{
			mechanismID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LinesID
	{
		get
		{
			return linesID_;
		}
		private set
		{
			linesID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string NameBGColor
	{
		get
		{
			return nameBGColor_;
		}
		private set
		{
			nameBGColor_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterType HeroType
	{
		get
		{
			return heroType_;
		}
		private set
		{
			heroType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterTagType TagType
	{
		get
		{
			return tagType_;
		}
		private set
		{
			tagType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDefault
	{
		get
		{
			return isDefault_;
		}
		private set
		{
			isDefault_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Blood
	{
		get
		{
			return blood_;
		}
		private set
		{
			blood_ = value;
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
		private set
		{
			attack_ = value;
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
		private set
		{
			defense_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActiveSkill
	{
		get
		{
			return activeSkill_;
		}
		private set
		{
			activeSkill_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PveActiveSkill
	{
		get
		{
			return pveActiveSkill_;
		}
		private set
		{
			pveActiveSkill_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PassiveSkills => passiveSkills_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PvePassiveSkills => pvePassiveSkills_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PveBreak => pveBreak_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CharacterMap
	{
		get
		{
			return characterMap_;
		}
		private set
		{
			characterMap_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> OffsetInMap => offsetInMap_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LandTex
	{
		get
		{
			return landTex_;
		}
		private set
		{
			landTex_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ExpressionPackID
	{
		get
		{
			return expressionPackID_;
		}
		private set
		{
			expressionPackID_ = value;
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
		private set
		{
			standingPainting_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool HasKizuna
	{
		get
		{
			return hasKizuna_;
		}
		private set
		{
			hasKizuna_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HeroFavorGift
	{
		get
		{
			return heroFavorGift_;
		}
		private set
		{
			heroFavorGift_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FavorLevelReward
	{
		get
		{
			return favorLevelReward_;
		}
		private set
		{
			favorLevelReward_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FavorBreakthroughReward
	{
		get
		{
			return favorBreakthroughReward_;
		}
		private set
		{
			favorBreakthroughReward_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int IntenseFixSkill
	{
		get
		{
			return intenseFixSkill_;
		}
		private set
		{
			intenseFixSkill_ = value;
		}
	}

	public SkillInfoConfigure PVPSkillInfoConfig
	{
		get
		{
			if (_PVPSkillInfoConfig == null && !StaticConfigure.Skill.InfoDict.TryGetValue(ActiveSkill, out _PVPSkillInfoConfig))
			{
				Debug.LogError("在Skill.Info表里并没有找到主动技能id：" + ActiveSkill);
			}
			return _PVPSkillInfoConfig;
		}
	}

	public SkillInfoConfigure PVESkillInfoConfig
	{
		get
		{
			if (_PVESkillInfoConfig == null && !StaticConfigure.Skill.InfoDict.TryGetValue(PveActiveSkill, out _PVESkillInfoConfig))
			{
				Debug.LogError("在Skill.Info表里并没有找到主动技能id：" + PveActiveSkill);
			}
			return _PVESkillInfoConfig;
		}
	}

	public FavorBreakthroughConfigure FavorBreakthroughConfigure
	{
		get
		{
			if (_FavorBreakthroughConfigure == null && !StaticConfigure.Favor.BreakthroughDict.TryGetValue(FavorBreakthroughReward, out _FavorBreakthroughConfigure))
			{
				Debug.LogError($"Favor.BreakthroughDict表里无法找到id:{FavorBreakthroughReward}对应的数据");
				return null;
			}
			return _FavorBreakthroughConfigure;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterInfoConfigure(CharacterInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		orderWeight_ = other.orderWeight_;
		isLinkage_ = other.isLinkage_;
		nameID_ = other.nameID_;
		nickID_ = other.nickID_;
		commentId_ = other.commentId_;
		biographyID_ = other.biographyID_;
		isGalleryShow_ = other.isGalleryShow_;
		mechanismID_ = other.mechanismID_;
		linesID_ = other.linesID_;
		nameBGColor_ = other.nameBGColor_;
		heroType_ = other.heroType_;
		tagType_ = other.tagType_;
		isDefault_ = other.isDefault_;
		blood_ = other.blood_;
		attack_ = other.attack_;
		defense_ = other.defense_;
		activeSkill_ = other.activeSkill_;
		pveActiveSkill_ = other.pveActiveSkill_;
		passiveSkills_ = other.passiveSkills_.Clone();
		pvePassiveSkills_ = other.pvePassiveSkills_.Clone();
		pveBreak_ = other.pveBreak_.Clone();
		characterMap_ = other.characterMap_;
		offsetInMap_ = other.offsetInMap_.Clone();
		landTex_ = other.landTex_;
		expressionPackID_ = other.expressionPackID_;
		standingPainting_ = other.standingPainting_;
		hasKizuna_ = other.hasKizuna_;
		heroFavorGift_ = other.heroFavorGift_;
		favorLevelReward_ = other.favorLevelReward_;
		favorBreakthroughReward_ = other.favorBreakthroughReward_;
		intenseFixSkill_ = other.intenseFixSkill_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterInfoConfigure Clone()
	{
		return new CharacterInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CharacterInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CharacterInfoConfigure other)
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
		if (OrderWeight != other.OrderWeight)
		{
			return false;
		}
		if (IsLinkage != other.IsLinkage)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (NickID != other.NickID)
		{
			return false;
		}
		if (CommentId != other.CommentId)
		{
			return false;
		}
		if (BiographyID != other.BiographyID)
		{
			return false;
		}
		if (IsGalleryShow != other.IsGalleryShow)
		{
			return false;
		}
		if (MechanismID != other.MechanismID)
		{
			return false;
		}
		if (LinesID != other.LinesID)
		{
			return false;
		}
		if (NameBGColor != other.NameBGColor)
		{
			return false;
		}
		if (HeroType != other.HeroType)
		{
			return false;
		}
		if (TagType != other.TagType)
		{
			return false;
		}
		if (IsDefault != other.IsDefault)
		{
			return false;
		}
		if (Blood != other.Blood)
		{
			return false;
		}
		if (Attack != other.Attack)
		{
			return false;
		}
		if (Defense != other.Defense)
		{
			return false;
		}
		if (ActiveSkill != other.ActiveSkill)
		{
			return false;
		}
		if (PveActiveSkill != other.PveActiveSkill)
		{
			return false;
		}
		if (!passiveSkills_.Equals(other.passiveSkills_))
		{
			return false;
		}
		if (!pvePassiveSkills_.Equals(other.pvePassiveSkills_))
		{
			return false;
		}
		if (!pveBreak_.Equals(other.pveBreak_))
		{
			return false;
		}
		if (CharacterMap != other.CharacterMap)
		{
			return false;
		}
		if (!offsetInMap_.Equals(other.offsetInMap_))
		{
			return false;
		}
		if (LandTex != other.LandTex)
		{
			return false;
		}
		if (ExpressionPackID != other.ExpressionPackID)
		{
			return false;
		}
		if (StandingPainting != other.StandingPainting)
		{
			return false;
		}
		if (HasKizuna != other.HasKizuna)
		{
			return false;
		}
		if (HeroFavorGift != other.HeroFavorGift)
		{
			return false;
		}
		if (FavorLevelReward != other.FavorLevelReward)
		{
			return false;
		}
		if (FavorBreakthroughReward != other.FavorBreakthroughReward)
		{
			return false;
		}
		if (IntenseFixSkill != other.IntenseFixSkill)
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
		if (OrderWeight != 0)
		{
			num ^= OrderWeight.GetHashCode();
		}
		if (IsLinkage)
		{
			num ^= IsLinkage.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (NickID != 0)
		{
			num ^= NickID.GetHashCode();
		}
		if (CommentId != 0)
		{
			num ^= CommentId.GetHashCode();
		}
		if (BiographyID != 0)
		{
			num ^= BiographyID.GetHashCode();
		}
		if (IsGalleryShow)
		{
			num ^= IsGalleryShow.GetHashCode();
		}
		if (MechanismID != 0)
		{
			num ^= MechanismID.GetHashCode();
		}
		if (LinesID != 0)
		{
			num ^= LinesID.GetHashCode();
		}
		if (NameBGColor.Length != 0)
		{
			num ^= NameBGColor.GetHashCode();
		}
		if (HeroType != CharacterType.None)
		{
			num ^= HeroType.GetHashCode();
		}
		if (TagType != CharacterTagType.None)
		{
			num ^= TagType.GetHashCode();
		}
		if (IsDefault)
		{
			num ^= IsDefault.GetHashCode();
		}
		if (Blood != 0)
		{
			num ^= Blood.GetHashCode();
		}
		if (Attack != 0)
		{
			num ^= Attack.GetHashCode();
		}
		if (Defense != 0)
		{
			num ^= Defense.GetHashCode();
		}
		if (ActiveSkill != 0)
		{
			num ^= ActiveSkill.GetHashCode();
		}
		if (PveActiveSkill != 0)
		{
			num ^= PveActiveSkill.GetHashCode();
		}
		num ^= passiveSkills_.GetHashCode();
		num ^= pvePassiveSkills_.GetHashCode();
		num ^= pveBreak_.GetHashCode();
		if (CharacterMap.Length != 0)
		{
			num ^= CharacterMap.GetHashCode();
		}
		num ^= offsetInMap_.GetHashCode();
		if (LandTex.Length != 0)
		{
			num ^= LandTex.GetHashCode();
		}
		if (ExpressionPackID != 0)
		{
			num ^= ExpressionPackID.GetHashCode();
		}
		if (StandingPainting != 0)
		{
			num ^= StandingPainting.GetHashCode();
		}
		if (HasKizuna)
		{
			num ^= HasKizuna.GetHashCode();
		}
		if (HeroFavorGift != 0)
		{
			num ^= HeroFavorGift.GetHashCode();
		}
		if (FavorLevelReward != 0)
		{
			num ^= FavorLevelReward.GetHashCode();
		}
		if (FavorBreakthroughReward != 0)
		{
			num ^= FavorBreakthroughReward.GetHashCode();
		}
		if (IntenseFixSkill != 0)
		{
			num ^= IntenseFixSkill.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (OrderWeight != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(OrderWeight);
		}
		if (IsLinkage)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsLinkage);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(NameID);
		}
		if (NickID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(NickID);
		}
		if (CommentId != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(CommentId);
		}
		if (BiographyID != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(BiographyID);
		}
		if (IsGalleryShow)
		{
			output.WriteRawTag(64);
			output.WriteBool(IsGalleryShow);
		}
		if (MechanismID != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(MechanismID);
		}
		if (LinesID != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(LinesID);
		}
		if (NameBGColor.Length != 0)
		{
			output.WriteRawTag(90);
			output.WriteString(NameBGColor);
		}
		if (HeroType != CharacterType.None)
		{
			output.WriteRawTag(96);
			output.WriteEnum((int)HeroType);
		}
		if (TagType != CharacterTagType.None)
		{
			output.WriteRawTag(104);
			output.WriteEnum((int)TagType);
		}
		if (IsDefault)
		{
			output.WriteRawTag(112);
			output.WriteBool(IsDefault);
		}
		if (Blood != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(Blood);
		}
		if (Attack != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(Attack);
		}
		if (Defense != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(Defense);
		}
		if (ActiveSkill != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(ActiveSkill);
		}
		if (PveActiveSkill != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(PveActiveSkill);
		}
		passiveSkills_.WriteTo(ref output, _repeated_passiveSkills_codec);
		pvePassiveSkills_.WriteTo(ref output, _repeated_pvePassiveSkills_codec);
		pveBreak_.WriteTo(ref output, _repeated_pveBreak_codec);
		if (CharacterMap.Length != 0)
		{
			output.WriteRawTag(186, 1);
			output.WriteString(CharacterMap);
		}
		offsetInMap_.WriteTo(ref output, _repeated_offsetInMap_codec);
		if (LandTex.Length != 0)
		{
			output.WriteRawTag(202, 1);
			output.WriteString(LandTex);
		}
		if (ExpressionPackID != 0)
		{
			output.WriteRawTag(213, 1);
			output.WriteSFixed32(ExpressionPackID);
		}
		if (StandingPainting != 0)
		{
			output.WriteRawTag(221, 1);
			output.WriteSFixed32(StandingPainting);
		}
		if (HasKizuna)
		{
			output.WriteRawTag(224, 1);
			output.WriteBool(HasKizuna);
		}
		if (HeroFavorGift != 0)
		{
			output.WriteRawTag(237, 1);
			output.WriteSFixed32(HeroFavorGift);
		}
		if (FavorLevelReward != 0)
		{
			output.WriteRawTag(245, 1);
			output.WriteSFixed32(FavorLevelReward);
		}
		if (FavorBreakthroughReward != 0)
		{
			output.WriteRawTag(253, 1);
			output.WriteSFixed32(FavorBreakthroughReward);
		}
		if (IntenseFixSkill != 0)
		{
			output.WriteRawTag(133, 2);
			output.WriteSFixed32(IntenseFixSkill);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (OrderWeight != 0)
		{
			num += 5;
		}
		if (IsLinkage)
		{
			num += 2;
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (NickID != 0)
		{
			num += 5;
		}
		if (CommentId != 0)
		{
			num += 5;
		}
		if (BiographyID != 0)
		{
			num += 5;
		}
		if (IsGalleryShow)
		{
			num += 2;
		}
		if (MechanismID != 0)
		{
			num += 5;
		}
		if (LinesID != 0)
		{
			num += 5;
		}
		if (NameBGColor.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(NameBGColor);
		}
		if (HeroType != CharacterType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)HeroType);
		}
		if (TagType != CharacterTagType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)TagType);
		}
		if (IsDefault)
		{
			num += 2;
		}
		if (Blood != 0)
		{
			num += 5;
		}
		if (Attack != 0)
		{
			num += 6;
		}
		if (Defense != 0)
		{
			num += 6;
		}
		if (ActiveSkill != 0)
		{
			num += 6;
		}
		if (PveActiveSkill != 0)
		{
			num += 6;
		}
		num += passiveSkills_.CalculateSize(_repeated_passiveSkills_codec);
		num += pvePassiveSkills_.CalculateSize(_repeated_pvePassiveSkills_codec);
		num += pveBreak_.CalculateSize(_repeated_pveBreak_codec);
		if (CharacterMap.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(CharacterMap);
		}
		num += offsetInMap_.CalculateSize(_repeated_offsetInMap_codec);
		if (LandTex.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(LandTex);
		}
		if (ExpressionPackID != 0)
		{
			num += 6;
		}
		if (StandingPainting != 0)
		{
			num += 6;
		}
		if (HasKizuna)
		{
			num += 3;
		}
		if (HeroFavorGift != 0)
		{
			num += 6;
		}
		if (FavorLevelReward != 0)
		{
			num += 6;
		}
		if (FavorBreakthroughReward != 0)
		{
			num += 6;
		}
		if (IntenseFixSkill != 0)
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
	public void MergeFrom(CharacterInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.OrderWeight != 0)
			{
				OrderWeight = other.OrderWeight;
			}
			if (other.IsLinkage)
			{
				IsLinkage = other.IsLinkage;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.NickID != 0)
			{
				NickID = other.NickID;
			}
			if (other.CommentId != 0)
			{
				CommentId = other.CommentId;
			}
			if (other.BiographyID != 0)
			{
				BiographyID = other.BiographyID;
			}
			if (other.IsGalleryShow)
			{
				IsGalleryShow = other.IsGalleryShow;
			}
			if (other.MechanismID != 0)
			{
				MechanismID = other.MechanismID;
			}
			if (other.LinesID != 0)
			{
				LinesID = other.LinesID;
			}
			if (other.NameBGColor.Length != 0)
			{
				NameBGColor = other.NameBGColor;
			}
			if (other.HeroType != CharacterType.None)
			{
				HeroType = other.HeroType;
			}
			if (other.TagType != CharacterTagType.None)
			{
				TagType = other.TagType;
			}
			if (other.IsDefault)
			{
				IsDefault = other.IsDefault;
			}
			if (other.Blood != 0)
			{
				Blood = other.Blood;
			}
			if (other.Attack != 0)
			{
				Attack = other.Attack;
			}
			if (other.Defense != 0)
			{
				Defense = other.Defense;
			}
			if (other.ActiveSkill != 0)
			{
				ActiveSkill = other.ActiveSkill;
			}
			if (other.PveActiveSkill != 0)
			{
				PveActiveSkill = other.PveActiveSkill;
			}
			passiveSkills_.Add(other.passiveSkills_);
			pvePassiveSkills_.Add(other.pvePassiveSkills_);
			pveBreak_.Add(other.pveBreak_);
			if (other.CharacterMap.Length != 0)
			{
				CharacterMap = other.CharacterMap;
			}
			offsetInMap_.Add(other.offsetInMap_);
			if (other.LandTex.Length != 0)
			{
				LandTex = other.LandTex;
			}
			if (other.ExpressionPackID != 0)
			{
				ExpressionPackID = other.ExpressionPackID;
			}
			if (other.StandingPainting != 0)
			{
				StandingPainting = other.StandingPainting;
			}
			if (other.HasKizuna)
			{
				HasKizuna = other.HasKizuna;
			}
			if (other.HeroFavorGift != 0)
			{
				HeroFavorGift = other.HeroFavorGift;
			}
			if (other.FavorLevelReward != 0)
			{
				FavorLevelReward = other.FavorLevelReward;
			}
			if (other.FavorBreakthroughReward != 0)
			{
				FavorBreakthroughReward = other.FavorBreakthroughReward;
			}
			if (other.IntenseFixSkill != 0)
			{
				IntenseFixSkill = other.IntenseFixSkill;
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
				Id = input.ReadSFixed32();
				break;
			case 21u:
				OrderWeight = input.ReadSFixed32();
				break;
			case 24u:
				IsLinkage = input.ReadBool();
				break;
			case 37u:
				NameID = input.ReadSFixed32();
				break;
			case 45u:
				NickID = input.ReadSFixed32();
				break;
			case 53u:
				CommentId = input.ReadSFixed32();
				break;
			case 61u:
				BiographyID = input.ReadSFixed32();
				break;
			case 64u:
				IsGalleryShow = input.ReadBool();
				break;
			case 77u:
				MechanismID = input.ReadSFixed32();
				break;
			case 85u:
				LinesID = input.ReadSFixed32();
				break;
			case 90u:
				NameBGColor = input.ReadString();
				break;
			case 96u:
				HeroType = (CharacterType)input.ReadEnum();
				break;
			case 104u:
				TagType = (CharacterTagType)input.ReadEnum();
				break;
			case 112u:
				IsDefault = input.ReadBool();
				break;
			case 125u:
				Blood = input.ReadSFixed32();
				break;
			case 133u:
				Attack = input.ReadSFixed32();
				break;
			case 141u:
				Defense = input.ReadSFixed32();
				break;
			case 149u:
				ActiveSkill = input.ReadSFixed32();
				break;
			case 157u:
				PveActiveSkill = input.ReadSFixed32();
				break;
			case 162u:
			case 165u:
				passiveSkills_.AddEntriesFrom(ref input, _repeated_passiveSkills_codec);
				break;
			case 170u:
			case 173u:
				pvePassiveSkills_.AddEntriesFrom(ref input, _repeated_pvePassiveSkills_codec);
				break;
			case 178u:
			case 181u:
				pveBreak_.AddEntriesFrom(ref input, _repeated_pveBreak_codec);
				break;
			case 186u:
				CharacterMap = input.ReadString();
				break;
			case 194u:
			case 197u:
				offsetInMap_.AddEntriesFrom(ref input, _repeated_offsetInMap_codec);
				break;
			case 202u:
				LandTex = input.ReadString();
				break;
			case 213u:
				ExpressionPackID = input.ReadSFixed32();
				break;
			case 221u:
				StandingPainting = input.ReadSFixed32();
				break;
			case 224u:
				HasKizuna = input.ReadBool();
				break;
			case 237u:
				HeroFavorGift = input.ReadSFixed32();
				break;
			case 245u:
				FavorLevelReward = input.ReadSFixed32();
				break;
			case 253u:
				FavorBreakthroughReward = input.ReadSFixed32();
				break;
			case 261u:
				IntenseFixSkill = input.ReadSFixed32();
				break;
			}
		}
	}

	public int GetBattleActiveSkillId(int talentId)
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room == null || !room.IsInRoom)
		{
			return ActiveSkill;
		}
		if (room.curRoomInfo.IsPVE())
		{
			int num = 0;
			if (talentId > 0)
			{
				if (!StaticConfigure.PVENurturance.BreakDict.TryGetValue(talentId, out var value))
				{
					Debug.LogError($"未从PVENurturance表中Break表中获取到ID：{talentId}的配置！");
					return 0;
				}
				num = value.ReplaceActiveSkill;
			}
			if (num != 0)
			{
				return num;
			}
			return PveActiveSkill;
		}
		return ActiveSkill;
	}

	public RepeatedField<int> GetBattlePassiveSkills(int talentId)
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room == null || !room.IsInRoom)
		{
			return PassiveSkills;
		}
		if (room.curRoomInfo.IsPVE())
		{
			if (talentId <= 0 || !StaticConfigure.PVENurturance.BreakDict.TryGetValue(talentId, out var value))
			{
				return PvePassiveSkills;
			}
			RepeatedField<int> repeatedField = new RepeatedField<int>();
			repeatedField.AddRange(PvePassiveSkills);
			foreach (int addPassiveSkill in value.AddPassiveSkills)
			{
				repeatedField.Add(addPassiveSkill);
			}
			{
				foreach (int delPassiveSkill in value.DelPassiveSkills)
				{
					repeatedField.Remove(delPassiveSkill);
				}
				return repeatedField;
			}
		}
		return PassiveSkills;
	}
}
