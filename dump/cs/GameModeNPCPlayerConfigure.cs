using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class GameModeNPCPlayerConfigure : IMessage<GameModeNPCPlayerConfigure>, IMessage, IEquatable<GameModeNPCPlayerConfigure>, IDeepCloneable<GameModeNPCPlayerConfigure>, IBufferMessage
{
	private static readonly MessageParser<GameModeNPCPlayerConfigure> _parser = new MessageParser<GameModeNPCPlayerConfigure>(() => new GameModeNPCPlayerConfigure());

	private UnknownFieldSet _unknownFields;

	public const int MapModeTypeFieldNumber = 1;

	private MapModeType mapModeType_;

	public const int MonsterIdFieldNumber = 2;

	private int monsterId_;

	public const int RoomPositionFieldNumber = 3;

	private int roomPosition_;

	public const int PlayerPhotoFieldNumber = 4;

	private string playerPhoto_ = "";

	public const int AccountBackgroundFieldNumber = 5;

	private string accountBackground_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GameModeNPCPlayerConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GameModeReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapModeType MapModeType
	{
		get
		{
			return mapModeType_;
		}
		private set
		{
			mapModeType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MonsterId
	{
		get
		{
			return monsterId_;
		}
		private set
		{
			monsterId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoomPosition
	{
		get
		{
			return roomPosition_;
		}
		private set
		{
			roomPosition_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PlayerPhoto
	{
		get
		{
			return playerPhoto_;
		}
		private set
		{
			playerPhoto_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AccountBackground
	{
		get
		{
			return accountBackground_;
		}
		private set
		{
			accountBackground_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeNPCPlayerConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeNPCPlayerConfigure(GameModeNPCPlayerConfigure other)
		: this()
	{
		mapModeType_ = other.mapModeType_;
		monsterId_ = other.monsterId_;
		roomPosition_ = other.roomPosition_;
		playerPhoto_ = other.playerPhoto_;
		accountBackground_ = other.accountBackground_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeNPCPlayerConfigure Clone()
	{
		return new GameModeNPCPlayerConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GameModeNPCPlayerConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GameModeNPCPlayerConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MapModeType != other.MapModeType)
		{
			return false;
		}
		if (MonsterId != other.MonsterId)
		{
			return false;
		}
		if (RoomPosition != other.RoomPosition)
		{
			return false;
		}
		if (PlayerPhoto != other.PlayerPhoto)
		{
			return false;
		}
		if (AccountBackground != other.AccountBackground)
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
		if (MapModeType != MapModeType.None)
		{
			num ^= MapModeType.GetHashCode();
		}
		if (MonsterId != 0)
		{
			num ^= MonsterId.GetHashCode();
		}
		if (RoomPosition != 0)
		{
			num ^= RoomPosition.GetHashCode();
		}
		if (PlayerPhoto.Length != 0)
		{
			num ^= PlayerPhoto.GetHashCode();
		}
		if (AccountBackground.Length != 0)
		{
			num ^= AccountBackground.GetHashCode();
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
		if (MapModeType != MapModeType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)MapModeType);
		}
		if (MonsterId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MonsterId);
		}
		if (RoomPosition != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(RoomPosition);
		}
		if (PlayerPhoto.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(PlayerPhoto);
		}
		if (AccountBackground.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(AccountBackground);
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
		if (MapModeType != MapModeType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MapModeType);
		}
		if (MonsterId != 0)
		{
			num += 5;
		}
		if (RoomPosition != 0)
		{
			num += 5;
		}
		if (PlayerPhoto.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PlayerPhoto);
		}
		if (AccountBackground.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccountBackground);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GameModeNPCPlayerConfigure other)
	{
		if (other != null)
		{
			if (other.MapModeType != MapModeType.None)
			{
				MapModeType = other.MapModeType;
			}
			if (other.MonsterId != 0)
			{
				MonsterId = other.MonsterId;
			}
			if (other.RoomPosition != 0)
			{
				RoomPosition = other.RoomPosition;
			}
			if (other.PlayerPhoto.Length != 0)
			{
				PlayerPhoto = other.PlayerPhoto;
			}
			if (other.AccountBackground.Length != 0)
			{
				AccountBackground = other.AccountBackground;
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
			case 8u:
				MapModeType = (MapModeType)input.ReadEnum();
				break;
			case 21u:
				MonsterId = input.ReadSFixed32();
				break;
			case 29u:
				RoomPosition = input.ReadSFixed32();
				break;
			case 34u:
				PlayerPhoto = input.ReadString();
				break;
			case 42u:
				AccountBackground = input.ReadString();
				break;
			}
		}
	}
}
