using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ExchangeStoreShelfConfigure : IMessage<ExchangeStoreShelfConfigure>, IMessage, IEquatable<ExchangeStoreShelfConfigure>, IDeepCloneable<ExchangeStoreShelfConfigure>, IBufferMessage
{
	private static readonly MessageParser<ExchangeStoreShelfConfigure> _parser = new MessageParser<ExchangeStoreShelfConfigure>(() => new ExchangeStoreShelfConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int TabOrderFieldNumber = 2;

	private int tabOrder_;

	public const int NameIDFieldNumber = 3;

	private int nameID_;

	public const int ShopTabTypesFieldNumber = 4;

	private static readonly FieldCodec<ShopTabType> _repeated_shopTabTypes_codec = FieldCodec.ForEnum(34u, (ShopTabType x) => (int)x, (int x) => (ShopTabType)x);

	private readonly RepeatedField<ShopTabType> shopTabTypes_ = new RepeatedField<ShopTabType>();

	public const int IsShowFieldNumber = 5;

	private bool isShow_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExchangeStoreShelfConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ExchangeStoreReflection.Descriptor.MessageTypes[0];

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
	public int TabOrder
	{
		get
		{
			return tabOrder_;
		}
		private set
		{
			tabOrder_ = value;
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
	public RepeatedField<ShopTabType> ShopTabTypes => shopTabTypes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsShow
	{
		get
		{
			return isShow_;
		}
		private set
		{
			isShow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreShelfConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreShelfConfigure(ExchangeStoreShelfConfigure other)
		: this()
	{
		id_ = other.id_;
		tabOrder_ = other.tabOrder_;
		nameID_ = other.nameID_;
		shopTabTypes_ = other.shopTabTypes_.Clone();
		isShow_ = other.isShow_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreShelfConfigure Clone()
	{
		return new ExchangeStoreShelfConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExchangeStoreShelfConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExchangeStoreShelfConfigure other)
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
		if (TabOrder != other.TabOrder)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (!shopTabTypes_.Equals(other.shopTabTypes_))
		{
			return false;
		}
		if (IsShow != other.IsShow)
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
		if (TabOrder != 0)
		{
			num ^= TabOrder.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		num ^= shopTabTypes_.GetHashCode();
		if (IsShow)
		{
			num ^= IsShow.GetHashCode();
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
		if (TabOrder != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(TabOrder);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(NameID);
		}
		shopTabTypes_.WriteTo(ref output, _repeated_shopTabTypes_codec);
		if (IsShow)
		{
			output.WriteRawTag(40);
			output.WriteBool(IsShow);
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
		if (TabOrder != 0)
		{
			num += 5;
		}
		if (NameID != 0)
		{
			num += 5;
		}
		num += shopTabTypes_.CalculateSize(_repeated_shopTabTypes_codec);
		if (IsShow)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ExchangeStoreShelfConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.TabOrder != 0)
			{
				TabOrder = other.TabOrder;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			shopTabTypes_.Add(other.shopTabTypes_);
			if (other.IsShow)
			{
				IsShow = other.IsShow;
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
				TabOrder = input.ReadSFixed32();
				break;
			case 29u:
				NameID = input.ReadSFixed32();
				break;
			case 32u:
			case 34u:
				shopTabTypes_.AddEntriesFrom(ref input, _repeated_shopTabTypes_codec);
				break;
			case 40u:
				IsShow = input.ReadBool();
				break;
			}
		}
	}
}
