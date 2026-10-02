using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ExchangeStoreRefreshPoolConfigure : IMessage<ExchangeStoreRefreshPoolConfigure>, IMessage, IEquatable<ExchangeStoreRefreshPoolConfigure>, IDeepCloneable<ExchangeStoreRefreshPoolConfigure>, IBufferMessage
{
	private static readonly MessageParser<ExchangeStoreRefreshPoolConfigure> _parser = new MessageParser<ExchangeStoreRefreshPoolConfigure>(() => new ExchangeStoreRefreshPoolConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ExchangeStoreRefreshPoolConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<ExchangeStoreRefreshPoolConfigureItem> _repeated_exchangeStoreRefreshPoolConfigureItems_codec = FieldCodec.ForMessage(18u, ExchangeStoreRefreshPoolConfigureItem.Parser);

	private readonly RepeatedField<ExchangeStoreRefreshPoolConfigureItem> exchangeStoreRefreshPoolConfigureItems_ = new RepeatedField<ExchangeStoreRefreshPoolConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExchangeStoreRefreshPoolConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ExchangeStoreReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ExchangeStoreRefreshPoolConfigureItem> ExchangeStoreRefreshPoolConfigureItems => exchangeStoreRefreshPoolConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreRefreshPoolConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreRefreshPoolConfigure(ExchangeStoreRefreshPoolConfigure other)
		: this()
	{
		id_ = other.id_;
		exchangeStoreRefreshPoolConfigureItems_ = other.exchangeStoreRefreshPoolConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreRefreshPoolConfigure Clone()
	{
		return new ExchangeStoreRefreshPoolConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExchangeStoreRefreshPoolConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExchangeStoreRefreshPoolConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (!exchangeStoreRefreshPoolConfigureItems_.Equals(other.exchangeStoreRefreshPoolConfigureItems_))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		num ^= exchangeStoreRefreshPoolConfigureItems_.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		exchangeStoreRefreshPoolConfigureItems_.WriteTo(ref output, _repeated_exchangeStoreRefreshPoolConfigureItems_codec);
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
		if (Id != 0)
		{
			num += 5;
		}
		num += exchangeStoreRefreshPoolConfigureItems_.CalculateSize(_repeated_exchangeStoreRefreshPoolConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ExchangeStoreRefreshPoolConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			exchangeStoreRefreshPoolConfigureItems_.Add(other.exchangeStoreRefreshPoolConfigureItems_);
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
			case 13u:
				Id = input.ReadSFixed32();
				break;
			case 18u:
				exchangeStoreRefreshPoolConfigureItems_.AddEntriesFrom(ref input, _repeated_exchangeStoreRefreshPoolConfigureItems_codec);
				break;
			}
		}
	}
}
