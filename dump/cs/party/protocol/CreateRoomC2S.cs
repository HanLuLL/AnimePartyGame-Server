using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class CreateRoomC2S : IMessage<CreateRoomC2S>, IMessage, IEquatable<CreateRoomC2S>, IDeepCloneable<CreateRoomC2S>, IBufferMessage
{
	private static readonly MessageParser<CreateRoomC2S> _parser = new MessageParser<CreateRoomC2S>(() => new CreateRoomC2S());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private long id_;

	public const int NameFieldNumber = 2;

	private string name_ = "";

	public const int PwdFieldNumber = 3;

	private string pwd_ = "";

	public const int MapIdFieldNumber = 4;

	private int mapId_;

	public const int MaxTimeFieldNumber = 5;

	private int maxTime_;

	public const int UpgradePlanFieldNumber = 6;

	private int upgradePlan_;

	public const int TimePlanFieldNumber = 7;

	private int timePlan_;

	public const int ModeFieldNumber = 8;

	private int mode_;

	public const int LobbyIdFieldNumber = 9;

	private ulong lobbyId_;

	public const int SpeedTypeFieldNumber = 10;

	private int speedType_;

	public const int DifficultyFieldNumber = 11;

	private int difficulty_;

	public const int SkipStoryFieldNumber = 12;

	private bool skipStory_;

	public const int RoomLabelFieldNumber = 13;

	private int roomLabel_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CreateRoomC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[29];

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
	public string Pwd
	{
		get
		{
			return pwd_;
		}
		set
		{
			pwd_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public int MaxTime
	{
		get
		{
			return maxTime_;
		}
		set
		{
			maxTime_ = value;
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
	public int Mode
	{
		get
		{
			return mode_;
		}
		set
		{
			mode_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ulong LobbyId
	{
		get
		{
			return lobbyId_;
		}
		set
		{
			lobbyId_ = value;
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
	public bool SkipStory
	{
		get
		{
			return skipStory_;
		}
		set
		{
			skipStory_ = value;
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
	public CreateRoomC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateRoomC2S(CreateRoomC2S other)
		: this()
	{
		id_ = other.id_;
		name_ = other.name_;
		pwd_ = other.pwd_;
		mapId_ = other.mapId_;
		maxTime_ = other.maxTime_;
		upgradePlan_ = other.upgradePlan_;
		timePlan_ = other.timePlan_;
		mode_ = other.mode_;
		lobbyId_ = other.lobbyId_;
		speedType_ = other.speedType_;
		difficulty_ = other.difficulty_;
		skipStory_ = other.skipStory_;
		roomLabel_ = other.roomLabel_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateRoomC2S Clone()
	{
		return new CreateRoomC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CreateRoomC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CreateRoomC2S other)
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
		if (Pwd != other.Pwd)
		{
			return false;
		}
		if (MapId != other.MapId)
		{
			return false;
		}
		if (MaxTime != other.MaxTime)
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
		if (Mode != other.Mode)
		{
			return false;
		}
		if (LobbyId != other.LobbyId)
		{
			return false;
		}
		if (SpeedType != other.SpeedType)
		{
			return false;
		}
		if (Difficulty != other.Difficulty)
		{
			return false;
		}
		if (SkipStory != other.SkipStory)
		{
			return false;
		}
		if (RoomLabel != other.RoomLabel)
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
		if (Pwd.Length != 0)
		{
			num ^= Pwd.GetHashCode();
		}
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		if (MaxTime != 0)
		{
			num ^= MaxTime.GetHashCode();
		}
		if (UpgradePlan != 0)
		{
			num ^= UpgradePlan.GetHashCode();
		}
		if (TimePlan != 0)
		{
			num ^= TimePlan.GetHashCode();
		}
		if (Mode != 0)
		{
			num ^= Mode.GetHashCode();
		}
		if (LobbyId != 0L)
		{
			num ^= LobbyId.GetHashCode();
		}
		if (SpeedType != 0)
		{
			num ^= SpeedType.GetHashCode();
		}
		if (Difficulty != 0)
		{
			num ^= Difficulty.GetHashCode();
		}
		if (SkipStory)
		{
			num ^= SkipStory.GetHashCode();
		}
		if (RoomLabel != 0)
		{
			num ^= RoomLabel.GetHashCode();
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
		if (Pwd.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Pwd);
		}
		if (MapId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(MapId);
		}
		if (MaxTime != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(MaxTime);
		}
		if (UpgradePlan != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(UpgradePlan);
		}
		if (TimePlan != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(TimePlan);
		}
		if (Mode != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(Mode);
		}
		if (LobbyId != 0L)
		{
			output.WriteRawTag(73);
			output.WriteFixed64(LobbyId);
		}
		if (SpeedType != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(SpeedType);
		}
		if (Difficulty != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(Difficulty);
		}
		if (SkipStory)
		{
			output.WriteRawTag(96);
			output.WriteBool(SkipStory);
		}
		if (RoomLabel != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(RoomLabel);
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
		if (Pwd.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Pwd);
		}
		if (MapId != 0)
		{
			num += 5;
		}
		if (MaxTime != 0)
		{
			num += 5;
		}
		if (UpgradePlan != 0)
		{
			num += 5;
		}
		if (TimePlan != 0)
		{
			num += 5;
		}
		if (Mode != 0)
		{
			num += 5;
		}
		if (LobbyId != 0L)
		{
			num += 9;
		}
		if (SpeedType != 0)
		{
			num += 5;
		}
		if (Difficulty != 0)
		{
			num += 5;
		}
		if (SkipStory)
		{
			num += 2;
		}
		if (RoomLabel != 0)
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
	public void MergeFrom(CreateRoomC2S other)
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
			if (other.Pwd.Length != 0)
			{
				Pwd = other.Pwd;
			}
			if (other.MapId != 0)
			{
				MapId = other.MapId;
			}
			if (other.MaxTime != 0)
			{
				MaxTime = other.MaxTime;
			}
			if (other.UpgradePlan != 0)
			{
				UpgradePlan = other.UpgradePlan;
			}
			if (other.TimePlan != 0)
			{
				TimePlan = other.TimePlan;
			}
			if (other.Mode != 0)
			{
				Mode = other.Mode;
			}
			if (other.LobbyId != 0L)
			{
				LobbyId = other.LobbyId;
			}
			if (other.SpeedType != 0)
			{
				SpeedType = other.SpeedType;
			}
			if (other.Difficulty != 0)
			{
				Difficulty = other.Difficulty;
			}
			if (other.SkipStory)
			{
				SkipStory = other.SkipStory;
			}
			if (other.RoomLabel != 0)
			{
				RoomLabel = other.RoomLabel;
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
			case 26u:
				Pwd = input.ReadString();
				break;
			case 37u:
				MapId = input.ReadSFixed32();
				break;
			case 45u:
				MaxTime = input.ReadSFixed32();
				break;
			case 53u:
				UpgradePlan = input.ReadSFixed32();
				break;
			case 61u:
				TimePlan = input.ReadSFixed32();
				break;
			case 69u:
				Mode = input.ReadSFixed32();
				break;
			case 73u:
				LobbyId = input.ReadFixed64();
				break;
			case 85u:
				SpeedType = input.ReadSFixed32();
				break;
			case 93u:
				Difficulty = input.ReadSFixed32();
				break;
			case 96u:
				SkipStory = input.ReadBool();
				break;
			case 109u:
				RoomLabel = input.ReadSFixed32();
				break;
			}
		}
	}
}
