using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class FixINTJPExchangeStoreGoodsConfigureItem : IMessage<FixINTJPExchangeStoreGoodsConfigureItem>, IMessage, IEquatable<FixINTJPExchangeStoreGoodsConfigureItem>, IDeepCloneable<FixINTJPExchangeStoreGoodsConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<FixINTJPExchangeStoreGoodsConfigureItem> _parser = new MessageParser<FixINTJPExchangeStoreGoodsConfigureItem>(() => new FixINTJPExchangeStoreGoodsConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int GoodsIDFieldNumber = 1;

	private int goodsID_;

	public const int RechargeIconFieldNumber = 2;

	private string rechargeIcon_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixINTJPExchangeStoreGoodsConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixINTJPReflection.Descriptor.MessageTypes[10];

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
	public string RechargeIcon
	{
		get
		{
			return rechargeIcon_;
		}
		private set
		{
			rechargeIcon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPExchangeStoreGoodsConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPExchangeStoreGoodsConfigureItem(FixINTJPExchangeStoreGoodsConfigureItem other)
		: this()
	{
		goodsID_ = other.goodsID_;
		rechargeIcon_ = other.rechargeIcon_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTJPExchangeStoreGoodsConfigureItem Clone()
	{
		return new FixINTJPExchangeStoreGoodsConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixINTJPExchangeStoreGoodsConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixINTJPExchangeStoreGoodsConfigureItem other)
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
		if (RechargeIcon != other.RechargeIcon)
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
		if (RechargeIcon.Length != 0)
		{
			num ^= RechargeIcon.GetHashCode();
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
		if (GoodsID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(GoodsID);
		}
		if (RechargeIcon.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(RechargeIcon);
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
		if (GoodsID != 0)
		{
			num += 5;
		}
		if (RechargeIcon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(RechargeIcon);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixINTJPExchangeStoreGoodsConfigureItem other)
	{
		if (other != null)
		{
			if (other.GoodsID != 0)
			{
				GoodsID = other.GoodsID;
			}
			if (other.RechargeIcon.Length != 0)
			{
				RechargeIcon = other.RechargeIcon;
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
				GoodsID = input.ReadSFixed32();
				break;
			case 18u:
				RechargeIcon = input.ReadString();
				break;
			}
		}
	}
}
