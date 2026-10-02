using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class EventThrowDiceC2S : IMessage<EventThrowDiceC2S>, IMessage, IEquatable<EventThrowDiceC2S>, IDeepCloneable<EventThrowDiceC2S>, IBufferMessage
{
	private static readonly MessageParser<EventThrowDiceC2S> _parser = new MessageParser<EventThrowDiceC2S>(() => new EventThrowDiceC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int EventIdFieldNumber = 2;

	private int eventId_;

	public const int DevPointFieldNumber = 3;

	private int devPoint_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<EventThrowDiceC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[298];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionInfo Info
	{
		get
		{
			return info_;
		}
		set
		{
			info_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EventId
	{
		get
		{
			return eventId_;
		}
		set
		{
			eventId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DevPoint
	{
		get
		{
			return devPoint_;
		}
		set
		{
			devPoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventThrowDiceC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventThrowDiceC2S(EventThrowDiceC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		eventId_ = other.eventId_;
		devPoint_ = other.devPoint_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventThrowDiceC2S Clone()
	{
		return new EventThrowDiceC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as EventThrowDiceC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(EventThrowDiceC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Info, other.Info))
		{
			return false;
		}
		if (EventId != other.EventId)
		{
			return false;
		}
		if (DevPoint != other.DevPoint)
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
		if (info_ != null)
		{
			num ^= Info.GetHashCode();
		}
		if (EventId != 0)
		{
			num ^= EventId.GetHashCode();
		}
		if (DevPoint != 0)
		{
			num ^= DevPoint.GetHashCode();
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
		if (info_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Info);
		}
		if (EventId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(EventId);
		}
		if (DevPoint != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DevPoint);
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
		if (info_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Info);
		}
		if (EventId != 0)
		{
			num += 5;
		}
		if (DevPoint != 0)
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
	public void MergeFrom(EventThrowDiceC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.info_ != null)
		{
			if (info_ == null)
			{
				Info = new ActionInfo();
			}
			Info.MergeFrom(other.Info);
		}
		if (other.EventId != 0)
		{
			EventId = other.EventId;
		}
		if (other.DevPoint != 0)
		{
			DevPoint = other.DevPoint;
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
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				break;
			case 10u:
				if (info_ == null)
				{
					Info = new ActionInfo();
				}
				input.ReadMessage(Info);
				break;
			case 21u:
				EventId = input.ReadSFixed32();
				break;
			case 29u:
				DevPoint = input.ReadSFixed32();
				break;
			}
		}
	}
}
