using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.code;
using party.model;

namespace party.protocol;

public sealed class EnterGameResp : IMessage<EnterGameResp>, IMessage, IEquatable<EnterGameResp>, IDeepCloneable<EnterGameResp>, IBufferMessage
{
	private static readonly MessageParser<EnterGameResp> _parser = new MessageParser<EnterGameResp>(() => new EnterGameResp());

	private UnknownFieldSet _unknownFields;

	public const int CodeFieldNumber = 1;

	private Code code_;

	public const int AccountFieldNumber = 2;

	private AccountInfo account_;

	public const int PlayerFieldNumber = 3;

	private Player player_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<EnterGameResp> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => InteriorReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Code Code
	{
		get
		{
			return code_;
		}
		set
		{
			code_ = value;
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
	public EnterGameResp()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EnterGameResp(EnterGameResp other)
		: this()
	{
		code_ = other.code_;
		account_ = ((other.account_ != null) ? other.account_.Clone() : null);
		player_ = ((other.player_ != null) ? other.player_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EnterGameResp Clone()
	{
		return new EnterGameResp(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as EnterGameResp);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(EnterGameResp other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Code != other.Code)
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
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Code != Code.Succ)
		{
			num ^= Code.GetHashCode();
		}
		if (account_ != null)
		{
			num ^= Account.GetHashCode();
		}
		if (player_ != null)
		{
			num ^= Player.GetHashCode();
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
		if (Code != Code.Succ)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)Code);
		}
		if (account_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(Account);
		}
		if (player_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(Player);
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
		if (Code != Code.Succ)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Code);
		}
		if (account_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Account);
		}
		if (player_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Player);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(EnterGameResp other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Code != Code.Succ)
		{
			Code = other.Code;
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
			case 8u:
				Code = (Code)input.ReadEnum();
				break;
			case 18u:
				if (account_ == null)
				{
					Account = new AccountInfo();
				}
				input.ReadMessage(Account);
				break;
			case 26u:
				if (player_ == null)
				{
					Player = new Player();
				}
				input.ReadMessage(Player);
				break;
			}
		}
	}
}
