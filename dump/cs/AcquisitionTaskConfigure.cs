using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class AcquisitionTaskConfigure : IMessage<AcquisitionTaskConfigure>, IMessage, IEquatable<AcquisitionTaskConfigure>, IDeepCloneable<AcquisitionTaskConfigure>, IBufferMessage
{
	private static readonly MessageParser<AcquisitionTaskConfigure> _parser = new MessageParser<AcquisitionTaskConfigure>(() => new AcquisitionTaskConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int OrderWeightFieldNumber = 2;

	private int orderWeight_;

	public const int ConditionTypeFieldNumber = 3;

	private ConditionType conditionType_;

	public const int ParamsFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	public const int DescIdFieldNumber = 5;

	private int descId_;

	public const int RewardFieldNumber = 6;

	private static readonly MapField<int, int>.Codec _map_reward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 50u);

	private readonly MapField<int, int> reward_ = new MapField<int, int>();

	public const int WayFieldNumber = 7;

	private int way_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AcquisitionTaskConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AcquisitionReflection.Descriptor.MessageTypes[1];

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
	public int OrderWeight
	{
		get
		{
			return orderWeight_;
		}
		private set
		{
			orderWeight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConditionType ConditionType
	{
		get
		{
			return conditionType_;
		}
		private set
		{
			conditionType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescId
	{
		get
		{
			return descId_;
		}
		private set
		{
			descId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Reward => reward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Way
	{
		get
		{
			return way_;
		}
		private set
		{
			way_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionTaskConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionTaskConfigure(AcquisitionTaskConfigure other)
		: this()
	{
		id_ = other.id_;
		orderWeight_ = other.orderWeight_;
		conditionType_ = other.conditionType_;
		params_ = other.params_.Clone();
		descId_ = other.descId_;
		reward_ = other.reward_.Clone();
		way_ = other.way_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionTaskConfigure Clone()
	{
		return new AcquisitionTaskConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AcquisitionTaskConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AcquisitionTaskConfigure other)
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
		if (OrderWeight != other.OrderWeight)
		{
			return false;
		}
		if (ConditionType != other.ConditionType)
		{
			return false;
		}
		if (!params_.Equals(other.params_))
		{
			return false;
		}
		if (DescId != other.DescId)
		{
			return false;
		}
		if (!Reward.Equals(other.Reward))
		{
			return false;
		}
		if (Way != other.Way)
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
		if (OrderWeight != 0)
		{
			num ^= OrderWeight.GetHashCode();
		}
		if (ConditionType != ConditionType.None)
		{
			num ^= ConditionType.GetHashCode();
		}
		num ^= params_.GetHashCode();
		if (DescId != 0)
		{
			num ^= DescId.GetHashCode();
		}
		num ^= Reward.GetHashCode();
		if (Way != 0)
		{
			num ^= Way.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (OrderWeight != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(OrderWeight);
		}
		if (ConditionType != ConditionType.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)ConditionType);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
		if (DescId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DescId);
		}
		reward_.WriteTo(ref output, _map_reward_codec);
		if (Way != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Way);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (OrderWeight != 0)
		{
			num += 5;
		}
		if (ConditionType != ConditionType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ConditionType);
		}
		num += params_.CalculateSize(_repeated_params_codec);
		if (DescId != 0)
		{
			num += 5;
		}
		num += reward_.CalculateSize(_map_reward_codec);
		if (Way != 0)
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
	public void MergeFrom(AcquisitionTaskConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.OrderWeight != 0)
			{
				OrderWeight = other.OrderWeight;
			}
			if (other.ConditionType != ConditionType.None)
			{
				ConditionType = other.ConditionType;
			}
			params_.Add(other.params_);
			if (other.DescId != 0)
			{
				DescId = other.DescId;
			}
			reward_.MergeFrom(other.reward_);
			if (other.Way != 0)
			{
				Way = other.Way;
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
			case 13u:
				Id = input.ReadSFixed32();
				break;
			case 21u:
				OrderWeight = input.ReadSFixed32();
				break;
			case 24u:
				ConditionType = (ConditionType)input.ReadEnum();
				break;
			case 34u:
			case 37u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 45u:
				DescId = input.ReadSFixed32();
				break;
			case 50u:
				reward_.AddEntriesFrom(ref input, _map_reward_codec);
				break;
			case 61u:
				Way = input.ReadSFixed32();
				break;
			}
		}
	}
}
