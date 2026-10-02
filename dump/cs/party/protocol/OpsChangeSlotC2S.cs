using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class OpsChangeSlotC2S : IMessage<OpsChangeSlotC2S>, IMessage, IEquatable<OpsChangeSlotC2S>, IDeepCloneable<OpsChangeSlotC2S>, IBufferMessage
{
	private static readonly MessageParser<OpsChangeSlotC2S> _parser = new MessageParser<OpsChangeSlotC2S>(() => new OpsChangeSlotC2S());

	private UnknownFieldSet _unknownFields;

	public const int IsAgreeFieldNumber = 1;

	private bool isAgree_;

	public const int ApplyIdFieldNumber = 2;

	private long applyId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<OpsChangeSlotC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[527];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsAgree
	{
		get
		{
			return isAgree_;
		}
		set
		{
			isAgree_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ApplyId
	{
		get
		{
			return applyId_;
		}
		set
		{
			applyId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public OpsChangeSlotC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public OpsChangeSlotC2S(OpsChangeSlotC2S other)
		: this()
	{
		isAgree_ = other.isAgree_;
		applyId_ = other.applyId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public OpsChangeSlotC2S Clone()
	{
		return new OpsChangeSlotC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as OpsChangeSlotC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(OpsChangeSlotC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (IsAgree != other.IsAgree)
		{
			return false;
		}
		if (ApplyId != other.ApplyId)
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
		if (IsAgree)
		{
			num ^= IsAgree.GetHashCode();
		}
		if (ApplyId != 0L)
		{
			num ^= ApplyId.GetHashCode();
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
		if (IsAgree)
		{
			output.WriteRawTag(8);
			output.WriteBool(IsAgree);
		}
		if (ApplyId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(ApplyId);
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
		if (IsAgree)
		{
			num += 2;
		}
		if (ApplyId != 0L)
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
	public void MergeFrom(OpsChangeSlotC2S other)
	{
		if (other != null)
		{
			if (other.IsAgree)
			{
				IsAgree = other.IsAgree;
			}
			if (other.ApplyId != 0L)
			{
				ApplyId = other.ApplyId;
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
				IsAgree = input.ReadBool();
				break;
			case 17u:
				ApplyId = input.ReadSFixed64();
				break;
			}
		}
	}
}
