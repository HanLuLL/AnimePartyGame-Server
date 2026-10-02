using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class FixMapMapLevelConfigureItem : IMessage<FixMapMapLevelConfigureItem>, IMessage, IEquatable<FixMapMapLevelConfigureItem>, IDeepCloneable<FixMapMapLevelConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<FixMapMapLevelConfigureItem> _parser = new MessageParser<FixMapMapLevelConfigureItem>(() => new FixMapMapLevelConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int BeginTimeFieldNumber = 2;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 3;

	private Timestamp endTime_;

	public const int BeginTimeMutatorFieldNumber = 4;

	private Timestamp beginTimeMutator_;

	public const int EndTimeMutatorFieldNumber = 5;

	private Timestamp endTimeMutator_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixMapMapLevelConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixMapReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Index
	{
		get
		{
			return index_;
		}
		private set
		{
			index_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp BeginTime
	{
		get
		{
			return beginTime_;
		}
		private set
		{
			beginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTime
	{
		get
		{
			return endTime_;
		}
		private set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp BeginTimeMutator
	{
		get
		{
			return beginTimeMutator_;
		}
		private set
		{
			beginTimeMutator_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTimeMutator
	{
		get
		{
			return endTimeMutator_;
		}
		private set
		{
			endTimeMutator_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixMapMapLevelConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixMapMapLevelConfigureItem(FixMapMapLevelConfigureItem other)
		: this()
	{
		index_ = other.index_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		beginTimeMutator_ = ((other.beginTimeMutator_ != null) ? other.beginTimeMutator_.Clone() : null);
		endTimeMutator_ = ((other.endTimeMutator_ != null) ? other.endTimeMutator_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixMapMapLevelConfigureItem Clone()
	{
		return new FixMapMapLevelConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixMapMapLevelConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixMapMapLevelConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Index != other.Index)
		{
			return false;
		}
		if (!object.Equals(BeginTime, other.BeginTime))
		{
			return false;
		}
		if (!object.Equals(EndTime, other.EndTime))
		{
			return false;
		}
		if (!object.Equals(BeginTimeMutator, other.BeginTimeMutator))
		{
			return false;
		}
		if (!object.Equals(EndTimeMutator, other.EndTimeMutator))
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
		if (Index != 0)
		{
			num ^= Index.GetHashCode();
		}
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (beginTimeMutator_ != null)
		{
			num ^= BeginTimeMutator.GetHashCode();
		}
		if (endTimeMutator_ != null)
		{
			num ^= EndTimeMutator.GetHashCode();
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
		if (Index != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Index);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(EndTime);
		}
		if (beginTimeMutator_ != null)
		{
			output.WriteRawTag(34);
			output.WriteMessage(BeginTimeMutator);
		}
		if (endTimeMutator_ != null)
		{
			output.WriteRawTag(42);
			output.WriteMessage(EndTimeMutator);
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
		if (Index != 0)
		{
			num += 5;
		}
		if (beginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (beginTimeMutator_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTimeMutator);
		}
		if (endTimeMutator_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTimeMutator);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixMapMapLevelConfigureItem other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Index != 0)
		{
			Index = other.Index;
		}
		if (other.beginTime_ != null)
		{
			if (beginTime_ == null)
			{
				BeginTime = new Timestamp();
			}
			BeginTime.MergeFrom(other.BeginTime);
		}
		if (other.endTime_ != null)
		{
			if (endTime_ == null)
			{
				EndTime = new Timestamp();
			}
			EndTime.MergeFrom(other.EndTime);
		}
		if (other.beginTimeMutator_ != null)
		{
			if (beginTimeMutator_ == null)
			{
				BeginTimeMutator = new Timestamp();
			}
			BeginTimeMutator.MergeFrom(other.BeginTimeMutator);
		}
		if (other.endTimeMutator_ != null)
		{
			if (endTimeMutator_ == null)
			{
				EndTimeMutator = new Timestamp();
			}
			EndTimeMutator.MergeFrom(other.EndTimeMutator);
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
			case 13u:
				Index = input.ReadSFixed32();
				break;
			case 18u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 26u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 34u:
				if (beginTimeMutator_ == null)
				{
					BeginTimeMutator = new Timestamp();
				}
				input.ReadMessage(BeginTimeMutator);
				break;
			case 42u:
				if (endTimeMutator_ == null)
				{
					EndTimeMutator = new Timestamp();
				}
				input.ReadMessage(EndTimeMutator);
				break;
			}
		}
	}
}
