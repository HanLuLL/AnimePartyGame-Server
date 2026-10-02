using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class GetChatMsgS2C : IMessage<GetChatMsgS2C>, IMessage, IEquatable<GetChatMsgS2C>, IDeepCloneable<GetChatMsgS2C>, IBufferMessage
{
	private static readonly MessageParser<GetChatMsgS2C> _parser = new MessageParser<GetChatMsgS2C>(() => new GetChatMsgS2C());

	private UnknownFieldSet _unknownFields;

	public const int TargetIdFieldNumber = 1;

	private long targetId_;

	public const int MessageFieldNumber = 2;

	private static readonly FieldCodec<ChatMessage> _repeated_message_codec = FieldCodec.ForMessage(18u, ChatMessage.Parser);

	private readonly RepeatedField<ChatMessage> message_ = new RepeatedField<ChatMessage>();

	public const int LastTimeFieldNumber = 3;

	private long lastTime_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GetChatMsgS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[89];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long TargetId
	{
		get
		{
			return targetId_;
		}
		set
		{
			targetId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ChatMessage> Message => message_;

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
	public GetChatMsgS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetChatMsgS2C(GetChatMsgS2C other)
		: this()
	{
		targetId_ = other.targetId_;
		message_ = other.message_.Clone();
		lastTime_ = other.lastTime_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetChatMsgS2C Clone()
	{
		return new GetChatMsgS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GetChatMsgS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GetChatMsgS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (TargetId != other.TargetId)
		{
			return false;
		}
		if (!message_.Equals(other.message_))
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
		if (TargetId != 0L)
		{
			num ^= TargetId.GetHashCode();
		}
		num ^= message_.GetHashCode();
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
		if (TargetId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(TargetId);
		}
		message_.WriteTo(ref output, _repeated_message_codec);
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
		if (TargetId != 0L)
		{
			num += 9;
		}
		num += message_.CalculateSize(_repeated_message_codec);
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
	public void MergeFrom(GetChatMsgS2C other)
	{
		if (other != null)
		{
			if (other.TargetId != 0L)
			{
				TargetId = other.TargetId;
			}
			message_.Add(other.message_);
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
			case 9u:
				TargetId = input.ReadSFixed64();
				break;
			case 18u:
				message_.AddEntriesFrom(ref input, _repeated_message_codec);
				break;
			case 25u:
				LastTime = input.ReadSFixed64();
				break;
			}
		}
	}
}
