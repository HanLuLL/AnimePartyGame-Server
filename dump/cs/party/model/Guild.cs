using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Guild : IMessage<Guild>, IMessage, IEquatable<Guild>, IDeepCloneable<Guild>, IBufferMessage
{
	private static readonly MessageParser<Guild> _parser = new MessageParser<Guild>(() => new Guild());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private long id_;

	public const int NameFieldNumber = 2;

	private string name_ = "";

	public const int TagIdsFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_tagIds_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> tagIds_ = new RepeatedField<int>();

	public const int StatusFieldNumber = 4;

	private int status_;

	public const int MemberCountFieldNumber = 5;

	private int memberCount_;

	public const int CreateTimeFieldNumber = 6;

	private long createTime_;

	public const int UpdateTimeFieldNumber = 7;

	private long updateTime_;

	public const int LastWeekActiveCountFieldNumber = 8;

	private int lastWeekActiveCount_;

	public const int MuteEndTimeFieldNumber = 9;

	private long muteEndTime_;

	public const int MembersFieldNumber = 101;

	private static readonly MapField<long, GuildMember>.Codec _map_members_codec = new MapField<long, GuildMember>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, GuildMember.Parser), 810u);

	private readonly MapField<long, GuildMember> members_ = new MapField<long, GuildMember>();

	public const int ExAnnouncementFieldNumber = 102;

	private string exAnnouncement_ = "";

	public const int LastExternalEditTimeFieldNumber = 103;

	private long lastExternalEditTime_;

	public const int LastExternalEditorIdFieldNumber = 104;

	private long lastExternalEditorId_;

	public const int ExternalEditCountFieldNumber = 105;

	private int externalEditCount_;

	public const int InAnnouncementFieldNumber = 106;

	private string inAnnouncement_ = "";

	public const int LastInternalEditTimeFieldNumber = 107;

	private long lastInternalEditTime_;

	public const int LastInternalEditorIdFieldNumber = 108;

	private long lastInternalEditorId_;

	public const int InternalEditCountFieldNumber = 109;

	private int internalEditCount_;

	public const int ApplicationsFieldNumber = 110;

	private static readonly MapField<long, GuildApplication>.Codec _map_applications_codec = new MapField<long, GuildApplication>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, GuildApplication.Parser), 882u);

	private readonly MapField<long, GuildApplication> applications_ = new MapField<long, GuildApplication>();

	public const int LastChatTimeFieldNumber = 111;

	private long lastChatTime_;

	public const int NextDayTimeFieldNumber = 201;

	private long nextDayTime_;

	public const int NextWeekTimeFieldNumber = 202;

	private long nextWeekTime_;

	public const int LastChangeMsgIdFieldNumber = 203;

	private long lastChangeMsgId_;

	public const int LastChatMsgIdFieldNumber = 204;

	private long lastChatMsgId_;

	public const int IsUpdateFieldNumber = 1001;

	private bool isUpdate_;

	public const int SaveTimeFieldNumber = 1002;

	private long saveTime_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Guild> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[113];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TagIds => tagIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Status
	{
		get
		{
			return status_;
		}
		set
		{
			status_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MemberCount
	{
		get
		{
			return memberCount_;
		}
		set
		{
			memberCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long CreateTime
	{
		get
		{
			return createTime_;
		}
		set
		{
			createTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long UpdateTime
	{
		get
		{
			return updateTime_;
		}
		set
		{
			updateTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LastWeekActiveCount
	{
		get
		{
			return lastWeekActiveCount_;
		}
		set
		{
			lastWeekActiveCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long MuteEndTime
	{
		get
		{
			return muteEndTime_;
		}
		set
		{
			muteEndTime_ = value;
		}
	}

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
	public bool IsUpdate
	{
		get
		{
			return isUpdate_;
		}
		set
		{
			isUpdate_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long SaveTime
	{
		get
		{
			return saveTime_;
		}
		set
		{
			saveTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Guild()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Guild(Guild other)
		: this()
	{
		id_ = other.id_;
		name_ = other.name_;
		tagIds_ = other.tagIds_.Clone();
		status_ = other.status_;
		memberCount_ = other.memberCount_;
		createTime_ = other.createTime_;
		updateTime_ = other.updateTime_;
		lastWeekActiveCount_ = other.lastWeekActiveCount_;
		muteEndTime_ = other.muteEndTime_;
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
		isUpdate_ = other.isUpdate_;
		saveTime_ = other.saveTime_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Guild Clone()
	{
		return new Guild(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Guild);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Guild other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (!tagIds_.Equals(other.tagIds_))
		{
			return false;
		}
		if (Status != other.Status)
		{
			return false;
		}
		if (MemberCount != other.MemberCount)
		{
			return false;
		}
		if (CreateTime != other.CreateTime)
		{
			return false;
		}
		if (UpdateTime != other.UpdateTime)
		{
			return false;
		}
		if (LastWeekActiveCount != other.LastWeekActiveCount)
		{
			return false;
		}
		if (MuteEndTime != other.MuteEndTime)
		{
			return false;
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
		if (IsUpdate != other.IsUpdate)
		{
			return false;
		}
		if (SaveTime != other.SaveTime)
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
		if (Id != 0L)
		{
			num ^= Id.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		num ^= tagIds_.GetHashCode();
		if (Status != 0)
		{
			num ^= Status.GetHashCode();
		}
		if (MemberCount != 0)
		{
			num ^= MemberCount.GetHashCode();
		}
		if (CreateTime != 0L)
		{
			num ^= CreateTime.GetHashCode();
		}
		if (UpdateTime != 0L)
		{
			num ^= UpdateTime.GetHashCode();
		}
		if (LastWeekActiveCount != 0)
		{
			num ^= LastWeekActiveCount.GetHashCode();
		}
		if (MuteEndTime != 0L)
		{
			num ^= MuteEndTime.GetHashCode();
		}
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
		if (IsUpdate)
		{
			num ^= IsUpdate.GetHashCode();
		}
		if (SaveTime != 0L)
		{
			num ^= SaveTime.GetHashCode();
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
		if (Id != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(Id);
		}
		if (Name.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Name);
		}
		tagIds_.WriteTo(ref output, _repeated_tagIds_codec);
		if (Status != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Status);
		}
		if (MemberCount != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(MemberCount);
		}
		if (CreateTime != 0L)
		{
			output.WriteRawTag(49);
			output.WriteSFixed64(CreateTime);
		}
		if (UpdateTime != 0L)
		{
			output.WriteRawTag(57);
			output.WriteSFixed64(UpdateTime);
		}
		if (LastWeekActiveCount != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(LastWeekActiveCount);
		}
		if (MuteEndTime != 0L)
		{
			output.WriteRawTag(73);
			output.WriteSFixed64(MuteEndTime);
		}
		members_.WriteTo(ref output, _map_members_codec);
		if (ExAnnouncement.Length != 0)
		{
			output.WriteRawTag(178, 6);
			output.WriteString(ExAnnouncement);
		}
		if (LastExternalEditTime != 0L)
		{
			output.WriteRawTag(185, 6);
			output.WriteSFixed64(LastExternalEditTime);
		}
		if (LastExternalEditorId != 0L)
		{
			output.WriteRawTag(193, 6);
			output.WriteSFixed64(LastExternalEditorId);
		}
		if (ExternalEditCount != 0)
		{
			output.WriteRawTag(205, 6);
			output.WriteSFixed32(ExternalEditCount);
		}
		if (InAnnouncement.Length != 0)
		{
			output.WriteRawTag(210, 6);
			output.WriteString(InAnnouncement);
		}
		if (LastInternalEditTime != 0L)
		{
			output.WriteRawTag(217, 6);
			output.WriteSFixed64(LastInternalEditTime);
		}
		if (LastInternalEditorId != 0L)
		{
			output.WriteRawTag(225, 6);
			output.WriteSFixed64(LastInternalEditorId);
		}
		if (InternalEditCount != 0)
		{
			output.WriteRawTag(237, 6);
			output.WriteSFixed32(InternalEditCount);
		}
		applications_.WriteTo(ref output, _map_applications_codec);
		if (LastChatTime != 0L)
		{
			output.WriteRawTag(249, 6);
			output.WriteSFixed64(LastChatTime);
		}
		if (NextDayTime != 0L)
		{
			output.WriteRawTag(201, 12);
			output.WriteSFixed64(NextDayTime);
		}
		if (NextWeekTime != 0L)
		{
			output.WriteRawTag(209, 12);
			output.WriteSFixed64(NextWeekTime);
		}
		if (LastChangeMsgId != 0L)
		{
			output.WriteRawTag(217, 12);
			output.WriteSFixed64(LastChangeMsgId);
		}
		if (LastChatMsgId != 0L)
		{
			output.WriteRawTag(225, 12);
			output.WriteSFixed64(LastChatMsgId);
		}
		if (IsUpdate)
		{
			output.WriteRawTag(200, 62);
			output.WriteBool(IsUpdate);
		}
		if (SaveTime != 0L)
		{
			output.WriteRawTag(209, 62);
			output.WriteSFixed64(SaveTime);
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
		if (Id != 0L)
		{
			num += 9;
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		num += tagIds_.CalculateSize(_repeated_tagIds_codec);
		if (Status != 0)
		{
			num += 5;
		}
		if (MemberCount != 0)
		{
			num += 5;
		}
		if (CreateTime != 0L)
		{
			num += 9;
		}
		if (UpdateTime != 0L)
		{
			num += 9;
		}
		if (LastWeekActiveCount != 0)
		{
			num += 5;
		}
		if (MuteEndTime != 0L)
		{
			num += 9;
		}
		num += members_.CalculateSize(_map_members_codec);
		if (ExAnnouncement.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(ExAnnouncement);
		}
		if (LastExternalEditTime != 0L)
		{
			num += 10;
		}
		if (LastExternalEditorId != 0L)
		{
			num += 10;
		}
		if (ExternalEditCount != 0)
		{
			num += 6;
		}
		if (InAnnouncement.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(InAnnouncement);
		}
		if (LastInternalEditTime != 0L)
		{
			num += 10;
		}
		if (LastInternalEditorId != 0L)
		{
			num += 10;
		}
		if (InternalEditCount != 0)
		{
			num += 6;
		}
		num += applications_.CalculateSize(_map_applications_codec);
		if (LastChatTime != 0L)
		{
			num += 10;
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
		if (IsUpdate)
		{
			num += 3;
		}
		if (SaveTime != 0L)
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
	public void MergeFrom(Guild other)
	{
		if (other != null)
		{
			if (other.Id != 0L)
			{
				Id = other.Id;
			}
			if (other.Name.Length != 0)
			{
				Name = other.Name;
			}
			tagIds_.Add(other.tagIds_);
			if (other.Status != 0)
			{
				Status = other.Status;
			}
			if (other.MemberCount != 0)
			{
				MemberCount = other.MemberCount;
			}
			if (other.CreateTime != 0L)
			{
				CreateTime = other.CreateTime;
			}
			if (other.UpdateTime != 0L)
			{
				UpdateTime = other.UpdateTime;
			}
			if (other.LastWeekActiveCount != 0)
			{
				LastWeekActiveCount = other.LastWeekActiveCount;
			}
			if (other.MuteEndTime != 0L)
			{
				MuteEndTime = other.MuteEndTime;
			}
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
			if (other.IsUpdate)
			{
				IsUpdate = other.IsUpdate;
			}
			if (other.SaveTime != 0L)
			{
				SaveTime = other.SaveTime;
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
				Id = input.ReadSFixed64();
				break;
			case 18u:
				Name = input.ReadString();
				break;
			case 26u:
			case 29u:
				tagIds_.AddEntriesFrom(ref input, _repeated_tagIds_codec);
				break;
			case 37u:
				Status = input.ReadSFixed32();
				break;
			case 45u:
				MemberCount = input.ReadSFixed32();
				break;
			case 49u:
				CreateTime = input.ReadSFixed64();
				break;
			case 57u:
				UpdateTime = input.ReadSFixed64();
				break;
			case 69u:
				LastWeekActiveCount = input.ReadSFixed32();
				break;
			case 73u:
				MuteEndTime = input.ReadSFixed64();
				break;
			case 810u:
				members_.AddEntriesFrom(ref input, _map_members_codec);
				break;
			case 818u:
				ExAnnouncement = input.ReadString();
				break;
			case 825u:
				LastExternalEditTime = input.ReadSFixed64();
				break;
			case 833u:
				LastExternalEditorId = input.ReadSFixed64();
				break;
			case 845u:
				ExternalEditCount = input.ReadSFixed32();
				break;
			case 850u:
				InAnnouncement = input.ReadString();
				break;
			case 857u:
				LastInternalEditTime = input.ReadSFixed64();
				break;
			case 865u:
				LastInternalEditorId = input.ReadSFixed64();
				break;
			case 877u:
				InternalEditCount = input.ReadSFixed32();
				break;
			case 882u:
				applications_.AddEntriesFrom(ref input, _map_applications_codec);
				break;
			case 889u:
				LastChatTime = input.ReadSFixed64();
				break;
			case 1609u:
				NextDayTime = input.ReadSFixed64();
				break;
			case 1617u:
				NextWeekTime = input.ReadSFixed64();
				break;
			case 1625u:
				LastChangeMsgId = input.ReadSFixed64();
				break;
			case 1633u:
				LastChatMsgId = input.ReadSFixed64();
				break;
			case 8008u:
				IsUpdate = input.ReadBool();
				break;
			case 8017u:
				SaveTime = input.ReadSFixed64();
				break;
			}
		}
	}
}
