using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SyncGuildMemberS2C : IMessage<SyncGuildMemberS2C>, IMessage, IEquatable<SyncGuildMemberS2C>, IDeepCloneable<SyncGuildMemberS2C>, IBufferMessage
{
	private static readonly MessageParser<SyncGuildMemberS2C> _parser = new MessageParser<SyncGuildMemberS2C>(() => new SyncGuildMemberS2C());

	private UnknownFieldSet _unknownFields;

	public const int MembersFieldNumber = 1;

	private static readonly MapField<long, GuildMember>.Codec _map_members_codec = new MapField<long, GuildMember>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, GuildMember.Parser), 10u);

	private readonly MapField<long, GuildMember> members_ = new MapField<long, GuildMember>();

	public const int InfosFieldNumber = 2;

	private static readonly MapField<long, FriendShowPlayerInfo>.Codec _map_infos_codec = new MapField<long, FriendShowPlayerInfo>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, FriendShowPlayerInfo.Parser), 18u);

	private readonly MapField<long, FriendShowPlayerInfo> infos_ = new MapField<long, FriendShowPlayerInfo>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SyncGuildMemberS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[558];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, GuildMember> Members => members_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, FriendShowPlayerInfo> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncGuildMemberS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncGuildMemberS2C(SyncGuildMemberS2C other)
		: this()
	{
		members_ = other.members_.Clone();
		infos_ = other.infos_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncGuildMemberS2C Clone()
	{
		return new SyncGuildMemberS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SyncGuildMemberS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SyncGuildMemberS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!Members.Equals(other.Members))
		{
			return false;
		}
		if (!Infos.Equals(other.Infos))
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
		num ^= Members.GetHashCode();
		num ^= Infos.GetHashCode();
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
		members_.WriteTo(ref output, _map_members_codec);
		infos_.WriteTo(ref output, _map_infos_codec);
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
		num += members_.CalculateSize(_map_members_codec);
		num += infos_.CalculateSize(_map_infos_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SyncGuildMemberS2C other)
	{
		if (other != null)
		{
			members_.MergeFrom(other.members_);
			infos_.MergeFrom(other.infos_);
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
			case 10u:
				members_.AddEntriesFrom(ref input, _map_members_codec);
				break;
			case 18u:
				infos_.AddEntriesFrom(ref input, _map_infos_codec);
				break;
			}
		}
	}
}
