using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class FriendInfo : IMessage<FriendInfo>, IMessage, IEquatable<FriendInfo>, IDeepCloneable<FriendInfo>, IBufferMessage
{
	private static readonly MessageParser<FriendInfo> _parser = new MessageParser<FriendInfo>(() => new FriendInfo());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int NameFieldNumber = 2;

	private string name_ = "";

	public const int RoomIdFieldNumber = 3;

	private long roomId_;

	public const int PlayerCountFieldNumber = 4;

	private int playerCount_;

	public const int StateFieldNumber = 5;

	private Room.Types.State state_;

	public const int OfflineTimeFieldNumber = 6;

	private long offlineTime_;

	public const int PwdFieldNumber = 7;

	private string pwd_ = "";

	public const int HeadIconFieldNumber = 8;

	private int headIcon_;

	public const int LvFieldNumber = 9;

	private int lv_;

	public const int BackgroundFieldNumber = 10;

	private int background_;

	public const int LoginTimeFieldNumber = 11;

	private long loginTime_;

	public const int IsOnlineFieldNumber = 12;

	private bool isOnline_;

	public const int RoomServerIdFieldNumber = 13;

	private int roomServerId_;

	public const int IsBusyFieldNumber = 14;

	private bool isBusy_;

	public const int SignalScoreFieldNumber = 15;

	private int signalScore_;

	public const int NoteFieldNumber = 16;

	private string note_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[65];

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
	public Room.Types.State State
	{
		get
		{
			return state_;
		}
		set
		{
			state_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long OfflineTime
	{
		get
		{
			return offlineTime_;
		}
		set
		{
			offlineTime_ = value;
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
	public long LoginTime
	{
		get
		{
			return loginTime_;
		}
		set
		{
			loginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsOnline
	{
		get
		{
			return isOnline_;
		}
		set
		{
			isOnline_ = value;
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
	public bool IsBusy
	{
		get
		{
			return isBusy_;
		}
		set
		{
			isBusy_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SignalScore
	{
		get
		{
			return signalScore_;
		}
		set
		{
			signalScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Note
	{
		get
		{
			return note_;
		}
		set
		{
			note_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInfo(FriendInfo other)
		: this()
	{
		playerId_ = other.playerId_;
		name_ = other.name_;
		roomId_ = other.roomId_;
		playerCount_ = other.playerCount_;
		state_ = other.state_;
		offlineTime_ = other.offlineTime_;
		pwd_ = other.pwd_;
		headIcon_ = other.headIcon_;
		lv_ = other.lv_;
		background_ = other.background_;
		loginTime_ = other.loginTime_;
		isOnline_ = other.isOnline_;
		roomServerId_ = other.roomServerId_;
		isBusy_ = other.isBusy_;
		signalScore_ = other.signalScore_;
		note_ = other.note_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInfo Clone()
	{
		return new FriendInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FriendInfo other)
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
		if (RoomId != other.RoomId)
		{
			return false;
		}
		if (PlayerCount != other.PlayerCount)
		{
			return false;
		}
		if (State != other.State)
		{
			return false;
		}
		if (OfflineTime != other.OfflineTime)
		{
			return false;
		}
		if (Pwd != other.Pwd)
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
		if (Background != other.Background)
		{
			return false;
		}
		if (LoginTime != other.LoginTime)
		{
			return false;
		}
		if (IsOnline != other.IsOnline)
		{
			return false;
		}
		if (RoomServerId != other.RoomServerId)
		{
			return false;
		}
		if (IsBusy != other.IsBusy)
		{
			return false;
		}
		if (SignalScore != other.SignalScore)
		{
			return false;
		}
		if (Note != other.Note)
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
		if (RoomId != 0L)
		{
			num ^= RoomId.GetHashCode();
		}
		if (PlayerCount != 0)
		{
			num ^= PlayerCount.GetHashCode();
		}
		if (State != Room.Types.State.None)
		{
			num ^= State.GetHashCode();
		}
		if (OfflineTime != 0L)
		{
			num ^= OfflineTime.GetHashCode();
		}
		if (Pwd.Length != 0)
		{
			num ^= Pwd.GetHashCode();
		}
		if (HeadIcon != 0)
		{
			num ^= HeadIcon.GetHashCode();
		}
		if (Lv != 0)
		{
			num ^= Lv.GetHashCode();
		}
		if (Background != 0)
		{
			num ^= Background.GetHashCode();
		}
		if (LoginTime != 0L)
		{
			num ^= LoginTime.GetHashCode();
		}
		if (IsOnline)
		{
			num ^= IsOnline.GetHashCode();
		}
		if (RoomServerId != 0)
		{
			num ^= RoomServerId.GetHashCode();
		}
		if (IsBusy)
		{
			num ^= IsBusy.GetHashCode();
		}
		if (SignalScore != 0)
		{
			num ^= SignalScore.GetHashCode();
		}
		if (Note.Length != 0)
		{
			num ^= Note.GetHashCode();
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
		if (RoomId != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(RoomId);
		}
		if (PlayerCount != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(PlayerCount);
		}
		if (State != Room.Types.State.None)
		{
			output.WriteRawTag(40);
			output.WriteEnum((int)State);
		}
		if (OfflineTime != 0L)
		{
			output.WriteRawTag(49);
			output.WriteSFixed64(OfflineTime);
		}
		if (Pwd.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(Pwd);
		}
		if (HeadIcon != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(HeadIcon);
		}
		if (Lv != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(Lv);
		}
		if (Background != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(Background);
		}
		if (LoginTime != 0L)
		{
			output.WriteRawTag(89);
			output.WriteSFixed64(LoginTime);
		}
		if (IsOnline)
		{
			output.WriteRawTag(96);
			output.WriteBool(IsOnline);
		}
		if (RoomServerId != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(RoomServerId);
		}
		if (IsBusy)
		{
			output.WriteRawTag(112);
			output.WriteBool(IsBusy);
		}
		if (SignalScore != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(SignalScore);
		}
		if (Note.Length != 0)
		{
			output.WriteRawTag(130, 1);
			output.WriteString(Note);
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
		if (RoomId != 0L)
		{
			num += 9;
		}
		if (PlayerCount != 0)
		{
			num += 5;
		}
		if (State != Room.Types.State.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)State);
		}
		if (OfflineTime != 0L)
		{
			num += 9;
		}
		if (Pwd.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Pwd);
		}
		if (HeadIcon != 0)
		{
			num += 5;
		}
		if (Lv != 0)
		{
			num += 5;
		}
		if (Background != 0)
		{
			num += 5;
		}
		if (LoginTime != 0L)
		{
			num += 9;
		}
		if (IsOnline)
		{
			num += 2;
		}
		if (RoomServerId != 0)
		{
			num += 5;
		}
		if (IsBusy)
		{
			num += 2;
		}
		if (SignalScore != 0)
		{
			num += 5;
		}
		if (Note.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Note);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendInfo other)
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
			if (other.RoomId != 0L)
			{
				RoomId = other.RoomId;
			}
			if (other.PlayerCount != 0)
			{
				PlayerCount = other.PlayerCount;
			}
			if (other.State != Room.Types.State.None)
			{
				State = other.State;
			}
			if (other.OfflineTime != 0L)
			{
				OfflineTime = other.OfflineTime;
			}
			if (other.Pwd.Length != 0)
			{
				Pwd = other.Pwd;
			}
			if (other.HeadIcon != 0)
			{
				HeadIcon = other.HeadIcon;
			}
			if (other.Lv != 0)
			{
				Lv = other.Lv;
			}
			if (other.Background != 0)
			{
				Background = other.Background;
			}
			if (other.LoginTime != 0L)
			{
				LoginTime = other.LoginTime;
			}
			if (other.IsOnline)
			{
				IsOnline = other.IsOnline;
			}
			if (other.RoomServerId != 0)
			{
				RoomServerId = other.RoomServerId;
			}
			if (other.IsBusy)
			{
				IsBusy = other.IsBusy;
			}
			if (other.SignalScore != 0)
			{
				SignalScore = other.SignalScore;
			}
			if (other.Note.Length != 0)
			{
				Note = other.Note;
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
			case 25u:
				RoomId = input.ReadSFixed64();
				break;
			case 37u:
				PlayerCount = input.ReadSFixed32();
				break;
			case 40u:
				State = (Room.Types.State)input.ReadEnum();
				break;
			case 49u:
				OfflineTime = input.ReadSFixed64();
				break;
			case 58u:
				Pwd = input.ReadString();
				break;
			case 69u:
				HeadIcon = input.ReadSFixed32();
				break;
			case 77u:
				Lv = input.ReadSFixed32();
				break;
			case 85u:
				Background = input.ReadSFixed32();
				break;
			case 89u:
				LoginTime = input.ReadSFixed64();
				break;
			case 96u:
				IsOnline = input.ReadBool();
				break;
			case 109u:
				RoomServerId = input.ReadSFixed32();
				break;
			case 112u:
				IsBusy = input.ReadBool();
				break;
			case 125u:
				SignalScore = input.ReadSFixed32();
				break;
			case 130u:
				Note = input.ReadString();
				break;
			}
		}
	}
}
