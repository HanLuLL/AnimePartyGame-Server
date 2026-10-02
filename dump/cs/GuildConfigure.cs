using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GuildConfigure : IMessage<GuildConfigure>, IMessage, IEquatable<GuildConfigure>, IDeepCloneable<GuildConfigure>, IBufferMessage
{
	private static readonly MessageParser<GuildConfigure> _parser = new MessageParser<GuildConfigure>(() => new GuildConfigure());

	private UnknownFieldSet _unknownFields;

	public const int AuthoritysFieldNumber = 1;

	private static readonly FieldCodec<GuildAuthorityConfigure> _repeated_authoritys_codec = FieldCodec.ForMessage(10u, GuildAuthorityConfigure.Parser);

	private readonly RepeatedField<GuildAuthorityConfigure> authoritys_ = new RepeatedField<GuildAuthorityConfigure>();

	public const int AuthorityDictFieldNumber = 2;

	private static readonly MapField<int, GuildAuthorityConfigure>.Codec _map_authorityDict_codec = new MapField<int, GuildAuthorityConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GuildAuthorityConfigure.Parser), 18u);

	private readonly MapField<int, GuildAuthorityConfigure> authorityDict_ = new MapField<int, GuildAuthorityConfigure>();

	public const int CreatesFieldNumber = 3;

	private static readonly FieldCodec<GuildCreateConfigure> _repeated_creates_codec = FieldCodec.ForMessage(26u, GuildCreateConfigure.Parser);

	private readonly RepeatedField<GuildCreateConfigure> creates_ = new RepeatedField<GuildCreateConfigure>();

	public const int CreateDictFieldNumber = 4;

	private static readonly MapField<int, GuildCreateConfigure>.Codec _map_createDict_codec = new MapField<int, GuildCreateConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GuildCreateConfigure.Parser), 34u);

	private readonly MapField<int, GuildCreateConfigure> createDict_ = new MapField<int, GuildCreateConfigure>();

	public const int MissionsFieldNumber = 5;

	private static readonly FieldCodec<GuildMissionConfigure> _repeated_missions_codec = FieldCodec.ForMessage(42u, GuildMissionConfigure.Parser);

	private readonly RepeatedField<GuildMissionConfigure> missions_ = new RepeatedField<GuildMissionConfigure>();

	public const int MissionDictFieldNumber = 6;

	private static readonly MapField<int, GuildMissionConfigure>.Codec _map_missionDict_codec = new MapField<int, GuildMissionConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GuildMissionConfigure.Parser), 50u);

	private readonly MapField<int, GuildMissionConfigure> missionDict_ = new MapField<int, GuildMissionConfigure>();

	public const int MemberChangeNotificationsFieldNumber = 7;

	private static readonly FieldCodec<GuildMemberChangeNotificationConfigure> _repeated_memberChangeNotifications_codec = FieldCodec.ForMessage(58u, GuildMemberChangeNotificationConfigure.Parser);

	private readonly RepeatedField<GuildMemberChangeNotificationConfigure> memberChangeNotifications_ = new RepeatedField<GuildMemberChangeNotificationConfigure>();

	public const int MemberChangeNotificationDictFieldNumber = 8;

	private static readonly MapField<int, GuildMemberChangeNotificationConfigure>.Codec _map_memberChangeNotificationDict_codec = new MapField<int, GuildMemberChangeNotificationConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GuildMemberChangeNotificationConfigure.Parser), 66u);

	private readonly MapField<int, GuildMemberChangeNotificationConfigure> memberChangeNotificationDict_ = new MapField<int, GuildMemberChangeNotificationConfigure>();

	public const int TagsFieldNumber = 9;

	private static readonly FieldCodec<GuildTagConfigure> _repeated_tags_codec = FieldCodec.ForMessage(74u, GuildTagConfigure.Parser);

	private readonly RepeatedField<GuildTagConfigure> tags_ = new RepeatedField<GuildTagConfigure>();

	public const int TagDictFieldNumber = 10;

	private static readonly MapField<int, GuildTagConfigure>.Codec _map_tagDict_codec = new MapField<int, GuildTagConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GuildTagConfigure.Parser), 82u);

	private readonly MapField<int, GuildTagConfigure> tagDict_ = new MapField<int, GuildTagConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GuildConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GuildReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GuildAuthorityConfigure> Authoritys => authoritys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GuildAuthorityConfigure> AuthorityDict => authorityDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GuildCreateConfigure> Creates => creates_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GuildCreateConfigure> CreateDict => createDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GuildMissionConfigure> Missions => missions_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GuildMissionConfigure> MissionDict => missionDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GuildMemberChangeNotificationConfigure> MemberChangeNotifications => memberChangeNotifications_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GuildMemberChangeNotificationConfigure> MemberChangeNotificationDict => memberChangeNotificationDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GuildTagConfigure> Tags => tags_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GuildTagConfigure> TagDict => tagDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildConfigure(GuildConfigure other)
		: this()
	{
		authoritys_ = other.authoritys_.Clone();
		authorityDict_ = other.authorityDict_.Clone();
		creates_ = other.creates_.Clone();
		createDict_ = other.createDict_.Clone();
		missions_ = other.missions_.Clone();
		missionDict_ = other.missionDict_.Clone();
		memberChangeNotifications_ = other.memberChangeNotifications_.Clone();
		memberChangeNotificationDict_ = other.memberChangeNotificationDict_.Clone();
		tags_ = other.tags_.Clone();
		tagDict_ = other.tagDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildConfigure Clone()
	{
		return new GuildConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GuildConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GuildConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!authoritys_.Equals(other.authoritys_))
		{
			return false;
		}
		if (!AuthorityDict.Equals(other.AuthorityDict))
		{
			return false;
		}
		if (!creates_.Equals(other.creates_))
		{
			return false;
		}
		if (!CreateDict.Equals(other.CreateDict))
		{
			return false;
		}
		if (!missions_.Equals(other.missions_))
		{
			return false;
		}
		if (!MissionDict.Equals(other.MissionDict))
		{
			return false;
		}
		if (!memberChangeNotifications_.Equals(other.memberChangeNotifications_))
		{
			return false;
		}
		if (!MemberChangeNotificationDict.Equals(other.MemberChangeNotificationDict))
		{
			return false;
		}
		if (!tags_.Equals(other.tags_))
		{
			return false;
		}
		if (!TagDict.Equals(other.TagDict))
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
		num ^= authoritys_.GetHashCode();
		num ^= AuthorityDict.GetHashCode();
		num ^= creates_.GetHashCode();
		num ^= CreateDict.GetHashCode();
		num ^= missions_.GetHashCode();
		num ^= MissionDict.GetHashCode();
		num ^= memberChangeNotifications_.GetHashCode();
		num ^= MemberChangeNotificationDict.GetHashCode();
		num ^= tags_.GetHashCode();
		num ^= TagDict.GetHashCode();
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
		authoritys_.WriteTo(ref output, _repeated_authoritys_codec);
		authorityDict_.WriteTo(ref output, _map_authorityDict_codec);
		creates_.WriteTo(ref output, _repeated_creates_codec);
		createDict_.WriteTo(ref output, _map_createDict_codec);
		missions_.WriteTo(ref output, _repeated_missions_codec);
		missionDict_.WriteTo(ref output, _map_missionDict_codec);
		memberChangeNotifications_.WriteTo(ref output, _repeated_memberChangeNotifications_codec);
		memberChangeNotificationDict_.WriteTo(ref output, _map_memberChangeNotificationDict_codec);
		tags_.WriteTo(ref output, _repeated_tags_codec);
		tagDict_.WriteTo(ref output, _map_tagDict_codec);
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
		num += authoritys_.CalculateSize(_repeated_authoritys_codec);
		num += authorityDict_.CalculateSize(_map_authorityDict_codec);
		num += creates_.CalculateSize(_repeated_creates_codec);
		num += createDict_.CalculateSize(_map_createDict_codec);
		num += missions_.CalculateSize(_repeated_missions_codec);
		num += missionDict_.CalculateSize(_map_missionDict_codec);
		num += memberChangeNotifications_.CalculateSize(_repeated_memberChangeNotifications_codec);
		num += memberChangeNotificationDict_.CalculateSize(_map_memberChangeNotificationDict_codec);
		num += tags_.CalculateSize(_repeated_tags_codec);
		num += tagDict_.CalculateSize(_map_tagDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GuildConfigure other)
	{
		if (other != null)
		{
			authoritys_.Add(other.authoritys_);
			authorityDict_.MergeFrom(other.authorityDict_);
			creates_.Add(other.creates_);
			createDict_.MergeFrom(other.createDict_);
			missions_.Add(other.missions_);
			missionDict_.MergeFrom(other.missionDict_);
			memberChangeNotifications_.Add(other.memberChangeNotifications_);
			memberChangeNotificationDict_.MergeFrom(other.memberChangeNotificationDict_);
			tags_.Add(other.tags_);
			tagDict_.MergeFrom(other.tagDict_);
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
				authoritys_.AddEntriesFrom(ref input, _repeated_authoritys_codec);
				break;
			case 18u:
				authorityDict_.AddEntriesFrom(ref input, _map_authorityDict_codec);
				break;
			case 26u:
				creates_.AddEntriesFrom(ref input, _repeated_creates_codec);
				break;
			case 34u:
				createDict_.AddEntriesFrom(ref input, _map_createDict_codec);
				break;
			case 42u:
				missions_.AddEntriesFrom(ref input, _repeated_missions_codec);
				break;
			case 50u:
				missionDict_.AddEntriesFrom(ref input, _map_missionDict_codec);
				break;
			case 58u:
				memberChangeNotifications_.AddEntriesFrom(ref input, _repeated_memberChangeNotifications_codec);
				break;
			case 66u:
				memberChangeNotificationDict_.AddEntriesFrom(ref input, _map_memberChangeNotificationDict_codec);
				break;
			case 74u:
				tags_.AddEntriesFrom(ref input, _repeated_tags_codec);
				break;
			case 82u:
				tagDict_.AddEntriesFrom(ref input, _map_tagDict_codec);
				break;
			}
		}
	}
}
