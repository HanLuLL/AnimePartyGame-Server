using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SysSendGuildInvitationC2S : IMessage<SysSendGuildInvitationC2S>, IMessage, IEquatable<SysSendGuildInvitationC2S>, IDeepCloneable<SysSendGuildInvitationC2S>, IBufferMessage
{
	private static readonly MessageParser<SysSendGuildInvitationC2S> _parser = new MessageParser<SysSendGuildInvitationC2S>(() => new SysSendGuildInvitationC2S());

	private UnknownFieldSet _unknownFields;

	public const int GuildIdFieldNumber = 1;

	private long guildId_;

	public const int InviterIdFieldNumber = 2;

	private long inviterId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysSendGuildInvitationC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => InteriorReflection.Descriptor.MessageTypes[18];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long GuildId
	{
		get
		{
			return guildId_;
		}
		set
		{
			guildId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long InviterId
	{
		get
		{
			return inviterId_;
		}
		set
		{
			inviterId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSendGuildInvitationC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSendGuildInvitationC2S(SysSendGuildInvitationC2S other)
		: this()
	{
		guildId_ = other.guildId_;
		inviterId_ = other.inviterId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSendGuildInvitationC2S Clone()
	{
		return new SysSendGuildInvitationC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysSendGuildInvitationC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysSendGuildInvitationC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GuildId != other.GuildId)
		{
			return false;
		}
		if (InviterId != other.InviterId)
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
		if (GuildId != 0L)
		{
			num ^= GuildId.GetHashCode();
		}
		if (InviterId != 0L)
		{
			num ^= InviterId.GetHashCode();
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
		if (GuildId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(GuildId);
		}
		if (InviterId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(InviterId);
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
		if (GuildId != 0L)
		{
			num += 9;
		}
		if (InviterId != 0L)
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
	public void MergeFrom(SysSendGuildInvitationC2S other)
	{
		if (other != null)
		{
			if (other.GuildId != 0L)
			{
				GuildId = other.GuildId;
			}
			if (other.InviterId != 0L)
			{
				InviterId = other.InviterId;
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
				GuildId = input.ReadSFixed64();
				break;
			case 17u:
				InviterId = input.ReadSFixed64();
				break;
			}
		}
	}
}
