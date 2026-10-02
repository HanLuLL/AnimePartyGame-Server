using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class TaptapInfo : IMessage<TaptapInfo>, IMessage, IEquatable<TaptapInfo>, IDeepCloneable<TaptapInfo>, IBufferMessage
{
	private static readonly MessageParser<TaptapInfo> _parser = new MessageParser<TaptapInfo>(() => new TaptapInfo());

	private UnknownFieldSet _unknownFields;

	public const int KidFieldNumber = 1;

	private string kid_ = "";

	public const int AccessTokenFieldNumber = 2;

	private string accessToken_ = "";

	public const int MacKeyFieldNumber = 3;

	private string macKey_ = "";

	public const int NickFieldNumber = 4;

	private string nick_ = "";

	public const int EmailFieldNumber = 5;

	private string email_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TaptapInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Kid
	{
		get
		{
			return kid_;
		}
		set
		{
			kid_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AccessToken
	{
		get
		{
			return accessToken_;
		}
		set
		{
			accessToken_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string MacKey
	{
		get
		{
			return macKey_;
		}
		set
		{
			macKey_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public string Email
	{
		get
		{
			return email_;
		}
		set
		{
			email_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaptapInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaptapInfo(TaptapInfo other)
		: this()
	{
		kid_ = other.kid_;
		accessToken_ = other.accessToken_;
		macKey_ = other.macKey_;
		nick_ = other.nick_;
		email_ = other.email_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaptapInfo Clone()
	{
		return new TaptapInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TaptapInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TaptapInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Kid != other.Kid)
		{
			return false;
		}
		if (AccessToken != other.AccessToken)
		{
			return false;
		}
		if (MacKey != other.MacKey)
		{
			return false;
		}
		if (Nick != other.Nick)
		{
			return false;
		}
		if (Email != other.Email)
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
		if (Kid.Length != 0)
		{
			num ^= Kid.GetHashCode();
		}
		if (AccessToken.Length != 0)
		{
			num ^= AccessToken.GetHashCode();
		}
		if (MacKey.Length != 0)
		{
			num ^= MacKey.GetHashCode();
		}
		if (Nick.Length != 0)
		{
			num ^= Nick.GetHashCode();
		}
		if (Email.Length != 0)
		{
			num ^= Email.GetHashCode();
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
		if (Kid.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(Kid);
		}
		if (AccessToken.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(AccessToken);
		}
		if (MacKey.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(MacKey);
		}
		if (Nick.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Nick);
		}
		if (Email.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Email);
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
		if (Kid.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Kid);
		}
		if (AccessToken.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccessToken);
		}
		if (MacKey.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(MacKey);
		}
		if (Nick.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Nick);
		}
		if (Email.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Email);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(TaptapInfo other)
	{
		if (other != null)
		{
			if (other.Kid.Length != 0)
			{
				Kid = other.Kid;
			}
			if (other.AccessToken.Length != 0)
			{
				AccessToken = other.AccessToken;
			}
			if (other.MacKey.Length != 0)
			{
				MacKey = other.MacKey;
			}
			if (other.Nick.Length != 0)
			{
				Nick = other.Nick;
			}
			if (other.Email.Length != 0)
			{
				Email = other.Email;
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
				Kid = input.ReadString();
				break;
			case 18u:
				AccessToken = input.ReadString();
				break;
			case 26u:
				MacKey = input.ReadString();
				break;
			case 34u:
				Nick = input.ReadString();
				break;
			case 42u:
				Email = input.ReadString();
				break;
			}
		}
	}
}
