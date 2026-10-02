using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class LoopNoticeS2C : IMessage<LoopNoticeS2C>, IMessage, IEquatable<LoopNoticeS2C>, IDeepCloneable<LoopNoticeS2C>, IBufferMessage
{
	private static readonly MessageParser<LoopNoticeS2C> _parser = new MessageParser<LoopNoticeS2C>(() => new LoopNoticeS2C());

	private UnknownFieldSet _unknownFields;

	public const int ContextFieldNumber = 1;

	private string context_ = "";

	public const int IntervalFieldNumber = 2;

	private int interval_;

	public const int StartTimeFieldNumber = 3;

	private long startTime_;

	public const int EndTimeFieldNumber = 4;

	private long endTime_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LoopNoticeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[141];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Interval
	{
		get
		{
			return interval_;
		}
		set
		{
			interval_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long StartTime
	{
		get
		{
			return startTime_;
		}
		set
		{
			startTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long EndTime
	{
		get
		{
			return endTime_;
		}
		set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LoopNoticeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LoopNoticeS2C(LoopNoticeS2C other)
		: this()
	{
		context_ = other.context_;
		interval_ = other.interval_;
		startTime_ = other.startTime_;
		endTime_ = other.endTime_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LoopNoticeS2C Clone()
	{
		return new LoopNoticeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LoopNoticeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LoopNoticeS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Context != other.Context)
		{
			return false;
		}
		if (Interval != other.Interval)
		{
			return false;
		}
		if (StartTime != other.StartTime)
		{
			return false;
		}
		if (EndTime != other.EndTime)
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
		if (Context.Length != 0)
		{
			num ^= Context.GetHashCode();
		}
		if (Interval != 0)
		{
			num ^= Interval.GetHashCode();
		}
		if (StartTime != 0L)
		{
			num ^= StartTime.GetHashCode();
		}
		if (EndTime != 0L)
		{
			num ^= EndTime.GetHashCode();
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
		if (Context.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(Context);
		}
		if (Interval != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Interval);
		}
		if (StartTime != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(StartTime);
		}
		if (EndTime != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(EndTime);
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
		if (Context.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Context);
		}
		if (Interval != 0)
		{
			num += 5;
		}
		if (StartTime != 0L)
		{
			num += 9;
		}
		if (EndTime != 0L)
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
	public void MergeFrom(LoopNoticeS2C other)
	{
		if (other != null)
		{
			if (other.Context.Length != 0)
			{
				Context = other.Context;
			}
			if (other.Interval != 0)
			{
				Interval = other.Interval;
			}
			if (other.StartTime != 0L)
			{
				StartTime = other.StartTime;
			}
			if (other.EndTime != 0L)
			{
				EndTime = other.EndTime;
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
				Context = input.ReadString();
				break;
			case 21u:
				Interval = input.ReadSFixed32();
				break;
			case 25u:
				StartTime = input.ReadSFixed64();
				break;
			case 33u:
				EndTime = input.ReadSFixed64();
				break;
			}
		}
	}
}
