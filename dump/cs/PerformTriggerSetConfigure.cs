using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class PerformTriggerSetConfigure : IMessage<PerformTriggerSetConfigure>, IMessage, IEquatable<PerformTriggerSetConfigure>, IDeepCloneable<PerformTriggerSetConfigure>, IBufferMessage
{
	private static readonly MessageParser<PerformTriggerSetConfigure> _parser = new MessageParser<PerformTriggerSetConfigure>(() => new PerformTriggerSetConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int PerformTriggerSetConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<PerformTriggerSetConfigureItem> _repeated_performTriggerSetConfigureItems_codec = FieldCodec.ForMessage(18u, PerformTriggerSetConfigureItem.Parser);

	private readonly RepeatedField<PerformTriggerSetConfigureItem> performTriggerSetConfigureItems_ = new RepeatedField<PerformTriggerSetConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PerformTriggerSetConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PerformReflection.Descriptor.MessageTypes[2];

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
	public RepeatedField<PerformTriggerSetConfigureItem> PerformTriggerSetConfigureItems => performTriggerSetConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformTriggerSetConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformTriggerSetConfigure(PerformTriggerSetConfigure other)
		: this()
	{
		id_ = other.id_;
		performTriggerSetConfigureItems_ = other.performTriggerSetConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformTriggerSetConfigure Clone()
	{
		return new PerformTriggerSetConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PerformTriggerSetConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PerformTriggerSetConfigure other)
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
		if (!performTriggerSetConfigureItems_.Equals(other.performTriggerSetConfigureItems_))
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
		num ^= performTriggerSetConfigureItems_.GetHashCode();
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
		performTriggerSetConfigureItems_.WriteTo(ref output, _repeated_performTriggerSetConfigureItems_codec);
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
		num += performTriggerSetConfigureItems_.CalculateSize(_repeated_performTriggerSetConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PerformTriggerSetConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			performTriggerSetConfigureItems_.Add(other.performTriggerSetConfigureItems_);
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
				performTriggerSetConfigureItems_.AddEntriesFrom(ref input, _repeated_performTriggerSetConfigureItems_codec);
				break;
			}
		}
	}
}
