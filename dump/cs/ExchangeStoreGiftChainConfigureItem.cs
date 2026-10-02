using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class ExchangeStoreGiftChainConfigureItem : IMessage<ExchangeStoreGiftChainConfigureItem>, IMessage, IEquatable<ExchangeStoreGiftChainConfigureItem>, IDeepCloneable<ExchangeStoreGiftChainConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<ExchangeStoreGiftChainConfigureItem> _parser = new MessageParser<ExchangeStoreGiftChainConfigureItem>(() => new ExchangeStoreGiftChainConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int GoodsIDFieldNumber = 1;

	private int goodsID_;

	public const int ParamFieldNumber = 2;

	private int param_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExchangeStoreGiftChainConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ExchangeStoreReflection.Descriptor.MessageTypes[7];

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
	public int Param
	{
		get
		{
			return param_;
		}
		private set
		{
			param_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGiftChainConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGiftChainConfigureItem(ExchangeStoreGiftChainConfigureItem other)
		: this()
	{
		goodsID_ = other.goodsID_;
		param_ = other.param_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExchangeStoreGiftChainConfigureItem Clone()
	{
		return new ExchangeStoreGiftChainConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExchangeStoreGiftChainConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExchangeStoreGiftChainConfigureItem other)
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
		if (Param != other.Param)
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
		if (Param != 0)
		{
			num ^= Param.GetHashCode();
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
		if (Param != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Param);
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
		if (Param != 0)
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
	public void MergeFrom(ExchangeStoreGiftChainConfigureItem other)
	{
		if (other != null)
		{
			if (other.GoodsID != 0)
			{
				GoodsID = other.GoodsID;
			}
			if (other.Param != 0)
			{
				Param = other.Param;
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
			case 21u:
				Param = input.ReadSFixed32();
				break;
			}
		}
	}
}
