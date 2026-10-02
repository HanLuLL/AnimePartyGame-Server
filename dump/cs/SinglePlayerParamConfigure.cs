using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerParamConfigure : IMessage<SinglePlayerParamConfigure>, IMessage, IEquatable<SinglePlayerParamConfigure>, IDeepCloneable<SinglePlayerParamConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerParamConfigure> _parser = new MessageParser<SinglePlayerParamConfigure>(() => new SinglePlayerParamConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int InitGoldFieldNumber = 2;

	private int initGold_;

	public const int InitCardFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_initCard_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> initCard_ = new RepeatedField<int>();

	public const int CardPriceFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_cardPrice_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> cardPrice_ = new RepeatedField<int>();

	public const int SameCardExpFieldNumber = 5;

	private int sameCardExp_;

	public const int RefreshPriceFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_refreshPrice_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> refreshPrice_ = new RepeatedField<int>();

	public const int ShopSellCountFieldNumber = 7;

	private int shopSellCount_;

	public const int BagSlotCountFieldNumber = 8;

	private int bagSlotCount_;

	public const int DevelopLandFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_developLand_codec = FieldCodec.ForSFixed32(74u);

	private readonly RepeatedField<int> developLand_ = new RepeatedField<int>();

	public const int RelicMaxFieldNumber = 10;

	private int relicMax_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerParamConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[4];

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
	public int InitGold
	{
		get
		{
			return initGold_;
		}
		private set
		{
			initGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> InitCard => initCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CardPrice => cardPrice_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SameCardExp
	{
		get
		{
			return sameCardExp_;
		}
		private set
		{
			sameCardExp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RefreshPrice => refreshPrice_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ShopSellCount
	{
		get
		{
			return shopSellCount_;
		}
		private set
		{
			shopSellCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BagSlotCount
	{
		get
		{
			return bagSlotCount_;
		}
		private set
		{
			bagSlotCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> DevelopLand => developLand_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicMax
	{
		get
		{
			return relicMax_;
		}
		private set
		{
			relicMax_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerParamConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerParamConfigure(SinglePlayerParamConfigure other)
		: this()
	{
		id_ = other.id_;
		initGold_ = other.initGold_;
		initCard_ = other.initCard_.Clone();
		cardPrice_ = other.cardPrice_.Clone();
		sameCardExp_ = other.sameCardExp_;
		refreshPrice_ = other.refreshPrice_.Clone();
		shopSellCount_ = other.shopSellCount_;
		bagSlotCount_ = other.bagSlotCount_;
		developLand_ = other.developLand_.Clone();
		relicMax_ = other.relicMax_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerParamConfigure Clone()
	{
		return new SinglePlayerParamConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerParamConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerParamConfigure other)
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
		if (InitGold != other.InitGold)
		{
			return false;
		}
		if (!initCard_.Equals(other.initCard_))
		{
			return false;
		}
		if (!cardPrice_.Equals(other.cardPrice_))
		{
			return false;
		}
		if (SameCardExp != other.SameCardExp)
		{
			return false;
		}
		if (!refreshPrice_.Equals(other.refreshPrice_))
		{
			return false;
		}
		if (ShopSellCount != other.ShopSellCount)
		{
			return false;
		}
		if (BagSlotCount != other.BagSlotCount)
		{
			return false;
		}
		if (!developLand_.Equals(other.developLand_))
		{
			return false;
		}
		if (RelicMax != other.RelicMax)
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
		if (InitGold != 0)
		{
			num ^= InitGold.GetHashCode();
		}
		num ^= initCard_.GetHashCode();
		num ^= cardPrice_.GetHashCode();
		if (SameCardExp != 0)
		{
			num ^= SameCardExp.GetHashCode();
		}
		num ^= refreshPrice_.GetHashCode();
		if (ShopSellCount != 0)
		{
			num ^= ShopSellCount.GetHashCode();
		}
		if (BagSlotCount != 0)
		{
			num ^= BagSlotCount.GetHashCode();
		}
		num ^= developLand_.GetHashCode();
		if (RelicMax != 0)
		{
			num ^= RelicMax.GetHashCode();
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
		if (InitGold != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(InitGold);
		}
		initCard_.WriteTo(ref output, _repeated_initCard_codec);
		cardPrice_.WriteTo(ref output, _repeated_cardPrice_codec);
		if (SameCardExp != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(SameCardExp);
		}
		refreshPrice_.WriteTo(ref output, _repeated_refreshPrice_codec);
		if (ShopSellCount != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(ShopSellCount);
		}
		if (BagSlotCount != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(BagSlotCount);
		}
		developLand_.WriteTo(ref output, _repeated_developLand_codec);
		if (RelicMax != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(RelicMax);
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
		if (InitGold != 0)
		{
			num += 5;
		}
		num += initCard_.CalculateSize(_repeated_initCard_codec);
		num += cardPrice_.CalculateSize(_repeated_cardPrice_codec);
		if (SameCardExp != 0)
		{
			num += 5;
		}
		num += refreshPrice_.CalculateSize(_repeated_refreshPrice_codec);
		if (ShopSellCount != 0)
		{
			num += 5;
		}
		if (BagSlotCount != 0)
		{
			num += 5;
		}
		num += developLand_.CalculateSize(_repeated_developLand_codec);
		if (RelicMax != 0)
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
	public void MergeFrom(SinglePlayerParamConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.InitGold != 0)
			{
				InitGold = other.InitGold;
			}
			initCard_.Add(other.initCard_);
			cardPrice_.Add(other.cardPrice_);
			if (other.SameCardExp != 0)
			{
				SameCardExp = other.SameCardExp;
			}
			refreshPrice_.Add(other.refreshPrice_);
			if (other.ShopSellCount != 0)
			{
				ShopSellCount = other.ShopSellCount;
			}
			if (other.BagSlotCount != 0)
			{
				BagSlotCount = other.BagSlotCount;
			}
			developLand_.Add(other.developLand_);
			if (other.RelicMax != 0)
			{
				RelicMax = other.RelicMax;
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
				InitGold = input.ReadSFixed32();
				break;
			case 26u:
			case 29u:
				initCard_.AddEntriesFrom(ref input, _repeated_initCard_codec);
				break;
			case 34u:
			case 37u:
				cardPrice_.AddEntriesFrom(ref input, _repeated_cardPrice_codec);
				break;
			case 45u:
				SameCardExp = input.ReadSFixed32();
				break;
			case 50u:
			case 53u:
				refreshPrice_.AddEntriesFrom(ref input, _repeated_refreshPrice_codec);
				break;
			case 61u:
				ShopSellCount = input.ReadSFixed32();
				break;
			case 69u:
				BagSlotCount = input.ReadSFixed32();
				break;
			case 74u:
			case 77u:
				developLand_.AddEntriesFrom(ref input, _repeated_developLand_codec);
				break;
			case 85u:
				RelicMax = input.ReadSFixed32();
				break;
			}
		}
	}
}
