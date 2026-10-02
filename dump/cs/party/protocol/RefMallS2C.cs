using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class RefMallS2C : IMessage<RefMallS2C>, IMessage, IEquatable<RefMallS2C>, IDeepCloneable<RefMallS2C>, IBufferMessage
{
	private static readonly MessageParser<RefMallS2C> _parser = new MessageParser<RefMallS2C>(() => new RefMallS2C());

	private UnknownFieldSet _unknownFields;

	public const int ShopInfoFieldNumber = 1;

	private PlayerShopInfo shopInfo_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RefMallS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[248];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerShopInfo ShopInfo
	{
		get
		{
			return shopInfo_;
		}
		set
		{
			shopInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefMallS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefMallS2C(RefMallS2C other)
		: this()
	{
		shopInfo_ = ((other.shopInfo_ != null) ? other.shopInfo_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefMallS2C Clone()
	{
		return new RefMallS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RefMallS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RefMallS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(ShopInfo, other.ShopInfo))
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
		if (shopInfo_ != null)
		{
			num ^= ShopInfo.GetHashCode();
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
		if (shopInfo_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(ShopInfo);
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
		if (shopInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ShopInfo);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RefMallS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.shopInfo_ != null)
		{
			if (shopInfo_ == null)
			{
				ShopInfo = new PlayerShopInfo();
			}
			ShopInfo.MergeFrom(other.ShopInfo);
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				continue;
			}
			if (shopInfo_ == null)
			{
				ShopInfo = new PlayerShopInfo();
			}
			input.ReadMessage(ShopInfo);
		}
	}
}
