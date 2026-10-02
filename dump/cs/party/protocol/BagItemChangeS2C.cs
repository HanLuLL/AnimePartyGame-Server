using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class BagItemChangeS2C : IMessage<BagItemChangeS2C>, IMessage, IEquatable<BagItemChangeS2C>, IDeepCloneable<BagItemChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<BagItemChangeS2C> _parser = new MessageParser<BagItemChangeS2C>(() => new BagItemChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int ItemFieldNumber = 1;

	private static readonly FieldCodec<ItemEtc> _repeated_item_codec = FieldCodec.ForMessage(10u, ItemEtc.Parser);

	private readonly RepeatedField<ItemEtc> item_ = new RepeatedField<ItemEtc>();

	public const int IsNotShowFieldNumber = 2;

	private bool isNotShow_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BagItemChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[448];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemEtc> Item => item_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsNotShow
	{
		get
		{
			return isNotShow_;
		}
		set
		{
			isNotShow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BagItemChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BagItemChangeS2C(BagItemChangeS2C other)
		: this()
	{
		item_ = other.item_.Clone();
		isNotShow_ = other.isNotShow_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BagItemChangeS2C Clone()
	{
		return new BagItemChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BagItemChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BagItemChangeS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!item_.Equals(other.item_))
		{
			return false;
		}
		if (IsNotShow != other.IsNotShow)
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
		num ^= item_.GetHashCode();
		if (IsNotShow)
		{
			num ^= IsNotShow.GetHashCode();
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
		item_.WriteTo(ref output, _repeated_item_codec);
		if (IsNotShow)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsNotShow);
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
		num += item_.CalculateSize(_repeated_item_codec);
		if (IsNotShow)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BagItemChangeS2C other)
	{
		if (other != null)
		{
			item_.Add(other.item_);
			if (other.IsNotShow)
			{
				IsNotShow = other.IsNotShow;
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
				item_.AddEntriesFrom(ref input, _repeated_item_codec);
				break;
			case 16u:
				IsNotShow = input.ReadBool();
				break;
			}
		}
	}
}
