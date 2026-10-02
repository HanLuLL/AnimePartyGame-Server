using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class CreateGuildS2C : IMessage<CreateGuildS2C>, IMessage, IEquatable<CreateGuildS2C>, IDeepCloneable<CreateGuildS2C>, IBufferMessage
{
	private static readonly MessageParser<CreateGuildS2C> _parser = new MessageParser<CreateGuildS2C>(() => new CreateGuildS2C());

	private UnknownFieldSet _unknownFields;

	public const int GuildFieldNumber = 1;

	private Guild guild_;

	public const int PlayerGuildFieldNumber = 2;

	private PlayerGuildInfo playerGuild_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CreateGuildS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[562];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Guild Guild
	{
		get
		{
			return guild_;
		}
		set
		{
			guild_ = value;
		}
	}

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
	public CreateGuildS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateGuildS2C(CreateGuildS2C other)
		: this()
	{
		guild_ = ((other.guild_ != null) ? other.guild_.Clone() : null);
		playerGuild_ = ((other.playerGuild_ != null) ? other.playerGuild_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateGuildS2C Clone()
	{
		return new CreateGuildS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CreateGuildS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CreateGuildS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Guild, other.Guild))
		{
			return false;
		}
		if (!object.Equals(PlayerGuild, other.PlayerGuild))
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
		if (guild_ != null)
		{
			num ^= Guild.GetHashCode();
		}
		if (playerGuild_ != null)
		{
			num ^= PlayerGuild.GetHashCode();
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
		if (guild_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Guild);
		}
		if (playerGuild_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(PlayerGuild);
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
		if (guild_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Guild);
		}
		if (playerGuild_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(PlayerGuild);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CreateGuildS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.guild_ != null)
		{
			if (guild_ == null)
			{
				Guild = new Guild();
			}
			Guild.MergeFrom(other.Guild);
		}
		if (other.playerGuild_ != null)
		{
			if (playerGuild_ == null)
			{
				PlayerGuild = new PlayerGuildInfo();
			}
			PlayerGuild.MergeFrom(other.PlayerGuild);
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
				if (guild_ == null)
				{
					Guild = new Guild();
				}
				input.ReadMessage(Guild);
				break;
			case 18u:
				if (playerGuild_ == null)
				{
					PlayerGuild = new PlayerGuildInfo();
				}
				input.ReadMessage(PlayerGuild);
				break;
			}
		}
	}
}
