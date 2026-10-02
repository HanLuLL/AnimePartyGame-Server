using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SysExitGuildC2S : IMessage<SysExitGuildC2S>, IMessage, IEquatable<SysExitGuildC2S>, IDeepCloneable<SysExitGuildC2S>, IBufferMessage
{
	private static readonly MessageParser<SysExitGuildC2S> _parser = new MessageParser<SysExitGuildC2S>(() => new SysExitGuildC2S());

	private UnknownFieldSet _unknownFields;

	public const int GuildIdFieldNumber = 1;

	private long guildId_;

	public const int GuildNameFieldNumber = 2;

	private string guildName_ = "";

	public const int PlayerIdFieldNumber = 3;

	private long playerId_;

	public const int PlayerNameFieldNumber = 4;

	private string playerName_ = "";

	public const int ExitTypeFieldNumber = 5;

	private int exitType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysExitGuildC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => InteriorReflection.Descriptor.MessageTypes[20];

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
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PlayerName
	{
		get
		{
			return playerName_;
		}
		set
		{
			playerName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ExitType
	{
		get
		{
			return exitType_;
		}
		set
		{
			exitType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysExitGuildC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysExitGuildC2S(SysExitGuildC2S other)
		: this()
	{
		guildId_ = other.guildId_;
		guildName_ = other.guildName_;
		playerId_ = other.playerId_;
		playerName_ = other.playerName_;
		exitType_ = other.exitType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysExitGuildC2S Clone()
	{
		return new SysExitGuildC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysExitGuildC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysExitGuildC2S other)
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
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (PlayerName != other.PlayerName)
		{
			return false;
		}
		if (ExitType != other.ExitType)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (PlayerName.Length != 0)
		{
			num ^= PlayerName.GetHashCode();
		}
		if (ExitType != 0)
		{
			num ^= ExitType.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(PlayerId);
		}
		if (PlayerName.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(PlayerName);
		}
		if (ExitType != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(ExitType);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (PlayerName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PlayerName);
		}
		if (ExitType != 0)
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
	public void MergeFrom(SysExitGuildC2S other)
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
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.PlayerName.Length != 0)
			{
				PlayerName = other.PlayerName;
			}
			if (other.ExitType != 0)
			{
				ExitType = other.ExitType;
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
			case 25u:
				PlayerId = input.ReadSFixed64();
				break;
			case 34u:
				PlayerName = input.ReadString();
				break;
			case 45u:
				ExitType = input.ReadSFixed32();
				break;
			}
		}
	}
}
