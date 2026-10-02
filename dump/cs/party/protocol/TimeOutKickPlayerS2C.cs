using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class TimeOutKickPlayerS2C : IMessage<TimeOutKickPlayerS2C>, IMessage, IEquatable<TimeOutKickPlayerS2C>, IDeepCloneable<TimeOutKickPlayerS2C>, IBufferMessage
{
	private static readonly MessageParser<TimeOutKickPlayerS2C> _parser = new MessageParser<TimeOutKickPlayerS2C>(() => new TimeOutKickPlayerS2C());

	private UnknownFieldSet _unknownFields;

	public const int IsExitFieldNumber = 1;

	private bool isExit_;

	public const int RoomIdFieldNumber = 2;

	private long roomId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TimeOutKickPlayerS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[375];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsExit
	{
		get
		{
			return isExit_;
		}
		set
		{
			isExit_ = value;
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
	public TimeOutKickPlayerS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TimeOutKickPlayerS2C(TimeOutKickPlayerS2C other)
		: this()
	{
		isExit_ = other.isExit_;
		roomId_ = other.roomId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TimeOutKickPlayerS2C Clone()
	{
		return new TimeOutKickPlayerS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TimeOutKickPlayerS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TimeOutKickPlayerS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (IsExit != other.IsExit)
		{
			return false;
		}
		if (RoomId != other.RoomId)
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
		if (IsExit)
		{
			num ^= IsExit.GetHashCode();
		}
		if (RoomId != 0L)
		{
			num ^= RoomId.GetHashCode();
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
		if (IsExit)
		{
			output.WriteRawTag(8);
			output.WriteBool(IsExit);
		}
		if (RoomId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(RoomId);
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
		if (IsExit)
		{
			num += 2;
		}
		if (RoomId != 0L)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(TimeOutKickPlayerS2C other)
	{
		if (other != null)
		{
			if (other.IsExit)
			{
				IsExit = other.IsExit;
			}
			if (other.RoomId != 0L)
			{
				RoomId = other.RoomId;
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
				IsExit = input.ReadBool();
				break;
			case 17u:
				RoomId = input.ReadSFixed64();
				break;
			}
		}
	}
}
