using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class FriendInviteInfo : IMessage<FriendInviteInfo>, IMessage, IEquatable<FriendInviteInfo>, IDeepCloneable<FriendInviteInfo>, IBufferMessage
{
	private static readonly MessageParser<FriendInviteInfo> _parser = new MessageParser<FriendInviteInfo>(() => new FriendInviteInfo());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int NameFieldNumber = 2;

	private string name_ = "";

	public const int HeadIconFieldNumber = 5;

	private int headIcon_;

	public const int LvFieldNumber = 6;

	private int lv_;

	public const int TimeFieldNumber = 7;

	private long time_;

	public const int BackgroundFieldNumber = 8;

	private int background_;

	public const int ValidFieldNumber = 9;

	private bool valid_;

	public const int RoomIdFieldNumber = 10;

	private long roomId_;

	public const int RoomNameFieldNumber = 11;

	private string roomName_ = "";

	public const int PwdFieldNumber = 12;

	private string pwd_ = "";

	public const int MapIdFieldNumber = 13;

	private int mapId_;

	public const int PlayerCountFieldNumber = 14;

	private int playerCount_;

	public const int RoomServerIdFieldNumber = 15;

	private int roomServerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendInviteInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[85];

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
	public long Time
	{
		get
		{
			return time_;
		}
		set
		{
			time_ = value;
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
	public bool Valid
	{
		get
		{
			return valid_;
		}
		set
		{
			valid_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long RoomId
	{
		get
		{
			return roomId_;
		}
		set
		{
			roomId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string RoomName
	{
		get
		{
			return roomName_;
		}
		set
		{
			roomName_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public FriendInviteInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteInfo(FriendInviteInfo other)
		: this()
	{
		playerId_ = other.playerId_;
		name_ = other.name_;
		headIcon_ = other.headIcon_;
		lv_ = other.lv_;
		time_ = other.time_;
		background_ = other.background_;
		valid_ = other.valid_;
		roomId_ = other.roomId_;
		roomName_ = other.roomName_;
		pwd_ = other.pwd_;
		mapId_ = other.mapId_;
		playerCount_ = other.playerCount_;
		roomServerId_ = other.roomServerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteInfo Clone()
	{
		return new FriendInviteInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendInviteInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FriendInviteInfo other)
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
		if (HeadIcon != other.HeadIcon)
		{
			return false;
		}
		if (Lv != other.Lv)
		{
			return false;
		}
		if (Time != other.Time)
		{
			return false;
		}
		if (Background != other.Background)
		{
			return false;
		}
		if (Valid != other.Valid)
		{
			return false;
		}
		if (RoomId != other.RoomId)
		{
			return false;
		}
		if (RoomName != other.RoomName)
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
		if (PlayerCount != other.PlayerCount)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (HeadIcon != 0)
		{
			num ^= HeadIcon.GetHashCode();
		}
		if (Lv != 0)
		{
			num ^= Lv.GetHashCode();
		}
		if (Time != 0L)
		{
			num ^= Time.GetHashCode();
		}
		if (Background != 0)
		{
			num ^= Background.GetHashCode();
		}
		if (Valid)
		{
			num ^= Valid.GetHashCode();
		}
		if (RoomId != 0L)
		{
			num ^= RoomId.GetHashCode();
		}
		if (RoomName.Length != 0)
		{
			num ^= RoomName.GetHashCode();
		}
		if (Pwd.Length != 0)
		{
			num ^= Pwd.GetHashCode();
		}
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		if (PlayerCount != 0)
		{
			num ^= PlayerCount.GetHashCode();
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
		if (HeadIcon != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(HeadIcon);
		}
		if (Lv != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Lv);
		}
		if (Time != 0L)
		{
			output.WriteRawTag(57);
			output.WriteSFixed64(Time);
		}
		if (Background != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(Background);
		}
		if (Valid)
		{
			output.WriteRawTag(72);
			output.WriteBool(Valid);
		}
		if (RoomId != 0L)
		{
			output.WriteRawTag(81);
			output.WriteSFixed64(RoomId);
		}
		if (RoomName.Length != 0)
		{
			output.WriteRawTag(90);
			output.WriteString(RoomName);
		}
		if (Pwd.Length != 0)
		{
			output.WriteRawTag(98);
			output.WriteString(Pwd);
		}
		if (MapId != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(MapId);
		}
		if (PlayerCount != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(PlayerCount);
		}
		if (RoomServerId != 0)
		{
			output.WriteRawTag(125);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (HeadIcon != 0)
		{
			num += 5;
		}
		if (Lv != 0)
		{
			num += 5;
		}
		if (Time != 0L)
		{
			num += 9;
		}
		if (Background != 0)
		{
			num += 5;
		}
		if (Valid)
		{
			num += 2;
		}
		if (RoomId != 0L)
		{
			num += 9;
		}
		if (RoomName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(RoomName);
		}
		if (Pwd.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Pwd);
		}
		if (MapId != 0)
		{
			num += 5;
		}
		if (PlayerCount != 0)
		{
			num += 5;
		}
		if (RoomServerId != 0)
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
	public void MergeFrom(FriendInviteInfo other)
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
			if (other.HeadIcon != 0)
			{
				HeadIcon = other.HeadIcon;
			}
			if (other.Lv != 0)
			{
				Lv = other.Lv;
			}
			if (other.Time != 0L)
			{
				Time = other.Time;
			}
			if (other.Background != 0)
			{
				Background = other.Background;
			}
			if (other.Valid)
			{
				Valid = other.Valid;
			}
			if (other.RoomId != 0L)
			{
				RoomId = other.RoomId;
			}
			if (other.RoomName.Length != 0)
			{
				RoomName = other.RoomName;
			}
			if (other.Pwd.Length != 0)
			{
				Pwd = other.Pwd;
			}
			if (other.MapId != 0)
			{
				MapId = other.MapId;
			}
			if (other.PlayerCount != 0)
			{
				PlayerCount = other.PlayerCount;
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
				PlayerId = input.ReadSFixed64();
				break;
			case 18u:
				Name = input.ReadString();
				break;
			case 45u:
				HeadIcon = input.ReadSFixed32();
				break;
			case 53u:
				Lv = input.ReadSFixed32();
				break;
			case 57u:
				Time = input.ReadSFixed64();
				break;
			case 69u:
				Background = input.ReadSFixed32();
				break;
			case 72u:
				Valid = input.ReadBool();
				break;
			case 81u:
				RoomId = input.ReadSFixed64();
				break;
			case 90u:
				RoomName = input.ReadString();
				break;
			case 98u:
				Pwd = input.ReadString();
				break;
			case 109u:
				MapId = input.ReadSFixed32();
				break;
			case 117u:
				PlayerCount = input.ReadSFixed32();
				break;
			case 125u:
				RoomServerId = input.ReadSFixed32();
				break;
			}
		}
	}
}
