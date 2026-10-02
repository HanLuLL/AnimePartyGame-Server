using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SysProcessGuildApplicationC2S : IMessage<SysProcessGuildApplicationC2S>, IMessage, IEquatable<SysProcessGuildApplicationC2S>, IDeepCloneable<SysProcessGuildApplicationC2S>, IBufferMessage
{
	private static readonly MessageParser<SysProcessGuildApplicationC2S> _parser = new MessageParser<SysProcessGuildApplicationC2S>(() => new SysProcessGuildApplicationC2S());

	private UnknownFieldSet _unknownFields;

	public const int GuildIdFieldNumber = 1;

	private long guildId_;

	public const int GuildNameFieldNumber = 2;

	private string guildName_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysProcessGuildApplicationC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => InteriorReflection.Descriptor.MessageTypes[16];

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
	public string GuildName
	{
		get
		{
			return guildName_;
		}
		set
		{
			guildName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysProcessGuildApplicationC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysProcessGuildApplicationC2S(SysProcessGuildApplicationC2S other)
		: this()
	{
		guildId_ = other.guildId_;
		guildName_ = other.guildName_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysProcessGuildApplicationC2S Clone()
	{
		return new SysProcessGuildApplicationC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysProcessGuildApplicationC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysProcessGuildApplicationC2S other)
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
		if (GuildName != other.GuildName)
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
		if (GuildName.Length != 0)
		{
			num ^= GuildName.GetHashCode();
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
		if (GuildName.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(GuildName);
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
		if (GuildName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(GuildName);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SysProcessGuildApplicationC2S other)
	{
		if (other != null)
		{
			if (other.GuildId != 0L)
			{
				GuildId = other.GuildId;
			}
			if (other.GuildName.Length != 0)
			{
				GuildName = other.GuildName;
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
			case 18u:
				GuildName = input.ReadString();
				break;
			}
		}
	}
}
