using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RechargeStoreShelfConfigure : IMessage<RechargeStoreShelfConfigure>, IMessage, IEquatable<RechargeStoreShelfConfigure>, IDeepCloneable<RechargeStoreShelfConfigure>, IBufferMessage
{
	private static readonly MessageParser<RechargeStoreShelfConfigure> _parser = new MessageParser<RechargeStoreShelfConfigure>(() => new RechargeStoreShelfConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int OrderFieldNumber = 2;

	private int order_;

	public const int NameIDFieldNumber = 3;

	private int nameID_;

	public const int BackgroundFieldNumber = 4;

	private string background_ = "";

	public const int ShopTabTypesFieldNumber = 5;

	private static readonly FieldCodec<ShopTabType> _repeated_shopTabTypes_codec = FieldCodec.ForEnum(42u, (ShopTabType x) => (int)x, (int x) => (ShopTabType)x);

	private readonly RepeatedField<ShopTabType> shopTabTypes_ = new RepeatedField<ShopTabType>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeStoreShelfConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeStoreReflection.Descriptor.MessageTypes[0];

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
	public int Order
	{
		get
		{
			return order_;
		}
		private set
		{
			order_ = value;
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
	public string Background
	{
		get
		{
			return background_;
		}
		private set
		{
			background_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ShopTabType> ShopTabTypes => shopTabTypes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreShelfConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreShelfConfigure(RechargeStoreShelfConfigure other)
		: this()
	{
		id_ = other.id_;
		order_ = other.order_;
		nameID_ = other.nameID_;
		background_ = other.background_;
		shopTabTypes_ = other.shopTabTypes_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreShelfConfigure Clone()
	{
		return new RechargeStoreShelfConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeStoreShelfConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeStoreShelfConfigure other)
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
		if (Order != other.Order)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (Background != other.Background)
		{
			return false;
		}
		if (!shopTabTypes_.Equals(other.shopTabTypes_))
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
		if (Order != 0)
		{
			num ^= Order.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (Background.Length != 0)
		{
			num ^= Background.GetHashCode();
		}
		num ^= shopTabTypes_.GetHashCode();
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
		if (Order != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Order);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(NameID);
		}
		if (Background.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Background);
		}
		shopTabTypes_.WriteTo(ref output, _repeated_shopTabTypes_codec);
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
		if (Order != 0)
		{
			num += 5;
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (Background.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Background);
		}
		num += shopTabTypes_.CalculateSize(_repeated_shopTabTypes_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RechargeStoreShelfConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.Order != 0)
			{
				Order = other.Order;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.Background.Length != 0)
			{
				Background = other.Background;
			}
			shopTabTypes_.Add(other.shopTabTypes_);
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
				Order = input.ReadSFixed32();
				break;
			case 29u:
				NameID = input.ReadSFixed32();
				break;
			case 34u:
				Background = input.ReadString();
				break;
			case 40u:
			case 42u:
				shopTabTypes_.AddEntriesFrom(ref input, _repeated_shopTabTypes_codec);
				break;
			}
		}
	}
}
