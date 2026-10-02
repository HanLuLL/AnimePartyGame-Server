using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SysChinaPayMsgC2S : IMessage<SysChinaPayMsgC2S>, IMessage, IEquatable<SysChinaPayMsgC2S>, IDeepCloneable<SysChinaPayMsgC2S>, IBufferMessage
{
	private static readonly MessageParser<SysChinaPayMsgC2S> _parser = new MessageParser<SysChinaPayMsgC2S>(() => new SysChinaPayMsgC2S());

	private UnknownFieldSet _unknownFields;

	public const int IsEffectFieldNumber = 1;

	private string isEffect_ = "";

	public const int IsSandboxFieldNumber = 2;

	private string isSandbox_ = "";

	public const int GoodsIdFieldNumber = 3;

	private string goodsId_ = "";

	public const int OrderIdFieldNumber = 4;

	private string orderId_ = "";

	public const int MoneyFieldNumber = 5;

	private long money_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysChinaPayMsgC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[515];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string IsEffect
	{
		get
		{
			return isEffect_;
		}
		set
		{
			isEffect_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string IsSandbox
	{
		get
		{
			return isSandbox_;
		}
		set
		{
			isSandbox_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string GoodsId
	{
		get
		{
			return goodsId_;
		}
		set
		{
			goodsId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string OrderId
	{
		get
		{
			return orderId_;
		}
		set
		{
			orderId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Money
	{
		get
		{
			return money_;
		}
		set
		{
			money_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysChinaPayMsgC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysChinaPayMsgC2S(SysChinaPayMsgC2S other)
		: this()
	{
		isEffect_ = other.isEffect_;
		isSandbox_ = other.isSandbox_;
		goodsId_ = other.goodsId_;
		orderId_ = other.orderId_;
		money_ = other.money_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysChinaPayMsgC2S Clone()
	{
		return new SysChinaPayMsgC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysChinaPayMsgC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysChinaPayMsgC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (IsEffect != other.IsEffect)
		{
			return false;
		}
		if (IsSandbox != other.IsSandbox)
		{
			return false;
		}
		if (GoodsId != other.GoodsId)
		{
			return false;
		}
		if (OrderId != other.OrderId)
		{
			return false;
		}
		if (Money != other.Money)
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
		if (IsEffect.Length != 0)
		{
			num ^= IsEffect.GetHashCode();
		}
		if (IsSandbox.Length != 0)
		{
			num ^= IsSandbox.GetHashCode();
		}
		if (GoodsId.Length != 0)
		{
			num ^= GoodsId.GetHashCode();
		}
		if (OrderId.Length != 0)
		{
			num ^= OrderId.GetHashCode();
		}
		if (Money != 0L)
		{
			num ^= Money.GetHashCode();
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
		if (IsEffect.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(IsEffect);
		}
		if (IsSandbox.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(IsSandbox);
		}
		if (GoodsId.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(GoodsId);
		}
		if (OrderId.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(OrderId);
		}
		if (Money != 0L)
		{
			output.WriteRawTag(41);
			output.WriteSFixed64(Money);
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
		if (IsEffect.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(IsEffect);
		}
		if (IsSandbox.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(IsSandbox);
		}
		if (GoodsId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(GoodsId);
		}
		if (OrderId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(OrderId);
		}
		if (Money != 0L)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SysChinaPayMsgC2S other)
	{
		if (other != null)
		{
			if (other.IsEffect.Length != 0)
			{
				IsEffect = other.IsEffect;
			}
			if (other.IsSandbox.Length != 0)
			{
				IsSandbox = other.IsSandbox;
			}
			if (other.GoodsId.Length != 0)
			{
				GoodsId = other.GoodsId;
			}
			if (other.OrderId.Length != 0)
			{
				OrderId = other.OrderId;
			}
			if (other.Money != 0L)
			{
				Money = other.Money;
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
			case 10u:
				IsEffect = input.ReadString();
				break;
			case 18u:
				IsSandbox = input.ReadString();
				break;
			case 26u:
				GoodsId = input.ReadString();
				break;
			case 34u:
				OrderId = input.ReadString();
				break;
			case 41u:
				Money = input.ReadSFixed64();
				break;
			}
		}
	}
}
