using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Core;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using UnityEngine;

public sealed class SkinStandingPaintingConfigureItem : IMessage<SkinStandingPaintingConfigureItem>, IMessage, IEquatable<SkinStandingPaintingConfigureItem>, IDeepCloneable<SkinStandingPaintingConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<SkinStandingPaintingConfigureItem> _parser = new MessageParser<SkinStandingPaintingConfigureItem>(() => new SkinStandingPaintingConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int IsDefaultFieldNumber = 2;

	private bool isDefault_;

	public const int ItemIDFieldNumber = 3;

	private int itemID_;

	public const int SkinAppearanceTypeFieldNumber = 4;

	private SkinAppearanceType skinAppearanceType_;

	public const int SkinScaleFieldNumber = 5;

	private static readonly FieldCodec<float> _repeated_skinScale_codec = FieldCodec.ForFloat(42u);

	private readonly RepeatedField<float> skinScale_ = new RepeatedField<float>();

	public const int LandIconFieldNumber = 6;

	private string landIcon_ = "";

	public const int CharacterFieldNumber = 7;

	private string character_ = "";

	public const int SfwCharacterFieldNumber = 8;

	private string sfwCharacter_ = "";

	public const int InGameCharacterFieldNumber = 9;

	private string inGameCharacter_ = "";

	public const int InGameCharacterSFWFieldNumber = 10;

	private string inGameCharacterSFW_ = "";

	public const int CharacterLevelUpFieldNumber = 11;

	private string characterLevelUp_ = "";

	public const int CharacterBattlleResFieldNumber = 12;

	private int characterBattlleRes_;

	public const int CharacterLabelFieldNumber = 13;

	private string characterLabel_ = "";

	public const int ProfilePhotoFieldNumber = 14;

	private string profilePhoto_ = "";

	public const int BustFieldNumber = 15;

	private static readonly FieldCodec<string> _repeated_bust_codec = FieldCodec.ForString(122u);

	private readonly RepeatedField<string> bust_ = new RepeatedField<string>();

	public const int SfwBustFieldNumber = 16;

	private static readonly FieldCodec<string> _repeated_sfwBust_codec = FieldCodec.ForString(130u);

	private readonly RepeatedField<string> sfwBust_ = new RepeatedField<string>();

	public const int CharacterReadyFieldNumber = 17;

	private string characterReady_ = "";

	public const int SfwCharacterReadyFieldNumber = 18;

	private string sfwCharacterReady_ = "";

	public const int CharacterThinFieldNumber = 19;

	private string characterThin_ = "";

	public const int SfwCharacterThinFieldNumber = 20;

	private string sfwCharacterThin_ = "";

	public const int SkillVideoFieldNumber = 21;

	private string skillVideo_ = "";

	public const int SfwSkillVideoFieldNumber = 22;

	private string sfwSkillVideo_ = "";

	public const int RolePhotoFieldNumber = 23;

	private string rolePhoto_ = "";

	public const int SfwRolePhotoFieldNumber = 24;

	private string sfwRolePhoto_ = "";

	public const int FanfarePerformFieldNumber = 25;

	private int fanfarePerform_;

	public const int PreviewVideoFieldNumber = 26;

	private string previewVideo_ = "";

	public const int FightVideosFieldNumber = 27;

	private static readonly FieldCodec<int> _repeated_fightVideos_codec = FieldCodec.ForSFixed32(218u);

	private readonly RepeatedField<int> fightVideos_ = new RepeatedField<int>();

	public const int FightBackGroundFieldNumber = 28;

	private string fightBackGround_ = "";

	public const int FightBackGroundPPFieldNumber = 29;

	private int fightBackGroundPP_;

	public const int SoundbankidFieldNumber = 30;

	private static readonly FieldCodec<int> _repeated_soundbankid_codec = FieldCodec.ForSFixed32(242u);

	private readonly RepeatedField<int> soundbankid_ = new RepeatedField<int>();

	public const int FightBGMFieldNumber = 31;

	private int fightBGM_;

	public const int VoiceFieldNumber = 32;

	private int voice_;

	public const int ActionsFieldNumber = 33;

	private static readonly FieldCodec<string> _repeated_actions_codec = FieldCodec.ForString(266u);

	private readonly RepeatedField<string> actions_ = new RepeatedField<string>();

	public const int MoveVfxIdFieldNumber = 34;

	private int moveVfxId_;

	public const int LongTextIDFieldNumber = 35;

	private int longTextID_;

	public const int ShortTextIDFieldNumber = 36;

	private int shortTextID_;

	private BattleResourceInfoConfigure _BattleResConfig;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SkinStandingPaintingConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SkinReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Index
	{
		get
		{
			return index_;
		}
		private set
		{
			index_ = value;
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
	public int ItemID
	{
		get
		{
			return itemID_;
		}
		private set
		{
			itemID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinAppearanceType SkinAppearanceType
	{
		get
		{
			return skinAppearanceType_;
		}
		private set
		{
			skinAppearanceType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<float> SkinScale => skinScale_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LandIcon
	{
		get
		{
			return landIcon_;
		}
		private set
		{
			landIcon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Character
	{
		get
		{
			return character_;
		}
		private set
		{
			character_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SfwCharacter
	{
		get
		{
			return sfwCharacter_;
		}
		private set
		{
			sfwCharacter_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string InGameCharacter
	{
		get
		{
			return inGameCharacter_;
		}
		private set
		{
			inGameCharacter_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string InGameCharacterSFW
	{
		get
		{
			return inGameCharacterSFW_;
		}
		private set
		{
			inGameCharacterSFW_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CharacterLevelUp
	{
		get
		{
			return characterLevelUp_;
		}
		private set
		{
			characterLevelUp_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CharacterBattlleRes
	{
		get
		{
			return characterBattlleRes_;
		}
		private set
		{
			characterBattlleRes_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CharacterLabel
	{
		get
		{
			return characterLabel_;
		}
		private set
		{
			characterLabel_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ProfilePhoto
	{
		get
		{
			return profilePhoto_;
		}
		private set
		{
			profilePhoto_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> Bust => bust_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> SfwBust => sfwBust_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CharacterReady
	{
		get
		{
			return characterReady_;
		}
		private set
		{
			characterReady_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SfwCharacterReady
	{
		get
		{
			return sfwCharacterReady_;
		}
		private set
		{
			sfwCharacterReady_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CharacterThin
	{
		get
		{
			return characterThin_;
		}
		private set
		{
			characterThin_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SfwCharacterThin
	{
		get
		{
			return sfwCharacterThin_;
		}
		private set
		{
			sfwCharacterThin_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SkillVideo
	{
		get
		{
			return skillVideo_;
		}
		private set
		{
			skillVideo_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SfwSkillVideo
	{
		get
		{
			return sfwSkillVideo_;
		}
		private set
		{
			sfwSkillVideo_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string RolePhoto
	{
		get
		{
			return rolePhoto_;
		}
		private set
		{
			rolePhoto_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SfwRolePhoto
	{
		get
		{
			return sfwRolePhoto_;
		}
		private set
		{
			sfwRolePhoto_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FanfarePerform
	{
		get
		{
			return fanfarePerform_;
		}
		private set
		{
			fanfarePerform_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PreviewVideo
	{
		get
		{
			return previewVideo_;
		}
		private set
		{
			previewVideo_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> FightVideos => fightVideos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string FightBackGround
	{
		get
		{
			return fightBackGround_;
		}
		private set
		{
			fightBackGround_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FightBackGroundPP
	{
		get
		{
			return fightBackGroundPP_;
		}
		private set
		{
			fightBackGroundPP_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Soundbankid => soundbankid_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FightBGM
	{
		get
		{
			return fightBGM_;
		}
		private set
		{
			fightBGM_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Voice
	{
		get
		{
			return voice_;
		}
		private set
		{
			voice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> Actions => actions_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MoveVfxId
	{
		get
		{
			return moveVfxId_;
		}
		private set
		{
			moveVfxId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LongTextID
	{
		get
		{
			return longTextID_;
		}
		private set
		{
			longTextID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ShortTextID
	{
		get
		{
			return shortTextID_;
		}
		private set
		{
			shortTextID_ = value;
		}
	}

	public BattleResourceInfoConfigure BattleResConfig
	{
		get
		{
			if (_BattleResConfig == null && !StaticConfigure.BattleResource.InfoDict.TryGetValue(CharacterBattlleRes, out _BattleResConfig))
			{
				Debug.LogError($"BattleResource.InfoDict无法通过id:{CharacterBattlleRes}获取资源");
			}
			return _BattleResConfig;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinStandingPaintingConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinStandingPaintingConfigureItem(SkinStandingPaintingConfigureItem other)
		: this()
	{
		index_ = other.index_;
		isDefault_ = other.isDefault_;
		itemID_ = other.itemID_;
		skinAppearanceType_ = other.skinAppearanceType_;
		skinScale_ = other.skinScale_.Clone();
		landIcon_ = other.landIcon_;
		character_ = other.character_;
		sfwCharacter_ = other.sfwCharacter_;
		inGameCharacter_ = other.inGameCharacter_;
		inGameCharacterSFW_ = other.inGameCharacterSFW_;
		characterLevelUp_ = other.characterLevelUp_;
		characterBattlleRes_ = other.characterBattlleRes_;
		characterLabel_ = other.characterLabel_;
		profilePhoto_ = other.profilePhoto_;
		bust_ = other.bust_.Clone();
		sfwBust_ = other.sfwBust_.Clone();
		characterReady_ = other.characterReady_;
		sfwCharacterReady_ = other.sfwCharacterReady_;
		characterThin_ = other.characterThin_;
		sfwCharacterThin_ = other.sfwCharacterThin_;
		skillVideo_ = other.skillVideo_;
		sfwSkillVideo_ = other.sfwSkillVideo_;
		rolePhoto_ = other.rolePhoto_;
		sfwRolePhoto_ = other.sfwRolePhoto_;
		fanfarePerform_ = other.fanfarePerform_;
		previewVideo_ = other.previewVideo_;
		fightVideos_ = other.fightVideos_.Clone();
		fightBackGround_ = other.fightBackGround_;
		fightBackGroundPP_ = other.fightBackGroundPP_;
		soundbankid_ = other.soundbankid_.Clone();
		fightBGM_ = other.fightBGM_;
		voice_ = other.voice_;
		actions_ = other.actions_.Clone();
		moveVfxId_ = other.moveVfxId_;
		longTextID_ = other.longTextID_;
		shortTextID_ = other.shortTextID_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinStandingPaintingConfigureItem Clone()
	{
		return new SkinStandingPaintingConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SkinStandingPaintingConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SkinStandingPaintingConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Index != other.Index)
		{
			return false;
		}
		if (IsDefault != other.IsDefault)
		{
			return false;
		}
		if (ItemID != other.ItemID)
		{
			return false;
		}
		if (SkinAppearanceType != other.SkinAppearanceType)
		{
			return false;
		}
		if (!skinScale_.Equals(other.skinScale_))
		{
			return false;
		}
		if (LandIcon != other.LandIcon)
		{
			return false;
		}
		if (Character != other.Character)
		{
			return false;
		}
		if (SfwCharacter != other.SfwCharacter)
		{
			return false;
		}
		if (InGameCharacter != other.InGameCharacter)
		{
			return false;
		}
		if (InGameCharacterSFW != other.InGameCharacterSFW)
		{
			return false;
		}
		if (CharacterLevelUp != other.CharacterLevelUp)
		{
			return false;
		}
		if (CharacterBattlleRes != other.CharacterBattlleRes)
		{
			return false;
		}
		if (CharacterLabel != other.CharacterLabel)
		{
			return false;
		}
		if (ProfilePhoto != other.ProfilePhoto)
		{
			return false;
		}
		if (!bust_.Equals(other.bust_))
		{
			return false;
		}
		if (!sfwBust_.Equals(other.sfwBust_))
		{
			return false;
		}
		if (CharacterReady != other.CharacterReady)
		{
			return false;
		}
		if (SfwCharacterReady != other.SfwCharacterReady)
		{
			return false;
		}
		if (CharacterThin != other.CharacterThin)
		{
			return false;
		}
		if (SfwCharacterThin != other.SfwCharacterThin)
		{
			return false;
		}
		if (SkillVideo != other.SkillVideo)
		{
			return false;
		}
		if (SfwSkillVideo != other.SfwSkillVideo)
		{
			return false;
		}
		if (RolePhoto != other.RolePhoto)
		{
			return false;
		}
		if (SfwRolePhoto != other.SfwRolePhoto)
		{
			return false;
		}
		if (FanfarePerform != other.FanfarePerform)
		{
			return false;
		}
		if (PreviewVideo != other.PreviewVideo)
		{
			return false;
		}
		if (!fightVideos_.Equals(other.fightVideos_))
		{
			return false;
		}
		if (FightBackGround != other.FightBackGround)
		{
			return false;
		}
		if (FightBackGroundPP != other.FightBackGroundPP)
		{
			return false;
		}
		if (!soundbankid_.Equals(other.soundbankid_))
		{
			return false;
		}
		if (FightBGM != other.FightBGM)
		{
			return false;
		}
		if (Voice != other.Voice)
		{
			return false;
		}
		if (!actions_.Equals(other.actions_))
		{
			return false;
		}
		if (MoveVfxId != other.MoveVfxId)
		{
			return false;
		}
		if (LongTextID != other.LongTextID)
		{
			return false;
		}
		if (ShortTextID != other.ShortTextID)
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
		if (Index != 0)
		{
			num ^= Index.GetHashCode();
		}
		if (IsDefault)
		{
			num ^= IsDefault.GetHashCode();
		}
		if (ItemID != 0)
		{
			num ^= ItemID.GetHashCode();
		}
		if (SkinAppearanceType != SkinAppearanceType.None)
		{
			num ^= SkinAppearanceType.GetHashCode();
		}
		num ^= skinScale_.GetHashCode();
		if (LandIcon.Length != 0)
		{
			num ^= LandIcon.GetHashCode();
		}
		if (Character.Length != 0)
		{
			num ^= Character.GetHashCode();
		}
		if (SfwCharacter.Length != 0)
		{
			num ^= SfwCharacter.GetHashCode();
		}
		if (InGameCharacter.Length != 0)
		{
			num ^= InGameCharacter.GetHashCode();
		}
		if (InGameCharacterSFW.Length != 0)
		{
			num ^= InGameCharacterSFW.GetHashCode();
		}
		if (CharacterLevelUp.Length != 0)
		{
			num ^= CharacterLevelUp.GetHashCode();
		}
		if (CharacterBattlleRes != 0)
		{
			num ^= CharacterBattlleRes.GetHashCode();
		}
		if (CharacterLabel.Length != 0)
		{
			num ^= CharacterLabel.GetHashCode();
		}
		if (ProfilePhoto.Length != 0)
		{
			num ^= ProfilePhoto.GetHashCode();
		}
		num ^= bust_.GetHashCode();
		num ^= sfwBust_.GetHashCode();
		if (CharacterReady.Length != 0)
		{
			num ^= CharacterReady.GetHashCode();
		}
		if (SfwCharacterReady.Length != 0)
		{
			num ^= SfwCharacterReady.GetHashCode();
		}
		if (CharacterThin.Length != 0)
		{
			num ^= CharacterThin.GetHashCode();
		}
		if (SfwCharacterThin.Length != 0)
		{
			num ^= SfwCharacterThin.GetHashCode();
		}
		if (SkillVideo.Length != 0)
		{
			num ^= SkillVideo.GetHashCode();
		}
		if (SfwSkillVideo.Length != 0)
		{
			num ^= SfwSkillVideo.GetHashCode();
		}
		if (RolePhoto.Length != 0)
		{
			num ^= RolePhoto.GetHashCode();
		}
		if (SfwRolePhoto.Length != 0)
		{
			num ^= SfwRolePhoto.GetHashCode();
		}
		if (FanfarePerform != 0)
		{
			num ^= FanfarePerform.GetHashCode();
		}
		if (PreviewVideo.Length != 0)
		{
			num ^= PreviewVideo.GetHashCode();
		}
		num ^= fightVideos_.GetHashCode();
		if (FightBackGround.Length != 0)
		{
			num ^= FightBackGround.GetHashCode();
		}
		if (FightBackGroundPP != 0)
		{
			num ^= FightBackGroundPP.GetHashCode();
		}
		num ^= soundbankid_.GetHashCode();
		if (FightBGM != 0)
		{
			num ^= FightBGM.GetHashCode();
		}
		if (Voice != 0)
		{
			num ^= Voice.GetHashCode();
		}
		num ^= actions_.GetHashCode();
		if (MoveVfxId != 0)
		{
			num ^= MoveVfxId.GetHashCode();
		}
		if (LongTextID != 0)
		{
			num ^= LongTextID.GetHashCode();
		}
		if (ShortTextID != 0)
		{
			num ^= ShortTextID.GetHashCode();
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
		if (Index != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Index);
		}
		if (IsDefault)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsDefault);
		}
		if (ItemID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ItemID);
		}
		if (SkinAppearanceType != SkinAppearanceType.None)
		{
			output.WriteRawTag(32);
			output.WriteEnum((int)SkinAppearanceType);
		}
		skinScale_.WriteTo(ref output, _repeated_skinScale_codec);
		if (LandIcon.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(LandIcon);
		}
		if (Character.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(Character);
		}
		if (SfwCharacter.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(SfwCharacter);
		}
		if (InGameCharacter.Length != 0)
		{
			output.WriteRawTag(74);
			output.WriteString(InGameCharacter);
		}
		if (InGameCharacterSFW.Length != 0)
		{
			output.WriteRawTag(82);
			output.WriteString(InGameCharacterSFW);
		}
		if (CharacterLevelUp.Length != 0)
		{
			output.WriteRawTag(90);
			output.WriteString(CharacterLevelUp);
		}
		if (CharacterBattlleRes != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(CharacterBattlleRes);
		}
		if (CharacterLabel.Length != 0)
		{
			output.WriteRawTag(106);
			output.WriteString(CharacterLabel);
		}
		if (ProfilePhoto.Length != 0)
		{
			output.WriteRawTag(114);
			output.WriteString(ProfilePhoto);
		}
		bust_.WriteTo(ref output, _repeated_bust_codec);
		sfwBust_.WriteTo(ref output, _repeated_sfwBust_codec);
		if (CharacterReady.Length != 0)
		{
			output.WriteRawTag(138, 1);
			output.WriteString(CharacterReady);
		}
		if (SfwCharacterReady.Length != 0)
		{
			output.WriteRawTag(146, 1);
			output.WriteString(SfwCharacterReady);
		}
		if (CharacterThin.Length != 0)
		{
			output.WriteRawTag(154, 1);
			output.WriteString(CharacterThin);
		}
		if (SfwCharacterThin.Length != 0)
		{
			output.WriteRawTag(162, 1);
			output.WriteString(SfwCharacterThin);
		}
		if (SkillVideo.Length != 0)
		{
			output.WriteRawTag(170, 1);
			output.WriteString(SkillVideo);
		}
		if (SfwSkillVideo.Length != 0)
		{
			output.WriteRawTag(178, 1);
			output.WriteString(SfwSkillVideo);
		}
		if (RolePhoto.Length != 0)
		{
			output.WriteRawTag(186, 1);
			output.WriteString(RolePhoto);
		}
		if (SfwRolePhoto.Length != 0)
		{
			output.WriteRawTag(194, 1);
			output.WriteString(SfwRolePhoto);
		}
		if (FanfarePerform != 0)
		{
			output.WriteRawTag(205, 1);
			output.WriteSFixed32(FanfarePerform);
		}
		if (PreviewVideo.Length != 0)
		{
			output.WriteRawTag(210, 1);
			output.WriteString(PreviewVideo);
		}
		fightVideos_.WriteTo(ref output, _repeated_fightVideos_codec);
		if (FightBackGround.Length != 0)
		{
			output.WriteRawTag(226, 1);
			output.WriteString(FightBackGround);
		}
		if (FightBackGroundPP != 0)
		{
			output.WriteRawTag(237, 1);
			output.WriteSFixed32(FightBackGroundPP);
		}
		soundbankid_.WriteTo(ref output, _repeated_soundbankid_codec);
		if (FightBGM != 0)
		{
			output.WriteRawTag(253, 1);
			output.WriteSFixed32(FightBGM);
		}
		if (Voice != 0)
		{
			output.WriteRawTag(133, 2);
			output.WriteSFixed32(Voice);
		}
		actions_.WriteTo(ref output, _repeated_actions_codec);
		if (MoveVfxId != 0)
		{
			output.WriteRawTag(149, 2);
			output.WriteSFixed32(MoveVfxId);
		}
		if (LongTextID != 0)
		{
			output.WriteRawTag(157, 2);
			output.WriteSFixed32(LongTextID);
		}
		if (ShortTextID != 0)
		{
			output.WriteRawTag(165, 2);
			output.WriteSFixed32(ShortTextID);
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
		if (Index != 0)
		{
			num += 5;
		}
		if (IsDefault)
		{
			num += 2;
		}
		if (ItemID != 0)
		{
			num += 5;
		}
		if (SkinAppearanceType != SkinAppearanceType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)SkinAppearanceType);
		}
		num += skinScale_.CalculateSize(_repeated_skinScale_codec);
		if (LandIcon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LandIcon);
		}
		if (Character.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Character);
		}
		if (SfwCharacter.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SfwCharacter);
		}
		if (InGameCharacter.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(InGameCharacter);
		}
		if (InGameCharacterSFW.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(InGameCharacterSFW);
		}
		if (CharacterLevelUp.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CharacterLevelUp);
		}
		if (CharacterBattlleRes != 0)
		{
			num += 5;
		}
		if (CharacterLabel.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CharacterLabel);
		}
		if (ProfilePhoto.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ProfilePhoto);
		}
		num += bust_.CalculateSize(_repeated_bust_codec);
		num += sfwBust_.CalculateSize(_repeated_sfwBust_codec);
		if (CharacterReady.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(CharacterReady);
		}
		if (SfwCharacterReady.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(SfwCharacterReady);
		}
		if (CharacterThin.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(CharacterThin);
		}
		if (SfwCharacterThin.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(SfwCharacterThin);
		}
		if (SkillVideo.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(SkillVideo);
		}
		if (SfwSkillVideo.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(SfwSkillVideo);
		}
		if (RolePhoto.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(RolePhoto);
		}
		if (SfwRolePhoto.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(SfwRolePhoto);
		}
		if (FanfarePerform != 0)
		{
			num += 6;
		}
		if (PreviewVideo.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(PreviewVideo);
		}
		num += fightVideos_.CalculateSize(_repeated_fightVideos_codec);
		if (FightBackGround.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(FightBackGround);
		}
		if (FightBackGroundPP != 0)
		{
			num += 6;
		}
		num += soundbankid_.CalculateSize(_repeated_soundbankid_codec);
		if (FightBGM != 0)
		{
			num += 6;
		}
		if (Voice != 0)
		{
			num += 6;
		}
		num += actions_.CalculateSize(_repeated_actions_codec);
		if (MoveVfxId != 0)
		{
			num += 6;
		}
		if (LongTextID != 0)
		{
			num += 6;
		}
		if (ShortTextID != 0)
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
	public void MergeFrom(SkinStandingPaintingConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.IsDefault)
			{
				IsDefault = other.IsDefault;
			}
			if (other.ItemID != 0)
			{
				ItemID = other.ItemID;
			}
			if (other.SkinAppearanceType != SkinAppearanceType.None)
			{
				SkinAppearanceType = other.SkinAppearanceType;
			}
			skinScale_.Add(other.skinScale_);
			if (other.LandIcon.Length != 0)
			{
				LandIcon = other.LandIcon;
			}
			if (other.Character.Length != 0)
			{
				Character = other.Character;
			}
			if (other.SfwCharacter.Length != 0)
			{
				SfwCharacter = other.SfwCharacter;
			}
			if (other.InGameCharacter.Length != 0)
			{
				InGameCharacter = other.InGameCharacter;
			}
			if (other.InGameCharacterSFW.Length != 0)
			{
				InGameCharacterSFW = other.InGameCharacterSFW;
			}
			if (other.CharacterLevelUp.Length != 0)
			{
				CharacterLevelUp = other.CharacterLevelUp;
			}
			if (other.CharacterBattlleRes != 0)
			{
				CharacterBattlleRes = other.CharacterBattlleRes;
			}
			if (other.CharacterLabel.Length != 0)
			{
				CharacterLabel = other.CharacterLabel;
			}
			if (other.ProfilePhoto.Length != 0)
			{
				ProfilePhoto = other.ProfilePhoto;
			}
			bust_.Add(other.bust_);
			sfwBust_.Add(other.sfwBust_);
			if (other.CharacterReady.Length != 0)
			{
				CharacterReady = other.CharacterReady;
			}
			if (other.SfwCharacterReady.Length != 0)
			{
				SfwCharacterReady = other.SfwCharacterReady;
			}
			if (other.CharacterThin.Length != 0)
			{
				CharacterThin = other.CharacterThin;
			}
			if (other.SfwCharacterThin.Length != 0)
			{
				SfwCharacterThin = other.SfwCharacterThin;
			}
			if (other.SkillVideo.Length != 0)
			{
				SkillVideo = other.SkillVideo;
			}
			if (other.SfwSkillVideo.Length != 0)
			{
				SfwSkillVideo = other.SfwSkillVideo;
			}
			if (other.RolePhoto.Length != 0)
			{
				RolePhoto = other.RolePhoto;
			}
			if (other.SfwRolePhoto.Length != 0)
			{
				SfwRolePhoto = other.SfwRolePhoto;
			}
			if (other.FanfarePerform != 0)
			{
				FanfarePerform = other.FanfarePerform;
			}
			if (other.PreviewVideo.Length != 0)
			{
				PreviewVideo = other.PreviewVideo;
			}
			fightVideos_.Add(other.fightVideos_);
			if (other.FightBackGround.Length != 0)
			{
				FightBackGround = other.FightBackGround;
			}
			if (other.FightBackGroundPP != 0)
			{
				FightBackGroundPP = other.FightBackGroundPP;
			}
			soundbankid_.Add(other.soundbankid_);
			if (other.FightBGM != 0)
			{
				FightBGM = other.FightBGM;
			}
			if (other.Voice != 0)
			{
				Voice = other.Voice;
			}
			actions_.Add(other.actions_);
			if (other.MoveVfxId != 0)
			{
				MoveVfxId = other.MoveVfxId;
			}
			if (other.LongTextID != 0)
			{
				LongTextID = other.LongTextID;
			}
			if (other.ShortTextID != 0)
			{
				ShortTextID = other.ShortTextID;
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
				Index = input.ReadSFixed32();
				break;
			case 16u:
				IsDefault = input.ReadBool();
				break;
			case 29u:
				ItemID = input.ReadSFixed32();
				break;
			case 32u:
				SkinAppearanceType = (SkinAppearanceType)input.ReadEnum();
				break;
			case 42u:
			case 45u:
				skinScale_.AddEntriesFrom(ref input, _repeated_skinScale_codec);
				break;
			case 50u:
				LandIcon = input.ReadString();
				break;
			case 58u:
				Character = input.ReadString();
				break;
			case 66u:
				SfwCharacter = input.ReadString();
				break;
			case 74u:
				InGameCharacter = input.ReadString();
				break;
			case 82u:
				InGameCharacterSFW = input.ReadString();
				break;
			case 90u:
				CharacterLevelUp = input.ReadString();
				break;
			case 101u:
				CharacterBattlleRes = input.ReadSFixed32();
				break;
			case 106u:
				CharacterLabel = input.ReadString();
				break;
			case 114u:
				ProfilePhoto = input.ReadString();
				break;
			case 122u:
				bust_.AddEntriesFrom(ref input, _repeated_bust_codec);
				break;
			case 130u:
				sfwBust_.AddEntriesFrom(ref input, _repeated_sfwBust_codec);
				break;
			case 138u:
				CharacterReady = input.ReadString();
				break;
			case 146u:
				SfwCharacterReady = input.ReadString();
				break;
			case 154u:
				CharacterThin = input.ReadString();
				break;
			case 162u:
				SfwCharacterThin = input.ReadString();
				break;
			case 170u:
				SkillVideo = input.ReadString();
				break;
			case 178u:
				SfwSkillVideo = input.ReadString();
				break;
			case 186u:
				RolePhoto = input.ReadString();
				break;
			case 194u:
				SfwRolePhoto = input.ReadString();
				break;
			case 205u:
				FanfarePerform = input.ReadSFixed32();
				break;
			case 210u:
				PreviewVideo = input.ReadString();
				break;
			case 218u:
			case 221u:
				fightVideos_.AddEntriesFrom(ref input, _repeated_fightVideos_codec);
				break;
			case 226u:
				FightBackGround = input.ReadString();
				break;
			case 237u:
				FightBackGroundPP = input.ReadSFixed32();
				break;
			case 242u:
			case 245u:
				soundbankid_.AddEntriesFrom(ref input, _repeated_soundbankid_codec);
				break;
			case 253u:
				FightBGM = input.ReadSFixed32();
				break;
			case 261u:
				Voice = input.ReadSFixed32();
				break;
			case 266u:
				actions_.AddEntriesFrom(ref input, _repeated_actions_codec);
				break;
			case 277u:
				MoveVfxId = input.ReadSFixed32();
				break;
			case 285u:
				LongTextID = input.ReadSFixed32();
				break;
			case 293u:
				ShortTextID = input.ReadSFixed32();
				break;
			}
		}
	}

	public (string, bool) GetCharacter()
	{
		if (GameSettings.angelMode)
		{
			if (string.IsNullOrEmpty(SfwCharacter))
			{
				return (Character, false);
			}
			return (SfwCharacter, false);
		}
		return (Character, false);
	}

	public (string, bool) GetCharacterInGame()
	{
		if (GameSettings.angelMode)
		{
			if (string.IsNullOrEmpty(InGameCharacterSFW))
			{
				return GetCharacter();
			}
			return (InGameCharacterSFW, false);
		}
		if (string.IsNullOrEmpty(InGameCharacter))
		{
			return GetCharacter();
		}
		return (InGameCharacter, false);
	}

	public RepeatedField<string> GetBust()
	{
		if (SfwBust == null || SfwBust.Count == 0)
		{
			return Bust;
		}
		if (!GameSettings.angelMode)
		{
			return Bust;
		}
		return SfwBust;
	}

	public string GetCharacterReady()
	{
		if (string.IsNullOrEmpty(SfwCharacterReady))
		{
			return CharacterReady;
		}
		if (!GameSettings.angelMode)
		{
			return CharacterReady;
		}
		return SfwCharacterReady;
	}

	public string GetCharacterThin()
	{
		if (string.IsNullOrEmpty(SfwCharacterThin))
		{
			return CharacterThin;
		}
		if (!GameSettings.angelMode)
		{
			return CharacterThin;
		}
		return SfwCharacterThin;
	}

	public string GetSkillVideo()
	{
		if (string.IsNullOrEmpty(SfwSkillVideo))
		{
			return SkillVideo;
		}
		if (!GameSettings.angelMode)
		{
			return SkillVideo;
		}
		return SfwSkillVideo;
	}

	public string GetCharacterPhoto()
	{
		if (GameSettings.angelMode && !string.IsNullOrEmpty(SfwRolePhoto))
		{
			return SfwRolePhoto;
		}
		return RolePhoto;
	}

	public string GetCharacterLabel()
	{
		return CharacterLabel;
	}

	public string GetCharacterLevelUp()
	{
		return CharacterLevelUp;
	}

	public string GetBattlePlatform()
	{
		return FightBackGround;
	}
}
