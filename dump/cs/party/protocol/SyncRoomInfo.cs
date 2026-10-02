using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SyncRoomInfo : IMessage<SyncRoomInfo>, IMessage, IEquatable<SyncRoomInfo>, IDeepCloneable<SyncRoomInfo>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum Oper
		{
			[OriginalName("NOOP")]
			Noop,
			[OriginalName("Insert")]
			Insert,
			[OriginalName("Delete")]
			Delete,
			[OriginalName("Update")]
			Update
		}
	}

	private static readonly MessageParser<SyncRoomInfo> _parser = new MessageParser<SyncRoomInfo>(() => new SyncRoomInfo());

	private UnknownFieldSet _unknownFields;

	public const int RoomIdFieldNumber = 1;

	private long roomId_;

	public const int OpFieldNumber = 2;

	private Types.Oper op_;

	public const int RoomFieldNumber = 3;

	private Room room_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SyncRoomInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[373];

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
	public Types.Oper Op
	{
		get
		{
			return op_;
		}
		set
		{
			op_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Room Room
	{
		get
		{
			return room_;
		}
		set
		{
			room_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncRoomInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncRoomInfo(SyncRoomInfo other)
		: this()
	{
		roomId_ = other.roomId_;
		op_ = other.op_;
		room_ = ((other.room_ != null) ? other.room_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncRoomInfo Clone()
	{
		return new SyncRoomInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SyncRoomInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SyncRoomInfo other)
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
		if (Op != other.Op)
		{
			return false;
		}
		if (!object.Equals(Room, other.Room))
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
		if (Op != Types.Oper.Noop)
		{
			num ^= Op.GetHashCode();
		}
		if (room_ != null)
		{
			num ^= Room.GetHashCode();
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
		if (Op != Types.Oper.Noop)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)Op);
		}
		if (room_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(Room);
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
		if (Op != Types.Oper.Noop)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Op);
		}
		if (room_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Room);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SyncRoomInfo other)
	{
		if (other == null)
		{
			return;
		}
		if (other.RoomId != 0L)
		{
			RoomId = other.RoomId;
		}
		if (other.Op != Types.Oper.Noop)
		{
			Op = other.Op;
		}
		if (other.room_ != null)
		{
			if (room_ == null)
			{
				Room = new Room();
			}
			Room.MergeFrom(other.Room);
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			case 16u:
				Op = (Types.Oper)input.ReadEnum();
				break;
			case 26u:
				if (room_ == null)
				{
					Room = new Room();
				}
				input.ReadMessage(Room);
				break;
			}
		}
	}
}
