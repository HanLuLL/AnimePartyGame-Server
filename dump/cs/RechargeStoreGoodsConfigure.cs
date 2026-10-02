using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RechargeStoreGoodsConfigure : IMessage<RechargeStoreGoodsConfigure>, IMessage, IEquatable<RechargeStoreGoodsConfigure>, IDeepCloneable<RechargeStoreGoodsConfigure>, IBufferMessage
{
	private static readonly MessageParser<RechargeStoreGoodsConfigure> _parser = new MessageParser<RechargeStoreGoodsConfigure>(() => new RechargeStoreGoodsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ShopTabTypeFieldNumber = 1;

	private ShopTabType shopTabType_;

	public const int RechargeStoreGoodsConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<RechargeStoreGoodsConfigureItem> _repeated_rechargeStoreGoodsConfigureItems_codec = FieldCodec.ForMessage(18u, RechargeStoreGoodsConfigureItem.Parser);

	private readonly RepeatedField<RechargeStoreGoodsConfigureItem> rechargeStoreGoodsConfigureItems_ = new RepeatedField<RechargeStoreGoodsConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeStoreGoodsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeStoreReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopTabType ShopTabType
	{
		get
		{
			return shopTabType_;
		}
		private set
		{
			shopTabType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RechargeStoreGoodsConfigureItem> RechargeStoreGoodsConfigureItems => rechargeStoreGoodsConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreGoodsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreGoodsConfigure(RechargeStoreGoodsConfigure other)
		: this()
	{
		shopTabType_ = other.shopTabType_;
		rechargeStoreGoodsConfigureItems_ = other.rechargeStoreGoodsConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreGoodsConfigure Clone()
	{
		return new RechargeStoreGoodsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeStoreGoodsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeStoreGoodsConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ShopTabType != other.ShopTabType)
		{
			return false;
		}
		if (!rechargeStoreGoodsConfigureItems_.Equals(other.rechargeStoreGoodsConfigureItems_))
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
		if (ShopTabType != ShopTabType.None)
		{
			num ^= ShopTabType.GetHashCode();
		}
		num ^= rechargeStoreGoodsConfigureItems_.GetHashCode();
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
		if (ShopTabType != ShopTabType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)ShopTabType);
		}
		rechargeStoreGoodsConfigureItems_.WriteTo(ref output, _repeated_rechargeStoreGoodsConfigureItems_codec);
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
		if (ShopTabType != ShopTabType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ShopTabType);
		}
		num += rechargeStoreGoodsConfigureItems_.CalculateSize(_repeated_rechargeStoreGoodsConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RechargeStoreGoodsConfigure other)
	{
		if (other != null)
		{
			if (other.ShopTabType != ShopTabType.None)
			{
				ShopTabType = other.ShopTabType;
			}
			rechargeStoreGoodsConfigureItems_.Add(other.rechargeStoreGoodsConfigureItems_);
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
				ShopTabType = (ShopTabType)input.ReadEnum();
				break;
			case 18u:
				rechargeStoreGoodsConfigureItems_.AddEntriesFrom(ref input, _repeated_rechargeStoreGoodsConfigureItems_codec);
				break;
			}
		}
	}
}
