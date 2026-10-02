using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class GetGuildMemberChangeMsgS2C : IMessage<GetGuildMemberChangeMsgS2C>, IMessage, IEquatable<GetGuildMemberChangeMsgS2C>, IDeepCloneable<GetGuildMemberChangeMsgS2C>, IBufferMessage
{
	private static readonly MessageParser<GetGuildMemberChangeMsgS2C> _parser = new MessageParser<GetGuildMemberChangeMsgS2C>(() => new GetGuildMemberChangeMsgS2C());

	private UnknownFieldSet _unknownFields;

	public const int MessagesFieldNumber = 1;

	private static readonly FieldCodec<GuildMemberChangeMsg> _repeated_messages_codec = FieldCodec.ForMessage(10u, GuildMemberChangeMsg.Parser);

	private readonly RepeatedField<GuildMemberChangeMsg> messages_ = new RepeatedField<GuildMemberChangeMsg>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GetGuildMemberChangeMsgS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[594];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GuildMemberChangeMsg> Messages => messages_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildMemberChangeMsgS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildMemberChangeMsgS2C(GetGuildMemberChangeMsgS2C other)
		: this()
	{
		messages_ = other.messages_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildMemberChangeMsgS2C Clone()
	{
		return new GetGuildMemberChangeMsgS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GetGuildMemberChangeMsgS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GetGuildMemberChangeMsgS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!messages_.Equals(other.messages_))
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
		num ^= messages_.GetHashCode();
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
		messages_.WriteTo(ref output, _repeated_messages_codec);
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
		num += messages_.CalculateSize(_repeated_messages_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GetGuildMemberChangeMsgS2C other)
	{
		if (other != null)
		{
			messages_.Add(other.messages_);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				messages_.AddEntriesFrom(ref input, _repeated_messages_codec);
			}
		}
	}
}
