using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class ActivityScratchoffPoolConfigureItem : IMessage<ActivityScratchoffPoolConfigureItem>, IMessage, IEquatable<ActivityScratchoffPoolConfigureItem>, IDeepCloneable<ActivityScratchoffPoolConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<ActivityScratchoffPoolConfigureItem> _parser = new MessageParser<ActivityScratchoffPoolConfigureItem>(() => new ActivityScratchoffPoolConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int ItemIDFieldNumber = 2;

	private int itemID_;

	public const int ItemNumFieldNumber = 3;

	private int itemNum_;

	public const int WeightFieldNumber = 4;

	private int weight_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityScratchoffPoolConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[6];

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
	public int ItemNum
	{
		get
		{
			return itemNum_;
		}
		private set
		{
			itemNum_ = value;
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
	public ActivityScratchoffPoolConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityScratchoffPoolConfigureItem(ActivityScratchoffPoolConfigureItem other)
		: this()
	{
		index_ = other.index_;
		itemID_ = other.itemID_;
		itemNum_ = other.itemNum_;
		weight_ = other.weight_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityScratchoffPoolConfigureItem Clone()
	{
		return new ActivityScratchoffPoolConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityScratchoffPoolConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityScratchoffPoolConfigureItem other)
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
		if (ItemID != other.ItemID)
		{
			return false;
		}
		if (ItemNum != other.ItemNum)
		{
			return false;
		}
		if (Weight != other.Weight)
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
		if (ItemID != 0)
		{
			num ^= ItemID.GetHashCode();
		}
		if (ItemNum != 0)
		{
			num ^= ItemNum.GetHashCode();
		}
		if (Weight != 0)
		{
			num ^= Weight.GetHashCode();
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
		if (ItemID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ItemID);
		}
		if (ItemNum != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ItemNum);
		}
		if (Weight != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Weight);
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
		if (ItemID != 0)
		{
			num += 5;
		}
		if (ItemNum != 0)
		{
			num += 5;
		}
		if (Weight != 0)
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
	public void MergeFrom(ActivityScratchoffPoolConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.ItemID != 0)
			{
				ItemID = other.ItemID;
			}
			if (other.ItemNum != 0)
			{
				ItemNum = other.ItemNum;
			}
			if (other.Weight != 0)
			{
				Weight = other.Weight;
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
				ItemID = input.ReadSFixed32();
				break;
			case 29u:
				ItemNum = input.ReadSFixed32();
				break;
			case 37u:
				Weight = input.ReadSFixed32();
				break;
			}
		}
	}
}
