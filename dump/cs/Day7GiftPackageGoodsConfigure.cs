using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class Day7GiftPackageGoodsConfigure : IMessage<Day7GiftPackageGoodsConfigure>, IMessage, IEquatable<Day7GiftPackageGoodsConfigure>, IDeepCloneable<Day7GiftPackageGoodsConfigure>, IBufferMessage
{
	private static readonly MessageParser<Day7GiftPackageGoodsConfigure> _parser = new MessageParser<Day7GiftPackageGoodsConfigure>(() => new Day7GiftPackageGoodsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GoodsIDFieldNumber = 1;

	private int goodsID_;

	public const int TitleIDFieldNumber = 2;

	private int titleID_;

	public const int DescriptionIDFieldNumber = 3;

	private int descriptionID_;

	public const int ValidityPeriodFieldNumber = 4;

	private int validityPeriod_;

	public const int Day7GiftPackageGoodsConfigureItemsFieldNumber = 5;

	private static readonly FieldCodec<Day7GiftPackageGoodsConfigureItem> _repeated_day7GiftPackageGoodsConfigureItems_codec = FieldCodec.ForMessage(42u, Day7GiftPackageGoodsConfigureItem.Parser);

	private readonly RepeatedField<Day7GiftPackageGoodsConfigureItem> day7GiftPackageGoodsConfigureItems_ = new RepeatedField<Day7GiftPackageGoodsConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Day7GiftPackageGoodsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => Day7GiftPackageReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoodsID
	{
		get
		{
			return goodsID_;
		}
		private set
		{
			goodsID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TitleID
	{
		get
		{
			return titleID_;
		}
		private set
		{
			titleID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescriptionID
	{
		get
		{
			return descriptionID_;
		}
		private set
		{
			descriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ValidityPeriod
	{
		get
		{
			return validityPeriod_;
		}
		private set
		{
			validityPeriod_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Day7GiftPackageGoodsConfigureItem> Day7GiftPackageGoodsConfigureItems => day7GiftPackageGoodsConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7GiftPackageGoodsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7GiftPackageGoodsConfigure(Day7GiftPackageGoodsConfigure other)
		: this()
	{
		goodsID_ = other.goodsID_;
		titleID_ = other.titleID_;
		descriptionID_ = other.descriptionID_;
		validityPeriod_ = other.validityPeriod_;
		day7GiftPackageGoodsConfigureItems_ = other.day7GiftPackageGoodsConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7GiftPackageGoodsConfigure Clone()
	{
		return new Day7GiftPackageGoodsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Day7GiftPackageGoodsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Day7GiftPackageGoodsConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GoodsID != other.GoodsID)
		{
			return false;
		}
		if (TitleID != other.TitleID)
		{
			return false;
		}
		if (DescriptionID != other.DescriptionID)
		{
			return false;
		}
		if (ValidityPeriod != other.ValidityPeriod)
		{
			return false;
		}
		if (!day7GiftPackageGoodsConfigureItems_.Equals(other.day7GiftPackageGoodsConfigureItems_))
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
		if (GoodsID != 0)
		{
			num ^= GoodsID.GetHashCode();
		}
		if (TitleID != 0)
		{
			num ^= TitleID.GetHashCode();
		}
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
		}
		if (ValidityPeriod != 0)
		{
			num ^= ValidityPeriod.GetHashCode();
		}
		num ^= day7GiftPackageGoodsConfigureItems_.GetHashCode();
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
		if (GoodsID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(GoodsID);
		}
		if (TitleID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(TitleID);
		}
		if (DescriptionID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DescriptionID);
		}
		if (ValidityPeriod != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ValidityPeriod);
		}
		day7GiftPackageGoodsConfigureItems_.WriteTo(ref output, _repeated_day7GiftPackageGoodsConfigureItems_codec);
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
		if (GoodsID != 0)
		{
			num += 5;
		}
		if (TitleID != 0)
		{
			num += 5;
		}
		if (DescriptionID != 0)
		{
			num += 5;
		}
		if (ValidityPeriod != 0)
		{
			num += 5;
		}
		num += day7GiftPackageGoodsConfigureItems_.CalculateSize(_repeated_day7GiftPackageGoodsConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(Day7GiftPackageGoodsConfigure other)
	{
		if (other != null)
		{
			if (other.GoodsID != 0)
			{
				GoodsID = other.GoodsID;
			}
			if (other.TitleID != 0)
			{
				TitleID = other.TitleID;
			}
			if (other.DescriptionID != 0)
			{
				DescriptionID = other.DescriptionID;
			}
			if (other.ValidityPeriod != 0)
			{
				ValidityPeriod = other.ValidityPeriod;
			}
			day7GiftPackageGoodsConfigureItems_.Add(other.day7GiftPackageGoodsConfigureItems_);
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
				GoodsID = input.ReadSFixed32();
				break;
			case 21u:
				TitleID = input.ReadSFixed32();
				break;
			case 29u:
				DescriptionID = input.ReadSFixed32();
				break;
			case 37u:
				ValidityPeriod = input.ReadSFixed32();
				break;
			case 42u:
				day7GiftPackageGoodsConfigureItems_.AddEntriesFrom(ref input, _repeated_day7GiftPackageGoodsConfigureItems_codec);
				break;
			}
		}
	}
}
