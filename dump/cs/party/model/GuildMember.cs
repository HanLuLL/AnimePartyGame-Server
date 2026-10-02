using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class GuildMember : IMessage<GuildMember>, IMessage, IEquatable<GuildMember>, IDeepCloneable<GuildMember>, IBufferMessage
{
	private static readonly MessageParser<GuildMember> _parser = new MessageParser<GuildMember>(() => new GuildMember());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int RoleFieldNumber = 2;

	private int role_;

	public const int WeeklyActivityFieldNumber = 3;

	private int weeklyActivity_;

	public const int LastWeeklyActivityFieldNumber = 4;

	private int lastWeeklyActivity_;

	public const int LastLoginTimeFieldNumber = 5;

	private long lastLoginTime_;

	public const int WeeklySignFieldNumber = 6;

	private bool weeklySign_;

	public const int PlayerNameFieldNumber = 7;

	private string playerName_ = "";

	public const int OnlineFieldNumber = 8;

	private bool online_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GuildMember> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[115];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public int Role
	{
		get
		{
			return role_;
		}
		set
		{
			role_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WeeklyActivity
	{
		get
		{
			return weeklyActivity_;
		}
		set
		{
			weeklyActivity_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LastWeeklyActivity
	{
		get
		{
			return lastWeeklyActivity_;
		}
		set
		{
			lastWeeklyActivity_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastLoginTime
	{
		get
		{
			return lastLoginTime_;
		}
		set
		{
			lastLoginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool WeeklySign
	{
		get
		{
			return weeklySign_;
		}
		set
		{
			weeklySign_ = value;
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
	public bool Online
	{
		get
		{
			return online_;
		}
		set
		{
			online_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMember()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMember(GuildMember other)
		: this()
	{
		playerId_ = other.playerId_;
		role_ = other.role_;
		weeklyActivity_ = other.weeklyActivity_;
		lastWeeklyActivity_ = other.lastWeeklyActivity_;
		lastLoginTime_ = other.lastLoginTime_;
		weeklySign_ = other.weeklySign_;
		playerName_ = other.playerName_;
		online_ = other.online_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMember Clone()
	{
		return new GuildMember(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GuildMember);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GuildMember other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (Role != other.Role)
		{
			return false;
		}
		if (WeeklyActivity != other.WeeklyActivity)
		{
			return false;
		}
		if (LastWeeklyActivity != other.LastWeeklyActivity)
		{
			return false;
		}
		if (LastLoginTime != other.LastLoginTime)
		{
			return false;
		}
		if (WeeklySign != other.WeeklySign)
		{
			return false;
		}
		if (PlayerName != other.PlayerName)
		{
			return false;
		}
		if (Online != other.Online)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (Role != 0)
		{
			num ^= Role.GetHashCode();
		}
		if (WeeklyActivity != 0)
		{
			num ^= WeeklyActivity.GetHashCode();
		}
		if (LastWeeklyActivity != 0)
		{
			num ^= LastWeeklyActivity.GetHashCode();
		}
		if (LastLoginTime != 0L)
		{
			num ^= LastLoginTime.GetHashCode();
		}
		if (WeeklySign)
		{
			num ^= WeeklySign.GetHashCode();
		}
		if (PlayerName.Length != 0)
		{
			num ^= PlayerName.GetHashCode();
		}
		if (Online)
		{
			num ^= Online.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (Role != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Role);
		}
		if (WeeklyActivity != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(WeeklyActivity);
		}
		if (LastWeeklyActivity != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(LastWeeklyActivity);
		}
		if (LastLoginTime != 0L)
		{
			output.WriteRawTag(41);
			output.WriteSFixed64(LastLoginTime);
		}
		if (WeeklySign)
		{
			output.WriteRawTag(48);
			output.WriteBool(WeeklySign);
		}
		if (PlayerName.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(PlayerName);
		}
		if (Online)
		{
			output.WriteRawTag(64);
			output.WriteBool(Online);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (Role != 0)
		{
			num += 5;
		}
		if (WeeklyActivity != 0)
		{
			num += 5;
		}
		if (LastWeeklyActivity != 0)
		{
			num += 5;
		}
		if (LastLoginTime != 0L)
		{
			num += 9;
		}
		if (WeeklySign)
		{
			num += 2;
		}
		if (PlayerName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PlayerName);
		}
		if (Online)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GuildMember other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.Role != 0)
			{
				Role = other.Role;
			}
			if (other.WeeklyActivity != 0)
			{
				WeeklyActivity = other.WeeklyActivity;
			}
			if (other.LastWeeklyActivity != 0)
			{
				LastWeeklyActivity = other.LastWeeklyActivity;
			}
			if (other.LastLoginTime != 0L)
			{
				LastLoginTime = other.LastLoginTime;
			}
			if (other.WeeklySign)
			{
				WeeklySign = other.WeeklySign;
			}
			if (other.PlayerName.Length != 0)
			{
				PlayerName = other.PlayerName;
			}
			if (other.Online)
			{
				Online = other.Online;
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
				PlayerId = input.ReadSFixed64();
				break;
			case 21u:
				Role = input.ReadSFixed32();
				break;
			case 29u:
				WeeklyActivity = input.ReadSFixed32();
				break;
			case 37u:
				LastWeeklyActivity = input.ReadSFixed32();
				break;
			case 41u:
				LastLoginTime = input.ReadSFixed64();
				break;
			case 48u:
				WeeklySign = input.ReadBool();
				break;
			case 58u:
				PlayerName = input.ReadString();
				break;
			case 64u:
				Online = input.ReadBool();
				break;
			}
		}
	}
}
