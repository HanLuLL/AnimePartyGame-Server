using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SteamSearchRoomS2C : IMessage<SteamSearchRoomS2C>, IMessage, IEquatable<SteamSearchRoomS2C>, IDeepCloneable<SteamSearchRoomS2C>, IBufferMessage
{
	private static readonly MessageParser<SteamSearchRoomS2C> _parser = new MessageParser<SteamSearchRoomS2C>(() => new SteamSearchRoomS2C());

	private UnknownFieldSet _unknownFields;

	public const int SteamLobbyIdFieldNumber = 1;

	private ulong steamLobbyId_;

	public const int RoomIdFieldNumber = 2;

	private long roomId_;

	public const int PlayerCountFieldNumber = 3;

	private int playerCount_;

	public const int PwdFieldNumber = 4;

	private string pwd_ = "";

	public const int StateFieldNumber = 5;

	private int state_;

	public const int RoomServerIdFieldNumber = 6;

	private int roomServerId_;

	public const int MapTypeFieldNumber = 7;

	private int mapType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SteamSearchRoomS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[38];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public int State
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
	public SteamSearchRoomS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SteamSearchRoomS2C(SteamSearchRoomS2C other)
		: this()
	{
		steamLobbyId_ = other.steamLobbyId_;
		roomId_ = other.roomId_;
		playerCount_ = other.playerCount_;
		pwd_ = other.pwd_;
		state_ = other.state_;
		roomServerId_ = other.roomServerId_;
		mapType_ = other.mapType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SteamSearchRoomS2C Clone()
	{
		return new SteamSearchRoomS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SteamSearchRoomS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SteamSearchRoomS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (SteamLobbyId != other.SteamLobbyId)
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
		if (Pwd != other.Pwd)
		{
			return false;
		}
		if (State != other.State)
		{
			return false;
		}
		if (RoomServerId != other.RoomServerId)
		{
			return false;
		}
		if (MapType != other.MapType)
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
		if (SteamLobbyId != 0L)
		{
			num ^= SteamLobbyId.GetHashCode();
		}
		if (RoomId != 0L)
		{
			num ^= RoomId.GetHashCode();
		}
		if (PlayerCount != 0)
		{
			num ^= PlayerCount.GetHashCode();
		}
		if (Pwd.Length != 0)
		{
			num ^= Pwd.GetHashCode();
		}
		if (State != 0)
		{
			num ^= State.GetHashCode();
		}
		if (RoomServerId != 0)
		{
			num ^= RoomServerId.GetHashCode();
		}
		if (MapType != 0)
		{
			num ^= MapType.GetHashCode();
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
		if (SteamLobbyId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteFixed64(SteamLobbyId);
		}
		if (RoomId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(RoomId);
		}
		if (PlayerCount != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(PlayerCount);
		}
		if (Pwd.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Pwd);
		}
		if (State != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(State);
		}
		if (RoomServerId != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(RoomServerId);
		}
		if (MapType != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(MapType);
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
		if (SteamLobbyId != 0L)
		{
			num += 9;
		}
		if (RoomId != 0L)
		{
			num += 9;
		}
		if (PlayerCount != 0)
		{
			num += 5;
		}
		if (Pwd.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Pwd);
		}
		if (State != 0)
		{
			num += 5;
		}
		if (RoomServerId != 0)
		{
			num += 5;
		}
		if (MapType != 0)
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
	public void MergeFrom(SteamSearchRoomS2C other)
	{
		if (other != null)
		{
			if (other.SteamLobbyId != 0L)
			{
				SteamLobbyId = other.SteamLobbyId;
			}
			if (other.RoomId != 0L)
			{
				RoomId = other.RoomId;
			}
			if (other.PlayerCount != 0)
			{
				PlayerCount = other.PlayerCount;
			}
			if (other.Pwd.Length != 0)
			{
				Pwd = other.Pwd;
			}
			if (other.State != 0)
			{
				State = other.State;
			}
			if (other.RoomServerId != 0)
			{
				RoomServerId = other.RoomServerId;
			}
			if (other.MapType != 0)
			{
				MapType = other.MapType;
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
				SteamLobbyId = input.ReadFixed64();
				break;
			case 17u:
				RoomId = input.ReadSFixed64();
				break;
			case 29u:
				PlayerCount = input.ReadSFixed32();
				break;
			case 34u:
				Pwd = input.ReadString();
				break;
			case 45u:
				State = input.ReadSFixed32();
				break;
			case 53u:
				RoomServerId = input.ReadSFixed32();
				break;
			case 61u:
				MapType = input.ReadSFixed32();
				break;
			}
		}
	}
}
