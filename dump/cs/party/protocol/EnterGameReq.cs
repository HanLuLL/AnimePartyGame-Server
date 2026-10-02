using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class EnterGameReq : IMessage<EnterGameReq>, IMessage, IEquatable<EnterGameReq>, IDeepCloneable<EnterGameReq>, IBufferMessage
{
	private static readonly MessageParser<EnterGameReq> _parser = new MessageParser<EnterGameReq>(() => new EnterGameReq());

	private UnknownFieldSet _unknownFields;

	public const int RandKeyFieldNumber = 1;

	private string randKey_ = "";

	public const int AccountFieldNumber = 2;

	private AccountInfo account_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<EnterGameReq> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => InteriorReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string RandKey
	{
		get
		{
			return randKey_;
		}
		set
		{
			randKey_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public EnterGameReq()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EnterGameReq(EnterGameReq other)
		: this()
	{
		randKey_ = other.randKey_;
		account_ = ((other.account_ != null) ? other.account_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EnterGameReq Clone()
	{
		return new EnterGameReq(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as EnterGameReq);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(EnterGameReq other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (RandKey != other.RandKey)
		{
			return false;
		}
		if (!object.Equals(Account, other.Account))
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
		if (RandKey.Length != 0)
		{
			num ^= RandKey.GetHashCode();
		}
		if (account_ != null)
		{
			num ^= Account.GetHashCode();
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
		if (RandKey.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(RandKey);
		}
		if (account_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(Account);
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
		if (RandKey.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(RandKey);
		}
		if (account_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Account);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(EnterGameReq other)
	{
		if (other == null)
		{
			return;
		}
		if (other.RandKey.Length != 0)
		{
			RandKey = other.RandKey;
		}
		if (other.account_ != null)
		{
			if (account_ == null)
			{
				Account = new AccountInfo();
			}
			Account.MergeFrom(other.Account);
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
				RandKey = input.ReadString();
				break;
			case 18u:
				if (account_ == null)
				{
					Account = new AccountInfo();
				}
				input.ReadMessage(Account);
				break;
			}
		}
	}
}
