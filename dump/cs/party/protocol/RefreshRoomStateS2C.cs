using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class RefreshRoomStateS2C : IMessage<RefreshRoomStateS2C>, IMessage, IEquatable<RefreshRoomStateS2C>, IDeepCloneable<RefreshRoomStateS2C>, IBufferMessage
{
	private static readonly MessageParser<RefreshRoomStateS2C> _parser = new MessageParser<RefreshRoomStateS2C>(() => new RefreshRoomStateS2C());

	private UnknownFieldSet _unknownFields;

	public const int RoomIdFieldNumber = 1;

	private long roomId_;

	public const int PlayerIdFieldNumber = 2;

	private long playerId_;

	public const int ProgressFieldNumber = 3;

	private int progress_;

	public const int StateFieldNumber = 4;

	private Room.Types.State state_;

	public const int PlayerIdsFieldNumber = 5;

	private static readonly FieldCodec<long> _repeated_playerIds_codec = FieldCodec.ForSFixed64(42u);

	private readonly RepeatedField<long> playerIds_ = new RepeatedField<long>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RefreshRoomStateS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[166];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public int Progress
	{
		get
		{
			return progress_;
		}
		set
		{
			progress_ = value;
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
	public RepeatedField<long> PlayerIds => playerIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefreshRoomStateS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefreshRoomStateS2C(RefreshRoomStateS2C other)
		: this()
	{
		roomId_ = other.roomId_;
		playerId_ = other.playerId_;
		progress_ = other.progress_;
		state_ = other.state_;
		playerIds_ = other.playerIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefreshRoomStateS2C Clone()
	{
		return new RefreshRoomStateS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RefreshRoomStateS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RefreshRoomStateS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (RoomId != other.RoomId)
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (Progress != other.Progress)
		{
			return false;
		}
		if (State != other.State)
		{
			return false;
		}
		if (!playerIds_.Equals(other.playerIds_))
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
		if (RoomId != 0L)
		{
			num ^= RoomId.GetHashCode();
		}
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (Progress != 0)
		{
			num ^= Progress.GetHashCode();
		}
		if (State != Room.Types.State.None)
		{
			num ^= State.GetHashCode();
		}
		num ^= playerIds_.GetHashCode();
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
		if (RoomId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(RoomId);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(PlayerId);
		}
		if (Progress != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Progress);
		}
		if (State != Room.Types.State.None)
		{
			output.WriteRawTag(32);
			output.WriteEnum((int)State);
		}
		playerIds_.WriteTo(ref output, _repeated_playerIds_codec);
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
		if (RoomId != 0L)
		{
			num += 9;
		}
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (Progress != 0)
		{
			num += 5;
		}
		if (State != Room.Types.State.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)State);
		}
		num += playerIds_.CalculateSize(_repeated_playerIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RefreshRoomStateS2C other)
	{
		if (other != null)
		{
			if (other.RoomId != 0L)
			{
				RoomId = other.RoomId;
			}
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.Progress != 0)
			{
				Progress = other.Progress;
			}
			if (other.State != Room.Types.State.None)
			{
				State = other.State;
			}
			playerIds_.Add(other.playerIds_);
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
				RoomId = input.ReadSFixed64();
				break;
			case 17u:
				PlayerId = input.ReadSFixed64();
				break;
			case 29u:
				Progress = input.ReadSFixed32();
				break;
			case 32u:
				State = (Room.Types.State)input.ReadEnum();
				break;
			case 41u:
			case 42u:
				playerIds_.AddEntriesFrom(ref input, _repeated_playerIds_codec);
				break;
			}
		}
	}
}
