using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SyncPlayerGuildS2C : IMessage<SyncPlayerGuildS2C>, IMessage, IEquatable<SyncPlayerGuildS2C>, IDeepCloneable<SyncPlayerGuildS2C>, IBufferMessage
{
	private static readonly MessageParser<SyncPlayerGuildS2C> _parser = new MessageParser<SyncPlayerGuildS2C>(() => new SyncPlayerGuildS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerGuildFieldNumber = 1;

	private PlayerGuildInfo playerGuild_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SyncPlayerGuildS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[555];

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
	public SyncPlayerGuildS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncPlayerGuildS2C(SyncPlayerGuildS2C other)
		: this()
	{
		playerGuild_ = ((other.playerGuild_ != null) ? other.playerGuild_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncPlayerGuildS2C Clone()
	{
		return new SyncPlayerGuildS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SyncPlayerGuildS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SyncPlayerGuildS2C other)
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
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SyncPlayerGuildS2C other)
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				continue;
			}
			if (playerGuild_ == null)
			{
				PlayerGuild = new PlayerGuildInfo();
			}
			input.ReadMessage(PlayerGuild);
		}
	}
}
