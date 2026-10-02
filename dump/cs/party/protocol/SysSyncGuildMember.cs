using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SysSyncGuildMember : IMessage<SysSyncGuildMember>, IMessage, IEquatable<SysSyncGuildMember>, IDeepCloneable<SysSyncGuildMember>, IBufferMessage
{
	private static readonly MessageParser<SysSyncGuildMember> _parser = new MessageParser<SysSyncGuildMember>(() => new SysSyncGuildMember());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int WeeklyActivityFieldNumber = 2;

	private int weeklyActivity_;

	public const int LastLoginTimeFieldNumber = 3;

	private long lastLoginTime_;

	public const int WeeklySignInFieldNumber = 4;

	private bool weeklySignIn_;

	public const int LastLogoutTimeFieldNumber = 5;

	private long lastLogoutTime_;

	public const int PlayerNameFieldNumber = 6;

	private string playerName_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysSyncGuildMember> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => InteriorReflection.Descriptor.MessageTypes[15];

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
	public bool WeeklySignIn
	{
		get
		{
			return weeklySignIn_;
		}
		set
		{
			weeklySignIn_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastLogoutTime
	{
		get
		{
			return lastLogoutTime_;
		}
		set
		{
			lastLogoutTime_ = value;
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
	public SysSyncGuildMember()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSyncGuildMember(SysSyncGuildMember other)
		: this()
	{
		playerId_ = other.playerId_;
		weeklyActivity_ = other.weeklyActivity_;
		lastLoginTime_ = other.lastLoginTime_;
		weeklySignIn_ = other.weeklySignIn_;
		lastLogoutTime_ = other.lastLogoutTime_;
		playerName_ = other.playerName_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSyncGuildMember Clone()
	{
		return new SysSyncGuildMember(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysSyncGuildMember);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysSyncGuildMember other)
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
		if (WeeklyActivity != other.WeeklyActivity)
		{
			return false;
		}
		if (LastLoginTime != other.LastLoginTime)
		{
			return false;
		}
		if (WeeklySignIn != other.WeeklySignIn)
		{
			return false;
		}
		if (LastLogoutTime != other.LastLogoutTime)
		{
			return false;
		}
		if (PlayerName != other.PlayerName)
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
		if (WeeklyActivity != 0)
		{
			num ^= WeeklyActivity.GetHashCode();
		}
		if (LastLoginTime != 0L)
		{
			num ^= LastLoginTime.GetHashCode();
		}
		if (WeeklySignIn)
		{
			num ^= WeeklySignIn.GetHashCode();
		}
		if (LastLogoutTime != 0L)
		{
			num ^= LastLogoutTime.GetHashCode();
		}
		if (PlayerName.Length != 0)
		{
			num ^= PlayerName.GetHashCode();
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
		if (WeeklyActivity != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(WeeklyActivity);
		}
		if (LastLoginTime != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(LastLoginTime);
		}
		if (WeeklySignIn)
		{
			output.WriteRawTag(32);
			output.WriteBool(WeeklySignIn);
		}
		if (LastLogoutTime != 0L)
		{
			output.WriteRawTag(41);
			output.WriteSFixed64(LastLogoutTime);
		}
		if (PlayerName.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(PlayerName);
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
		if (WeeklyActivity != 0)
		{
			num += 5;
		}
		if (LastLoginTime != 0L)
		{
			num += 9;
		}
		if (WeeklySignIn)
		{
			num += 2;
		}
		if (LastLogoutTime != 0L)
		{
			num += 9;
		}
		if (PlayerName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PlayerName);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SysSyncGuildMember other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.WeeklyActivity != 0)
			{
				WeeklyActivity = other.WeeklyActivity;
			}
			if (other.LastLoginTime != 0L)
			{
				LastLoginTime = other.LastLoginTime;
			}
			if (other.WeeklySignIn)
			{
				WeeklySignIn = other.WeeklySignIn;
			}
			if (other.LastLogoutTime != 0L)
			{
				LastLogoutTime = other.LastLogoutTime;
			}
			if (other.PlayerName.Length != 0)
			{
				PlayerName = other.PlayerName;
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
				WeeklyActivity = input.ReadSFixed32();
				break;
			case 25u:
				LastLoginTime = input.ReadSFixed64();
				break;
			case 32u:
				WeeklySignIn = input.ReadBool();
				break;
			case 41u:
				LastLogoutTime = input.ReadSFixed64();
				break;
			case 50u:
				PlayerName = input.ReadString();
				break;
			}
		}
	}
}
