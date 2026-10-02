using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class ItemTagConfigure : IMessage<ItemTagConfigure>, IMessage, IEquatable<ItemTagConfigure>, IDeepCloneable<ItemTagConfigure>, IBufferMessage
{
	private static readonly MessageParser<ItemTagConfigure> _parser = new MessageParser<ItemTagConfigure>(() => new ItemTagConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ItemTypeFieldNumber = 1;

	private ItemType itemType_;

	public const int MaxCountFieldNumber = 2;

	private int maxCount_;

	public const int EnableUseFieldNumber = 3;

	private bool enableUse_;

	public const int SelectedIndexFieldNumber = 4;

	private int selectedIndex_;

	public const int NameIDFieldNumber = 5;

	private int nameID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ItemTagConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ItemReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemType ItemType
	{
		get
		{
			return itemType_;
		}
		private set
		{
			itemType_ = value;
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
	public bool EnableUse
	{
		get
		{
			return enableUse_;
		}
		private set
		{
			enableUse_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SelectedIndex
	{
		get
		{
			return selectedIndex_;
		}
		private set
		{
			selectedIndex_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemTagConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemTagConfigure(ItemTagConfigure other)
		: this()
	{
		itemType_ = other.itemType_;
		maxCount_ = other.maxCount_;
		enableUse_ = other.enableUse_;
		selectedIndex_ = other.selectedIndex_;
		nameID_ = other.nameID_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemTagConfigure Clone()
	{
		return new ItemTagConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ItemTagConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ItemTagConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ItemType != other.ItemType)
		{
			return false;
		}
		if (MaxCount != other.MaxCount)
		{
			return false;
		}
		if (EnableUse != other.EnableUse)
		{
			return false;
		}
		if (SelectedIndex != other.SelectedIndex)
		{
			return false;
		}
		if (NameID != other.NameID)
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
		if (ItemType != ItemType.None)
		{
			num ^= ItemType.GetHashCode();
		}
		if (MaxCount != 0)
		{
			num ^= MaxCount.GetHashCode();
		}
		if (EnableUse)
		{
			num ^= EnableUse.GetHashCode();
		}
		if (SelectedIndex != 0)
		{
			num ^= SelectedIndex.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
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
		if (ItemType != ItemType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)ItemType);
		}
		if (MaxCount != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MaxCount);
		}
		if (EnableUse)
		{
			output.WriteRawTag(24);
			output.WriteBool(EnableUse);
		}
		if (SelectedIndex != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(SelectedIndex);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(NameID);
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
		if (ItemType != ItemType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ItemType);
		}
		if (MaxCount != 0)
		{
			num += 5;
		}
		if (EnableUse)
		{
			num += 2;
		}
		if (SelectedIndex != 0)
		{
			num += 5;
		}
		if (NameID != 0)
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
	public void MergeFrom(ItemTagConfigure other)
	{
		if (other != null)
		{
			if (other.ItemType != ItemType.None)
			{
				ItemType = other.ItemType;
			}
			if (other.MaxCount != 0)
			{
				MaxCount = other.MaxCount;
			}
			if (other.EnableUse)
			{
				EnableUse = other.EnableUse;
			}
			if (other.SelectedIndex != 0)
			{
				SelectedIndex = other.SelectedIndex;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
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
			case 8u:
				ItemType = (ItemType)input.ReadEnum();
				break;
			case 21u:
				MaxCount = input.ReadSFixed32();
				break;
			case 24u:
				EnableUse = input.ReadBool();
				break;
			case 37u:
				SelectedIndex = input.ReadSFixed32();
				break;
			case 45u:
				NameID = input.ReadSFixed32();
				break;
			}
		}
	}
}
