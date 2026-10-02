using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MutatorPoolComposeConfigure : IMessage<MutatorPoolComposeConfigure>, IMessage, IEquatable<MutatorPoolComposeConfigure>, IDeepCloneable<MutatorPoolComposeConfigure>, IBufferMessage
{
	private static readonly MessageParser<MutatorPoolComposeConfigure> _parser = new MessageParser<MutatorPoolComposeConfigure>(() => new MutatorPoolComposeConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int MutatorPoolComposeConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<MutatorPoolComposeConfigureItem> _repeated_mutatorPoolComposeConfigureItems_codec = FieldCodec.ForMessage(18u, MutatorPoolComposeConfigureItem.Parser);

	private readonly RepeatedField<MutatorPoolComposeConfigureItem> mutatorPoolComposeConfigureItems_ = new RepeatedField<MutatorPoolComposeConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MutatorPoolComposeConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MutatorReflection.Descriptor.MessageTypes[3];

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
	public RepeatedField<MutatorPoolComposeConfigureItem> MutatorPoolComposeConfigureItems => mutatorPoolComposeConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MutatorPoolComposeConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MutatorPoolComposeConfigure(MutatorPoolComposeConfigure other)
		: this()
	{
		id_ = other.id_;
		mutatorPoolComposeConfigureItems_ = other.mutatorPoolComposeConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MutatorPoolComposeConfigure Clone()
	{
		return new MutatorPoolComposeConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MutatorPoolComposeConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MutatorPoolComposeConfigure other)
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
		if (!mutatorPoolComposeConfigureItems_.Equals(other.mutatorPoolComposeConfigureItems_))
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
		num ^= mutatorPoolComposeConfigureItems_.GetHashCode();
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
		mutatorPoolComposeConfigureItems_.WriteTo(ref output, _repeated_mutatorPoolComposeConfigureItems_codec);
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
		num += mutatorPoolComposeConfigureItems_.CalculateSize(_repeated_mutatorPoolComposeConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MutatorPoolComposeConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			mutatorPoolComposeConfigureItems_.Add(other.mutatorPoolComposeConfigureItems_);
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
				mutatorPoolComposeConfigureItems_.AddEntriesFrom(ref input, _repeated_mutatorPoolComposeConfigureItems_codec);
				break;
			}
		}
	}
}
