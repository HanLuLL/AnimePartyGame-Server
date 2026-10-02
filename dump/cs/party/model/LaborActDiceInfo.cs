using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class LaborActDiceInfo : IMessage<LaborActDiceInfo>, IMessage, IEquatable<LaborActDiceInfo>, IDeepCloneable<LaborActDiceInfo>, IBufferMessage
{
	private static readonly MessageParser<LaborActDiceInfo> _parser = new MessageParser<LaborActDiceInfo>(() => new LaborActDiceInfo());

	private UnknownFieldSet _unknownFields;

	public const int CurrDiceFieldNumber = 1;

	private static readonly FieldCodec<int> _repeated_currDice_codec = FieldCodec.ForSFixed32(10u);

	private readonly RepeatedField<int> currDice_ = new RepeatedField<int>();

	public const int TotalDicePointFieldNumber = 2;

	private int totalDicePoint_;

	public const int ItemFieldNumber = 3;

	private static readonly FieldCodec<ItemEtc> _repeated_item_codec = FieldCodec.ForMessage(26u, ItemEtc.Parser);

	private readonly RepeatedField<ItemEtc> item_ = new RepeatedField<ItemEtc>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LaborActDiceInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[14];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CurrDice => currDice_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TotalDicePoint
	{
		get
		{
			return totalDicePoint_;
		}
		set
		{
			totalDicePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemEtc> Item => item_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LaborActDiceInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LaborActDiceInfo(LaborActDiceInfo other)
		: this()
	{
		currDice_ = other.currDice_.Clone();
		totalDicePoint_ = other.totalDicePoint_;
		item_ = other.item_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LaborActDiceInfo Clone()
	{
		return new LaborActDiceInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LaborActDiceInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LaborActDiceInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!currDice_.Equals(other.currDice_))
		{
			return false;
		}
		if (TotalDicePoint != other.TotalDicePoint)
		{
			return false;
		}
		if (!item_.Equals(other.item_))
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
		num ^= currDice_.GetHashCode();
		if (TotalDicePoint != 0)
		{
			num ^= TotalDicePoint.GetHashCode();
		}
		num ^= item_.GetHashCode();
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
		currDice_.WriteTo(ref output, _repeated_currDice_codec);
		if (TotalDicePoint != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(TotalDicePoint);
		}
		item_.WriteTo(ref output, _repeated_item_codec);
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
		num += currDice_.CalculateSize(_repeated_currDice_codec);
		if (TotalDicePoint != 0)
		{
			num += 5;
		}
		num += item_.CalculateSize(_repeated_item_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LaborActDiceInfo other)
	{
		if (other != null)
		{
			currDice_.Add(other.currDice_);
			if (other.TotalDicePoint != 0)
			{
				TotalDicePoint = other.TotalDicePoint;
			}
			item_.Add(other.item_);
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
			case 13u:
				currDice_.AddEntriesFrom(ref input, _repeated_currDice_codec);
				break;
			case 21u:
				TotalDicePoint = input.ReadSFixed32();
				break;
			case 26u:
				item_.AddEntriesFrom(ref input, _repeated_item_codec);
				break;
			}
		}
	}
}
