using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class AccountInfo : IMessage<AccountInfo>, IMessage, IEquatable<AccountInfo>, IDeepCloneable<AccountInfo>, IBufferMessage
{
	private static readonly MessageParser<AccountInfo> _parser = new MessageParser<AccountInfo>(() => new AccountInfo());

	private UnknownFieldSet _unknownFields;

	public const int AccountIdFieldNumber = 1;

	private ulong accountId_;

	public const int NickFieldNumber = 2;

	private string nick_ = "";

	public const int PlayerIdFieldNumber = 3;

	private long playerId_;

	public const int TokenFieldNumber = 4;

	private string token_ = "";

	public const int PlatFieldNumber = 5;

	private string plat_ = "";

	public const int DeviceIdFieldNumber = 6;

	private string deviceId_ = "";

	public const int SteamIdFieldNumber = 7;

	private string steamId_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AccountInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[65];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ulong AccountId
	{
		get
		{
			return accountId_;
		}
		set
		{
			accountId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Nick
	{
		get
		{
			return nick_;
		}
		set
		{
			nick_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

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
	public string Token
	{
		get
		{
			return token_;
		}
		set
		{
			token_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Plat
	{
		get
		{
			return plat_;
		}
		set
		{
			plat_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string DeviceId
	{
		get
		{
			return deviceId_;
		}
		set
		{
			deviceId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SteamId
	{
		get
		{
			return steamId_;
		}
		set
		{
			steamId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AccountInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AccountInfo(AccountInfo other)
		: this()
	{
		accountId_ = other.accountId_;
		nick_ = other.nick_;
		playerId_ = other.playerId_;
		token_ = other.token_;
		plat_ = other.plat_;
		deviceId_ = other.deviceId_;
		steamId_ = other.steamId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AccountInfo Clone()
	{
		return new AccountInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AccountInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AccountInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (AccountId != other.AccountId)
		{
			return false;
		}
		if (Nick != other.Nick)
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (Token != other.Token)
		{
			return false;
		}
		if (Plat != other.Plat)
		{
			return false;
		}
		if (DeviceId != other.DeviceId)
		{
			return false;
		}
		if (SteamId != other.SteamId)
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
		if (AccountId != 0L)
		{
			num ^= AccountId.GetHashCode();
		}
		if (Nick.Length != 0)
		{
			num ^= Nick.GetHashCode();
		}
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (Token.Length != 0)
		{
			num ^= Token.GetHashCode();
		}
		if (Plat.Length != 0)
		{
			num ^= Plat.GetHashCode();
		}
		if (DeviceId.Length != 0)
		{
			num ^= DeviceId.GetHashCode();
		}
		if (SteamId.Length != 0)
		{
			num ^= SteamId.GetHashCode();
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
		if (AccountId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteFixed64(AccountId);
		}
		if (Nick.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Nick);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(PlayerId);
		}
		if (Token.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Token);
		}
		if (Plat.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Plat);
		}
		if (DeviceId.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(DeviceId);
		}
		if (SteamId.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(SteamId);
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
		if (AccountId != 0L)
		{
			num += 9;
		}
		if (Nick.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Nick);
		}
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (Token.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Token);
		}
		if (Plat.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Plat);
		}
		if (DeviceId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DeviceId);
		}
		if (SteamId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SteamId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(AccountInfo other)
	{
		if (other != null)
		{
			if (other.AccountId != 0L)
			{
				AccountId = other.AccountId;
			}
			if (other.Nick.Length != 0)
			{
				Nick = other.Nick;
			}
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.Token.Length != 0)
			{
				Token = other.Token;
			}
			if (other.Plat.Length != 0)
			{
				Plat = other.Plat;
			}
			if (other.DeviceId.Length != 0)
			{
				DeviceId = other.DeviceId;
			}
			if (other.SteamId.Length != 0)
			{
				SteamId = other.SteamId;
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
				AccountId = input.ReadFixed64();
				break;
			case 18u:
				Nick = input.ReadString();
				break;
			case 25u:
				PlayerId = input.ReadSFixed64();
				break;
			case 34u:
				Token = input.ReadString();
				break;
			case 42u:
				Plat = input.ReadString();
				break;
			case 50u:
				DeviceId = input.ReadString();
				break;
			case 58u:
				SteamId = input.ReadString();
				break;
			}
		}
	}
}
