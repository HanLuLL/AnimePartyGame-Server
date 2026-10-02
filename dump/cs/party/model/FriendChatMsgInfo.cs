using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class FriendChatMsgInfo : IMessage<FriendChatMsgInfo>, IMessage, IEquatable<FriendChatMsgInfo>, IDeepCloneable<FriendChatMsgInfo>, IBufferMessage
{
	private static readonly MessageParser<FriendChatMsgInfo> _parser = new MessageParser<FriendChatMsgInfo>(() => new FriendChatMsgInfo());

	private UnknownFieldSet _unknownFields;

	public const int ReadTimeFieldNumber = 2;

	private long readTime_;

	public const int LastTimeFieldNumber = 3;

	private long lastTime_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendChatMsgInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[97];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ReadTime
	{
		get
		{
			return readTime_;
		}
		set
		{
			readTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastTime
	{
		get
		{
			return lastTime_;
		}
		set
		{
			lastTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendChatMsgInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendChatMsgInfo(FriendChatMsgInfo other)
		: this()
	{
		readTime_ = other.readTime_;
		lastTime_ = other.lastTime_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendChatMsgInfo Clone()
	{
		return new FriendChatMsgInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendChatMsgInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FriendChatMsgInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ReadTime != other.ReadTime)
		{
			return false;
		}
		if (LastTime != other.LastTime)
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
		if (ReadTime != 0L)
		{
			num ^= ReadTime.GetHashCode();
		}
		if (LastTime != 0L)
		{
			num ^= LastTime.GetHashCode();
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
		if (ReadTime != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(ReadTime);
		}
		if (LastTime != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(LastTime);
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
		if (ReadTime != 0L)
		{
			num += 9;
		}
		if (LastTime != 0L)
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
	public void MergeFrom(FriendChatMsgInfo other)
	{
		if (other != null)
		{
			if (other.ReadTime != 0L)
			{
				ReadTime = other.ReadTime;
			}
			if (other.LastTime != 0L)
			{
				LastTime = other.LastTime;
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
			case 17u:
				ReadTime = input.ReadSFixed64();
				break;
			case 25u:
				LastTime = input.ReadSFixed64();
				break;
			}
		}
	}
}
