using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class RechargeStoreTransferTypeConfigure : IMessage<RechargeStoreTransferTypeConfigure>, IMessage, IEquatable<RechargeStoreTransferTypeConfigure>, IDeepCloneable<RechargeStoreTransferTypeConfigure>, IBufferMessage
{
	private static readonly MessageParser<RechargeStoreTransferTypeConfigure> _parser = new MessageParser<RechargeStoreTransferTypeConfigure>(() => new RechargeStoreTransferTypeConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ShopTabTypeFieldNumber = 1;

	private ShopTabType shopTabType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeStoreTransferTypeConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeStoreReflection.Descriptor.MessageTypes[4];

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
	public RechargeStoreTransferTypeConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreTransferTypeConfigure(RechargeStoreTransferTypeConfigure other)
		: this()
	{
		shopTabType_ = other.shopTabType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreTransferTypeConfigure Clone()
	{
		return new RechargeStoreTransferTypeConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeStoreTransferTypeConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeStoreTransferTypeConfigure other)
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
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RechargeStoreTransferTypeConfigure other)
	{
		if (other != null)
		{
			if (other.ShopTabType != ShopTabType.None)
			{
				ShopTabType = other.ShopTabType;
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
			if (num != 8)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				ShopTabType = (ShopTabType)input.ReadEnum();
			}
		}
	}
}
