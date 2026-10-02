using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class PlayerFightData : IMessage<PlayerFightData>, IMessage, IEquatable<PlayerFightData>, IDeepCloneable<PlayerFightData>, IBufferMessage
{
	private static readonly MessageParser<PlayerFightData> _parser = new MessageParser<PlayerFightData>(() => new PlayerFightData());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int NameFieldNumber = 2;

	private string name_ = "";

	public const int LvFieldNumber = 3;

	private int lv_;

	public const int GoldFieldNumber = 4;

	private int gold_;

	public const int HeroIdFieldNumber = 5;

	private int heroId_;

	public const int SlotFieldNumber = 6;

	private int slot_;

	public const int RankFieldNumber = 7;

	private int rank_;

	public const int MapTypeFieldNumber = 8;

	private int mapType_;

	public const int IsGiveUpFieldNumber = 9;

	private bool isGiveUp_;

	public const int HeadIconFieldNumber = 10;

	private int headIcon_;

	public const int BackgroundFieldNumber = 11;

	private int background_;

	public const int PlayerLevelFieldNumber = 12;

	private int playerLevel_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PlayerFightData> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[27];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Lv
	{
		get
		{
			return lv_;
		}
		set
		{
			lv_ = value;
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
	public int Slot
	{
		get
		{
			return slot_;
		}
		set
		{
			slot_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Rank
	{
		get
		{
			return rank_;
		}
		set
		{
			rank_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapType
	{
		get
		{
			return mapType_;
		}
		set
		{
			mapType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsGiveUp
	{
		get
		{
			return isGiveUp_;
		}
		set
		{
			isGiveUp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HeadIcon
	{
		get
		{
			return headIcon_;
		}
		set
		{
			headIcon_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Background
	{
		get
		{
			return background_;
		}
		set
		{
			background_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PlayerLevel
	{
		get
		{
			return playerLevel_;
		}
		set
		{
			playerLevel_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerFightData()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerFightData(PlayerFightData other)
		: this()
	{
		playerId_ = other.playerId_;
		name_ = other.name_;
		lv_ = other.lv_;
		gold_ = other.gold_;
		heroId_ = other.heroId_;
		slot_ = other.slot_;
		rank_ = other.rank_;
		mapType_ = other.mapType_;
		isGiveUp_ = other.isGiveUp_;
		headIcon_ = other.headIcon_;
		background_ = other.background_;
		playerLevel_ = other.playerLevel_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerFightData Clone()
	{
		return new PlayerFightData(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PlayerFightData);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PlayerFightData other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (Lv != other.Lv)
		{
			return false;
		}
		if (Gold != other.Gold)
		{
			return false;
		}
		if (HeroId != other.HeroId)
		{
			return false;
		}
		if (Slot != other.Slot)
		{
			return false;
		}
		if (Rank != other.Rank)
		{
			return false;
		}
		if (MapType != other.MapType)
		{
			return false;
		}
		if (IsGiveUp != other.IsGiveUp)
		{
			return false;
		}
		if (HeadIcon != other.HeadIcon)
		{
			return false;
		}
		if (Background != other.Background)
		{
			return false;
		}
		if (PlayerLevel != other.PlayerLevel)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (Lv != 0)
		{
			num ^= Lv.GetHashCode();
		}
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
		}
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
		}
		if (Slot != 0)
		{
			num ^= Slot.GetHashCode();
		}
		if (Rank != 0)
		{
			num ^= Rank.GetHashCode();
		}
		if (MapType != 0)
		{
			num ^= MapType.GetHashCode();
		}
		if (IsGiveUp)
		{
			num ^= IsGiveUp.GetHashCode();
		}
		if (HeadIcon != 0)
		{
			num ^= HeadIcon.GetHashCode();
		}
		if (Background != 0)
		{
			num ^= Background.GetHashCode();
		}
		if (PlayerLevel != 0)
		{
			num ^= PlayerLevel.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (Name.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Name);
		}
		if (Lv != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Lv);
		}
		if (Gold != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Gold);
		}
		if (HeroId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(HeroId);
		}
		if (Slot != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Slot);
		}
		if (Rank != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Rank);
		}
		if (MapType != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(MapType);
		}
		if (IsGiveUp)
		{
			output.WriteRawTag(72);
			output.WriteBool(IsGiveUp);
		}
		if (HeadIcon != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(HeadIcon);
		}
		if (Background != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(Background);
		}
		if (PlayerLevel != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(PlayerLevel);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (Lv != 0)
		{
			num += 5;
		}
		if (Gold != 0)
		{
			num += 5;
		}
		if (HeroId != 0)
		{
			num += 5;
		}
		if (Slot != 0)
		{
			num += 5;
		}
		if (Rank != 0)
		{
			num += 5;
		}
		if (MapType != 0)
		{
			num += 5;
		}
		if (IsGiveUp)
		{
			num += 2;
		}
		if (HeadIcon != 0)
		{
			num += 5;
		}
		if (Background != 0)
		{
			num += 5;
		}
		if (PlayerLevel != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PlayerFightData other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.Name.Length != 0)
			{
				Name = other.Name;
			}
			if (other.Lv != 0)
			{
				Lv = other.Lv;
			}
			if (other.Gold != 0)
			{
				Gold = other.Gold;
			}
			if (other.HeroId != 0)
			{
				HeroId = other.HeroId;
			}
			if (other.Slot != 0)
			{
				Slot = other.Slot;
			}
			if (other.Rank != 0)
			{
				Rank = other.Rank;
			}
			if (other.MapType != 0)
			{
				MapType = other.MapType;
			}
			if (other.IsGiveUp)
			{
				IsGiveUp = other.IsGiveUp;
			}
			if (other.HeadIcon != 0)
			{
				HeadIcon = other.HeadIcon;
			}
			if (other.Background != 0)
			{
				Background = other.Background;
			}
			if (other.PlayerLevel != 0)
			{
				PlayerLevel = other.PlayerLevel;
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 18u:
				Name = input.ReadString();
				break;
			case 29u:
				Lv = input.ReadSFixed32();
				break;
			case 37u:
				Gold = input.ReadSFixed32();
				break;
			case 45u:
				HeroId = input.ReadSFixed32();
				break;
			case 53u:
				Slot = input.ReadSFixed32();
				break;
			case 61u:
				Rank = input.ReadSFixed32();
				break;
			case 69u:
				MapType = input.ReadSFixed32();
				break;
			case 72u:
				IsGiveUp = input.ReadBool();
				break;
			case 85u:
				HeadIcon = input.ReadSFixed32();
				break;
			case 93u:
				Background = input.ReadSFixed32();
				break;
			case 101u:
				PlayerLevel = input.ReadSFixed32();
				break;
			}
		}
	}
}
