using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class AgeVerifyS2C : IMessage<AgeVerifyS2C>, IMessage, IEquatable<AgeVerifyS2C>, IDeepCloneable<AgeVerifyS2C>, IBufferMessage
{
	private static readonly MessageParser<AgeVerifyS2C> _parser = new MessageParser<AgeVerifyS2C>(() => new AgeVerifyS2C());

	private UnknownFieldSet _unknownFields;

	public const int PayAmountInfoFieldNumber = 1;

	private PayAmountInfo payAmountInfo_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AgeVerifyS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[533];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayAmountInfo PayAmountInfo
	{
		get
		{
			return payAmountInfo_;
		}
		set
		{
			payAmountInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AgeVerifyS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AgeVerifyS2C(AgeVerifyS2C other)
		: this()
	{
		payAmountInfo_ = ((other.payAmountInfo_ != null) ? other.payAmountInfo_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AgeVerifyS2C Clone()
	{
		return new AgeVerifyS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AgeVerifyS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AgeVerifyS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(PayAmountInfo, other.PayAmountInfo))
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
		if (payAmountInfo_ != null)
		{
			num ^= PayAmountInfo.GetHashCode();
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
		if (payAmountInfo_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(PayAmountInfo);
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
		if (payAmountInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(PayAmountInfo);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(AgeVerifyS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.payAmountInfo_ != null)
		{
			if (payAmountInfo_ == null)
			{
				PayAmountInfo = new PayAmountInfo();
			}
			PayAmountInfo.MergeFrom(other.PayAmountInfo);
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
			if (payAmountInfo_ == null)
			{
				PayAmountInfo = new PayAmountInfo();
			}
			input.ReadMessage(PayAmountInfo);
		}
	}
}
