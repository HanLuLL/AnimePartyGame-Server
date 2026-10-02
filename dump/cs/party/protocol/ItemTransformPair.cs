using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class ItemTransformPair : IMessage<ItemTransformPair>, IMessage, IEquatable<ItemTransformPair>, IDeepCloneable<ItemTransformPair>, IBufferMessage
{
	private static readonly MessageParser<ItemTransformPair> _parser = new MessageParser<ItemTransformPair>(() => new ItemTransformPair());

	private UnknownFieldSet _unknownFields;

	public const int ReplacedFieldNumber = 1;

	private ItemEtc replaced_;

	public const int GainsFieldNumber = 2;

	private static readonly FieldCodec<ItemEtc> _repeated_gains_codec = FieldCodec.ForMessage(18u, ItemEtc.Parser);

	private readonly RepeatedField<ItemEtc> gains_ = new RepeatedField<ItemEtc>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ItemTransformPair> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[194];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemEtc Replaced
	{
		get
		{
			return replaced_;
		}
		set
		{
			replaced_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemEtc> Gains => gains_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemTransformPair()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemTransformPair(ItemTransformPair other)
		: this()
	{
		replaced_ = ((other.replaced_ != null) ? other.replaced_.Clone() : null);
		gains_ = other.gains_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemTransformPair Clone()
	{
		return new ItemTransformPair(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ItemTransformPair);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ItemTransformPair other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Replaced, other.Replaced))
		{
			return false;
		}
		if (!gains_.Equals(other.gains_))
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
		if (replaced_ != null)
		{
			num ^= Replaced.GetHashCode();
		}
		num ^= gains_.GetHashCode();
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
		if (replaced_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Replaced);
		}
		gains_.WriteTo(ref output, _repeated_gains_codec);
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
		if (replaced_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Replaced);
		}
		num += gains_.CalculateSize(_repeated_gains_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ItemTransformPair other)
	{
		if (other == null)
		{
			return;
		}
		if (other.replaced_ != null)
		{
			if (replaced_ == null)
			{
				Replaced = new ItemEtc();
			}
			Replaced.MergeFrom(other.Replaced);
		}
		gains_.Add(other.gains_);
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				if (replaced_ == null)
				{
					Replaced = new ItemEtc();
				}
				input.ReadMessage(Replaced);
				break;
			case 18u:
				gains_.AddEntriesFrom(ref input, _repeated_gains_codec);
				break;
			}
		}
	}
}
