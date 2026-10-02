using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class SkinSellInfoConfigureItem : IMessage<SkinSellInfoConfigureItem>, IMessage, IEquatable<SkinSellInfoConfigureItem>, IDeepCloneable<SkinSellInfoConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<SkinSellInfoConfigureItem> _parser = new MessageParser<SkinSellInfoConfigureItem>(() => new SkinSellInfoConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int OriginalPriceFieldNumber = 2;

	private int originalPrice_;

	public const int DiscountPriceFieldNumber = 3;

	private int discountPrice_;

	public const int ItemIDFieldNumber = 4;

	private int itemID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SkinSellInfoConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SkinSellReflection.Descriptor.MessageTypes[1];

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
	public int OriginalPrice
	{
		get
		{
			return originalPrice_;
		}
		private set
		{
			originalPrice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiscountPrice
	{
		get
		{
			return discountPrice_;
		}
		private set
		{
			discountPrice_ = value;
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
	public SkinSellInfoConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinSellInfoConfigureItem(SkinSellInfoConfigureItem other)
		: this()
	{
		id_ = other.id_;
		originalPrice_ = other.originalPrice_;
		discountPrice_ = other.discountPrice_;
		itemID_ = other.itemID_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinSellInfoConfigureItem Clone()
	{
		return new SkinSellInfoConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SkinSellInfoConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SkinSellInfoConfigureItem other)
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
		if (OriginalPrice != other.OriginalPrice)
		{
			return false;
		}
		if (DiscountPrice != other.DiscountPrice)
		{
			return false;
		}
		if (ItemID != other.ItemID)
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
		if (OriginalPrice != 0)
		{
			num ^= OriginalPrice.GetHashCode();
		}
		if (DiscountPrice != 0)
		{
			num ^= DiscountPrice.GetHashCode();
		}
		if (ItemID != 0)
		{
			num ^= ItemID.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (OriginalPrice != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(OriginalPrice);
		}
		if (DiscountPrice != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DiscountPrice);
		}
		if (ItemID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ItemID);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (OriginalPrice != 0)
		{
			num += 5;
		}
		if (DiscountPrice != 0)
		{
			num += 5;
		}
		if (ItemID != 0)
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
	public void MergeFrom(SkinSellInfoConfigureItem other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.OriginalPrice != 0)
			{
				OriginalPrice = other.OriginalPrice;
			}
			if (other.DiscountPrice != 0)
			{
				DiscountPrice = other.DiscountPrice;
			}
			if (other.ItemID != 0)
			{
				ItemID = other.ItemID;
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
				Id = input.ReadSFixed32();
				break;
			case 21u:
				OriginalPrice = input.ReadSFixed32();
				break;
			case 29u:
				DiscountPrice = input.ReadSFixed32();
				break;
			case 37u:
				ItemID = input.ReadSFixed32();
				break;
			}
		}
	}
}
