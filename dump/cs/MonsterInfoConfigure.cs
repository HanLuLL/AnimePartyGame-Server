using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using GameLogic;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Tools;
using UnityEngine;

public sealed class MonsterInfoConfigure : IMessage<MonsterInfoConfigure>, IMessage, IEquatable<MonsterInfoConfigure>, IDeepCloneable<MonsterInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<MonsterInfoConfigure> _parser = new MessageParser<MonsterInfoConfigure>(() => new MonsterInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int OrderWeightFieldNumber = 2;

	private int orderWeight_;

	public const int NameIDFieldNumber = 3;

	private int nameID_;

	public const int NickIDFieldNumber = 4;

	private int nickID_;

	public const int CommentIdFieldNumber = 5;

	private int commentId_;

	public const int BiographyIDFieldNumber = 6;

	private int biographyID_;

	public const int IsGalleryShowFieldNumber = 7;

	private bool isGalleryShow_;

	public const int MechanismIDFieldNumber = 8;

	private int mechanismID_;

	public const int HeroTypeFieldNumber = 9;

	private CharacterType heroType_;

	public const int MonsterTypeFieldNumber = 10;

	private MonsterType monsterType_;

	public const int MonsterTypeRaceFieldNumber = 11;

	private static readonly FieldCodec<MonsterRaceType> _repeated_monsterTypeRace_codec = FieldCodec.ForEnum(90u, (MonsterRaceType x) => (int)x, (int x) => (MonsterRaceType)x);

	private readonly RepeatedField<MonsterRaceType> monsterTypeRace_ = new RepeatedField<MonsterRaceType>();

	public const int TagTypeFieldNumber = 12;

	private CharacterTagType tagType_;

	public const int GoldFieldNumber = 13;

	private int gold_;

	public const int BloodFieldNumber = 14;

	private int blood_;

	public const int AttackFieldNumber = 15;

	private int attack_;

	public const int DefenseFieldNumber = 16;

	private int defense_;

	public const int ActiveSkillFieldNumber = 17;

	private int activeSkill_;

	public const int PassiveSkillsFieldNumber = 18;

	private static readonly FieldCodec<int> _repeated_passiveSkills_codec = FieldCodec.ForSFixed32(146u);

	private readonly RepeatedField<int> passiveSkills_ = new RepeatedField<int>();

	public const int CharacterMapFieldNumber = 19;

	private string characterMap_ = "";

	public const int OffsetInMapFieldNumber = 20;

	private static readonly FieldCodec<int> _repeated_offsetInMap_codec = FieldCodec.ForSFixed32(162u);

	private readonly RepeatedField<int> offsetInMap_ = new RepeatedField<int>();

	public const int StandingPaintingFieldNumber = 21;

	private int standingPainting_;

	public const int CanCounterFieldNumber = 22;

	private bool canCounter_;

	private MonsterAttributeConfigure _MonsterAttributeConfigure;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MonsterInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MonsterReflection.Descriptor.MessageTypes[0];

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
	public MonsterType MonsterType
	{
		get
		{
			return monsterType_;
		}
		private set
		{
			monsterType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MonsterRaceType> MonsterTypeRace => monsterTypeRace_;

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
	public int Gold
	{
		get
		{
			return gold_;
		}
		private set
		{
			gold_ = value;
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
	public RepeatedField<int> PassiveSkills => passiveSkills_;

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
	public bool CanCounter
	{
		get
		{
			return canCounter_;
		}
		private set
		{
			canCounter_ = value;
		}
	}

	public MonsterAttributeConfigure MonsterAttributeConfigure
	{
		get
		{
			if (_MonsterAttributeConfigure == null && !StaticConfigure.Monster.AttributeDict.TryGetValue(id_, out _MonsterAttributeConfigure))
			{
				return null;
			}
			return _MonsterAttributeConfigure;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterInfoConfigure(MonsterInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		orderWeight_ = other.orderWeight_;
		nameID_ = other.nameID_;
		nickID_ = other.nickID_;
		commentId_ = other.commentId_;
		biographyID_ = other.biographyID_;
		isGalleryShow_ = other.isGalleryShow_;
		mechanismID_ = other.mechanismID_;
		heroType_ = other.heroType_;
		monsterType_ = other.monsterType_;
		monsterTypeRace_ = other.monsterTypeRace_.Clone();
		tagType_ = other.tagType_;
		gold_ = other.gold_;
		blood_ = other.blood_;
		attack_ = other.attack_;
		defense_ = other.defense_;
		activeSkill_ = other.activeSkill_;
		passiveSkills_ = other.passiveSkills_.Clone();
		characterMap_ = other.characterMap_;
		offsetInMap_ = other.offsetInMap_.Clone();
		standingPainting_ = other.standingPainting_;
		canCounter_ = other.canCounter_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterInfoConfigure Clone()
	{
		return new MonsterInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MonsterInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MonsterInfoConfigure other)
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
		if (HeroType != other.HeroType)
		{
			return false;
		}
		if (MonsterType != other.MonsterType)
		{
			return false;
		}
		if (!monsterTypeRace_.Equals(other.monsterTypeRace_))
		{
			return false;
		}
		if (TagType != other.TagType)
		{
			return false;
		}
		if (Gold != other.Gold)
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
		if (!passiveSkills_.Equals(other.passiveSkills_))
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
		if (StandingPainting != other.StandingPainting)
		{
			return false;
		}
		if (CanCounter != other.CanCounter)
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
		if (HeroType != CharacterType.None)
		{
			num ^= HeroType.GetHashCode();
		}
		if (MonsterType != MonsterType.None)
		{
			num ^= MonsterType.GetHashCode();
		}
		num ^= monsterTypeRace_.GetHashCode();
		if (TagType != CharacterTagType.None)
		{
			num ^= TagType.GetHashCode();
		}
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
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
		num ^= passiveSkills_.GetHashCode();
		if (CharacterMap.Length != 0)
		{
			num ^= CharacterMap.GetHashCode();
		}
		num ^= offsetInMap_.GetHashCode();
		if (StandingPainting != 0)
		{
			num ^= StandingPainting.GetHashCode();
		}
		if (CanCounter)
		{
			num ^= CanCounter.GetHashCode();
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
		if (NameID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(NameID);
		}
		if (NickID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(NickID);
		}
		if (CommentId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(CommentId);
		}
		if (BiographyID != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(BiographyID);
		}
		if (IsGalleryShow)
		{
			output.WriteRawTag(56);
			output.WriteBool(IsGalleryShow);
		}
		if (MechanismID != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(MechanismID);
		}
		if (HeroType != CharacterType.None)
		{
			output.WriteRawTag(72);
			output.WriteEnum((int)HeroType);
		}
		if (MonsterType != MonsterType.None)
		{
			output.WriteRawTag(80);
			output.WriteEnum((int)MonsterType);
		}
		monsterTypeRace_.WriteTo(ref output, _repeated_monsterTypeRace_codec);
		if (TagType != CharacterTagType.None)
		{
			output.WriteRawTag(96);
			output.WriteEnum((int)TagType);
		}
		if (Gold != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(Gold);
		}
		if (Blood != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(Blood);
		}
		if (Attack != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(Attack);
		}
		if (Defense != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(Defense);
		}
		if (ActiveSkill != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(ActiveSkill);
		}
		passiveSkills_.WriteTo(ref output, _repeated_passiveSkills_codec);
		if (CharacterMap.Length != 0)
		{
			output.WriteRawTag(154, 1);
			output.WriteString(CharacterMap);
		}
		offsetInMap_.WriteTo(ref output, _repeated_offsetInMap_codec);
		if (StandingPainting != 0)
		{
			output.WriteRawTag(173, 1);
			output.WriteSFixed32(StandingPainting);
		}
		if (CanCounter)
		{
			output.WriteRawTag(176, 1);
			output.WriteBool(CanCounter);
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
		if (HeroType != CharacterType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)HeroType);
		}
		if (MonsterType != MonsterType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MonsterType);
		}
		num += monsterTypeRace_.CalculateSize(_repeated_monsterTypeRace_codec);
		if (TagType != CharacterTagType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)TagType);
		}
		if (Gold != 0)
		{
			num += 5;
		}
		if (Blood != 0)
		{
			num += 5;
		}
		if (Attack != 0)
		{
			num += 5;
		}
		if (Defense != 0)
		{
			num += 6;
		}
		if (ActiveSkill != 0)
		{
			num += 6;
		}
		num += passiveSkills_.CalculateSize(_repeated_passiveSkills_codec);
		if (CharacterMap.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(CharacterMap);
		}
		num += offsetInMap_.CalculateSize(_repeated_offsetInMap_codec);
		if (StandingPainting != 0)
		{
			num += 6;
		}
		if (CanCounter)
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
	public void MergeFrom(MonsterInfoConfigure other)
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
			if (other.HeroType != CharacterType.None)
			{
				HeroType = other.HeroType;
			}
			if (other.MonsterType != MonsterType.None)
			{
				MonsterType = other.MonsterType;
			}
			monsterTypeRace_.Add(other.monsterTypeRace_);
			if (other.TagType != CharacterTagType.None)
			{
				TagType = other.TagType;
			}
			if (other.Gold != 0)
			{
				Gold = other.Gold;
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
			passiveSkills_.Add(other.passiveSkills_);
			if (other.CharacterMap.Length != 0)
			{
				CharacterMap = other.CharacterMap;
			}
			offsetInMap_.Add(other.offsetInMap_);
			if (other.StandingPainting != 0)
			{
				StandingPainting = other.StandingPainting;
			}
			if (other.CanCounter)
			{
				CanCounter = other.CanCounter;
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
			case 29u:
				NameID = input.ReadSFixed32();
				break;
			case 37u:
				NickID = input.ReadSFixed32();
				break;
			case 45u:
				CommentId = input.ReadSFixed32();
				break;
			case 53u:
				BiographyID = input.ReadSFixed32();
				break;
			case 56u:
				IsGalleryShow = input.ReadBool();
				break;
			case 69u:
				MechanismID = input.ReadSFixed32();
				break;
			case 72u:
				HeroType = (CharacterType)input.ReadEnum();
				break;
			case 80u:
				MonsterType = (MonsterType)input.ReadEnum();
				break;
			case 88u:
			case 90u:
				monsterTypeRace_.AddEntriesFrom(ref input, _repeated_monsterTypeRace_codec);
				break;
			case 96u:
				TagType = (CharacterTagType)input.ReadEnum();
				break;
			case 109u:
				Gold = input.ReadSFixed32();
				break;
			case 117u:
				Blood = input.ReadSFixed32();
				break;
			case 125u:
				Attack = input.ReadSFixed32();
				break;
			case 133u:
				Defense = input.ReadSFixed32();
				break;
			case 141u:
				ActiveSkill = input.ReadSFixed32();
				break;
			case 146u:
			case 149u:
				passiveSkills_.AddEntriesFrom(ref input, _repeated_passiveSkills_codec);
				break;
			case 154u:
				CharacterMap = input.ReadString();
				break;
			case 162u:
			case 165u:
				offsetInMap_.AddEntriesFrom(ref input, _repeated_offsetInMap_codec);
				break;
			case 173u:
				StandingPainting = input.ReadSFixed32();
				break;
			case 176u:
				CanCounter = input.ReadBool();
				break;
			}
		}
	}

	public MonsterAttributeConfigureItem GetMonsterAttributeConfigureByDifficulty(int difficulty)
	{
		MonsterAttributeConfigure monsterAttributeConfigure = MonsterAttributeConfigure;
		if (monsterAttributeConfigure != null)
		{
			int count = monsterAttributeConfigure.MonsterAttributeConfigureItems.Count;
			if (count > difficulty)
			{
				return monsterAttributeConfigure.MonsterAttributeConfigureItems.GetSafeByIndex(difficulty);
			}
			Debug.LogError($"current show difficultyType:{difficulty}, but AttributeConfigureItems.Count = {count}");
		}
		return null;
	}

	public int GetBattleActiveSkillId()
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room == null || !room.IsInRoom)
		{
			return ActiveSkill;
		}
		if (room.curRoomInfo.IsPVE() && MonsterAttributeConfigure != null)
		{
			foreach (MonsterAttributeConfigureItem monsterAttributeConfigureItem in MonsterAttributeConfigure.MonsterAttributeConfigureItems)
			{
				if (monsterAttributeConfigureItem.Index == room.curRoomInfo.Difficulty)
				{
					return monsterAttributeConfigureItem.PveActiveSkill;
				}
			}
		}
		return ActiveSkill;
	}

	public RepeatedField<int> GetBattlePassiveSkills()
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room == null || !room.IsInRoom)
		{
			return PassiveSkills;
		}
		if (room.curRoomInfo.IsPVE() && MonsterAttributeConfigure != null)
		{
			foreach (MonsterAttributeConfigureItem monsterAttributeConfigureItem in MonsterAttributeConfigure.MonsterAttributeConfigureItems)
			{
				if (monsterAttributeConfigureItem.Index == room.curRoomInfo.Difficulty)
				{
					return monsterAttributeConfigureItem.PvePassiveSkills;
				}
			}
		}
		return PassiveSkills;
	}
}
