using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BattlePassNewInfoConfigure : IMessage<BattlePassNewInfoConfigure>, IMessage, IEquatable<BattlePassNewInfoConfigure>, IDeepCloneable<BattlePassNewInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<BattlePassNewInfoConfigure> _parser = new MessageParser<BattlePassNewInfoConfigure>(() => new BattlePassNewInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ShopTabTypeFieldNumber = 2;

	private ShopTabType shopTabType_;

	public const int BattlePassNewInfoConfigureItemsFieldNumber = 3;

	private static readonly FieldCodec<BattlePassNewInfoConfigureItem> _repeated_battlePassNewInfoConfigureItems_codec = FieldCodec.ForMessage(26u, BattlePassNewInfoConfigureItem.Parser);

	private readonly RepeatedField<BattlePassNewInfoConfigureItem> battlePassNewInfoConfigureItems_ = new RepeatedField<BattlePassNewInfoConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassNewInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassNewReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<BattlePassNewInfoConfigureItem> BattlePassNewInfoConfigureItems => battlePassNewInfoConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassNewInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassNewInfoConfigure(BattlePassNewInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		shopTabType_ = other.shopTabType_;
		battlePassNewInfoConfigureItems_ = other.battlePassNewInfoConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassNewInfoConfigure Clone()
	{
		return new BattlePassNewInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassNewInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassNewInfoConfigure other)
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
		if (ShopTabType != other.ShopTabType)
		{
			return false;
		}
		if (!battlePassNewInfoConfigureItems_.Equals(other.battlePassNewInfoConfigureItems_))
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
		if (ShopTabType != ShopTabType.None)
		{
			num ^= ShopTabType.GetHashCode();
		}
		num ^= battlePassNewInfoConfigureItems_.GetHashCode();
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
		if (ShopTabType != ShopTabType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)ShopTabType);
		}
		battlePassNewInfoConfigureItems_.WriteTo(ref output, _repeated_battlePassNewInfoConfigureItems_codec);
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
		if (ShopTabType != ShopTabType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ShopTabType);
		}
		num += battlePassNewInfoConfigureItems_.CalculateSize(_repeated_battlePassNewInfoConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattlePassNewInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.ShopTabType != ShopTabType.None)
			{
				ShopTabType = other.ShopTabType;
			}
			battlePassNewInfoConfigureItems_.Add(other.battlePassNewInfoConfigureItems_);
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
			case 16u:
				ShopTabType = (ShopTabType)input.ReadEnum();
				break;
			case 26u:
				battlePassNewInfoConfigureItems_.AddEntriesFrom(ref input, _repeated_battlePassNewInfoConfigureItems_codec);
				break;
			}
		}
	}
}
