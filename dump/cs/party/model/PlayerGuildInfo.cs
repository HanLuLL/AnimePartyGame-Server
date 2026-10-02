using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class PlayerGuildInfo : IMessage<PlayerGuildInfo>, IMessage, IEquatable<PlayerGuildInfo>, IDeepCloneable<PlayerGuildInfo>, IBufferMessage
{
	private static readonly MessageParser<PlayerGuildInfo> _parser = new MessageParser<PlayerGuildInfo>(() => new PlayerGuildInfo());

	private UnknownFieldSet _unknownFields;

	public const int GuildIdFieldNumber = 1;

	private long guildId_;

	public const int LastJoinTimeFieldNumber = 2;

	private long lastJoinTime_;

	public const int ApplyCountFieldNumber = 3;

	private int applyCount_;

	public const int ApplicationsFieldNumber = 4;

	private static readonly MapField<long, long>.Codec _map_applications_codec = new MapField<long, long>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForSFixed64(17u, 0L), 34u);

	private readonly MapField<long, long> applications_ = new MapField<long, long>();

	public const int InvitationsFieldNumber = 5;

	private static readonly MapField<long, long>.Codec _map_invitations_codec = new MapField<long, long>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForSFixed64(17u, 0L), 42u);

	private readonly MapField<long, long> invitations_ = new MapField<long, long>();

	public const int LastSearchTimeFieldNumber = 6;

	private long lastSearchTime_;

	public const int InviteDisabledFieldNumber = 7;

	private bool inviteDisabled_;

	public const int ReceivedInvitationsFieldNumber = 8;

	private static readonly MapField<long, GuildInvitation>.Codec _map_receivedInvitations_codec = new MapField<long, GuildInvitation>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, GuildInvitation.Parser), 66u);

	private readonly MapField<long, GuildInvitation> receivedInvitations_ = new MapField<long, GuildInvitation>();

	public const int GuildTasksFieldNumber = 9;

	private static readonly MapField<int, TaskDSO>.Codec _map_guildTasks_codec = new MapField<int, TaskDSO>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, TaskDSO.Parser), 74u);

	private readonly MapField<int, TaskDSO> guildTasks_ = new MapField<int, TaskDSO>();

	public const int WeeklySignFieldNumber = 10;

	private static readonly MapField<long, bool>.Codec _map_weeklySign_codec = new MapField<long, bool>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForBool(16u, defaultValue: false), 82u);

	private readonly MapField<long, bool> weeklySign_ = new MapField<long, bool>();

	public const int CreateBannedFieldNumber = 11;

	private bool createBanned_;

	public const int MuteEndTimeFieldNumber = 12;

	private long muteEndTime_;

	public const int LastChatTimeFieldNumber = 13;

	private long lastChatTime_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PlayerGuildInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[111];

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
	public long LastJoinTime
	{
		get
		{
			return lastJoinTime_;
		}
		set
		{
			lastJoinTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ApplyCount
	{
		get
		{
			return applyCount_;
		}
		set
		{
			applyCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, long> Applications => applications_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, long> Invitations => invitations_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastSearchTime
	{
		get
		{
			return lastSearchTime_;
		}
		set
		{
			lastSearchTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool InviteDisabled
	{
		get
		{
			return inviteDisabled_;
		}
		set
		{
			inviteDisabled_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, GuildInvitation> ReceivedInvitations => receivedInvitations_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, TaskDSO> GuildTasks => guildTasks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, bool> WeeklySign => weeklySign_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CreateBanned
	{
		get
		{
			return createBanned_;
		}
		set
		{
			createBanned_ = value;
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
	public PlayerGuildInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerGuildInfo(PlayerGuildInfo other)
		: this()
	{
		guildId_ = other.guildId_;
		lastJoinTime_ = other.lastJoinTime_;
		applyCount_ = other.applyCount_;
		applications_ = other.applications_.Clone();
		invitations_ = other.invitations_.Clone();
		lastSearchTime_ = other.lastSearchTime_;
		inviteDisabled_ = other.inviteDisabled_;
		receivedInvitations_ = other.receivedInvitations_.Clone();
		guildTasks_ = other.guildTasks_.Clone();
		weeklySign_ = other.weeklySign_.Clone();
		createBanned_ = other.createBanned_;
		muteEndTime_ = other.muteEndTime_;
		lastChatTime_ = other.lastChatTime_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerGuildInfo Clone()
	{
		return new PlayerGuildInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PlayerGuildInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PlayerGuildInfo other)
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
		if (LastJoinTime != other.LastJoinTime)
		{
			return false;
		}
		if (ApplyCount != other.ApplyCount)
		{
			return false;
		}
		if (!Applications.Equals(other.Applications))
		{
			return false;
		}
		if (!Invitations.Equals(other.Invitations))
		{
			return false;
		}
		if (LastSearchTime != other.LastSearchTime)
		{
			return false;
		}
		if (InviteDisabled != other.InviteDisabled)
		{
			return false;
		}
		if (!ReceivedInvitations.Equals(other.ReceivedInvitations))
		{
			return false;
		}
		if (!GuildTasks.Equals(other.GuildTasks))
		{
			return false;
		}
		if (!WeeklySign.Equals(other.WeeklySign))
		{
			return false;
		}
		if (CreateBanned != other.CreateBanned)
		{
			return false;
		}
		if (MuteEndTime != other.MuteEndTime)
		{
			return false;
		}
		if (LastChatTime != other.LastChatTime)
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
		if (LastJoinTime != 0L)
		{
			num ^= LastJoinTime.GetHashCode();
		}
		if (ApplyCount != 0)
		{
			num ^= ApplyCount.GetHashCode();
		}
		num ^= Applications.GetHashCode();
		num ^= Invitations.GetHashCode();
		if (LastSearchTime != 0L)
		{
			num ^= LastSearchTime.GetHashCode();
		}
		if (InviteDisabled)
		{
			num ^= InviteDisabled.GetHashCode();
		}
		num ^= ReceivedInvitations.GetHashCode();
		num ^= GuildTasks.GetHashCode();
		num ^= WeeklySign.GetHashCode();
		if (CreateBanned)
		{
			num ^= CreateBanned.GetHashCode();
		}
		if (MuteEndTime != 0L)
		{
			num ^= MuteEndTime.GetHashCode();
		}
		if (LastChatTime != 0L)
		{
			num ^= LastChatTime.GetHashCode();
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
		if (LastJoinTime != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(LastJoinTime);
		}
		if (ApplyCount != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ApplyCount);
		}
		applications_.WriteTo(ref output, _map_applications_codec);
		invitations_.WriteTo(ref output, _map_invitations_codec);
		if (LastSearchTime != 0L)
		{
			output.WriteRawTag(49);
			output.WriteSFixed64(LastSearchTime);
		}
		if (InviteDisabled)
		{
			output.WriteRawTag(56);
			output.WriteBool(InviteDisabled);
		}
		receivedInvitations_.WriteTo(ref output, _map_receivedInvitations_codec);
		guildTasks_.WriteTo(ref output, _map_guildTasks_codec);
		weeklySign_.WriteTo(ref output, _map_weeklySign_codec);
		if (CreateBanned)
		{
			output.WriteRawTag(88);
			output.WriteBool(CreateBanned);
		}
		if (MuteEndTime != 0L)
		{
			output.WriteRawTag(97);
			output.WriteSFixed64(MuteEndTime);
		}
		if (LastChatTime != 0L)
		{
			output.WriteRawTag(105);
			output.WriteSFixed64(LastChatTime);
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
		if (LastJoinTime != 0L)
		{
			num += 9;
		}
		if (ApplyCount != 0)
		{
			num += 5;
		}
		num += applications_.CalculateSize(_map_applications_codec);
		num += invitations_.CalculateSize(_map_invitations_codec);
		if (LastSearchTime != 0L)
		{
			num += 9;
		}
		if (InviteDisabled)
		{
			num += 2;
		}
		num += receivedInvitations_.CalculateSize(_map_receivedInvitations_codec);
		num += guildTasks_.CalculateSize(_map_guildTasks_codec);
		num += weeklySign_.CalculateSize(_map_weeklySign_codec);
		if (CreateBanned)
		{
			num += 2;
		}
		if (MuteEndTime != 0L)
		{
			num += 9;
		}
		if (LastChatTime != 0L)
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
	public void MergeFrom(PlayerGuildInfo other)
	{
		if (other != null)
		{
			if (other.GuildId != 0L)
			{
				GuildId = other.GuildId;
			}
			if (other.LastJoinTime != 0L)
			{
				LastJoinTime = other.LastJoinTime;
			}
			if (other.ApplyCount != 0)
			{
				ApplyCount = other.ApplyCount;
			}
			applications_.MergeFrom(other.applications_);
			invitations_.MergeFrom(other.invitations_);
			if (other.LastSearchTime != 0L)
			{
				LastSearchTime = other.LastSearchTime;
			}
			if (other.InviteDisabled)
			{
				InviteDisabled = other.InviteDisabled;
			}
			receivedInvitations_.MergeFrom(other.receivedInvitations_);
			guildTasks_.MergeFrom(other.guildTasks_);
			weeklySign_.MergeFrom(other.weeklySign_);
			if (other.CreateBanned)
			{
				CreateBanned = other.CreateBanned;
			}
			if (other.MuteEndTime != 0L)
			{
				MuteEndTime = other.MuteEndTime;
			}
			if (other.LastChatTime != 0L)
			{
				LastChatTime = other.LastChatTime;
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
				LastJoinTime = input.ReadSFixed64();
				break;
			case 29u:
				ApplyCount = input.ReadSFixed32();
				break;
			case 34u:
				applications_.AddEntriesFrom(ref input, _map_applications_codec);
				break;
			case 42u:
				invitations_.AddEntriesFrom(ref input, _map_invitations_codec);
				break;
			case 49u:
				LastSearchTime = input.ReadSFixed64();
				break;
			case 56u:
				InviteDisabled = input.ReadBool();
				break;
			case 66u:
				receivedInvitations_.AddEntriesFrom(ref input, _map_receivedInvitations_codec);
				break;
			case 74u:
				guildTasks_.AddEntriesFrom(ref input, _map_guildTasks_codec);
				break;
			case 82u:
				weeklySign_.AddEntriesFrom(ref input, _map_weeklySign_codec);
				break;
			case 88u:
				CreateBanned = input.ReadBool();
				break;
			case 97u:
				MuteEndTime = input.ReadSFixed64();
				break;
			case 105u:
				LastChatTime = input.ReadSFixed64();
				break;
			}
		}
	}
}
