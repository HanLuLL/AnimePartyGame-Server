using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixINTJPSkinStandingPaintingConfigureItem : IMessage<FixINTJPSkinStandingPaintingConfigureItem>, IMessage, IEquatable<FixINTJPSkinStandingPaintingConfigureItem>, IDeepCloneable<FixINTJPSkinStandingPaintingConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<FixINTJPSkinStandingPaintingConfigureItem> _parser = new MessageParser<FixINTJPSkinStandingPaintingConfigureItem>(() => new FixINTJPSkinStandingPaintingConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int LandIconFieldNumber = 2;

	private string landIcon_ = "";

	public const int CharacterFieldNumber = 3;

	private string character_ = "";

	public const int SfwCharacterFieldNumber = 4;

	private string sfwCharacter_ = "";

	public const int CharacterLevelUpFieldNumber = 5;

	private string characterLevelUp_ = "";

	public const int CharacterBattlleResFieldNumber = 6;

	private int characterBattlleRes_;

	public const int CharacterLabelFieldNumber = 7;

	private string characterLabel_ = "";

	public const int ProfilePhotoFieldNumber = 8;

	private string profilePhoto_ = "";

	public const int BustFieldNumber = 9;

	private static readonly FieldCodec<string> _repeated_bust_codec = FieldCodec.ForString(74u);

	private readonly RepeatedField<string> bust_ = new RepeatedField<string>();

	public const int SfwBustFieldNumber = 10;

	private static readonly FieldCodec<string> _repeated_sfwBust_codec = FieldCodec.ForString(82u);

	private readonly RepeatedField<string> sfwBust_ = new RepeatedField<string>();

	public const int CharacterReadyFieldNumber = 11;

	private string characterReady_ = "";

	public const int SfwCharacterReadyFieldNumber = 12;

	private string sfwCharacterReady_ = "";

	public const int CharacterThinFieldNumber = 13;

	private string characterThin_ = "";

	public const int SfwCharacterThinFieldNumber = 14;

	private string sfwCharacterThin_ = "";

	public const int SkillVideoFieldNumber = 15;

	private string skillVideo_ = "";

	public const int SfwSkillVideoFieldNumber = 16;

	private string sfwSkillVideo_ = "";

	public const int RolePhotoFieldNumber = 17;

	private string rolePhoto_ = "";

	public const int SfwRolePhotoFieldNumber = 18;

	private string sfwRolePhoto_ = "";

	public const int PreviewVideoFieldNumber = 19;

	private string previewVideo_ = "";

	public const int ActionsFieldNumber = 20;

	private static readonly FieldCodec<string> _repeated_actions_codec = FieldCodec.ForString(162u);

	private readonly RepeatedField<string> actions_ = new RepeatedField<string>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixINTJPSkinStandingPaintingConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixINTJPReflection.Descriptor.MessageTypes[4];

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
	public RepeatedField<string> Actions => actions_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPSkinStandingPaintingConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPSkinStandingPaintingConfigureItem(FixINTJPSkinStandingPaintingConfigureItem other)
		: this()
	{
		index_ = other.index_;
		landIcon_ = other.landIcon_;
		character_ = other.character_;
		sfwCharacter_ = other.sfwCharacter_;
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
		previewVideo_ = other.previewVideo_;
		actions_ = other.actions_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPSkinStandingPaintingConfigureItem Clone()
	{
		return new FixINTJPSkinStandingPaintingConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixINTJPSkinStandingPaintingConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixINTJPSkinStandingPaintingConfigureItem other)
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
		if (PreviewVideo != other.PreviewVideo)
		{
			return false;
		}
		if (!actions_.Equals(other.actions_))
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
		if (PreviewVideo.Length != 0)
		{
			num ^= PreviewVideo.GetHashCode();
		}
		num ^= actions_.GetHashCode();
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
		if (LandIcon.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(LandIcon);
		}
		if (Character.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Character);
		}
		if (SfwCharacter.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(SfwCharacter);
		}
		if (CharacterLevelUp.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(CharacterLevelUp);
		}
		if (CharacterBattlleRes != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(CharacterBattlleRes);
		}
		if (CharacterLabel.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(CharacterLabel);
		}
		if (ProfilePhoto.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(ProfilePhoto);
		}
		bust_.WriteTo(ref output, _repeated_bust_codec);
		sfwBust_.WriteTo(ref output, _repeated_sfwBust_codec);
		if (CharacterReady.Length != 0)
		{
			output.WriteRawTag(90);
			output.WriteString(CharacterReady);
		}
		if (SfwCharacterReady.Length != 0)
		{
			output.WriteRawTag(98);
			output.WriteString(SfwCharacterReady);
		}
		if (CharacterThin.Length != 0)
		{
			output.WriteRawTag(106);
			output.WriteString(CharacterThin);
		}
		if (SfwCharacterThin.Length != 0)
		{
			output.WriteRawTag(114);
			output.WriteString(SfwCharacterThin);
		}
		if (SkillVideo.Length != 0)
		{
			output.WriteRawTag(122);
			output.WriteString(SkillVideo);
		}
		if (SfwSkillVideo.Length != 0)
		{
			output.WriteRawTag(130, 1);
			output.WriteString(SfwSkillVideo);
		}
		if (RolePhoto.Length != 0)
		{
			output.WriteRawTag(138, 1);
			output.WriteString(RolePhoto);
		}
		if (SfwRolePhoto.Length != 0)
		{
			output.WriteRawTag(146, 1);
			output.WriteString(SfwRolePhoto);
		}
		if (PreviewVideo.Length != 0)
		{
			output.WriteRawTag(154, 1);
			output.WriteString(PreviewVideo);
		}
		actions_.WriteTo(ref output, _repeated_actions_codec);
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
			num += 1 + CodedOutputStream.ComputeStringSize(CharacterReady);
		}
		if (SfwCharacterReady.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SfwCharacterReady);
		}
		if (CharacterThin.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CharacterThin);
		}
		if (SfwCharacterThin.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SfwCharacterThin);
		}
		if (SkillVideo.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SkillVideo);
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
		if (PreviewVideo.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(PreviewVideo);
		}
		num += actions_.CalculateSize(_repeated_actions_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixINTJPSkinStandingPaintingConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
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
			if (other.PreviewVideo.Length != 0)
			{
				PreviewVideo = other.PreviewVideo;
			}
			actions_.Add(other.actions_);
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
			case 18u:
				LandIcon = input.ReadString();
				break;
			case 26u:
				Character = input.ReadString();
				break;
			case 34u:
				SfwCharacter = input.ReadString();
				break;
			case 42u:
				CharacterLevelUp = input.ReadString();
				break;
			case 53u:
				CharacterBattlleRes = input.ReadSFixed32();
				break;
			case 58u:
				CharacterLabel = input.ReadString();
				break;
			case 66u:
				ProfilePhoto = input.ReadString();
				break;
			case 74u:
				bust_.AddEntriesFrom(ref input, _repeated_bust_codec);
				break;
			case 82u:
				sfwBust_.AddEntriesFrom(ref input, _repeated_sfwBust_codec);
				break;
			case 90u:
				CharacterReady = input.ReadString();
				break;
			case 98u:
				SfwCharacterReady = input.ReadString();
				break;
			case 106u:
				CharacterThin = input.ReadString();
				break;
			case 114u:
				SfwCharacterThin = input.ReadString();
				break;
			case 122u:
				SkillVideo = input.ReadString();
				break;
			case 130u:
				SfwSkillVideo = input.ReadString();
				break;
			case 138u:
				RolePhoto = input.ReadString();
				break;
			case 146u:
				SfwRolePhoto = input.ReadString();
				break;
			case 154u:
				PreviewVideo = input.ReadString();
				break;
			case 162u:
				actions_.AddEntriesFrom(ref input, _repeated_actions_codec);
				break;
			}
		}
	}
}
