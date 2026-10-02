using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class GachaGroupConfigureItem : IMessage<GachaGroupConfigureItem>, IMessage, IEquatable<GachaGroupConfigureItem>, IDeepCloneable<GachaGroupConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<GachaGroupConfigureItem> _parser = new MessageParser<GachaGroupConfigureItem>(() => new GachaGroupConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int ItemIdFieldNumber = 2;

	private int itemId_;

	public const int NumberMinFieldNumber = 3;

	private int numberMin_;

	public const int ItemWeightFieldNumber = 4;

	private int itemWeight_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaGroupConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GachaReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Index
	{
		get
		{
			return index_;
		}
		private set
		{
			index_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemId
	{
		get
		{
			return itemId_;
		}
		private set
		{
			itemId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NumberMin
	{
		get
		{
			return numberMin_;
		}
		private set
		{
			numberMin_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemWeight
	{
		get
		{
			return itemWeight_;
		}
		private set
		{
			itemWeight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaGroupConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaGroupConfigureItem(GachaGroupConfigureItem other)
		: this()
	{
		index_ = other.index_;
		itemId_ = other.itemId_;
		numberMin_ = other.numberMin_;
		itemWeight_ = other.itemWeight_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaGroupConfigureItem Clone()
	{
		return new GachaGroupConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaGroupConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaGroupConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Index != other.Index)
		{
			return false;
		}
		if (ItemId != other.ItemId)
		{
			return false;
		}
		if (NumberMin != other.NumberMin)
		{
			return false;
		}
		if (ItemWeight != other.ItemWeight)
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
		if (Index != 0)
		{
			num ^= Index.GetHashCode();
		}
		if (ItemId != 0)
		{
			num ^= ItemId.GetHashCode();
		}
		if (NumberMin != 0)
		{
			num ^= NumberMin.GetHashCode();
		}
		if (ItemWeight != 0)
		{
			num ^= ItemWeight.GetHashCode();
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
		if (Index != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Index);
		}
		if (ItemId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ItemId);
		}
		if (NumberMin != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(NumberMin);
		}
		if (ItemWeight != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ItemWeight);
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
		if (Index != 0)
		{
			num += 5;
		}
		if (ItemId != 0)
		{
			num += 5;
		}
		if (NumberMin != 0)
		{
			num += 5;
		}
		if (ItemWeight != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GachaGroupConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.ItemId != 0)
			{
				ItemId = other.ItemId;
			}
			if (other.NumberMin != 0)
			{
				NumberMin = other.NumberMin;
			}
			if (other.ItemWeight != 0)
			{
				ItemWeight = other.ItemWeight;
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
			case 13u:
				Index = input.ReadSFixed32();
				break;
			case 21u:
				ItemId = input.ReadSFixed32();
				break;
			case 29u:
				NumberMin = input.ReadSFixed32();
				break;
			case 37u:
				ItemWeight = input.ReadSFixed32();
				break;
			}
		}
	}
}
