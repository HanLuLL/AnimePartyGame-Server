using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class ShopTabInfoConfigure : IMessage<ShopTabInfoConfigure>, IMessage, IEquatable<ShopTabInfoConfigure>, IDeepCloneable<ShopTabInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<ShopTabInfoConfigure> _parser = new MessageParser<ShopTabInfoConfigure>(() => new ShopTabInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ShopTabTypeFieldNumber = 1;

	private ShopTabType shopTabType_;

	public const int GoodsPurchaseTypeFieldNumber = 2;

	private GoodsPurchaseType goodsPurchaseType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ShopTabInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ShopTabReflection.Descriptor.MessageTypes[0];

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
	public GoodsPurchaseType GoodsPurchaseType
	{
		get
		{
			return goodsPurchaseType_;
		}
		private set
		{
			goodsPurchaseType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopTabInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopTabInfoConfigure(ShopTabInfoConfigure other)
		: this()
	{
		shopTabType_ = other.shopTabType_;
		goodsPurchaseType_ = other.goodsPurchaseType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopTabInfoConfigure Clone()
	{
		return new ShopTabInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ShopTabInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ShopTabInfoConfigure other)
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
		if (GoodsPurchaseType != other.GoodsPurchaseType)
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
		if (GoodsPurchaseType != GoodsPurchaseType.None)
		{
			num ^= GoodsPurchaseType.GetHashCode();
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
		if (ShopTabType != ShopTabType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)ShopTabType);
		}
		if (GoodsPurchaseType != GoodsPurchaseType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)GoodsPurchaseType);
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
		if (ShopTabType != ShopTabType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ShopTabType);
		}
		if (GoodsPurchaseType != GoodsPurchaseType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GoodsPurchaseType);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ShopTabInfoConfigure other)
	{
		if (other != null)
		{
			if (other.ShopTabType != ShopTabType.None)
			{
				ShopTabType = other.ShopTabType;
			}
			if (other.GoodsPurchaseType != GoodsPurchaseType.None)
			{
				GoodsPurchaseType = other.GoodsPurchaseType;
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
				ShopTabType = (ShopTabType)input.ReadEnum();
				break;
			case 16u:
				GoodsPurchaseType = (GoodsPurchaseType)input.ReadEnum();
				break;
			}
		}
	}
}
