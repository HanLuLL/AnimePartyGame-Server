using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SkinStandingPaintingConfigure : IMessage<SkinStandingPaintingConfigure>, IMessage, IEquatable<SkinStandingPaintingConfigure>, IDeepCloneable<SkinStandingPaintingConfigure>, IBufferMessage
{
	private static readonly MessageParser<SkinStandingPaintingConfigure> _parser = new MessageParser<SkinStandingPaintingConfigure>(() => new SkinStandingPaintingConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int SkinStandingPaintingConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<SkinStandingPaintingConfigureItem> _repeated_skinStandingPaintingConfigureItems_codec = FieldCodec.ForMessage(18u, SkinStandingPaintingConfigureItem.Parser);

	private readonly RepeatedField<SkinStandingPaintingConfigureItem> skinStandingPaintingConfigureItems_ = new RepeatedField<SkinStandingPaintingConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SkinStandingPaintingConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SkinReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<SkinStandingPaintingConfigureItem> SkinStandingPaintingConfigureItems => skinStandingPaintingConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinStandingPaintingConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinStandingPaintingConfigure(SkinStandingPaintingConfigure other)
		: this()
	{
		id_ = other.id_;
		skinStandingPaintingConfigureItems_ = other.skinStandingPaintingConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinStandingPaintingConfigure Clone()
	{
		return new SkinStandingPaintingConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SkinStandingPaintingConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SkinStandingPaintingConfigure other)
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
		if (!skinStandingPaintingConfigureItems_.Equals(other.skinStandingPaintingConfigureItems_))
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
		num ^= skinStandingPaintingConfigureItems_.GetHashCode();
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
		skinStandingPaintingConfigureItems_.WriteTo(ref output, _repeated_skinStandingPaintingConfigureItems_codec);
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
		num += skinStandingPaintingConfigureItems_.CalculateSize(_repeated_skinStandingPaintingConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SkinStandingPaintingConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			skinStandingPaintingConfigureItems_.Add(other.skinStandingPaintingConfigureItems_);
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
				skinStandingPaintingConfigureItems_.AddEntriesFrom(ref input, _repeated_skinStandingPaintingConfigureItems_codec);
				break;
			}
		}
	}

	private SkinStandingPaintingConfigureItem GetStandingPaintingItem(int index)
	{
		RepeatedField<SkinStandingPaintingConfigureItem> repeatedField = skinStandingPaintingConfigureItems_;
		for (int i = 0; i < repeatedField.Count; i++)
		{
			if (repeatedField[i].Index == index)
			{
				return repeatedField[i];
			}
		}
		return null;
	}
}
