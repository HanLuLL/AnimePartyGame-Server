using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ActivityInfo : IMessage<ActivityInfo>, IMessage, IEquatable<ActivityInfo>, IDeepCloneable<ActivityInfo>, IBufferMessage
{
	private static readonly MessageParser<ActivityInfo> _parser = new MessageParser<ActivityInfo>(() => new ActivityInfo());

	private UnknownFieldSet _unknownFields;

	public const int InfoIdFieldNumber = 1;

	private int infoId_;

	public const int ConditionFieldNumber = 5;

	private static readonly MapField<int, int>.Codec _map_condition_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 42u);

	private readonly MapField<int, int> condition_ = new MapField<int, int>();

	public const int TaskRewardIsFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_taskRewardIs_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> taskRewardIs_ = new RepeatedField<int>();

	public const int Condition1FieldNumber = 7;

	private static readonly MapField<int, ConditionData>.Codec _map_condition1_codec = new MapField<int, ConditionData>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ConditionData.Parser), 58u);

	private readonly MapField<int, ConditionData> condition1_ = new MapField<int, ConditionData>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[45];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int InfoId
	{
		get
		{
			return infoId_;
		}
		set
		{
			infoId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Condition => condition_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TaskRewardIs => taskRewardIs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ConditionData> Condition1 => condition1_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityInfo(ActivityInfo other)
		: this()
	{
		infoId_ = other.infoId_;
		condition_ = other.condition_.Clone();
		taskRewardIs_ = other.taskRewardIs_.Clone();
		condition1_ = other.condition1_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityInfo Clone()
	{
		return new ActivityInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (InfoId != other.InfoId)
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
		if (InfoId != 0)
		{
			num ^= InfoId.GetHashCode();
		}
		num ^= Condition.GetHashCode();
		num ^= taskRewardIs_.GetHashCode();
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
		if (InfoId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(InfoId);
		}
		condition_.WriteTo(ref output, _map_condition_codec);
		taskRewardIs_.WriteTo(ref output, _repeated_taskRewardIs_codec);
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
		if (InfoId != 0)
		{
			num += 5;
		}
		num += condition_.CalculateSize(_map_condition_codec);
		num += taskRewardIs_.CalculateSize(_repeated_taskRewardIs_codec);
		num += condition1_.CalculateSize(_map_condition1_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ActivityInfo other)
	{
		if (other != null)
		{
			if (other.InfoId != 0)
			{
				InfoId = other.InfoId;
			}
			condition_.MergeFrom(other.condition_);
			taskRewardIs_.Add(other.taskRewardIs_);
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
				InfoId = input.ReadSFixed32();
				break;
			case 42u:
				condition_.AddEntriesFrom(ref input, _map_condition_codec);
				break;
			case 50u:
			case 53u:
				taskRewardIs_.AddEntriesFrom(ref input, _repeated_taskRewardIs_codec);
				break;
			case 58u:
				condition1_.AddEntriesFrom(ref input, _map_condition1_codec);
				break;
			}
		}
	}
}
