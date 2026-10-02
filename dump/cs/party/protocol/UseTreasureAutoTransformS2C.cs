using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class UseTreasureAutoTransformS2C : IMessage<UseTreasureAutoTransformS2C>, IMessage, IEquatable<UseTreasureAutoTransformS2C>, IDeepCloneable<UseTreasureAutoTransformS2C>, IBufferMessage
{
	private static readonly MessageParser<UseTreasureAutoTransformS2C> _parser = new MessageParser<UseTreasureAutoTransformS2C>(() => new UseTreasureAutoTransformS2C());

	private UnknownFieldSet _unknownFields;

	public const int ItemsFieldNumber = 1;

	private static readonly FieldCodec<ItemEtc> _repeated_items_codec = FieldCodec.ForMessage(10u, ItemEtc.Parser);

	private readonly RepeatedField<ItemEtc> items_ = new RepeatedField<ItemEtc>();

	public const int TransformItemsFieldNumber = 2;

	private static readonly FieldCodec<ItemEtc> _repeated_transformItems_codec = FieldCodec.ForMessage(18u, ItemEtc.Parser);

	private readonly RepeatedField<ItemEtc> transformItems_ = new RepeatedField<ItemEtc>();

	public const int TransformsFieldNumber = 3;

	private static readonly FieldCodec<ItemTransformPair> _repeated_transforms_codec = FieldCodec.ForMessage(26u, ItemTransformPair.Parser);

	private readonly RepeatedField<ItemTransformPair> transforms_ = new RepeatedField<ItemTransformPair>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UseTreasureAutoTransformS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[195];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemEtc> Items => items_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemEtc> TransformItems => transformItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemTransformPair> Transforms => transforms_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseTreasureAutoTransformS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseTreasureAutoTransformS2C(UseTreasureAutoTransformS2C other)
		: this()
	{
		items_ = other.items_.Clone();
		transformItems_ = other.transformItems_.Clone();
		transforms_ = other.transforms_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseTreasureAutoTransformS2C Clone()
	{
		return new UseTreasureAutoTransformS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UseTreasureAutoTransformS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UseTreasureAutoTransformS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!items_.Equals(other.items_))
		{
			return false;
		}
		if (!transformItems_.Equals(other.transformItems_))
		{
			return false;
		}
		if (!transforms_.Equals(other.transforms_))
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
		num ^= items_.GetHashCode();
		num ^= transformItems_.GetHashCode();
		num ^= transforms_.GetHashCode();
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
		items_.WriteTo(ref output, _repeated_items_codec);
		transformItems_.WriteTo(ref output, _repeated_transformItems_codec);
		transforms_.WriteTo(ref output, _repeated_transforms_codec);
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
		num += items_.CalculateSize(_repeated_items_codec);
		num += transformItems_.CalculateSize(_repeated_transformItems_codec);
		num += transforms_.CalculateSize(_repeated_transforms_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(UseTreasureAutoTransformS2C other)
	{
		if (other != null)
		{
			items_.Add(other.items_);
			transformItems_.Add(other.transformItems_);
			transforms_.Add(other.transforms_);
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
				items_.AddEntriesFrom(ref input, _repeated_items_codec);
				break;
			case 18u:
				transformItems_.AddEntriesFrom(ref input, _repeated_transformItems_codec);
				break;
			case 26u:
				transforms_.AddEntriesFrom(ref input, _repeated_transforms_codec);
				break;
			}
		}
	}
}
