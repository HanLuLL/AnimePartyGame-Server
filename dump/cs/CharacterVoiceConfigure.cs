using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class CharacterVoiceConfigure : IMessage<CharacterVoiceConfigure>, IMessage, IEquatable<CharacterVoiceConfigure>, IDeepCloneable<CharacterVoiceConfigure>, IBufferMessage
{
	private static readonly MessageParser<CharacterVoiceConfigure> _parser = new MessageParser<CharacterVoiceConfigure>(() => new CharacterVoiceConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int SoundBankFieldNumber = 2;

	private int soundBank_;

	public const int AchieveVoiceFieldNumber = 3;

	private int achieveVoice_;

	public const int AtkVoiceFieldNumber = 4;

	private int atkVoice_;

	public const int CardVoiceFieldNumber = 5;

	private int cardVoice_;

	public const int DefVoiceFieldNumber = 6;

	private int defVoice_;

	public const int EventVoiceFieldNumber = 7;

	private int eventVoice_;

	public const int KillVoiceFieldNumber = 8;

	private int killVoice_;

	public const int LvUpVoiceFieldNumber = 9;

	private int lvUpVoice_;

	public const int MoveVoiceFieldNumber = 10;

	private int moveVoice_;

	public const int SelectVoiceFieldNumber = 11;

	private int selectVoice_;

	public const int ShopVoiceFieldNumber = 12;

	private int shopVoice_;

	public const int SkillVoiceFieldNumber = 13;

	private int skillVoice_;

	public const int WinVoiceFieldNumber = 14;

	private int winVoice_;

	public const int AwakeVoiceFieldNumber = 15;

	private int awakeVoice_;

	public const int FanfareVoiceFieldNumber = 16;

	private int fanfareVoice_;

	public const int ShowVoiceFieldNumber = 17;

	private static readonly MapField<int, int>.Codec _map_showVoice_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 138u);

	private readonly MapField<int, int> showVoice_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CharacterVoiceConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CharacterReflection.Descriptor.MessageTypes[5];

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
	public int SoundBank
	{
		get
		{
			return soundBank_;
		}
		private set
		{
			soundBank_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AchieveVoice
	{
		get
		{
			return achieveVoice_;
		}
		private set
		{
			achieveVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AtkVoice
	{
		get
		{
			return atkVoice_;
		}
		private set
		{
			atkVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardVoice
	{
		get
		{
			return cardVoice_;
		}
		private set
		{
			cardVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DefVoice
	{
		get
		{
			return defVoice_;
		}
		private set
		{
			defVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EventVoice
	{
		get
		{
			return eventVoice_;
		}
		private set
		{
			eventVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int KillVoice
	{
		get
		{
			return killVoice_;
		}
		private set
		{
			killVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LvUpVoice
	{
		get
		{
			return lvUpVoice_;
		}
		private set
		{
			lvUpVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MoveVoice
	{
		get
		{
			return moveVoice_;
		}
		private set
		{
			moveVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SelectVoice
	{
		get
		{
			return selectVoice_;
		}
		private set
		{
			selectVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ShopVoice
	{
		get
		{
			return shopVoice_;
		}
		private set
		{
			shopVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkillVoice
	{
		get
		{
			return skillVoice_;
		}
		private set
		{
			skillVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WinVoice
	{
		get
		{
			return winVoice_;
		}
		private set
		{
			winVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AwakeVoice
	{
		get
		{
			return awakeVoice_;
		}
		private set
		{
			awakeVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FanfareVoice
	{
		get
		{
			return fanfareVoice_;
		}
		private set
		{
			fanfareVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> ShowVoice => showVoice_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterVoiceConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterVoiceConfigure(CharacterVoiceConfigure other)
		: this()
	{
		id_ = other.id_;
		soundBank_ = other.soundBank_;
		achieveVoice_ = other.achieveVoice_;
		atkVoice_ = other.atkVoice_;
		cardVoice_ = other.cardVoice_;
		defVoice_ = other.defVoice_;
		eventVoice_ = other.eventVoice_;
		killVoice_ = other.killVoice_;
		lvUpVoice_ = other.lvUpVoice_;
		moveVoice_ = other.moveVoice_;
		selectVoice_ = other.selectVoice_;
		shopVoice_ = other.shopVoice_;
		skillVoice_ = other.skillVoice_;
		winVoice_ = other.winVoice_;
		awakeVoice_ = other.awakeVoice_;
		fanfareVoice_ = other.fanfareVoice_;
		showVoice_ = other.showVoice_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterVoiceConfigure Clone()
	{
		return new CharacterVoiceConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CharacterVoiceConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CharacterVoiceConfigure other)
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
		if (SoundBank != other.SoundBank)
		{
			return false;
		}
		if (AchieveVoice != other.AchieveVoice)
		{
			return false;
		}
		if (AtkVoice != other.AtkVoice)
		{
			return false;
		}
		if (CardVoice != other.CardVoice)
		{
			return false;
		}
		if (DefVoice != other.DefVoice)
		{
			return false;
		}
		if (EventVoice != other.EventVoice)
		{
			return false;
		}
		if (KillVoice != other.KillVoice)
		{
			return false;
		}
		if (LvUpVoice != other.LvUpVoice)
		{
			return false;
		}
		if (MoveVoice != other.MoveVoice)
		{
			return false;
		}
		if (SelectVoice != other.SelectVoice)
		{
			return false;
		}
		if (ShopVoice != other.ShopVoice)
		{
			return false;
		}
		if (SkillVoice != other.SkillVoice)
		{
			return false;
		}
		if (WinVoice != other.WinVoice)
		{
			return false;
		}
		if (AwakeVoice != other.AwakeVoice)
		{
			return false;
		}
		if (FanfareVoice != other.FanfareVoice)
		{
			return false;
		}
		if (!ShowVoice.Equals(other.ShowVoice))
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
		if (SoundBank != 0)
		{
			num ^= SoundBank.GetHashCode();
		}
		if (AchieveVoice != 0)
		{
			num ^= AchieveVoice.GetHashCode();
		}
		if (AtkVoice != 0)
		{
			num ^= AtkVoice.GetHashCode();
		}
		if (CardVoice != 0)
		{
			num ^= CardVoice.GetHashCode();
		}
		if (DefVoice != 0)
		{
			num ^= DefVoice.GetHashCode();
		}
		if (EventVoice != 0)
		{
			num ^= EventVoice.GetHashCode();
		}
		if (KillVoice != 0)
		{
			num ^= KillVoice.GetHashCode();
		}
		if (LvUpVoice != 0)
		{
			num ^= LvUpVoice.GetHashCode();
		}
		if (MoveVoice != 0)
		{
			num ^= MoveVoice.GetHashCode();
		}
		if (SelectVoice != 0)
		{
			num ^= SelectVoice.GetHashCode();
		}
		if (ShopVoice != 0)
		{
			num ^= ShopVoice.GetHashCode();
		}
		if (SkillVoice != 0)
		{
			num ^= SkillVoice.GetHashCode();
		}
		if (WinVoice != 0)
		{
			num ^= WinVoice.GetHashCode();
		}
		if (AwakeVoice != 0)
		{
			num ^= AwakeVoice.GetHashCode();
		}
		if (FanfareVoice != 0)
		{
			num ^= FanfareVoice.GetHashCode();
		}
		num ^= ShowVoice.GetHashCode();
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
		if (SoundBank != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(SoundBank);
		}
		if (AchieveVoice != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(AchieveVoice);
		}
		if (AtkVoice != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(AtkVoice);
		}
		if (CardVoice != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(CardVoice);
		}
		if (DefVoice != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(DefVoice);
		}
		if (EventVoice != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(EventVoice);
		}
		if (KillVoice != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(KillVoice);
		}
		if (LvUpVoice != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(LvUpVoice);
		}
		if (MoveVoice != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(MoveVoice);
		}
		if (SelectVoice != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(SelectVoice);
		}
		if (ShopVoice != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(ShopVoice);
		}
		if (SkillVoice != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(SkillVoice);
		}
		if (WinVoice != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(WinVoice);
		}
		if (AwakeVoice != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(AwakeVoice);
		}
		if (FanfareVoice != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(FanfareVoice);
		}
		showVoice_.WriteTo(ref output, _map_showVoice_codec);
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
		if (SoundBank != 0)
		{
			num += 5;
		}
		if (AchieveVoice != 0)
		{
			num += 5;
		}
		if (AtkVoice != 0)
		{
			num += 5;
		}
		if (CardVoice != 0)
		{
			num += 5;
		}
		if (DefVoice != 0)
		{
			num += 5;
		}
		if (EventVoice != 0)
		{
			num += 5;
		}
		if (KillVoice != 0)
		{
			num += 5;
		}
		if (LvUpVoice != 0)
		{
			num += 5;
		}
		if (MoveVoice != 0)
		{
			num += 5;
		}
		if (SelectVoice != 0)
		{
			num += 5;
		}
		if (ShopVoice != 0)
		{
			num += 5;
		}
		if (SkillVoice != 0)
		{
			num += 5;
		}
		if (WinVoice != 0)
		{
			num += 5;
		}
		if (AwakeVoice != 0)
		{
			num += 5;
		}
		if (FanfareVoice != 0)
		{
			num += 6;
		}
		num += showVoice_.CalculateSize(_map_showVoice_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CharacterVoiceConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.SoundBank != 0)
			{
				SoundBank = other.SoundBank;
			}
			if (other.AchieveVoice != 0)
			{
				AchieveVoice = other.AchieveVoice;
			}
			if (other.AtkVoice != 0)
			{
				AtkVoice = other.AtkVoice;
			}
			if (other.CardVoice != 0)
			{
				CardVoice = other.CardVoice;
			}
			if (other.DefVoice != 0)
			{
				DefVoice = other.DefVoice;
			}
			if (other.EventVoice != 0)
			{
				EventVoice = other.EventVoice;
			}
			if (other.KillVoice != 0)
			{
				KillVoice = other.KillVoice;
			}
			if (other.LvUpVoice != 0)
			{
				LvUpVoice = other.LvUpVoice;
			}
			if (other.MoveVoice != 0)
			{
				MoveVoice = other.MoveVoice;
			}
			if (other.SelectVoice != 0)
			{
				SelectVoice = other.SelectVoice;
			}
			if (other.ShopVoice != 0)
			{
				ShopVoice = other.ShopVoice;
			}
			if (other.SkillVoice != 0)
			{
				SkillVoice = other.SkillVoice;
			}
			if (other.WinVoice != 0)
			{
				WinVoice = other.WinVoice;
			}
			if (other.AwakeVoice != 0)
			{
				AwakeVoice = other.AwakeVoice;
			}
			if (other.FanfareVoice != 0)
			{
				FanfareVoice = other.FanfareVoice;
			}
			showVoice_.MergeFrom(other.showVoice_);
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
				SoundBank = input.ReadSFixed32();
				break;
			case 29u:
				AchieveVoice = input.ReadSFixed32();
				break;
			case 37u:
				AtkVoice = input.ReadSFixed32();
				break;
			case 45u:
				CardVoice = input.ReadSFixed32();
				break;
			case 53u:
				DefVoice = input.ReadSFixed32();
				break;
			case 61u:
				EventVoice = input.ReadSFixed32();
				break;
			case 69u:
				KillVoice = input.ReadSFixed32();
				break;
			case 77u:
				LvUpVoice = input.ReadSFixed32();
				break;
			case 85u:
				MoveVoice = input.ReadSFixed32();
				break;
			case 93u:
				SelectVoice = input.ReadSFixed32();
				break;
			case 101u:
				ShopVoice = input.ReadSFixed32();
				break;
			case 109u:
				SkillVoice = input.ReadSFixed32();
				break;
			case 117u:
				WinVoice = input.ReadSFixed32();
				break;
			case 125u:
				AwakeVoice = input.ReadSFixed32();
				break;
			case 133u:
				FanfareVoice = input.ReadSFixed32();
				break;
			case 138u:
				showVoice_.AddEntriesFrom(ref input, _map_showVoice_codec);
				break;
			}
		}
	}
}
