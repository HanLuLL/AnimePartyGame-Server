using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RechargeStoreAdsAdsConfigure : IMessage<RechargeStoreAdsAdsConfigure>, IMessage, IEquatable<RechargeStoreAdsAdsConfigure>, IDeepCloneable<RechargeStoreAdsAdsConfigure>, IBufferMessage
{
	private static readonly MessageParser<RechargeStoreAdsAdsConfigure> _parser = new MessageParser<RechargeStoreAdsAdsConfigure>(() => new RechargeStoreAdsAdsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ShopTabTypeFieldNumber = 1;

	private ShopTabType shopTabType_;

	public const int RechargeStoreAdsAdsConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<RechargeStoreAdsAdsConfigureItem> _repeated_rechargeStoreAdsAdsConfigureItems_codec = FieldCodec.ForMessage(18u, RechargeStoreAdsAdsConfigureItem.Parser);

	private readonly RepeatedField<RechargeStoreAdsAdsConfigureItem> rechargeStoreAdsAdsConfigureItems_ = new RepeatedField<RechargeStoreAdsAdsConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeStoreAdsAdsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeStoreAdsReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<RechargeStoreAdsAdsConfigureItem> RechargeStoreAdsAdsConfigureItems => rechargeStoreAdsAdsConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreAdsAdsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreAdsAdsConfigure(RechargeStoreAdsAdsConfigure other)
		: this()
	{
		shopTabType_ = other.shopTabType_;
		rechargeStoreAdsAdsConfigureItems_ = other.rechargeStoreAdsAdsConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreAdsAdsConfigure Clone()
	{
		return new RechargeStoreAdsAdsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeStoreAdsAdsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeStoreAdsAdsConfigure other)
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
		if (!rechargeStoreAdsAdsConfigureItems_.Equals(other.rechargeStoreAdsAdsConfigureItems_))
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
		num ^= rechargeStoreAdsAdsConfigureItems_.GetHashCode();
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
		rechargeStoreAdsAdsConfigureItems_.WriteTo(ref output, _repeated_rechargeStoreAdsAdsConfigureItems_codec);
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
		num += rechargeStoreAdsAdsConfigureItems_.CalculateSize(_repeated_rechargeStoreAdsAdsConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RechargeStoreAdsAdsConfigure other)
	{
		if (other != null)
		{
			if (other.ShopTabType != ShopTabType.None)
			{
				ShopTabType = other.ShopTabType;
			}
			rechargeStoreAdsAdsConfigureItems_.Add(other.rechargeStoreAdsAdsConfigureItems_);
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
				rechargeStoreAdsAdsConfigureItems_.AddEntriesFrom(ref input, _repeated_rechargeStoreAdsAdsConfigureItems_codec);
				break;
			}
		}
	}
}
