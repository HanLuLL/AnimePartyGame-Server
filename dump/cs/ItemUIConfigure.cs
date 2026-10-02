using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ItemUIConfigure : IMessage<ItemUIConfigure>, IMessage, IEquatable<ItemUIConfigure>, IDeepCloneable<ItemUIConfigure>, IBufferMessage
{
	private static readonly MessageParser<ItemUIConfigure> _parser = new MessageParser<ItemUIConfigure>(() => new ItemUIConfigure());

	private UnknownFieldSet _unknownFields;

	public const int MenuSubTypeFieldNumber = 1;

	private ItemUIMenuSubType menuSubType_;

	public const int FilterNameIDFieldNumber = 2;

	private int filterNameID_;

	public const int ItemTypesFieldNumber = 3;

	private static readonly FieldCodec<ItemType> _repeated_itemTypes_codec = FieldCodec.ForEnum(26u, (ItemType x) => (int)x, (int x) => (ItemType)x);

	private readonly RepeatedField<ItemType> itemTypes_ = new RepeatedField<ItemType>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ItemUIConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ItemReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemUIMenuSubType MenuSubType
	{
		get
		{
			return menuSubType_;
		}
		private set
		{
			menuSubType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FilterNameID
	{
		get
		{
			return filterNameID_;
		}
		private set
		{
			filterNameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemType> ItemTypes => itemTypes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemUIConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemUIConfigure(ItemUIConfigure other)
		: this()
	{
		menuSubType_ = other.menuSubType_;
		filterNameID_ = other.filterNameID_;
		itemTypes_ = other.itemTypes_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemUIConfigure Clone()
	{
		return new ItemUIConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ItemUIConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ItemUIConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MenuSubType != other.MenuSubType)
		{
			return false;
		}
		if (FilterNameID != other.FilterNameID)
		{
			return false;
		}
		if (!itemTypes_.Equals(other.itemTypes_))
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
		if (MenuSubType != ItemUIMenuSubType.None)
		{
			num ^= MenuSubType.GetHashCode();
		}
		if (FilterNameID != 0)
		{
			num ^= FilterNameID.GetHashCode();
		}
		num ^= itemTypes_.GetHashCode();
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
		if (MenuSubType != ItemUIMenuSubType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)MenuSubType);
		}
		if (FilterNameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(FilterNameID);
		}
		itemTypes_.WriteTo(ref output, _repeated_itemTypes_codec);
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
		if (MenuSubType != ItemUIMenuSubType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MenuSubType);
		}
		if (FilterNameID != 0)
		{
			num += 5;
		}
		num += itemTypes_.CalculateSize(_repeated_itemTypes_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ItemUIConfigure other)
	{
		if (other != null)
		{
			if (other.MenuSubType != ItemUIMenuSubType.None)
			{
				MenuSubType = other.MenuSubType;
			}
			if (other.FilterNameID != 0)
			{
				FilterNameID = other.FilterNameID;
			}
			itemTypes_.Add(other.itemTypes_);
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
				MenuSubType = (ItemUIMenuSubType)input.ReadEnum();
				break;
			case 21u:
				FilterNameID = input.ReadSFixed32();
				break;
			case 24u:
			case 26u:
				itemTypes_.AddEntriesFrom(ref input, _repeated_itemTypes_codec);
				break;
			}
		}
	}
}
