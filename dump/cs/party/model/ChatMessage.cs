using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ChatMessage : IMessage<ChatMessage>, IMessage, IEquatable<ChatMessage>, IDeepCloneable<ChatMessage>, IBufferMessage
{
	private static readonly MessageParser<ChatMessage> _parser = new MessageParser<ChatMessage>(() => new ChatMessage());

	private UnknownFieldSet _unknownFields;

	public const int SenderIdFieldNumber = 1;

	private long senderId_;

	public const int ReceiverIdFieldNumber = 2;

	private long receiverId_;

	public const int MessageFieldNumber = 3;

	private string message_ = "";

	public const int TimeFieldNumber = 4;

	private long time_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChatMessage> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[98];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long SenderId
	{
		get
		{
			return senderId_;
		}
		set
		{
			senderId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ReceiverId
	{
		get
		{
			return receiverId_;
		}
		set
		{
			receiverId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Message
	{
		get
		{
			return message_;
		}
		set
		{
			message_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Time
	{
		get
		{
			return time_;
		}
		set
		{
			time_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatMessage()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatMessage(ChatMessage other)
		: this()
	{
		senderId_ = other.senderId_;
		receiverId_ = other.receiverId_;
		message_ = other.message_;
		time_ = other.time_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatMessage Clone()
	{
		return new ChatMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChatMessage);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChatMessage other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (SenderId != other.SenderId)
		{
			return false;
		}
		if (ReceiverId != other.ReceiverId)
		{
			return false;
		}
		if (Message != other.Message)
		{
			return false;
		}
		if (Time != other.Time)
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
		if (SenderId != 0L)
		{
			num ^= SenderId.GetHashCode();
		}
		if (ReceiverId != 0L)
		{
			num ^= ReceiverId.GetHashCode();
		}
		if (Message.Length != 0)
		{
			num ^= Message.GetHashCode();
		}
		if (Time != 0L)
		{
			num ^= Time.GetHashCode();
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
		if (SenderId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(SenderId);
		}
		if (ReceiverId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(ReceiverId);
		}
		if (Message.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Message);
		}
		if (Time != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(Time);
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
		if (SenderId != 0L)
		{
			num += 9;
		}
		if (ReceiverId != 0L)
		{
			num += 9;
		}
		if (Message.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Message);
		}
		if (Time != 0L)
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
	public void MergeFrom(ChatMessage other)
	{
		if (other != null)
		{
			if (other.SenderId != 0L)
			{
				SenderId = other.SenderId;
			}
			if (other.ReceiverId != 0L)
			{
				ReceiverId = other.ReceiverId;
			}
			if (other.Message.Length != 0)
			{
				Message = other.Message;
			}
			if (other.Time != 0L)
			{
				Time = other.Time;
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
				SenderId = input.ReadSFixed64();
				break;
			case 17u:
				ReceiverId = input.ReadSFixed64();
				break;
			case 26u:
				Message = input.ReadString();
				break;
			case 33u:
				Time = input.ReadSFixed64();
				break;
			}
		}
	}
}
