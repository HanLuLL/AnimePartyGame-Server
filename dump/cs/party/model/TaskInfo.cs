using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class TaskInfo : IMessage<TaskInfo>, IMessage, IEquatable<TaskInfo>, IDeepCloneable<TaskInfo>, IBufferMessage
{
	private static readonly MessageParser<TaskInfo> _parser = new MessageParser<TaskInfo>(() => new TaskInfo());

	private UnknownFieldSet _unknownFields;

	public const int ProgressRewardFieldNumber = 1;

	private int progressReward_;

	public const int IsTeachingFieldNumber = 2;

	private bool isTeaching_;

	public const int WeekConditionFieldNumber = 6;

	private static readonly MapField<int, int>.Codec _map_weekCondition_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 50u);

	private readonly MapField<int, int> weekCondition_ = new MapField<int, int>();

	public const int ConditionFieldNumber = 7;

	private static readonly MapField<int, int>.Codec _map_condition_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 58u);

	private readonly MapField<int, int> condition_ = new MapField<int, int>();

	public const int TaskRewardIsFieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_taskRewardIs_codec = FieldCodec.ForSFixed32(66u);

	private readonly RepeatedField<int> taskRewardIs_ = new RepeatedField<int>();

	public const int AchieveRewardIsFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_achieveRewardIs_codec = FieldCodec.ForSFixed32(74u);

	private readonly RepeatedField<int> achieveRewardIs_ = new RepeatedField<int>();

	public const int WeekTaskRewardIsFieldNumber = 10;

	private static readonly FieldCodec<int> _repeated_weekTaskRewardIs_codec = FieldCodec.ForSFixed32(82u);

	private readonly RepeatedField<int> weekTaskRewardIs_ = new RepeatedField<int>();

	public const int Condition1FieldNumber = 11;

	private static readonly MapField<int, ConditionData>.Codec _map_condition1_codec = new MapField<int, ConditionData>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ConditionData.Parser), 90u);

	private readonly MapField<int, ConditionData> condition1_ = new MapField<int, ConditionData>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TaskInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[42];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ProgressReward
	{
		get
		{
			return progressReward_;
		}
		set
		{
			progressReward_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsTeaching
	{
		get
		{
			return isTeaching_;
		}
		set
		{
			isTeaching_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> WeekCondition => weekCondition_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Condition => condition_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TaskRewardIs => taskRewardIs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> AchieveRewardIs => achieveRewardIs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> WeekTaskRewardIs => weekTaskRewardIs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ConditionData> Condition1 => condition1_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskInfo(TaskInfo other)
		: this()
	{
		progressReward_ = other.progressReward_;
		isTeaching_ = other.isTeaching_;
		weekCondition_ = other.weekCondition_.Clone();
		condition_ = other.condition_.Clone();
		taskRewardIs_ = other.taskRewardIs_.Clone();
		achieveRewardIs_ = other.achieveRewardIs_.Clone();
		weekTaskRewardIs_ = other.weekTaskRewardIs_.Clone();
		condition1_ = other.condition1_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskInfo Clone()
	{
		return new TaskInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TaskInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TaskInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ProgressReward != other.ProgressReward)
		{
			return false;
		}
		if (IsTeaching != other.IsTeaching)
		{
			return false;
		}
		if (!WeekCondition.Equals(other.WeekCondition))
		{
			return false;
		}
		if (!Condition.Equals(other.Condition))
		{
			return false;
		}
		if (!taskRewardIs_.Equals(other.taskRewardIs_))
		{
			return false;
		}
		if (!achieveRewardIs_.Equals(other.achieveRewardIs_))
		{
			return false;
		}
		if (!weekTaskRewardIs_.Equals(other.weekTaskRewardIs_))
		{
			return false;
		}
		if (!Condition1.Equals(other.Condition1))
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
		if (ProgressReward != 0)
		{
			num ^= ProgressReward.GetHashCode();
		}
		if (IsTeaching)
		{
			num ^= IsTeaching.GetHashCode();
		}
		num ^= WeekCondition.GetHashCode();
		num ^= Condition.GetHashCode();
		num ^= taskRewardIs_.GetHashCode();
		num ^= achieveRewardIs_.GetHashCode();
		num ^= weekTaskRewardIs_.GetHashCode();
		num ^= Condition1.GetHashCode();
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
		if (ProgressReward != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ProgressReward);
		}
		if (IsTeaching)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsTeaching);
		}
		weekCondition_.WriteTo(ref output, _map_weekCondition_codec);
		condition_.WriteTo(ref output, _map_condition_codec);
		taskRewardIs_.WriteTo(ref output, _repeated_taskRewardIs_codec);
		achieveRewardIs_.WriteTo(ref output, _repeated_achieveRewardIs_codec);
		weekTaskRewardIs_.WriteTo(ref output, _repeated_weekTaskRewardIs_codec);
		condition1_.WriteTo(ref output, _map_condition1_codec);
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
		if (ProgressReward != 0)
		{
			num += 5;
		}
		if (IsTeaching)
		{
			num += 2;
		}
		num += weekCondition_.CalculateSize(_map_weekCondition_codec);
		num += condition_.CalculateSize(_map_condition_codec);
		num += taskRewardIs_.CalculateSize(_repeated_taskRewardIs_codec);
		num += achieveRewardIs_.CalculateSize(_repeated_achieveRewardIs_codec);
		num += weekTaskRewardIs_.CalculateSize(_repeated_weekTaskRewardIs_codec);
		num += condition1_.CalculateSize(_map_condition1_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(TaskInfo other)
	{
		if (other != null)
		{
			if (other.ProgressReward != 0)
			{
				ProgressReward = other.ProgressReward;
			}
			if (other.IsTeaching)
			{
				IsTeaching = other.IsTeaching;
			}
			weekCondition_.MergeFrom(other.weekCondition_);
			condition_.MergeFrom(other.condition_);
			taskRewardIs_.Add(other.taskRewardIs_);
			achieveRewardIs_.Add(other.achieveRewardIs_);
			weekTaskRewardIs_.Add(other.weekTaskRewardIs_);
			condition1_.MergeFrom(other.condition1_);
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
				ProgressReward = input.ReadSFixed32();
				break;
			case 16u:
				IsTeaching = input.ReadBool();
				break;
			case 50u:
				weekCondition_.AddEntriesFrom(ref input, _map_weekCondition_codec);
				break;
			case 58u:
				condition_.AddEntriesFrom(ref input, _map_condition_codec);
				break;
			case 66u:
			case 69u:
				taskRewardIs_.AddEntriesFrom(ref input, _repeated_taskRewardIs_codec);
				break;
			case 74u:
			case 77u:
				achieveRewardIs_.AddEntriesFrom(ref input, _repeated_achieveRewardIs_codec);
				break;
			case 82u:
			case 85u:
				weekTaskRewardIs_.AddEntriesFrom(ref input, _repeated_weekTaskRewardIs_codec);
				break;
			case 90u:
				condition1_.AddEntriesFrom(ref input, _map_condition1_codec);
				break;
			}
		}
	}
}
