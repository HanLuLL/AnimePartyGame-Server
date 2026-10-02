using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BotConfigure : IMessage<BotConfigure>, IMessage, IEquatable<BotConfigure>, IDeepCloneable<BotConfigure>, IBufferMessage
{
	private static readonly MessageParser<BotConfigure> _parser = new MessageParser<BotConfigure>(() => new BotConfigure());

	private UnknownFieldSet _unknownFields;

	public const int AccountsFieldNumber = 1;

	private static readonly FieldCodec<BotAccountConfigure> _repeated_accounts_codec = FieldCodec.ForMessage(10u, BotAccountConfigure.Parser);

	private readonly RepeatedField<BotAccountConfigure> accounts_ = new RepeatedField<BotAccountConfigure>();

	public const int AccountDictFieldNumber = 2;

	private static readonly MapField<int, BotAccountConfigure>.Codec _map_accountDict_codec = new MapField<int, BotAccountConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BotAccountConfigure.Parser), 18u);

	private readonly MapField<int, BotAccountConfigure> accountDict_ = new MapField<int, BotAccountConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BotConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BotReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BotAccountConfigure> Accounts => accounts_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BotAccountConfigure> AccountDict => accountDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BotConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BotConfigure(BotConfigure other)
		: this()
	{
		accounts_ = other.accounts_.Clone();
		accountDict_ = other.accountDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BotConfigure Clone()
	{
		return new BotConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BotConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BotConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!accounts_.Equals(other.accounts_))
		{
			return false;
		}
		if (!AccountDict.Equals(other.AccountDict))
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
		num ^= accounts_.GetHashCode();
		num ^= AccountDict.GetHashCode();
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
		accounts_.WriteTo(ref output, _repeated_accounts_codec);
		accountDict_.WriteTo(ref output, _map_accountDict_codec);
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
		num += accounts_.CalculateSize(_repeated_accounts_codec);
		num += accountDict_.CalculateSize(_map_accountDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BotConfigure other)
	{
		if (other != null)
		{
			accounts_.Add(other.accounts_);
			accountDict_.MergeFrom(other.accountDict_);
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
				accounts_.AddEntriesFrom(ref input, _repeated_accounts_codec);
				break;
			case 18u:
				accountDict_.AddEntriesFrom(ref input, _map_accountDict_codec);
				break;
			}
		}
	}
}
