using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class BuffInterrupt : IMessage<BuffInterrupt>, IMessage, IEquatable<BuffInterrupt>, IDeepCloneable<BuffInterrupt>, IBufferMessage
{
	private static readonly MessageParser<BuffInterrupt> _parser = new MessageParser<BuffInterrupt>(() => new BuffInterrupt());

	private UnknownFieldSet _unknownFields;

	public const int BuffSnFieldNumber = 1;

	private long buffSn_;

	public const int NextBuffSnFieldNumber = 2;

	private long nextBuffSn_;

	public const int FFieldNumber = 3;

	private BuffFrom f_;

	public const int TriggerTypeFieldNumber = 4;

	private int triggerType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BuffInterrupt> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[87];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long BuffSn
	{
		get
		{
			return buffSn_;
		}
		set
		{
			buffSn_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long NextBuffSn
	{
		get
		{
			return nextBuffSn_;
		}
		set
		{
			nextBuffSn_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuffFrom F
	{
		get
		{
			return f_;
		}
		set
		{
			f_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TriggerType
	{
		get
		{
			return triggerType_;
		}
		set
		{
			triggerType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuffInterrupt()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuffInterrupt(BuffInterrupt other)
		: this()
	{
		buffSn_ = other.buffSn_;
		nextBuffSn_ = other.nextBuffSn_;
		f_ = other.f_;
		triggerType_ = other.triggerType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuffInterrupt Clone()
	{
		return new BuffInterrupt(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BuffInterrupt);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BuffInterrupt other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (BuffSn != other.BuffSn)
		{
			return false;
		}
		if (NextBuffSn != other.NextBuffSn)
		{
			return false;
		}
		if (F != other.F)
		{
			return false;
		}
		if (TriggerType != other.TriggerType)
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
		if (BuffSn != 0L)
		{
			num ^= BuffSn.GetHashCode();
		}
		if (NextBuffSn != 0L)
		{
			num ^= NextBuffSn.GetHashCode();
		}
		if (F != BuffFrom.None)
		{
			num ^= F.GetHashCode();
		}
		if (TriggerType != 0)
		{
			num ^= TriggerType.GetHashCode();
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
		if (BuffSn != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(BuffSn);
		}
		if (NextBuffSn != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(NextBuffSn);
		}
		if (F != BuffFrom.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)F);
		}
		if (TriggerType != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(TriggerType);
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
		if (BuffSn != 0L)
		{
			num += 9;
		}
		if (NextBuffSn != 0L)
		{
			num += 9;
		}
		if (F != BuffFrom.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)F);
		}
		if (TriggerType != 0)
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
	public void MergeFrom(BuffInterrupt other)
	{
		if (other != null)
		{
			if (other.BuffSn != 0L)
			{
				BuffSn = other.BuffSn;
			}
			if (other.NextBuffSn != 0L)
			{
				NextBuffSn = other.NextBuffSn;
			}
			if (other.F != BuffFrom.None)
			{
				F = other.F;
			}
			if (other.TriggerType != 0)
			{
				TriggerType = other.TriggerType;
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
			case 9u:
				BuffSn = input.ReadSFixed64();
				break;
			case 17u:
				NextBuffSn = input.ReadSFixed64();
				break;
			case 24u:
				F = (BuffFrom)input.ReadEnum();
				break;
			case 37u:
				TriggerType = input.ReadSFixed32();
				break;
			}
		}
	}
}
