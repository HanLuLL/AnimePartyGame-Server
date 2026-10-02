using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SimpleRoomInfo : IMessage<SimpleRoomInfo>, IMessage, IEquatable<SimpleRoomInfo>, IDeepCloneable<SimpleRoomInfo>, IBufferMessage
{
	private static readonly MessageParser<SimpleRoomInfo> _parser = new MessageParser<SimpleRoomInfo>(() => new SimpleRoomInfo());

	private UnknownFieldSet _unknownFields;

	public const int RoomIdFieldNumber = 1;

	private long roomId_;

	public const int RoomServerIdFieldNumber = 2;

	private int roomServerId_;

	public const int IsBusyFieldNumber = 3;

	private bool isBusy_;

	public const int RoomStatusFieldNumber = 4;

	private int roomStatus_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SimpleRoomInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[102];

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
	public int RoomStatus
	{
		get
		{
			return roomStatus_;
		}
		set
		{
			roomStatus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SimpleRoomInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SimpleRoomInfo(SimpleRoomInfo other)
		: this()
	{
		roomId_ = other.roomId_;
		roomServerId_ = other.roomServerId_;
		isBusy_ = other.isBusy_;
		roomStatus_ = other.roomStatus_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SimpleRoomInfo Clone()
	{
		return new SimpleRoomInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SimpleRoomInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SimpleRoomInfo other)
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
		if (RoomServerId != other.RoomServerId)
		{
			return false;
		}
		if (IsBusy != other.IsBusy)
		{
			return false;
		}
		if (RoomStatus != other.RoomStatus)
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
		if (RoomServerId != 0)
		{
			num ^= RoomServerId.GetHashCode();
		}
		if (IsBusy)
		{
			num ^= IsBusy.GetHashCode();
		}
		if (RoomStatus != 0)
		{
			num ^= RoomStatus.GetHashCode();
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
		if (RoomServerId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(RoomServerId);
		}
		if (IsBusy)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsBusy);
		}
		if (RoomStatus != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(RoomStatus);
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
		if (RoomServerId != 0)
		{
			num += 5;
		}
		if (IsBusy)
		{
			num += 2;
		}
		if (RoomStatus != 0)
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
	public void MergeFrom(SimpleRoomInfo other)
	{
		if (other != null)
		{
			if (other.RoomId != 0L)
			{
				RoomId = other.RoomId;
			}
			if (other.RoomServerId != 0)
			{
				RoomServerId = other.RoomServerId;
			}
			if (other.IsBusy)
			{
				IsBusy = other.IsBusy;
			}
			if (other.RoomStatus != 0)
			{
				RoomStatus = other.RoomStatus;
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
			case 21u:
				RoomServerId = input.ReadSFixed32();
				break;
			case 24u:
				IsBusy = input.ReadBool();
				break;
			case 37u:
				RoomStatus = input.ReadSFixed32();
				break;
			}
		}
	}
}
