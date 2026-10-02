using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SyncPlayerJoinGuildS2C : IMessage<SyncPlayerJoinGuildS2C>, IMessage, IEquatable<SyncPlayerJoinGuildS2C>, IDeepCloneable<SyncPlayerJoinGuildS2C>, IBufferMessage
{
	private static readonly MessageParser<SyncPlayerJoinGuildS2C> _parser = new MessageParser<SyncPlayerJoinGuildS2C>(() => new SyncPlayerJoinGuildS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerGuildFieldNumber = 1;

	private PlayerGuildInfo playerGuild_;

	public const int GuildNameFieldNumber = 2;

	private string guildName_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SyncPlayerJoinGuildS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[556];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerGuildInfo PlayerGuild
	{
		get
		{
			return playerGuild_;
		}
		set
		{
			playerGuild_ = value;
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
	public SyncPlayerJoinGuildS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncPlayerJoinGuildS2C(SyncPlayerJoinGuildS2C other)
		: this()
	{
		playerGuild_ = ((other.playerGuild_ != null) ? other.playerGuild_.Clone() : null);
		guildName_ = other.guildName_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncPlayerJoinGuildS2C Clone()
	{
		return new SyncPlayerJoinGuildS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SyncPlayerJoinGuildS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SyncPlayerJoinGuildS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(PlayerGuild, other.PlayerGuild))
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
		if (playerGuild_ != null)
		{
			num ^= PlayerGuild.GetHashCode();
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
		if (playerGuild_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(PlayerGuild);
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
		if (playerGuild_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(PlayerGuild);
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
	public void MergeFrom(SyncPlayerJoinGuildS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.playerGuild_ != null)
		{
			if (playerGuild_ == null)
			{
				PlayerGuild = new PlayerGuildInfo();
			}
			PlayerGuild.MergeFrom(other.PlayerGuild);
		}
		if (other.GuildName.Length != 0)
		{
			GuildName = other.GuildName;
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
			case 10u:
				if (playerGuild_ == null)
				{
					PlayerGuild = new PlayerGuildInfo();
				}
				input.ReadMessage(PlayerGuild);
				break;
			case 18u:
				GuildName = input.ReadString();
				break;
			}
		}
	}
}
