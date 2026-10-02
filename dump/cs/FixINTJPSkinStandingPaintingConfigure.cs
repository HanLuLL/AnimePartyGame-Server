using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixINTJPSkinStandingPaintingConfigure : IMessage<FixINTJPSkinStandingPaintingConfigure>, IMessage, IEquatable<FixINTJPSkinStandingPaintingConfigure>, IDeepCloneable<FixINTJPSkinStandingPaintingConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixINTJPSkinStandingPaintingConfigure> _parser = new MessageParser<FixINTJPSkinStandingPaintingConfigure>(() => new FixINTJPSkinStandingPaintingConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int FixINTJPSkinStandingPaintingConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<FixINTJPSkinStandingPaintingConfigureItem> _repeated_fixINTJPSkinStandingPaintingConfigureItems_codec = FieldCodec.ForMessage(18u, FixINTJPSkinStandingPaintingConfigureItem.Parser);

	private readonly RepeatedField<FixINTJPSkinStandingPaintingConfigureItem> fixINTJPSkinStandingPaintingConfigureItems_ = new RepeatedField<FixINTJPSkinStandingPaintingConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixINTJPSkinStandingPaintingConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixINTJPReflection.Descriptor.MessageTypes[3];

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
	public RepeatedField<FixINTJPSkinStandingPaintingConfigureItem> FixINTJPSkinStandingPaintingConfigureItems => fixINTJPSkinStandingPaintingConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPSkinStandingPaintingConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPSkinStandingPaintingConfigure(FixINTJPSkinStandingPaintingConfigure other)
		: this()
	{
		id_ = other.id_;
		fixINTJPSkinStandingPaintingConfigureItems_ = other.fixINTJPSkinStandingPaintingConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPSkinStandingPaintingConfigure Clone()
	{
		return new FixINTJPSkinStandingPaintingConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixINTJPSkinStandingPaintingConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixINTJPSkinStandingPaintingConfigure other)
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
		if (!fixINTJPSkinStandingPaintingConfigureItems_.Equals(other.fixINTJPSkinStandingPaintingConfigureItems_))
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
		num ^= fixINTJPSkinStandingPaintingConfigureItems_.GetHashCode();
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
		fixINTJPSkinStandingPaintingConfigureItems_.WriteTo(ref output, _repeated_fixINTJPSkinStandingPaintingConfigureItems_codec);
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
		num += fixINTJPSkinStandingPaintingConfigureItems_.CalculateSize(_repeated_fixINTJPSkinStandingPaintingConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixINTJPSkinStandingPaintingConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			fixINTJPSkinStandingPaintingConfigureItems_.Add(other.fixINTJPSkinStandingPaintingConfigureItems_);
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
				fixINTJPSkinStandingPaintingConfigureItems_.AddEntriesFrom(ref input, _repeated_fixINTJPSkinStandingPaintingConfigureItems_codec);
				break;
			}
		}
	}
}
