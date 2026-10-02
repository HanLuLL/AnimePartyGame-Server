using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class PVENurturanceEnhancementConfigure : IMessage<PVENurturanceEnhancementConfigure>, IMessage, IEquatable<PVENurturanceEnhancementConfigure>, IDeepCloneable<PVENurturanceEnhancementConfigure>, IBufferMessage
{
	private static readonly MessageParser<PVENurturanceEnhancementConfigure> _parser = new MessageParser<PVENurturanceEnhancementConfigure>(() => new PVENurturanceEnhancementConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int PVENurturanceEnhancementConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<PVENurturanceEnhancementConfigureItem> _repeated_pVENurturanceEnhancementConfigureItems_codec = FieldCodec.ForMessage(18u, PVENurturanceEnhancementConfigureItem.Parser);

	private readonly RepeatedField<PVENurturanceEnhancementConfigureItem> pVENurturanceEnhancementConfigureItems_ = new RepeatedField<PVENurturanceEnhancementConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PVENurturanceEnhancementConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PVENurturanceReflection.Descriptor.MessageTypes[1];

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
	public RepeatedField<PVENurturanceEnhancementConfigureItem> PVENurturanceEnhancementConfigureItems => pVENurturanceEnhancementConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVENurturanceEnhancementConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVENurturanceEnhancementConfigure(PVENurturanceEnhancementConfigure other)
		: this()
	{
		id_ = other.id_;
		pVENurturanceEnhancementConfigureItems_ = other.pVENurturanceEnhancementConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVENurturanceEnhancementConfigure Clone()
	{
		return new PVENurturanceEnhancementConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PVENurturanceEnhancementConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PVENurturanceEnhancementConfigure other)
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
		if (!pVENurturanceEnhancementConfigureItems_.Equals(other.pVENurturanceEnhancementConfigureItems_))
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
		num ^= pVENurturanceEnhancementConfigureItems_.GetHashCode();
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
		pVENurturanceEnhancementConfigureItems_.WriteTo(ref output, _repeated_pVENurturanceEnhancementConfigureItems_codec);
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
		num += pVENurturanceEnhancementConfigureItems_.CalculateSize(_repeated_pVENurturanceEnhancementConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PVENurturanceEnhancementConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			pVENurturanceEnhancementConfigureItems_.Add(other.pVENurturanceEnhancementConfigureItems_);
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
				pVENurturanceEnhancementConfigureItems_.AddEntriesFrom(ref input, _repeated_pVENurturanceEnhancementConfigureItems_codec);
				break;
			}
		}
	}
}
