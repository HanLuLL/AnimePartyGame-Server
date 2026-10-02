using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class ConnectS2C : IMessage<ConnectS2C>, IMessage, IEquatable<ConnectS2C>, IDeepCloneable<ConnectS2C>, IBufferMessage
{
	private static readonly MessageParser<ConnectS2C> _parser = new MessageParser<ConnectS2C>(() => new ConnectS2C());

	private UnknownFieldSet _unknownFields;

	public const int CipherKeyFieldNumber = 1;

	private string cipherKey_ = "";

	public const int SessionIdFieldNumber = 2;

	private long sessionId_;

	public const int AccountFieldNumber = 3;

	private AccountInfo account_;

	public const int PlayerFieldNumber = 4;

	private Player player_;

	public const int QueueTimeFieldNumber = 5;

	private int queueTime_;

	public const int DataFieldNumber = 6;

	private string data_ = "";

	public const int BanTimeFieldNumber = 7;

	private long banTime_;

	public const int NowTimeFieldNumber = 8;

	private long nowTime_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ConnectS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[6];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CipherKey
	{
		get
		{
			return cipherKey_;
		}
		set
		{
			cipherKey_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long SessionId
	{
		get
		{
			return sessionId_;
		}
		set
		{
			sessionId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AccountInfo Account
	{
		get
		{
			return account_;
		}
		set
		{
			account_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Player Player
	{
		get
		{
			return player_;
		}
		set
		{
			player_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int QueueTime
	{
		get
		{
			return queueTime_;
		}
		set
		{
			queueTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Data
	{
		get
		{
			return data_;
		}
		set
		{
			data_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long BanTime
	{
		get
		{
			return banTime_;
		}
		set
		{
			banTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long NowTime
	{
		get
		{
			return nowTime_;
		}
		set
		{
			nowTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConnectS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConnectS2C(ConnectS2C other)
		: this()
	{
		cipherKey_ = other.cipherKey_;
		sessionId_ = other.sessionId_;
		account_ = ((other.account_ != null) ? other.account_.Clone() : null);
		player_ = ((other.player_ != null) ? other.player_.Clone() : null);
		queueTime_ = other.queueTime_;
		data_ = other.data_;
		banTime_ = other.banTime_;
		nowTime_ = other.nowTime_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConnectS2C Clone()
	{
		return new ConnectS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ConnectS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ConnectS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (CipherKey != other.CipherKey)
		{
			return false;
		}
		if (SessionId != other.SessionId)
		{
			return false;
		}
		if (!object.Equals(Account, other.Account))
		{
			return false;
		}
		if (!object.Equals(Player, other.Player))
		{
			return false;
		}
		if (QueueTime != other.QueueTime)
		{
			return false;
		}
		if (Data != other.Data)
		{
			return false;
		}
		if (BanTime != other.BanTime)
		{
			return false;
		}
		if (NowTime != other.NowTime)
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
		if (CipherKey.Length != 0)
		{
			num ^= CipherKey.GetHashCode();
		}
		if (SessionId != 0L)
		{
			num ^= SessionId.GetHashCode();
		}
		if (account_ != null)
		{
			num ^= Account.GetHashCode();
		}
		if (player_ != null)
		{
			num ^= Player.GetHashCode();
		}
		if (QueueTime != 0)
		{
			num ^= QueueTime.GetHashCode();
		}
		if (Data.Length != 0)
		{
			num ^= Data.GetHashCode();
		}
		if (BanTime != 0L)
		{
			num ^= BanTime.GetHashCode();
		}
		if (NowTime != 0L)
		{
			num ^= NowTime.GetHashCode();
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
		if (CipherKey.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(CipherKey);
		}
		if (SessionId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(SessionId);
		}
		if (account_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(Account);
		}
		if (player_ != null)
		{
			output.WriteRawTag(34);
			output.WriteMessage(Player);
		}
		if (QueueTime != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(QueueTime);
		}
		if (Data.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(Data);
		}
		if (BanTime != 0L)
		{
			output.WriteRawTag(57);
			output.WriteSFixed64(BanTime);
		}
		if (NowTime != 0L)
		{
			output.WriteRawTag(65);
			output.WriteSFixed64(NowTime);
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
		if (CipherKey.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CipherKey);
		}
		if (SessionId != 0L)
		{
			num += 9;
		}
		if (account_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Account);
		}
		if (player_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Player);
		}
		if (QueueTime != 0)
		{
			num += 5;
		}
		if (Data.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Data);
		}
		if (BanTime != 0L)
		{
			num += 9;
		}
		if (NowTime != 0L)
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
	public void MergeFrom(ConnectS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.CipherKey.Length != 0)
		{
			CipherKey = other.CipherKey;
		}
		if (other.SessionId != 0L)
		{
			SessionId = other.SessionId;
		}
		if (other.account_ != null)
		{
			if (account_ == null)
			{
				Account = new AccountInfo();
			}
			Account.MergeFrom(other.Account);
		}
		if (other.player_ != null)
		{
			if (player_ == null)
			{
				Player = new Player();
			}
			Player.MergeFrom(other.Player);
		}
		if (other.QueueTime != 0)
		{
			QueueTime = other.QueueTime;
		}
		if (other.Data.Length != 0)
		{
			Data = other.Data;
		}
		if (other.BanTime != 0L)
		{
			BanTime = other.BanTime;
		}
		if (other.NowTime != 0L)
		{
			NowTime = other.NowTime;
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
				CipherKey = input.ReadString();
				break;
			case 17u:
				SessionId = input.ReadSFixed64();
				break;
			case 26u:
				if (account_ == null)
				{
					Account = new AccountInfo();
				}
				input.ReadMessage(Account);
				break;
			case 34u:
				if (player_ == null)
				{
					Player = new Player();
				}
				input.ReadMessage(Player);
				break;
			case 45u:
				QueueTime = input.ReadSFixed32();
				break;
			case 50u:
				Data = input.ReadString();
				break;
			case 57u:
				BanTime = input.ReadSFixed64();
				break;
			case 65u:
				NowTime = input.ReadSFixed64();
				break;
			}
		}
	}
}
