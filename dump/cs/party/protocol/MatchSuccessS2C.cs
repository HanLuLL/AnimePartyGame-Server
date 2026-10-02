using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.code;
using party.model;

namespace party.protocol;

public sealed class MatchSuccessS2C : IMessage<MatchSuccessS2C>, IMessage, IEquatable<MatchSuccessS2C>, IDeepCloneable<MatchSuccessS2C>, IBufferMessage
{
	private static readonly MessageParser<MatchSuccessS2C> _parser = new MessageParser<MatchSuccessS2C>(() => new MatchSuccessS2C());

	private UnknownFieldSet _unknownFields;

	public const int CodeFieldNumber = 1;

	private Code code_;

	public const int RoomFieldNumber = 2;

	private Room room_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MatchSuccessS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[34];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Code Code
	{
		get
		{
			return code_;
		}
		set
		{
			code_ = value;
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
	public MatchSuccessS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchSuccessS2C(MatchSuccessS2C other)
		: this()
	{
		code_ = other.code_;
		room_ = ((other.room_ != null) ? other.room_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchSuccessS2C Clone()
	{
		return new MatchSuccessS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MatchSuccessS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MatchSuccessS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Code != other.Code)
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
		if (Code != Code.Succ)
		{
			num ^= Code.GetHashCode();
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
		if (Code != Code.Succ)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)Code);
		}
		if (room_ != null)
		{
			output.WriteRawTag(18);
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
		if (Code != Code.Succ)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Code);
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
	public void MergeFrom(MatchSuccessS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Code != Code.Succ)
		{
			Code = other.Code;
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
			case 8u:
				Code = (Code)input.ReadEnum();
				break;
			case 18u:
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
