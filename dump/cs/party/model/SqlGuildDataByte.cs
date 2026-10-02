using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SqlGuildDataByte : IMessage<SqlGuildDataByte>, IMessage, IEquatable<SqlGuildDataByte>, IDeepCloneable<SqlGuildDataByte>, IBufferMessage
{
	private static readonly MessageParser<SqlGuildDataByte> _parser = new MessageParser<SqlGuildDataByte>(() => new SqlGuildDataByte());

	private UnknownFieldSet _unknownFields;

	public const int MembersFieldNumber = 1;

	private static readonly MapField<long, GuildMember>.Codec _map_members_codec = new MapField<long, GuildMember>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, GuildMember.Parser), 10u);

	private readonly MapField<long, GuildMember> members_ = new MapField<long, GuildMember>();

	public const int ExAnnouncementFieldNumber = 2;

	private string exAnnouncement_ = "";

	public const int LastExternalEditTimeFieldNumber = 3;

	private long lastExternalEditTime_;

	public const int LastExternalEditorIdFieldNumber = 4;

	private long lastExternalEditorId_;

	public const int ExternalEditCountFieldNumber = 5;

	private int externalEditCount_;

	public const int InAnnouncementFieldNumber = 6;

	private string inAnnouncement_ = "";

	public const int LastInternalEditTimeFieldNumber = 7;

	private long lastInternalEditTime_;

	public const int LastInternalEditorIdFieldNumber = 8;

	private long lastInternalEditorId_;

	public const int InternalEditCountFieldNumber = 9;

	private int internalEditCount_;

	public const int ApplicationsFieldNumber = 10;

	private static readonly MapField<long, GuildApplication>.Codec _map_applications_codec = new MapField<long, GuildApplication>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, GuildApplication.Parser), 82u);

	private readonly MapField<long, GuildApplication> applications_ = new MapField<long, GuildApplication>();

	public const int LastChatTimeFieldNumber = 11;

	private long lastChatTime_;

	public const int NextDayTimeFieldNumber = 101;

	private long nextDayTime_;

	public const int NextWeekTimeFieldNumber = 102;

	private long nextWeekTime_;

	public const int LastChangeMsgIdFieldNumber = 103;

	private long lastChangeMsgId_;

	public const int LastChatMsgIdFieldNumber = 104;

	private long lastChatMsgId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SqlGuildDataByte> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[114];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, GuildMember> Members => members_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ExAnnouncement
	{
		get
		{
			return exAnnouncement_;
		}
		set
		{
			exAnnouncement_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastExternalEditTime
	{
		get
		{
			return lastExternalEditTime_;
		}
		set
		{
			lastExternalEditTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastExternalEditorId
	{
		get
		{
			return lastExternalEditorId_;
		}
		set
		{
			lastExternalEditorId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ExternalEditCount
	{
		get
		{
			return externalEditCount_;
		}
		set
		{
			externalEditCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string InAnnouncement
	{
		get
		{
			return inAnnouncement_;
		}
		set
		{
			inAnnouncement_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastInternalEditTime
	{
		get
		{
			return lastInternalEditTime_;
		}
		set
		{
			lastInternalEditTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastInternalEditorId
	{
		get
		{
			return lastInternalEditorId_;
		}
		set
		{
			lastInternalEditorId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int InternalEditCount
	{
		get
		{
			return internalEditCount_;
		}
		set
		{
			internalEditCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, GuildApplication> Applications => applications_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastChatTime
	{
		get
		{
			return lastChatTime_;
		}
		set
		{
			lastChatTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long NextDayTime
	{
		get
		{
			return nextDayTime_;
		}
		set
		{
			nextDayTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long NextWeekTime
	{
		get
		{
			return nextWeekTime_;
		}
		set
		{
			nextWeekTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastChangeMsgId
	{
		get
		{
			return lastChangeMsgId_;
		}
		set
		{
			lastChangeMsgId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastChatMsgId
	{
		get
		{
			return lastChatMsgId_;
		}
		set
		{
			lastChatMsgId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlGuildDataByte()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlGuildDataByte(SqlGuildDataByte other)
		: this()
	{
		members_ = other.members_.Clone();
		exAnnouncement_ = other.exAnnouncement_;
		lastExternalEditTime_ = other.lastExternalEditTime_;
		lastExternalEditorId_ = other.lastExternalEditorId_;
		externalEditCount_ = other.externalEditCount_;
		inAnnouncement_ = other.inAnnouncement_;
		lastInternalEditTime_ = other.lastInternalEditTime_;
		lastInternalEditorId_ = other.lastInternalEditorId_;
		internalEditCount_ = other.internalEditCount_;
		applications_ = other.applications_.Clone();
		lastChatTime_ = other.lastChatTime_;
		nextDayTime_ = other.nextDayTime_;
		nextWeekTime_ = other.nextWeekTime_;
		lastChangeMsgId_ = other.lastChangeMsgId_;
		lastChatMsgId_ = other.lastChatMsgId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlGuildDataByte Clone()
	{
		return new SqlGuildDataByte(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SqlGuildDataByte);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SqlGuildDataByte other)
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
		if (ExAnnouncement != other.ExAnnouncement)
		{
			return false;
		}
		if (LastExternalEditTime != other.LastExternalEditTime)
		{
			return false;
		}
		if (LastExternalEditorId != other.LastExternalEditorId)
		{
			return false;
		}
		if (ExternalEditCount != other.ExternalEditCount)
		{
			return false;
		}
		if (InAnnouncement != other.InAnnouncement)
		{
			return false;
		}
		if (LastInternalEditTime != other.LastInternalEditTime)
		{
			return false;
		}
		if (LastInternalEditorId != other.LastInternalEditorId)
		{
			return false;
		}
		if (InternalEditCount != other.InternalEditCount)
		{
			return false;
		}
		if (!Applications.Equals(other.Applications))
		{
			return false;
		}
		if (LastChatTime != other.LastChatTime)
		{
			return false;
		}
		if (NextDayTime != other.NextDayTime)
		{
			return false;
		}
		if (NextWeekTime != other.NextWeekTime)
		{
			return false;
		}
		if (LastChangeMsgId != other.LastChangeMsgId)
		{
			return false;
		}
		if (LastChatMsgId != other.LastChatMsgId)
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
		if (ExAnnouncement.Length != 0)
		{
			num ^= ExAnnouncement.GetHashCode();
		}
		if (LastExternalEditTime != 0L)
		{
			num ^= LastExternalEditTime.GetHashCode();
		}
		if (LastExternalEditorId != 0L)
		{
			num ^= LastExternalEditorId.GetHashCode();
		}
		if (ExternalEditCount != 0)
		{
			num ^= ExternalEditCount.GetHashCode();
		}
		if (InAnnouncement.Length != 0)
		{
			num ^= InAnnouncement.GetHashCode();
		}
		if (LastInternalEditTime != 0L)
		{
			num ^= LastInternalEditTime.GetHashCode();
		}
		if (LastInternalEditorId != 0L)
		{
			num ^= LastInternalEditorId.GetHashCode();
		}
		if (InternalEditCount != 0)
		{
			num ^= InternalEditCount.GetHashCode();
		}
		num ^= Applications.GetHashCode();
		if (LastChatTime != 0L)
		{
			num ^= LastChatTime.GetHashCode();
		}
		if (NextDayTime != 0L)
		{
			num ^= NextDayTime.GetHashCode();
		}
		if (NextWeekTime != 0L)
		{
			num ^= NextWeekTime.GetHashCode();
		}
		if (LastChangeMsgId != 0L)
		{
			num ^= LastChangeMsgId.GetHashCode();
		}
		if (LastChatMsgId != 0L)
		{
			num ^= LastChatMsgId.GetHashCode();
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
		members_.WriteTo(ref output, _map_members_codec);
		if (ExAnnouncement.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(ExAnnouncement);
		}
		if (LastExternalEditTime != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(LastExternalEditTime);
		}
		if (LastExternalEditorId != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(LastExternalEditorId);
		}
		if (ExternalEditCount != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(ExternalEditCount);
		}
		if (InAnnouncement.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(InAnnouncement);
		}
		if (LastInternalEditTime != 0L)
		{
			output.WriteRawTag(57);
			output.WriteSFixed64(LastInternalEditTime);
		}
		if (LastInternalEditorId != 0L)
		{
			output.WriteRawTag(65);
			output.WriteSFixed64(LastInternalEditorId);
		}
		if (InternalEditCount != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(InternalEditCount);
		}
		applications_.WriteTo(ref output, _map_applications_codec);
		if (LastChatTime != 0L)
		{
			output.WriteRawTag(89);
			output.WriteSFixed64(LastChatTime);
		}
		if (NextDayTime != 0L)
		{
			output.WriteRawTag(169, 6);
			output.WriteSFixed64(NextDayTime);
		}
		if (NextWeekTime != 0L)
		{
			output.WriteRawTag(177, 6);
			output.WriteSFixed64(NextWeekTime);
		}
		if (LastChangeMsgId != 0L)
		{
			output.WriteRawTag(185, 6);
			output.WriteSFixed64(LastChangeMsgId);
		}
		if (LastChatMsgId != 0L)
		{
			output.WriteRawTag(193, 6);
			output.WriteSFixed64(LastChatMsgId);
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
		num += members_.CalculateSize(_map_members_codec);
		if (ExAnnouncement.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ExAnnouncement);
		}
		if (LastExternalEditTime != 0L)
		{
			num += 9;
		}
		if (LastExternalEditorId != 0L)
		{
			num += 9;
		}
		if (ExternalEditCount != 0)
		{
			num += 5;
		}
		if (InAnnouncement.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(InAnnouncement);
		}
		if (LastInternalEditTime != 0L)
		{
			num += 9;
		}
		if (LastInternalEditorId != 0L)
		{
			num += 9;
		}
		if (InternalEditCount != 0)
		{
			num += 5;
		}
		num += applications_.CalculateSize(_map_applications_codec);
		if (LastChatTime != 0L)
		{
			num += 9;
		}
		if (NextDayTime != 0L)
		{
			num += 10;
		}
		if (NextWeekTime != 0L)
		{
			num += 10;
		}
		if (LastChangeMsgId != 0L)
		{
			num += 10;
		}
		if (LastChatMsgId != 0L)
		{
			num += 10;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SqlGuildDataByte other)
	{
		if (other != null)
		{
			members_.MergeFrom(other.members_);
			if (other.ExAnnouncement.Length != 0)
			{
				ExAnnouncement = other.ExAnnouncement;
			}
			if (other.LastExternalEditTime != 0L)
			{
				LastExternalEditTime = other.LastExternalEditTime;
			}
			if (other.LastExternalEditorId != 0L)
			{
				LastExternalEditorId = other.LastExternalEditorId;
			}
			if (other.ExternalEditCount != 0)
			{
				ExternalEditCount = other.ExternalEditCount;
			}
			if (other.InAnnouncement.Length != 0)
			{
				InAnnouncement = other.InAnnouncement;
			}
			if (other.LastInternalEditTime != 0L)
			{
				LastInternalEditTime = other.LastInternalEditTime;
			}
			if (other.LastInternalEditorId != 0L)
			{
				LastInternalEditorId = other.LastInternalEditorId;
			}
			if (other.InternalEditCount != 0)
			{
				InternalEditCount = other.InternalEditCount;
			}
			applications_.MergeFrom(other.applications_);
			if (other.LastChatTime != 0L)
			{
				LastChatTime = other.LastChatTime;
			}
			if (other.NextDayTime != 0L)
			{
				NextDayTime = other.NextDayTime;
			}
			if (other.NextWeekTime != 0L)
			{
				NextWeekTime = other.NextWeekTime;
			}
			if (other.LastChangeMsgId != 0L)
			{
				LastChangeMsgId = other.LastChangeMsgId;
			}
			if (other.LastChatMsgId != 0L)
			{
				LastChatMsgId = other.LastChatMsgId;
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
			case 10u:
				members_.AddEntriesFrom(ref input, _map_members_codec);
				break;
			case 18u:
				ExAnnouncement = input.ReadString();
				break;
			case 25u:
				LastExternalEditTime = input.ReadSFixed64();
				break;
			case 33u:
				LastExternalEditorId = input.ReadSFixed64();
				break;
			case 45u:
				ExternalEditCount = input.ReadSFixed32();
				break;
			case 50u:
				InAnnouncement = input.ReadString();
				break;
			case 57u:
				LastInternalEditTime = input.ReadSFixed64();
				break;
			case 65u:
				LastInternalEditorId = input.ReadSFixed64();
				break;
			case 77u:
				InternalEditCount = input.ReadSFixed32();
				break;
			case 82u:
				applications_.AddEntriesFrom(ref input, _map_applications_codec);
				break;
			case 89u:
				LastChatTime = input.ReadSFixed64();
				break;
			case 809u:
				NextDayTime = input.ReadSFixed64();
				break;
			case 817u:
				NextWeekTime = input.ReadSFixed64();
				break;
			case 825u:
				LastChangeMsgId = input.ReadSFixed64();
				break;
			case 833u:
				LastChatMsgId = input.ReadSFixed64();
				break;
			}
		}
	}
}
