using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class RoomShortInfo : IMessage<RoomShortInfo>, IMessage, IEquatable<RoomShortInfo>, IDeepCloneable<RoomShortInfo>, IBufferMessage
{
	private static readonly MessageParser<RoomShortInfo> _parser = new MessageParser<RoomShortInfo>(() => new RoomShortInfo());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private long id_;

	public const int NameFieldNumber = 2;

	private string name_ = "";

	public const int MapIdFieldNumber = 3;

	private int mapId_;

	public const int MasterIdFieldNumber = 4;

	private long masterId_;

	public const int CreateTimeFieldNumber = 5;

	private long createTime_;

	public const int HeadIconFieldNumber = 6;

	private int headIcon_;

	public const int IsPwdFieldNumber = 7;

	private bool isPwd_;

	public const int PlayerCountFieldNumber = 8;

	private int playerCount_;

	public const int SteamLobbyIdFieldNumber = 10;

	private ulong steamLobbyId_;

	public const int UpgradePlanFieldNumber = 11;

	private int upgradePlan_;

	public const int TimePlanFieldNumber = 12;

	private int timePlan_;

	public const int WaitTimeFieldNumber = 13;

	private long waitTime_;

	public const int SpeedTypeFieldNumber = 14;

	private int speedType_;

	public const int MapTypeFieldNumber = 15;

	private int mapType_;

	public const int MasterBackBoardFieldNumber = 16;

	private int masterBackBoard_;

	public const int DifficultyFieldNumber = 17;

	private int difficulty_;

	public const int RoomLabelFieldNumber = 18;

	private int roomLabel_;

	public const int RoomServerIdFieldNumber = 19;

	private int roomServerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RoomShortInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[173];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
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
	public int MapId
	{
		get
		{
			return mapId_;
		}
		set
		{
			mapId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long MasterId
	{
		get
		{
			return masterId_;
		}
		set
		{
			masterId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long CreateTime
	{
		get
		{
			return createTime_;
		}
		set
		{
			createTime_ = value;
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
	public bool IsPwd
	{
		get
		{
			return isPwd_;
		}
		set
		{
			isPwd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PlayerCount
	{
		get
		{
			return playerCount_;
		}
		set
		{
			playerCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ulong SteamLobbyId
	{
		get
		{
			return steamLobbyId_;
		}
		set
		{
			steamLobbyId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UpgradePlan
	{
		get
		{
			return upgradePlan_;
		}
		set
		{
			upgradePlan_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TimePlan
	{
		get
		{
			return timePlan_;
		}
		set
		{
			timePlan_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long WaitTime
	{
		get
		{
			return waitTime_;
		}
		set
		{
			waitTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SpeedType
	{
		get
		{
			return speedType_;
		}
		set
		{
			speedType_ = value;
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
	public int MasterBackBoard
	{
		get
		{
			return masterBackBoard_;
		}
		set
		{
			masterBackBoard_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Difficulty
	{
		get
		{
			return difficulty_;
		}
		set
		{
			difficulty_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoomLabel
	{
		get
		{
			return roomLabel_;
		}
		set
		{
			roomLabel_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoomServerId
	{
		get
		{
			return roomServerId_;
		}
		set
		{
			roomServerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomShortInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomShortInfo(RoomShortInfo other)
		: this()
	{
		id_ = other.id_;
		name_ = other.name_;
		mapId_ = other.mapId_;
		masterId_ = other.masterId_;
		createTime_ = other.createTime_;
		headIcon_ = other.headIcon_;
		isPwd_ = other.isPwd_;
		playerCount_ = other.playerCount_;
		steamLobbyId_ = other.steamLobbyId_;
		upgradePlan_ = other.upgradePlan_;
		timePlan_ = other.timePlan_;
		waitTime_ = other.waitTime_;
		speedType_ = other.speedType_;
		mapType_ = other.mapType_;
		masterBackBoard_ = other.masterBackBoard_;
		difficulty_ = other.difficulty_;
		roomLabel_ = other.roomLabel_;
		roomServerId_ = other.roomServerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomShortInfo Clone()
	{
		return new RoomShortInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RoomShortInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RoomShortInfo other)
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
		if (Name != other.Name)
		{
			return false;
		}
		if (MapId != other.MapId)
		{
			return false;
		}
		if (MasterId != other.MasterId)
		{
			return false;
		}
		if (CreateTime != other.CreateTime)
		{
			return false;
		}
		if (HeadIcon != other.HeadIcon)
		{
			return false;
		}
		if (IsPwd != other.IsPwd)
		{
			return false;
		}
		if (PlayerCount != other.PlayerCount)
		{
			return false;
		}
		if (SteamLobbyId != other.SteamLobbyId)
		{
			return false;
		}
		if (UpgradePlan != other.UpgradePlan)
		{
			return false;
		}
		if (TimePlan != other.TimePlan)
		{
			return false;
		}
		if (WaitTime != other.WaitTime)
		{
			return false;
		}
		if (SpeedType != other.SpeedType)
		{
			return false;
		}
		if (MapType != other.MapType)
		{
			return false;
		}
		if (MasterBackBoard != other.MasterBackBoard)
		{
			return false;
		}
		if (Difficulty != other.Difficulty)
		{
			return false;
		}
		if (RoomLabel != other.RoomLabel)
		{
			return false;
		}
		if (RoomServerId != other.RoomServerId)
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
		if (Id != 0L)
		{
			num ^= Id.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		if (MasterId != 0L)
		{
			num ^= MasterId.GetHashCode();
		}
		if (CreateTime != 0L)
		{
			num ^= CreateTime.GetHashCode();
		}
		if (HeadIcon != 0)
		{
			num ^= HeadIcon.GetHashCode();
		}
		if (IsPwd)
		{
			num ^= IsPwd.GetHashCode();
		}
		if (PlayerCount != 0)
		{
			num ^= PlayerCount.GetHashCode();
		}
		if (SteamLobbyId != 0L)
		{
			num ^= SteamLobbyId.GetHashCode();
		}
		if (UpgradePlan != 0)
		{
			num ^= UpgradePlan.GetHashCode();
		}
		if (TimePlan != 0)
		{
			num ^= TimePlan.GetHashCode();
		}
		if (WaitTime != 0L)
		{
			num ^= WaitTime.GetHashCode();
		}
		if (SpeedType != 0)
		{
			num ^= SpeedType.GetHashCode();
		}
		if (MapType != 0)
		{
			num ^= MapType.GetHashCode();
		}
		if (MasterBackBoard != 0)
		{
			num ^= MasterBackBoard.GetHashCode();
		}
		if (Difficulty != 0)
		{
			num ^= Difficulty.GetHashCode();
		}
		if (RoomLabel != 0)
		{
			num ^= RoomLabel.GetHashCode();
		}
		if (RoomServerId != 0)
		{
			num ^= RoomServerId.GetHashCode();
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
		if (Id != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(Id);
		}
		if (Name.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Name);
		}
		if (MapId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(MapId);
		}
		if (MasterId != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(MasterId);
		}
		if (CreateTime != 0L)
		{
			output.WriteRawTag(41);
			output.WriteSFixed64(CreateTime);
		}
		if (HeadIcon != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(HeadIcon);
		}
		if (IsPwd)
		{
			output.WriteRawTag(56);
			output.WriteBool(IsPwd);
		}
		if (PlayerCount != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(PlayerCount);
		}
		if (SteamLobbyId != 0L)
		{
			output.WriteRawTag(81);
			output.WriteFixed64(SteamLobbyId);
		}
		if (UpgradePlan != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(UpgradePlan);
		}
		if (TimePlan != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(TimePlan);
		}
		if (WaitTime != 0L)
		{
			output.WriteRawTag(105);
			output.WriteSFixed64(WaitTime);
		}
		if (SpeedType != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(SpeedType);
		}
		if (MapType != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(MapType);
		}
		if (MasterBackBoard != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(MasterBackBoard);
		}
		if (Difficulty != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(Difficulty);
		}
		if (RoomLabel != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(RoomLabel);
		}
		if (RoomServerId != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(RoomServerId);
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
		if (Id != 0L)
		{
			num += 9;
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (MapId != 0)
		{
			num += 5;
		}
		if (MasterId != 0L)
		{
			num += 9;
		}
		if (CreateTime != 0L)
		{
			num += 9;
		}
		if (HeadIcon != 0)
		{
			num += 5;
		}
		if (IsPwd)
		{
			num += 2;
		}
		if (PlayerCount != 0)
		{
			num += 5;
		}
		if (SteamLobbyId != 0L)
		{
			num += 9;
		}
		if (UpgradePlan != 0)
		{
			num += 5;
		}
		if (TimePlan != 0)
		{
			num += 5;
		}
		if (WaitTime != 0L)
		{
			num += 9;
		}
		if (SpeedType != 0)
		{
			num += 5;
		}
		if (MapType != 0)
		{
			num += 5;
		}
		if (MasterBackBoard != 0)
		{
			num += 6;
		}
		if (Difficulty != 0)
		{
			num += 6;
		}
		if (RoomLabel != 0)
		{
			num += 6;
		}
		if (RoomServerId != 0)
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
	public void MergeFrom(RoomShortInfo other)
	{
		if (other != null)
		{
			if (other.Id != 0L)
			{
				Id = other.Id;
			}
			if (other.Name.Length != 0)
			{
				Name = other.Name;
			}
			if (other.MapId != 0)
			{
				MapId = other.MapId;
			}
			if (other.MasterId != 0L)
			{
				MasterId = other.MasterId;
			}
			if (other.CreateTime != 0L)
			{
				CreateTime = other.CreateTime;
			}
			if (other.HeadIcon != 0)
			{
				HeadIcon = other.HeadIcon;
			}
			if (other.IsPwd)
			{
				IsPwd = other.IsPwd;
			}
			if (other.PlayerCount != 0)
			{
				PlayerCount = other.PlayerCount;
			}
			if (other.SteamLobbyId != 0L)
			{
				SteamLobbyId = other.SteamLobbyId;
			}
			if (other.UpgradePlan != 0)
			{
				UpgradePlan = other.UpgradePlan;
			}
			if (other.TimePlan != 0)
			{
				TimePlan = other.TimePlan;
			}
			if (other.WaitTime != 0L)
			{
				WaitTime = other.WaitTime;
			}
			if (other.SpeedType != 0)
			{
				SpeedType = other.SpeedType;
			}
			if (other.MapType != 0)
			{
				MapType = other.MapType;
			}
			if (other.MasterBackBoard != 0)
			{
				MasterBackBoard = other.MasterBackBoard;
			}
			if (other.Difficulty != 0)
			{
				Difficulty = other.Difficulty;
			}
			if (other.RoomLabel != 0)
			{
				RoomLabel = other.RoomLabel;
			}
			if (other.RoomServerId != 0)
			{
				RoomServerId = other.RoomServerId;
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
				Id = input.ReadSFixed64();
				break;
			case 18u:
				Name = input.ReadString();
				break;
			case 29u:
				MapId = input.ReadSFixed32();
				break;
			case 33u:
				MasterId = input.ReadSFixed64();
				break;
			case 41u:
				CreateTime = input.ReadSFixed64();
				break;
			case 53u:
				HeadIcon = input.ReadSFixed32();
				break;
			case 56u:
				IsPwd = input.ReadBool();
				break;
			case 69u:
				PlayerCount = input.ReadSFixed32();
				break;
			case 81u:
				SteamLobbyId = input.ReadFixed64();
				break;
			case 93u:
				UpgradePlan = input.ReadSFixed32();
				break;
			case 101u:
				TimePlan = input.ReadSFixed32();
				break;
			case 105u:
				WaitTime = input.ReadSFixed64();
				break;
			case 117u:
				SpeedType = input.ReadSFixed32();
				break;
			case 125u:
				MapType = input.ReadSFixed32();
				break;
			case 133u:
				MasterBackBoard = input.ReadSFixed32();
				break;
			case 141u:
				Difficulty = input.ReadSFixed32();
				break;
			case 149u:
				RoomLabel = input.ReadSFixed32();
				break;
			case 157u:
				RoomServerId = input.ReadSFixed32();
				break;
			}
		}
	}
}
