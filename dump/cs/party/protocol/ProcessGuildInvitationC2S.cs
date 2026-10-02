using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ProcessGuildInvitationC2S : IMessage<ProcessGuildInvitationC2S>, IMessage, IEquatable<ProcessGuildInvitationC2S>, IDeepCloneable<ProcessGuildInvitationC2S>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum Action
		{
			[OriginalName("Accept")]
			Accept,
			[OriginalName("Reject")]
			Reject
		}
	}

	private static readonly MessageParser<ProcessGuildInvitationC2S> _parser = new MessageParser<ProcessGuildInvitationC2S>(() => new ProcessGuildInvitationC2S());

	private UnknownFieldSet _unknownFields;

	public const int GuildIdsFieldNumber = 1;

	private static readonly FieldCodec<long> _repeated_guildIds_codec = FieldCodec.ForSFixed64(10u);

	private readonly RepeatedField<long> guildIds_ = new RepeatedField<long>();

	public const int ActionFieldNumber = 2;

	private Types.Action action_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ProcessGuildInvitationC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[571];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> GuildIds => guildIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.Action Action
	{
		get
		{
			return action_;
		}
		set
		{
			action_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProcessGuildInvitationC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProcessGuildInvitationC2S(ProcessGuildInvitationC2S other)
		: this()
	{
		guildIds_ = other.guildIds_.Clone();
		action_ = other.action_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProcessGuildInvitationC2S Clone()
	{
		return new ProcessGuildInvitationC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ProcessGuildInvitationC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ProcessGuildInvitationC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!guildIds_.Equals(other.guildIds_))
		{
			return false;
		}
		if (Action != other.Action)
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
		num ^= guildIds_.GetHashCode();
		if (Action != Types.Action.Accept)
		{
			num ^= Action.GetHashCode();
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
		guildIds_.WriteTo(ref output, _repeated_guildIds_codec);
		if (Action != Types.Action.Accept)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)Action);
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
		num += guildIds_.CalculateSize(_repeated_guildIds_codec);
		if (Action != Types.Action.Accept)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Action);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ProcessGuildInvitationC2S other)
	{
		if (other != null)
		{
			guildIds_.Add(other.guildIds_);
			if (other.Action != Types.Action.Accept)
			{
				Action = other.Action;
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
			case 10u:
				guildIds_.AddEntriesFrom(ref input, _repeated_guildIds_codec);
				break;
			case 16u:
				Action = (Types.Action)input.ReadEnum();
				break;
			}
		}
	}
}
