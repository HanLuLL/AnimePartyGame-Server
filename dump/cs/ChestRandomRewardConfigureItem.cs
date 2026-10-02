using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class ChestRandomRewardConfigureItem : IMessage<ChestRandomRewardConfigureItem>, IMessage, IEquatable<ChestRandomRewardConfigureItem>, IDeepCloneable<ChestRandomRewardConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<ChestRandomRewardConfigureItem> _parser = new MessageParser<ChestRandomRewardConfigureItem>(() => new ChestRandomRewardConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int WeightFieldNumber = 2;

	private int weight_;

	public const int ItemIDFieldNumber = 3;

	private int itemID_;

	public const int MinCountFieldNumber = 4;

	private int minCount_;

	public const int MaxCountFieldNumber = 5;

	private int maxCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChestRandomRewardConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChestReflection.Descriptor.MessageTypes[2];

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
	public int Weight
	{
		get
		{
			return weight_;
		}
		private set
		{
			weight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemID
	{
		get
		{
			return itemID_;
		}
		private set
		{
			itemID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MinCount
	{
		get
		{
			return minCount_;
		}
		private set
		{
			minCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxCount
	{
		get
		{
			return maxCount_;
		}
		private set
		{
			maxCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChestRandomRewardConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChestRandomRewardConfigureItem(ChestRandomRewardConfigureItem other)
		: this()
	{
		index_ = other.index_;
		weight_ = other.weight_;
		itemID_ = other.itemID_;
		minCount_ = other.minCount_;
		maxCount_ = other.maxCount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChestRandomRewardConfigureItem Clone()
	{
		return new ChestRandomRewardConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChestRandomRewardConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChestRandomRewardConfigureItem other)
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
		if (Weight != other.Weight)
		{
			return false;
		}
		if (ItemID != other.ItemID)
		{
			return false;
		}
		if (MinCount != other.MinCount)
		{
			return false;
		}
		if (MaxCount != other.MaxCount)
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
		if (Weight != 0)
		{
			num ^= Weight.GetHashCode();
		}
		if (ItemID != 0)
		{
			num ^= ItemID.GetHashCode();
		}
		if (MinCount != 0)
		{
			num ^= MinCount.GetHashCode();
		}
		if (MaxCount != 0)
		{
			num ^= MaxCount.GetHashCode();
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
		if (Weight != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Weight);
		}
		if (ItemID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ItemID);
		}
		if (MinCount != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(MinCount);
		}
		if (MaxCount != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(MaxCount);
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
		if (Weight != 0)
		{
			num += 5;
		}
		if (ItemID != 0)
		{
			num += 5;
		}
		if (MinCount != 0)
		{
			num += 5;
		}
		if (MaxCount != 0)
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
	public void MergeFrom(ChestRandomRewardConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.Weight != 0)
			{
				Weight = other.Weight;
			}
			if (other.ItemID != 0)
			{
				ItemID = other.ItemID;
			}
			if (other.MinCount != 0)
			{
				MinCount = other.MinCount;
			}
			if (other.MaxCount != 0)
			{
				MaxCount = other.MaxCount;
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
				Weight = input.ReadSFixed32();
				break;
			case 29u:
				ItemID = input.ReadSFixed32();
				break;
			case 37u:
				MinCount = input.ReadSFixed32();
				break;
			case 45u:
				MaxCount = input.ReadSFixed32();
				break;
			}
		}
	}
}
