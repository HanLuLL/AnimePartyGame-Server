using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class ActivityTaskConfigure : IMessage<ActivityTaskConfigure>, IMessage, IEquatable<ActivityTaskConfigure>, IDeepCloneable<ActivityTaskConfigure>, IBufferMessage
{
	private static readonly MessageParser<ActivityTaskConfigure> _parser = new MessageParser<ActivityTaskConfigure>(() => new ActivityTaskConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int FrontIDFieldNumber = 2;

	private int frontID_;

	public const int OrderWeightFieldNumber = 3;

	private int orderWeight_;

	public const int BeginTimeFieldNumber = 4;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 5;

	private Timestamp endTime_;

	public const int ConditionTypeFieldNumber = 6;

	private ConditionType conditionType_;

	public const int ParamFieldNumber = 7;

	private int param_;

	public const int RefFieldNumber = 8;

	private int ref_;

	public const int NameIDFieldNumber = 9;

	private int nameID_;

	public const int DescIdFieldNumber = 10;

	private int descId_;

	public const int RewardFieldNumber = 11;

	private static readonly MapField<int, int>.Codec _map_reward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 90u);

	private readonly MapField<int, int> reward_ = new MapField<int, int>();

	public const int WayFieldNumber = 12;

	private int way_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityTaskConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[2];

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
	public int FrontID
	{
		get
		{
			return frontID_;
		}
		private set
		{
			frontID_ = value;
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
	public int Param
	{
		get
		{
			return param_;
		}
		private set
		{
			param_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Ref
	{
		get
		{
			return ref_;
		}
		private set
		{
			ref_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

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
	public ActivityTaskConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityTaskConfigure(ActivityTaskConfigure other)
		: this()
	{
		id_ = other.id_;
		frontID_ = other.frontID_;
		orderWeight_ = other.orderWeight_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		conditionType_ = other.conditionType_;
		param_ = other.param_;
		ref_ = other.ref_;
		nameID_ = other.nameID_;
		descId_ = other.descId_;
		reward_ = other.reward_.Clone();
		way_ = other.way_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityTaskConfigure Clone()
	{
		return new ActivityTaskConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityTaskConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityTaskConfigure other)
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
		if (FrontID != other.FrontID)
		{
			return false;
		}
		if (OrderWeight != other.OrderWeight)
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
		if (ConditionType != other.ConditionType)
		{
			return false;
		}
		if (Param != other.Param)
		{
			return false;
		}
		if (Ref != other.Ref)
		{
			return false;
		}
		if (NameID != other.NameID)
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
		if (FrontID != 0)
		{
			num ^= FrontID.GetHashCode();
		}
		if (OrderWeight != 0)
		{
			num ^= OrderWeight.GetHashCode();
		}
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (ConditionType != ConditionType.None)
		{
			num ^= ConditionType.GetHashCode();
		}
		if (Param != 0)
		{
			num ^= Param.GetHashCode();
		}
		if (Ref != 0)
		{
			num ^= Ref.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
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
		if (FrontID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(FrontID);
		}
		if (OrderWeight != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(OrderWeight);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(34);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(42);
			output.WriteMessage(EndTime);
		}
		if (ConditionType != ConditionType.None)
		{
			output.WriteRawTag(48);
			output.WriteEnum((int)ConditionType);
		}
		if (Param != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Param);
		}
		if (Ref != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(Ref);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(NameID);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(DescId);
		}
		reward_.WriteTo(ref output, _map_reward_codec);
		if (Way != 0)
		{
			output.WriteRawTag(101);
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
		if (FrontID != 0)
		{
			num += 5;
		}
		if (OrderWeight != 0)
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
		if (ConditionType != ConditionType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ConditionType);
		}
		if (Param != 0)
		{
			num += 5;
		}
		if (Ref != 0)
		{
			num += 5;
		}
		if (NameID != 0)
		{
			num += 5;
		}
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
	public void MergeFrom(ActivityTaskConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0)
		{
			Id = other.Id;
		}
		if (other.FrontID != 0)
		{
			FrontID = other.FrontID;
		}
		if (other.OrderWeight != 0)
		{
			OrderWeight = other.OrderWeight;
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
		if (other.ConditionType != ConditionType.None)
		{
			ConditionType = other.ConditionType;
		}
		if (other.Param != 0)
		{
			Param = other.Param;
		}
		if (other.Ref != 0)
		{
			Ref = other.Ref;
		}
		if (other.NameID != 0)
		{
			NameID = other.NameID;
		}
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
				FrontID = input.ReadSFixed32();
				break;
			case 29u:
				OrderWeight = input.ReadSFixed32();
				break;
			case 34u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 42u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 48u:
				ConditionType = (ConditionType)input.ReadEnum();
				break;
			case 61u:
				Param = input.ReadSFixed32();
				break;
			case 69u:
				Ref = input.ReadSFixed32();
				break;
			case 77u:
				NameID = input.ReadSFixed32();
				break;
			case 85u:
				DescId = input.ReadSFixed32();
				break;
			case 90u:
				reward_.AddEntriesFrom(ref input, _map_reward_codec);
				break;
			case 101u:
				Way = input.ReadSFixed32();
				break;
			}
		}
	}
}
