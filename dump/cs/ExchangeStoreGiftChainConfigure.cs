using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ExchangeStoreGiftChainConfigure : IMessage<ExchangeStoreGiftChainConfigure>, IMessage, IEquatable<ExchangeStoreGiftChainConfigure>, IDeepCloneable<ExchangeStoreGiftChainConfigure>, IBufferMessage
{
	private static readonly MessageParser<ExchangeStoreGiftChainConfigure> _parser = new MessageParser<ExchangeStoreGiftChainConfigure>(() => new ExchangeStoreGiftChainConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ExchangeStoreGiftChainConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<ExchangeStoreGiftChainConfigureItem> _repeated_exchangeStoreGiftChainConfigureItems_codec = FieldCodec.ForMessage(18u, ExchangeStoreGiftChainConfigureItem.Parser);

	private readonly RepeatedField<ExchangeStoreGiftChainConfigureItem> exchangeStoreGiftChainConfigureItems_ = new RepeatedField<ExchangeStoreGiftChainConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExchangeStoreGiftChainConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ExchangeStoreReflection.Descriptor.MessageTypes[6];

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
	public RepeatedField<ExchangeStoreGiftChainConfigureItem> ExchangeStoreGiftChainConfigureItems => exchangeStoreGiftChainConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGiftChainConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGiftChainConfigure(ExchangeStoreGiftChainConfigure other)
		: this()
	{
		id_ = other.id_;
		exchangeStoreGiftChainConfigureItems_ = other.exchangeStoreGiftChainConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGiftChainConfigure Clone()
	{
		return new ExchangeStoreGiftChainConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExchangeStoreGiftChainConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExchangeStoreGiftChainConfigure other)
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
		if (!exchangeStoreGiftChainConfigureItems_.Equals(other.exchangeStoreGiftChainConfigureItems_))
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
		num ^= exchangeStoreGiftChainConfigureItems_.GetHashCode();
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
		exchangeStoreGiftChainConfigureItems_.WriteTo(ref output, _repeated_exchangeStoreGiftChainConfigureItems_codec);
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
		num += exchangeStoreGiftChainConfigureItems_.CalculateSize(_repeated_exchangeStoreGiftChainConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ExchangeStoreGiftChainConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			exchangeStoreGiftChainConfigureItems_.Add(other.exchangeStoreGiftChainConfigureItems_);
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
				exchangeStoreGiftChainConfigureItems_.AddEntriesFrom(ref input, _repeated_exchangeStoreGiftChainConfigureItems_codec);
				break;
			}
		}
	}
}
