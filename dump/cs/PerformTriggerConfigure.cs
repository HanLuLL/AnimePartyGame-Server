using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class PerformTriggerConfigure : IMessage<PerformTriggerConfigure>, IMessage, IEquatable<PerformTriggerConfigure>, IDeepCloneable<PerformTriggerConfigure>, IBufferMessage
{
	private static readonly MessageParser<PerformTriggerConfigure> _parser = new MessageParser<PerformTriggerConfigure>(() => new PerformTriggerConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int PerformTriggerTypeFieldNumber = 2;

	private PerformTriggerType performTriggerType_;

	public const int PerformTriggerParamsFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_performTriggerParams_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> performTriggerParams_ = new RepeatedField<int>();

	public const int ApplyProbabilityFieldNumber = 4;

	private int applyProbability_;

	public const int PriorityFieldNumber = 5;

	private int priority_;

	public const int PerformTriggerActionTypeFieldNumber = 6;

	private PerformTriggerActionType performTriggerActionType_;

	public const int PerformTriggerActionParamsFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_performTriggerActionParams_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> performTriggerActionParams_ = new RepeatedField<int>();

	public const int TimeDelayFieldNumber = 8;

	private int timeDelay_;

	public const int AudioFieldNumber = 9;

	private static readonly MapField<int, int>.Codec _map_audio_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 74u);

	private readonly MapField<int, int> audio_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PerformTriggerConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PerformReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformTriggerType PerformTriggerType
	{
		get
		{
			return performTriggerType_;
		}
		private set
		{
			performTriggerType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PerformTriggerParams => performTriggerParams_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ApplyProbability
	{
		get
		{
			return applyProbability_;
		}
		private set
		{
			applyProbability_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Priority
	{
		get
		{
			return priority_;
		}
		private set
		{
			priority_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformTriggerActionType PerformTriggerActionType
	{
		get
		{
			return performTriggerActionType_;
		}
		private set
		{
			performTriggerActionType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PerformTriggerActionParams => performTriggerActionParams_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TimeDelay
	{
		get
		{
			return timeDelay_;
		}
		private set
		{
			timeDelay_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Audio => audio_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformTriggerConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformTriggerConfigure(PerformTriggerConfigure other)
		: this()
	{
		id_ = other.id_;
		performTriggerType_ = other.performTriggerType_;
		performTriggerParams_ = other.performTriggerParams_.Clone();
		applyProbability_ = other.applyProbability_;
		priority_ = other.priority_;
		performTriggerActionType_ = other.performTriggerActionType_;
		performTriggerActionParams_ = other.performTriggerActionParams_.Clone();
		timeDelay_ = other.timeDelay_;
		audio_ = other.audio_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformTriggerConfigure Clone()
	{
		return new PerformTriggerConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PerformTriggerConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PerformTriggerConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (PerformTriggerType != other.PerformTriggerType)
		{
			return false;
		}
		if (!performTriggerParams_.Equals(other.performTriggerParams_))
		{
			return false;
		}
		if (ApplyProbability != other.ApplyProbability)
		{
			return false;
		}
		if (Priority != other.Priority)
		{
			return false;
		}
		if (PerformTriggerActionType != other.PerformTriggerActionType)
		{
			return false;
		}
		if (!performTriggerActionParams_.Equals(other.performTriggerActionParams_))
		{
			return false;
		}
		if (TimeDelay != other.TimeDelay)
		{
			return false;
		}
		if (!Audio.Equals(other.Audio))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (PerformTriggerType != PerformTriggerType.None)
		{
			num ^= PerformTriggerType.GetHashCode();
		}
		num ^= performTriggerParams_.GetHashCode();
		if (ApplyProbability != 0)
		{
			num ^= ApplyProbability.GetHashCode();
		}
		if (Priority != 0)
		{
			num ^= Priority.GetHashCode();
		}
		if (PerformTriggerActionType != PerformTriggerActionType.None)
		{
			num ^= PerformTriggerActionType.GetHashCode();
		}
		num ^= performTriggerActionParams_.GetHashCode();
		if (TimeDelay != 0)
		{
			num ^= TimeDelay.GetHashCode();
		}
		num ^= Audio.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (PerformTriggerType != PerformTriggerType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)PerformTriggerType);
		}
		performTriggerParams_.WriteTo(ref output, _repeated_performTriggerParams_codec);
		if (ApplyProbability != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ApplyProbability);
		}
		if (Priority != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Priority);
		}
		if (PerformTriggerActionType != PerformTriggerActionType.None)
		{
			output.WriteRawTag(48);
			output.WriteEnum((int)PerformTriggerActionType);
		}
		performTriggerActionParams_.WriteTo(ref output, _repeated_performTriggerActionParams_codec);
		if (TimeDelay != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(TimeDelay);
		}
		audio_.WriteTo(ref output, _map_audio_codec);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (PerformTriggerType != PerformTriggerType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PerformTriggerType);
		}
		num += performTriggerParams_.CalculateSize(_repeated_performTriggerParams_codec);
		if (ApplyProbability != 0)
		{
			num += 5;
		}
		if (Priority != 0)
		{
			num += 5;
		}
		if (PerformTriggerActionType != PerformTriggerActionType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PerformTriggerActionType);
		}
		num += performTriggerActionParams_.CalculateSize(_repeated_performTriggerActionParams_codec);
		if (TimeDelay != 0)
		{
			num += 5;
		}
		num += audio_.CalculateSize(_map_audio_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PerformTriggerConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.PerformTriggerType != PerformTriggerType.None)
			{
				PerformTriggerType = other.PerformTriggerType;
			}
			performTriggerParams_.Add(other.performTriggerParams_);
			if (other.ApplyProbability != 0)
			{
				ApplyProbability = other.ApplyProbability;
			}
			if (other.Priority != 0)
			{
				Priority = other.Priority;
			}
			if (other.PerformTriggerActionType != PerformTriggerActionType.None)
			{
				PerformTriggerActionType = other.PerformTriggerActionType;
			}
			performTriggerActionParams_.Add(other.performTriggerActionParams_);
			if (other.TimeDelay != 0)
			{
				TimeDelay = other.TimeDelay;
			}
			audio_.MergeFrom(other.audio_);
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
				Id = input.ReadSFixed32();
				break;
			case 16u:
				PerformTriggerType = (PerformTriggerType)input.ReadEnum();
				break;
			case 26u:
			case 29u:
				performTriggerParams_.AddEntriesFrom(ref input, _repeated_performTriggerParams_codec);
				break;
			case 37u:
				ApplyProbability = input.ReadSFixed32();
				break;
			case 45u:
				Priority = input.ReadSFixed32();
				break;
			case 48u:
				PerformTriggerActionType = (PerformTriggerActionType)input.ReadEnum();
				break;
			case 58u:
			case 61u:
				performTriggerActionParams_.AddEntriesFrom(ref input, _repeated_performTriggerActionParams_codec);
				break;
			case 69u:
				TimeDelay = input.ReadSFixed32();
				break;
			case 74u:
				audio_.AddEntriesFrom(ref input, _map_audio_codec);
				break;
			}
		}
	}
}
