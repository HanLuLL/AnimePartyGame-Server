using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ExitRoomS2C : IMessage<ExitRoomS2C>, IMessage, IEquatable<ExitRoomS2C>, IDeepCloneable<ExitRoomS2C>, IBufferMessage
{
	private static readonly MessageParser<ExitRoomS2C> _parser = new MessageParser<ExitRoomS2C>(() => new ExitRoomS2C());

	private UnknownFieldSet _unknownFields;

	public const int RoomIdFieldNumber = 1;

	private long roomId_;

	public const int MasterIdFieldNumber = 2;

	private long masterId_;

	public const int PlayerIdFieldNumber = 3;

	private long playerId_;

	public const int DissolveFieldNumber = 4;

	private bool dissolve_;

	public const int GameFinishFieldNumber = 5;

	private bool gameFinish_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExitRoomS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[152];

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
	public bool Dissolve
	{
		get
		{
			return dissolve_;
		}
		set
		{
			dissolve_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool GameFinish
	{
		get
		{
			return gameFinish_;
		}
		set
		{
			gameFinish_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitRoomS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitRoomS2C(ExitRoomS2C other)
		: this()
	{
		roomId_ = other.roomId_;
		masterId_ = other.masterId_;
		playerId_ = other.playerId_;
		dissolve_ = other.dissolve_;
		gameFinish_ = other.gameFinish_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitRoomS2C Clone()
	{
		return new ExitRoomS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExitRoomS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExitRoomS2C other)
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
		if (MasterId != other.MasterId)
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (Dissolve != other.Dissolve)
		{
			return false;
		}
		if (GameFinish != other.GameFinish)
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
		if (MasterId != 0L)
		{
			num ^= MasterId.GetHashCode();
		}
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (Dissolve)
		{
			num ^= Dissolve.GetHashCode();
		}
		if (GameFinish)
		{
			num ^= GameFinish.GetHashCode();
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
		if (RoomId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(RoomId);
		}
		if (MasterId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(MasterId);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(PlayerId);
		}
		if (Dissolve)
		{
			output.WriteRawTag(32);
			output.WriteBool(Dissolve);
		}
		if (GameFinish)
		{
			output.WriteRawTag(40);
			output.WriteBool(GameFinish);
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
		if (RoomId != 0L)
		{
			num += 9;
		}
		if (MasterId != 0L)
		{
			num += 9;
		}
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (Dissolve)
		{
			num += 2;
		}
		if (GameFinish)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ExitRoomS2C other)
	{
		if (other != null)
		{
			if (other.RoomId != 0L)
			{
				RoomId = other.RoomId;
			}
			if (other.MasterId != 0L)
			{
				MasterId = other.MasterId;
			}
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.Dissolve)
			{
				Dissolve = other.Dissolve;
			}
			if (other.GameFinish)
			{
				GameFinish = other.GameFinish;
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
				RoomId = input.ReadSFixed64();
				break;
			case 17u:
				MasterId = input.ReadSFixed64();
				break;
			case 25u:
				PlayerId = input.ReadSFixed64();
				break;
			case 32u:
				Dissolve = input.ReadBool();
				break;
			case 40u:
				GameFinish = input.ReadBool();
				break;
			}
		}
	}
}
